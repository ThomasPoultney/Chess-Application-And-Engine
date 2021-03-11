using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Timers;
using System.Collections.Generic;
using System.Linq;

namespace ChessB
{
    class MoveableImage : Image
    {

        public static Point point;
        public String name;
        static int pieceStartLocation;

        public MoveableImage()
        {
        }

        //Destructor.
        ~MoveableImage()
        {
        }



        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if (Ui.imageSelected == null)
                {
                    Ui.imageSelected = this;
                    Console.WriteLine(this.name);
                    point = e.GetPosition(Ui.imageSelected);
                    Ui.resetPositionY = Canvas.GetTop(Ui.imageSelected);
                    Ui.resetPositionX = Canvas.GetLeft(Ui.imageSelected);

                    int xlocation = (int)(Canvas.GetLeft(Ui.imageSelected) / Ui.squareSize);
                    int ylocation = Board.boardSize - 1 - (int)(Canvas.GetTop(Ui.imageSelected) / Ui.squareSize);
                    pieceStartLocation = Board.boardSize * ylocation + xlocation;
                    Ui.pieceSelected = Game.activeBoard.getPiece()[pieceStartLocation];

                    if (Ui.pieceSelected.getIsWhite() != Game.activeBoard.getIsWhiteTurn())
                    {
                        Ui.imageSelected = null;
                        return;
                    }

                    Game.validMoves = Game.activeBoard.removeMovesThatPutInCheck(Ui.pieceSelected.generateValidMoves(Game.activeBoard, Game.activeBoard.getPiece()));

                    Ui.drawValidMoves();
                    base.OnMouseDown(e);
                }
                else
                {
                    return;
                }
            }



        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {


            if (Ui.imageSelected != null)
            {
                double topPosition = Math.Round(Canvas.GetTop(Ui.imageSelected) / Ui.squareSize) * Ui.squareSize;
                double leftPosition = Math.Round(Canvas.GetLeft(Ui.imageSelected) / Ui.squareSize) * Ui.squareSize;
                Canvas.SetTop(Ui.imageSelected, topPosition);
                Canvas.SetLeft(Ui.imageSelected, leftPosition);

                int endXlocation = (int)(leftPosition / Ui.squareSize);
                int endYlocation = Board.boardSize - 1 - (int)(topPosition / Ui.squareSize);
                int pieceEndLocation = Board.boardSize * endYlocation + endXlocation;
                Move move = new Move(pieceStartLocation, pieceEndLocation, Game.activeBoard.getPiece()[pieceStartLocation]);




                if (Game.activeBoard.makeMove(move) == false)
                {
                    resetImage();
                }


                //Console.WriteLine("Moved to Location = " + pieceEndLocation);
                setUpTurn();

            }
            Ui.pieceSelected = null;
            Ui.imageSelected = null;
            Ui.removeValidMovesImages();


        }

        private void setUpTurn()
        {

        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            if (Ui.imageSelected != null)
            {
                resetImage();
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





    }

}