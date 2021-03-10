using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessB
{
    class Pawn : Piece
    {
        private bool canMoveTwice = true;
        private bool movingUp = true;

        public Pawn(bool isWhite, int location) : base(isWhite, location)
        {

            this.location = location;
            this.isWhite = isWhite;
            this.strength = 1;
            if (isWhite == true)
            {
                this.imagePath = "C:/Users/tompo/source/repos/ChessAI/ChessImages/wP.PNG"; ;
            }
            else
            {
                this.imagePath = "C:/Users/tompo/source/repos/ChessAI/ChessImages/bP.PNG"; ;
            }

        }

        public void setMovingUp(bool movingUp)
        {
            this.movingUp = movingUp;
        }



        public override void setCanMoveTwice(bool canMoveTwice)
        {
            this.canMoveTwice = canMoveTwice;
        }

        public override List<int> generateValidMoves(Board board)
        {
            int currentLocation = this.location;
            List<int> validMoves = new List<int>();
            int nextLocation = this.location;
            //valid moves for pawns moving up board
            if (movingUp == true)
            {
                //check if we can move forward one if not on top rank
                if (nextLocation < (board.getBoardSize() * (board.getBoardSize() - 1)))
                {
                    if (board.getPiece()[nextLocation + board.getBoardSize()] == null)
                    {
                        validMoves.Add(nextLocation + board.getBoardSize());

                        //check if we can move forward two if piece hasnt moved yet and not on second from last rank and can moveforward

                        if (canMoveTwice == true)
                        {
                            if (nextLocation + board.getBoardSize() < (board.getBoardSize() * (board.getBoardSize() - 2)))
                            {
                                if (board.getPiece()[nextLocation + (board.getBoardSize() * 2)] == null)
                                {
                                    validMoves.Add(nextLocation + (board.getBoardSize() * 2));
                                }
                            }
                        }

                    }

                    //check if we can take diagonally
                    //if not on left file check left diagonal
                    if (nextLocation % board.getBoardSize() != 0)
                    {
                        if (board.getPiece()[nextLocation + (board.getBoardSize() - 1)] != null)
                        {
                            if (board.getPiece()[nextLocation + (board.getBoardSize() - 1)].getIsWhite() != this.getIsWhite())
                            {
                                validMoves.Add(nextLocation + (board.getBoardSize() - 1));
                            }
                        }
                    }


                    //if not on Right file check left diagonal
                    if (nextLocation % board.getBoardSize() != (board.getBoardSize() - 1))
                    {
                        if (board.getPiece()[nextLocation + (board.getBoardSize() + 1)] != null)
                        {
                            if (board.getPiece()[nextLocation + (board.getBoardSize() + 1)].getIsWhite() != this.getIsWhite())
                            {
                                validMoves.Add(nextLocation + (board.getBoardSize() + 1));
                            }
                        }
                    }

                }

                //pawns moving down
            }
            else
            {
                //if not on bottom rank
                if (nextLocation > (board.getBoardSize() - 1))
                {
                    if (board.getPiece()[nextLocation - board.getBoardSize()] == null)
                    {
                        validMoves.Add(nextLocation - board.getBoardSize());

                        //check if we can move down two if piece hasnt moved yet and not on first or second rank

                        if (canMoveTwice == true)
                        {
                            if (nextLocation > ((board.getBoardSize() * 2) - 1))
                            {
                                if (board.getPiece()[nextLocation - (board.getBoardSize() * 2)] == null)
                                {
                                    validMoves.Add(nextLocation - (board.getBoardSize() * 2));
                                }
                            }
                        }

                    }

                    //check if we can take diagonally
                    //if not on left file check left diagonal
                    if (nextLocation % board.getBoardSize() != 0)
                    {
                        if (board.getPiece()[nextLocation - (board.getBoardSize() - 1)] != null)
                        {
                            if (board.getPiece()[nextLocation - (board.getBoardSize() - 1)].getIsWhite() != this.getIsWhite())
                            {
                                validMoves.Add(nextLocation - (board.getBoardSize() - 1));
                            }
                        }
                    }


                    //if not on Right file check left diagonal
                    if (nextLocation % board.getBoardSize() != (board.getBoardSize() - 1))
                    {
                        if (board.getPiece()[nextLocation - (board.getBoardSize() + 1)] != null)
                        {
                            if (board.getPiece()[nextLocation - (board.getBoardSize() + 1)].getIsWhite() != this.getIsWhite())
                            {
                                validMoves.Add(nextLocation - (board.getBoardSize() + 1));
                            }
                        }
                    }

                }
            }

            return validMoves;
        }

    }
}
