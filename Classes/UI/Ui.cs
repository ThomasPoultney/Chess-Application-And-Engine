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

        public static void drawBoard(Board board, Canvas canvas)
        {

            Piece[] piece = board.getPiece();

            for (int i = board.getBoardSize() * board.getBoardSize() - 1; i >= 0; i--)
            {
                int xLocation = (i) % ((board.getBoardSize()));
                int yLocation = (int)(i / board.getBoardSize());

                Point nextLocation = new Point(xLocation * squareSize, yLocation * squareSize);
                string whiteTileImageURL = "C:/Users/tompo/source/repos/ChessB/Images/creamTile.PNG";
                string blackTileImageURL = "C:/Users/tompo/source/repos/ChessB/Images/darkBlueTile.PNG";

                String nextImageURL = ((xLocation + yLocation) % 2 == 1) ? blackTileImageURL : whiteTileImageURL;
                Rectangle rectangle = new Rectangle();
                rectangle.Width = Ui.squareSize;
                rectangle.Height = Ui.squareSize;
                SolidColorBrush blackBrush = new SolidColorBrush();

                Canvas.SetZIndex(rectangle, 10000);
                Canvas.SetTop(rectangle, yLocation * squareSize);
                Canvas.SetLeft(rectangle, xLocation * squareSize);
                Ui.canvas.Children.Add(rectangle);
                Tile tile = new Tile();
                ImageSource tileImage = new BitmapImage(new Uri(nextImageURL));
                tile.Source = tileImage;
                tile.Width = squareSize;
                tile.Height = squareSize;
                Canvas.SetZIndex(tile, 1);

                Canvas.SetTop(tile, yLocation * squareSize);
                Canvas.SetLeft(tile, xLocation * squareSize);
                canvas.Children.Add(tile);

                if (piece[i] != null)
                {
                    MoveableImage mpi = new MoveableImage();
                    ImageSource pieceImage = new BitmapImage(new Uri(piece[i].getImagePath()));

                    mpi.Source = pieceImage;
                    mpi.Width = squareSize;
                    mpi.Height = squareSize;

                    Canvas.SetZIndex(mpi, 1500);
                    Canvas.SetTop(mpi, (board.getBoardSize() - 1 - yLocation) * squareSize);
                    Canvas.SetLeft(mpi, xLocation * squareSize);
                    canvas.Children.Add(mpi);
                    mpi.name = piece[i].getImagePath();
                    piece[i].setImage(mpi);
                }
            }
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
            Canvas.SetTop(highlightImage, ((Game.activeBoard.getBoardSize() - 1 - yLocation) * squareSize));
            Canvas.SetLeft(highlightImage, xLocation * squareSize);
            Canvas.SetZIndex(highlightImage, 500);
            hightlightImages.Add(highlightImage);
            canvas.Children.Add(highlightImage);
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
            Grid.SetRow(image, 0);
            Grid.SetColumn(image, numBlackPieceCaptured);
            image.setIsCapturedPiece(true);
            image.Width = 15;
            image.Height = 15;
            blackScore += pieceValue;

            if (whiteScore - blackScore > 0)
            {
                whiteScoreLabel.Content = (whiteScore - blackScore).ToString();
                blackScoreLabel.Content = "";
            }

            blackScoreLabel.Content = (blackScore - whiteScore).ToString();
            whiteScoreLabel.Content = (whiteScore - blackScore).ToString();
            blackImageGrid.Children.Add(image);

            numBlackPieceCaptured++;
        }

        public static void addWhiteCaptureImage(MoveableImage image, int pieceValue)
        {
            Grid.SetRow(image, 0);
            Grid.SetColumn(image, numWhitePieceCaptured);
            image.setIsCapturedPiece(true);
            whiteScore += pieceValue;
            image.Width = 15;
            image.Height = 15;
            blackScoreLabel.Content = (blackScore - whiteScore).ToString();
            whiteScoreLabel.Content = (whiteScore - blackScore).ToString();

            whiteImageGrid.Children.Add(image);

            numWhitePieceCaptured++;
        }

        public static void applyMoveToGUI(Board board, Move move)
        {

        }

        public static void drawArrow(UIElement element)
        {
            Point endPosition = new Point(Canvas.GetLeft(element), Canvas.GetTop(element));
            if (Ui.ArrowStartingPosition == endPosition)
            {
                return;
            }
            else
            {
                Line line = new Line();
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = Colors.DarkGoldenrod;
                line.StrokeThickness = 7;
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
                poly.StrokeThickness = 7;
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
