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

        public override List<Move> generateValidMoves(Board board, Piece[] piece)
        {
            int currentLocation = this.location;
            List<Move> validMoves = new List<Move>();
            int nextLocation = currentLocation;


            //NW Direction
            //while not on left file or top rank
            if (nextLocation % board.getBoardSize() != 0 & nextLocation < (board.getBoardSize() * (board.getBoardSize() - 1)))
            {

                if (piece[nextLocation + (board.getBoardSize() - 1)] == null)
                {
                    Move move = new Move(currentLocation, nextLocation + (board.getBoardSize() - 1), this);
                    validMoves.Add(move);

                }
                else if (piece[nextLocation + (board.getBoardSize() - 1)].getIsWhite() != this.isWhite)
                {
                    Move move = new Move(currentLocation, nextLocation + (board.getBoardSize() - 1), this);
                    validMoves.Add(move);

                }
            }

            //NE Direction
            //while not on Right file or top rank
            if (nextLocation % board.getBoardSize() != (board.getBoardSize() - 1) & nextLocation < (board.getBoardSize() * (board.getBoardSize() - 1)))
            {

                if (piece[nextLocation + (board.getBoardSize() + 1)] == null)
                {
                    Move move = new Move(currentLocation, nextLocation + (board.getBoardSize() + 1), this);
                    validMoves.Add(move);

                }
                else if (piece[nextLocation + (board.getBoardSize() + 1)].getIsWhite() != this.isWhite)
                {
                    Move move = new Move(currentLocation, nextLocation + (board.getBoardSize() + 1), this);
                    validMoves.Add(move);
                }
            }

            //SW Direction
            //while not on Right file or bottom rank
            if (nextLocation % board.getBoardSize() != board.getBoardSize() - 1 & nextLocation > (board.getBoardSize() - 1))
            {

                if (piece[nextLocation - (board.getBoardSize() - 1)] == null)
                {
                    Move move = new Move(currentLocation, nextLocation - (board.getBoardSize() - 1), this);
                    validMoves.Add(move);


                }

                else if (piece[nextLocation - (board.getBoardSize() - 1)].getIsWhite() != this.isWhite)
                {
                    Move move = new Move(currentLocation, nextLocation - (board.getBoardSize() - 1), this);
                    validMoves.Add(move);

                }
            }

            //SE Direction
            //while not on left file or bottom rank

            if (nextLocation % board.getBoardSize() != 0 & nextLocation > board.getBoardSize() - 1)
            {

                if (piece[nextLocation - (board.getBoardSize() + 1)] == null)
                {
                    Move move = new Move(currentLocation, nextLocation - (board.getBoardSize() + 1), this);
                    validMoves.Add(move);
                }

                else if (piece[nextLocation - (board.getBoardSize() + 1)].getIsWhite() != this.isWhite)
                {
                    Move move = new Move(currentLocation, nextLocation - (board.getBoardSize() + 1), this);
                    validMoves.Add(move);
                }
            }
            nextLocation = currentLocation;
            //left direction
            if (nextLocation % board.getBoardSize() != 0)
            {
                if (piece[nextLocation - 1] == null)
                {
                    Move move = new Move(currentLocation, nextLocation - 1, this);
                    validMoves.Add(move);
                }
                else if (piece[nextLocation - 1].getIsWhite() != this.isWhite)
                {
                    Move move = new Move(currentLocation, nextLocation - 1, this);
                    validMoves.Add(move);
                }
            }
            nextLocation = currentLocation;
            //right direction
            if (nextLocation % board.getBoardSize() != board.getBoardSize() - 1)
            {

                if (piece[nextLocation + 1] == null)
                {
                    Move move = new Move(currentLocation, nextLocation + 1, this);
                    validMoves.Add(move);

                }
                else if (piece[nextLocation + 1].getIsWhite() != this.isWhite)
                {
                    Move move = new Move(currentLocation, nextLocation + 1, this);
                    validMoves.Add(move);
                }
            }


            //up direction
            if (nextLocation < board.getBoardSize() * (board.getBoardSize() - 1))
            {
                if (piece[nextLocation + board.getBoardSize()] == null)
                {
                    Move move = new Move(currentLocation, nextLocation + board.getBoardSize(), this);
                    validMoves.Add(move);

                }
                else if (piece[nextLocation + 8].getIsWhite() != this.isWhite)
                {
                    Move move = new Move(currentLocation, nextLocation + board.getBoardSize(), this);
                    validMoves.Add(move);
                }
            }

            //down direction
            if (nextLocation > board.getBoardSize() - 1)
            {
                if (piece[nextLocation - board.getBoardSize()] == null)
                {
                    Move move = new Move(currentLocation, nextLocation - board.getBoardSize(), this);
                    validMoves.Add(move);
                }
                else if (piece[nextLocation - board.getBoardSize()].getIsWhite() != this.isWhite)
                {
                    Move move = new Move(currentLocation, nextLocation - board.getBoardSize(), this);
                    validMoves.Add(move);
                }

            }


            return validMoves;
        }

    }
}
