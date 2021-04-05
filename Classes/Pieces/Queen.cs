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
            this.letterRepresentation = 'Q';
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

        public override List<Move> generateValidMoves(Board board, Piece[] piece, int location)
        {
            int currentLocation = location;
            List<Move> validMoves = new List<Move>();
            int nextLocation = currentLocation;


            //NW Direction
            //while not on left file or top rank
            while (nextLocation % board.getBoardSize() != 0 & nextLocation < (board.getBoardSize() * (board.getBoardSize() - 1)))
            {

                if (piece[nextLocation + (board.getBoardSize() - 1)] == null)
                {
                    Move move = new Move(currentLocation, nextLocation + (board.getBoardSize() - 1), this);
                    validMoves.Add(move);
                    nextLocation += (board.getBoardSize() - 1);
                }
                else if (piece[nextLocation + (board.getBoardSize() - 1)].getIsWhite() == this.isWhite)
                {
                    break;
                }
                else if (piece[nextLocation + (board.getBoardSize() - 1)].getIsWhite() != this.isWhite)
                {
                    Move move = new Move(currentLocation, nextLocation + (board.getBoardSize() - 1), this);
                    validMoves.Add(move);
                    break;
                }
            }

            //NE Direction
            //while not on Right file or top rank
            nextLocation = currentLocation;
            while (nextLocation % board.getBoardSize() != (board.getBoardSize() - 1) & nextLocation < (board.getBoardSize() * (board.getBoardSize() - 1)))
            {

                if (piece[nextLocation + (board.getBoardSize() + 1)] == null)
                {
                    Move move = new Move(currentLocation, nextLocation + (board.getBoardSize() + 1), this);
                    validMoves.Add(move);

                    nextLocation += (board.getBoardSize() + 1);
                }
                else if (piece[nextLocation + (board.getBoardSize() + 1)].getIsWhite() == this.isWhite)
                {
                    break;
                }
                else if (piece[nextLocation + (board.getBoardSize() + 1)].getIsWhite() != this.isWhite)
                {
                    Move move = new Move(currentLocation, nextLocation + (board.getBoardSize() + 1), this);
                    validMoves.Add(move);
                    break;
                }
            }

            //SW Direction
            //while not on Right file or bottom rank
            nextLocation = currentLocation;
            while (nextLocation % board.getBoardSize() != board.getBoardSize() - 1 & nextLocation > (board.getBoardSize() - 1))
            {

                if (piece[nextLocation - (board.getBoardSize() - 1)] == null)
                {
                    Move move = new Move(currentLocation, nextLocation - (board.getBoardSize() - 1), this);
                    validMoves.Add(move);
                    nextLocation -= (board.getBoardSize() - 1);
                }
                else if (piece[nextLocation - (board.getBoardSize() - 1)].getIsWhite() == this.isWhite)
                {
                    break;
                }
                else if (piece[nextLocation - (board.getBoardSize() - 1)].getIsWhite() != this.isWhite)
                {
                    Move move = new Move(currentLocation, nextLocation - (board.getBoardSize() - 1), this);
                    validMoves.Add(move);
                    break;
                }
            }

            //SE Direction
            //while not on left file or bottom rank
            nextLocation = currentLocation;
            while (nextLocation % board.getBoardSize() != 0 & nextLocation > board.getBoardSize() - 1)
            {

                if (piece[nextLocation - (board.getBoardSize() + 1)] == null)
                {
                    Move move = new Move(currentLocation, nextLocation - (board.getBoardSize() + 1), this);
                    validMoves.Add(move);
                    nextLocation -= (board.getBoardSize() + 1);
                }
                else if (piece[nextLocation - (board.getBoardSize() + 1)].getIsWhite() == this.isWhite)
                {
                    break;
                }
                else if (piece[nextLocation - (board.getBoardSize() + 1)].getIsWhite() != this.isWhite)
                {
                    Move move = new Move(currentLocation, nextLocation - (board.getBoardSize() + 1), this);
                    validMoves.Add(move);
                    break;
                }
            }
            nextLocation = currentLocation;
            //left direction
            while (nextLocation % board.getBoardSize() != 0)
            {
                if (piece[nextLocation - 1] == null)
                {
                    Move move = new Move(currentLocation, nextLocation - 1, this);
                    validMoves.Add(move);
                    nextLocation -= 1;

                }
                else if (piece[nextLocation - 1].getIsWhite() != this.isWhite)
                {
                    Move move = new Move(currentLocation, nextLocation - 1, this);
                    validMoves.Add(move);
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

                if (piece[nextLocation + 1] == null)
                {
                    Move move = new Move(currentLocation, nextLocation + 1, this);
                    validMoves.Add(move);
                    nextLocation += 1;
                }
                else if (piece[nextLocation + 1].getIsWhite() != this.isWhite)
                {
                    Move move = new Move(currentLocation, nextLocation + 1, this);
                    validMoves.Add(move);
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
                if (piece[nextLocation + board.getBoardSize()] == null)
                {
                    Move move = new Move(currentLocation, nextLocation + board.getBoardSize(), this);
                    validMoves.Add(move);
                    nextLocation += board.getBoardSize();
                }
                else if (piece[nextLocation + 8].getIsWhite() != this.isWhite)
                {
                    Move move = new Move(currentLocation, nextLocation + board.getBoardSize(), this);
                    validMoves.Add(move);
                    nextLocation += board.getBoardSize();
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
                if (piece[nextLocation - 8] == null)
                {
                    Move move = new Move(currentLocation, nextLocation - board.getBoardSize(), this);
                    validMoves.Add(move);
                    nextLocation -= board.getBoardSize();
                }
                else if (piece[nextLocation - 8].getIsWhite() != this.isWhite)
                {
                    Move move = new Move(currentLocation, nextLocation - board.getBoardSize(), this);
                    validMoves.Add(move);
                    nextLocation -= board.getBoardSize();
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
