using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Microsoft.Win32;

namespace ChessB
{
    public class MoveableImage : Image
    {

        public static Point point;
        public String name;
        static int pieceStartLocation;
        public bool isCapturedPiece;
        private static List<Board> boardStates = new List<Board>();

        public MoveableImage()
        {
        }

        //Destructor.
        ~MoveableImage()
        {
        }

        public bool getIsCapturedPiece()
        {
            return this.isCapturedPiece;
        }

        public void setIsCapturedPiece(bool isCaptured)
        {
            this.isCapturedPiece = isCaptured;
        }


        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if (Ui.imageSelected == null && Ui.upgradeChoiceRequired == false)
                {

                    Ui.imageSelected = this;
                    point = e.GetPosition(Ui.imageSelected);
                    Ui.resetPositionY = Canvas.GetTop(Ui.imageSelected);
                    Ui.resetPositionX = Canvas.GetLeft(Ui.imageSelected);
                    int xLocation;
                    int yLocation;
                    if (Ui.whtiePerspective)
                    {
                        xLocation = (int)(Canvas.GetLeft(Ui.imageSelected) / Ui.squareSize);
                        yLocation = Board.boardSize - 1 - (int)(Canvas.GetTop(Ui.imageSelected) / Ui.squareSize);
                    }
                    else
                    {
                        xLocation = Board.boardSize - 1 - ((int)(Canvas.GetLeft(Ui.imageSelected) / Ui.squareSize));
                        yLocation = Board.boardSize - 1 - (Board.boardSize - 1 - (int)(Canvas.GetTop(Ui.imageSelected) / Ui.squareSize));
                    }

                    pieceStartLocation = Board.boardSize * yLocation + xLocation;
                    Ui.pieceSelected = Game.activeBoard.getPiece()[pieceStartLocation];
                    if (Ui.pieceSelected != null)
                    {
                        if (Ui.pieceSelected.getIsWhite() != Game.activeBoard.getIsWhiteTurn())
                        {
                            Ui.imageSelected = null;
                            return;
                        }
                    }

                    Board activeBoard = Game.activeBoard;
                    Piece pieceSelected = Ui.pieceSelected;
                    Piece[] pieceArray = activeBoard.getPiece();

                    int pieceLocation = -5;

                    for (int i = 0; i < activeBoard.getBoardSize() * activeBoard.getBoardSize(); i++)
                    {
                        if (pieceArray[i] == pieceSelected)
                        {
                            pieceLocation = i;
                            break;
                        }
                    }

                    if (Ui.pieceSelected.GetType() == typeof(King))
                    {
                        Ui.validMoves = activeBoard.removeMovesThatPutInCheck(pieceSelected.generateValidMoves(activeBoard, pieceArray, pieceLocation,
                                                                                activeBoard.generateBlackAttackingMoves(pieceArray),
                                                                                activeBoard.generateWhiteAttackingMoves(pieceArray)));
                    }
                    else
                    {
                        Ui.validMoves = activeBoard.removeMovesThatPutInCheck(pieceSelected.generateValidMoves(activeBoard, pieceArray, pieceLocation));
                    }
                    Ui.drawValidMoves();

                }
                else
                {
                    return;
                }
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            Ui.removeHoveredTileImage();
            if (Ui.imageSelected != null)
            {
                double topPosition = Math.Round(Canvas.GetTop(Ui.imageSelected) / Ui.squareSize) * Ui.squareSize;
                double leftPosition = Math.Round(Canvas.GetLeft(Ui.imageSelected) / Ui.squareSize) * Ui.squareSize;
                Canvas.SetTop(Ui.imageSelected, topPosition);
                Canvas.SetLeft(Ui.imageSelected, leftPosition);
                int endXlocation;
                int endYlocation;
                if (Ui.whtiePerspective)
                {
                    endXlocation = (int)(leftPosition / Ui.squareSize);
                    endYlocation = Board.boardSize - 1 - (int)(topPosition / Ui.squareSize);
                }
                else
                {
                    endXlocation = Board.boardSize - 1 - (int)(leftPosition / Ui.squareSize);
                    endYlocation = (int)(topPosition / Ui.squareSize);
                }

                int pieceEndLocation = Board.boardSize * endYlocation + endXlocation;
                Move move = new Move(pieceStartLocation, pieceEndLocation, Game.activeBoard.getPiece()[pieceStartLocation]);
                int boardSize = Game.activeBoard.getBoardSize();
                Board boardAfterMove = Game.activeBoard.makeMoveOnNewBoard(move);

                if (boardAfterMove == null)
                {
                    resetImage();
                }
                else
                {

                    if (Ui.pieceSelected is ChessB.Pawn)
                    {

                        if (endYlocation == Board.boardSize - 1)
                        {
                            Canvas.SetTop(Ui.queenUpgradeButton, topPosition);
                            Canvas.SetLeft(Ui.queenUpgradeButton, leftPosition);
                            Canvas.SetZIndex(Ui.queenUpgradeButton, 150000);
                            Ui.canvas.Children.Add(Ui.queenUpgradeButton);

                            Canvas.SetZIndex(Ui.rookUpgradeButton, 150000);
                            Ui.canvas.Children.Add(Ui.rookUpgradeButton);

                            Canvas.SetZIndex(Ui.bishopUpgradeButton, 150000);
                            Ui.canvas.Children.Add(Ui.bishopUpgradeButton);

                            Canvas.SetZIndex(Ui.knightUpgradeButton, 150000);
                            Ui.canvas.Children.Add(Ui.knightUpgradeButton);


                            Canvas.SetZIndex(Ui.upgradeCancelButton, 150000);
                            Ui.canvas.Children.Add(Ui.upgradeCancelButton);

                            if (Ui.whtiePerspective)
                            {
                                Canvas.SetTop(Ui.rookUpgradeButton, topPosition + Ui.squareSize);
                                Canvas.SetLeft(Ui.rookUpgradeButton, leftPosition);

                                Canvas.SetTop(Ui.bishopUpgradeButton, topPosition + 2 * Ui.squareSize);
                                Canvas.SetLeft(Ui.bishopUpgradeButton, leftPosition);

                                Canvas.SetTop(Ui.knightUpgradeButton, topPosition + 3 * Ui.squareSize);
                                Canvas.SetLeft(Ui.knightUpgradeButton, leftPosition);

                                Canvas.SetTop(Ui.upgradeCancelButton, (topPosition + 4 * Ui.squareSize));
                                Canvas.SetLeft(Ui.upgradeCancelButton, leftPosition);
                            }
                            else
                            {
                                Canvas.SetTop(Ui.rookUpgradeButton, topPosition - Ui.squareSize);
                                Canvas.SetLeft(Ui.rookUpgradeButton, leftPosition);

                                Canvas.SetTop(Ui.bishopUpgradeButton, topPosition - 2 * Ui.squareSize);
                                Canvas.SetLeft(Ui.bishopUpgradeButton, leftPosition);

                                Canvas.SetTop(Ui.knightUpgradeButton, topPosition - 3 * Ui.squareSize);
                                Canvas.SetLeft(Ui.knightUpgradeButton, leftPosition);

                                Canvas.SetTop(Ui.upgradeCancelButton, (topPosition - 3 * Ui.squareSize) - Ui.squareSize / 3);
                                Canvas.SetLeft(Ui.upgradeCancelButton, leftPosition);
                            }
                            Ui.upgradeChoiceRequired = true;
                            Ui.setUpgradeButtonWhite();

                        }
                        else if (endYlocation == 0)
                        {


                            Canvas.SetTop(Ui.queenUpgradeButton, topPosition);
                            Canvas.SetLeft(Ui.queenUpgradeButton, leftPosition);
                            Canvas.SetZIndex(Ui.queenUpgradeButton, 150000);
                            Ui.canvas.Children.Add(Ui.queenUpgradeButton);



                            Canvas.SetZIndex(Ui.rookUpgradeButton, 150000);
                            Ui.canvas.Children.Add(Ui.rookUpgradeButton);


                            Canvas.SetZIndex(Ui.bishopUpgradeButton, 150000);
                            Ui.canvas.Children.Add(Ui.bishopUpgradeButton);


                            Canvas.SetZIndex(Ui.knightUpgradeButton, 150000);
                            Ui.canvas.Children.Add(Ui.knightUpgradeButton);


                            Canvas.SetZIndex(Ui.upgradeCancelButton, 150000);
                            Ui.canvas.Children.Add(Ui.upgradeCancelButton);

                            if (Ui.whtiePerspective)
                            {
                                Canvas.SetTop(Ui.rookUpgradeButton, topPosition - Ui.squareSize);
                                Canvas.SetLeft(Ui.rookUpgradeButton, leftPosition);

                                Canvas.SetTop(Ui.bishopUpgradeButton, topPosition - 2 * Ui.squareSize);
                                Canvas.SetLeft(Ui.bishopUpgradeButton, leftPosition);

                                Canvas.SetTop(Ui.knightUpgradeButton, topPosition - 3 * Ui.squareSize);
                                Canvas.SetLeft(Ui.knightUpgradeButton, leftPosition);

                                Canvas.SetTop(Ui.upgradeCancelButton, (topPosition - 3 * Ui.squareSize) - Ui.squareSize / 3);
                                Canvas.SetLeft(Ui.upgradeCancelButton, leftPosition);
                            }
                            else
                            {
                                Canvas.SetTop(Ui.rookUpgradeButton, topPosition + Ui.squareSize);
                                Canvas.SetLeft(Ui.rookUpgradeButton, leftPosition);

                                Canvas.SetTop(Ui.bishopUpgradeButton, topPosition + 2 * Ui.squareSize);
                                Canvas.SetLeft(Ui.bishopUpgradeButton, leftPosition);

                                Canvas.SetTop(Ui.knightUpgradeButton, topPosition + 3 * Ui.squareSize);
                                Canvas.SetLeft(Ui.knightUpgradeButton, leftPosition);

                                Canvas.SetTop(Ui.upgradeCancelButton, topPosition + 4 * Ui.squareSize);
                                Canvas.SetLeft(Ui.upgradeCancelButton, leftPosition);
                            }
                            Ui.upgradeChoiceRequired = true;
                            Ui.setUpgradeButtonBlack();
                        }

                    }
                    Ui.upgradeMove = move;

                    if (Ui.upgradeChoiceRequired == false)
                    {
                        Game.activeBoard = boardAfterMove;
                        //Console.WriteLine(boardAfterMove.moveGenerationTest(3));
                        Console.WriteLine(boardAfterMove.getIsWhiteTurn() + " " + boardAfterMove.getValidMoves().Count);
                        Ui.drawUi(boardAfterMove, Ui.canvas);

                    }

                    if (Game.whitesTurn)
                    {
                        Game.startBlackTimer();
                    }
                    else
                    {
                        Game.startWhiteTimer();

                    }

                    MediaPlayer mediaPlayer = new MediaPlayer();
                    //OpenFileDialog openFileDialog = new OpenFileDialog();
                    //openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
                    //if (openFileDialog.ShowDialog() == true)
                    //{
                    string soundEffectString = "";
                    // mediaPlayer.Open(new Uri(soundEffectString));
                    mediaPlayer.Play();
                    //}
                    Game.whitesTurn = !Game.whitesTurn;
                }
            }

