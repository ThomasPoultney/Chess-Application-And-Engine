using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessB
{
    class Bishop : Piece
    {
        public Bishop(bool isWhite, int location) : base(isWhite, location)
        {

            this.location = location;
            this.isWhite = isWhite;
            this.strength = 3;
            if (isWhite == true)
            {
                this.imagePath = "C:/Users/tompo/source/repos/ChessAI/ChessImages/wB.PNG";
            }
            else
            {
                this.imagePath = "C:/Users/tompo/source/repos/ChessAI/ChessImages/bB.PNG"; ;
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
                } else if (board.getPiece()[nextLocation + (board.getBoardSize() - 1)].getIsWhite() == this.isWhite)
                {
                    break;
                }
                else if (board.getPiece()[nextLocation+ (board.getBoardSize() - 1)].getIsWhite() != this.isWhite)
                {
                    validMoves.Add(nextLocation + (board.getBoardSize() - 1));
                    break;
                }
            }

            //NE Direction
            //while not on Right file or top rank
            nextLocation = currentLocation;
            while (nextLocation % board.getBoardSize() != (board.getBoardSize()-1) & nextLocation < (board.getBoardSize() * (board.getBoardSize() - 1)))
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
            while (nextLocation % board.getBoardSize() != board.getBoardSize()-1 & nextLocation > (board.getBoardSize() - 1))
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
                    validMoves.Add(nextLocation -(board.getBoardSize() - 1));
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




            return validMoves;
        }
    }
}
