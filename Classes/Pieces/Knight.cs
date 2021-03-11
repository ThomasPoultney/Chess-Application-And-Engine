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

        public override List<Move> generateValidMoves(Board board)
        {
            int currentLocation = this.location;
            List<Move> validMoves = new List<Move>();


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
                    Move move = new Move(currentLocation, twoRightOneUp, this);
                    validMoves.Add(move);
                }
                else if (board.getPiece()[currentLocation].getIsWhite() != board.getPiece()[twoRightOneUp].getIsWhite())
                {
                    Move move = new Move(currentLocation, twoRightOneUp, this);
                    validMoves.Add(move);
                }
            }

            //two Left, One Up
            //if not on top rank and not on last two ranks
            if (currentLocation % board.getBoardSize() > 1 && currentLocation < (board.getBoardSize() * (board.getBoardSize() - 1)))
            {
                if (board.getPiece()[twoLeftOneUp] == null)
                {
                    Move move = new Move(currentLocation, twoLeftOneUp, this);
                    validMoves.Add(move);
                }
                else if (board.getPiece()[currentLocation].getIsWhite() != board.getPiece()[twoLeftOneUp].getIsWhite())
                {
                    Move move = new Move(currentLocation, twoLeftOneUp, this);
                    validMoves.Add(move);
                }
            }

            //two up one right
            //if not on last two ranks or right file
            if (currentLocation % board.getBoardSize() < (board.getBoardSize() - 1) && currentLocation < (board.getBoardSize() * (board.getBoardSize() - 2)))
            {
                if (board.getPiece()[twoUpOneRight] == null)
                {
                    Move move = new Move(currentLocation, twoUpOneRight, this);
                    validMoves.Add(move);
                }
                else if (board.getPiece()[currentLocation].getIsWhite() != board.getPiece()[twoUpOneRight].getIsWhite())
                {
                    Move move = new Move(currentLocation, twoUpOneRight, this);
                    validMoves.Add(move);

                }
            }

            //two up one Left
            //if not on last two ranks or left file.
            if (currentLocation % board.getBoardSize() > 0 && currentLocation < (board.getBoardSize() * (board.getBoardSize() - 2)))
            {
                if (board.getPiece()[twoUpOneLeft] == null)
                {
                    Move move = new Move(currentLocation, twoUpOneLeft, this);
                    validMoves.Add(move);

                }
                else if (board.getPiece()[currentLocation].getIsWhite() != board.getPiece()[twoUpOneLeft].getIsWhite())
                {
                    Move move = new Move(currentLocation, twoUpOneLeft, this);
                    validMoves.Add(move);
                }
            }

            //two left one down
            //if not on bottom rank or first two files
            if (currentLocation > (board.getBoardSize() - 1) && currentLocation % board.getBoardSize() > 1)
            {
                if (board.getPiece()[twoLeftOneDown] == null)
                {
                    Move move = new Move(currentLocation, twoLeftOneDown, this);
                    validMoves.Add(move);
                }
                else if (board.getPiece()[currentLocation].getIsWhite() != board.getPiece()[twoLeftOneDown].getIsWhite())
                {
                    Move move = new Move(currentLocation, twoLeftOneDown, this);
                    validMoves.Add(move);
                }
            }


            //two right one down
            //if not on bottom rank or last two files
            if (currentLocation > (board.getBoardSize() - 1) && currentLocation % board.getBoardSize() < (board.getBoardSize() - 2))
            {
                if (board.getPiece()[twoRightOneDown] == null)
                {
                    Move move = new Move(currentLocation, twoRightOneDown, this);
                    validMoves.Add(move);

                }
                else if (board.getPiece()[currentLocation].getIsWhite() != board.getPiece()[twoRightOneDown].getIsWhite())
                {
                    Move move = new Move(currentLocation, twoRightOneDown, this);
                    validMoves.Add(move);

                }
            }

            //two down one right
            //if not on bottom two ranks or right most file
            if (currentLocation > ((board.getBoardSize() * 2) - 1) && (currentLocation % board.getBoardSize()) < (board.getBoardSize() - 1))
            {
                if (board.getPiece()[twoDownOneRight] == null)
                {
                    Move move = new Move(currentLocation, twoDownOneRight, this);
                    validMoves.Add(move);
                }
                else if (board.getPiece()[currentLocation].getIsWhite() != board.getPiece()[twoDownOneRight].getIsWhite())
                {
                    Move move = new Move(currentLocation, twoDownOneRight, this);
                    validMoves.Add(move);

                }
            }

            //two down one left
            //if not on bottom two ranks or left most file
            if (currentLocation > ((board.getBoardSize() * 2) - 1) && (currentLocation % board.getBoardSize()) > 0)
            {
                if (board.getPiece()[twoDownOneLeft] == null)
                {
                    Move move = new Move(currentLocation, twoDownOneLeft, this);
                    validMoves.Add(move);
                }
                else if (board.getPiece()[currentLocation].getIsWhite() != board.getPiece()[twoDownOneLeft].getIsWhite())
                {
                    Move move = new Move(currentLocation, twoDownOneLeft, this);
                    validMoves.Add(move);
                }
            }
            return validMoves;
        }


    }
}