            Ui.pieceSelected = null;
            Ui.imageSelected = null;
            Ui.removeValidMovesImages();
        }




        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            Ui.removeHoveredTileImage();
            if (Ui.imageSelected != null)
            {

                resetImage();
            }
            else
            {
                if (Ui.tileSelected == null)
                {
                    Ui.tileSelected = this;
                    Ui.ArrowStartingPosition = new Point(Canvas.GetLeft(this), Canvas.GetTop(this));
                }
            }


        }

        protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
        {
            if (Ui.tileSelected != null)
            {
                Ui.drawArrow(this);
                Ui.tileSelected = null;
            }
        }

        public void resetImage()
        {
            if (Ui.imageSelected != null)
            {
                Ui.removeValidMovesImages();
                Canvas.SetTop(Ui.imageSelected, Ui.resetPositionY);
                Canvas.SetLeft(Ui.imageSelected, Ui.resetPositionX);

                Ui.pieceSelected = null;
                Ui.imageSelected = null;
                pieceStartLocation = 0;

            }
        }

        public void MoveImage(Image image, int location, Board board)
        {
            int xLocation = (location) % ((board.getBoardSize()));
            int yLocation = (int)(location / board.getBoardSize());
            Canvas.SetTop(image, xLocation);
            Canvas.SetLeft(image, yLocation);
        }
    }
}