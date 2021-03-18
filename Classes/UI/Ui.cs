﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

                Image tile = new Image();
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

            blackScoreLabel.Content = (blackScore - whiteScore).ToString();
            whiteScoreLabel.Content = (whiteScore - blackScore).ToString();

            whiteImageGrid.Children.Add(image);

            numWhitePieceCaptured++;
        }

        public static void applyMoveToGUI(Board board, Move move)
        {

        }
    }
}
