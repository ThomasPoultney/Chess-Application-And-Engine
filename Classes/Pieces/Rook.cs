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

        public override List<Move> generateValidMoves(Board board)
        {
            int currentLocation = this.location;
            List<Move> validMoves = new List<Move>();
            int nextLocation = currentLocation;


            //left direction
            while (nextLocation % board.getBoardSize() != 0)
            {
                if (board.getPiece()[nextLocation - 1] == null)
                {
                    Move move = new Move(currentLocation, nextLocation - 1, this);
                    validMoves.Add(move);
                    nextLocation -= 1;

                }
                else if (board.getPiece()[nextLocation - 1].getIsWhite() != this.isWhite)
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

                if (board.getPiece()[nextLocation + 1] == null)
                {
                    Move move = new Move(currentLocation, nextLocation + 1, this);
                    validMoves.Add(move);
                    nextLocation += 1;
                }
                else if (board.getPiece()[nextLocation + 1].getIsWhite() != this.isWhite)
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
                if (board.getPiece()[nextLocation + board.getBoardSize()] == null)
                {
                    Move move = new Move(currentLocation, nextLocation + board.getBoardSize(), this);
                    validMoves.Add(move);
                    nextLocation += board.getBoardSize();
                }
                else if (board.getPiece()[nextLocation + 8].getIsWhite() != this.isWhite)
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
                if (board.getPiece()[nextLocation - 8] == null)
                {
                    Move move = new Move(currentLocation, nextLocation - board.getBoardSize(), this);
                    validMoves.Add(move);

                    nextLocation -= board.getBoardSize();
                }
                else if (board.getPiece()[nextLocation - 8].getIsWhite() != this.isWhite)
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
