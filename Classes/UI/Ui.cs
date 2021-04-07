using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChessB
{
    public static class Ui
    {
        public static int squareSize = 70;

        public static double resetPositionX;
        public static double resetPositionY;

        public static Point mousePos;
        public static Window MainWindow;
        public static Canvas canvas;

        public static MoveableImage imageSelected = null;
        public static Piece pieceSelected;
        public static List<Image> validMoveImages = new List<Image>();
        public static Grid grid;

        public static Grid whiteImageGrid;
        public static Grid blackImageGrid;

        public static int numBlackPieceCaptured = 0;
        public static int numWhitePieceCaptured = 0;

        public static List<Tuple<Image, int>> blackCapturedImagesList = new List<Tuple<Image, int>>();
        public static List<Tuple<Image, int>> whiteCapturedImageList = new List<Tuple<Image, int>>();


        public static Label blackScoreLabel = new Label();
        public static Label whiteScoreLabel = new Label();

        public static int blackScore = 0;
        public static int whiteScore = 0;

        public static List<Image> whiteCapturedImages = new List<Image>();
        public static List<Image> blackCapturedImages = new List<Image>();

        public static ListView moveList = new ListView();

        public static Image checkImageToRemove;
        public static List<Image> hightlightImages = new List<Image>();

        public static UIElement tileSelected;
        public static Point ArrowStartingPosition;

        public static List<Line> arrows = new List<Line>();
        public static List<Polyline> polyArrows = new List<Polyline>();

        public static List<Image> markedImages = new List<Image>();
        public static List<int> markedImageLocations = new List<int>();

        public static string whiteUpgradeChoice;
        public static string blackUpgradeChoice;

        public static ListBox moveListBox;
        public static Image hoveredTileImage;
        public static void drawBoardGrid(Board board, Canvas canvas)
        {

            Piece[] piece = board.getPiece();

            for (int i = board.getBoardSize() * board.getBoardSize() - 1; i >= 0; i--)
            {

                int xLocation = (i) % ((board.getBoardSize()));
                int yLocation = (int)(i / board.getBoardSize());

                Point nextLocation = new Point(xLocation * squareSize, yLocation * squareSize);
                string whiteTileImageURL = "C:/Users/tompo/source/repos/ChessB/Images/whiteTournament.PNG";
                string blackTileImageURL = "C:/Users/tompo/source/repos/ChessB/Images/greenTournament.PNG";

                String nextImageURL = ((xLocation + yLocation) % 2 == 1) ? blackTileImageURL : whiteTileImageURL;

                SolidColorBrush blackBrush = new SolidColorBrush();


                Tile tile = new Tile();
                ImageSource tileImage = new BitmapImage(new Uri(nextImageURL));
                tile.Source = tileImage;
                tile.Width = squareSize;
                tile.Height = squareSize;
                Canvas.SetZIndex(tile, 1);

                Canvas.SetTop(tile, yLocation * squareSize);
                Canvas.SetLeft(tile, xLocation * squareSize);
                canvas.Children.Add(tile);
            }
        }


        public static void drawUi(Board board, Canvas canvas)
        {
            //removes everything from board canvas except to tile images
            for (int i = 0; i < canvas.Children.Count; i++)
            {
                if (!(canvas.Children[i] is Tile))
                {
                    canvas.Children.RemoveAt(i);
                    i--;
                }
            }

            //draw board pieces to board
            Piece[] piece = board.getPiece();
            for (int i = board.getBoardSize() * board.getBoardSize() - 1; i >= 0; i--)
            {
                int xLocation = (i) % ((board.getBoardSize()));
                int yLocation = (int)(i / board.getBoardSize());

                if (piece[i] != null)
                {
                    MoveableImage mpi = new MoveableImage();
                    ImageSource pieceImage = new BitmapImage(new Uri(piece[i].getImagePath()));

                    mpi.Source = pieceImage;
                    mpi.Width = squareSize;
                    mpi.Height = squareSize;

                    Canvas.SetZIndex(mpi, 1500);
                    // Canvas.SetTop(mpi, (board.getBoardSize() - 1 - yLocation) * squareSize);
                    Canvas.SetTop(mpi, (board.getBoardSize() - 1 - yLocation) * squareSize);
                    Canvas.SetLeft(mpi, xLocation * squareSize);
                    canvas.Children.Add(mpi);
                    mpi.name = piece[i].getImagePath();
                    piece[i].setImage(mpi);
                }



            }


            blackCapturedImagesList.Clear();
            whiteCapturedImageList.Clear();
            int j = 0;
            //adds all captured pieces to the UI
            foreach (MoveableImage image in board.getBlackCapturedImages())
            {

                Ui.addWhiteCaptureImage(image, board.getBlackCaptureValues()[j]);
                j++;
            }

            j = 0;
            foreach (MoveableImage image in board.getWhiteCapturedImages())
            {
                Ui.addBlackCaptureImage(image, board.getWhiteCaptureValues()[j]);
                j++;
            }

            //draw highlights of last move

            if (board.getMoves().Count > 0)
            {

                drawHighlightTile(board.getMoves().Last().getStartLocation());
                drawHighlightTile(board.getMoves().Last().getEndLocation());
            }

            //draw check tile if we are in check
            if (board.getIsWhiteInCheck() == true)
            {
                drawCheckTile(board.getWhiteKingLocation());
            }
            else if (board.getIsBlackInCheck() == true)
            {
                drawCheckTile(board.getBlackKingLocation());
            }

            //genereate board relevant values for strenght labels
            (int, int) strengths = board.generateBoardStrengths();

            if (strengths.Item1 - strengths.Item2 > 0)
            {
                Ui.blackScoreLabel.Content = "+" + (strengths.Item1 - strengths.Item2).ToString();
            }
            else
            {
                Ui.blackScoreLabel.Content = "";
            }

            //Updates score lables
            if (strengths.Item2 - strengths.Item1 > 0)
            {
                Ui.whiteScoreLabel.Content = "+" + (strengths.Item2 - strengths.Item1).ToString();
            }
            else
            {
                Ui.whiteScoreLabel.Content = "";
            }

            moveListBox.Items.Clear();


            //Adds move notations to the move list
            int moveNumber = 0;
            foreach (Move move in board.getMoves())
            {
                moveNumber++;
                moveListBox.Items.Add(moveNumber + "\t" + move.getChessNotation());
            }


            //adds end of game to move list UI;
            if (board.draw == true)
            {
                Ui.moveListBox.Items.Add("1/2 - 1/2");
                return;
            }
            else if (board.whiteWins == true)
            {
                Ui.moveListBox.Items.Add("1 - 0");
            }
            else if (board.blackWins == true)
            {
                Ui.moveListBox.Items.Add("0 - 1");
            }
        }

        public static void drawHoveredTileImage()
        {
            if (!Ui.canvas.Children.Contains(hoveredTileImage))
            {
                Ui.canvas.Children.Add(hoveredTileImage);
            }

            Canvas.SetZIndex(hoveredTileImage, 10000);
            double topPosition = Math.Round(Canvas.GetTop(Ui.imageSelected) / Ui.squareSize) * Ui.squareSize;
            double leftPosition = Math.Round(Canvas.GetLeft(Ui.imageSelected) / Ui.squareSize) * Ui.squareSize;
            Canvas.SetTop(hoveredTileImage, topPosition);
            Canvas.SetLeft(hoveredTileImage, leftPosition);
        }

        public static void removeHoveredTileImage()
        {
            canvas.Children.Remove(hoveredTileImage);
        }
        public static void drawValidMoves()
        {
            validMoveImages = new List<Image>();
            foreach (Move validMove in Game.validMoves)
            {
                int xLocation = (validMove.getEndLocation()) % ((Game.activeBoard.getBoardSize()));
                int yLocation = (int)(validMove.getEndLocation() / Game.activeBoard.getBoardSize());
                String VMImageEmptyTileURL = "C:/Users/tompo/source/repos/ChessB/Images/ValidMoveEmpty.PNG";
                String VMImagePieceTileURL = "C:/Users/tompo/source/repos/ChessB/Images/ValidMovePiece.PNG";
                Image validMoveImage = new Image();

                if (Game.activeBoard.getPiece()[validMove.getEndLocation()] == null)
                {
                    ImageSource validMoveImageSource = new BitmapImage(new Uri(VMImageEmptyTileURL));
                    validMoveImage.Source = validMoveImageSource;
                    validMoveImage.Opacity = 0.5;
                    validMoveImage.Width = squareSize / 2;
                    validMoveImage.Height = squareSize / 2;
                    Canvas.SetZIndex(validMoveImage, 1000);
                    Canvas.SetTop(validMoveImage, ((Game.activeBoard.getBoardSize() - 1 - yLocation) * squareSize + squareSize / 4));
                    Canvas.SetLeft(validMoveImage, xLocation * squareSize + squareSize / 4);
                }
                else
                {
                    ImageSource validMoveImageSource = new BitmapImage(new Uri(VMImagePieceTileURL));
                    validMoveImage.Source = validMoveImageSource;
                    validMoveImage.Width = squareSize;
                    validMoveImage.Height = squareSize;
                    Canvas.SetZIndex(validMoveImage, 1000);
                    Canvas.SetTop(validMoveImage, ((Game.activeBoard.getBoardSize() - 1 - yLocation) * squareSize));
                    Canvas.SetLeft(validMoveImage, xLocation * squareSize);
                }

                canvas.Children.Add(validMoveImage);
                validMoveImages.Add(validMoveImage);

            }
        }

        public static void drawCheckTile(int kingLocation)
        {
            int xLocation = (kingLocation) % ((Game.activeBoard.getBoardSize()));
            int yLocation = (int)(kingLocation / Game.activeBoard.getBoardSize());

            Image checkImage = new Image();
            String checkImageURL = "C:/Users/tompo/source/repos/ChessB/Images/red.PNG";
            ImageSource checkImageSource = new BitmapImage(new Uri(checkImageURL));
            checkImage.Source = checkImageSource;
            checkImage.Width = squareSize;
            checkImage.Height = squareSize;
            Canvas.SetTop(checkImage, ((Game.activeBoard.getBoardSize() - 1 - yLocation) * squareSize));
            Canvas.SetLeft(checkImage, xLocation * squareSize);
            Canvas.SetZIndex(checkImage, 500);
            checkImageToRemove = checkImage;
            canvas.Children.Add(checkImage);
        }

        public static void drawHighlightTile(int highLightLocation)
        {
            int xLocation = (highLightLocation) % ((Game.activeBoard.getBoardSize()));
            int yLocation = (int)(highLightLocation / Game.activeBoard.getBoardSize());

            Image highlightImage = new Image();
            String highlightImageURL = "C:/Users/tompo/source/repos/ChessB/Images/HighlightTile.PNG";
            ImageSource highlightImageSource = new BitmapImage(new Uri(highlightImageURL));
            highlightImage.Source = highlightImageSource;
            highlightImage.Width = squareSize;
            highlightImage.Height = squareSize;
            highlightImage.Opacity = 0.75;
            highlightImage.IsHitTestVisible = false;
            Canvas.SetTop(highlightImage, ((Game.activeBoard.getBoardSize() - 1 - yLocation) * squareSize));
            Canvas.SetLeft(highlightImage, xLocation * squareSize);
            Canvas.SetZIndex(highlightImage, 100);
            hightlightImages.Add(highlightImage);
            canvas.Children.Add(highlightImage);
        }

        public static void addMarkedTile(int markedLocation)
        {
            //if the tile is already marked we remove it.
            if (markedImageLocations.Contains(markedLocation))
            {
                Image imageToRemove = markedImages[markedImageLocations.IndexOf(markedLocation)];
                markedImages.Remove(imageToRemove);
                markedImageLocations.Remove(markedLocation);
                canvas.Children.Remove(imageToRemove);
            }
            else
            {
                int xLocation = (markedLocation) % ((Game.activeBoard.getBoardSize()));
                int yLocation = (int)(markedLocation / Game.activeBoard.getBoardSize());

                Image markedImage = new Image();
                String markedImageURL = "C:/Users/tompo/source/repos/ChessB/Images/Red.PNG";
                ImageSource markedImageSource = new BitmapImage(new Uri(markedImageURL));
                markedImage.Source = markedImageSource;
                markedImage.Width = squareSize;
                markedImage.Height = squareSize;
                markedImage.Opacity = 0.9;
                markedImage.IsHitTestVisible = false;
                Canvas.SetTop(markedImage, ((Game.activeBoard.getBoardSize() - 1 - yLocation) * squareSize));
                Canvas.SetLeft(markedImage, xLocation * squareSize);
                Canvas.SetZIndex(markedImage, 500);

                markedImages.Add(markedImage);
                markedImageLocations.Add(markedLocation);
                canvas.Children.Add(markedImage);
            }


        }

        public static void removeMarkedTiles()
        {
            markedImageLocations.Clear();
            foreach (Image image in markedImages)
            {
                canvas.Children.Remove(image);
            }
        }



        public static void removeHighlightTile()
        {
            foreach (Image image in hightlightImages)
            {
                canvas.Children.Remove(image);

            }
        }

        public static void removeCheckTile()
        {
            canvas.Children.Remove(checkImageToRemove);
        }

        public static void removeValidMovesImages()
        {
            foreach (Image validMoveImage in Ui.validMoveImages)
            {
                canvas.Children.Remove(validMoveImage);
            }
        }

        private static string GetColumnName(int index)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var value = "";

            if (index >= letters.Length)
                value += letters[index / letters.Length - 1];

            value += letters[index % letters.Length];

            return value;
        }



        public static void addBlackCaptureImage(MoveableImage image, int pieceValue)
        {
            blackImageGrid.Children.Clear();
            Grid.SetRow(blackScoreLabel, 0);
            Grid.SetColumn(blackScoreLabel, 0);
            blackImageGrid.Children.Add(blackScoreLabel);
            blackScore = 0;
            blackCapturedImagesList.Add(Tuple.Create<Image, int>(image, pieceValue));
            blackCapturedImagesList.Sort((x, y) => y.Item2.CompareTo(x.Item2));
            int i = 1;
            foreach (Tuple<Image, int> imageTuple in blackCapturedImagesList)
            {
                Grid.SetRow(imageTuple.Item1, 0);
                Grid.SetColumn(imageTuple.Item1, i);
                image.setIsCapturedPiece(true);
                image.IsHitTestVisible = false;
                image.Width = 15;
                image.Height = 15;
                blackImageGrid.Children.Add(imageTuple.Item1);

                i++;
            }

        }

        public static void addWhiteCaptureImage(MoveableImage image, int pieceValue)
        {
            whiteImageGrid.Children.Clear();
            Grid.SetRow(whiteScoreLabel, 0);
            Grid.SetColumn(whiteScoreLabel, 0);
            whiteImageGrid.Children.Add(whiteScoreLabel);
            whiteScore = 0;
            whiteCapturedImageList.Add(Tuple.Create<Image, int>(image, pieceValue));
            whiteCapturedImageList.Sort((x, y) => y.Item2.CompareTo(x.Item2));
            int i = 1;
            foreach (Tuple<Image, int> imageTuple in whiteCapturedImageList)
            {
                Grid.SetRow(imageTuple.Item1, 0);
                Grid.SetColumn(imageTuple.Item1, i);
                image.setIsCapturedPiece(true);
                image.IsHitTestVisible = false;
                image.Width = 15;
                image.Height = 15;

                whiteImageGrid.Children.Add(imageTuple.Item1);

                i++;
            }

        }

        public static void addWhiteScore(int score)
        {
            whiteScore += score;
            blackScoreLabel.Content = (blackScore - whiteScore).ToString();
            whiteScoreLabel.Content = (whiteScore - blackScore).ToString();
        }

        public static void addBlackScore(int score)
        {
            blackScore += score;
            blackScoreLabel.Content = (blackScore - whiteScore).ToString();
            whiteScoreLabel.Content = (whiteScore - blackScore).ToString();
        }

        public static void applyMoveToGUI(Board board, Move move)
        {

        }

        public static void drawArrow(UIElement element)
        {
            Point endPosition = new Point(Canvas.GetLeft(element), Canvas.GetTop(element));
            if (Ui.ArrowStartingPosition == endPosition)
            {
                int xLocation = (int)Ui.ArrowStartingPosition.X / squareSize;
                int yLocation = Game.activeBoard.getBoardSize() - 1 - (int)(Ui.ArrowStartingPosition.Y / squareSize);

                addMarkedTile(yLocation * Game.activeBoard.getBoardSize() + xLocation);
                return;
            }
            else
            {
                Line line = new Line();
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = Colors.DarkGoldenrod;
                line.StrokeThickness = 4;
                line.Stroke = brush;
                line.Opacity = 1;
                line.IsHitTestVisible = false;
                PointCollection polygonPoints = new PointCollection();

                //Console.WriteLine("start Position = \t\t\t " + Ui.ArrowStartingPosition + "\n");

                //Console.WriteLine("end Position = \t\t\t " + endPosition + "\n");

                line.X1 = Ui.ArrowStartingPosition.X;
                line.Y1 = Ui.ArrowStartingPosition.Y;

                line.X2 = endPosition.X;
                line.Y2 = endPosition.Y;

                double theta = Math.Atan2(Ui.ArrowStartingPosition.Y - endPosition.Y, Ui.ArrowStartingPosition.X - endPosition.X);
                double sint = Math.Sin(theta);
                double cost = Math.Cos(theta);

                Polyline poly = new Polyline();
                double pointLength = squareSize / 4;
                Point rightArrowPoint = new Point(endPosition.X + (pointLength * cost - pointLength * sint),
                endPosition.Y + (pointLength * sint + pointLength * cost));

                Point leftArrowPoint = new Point(endPosition.X + (pointLength * cost + pointLength * sint),
                endPosition.Y - (pointLength * cost - pointLength * sint));
                Point centerArrowPoint = new Point(endPosition.X, endPosition.Y);

                polygonPoints.Add(leftArrowPoint);
                polygonPoints.Add(centerArrowPoint);
                polygonPoints.Add(rightArrowPoint);
                poly.IsHitTestVisible = false;
                poly.Points = polygonPoints;
                poly.StrokeThickness = 4;
                poly.Stroke = brush;
                poly.Opacity = 1;

                Canvas.SetLeft(line, 0 + Ui.squareSize / 2);
                Canvas.SetTop(line, 0 + Ui.squareSize / 2);
                Canvas.SetZIndex(line, 10000);
                Ui.canvas.Children.Add(line);

                Canvas.SetLeft(poly, 0 + Ui.squareSize / 2);
                Canvas.SetTop(poly, 0 + Ui.squareSize / 2);
                Canvas.SetZIndex(poly, 10000);
                Ui.canvas.Children.Add(poly);
                Ui.arrows.Add(line);
                Ui.polyArrows.Add(poly);
                Ui.tileSelected = null;
            }

        }

        public static void removeArrows()
        {
            foreach (Line line in arrows)
            {
                canvas.Children.Remove(line);
            }

            foreach (Polyline polyline in polyArrows)
            {
                canvas.Children.Remove(polyline);
            }
        }
    }
}
