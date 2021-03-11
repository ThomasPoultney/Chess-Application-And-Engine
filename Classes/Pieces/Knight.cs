using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessB
{
    class Knight : Piece
    {
        public Knight(bool isWhite, int location) : base(isWhite, location)
        {

            this.location = location;
            this.isWhite = isWhite;
            this.strength = 3;
            if (isWhite == true)
            {
                this.imagePath = "C:/Users/tompo/source/repos/ChessAI/ChessImages/wN.PNG"; ;
            }
            else
            {
                this.imagePath = "C:/Users/tompo/source/repos/ChessAI/ChessImages/bN.PNG"; ;
            }
        }

        public override List<int> generateValidMoves(Board board)
        {
            int currentLocation = this.location;
            List<int> validMoves = new List<int>();


            int twoRightOneUp = currentLocation + board.getBoardSize() + 2;
            int twoLeftOneUp = currentLocation + (board.getBoardSize() - 2);

            int twoUpOneRight = currentLocation + (board.getBoardSize() * 2) + 1;
            int twoUpOneLeft = currentLocation + (board.getBoardSize() * 2) - 1;

            int twoLeftOneDown = currentLocation - (board.getBoardSize() + 2);
            int twoRightOneDown = currentLocation - (board.getBoardSize() - 2);

            int twoDownOneLeft = currentLocation - ((board.getBoardSize() * 2) + 1);
            int twoDownOneRight = currentLocation - ((board.getBoardSize() * 2) - 1);


            //two right, One Up
            //if not on top rank and not on last two files
            if (currentLocation % board.getBoardSize() < (board.getBoardSize() - 2) && currentLocation < (board.getBoardSize() * (board.getBoardSize() - 1)))
            {
                if (board.getPiece()[twoRightOneUp] == null)
                {
                    validMoves.Add(twoRightOneUp);
                }
                else if (board.getPiece()[currentLocation].getIsWhite() != board.getPiece()[twoRightOneUp].getIsWhite())
                {
                    validMoves.Add(twoRightOneUp);
                }
            }

            //two Left, One Up
            //if not on top rank and not on last two ranks
            if (currentLocation % board.getBoardSize() > 1 && currentLocation < (board.getBoardSize() * (board.getBoardSize() - 1)))
            {
                if (board.getPiece()[twoLeftOneUp] == null)
                {
                    validMoves.Add(twoLeftOneUp);
                }
                else if (board.getPiece()[currentLocation].getIsWhite() != board.getPiece()[twoLeftOneUp].getIsWhite())
                {
                    validMoves.Add(twoLeftOneUp);
                }
            }

            //two up one right
            //if not on last two ranks or right file
            if (currentLocation % board.getBoardSize() < (board.getBoardSize() - 1) && currentLocation < (board.getBoardSize() * (board.getBoardSize() - 2)))
            {
                if (board.getPiece()[twoUpOneRight] == null)
                {
                    validMoves.Add(twoUpOneRight);
                }
                else if (board.getPiece()[currentLocation].getIsWhite() != board.getPiece()[twoUpOneRight].getIsWhite())
                {
                    validMoves.Add(twoUpOneRight);
                }
            }

            //two up one Left
            //if not on last two ranks or left file.
            if (currentLocation % board.getBoardSize() > 0 && currentLocation < (board.getBoardSize() * (board.getBoardSize() - 2)))
            {
                if (board.getPiece()[twoUpOneLeft] == null)
                {
                    validMoves.Add(twoUpOneLeft);
                }
                else if (board.getPiece()[currentLocation].getIsWhite() != board.getPiece()[twoUpOneLeft].getIsWhite())
                {
                    validMoves.Add(twoUpOneLeft);
                }
            }

            //two left one down
            //if not on bottom rank or first two files
            if (currentLocation > (board.getBoardSize() - 1) && currentLocation % board.getBoardSize() > 1)
            {
                if (board.getPiece()[twoLeftOneDown] == null)
                {
                    validMoves.Add(twoLeftOneDown);
                }
                else if (board.getPiece()[currentLocation].getIsWhite() != board.getPiece()[twoLeftOneDown].getIsWhite())
                {
                    validMoves.Add(twoLeftOneDown);
                }
            }


            //two right one down
            //if not on bottom rank or last two files
            if (currentLocation > (board.getBoardSize() - 1) && currentLocation % board.getBoardSize() < (board.getBoardSize() - 2))
            {
                if (board.getPiece()[twoRightOneDown] == null)
                {
                    validMoves.Add(twoRightOneDown);
                }
                else if (board.getPiece()[currentLocation].getIsWhite() != board.getPiece()[twoRightOneDown].getIsWhite())
                {
                    validMoves.Add(twoRightOneDown);
                }
            }

            //two down one right
            //if not on bottom two ranks or right most file
            if (currentLocation > ((board.getBoardSize() * 2) - 1) && (currentLocation % board.getBoardSize()) < (board.getBoardSize() - 1))
            {
                if (board.getPiece()[twoDownOneRight] == null)
                {
                    validMoves.Add(twoDownOneRight);
                }
                else if (board.getPiece()[currentLocation].getIsWhite() != board.getPiece()[twoDownOneRight].getIsWhite())
                {
                    validMoves.Add(twoDownOneRight);
                }
            }

            //two down one left
            //if not on bottom two ranks or left most file
            if (currentLocation > ((board.getBoardSize() * 2) - 1) && (currentLocation % board.getBoardSize()) > 0)
            {
                if (board.getPiece()[twoDownOneLeft] == null)
                {
                    validMoves.Add(twoDownOneLeft);
                }
                else if (board.getPiece()[currentLocation].getIsWhite() != board.getPiece()[twoDownOneLeft].getIsWhite())
                {
                    validMoves.Add(twoDownOneLeft);
                }
            }
            return validMoves;
        }


    }
}
