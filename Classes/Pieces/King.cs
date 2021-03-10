using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessB
{
    class King : Piece
    {
        public King(bool isWhite, int location) : base(isWhite, location)
        {

            this.location = location;
            this.isWhite = isWhite;
            this.strength = 4;
            if (isWhite == true)
            {
                this.imagePath = "C:/Users/tompo/source/repos/ChessAI/ChessImages/wK.PNG";
            }
            else
            {
                this.imagePath = "C:/Users/tompo/source/repos/ChessAI/ChessImages/bK.PNG";
            }
        }

        public override List<int> generateValidMoves(Board board)
        {
            int currentLocation = this.location;
            List<int> validMoves = new List<int>();
            int nextLocation = currentLocation;


            //NW Direction
            //while not on left file or top rank
            if (nextLocation % board.getBoardSize() != 0 & nextLocation < (board.getBoardSize() * (board.getBoardSize() - 1)))
            {

                if (board.getPiece()[nextLocation + (board.getBoardSize() - 1)] == null)
                {
                    validMoves.Add(nextLocation + (board.getBoardSize() - 1));

                }
                else if (board.getPiece()[nextLocation + (board.getBoardSize() - 1)].getIsWhite() != this.isWhite)
                {
                    validMoves.Add(nextLocation + (board.getBoardSize() - 1));

                }
            }

            //NE Direction
            //while not on Right file or top rank
            if (nextLocation % board.getBoardSize() != (board.getBoardSize() - 1) & nextLocation < (board.getBoardSize() * (board.getBoardSize() - 1)))
            {

                if (board.getPiece()[nextLocation + (board.getBoardSize() + 1)] == null)
                {
                    validMoves.Add(nextLocation + (board.getBoardSize() + 1));

                }
                else if (board.getPiece()[nextLocation + (board.getBoardSize() + 1)].getIsWhite() != this.isWhite)
                {
                    validMoves.Add(nextLocation + (board.getBoardSize() + 1));

                }
            }

            //SW Direction
            //while not on Right file or bottom rank
            if (nextLocation % board.getBoardSize() != board.getBoardSize() - 1 & nextLocation > (board.getBoardSize() - 1))
            {

                if (board.getPiece()[nextLocation - (board.getBoardSize() - 1)] == null)
                {
                    validMoves.Add(nextLocation - (board.getBoardSize() - 1));

                }

                else if (board.getPiece()[nextLocation - (board.getBoardSize() - 1)].getIsWhite() != this.isWhite)
                {
                    validMoves.Add(nextLocation - (board.getBoardSize() - 1));

                }
            }

            //SE Direction
            //while not on left file or bottom rank

            if (nextLocation % board.getBoardSize() != 0 & nextLocation > board.getBoardSize() - 1)
            {

                if (board.getPiece()[nextLocation - (board.getBoardSize() + 1)] == null)
                {
                    validMoves.Add(nextLocation - (board.getBoardSize() + 1));

                }

                else if (board.getPiece()[nextLocation - (board.getBoardSize() + 1)].getIsWhite() != this.isWhite)
                {
                    validMoves.Add(nextLocation - (board.getBoardSize() + 1));

                }
            }
            nextLocation = currentLocation;
            //left direction
            if (nextLocation % board.getBoardSize() != 0)
            {
                if (board.getPiece()[nextLocation - 1] == null)
                {
                    validMoves.Add(nextLocation - 1);


                }
                else if (board.getPiece()[nextLocation - 1].getIsWhite() != this.isWhite)
                {
                    validMoves.Add(nextLocation - 1);


                }



            }
            nextLocation = currentLocation;
            //right direction
            if (nextLocation % board.getBoardSize() != board.getBoardSize() - 1)
            {

                if (board.getPiece()[nextLocation + 1] == null)
                {
                    validMoves.Add(nextLocation + 1);

                }
                else if (board.getPiece()[nextLocation + 1].getIsWhite() != this.isWhite)
                {
                    validMoves.Add(nextLocation + 1);

                }




            }


            //up direction
            if (nextLocation < board.getBoardSize() * (board.getBoardSize() - 1))
            {
                if (board.getPiece()[nextLocation + 8] == null)
                {
                    validMoves.Add(nextLocation + 8);

                }
                else if (board.getPiece()[nextLocation + 8].getIsWhite() != this.isWhite)
                {
                    validMoves.Add(nextLocation + 8);


                }



            }

            //down direction
            if (nextLocation > board.getBoardSize() - 1)
            {
                if (board.getPiece()[nextLocation - 8] == null)
                {
                    validMoves.Add(nextLocation - 8);

                }
                else if (board.getPiece()[nextLocation - 8].getIsWhite() != this.isWhite)
                {
                    validMoves.Add(nextLocation - 8);

                }

            }


            return validMoves;
        }

    }
}
