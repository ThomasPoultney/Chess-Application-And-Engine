using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessB
{
    class Queen : Piece
    {
        public Queen(bool isWhite, int location) : base(isWhite, location)
        {

            this.location = location;
            this.isWhite = isWhite;
            this.strength = 8;
            if (isWhite == true)
            {
                this.imagePath = "C:/Users/tompo/source/repos/ChessAI/ChessImages/wQ.PNG";
            }
            else
            {
                this.imagePath = "C:/Users/tompo/source/repos/ChessAI/ChessImages/bQ.PNG";
            }
        }

        public override List<int> generateValidMoves(Board board)
        {
            int currentLocation = this.location;
            List<int> validMoves = new List<int>();
            int nextLocation = currentLocation;


            //NW Direction
            //while not on left file or top rank
            while (nextLocation % board.getBoardSize() != 0 & nextLocation < (board.getBoardSize() * (board.getBoardSize() - 1)))
            {

                if (board.getPiece()[nextLocation + (board.getBoardSize() - 1)] == null)
                {
                    validMoves.Add(nextLocation + (board.getBoardSize() - 1));
                    nextLocation += (board.getBoardSize() - 1);
                }
                else if (board.getPiece()[nextLocation + (board.getBoardSize() - 1)].getIsWhite() == this.isWhite)
                {
                    break;
                }
                else if (board.getPiece()[nextLocation + (board.getBoardSize() - 1)].getIsWhite() != this.isWhite)
                {
                    validMoves.Add(nextLocation + (board.getBoardSize() - 1));
                    break;
                }
            }

            //NE Direction
            //while not on Right file or top rank
            nextLocation = currentLocation;
            while (nextLocation % board.getBoardSize() != (board.getBoardSize() - 1) & nextLocation < (board.getBoardSize() * (board.getBoardSize() - 1)))
            {

                if (board.getPiece()[nextLocation + (board.getBoardSize() + 1)] == null)
                {
                    validMoves.Add(nextLocation + (board.getBoardSize() + 1));
                    nextLocation += (board.getBoardSize() + 1);
                }
                else if (board.getPiece()[nextLocation + (board.getBoardSize() + 1)].getIsWhite() == this.isWhite)
                {
                    break;
                }
                else if (board.getPiece()[nextLocation + (board.getBoardSize() + 1)].getIsWhite() != this.isWhite)
                {
                    validMoves.Add(nextLocation + (board.getBoardSize() + 1));
                    break;
                }
            }

            //SW Direction
            //while not on Right file or bottom rank
            nextLocation = currentLocation;
            while (nextLocation % board.getBoardSize() != board.getBoardSize() - 1 & nextLocation > (board.getBoardSize() - 1))
            {

                if (board.getPiece()[nextLocation - (board.getBoardSize() - 1)] == null)
                {
                    validMoves.Add(nextLocation - (board.getBoardSize() - 1));
                    nextLocation -= (board.getBoardSize() - 1);
                }
                else if (board.getPiece()[nextLocation - (board.getBoardSize() - 1)].getIsWhite() == this.isWhite)
                {
                    break;
                }
                else if (board.getPiece()[nextLocation - (board.getBoardSize() - 1)].getIsWhite() != this.isWhite)
                {
                    validMoves.Add(nextLocation - (board.getBoardSize() - 1));
                    break;
                }
            }

            //SE Direction
            //while not on left file or bottom rank
            nextLocation = currentLocation;
            while (nextLocation % board.getBoardSize() != 0 & nextLocation > board.getBoardSize() - 1)
            {

                if (board.getPiece()[nextLocation - (board.getBoardSize() + 1)] == null)
                {
                    validMoves.Add(nextLocation - (board.getBoardSize() + 1));
                    nextLocation -= (board.getBoardSize() + 1);
                }
                else if (board.getPiece()[nextLocation - (board.getBoardSize() + 1)].getIsWhite() == this.isWhite)
                {
                    break;
                }
                else if (board.getPiece()[nextLocation - (board.getBoardSize() + 1)].getIsWhite() != this.isWhite)
                {
                    validMoves.Add(nextLocation - (board.getBoardSize() + 1));
                    break;
                }
            }
            nextLocation = currentLocation;
            //left direction
            while (nextLocation % board.getBoardSize() != 0)
            {
                if (board.getPiece()[nextLocation - 1] == null)
                {
                    validMoves.Add(nextLocation - 1);
                    nextLocation -= 1;

                }
                else if (board.getPiece()[nextLocation - 1].getIsWhite() != this.isWhite)
                {
                    validMoves.Add(nextLocation - 1);
                    nextLocation -= 1;
                    break;
                }
                else
                {
                    break;
                }


            }
            nextLocation = currentLocation;
            //right direction
            while (nextLocation % board.getBoardSize() != board.getBoardSize() - 1)
            {

                if (board.getPiece()[nextLocation + 1] == null)
                {
                    validMoves.Add(nextLocation + 1);
                    nextLocation += 1;
                }
                else if (board.getPiece()[nextLocation + 1].getIsWhite() != this.isWhite)
                {
                    validMoves.Add(nextLocation + 1);
                    nextLocation += 1;
                    break;
                }
                else
                {

                    break;
                }



            }

            nextLocation = currentLocation;
            //up direction
            while (nextLocation < board.getBoardSize() * (board.getBoardSize() - 1))
            {
                if (board.getPiece()[nextLocation + 8] == null)
                {
                    validMoves.Add(nextLocation + 8);
                    nextLocation += 8;
                }
                else if (board.getPiece()[nextLocation + 8].getIsWhite() != this.isWhite)
                {
                    validMoves.Add(nextLocation + 8);
                    nextLocation += 8;
                    break;

                }
                else
                {
                    break;
                }


            }
            nextLocation = currentLocation;
            //down direction
            while (nextLocation > board.getBoardSize() - 1)
            {
                if (board.getPiece()[nextLocation - 8] == null)
                {
                    validMoves.Add(nextLocation - 8);
                    nextLocation -= 8;
                }
                else if (board.getPiece()[nextLocation - 8].getIsWhite() != this.isWhite)
                {
                    validMoves.Add(nextLocation - 8);
                    nextLocation -= 8;
                    break;
                }
                else
                {
                    break;
                }



            }


            return validMoves;
        }
    }
}
