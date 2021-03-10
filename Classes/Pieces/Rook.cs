using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessB
{
    class Rook : Piece
    {
        public Rook(bool isWhite, int location) : base(isWhite, location)
        {

            this.location = location;
            this.isWhite = isWhite;
            this.strength = 5;
            if (isWhite == true)
            {
                this.imagePath = "C:/Users/tompo/source/repos/ChessAI/ChessImages/wR.PNG"; ;
            }
            else
            {
                this.imagePath = "C:/Users/tompo/source/repos/ChessAI/ChessImages/bR.PNG"; ;
            }
        }

        public override List<int> generateValidMoves(Board board)
        {
            int currentLocation = this.location;
            List<int> validMoves = new List<int>();
            int nextLocation = currentLocation;


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
