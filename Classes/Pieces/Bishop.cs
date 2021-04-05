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
            this.letterRepresentation = 'B';
            if (isWhite == true)
            {
                this.imagePath = "C:/Users/tompo/source/repos/ChessAI/ChessImages/wB.PNG";
            }
            else
            {
                this.imagePath = "C:/Users/tompo/source/repos/ChessAI/ChessImages/bB.PNG"; ;
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




            return validMoves;
        }
    }
}
