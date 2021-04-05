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
            this.letterRepresentation = 'P';
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

        public override List<Move> generateValidMoves(Board board, Piece[] piece, int location)
        {
            int currentLocation = location;
            List<Move> validMoves = new List<Move>();
            int nextLocation = this.location;
            //valid moves for pawns moving up board
            if (movingUp == true)
            {
                //check if we can move forward one if not on top rank
                if (nextLocation < (board.getBoardSize() * (board.getBoardSize() - 1)))
                {
                    if (piece[nextLocation + board.getBoardSize()] == null)
                    {
                        Move moveOneForward = new Move(nextLocation, nextLocation + board.getBoardSize(), this);
                        if (moveOneForward.getEndLocation() >= (board.getBoardSize() * (board.getBoardSize() - 1)) || moveOneForward.getEndLocation() < board.getBoardSize())
                        {
                            validMoves.Add(new Move(moveOneForward.getStartLocation(), moveOneForward.getEndLocation(), moveOneForward.getPiece(), "promoteToRook"));
                            validMoves.Add(new Move(moveOneForward.getStartLocation(), moveOneForward.getEndLocation(), moveOneForward.getPiece(), "promoteToQueen"));
                            validMoves.Add(new Move(moveOneForward.getStartLocation(), moveOneForward.getEndLocation(), moveOneForward.getPiece(), "promoteToKnight"));
                            validMoves.Add(new Move(moveOneForward.getStartLocation(), moveOneForward.getEndLocation(), moveOneForward.getPiece(), "promoteToBishop"));
                        }
                        else
                        {
                            validMoves.Add(moveOneForward);
                        }

                        //check if we can move forward two if piece hasnt moved yet and not on second from last rank and can moveforward

                        if (!board.pawnsThatHaveMoved.Contains(this))
                        {
                            if (nextLocation + board.getBoardSize() <= (board.getBoardSize() * (board.getBoardSize() - 2)))
                            {
                                if (piece[nextLocation + (board.getBoardSize() * 2)] == null)
                                {
                                    Move move = new Move(nextLocation, nextLocation + (board.getBoardSize() * 2), this);
                                    if (move.getEndLocation() >= board.getBoardSize() * (board.getBoardSize() - 1) || move.getEndLocation() < board.getBoardSize())
                                    {
                                        validMoves.Add(new Move(move.getStartLocation(), move.getEndLocation(), move.getPiece(), "promoteToRook"));
                                        validMoves.Add(new Move(move.getStartLocation(), move.getEndLocation(), move.getPiece(), "promoteToQueen"));
                                        validMoves.Add(new Move(move.getStartLocation(), move.getEndLocation(), move.getPiece(), "promoteToKnight"));
                                        validMoves.Add(new Move(move.getStartLocation(), move.getEndLocation(), move.getPiece(), "promoteToBishop"));
                                    }
                                    else
                                    {
                                        validMoves.Add(move);
                                    }

                                }
                            }
                        }

                    }

                    //check if we can take diagonally
                    //if not on left file check left diagonal
                    if (nextLocation % board.getBoardSize() != 0)
                    {
                        if (piece[nextLocation + (board.getBoardSize() - 1)] != null)
                        {
                            if (piece[nextLocation + (board.getBoardSize() - 1)].getIsWhite() != this.getIsWhite())
                            {
                                Move move = new Move(nextLocation, nextLocation + (board.getBoardSize() - 1), this);
                                if (move.getEndLocation() >= board.getBoardSize() * (board.getBoardSize() - 1) || move.getEndLocation() < board.getBoardSize())
                                {
                                    validMoves.Add(new Move(move.getStartLocation(), move.getEndLocation(), move.getPiece(), "promoteToRook"));
                                    validMoves.Add(new Move(move.getStartLocation(), move.getEndLocation(), move.getPiece(), "promoteToQueen"));
                                    validMoves.Add(new Move(move.getStartLocation(), move.getEndLocation(), move.getPiece(), "promoteToKnight"));
                                    validMoves.Add(new Move(move.getStartLocation(), move.getEndLocation(), move.getPiece(), "promoteToBishop"));
                                }
                                else
                                {
                                    validMoves.Add(move);
                                }

                            }
                        }
                        else
                        {
                            if (board.getEnPassantLocation() == nextLocation + (board.getBoardSize() - 1))
                            {
                                Move move = new Move(currentLocation, currentLocation + (board.getBoardSize() - 1), this, "enPassant");
                                validMoves.Add(move);
                            }
                        }
                    }


                    //if not on Right file check left diagonal
                    if (nextLocation % board.getBoardSize() != (board.getBoardSize() - 1))
                    {
                        if (piece[nextLocation + (board.getBoardSize() + 1)] != null)
                        {
                            if (piece[nextLocation + (board.getBoardSize() + 1)].getIsWhite() != this.getIsWhite())
                            {
                                Move move = new Move(nextLocation, nextLocation + (board.getBoardSize() + 1), this);
                                if (move.getEndLocation() >= board.getBoardSize() * (board.getBoardSize() - 1) || move.getEndLocation() < board.getBoardSize())
                                {
                                    validMoves.Add(new Move(move.getStartLocation(), move.getEndLocation(), move.getPiece(), "promoteToRook"));
                                    validMoves.Add(new Move(move.getStartLocation(), move.getEndLocation(), move.getPiece(), "promoteToQueen"));
                                    validMoves.Add(new Move(move.getStartLocation(), move.getEndLocation(), move.getPiece(), "promoteToKnight"));
                                    validMoves.Add(new Move(move.getStartLocation(), move.getEndLocation(), move.getPiece(), "promoteToBishop"));
                                }
                                else
                                {
                                    validMoves.Add(move);
                                }

                            }
                        }
                        else
                        {
                            if (board.getEnPassantLocation() == nextLocation + (board.getBoardSize() + 1))
                            {
                                Move move = new Move(currentLocation, nextLocation + (board.getBoardSize() + 1), this, "enPassant");
                                validMoves.Add(move);
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
                    if (piece[nextLocation - board.getBoardSize()] == null)
                    {
                        Move moveOne = new Move(nextLocation, nextLocation - board.getBoardSize(), this);

                        if (moveOne.getEndLocation() >= board.getBoardSize() * (board.getBoardSize() - 1) || moveOne.getEndLocation() < board.getBoardSize())
                        {
                            validMoves.Add(new Move(moveOne.getStartLocation(), moveOne.getEndLocation(), moveOne.getPiece(), "promoteToRook"));
                            validMoves.Add(new Move(moveOne.getStartLocation(), moveOne.getEndLocation(), moveOne.getPiece(), "promoteToQueen"));
                            validMoves.Add(new Move(moveOne.getStartLocation(), moveOne.getEndLocation(), moveOne.getPiece(), "promoteToKnight"));
                            validMoves.Add(new Move(moveOne.getStartLocation(), moveOne.getEndLocation(), moveOne.getPiece(), "promoteToBishop"));
                        }
                        else
                        {
                            validMoves.Add(moveOne);
                        }

                        //check if we can move down two if piece hasnt moved yet and not on first or second rank

                        if (!board.pawnsThatHaveMoved.Contains(this))
                        {
                            if (nextLocation > ((board.getBoardSize() * 2) - 1))
                            {
                                if (piece[nextLocation - (board.getBoardSize() * 2)] == null)
                                {
                                    Move move = new Move(nextLocation, nextLocation - (board.getBoardSize() * 2), this);
                                    if (move.getEndLocation() >= board.getBoardSize() * (board.getBoardSize() - 1) || move.getEndLocation() < board.getBoardSize())
                                    {
                                        validMoves.Add(new Move(move.getStartLocation(), move.getEndLocation(), move.getPiece(), "promoteToRook"));
                                        validMoves.Add(new Move(move.getStartLocation(), move.getEndLocation(), move.getPiece(), "promoteToQueen"));
                                        validMoves.Add(new Move(move.getStartLocation(), move.getEndLocation(), move.getPiece(), "promoteToKnight"));
                                        validMoves.Add(new Move(move.getStartLocation(), move.getEndLocation(), move.getPiece(), "promoteToBishop"));
                                    }
                                    else
                                    {
                                        validMoves.Add(move);
                                    }

                                }
                            }
                        }

                    }

                    //check if we can take diagonally
                    //if not on left file check left diagonal
                    if (nextLocation % board.getBoardSize() != 0)
                    {
                        if (piece[nextLocation - (board.getBoardSize() + 1)] != null)
                        {
                            if (piece[nextLocation - (board.getBoardSize() + 1)].getIsWhite() != this.getIsWhite())
                            {
                                Move move = new Move(nextLocation, nextLocation - (board.getBoardSize() + 1), this);
                                if (move.getEndLocation() >= board.getBoardSize() * (board.getBoardSize() - 1) || move.getEndLocation() < board.getBoardSize())
                                {
                                    validMoves.Add(new Move(move.getStartLocation(), move.getEndLocation(), move.getPiece(), "promoteToRook"));
                                    validMoves.Add(new Move(move.getStartLocation(), move.getEndLocation(), move.getPiece(), "promoteToQueen"));
                                    validMoves.Add(new Move(move.getStartLocation(), move.getEndLocation(), move.getPiece(), "promoteToKnight"));
                                    validMoves.Add(new Move(move.getStartLocation(), move.getEndLocation(), move.getPiece(), "promoteToBishop"));
                                }
                                else
                                {
                                    validMoves.Add(move);
                                }
                            }
                        }
                        else
                        {
                            if (board.getEnPassantLocation() == nextLocation - (board.getBoardSize() + 1))
                            {
                                Move move = new Move(currentLocation, (nextLocation - (board.getBoardSize() + 1)), this, "enPassant");
                                validMoves.Add(move);

                            }
                        }
                    }


                    //if not on Right file check left diagonal
                    if (nextLocation % board.getBoardSize() != (board.getBoardSize() - 1))
                    {
                        if (piece[nextLocation - (board.getBoardSize() - 1)] != null)
                        {
                            if (piece[nextLocation - (board.getBoardSize() - 1)].getIsWhite() != this.getIsWhite())
                            {
                                Move move = new Move(nextLocation, nextLocation - (board.getBoardSize() - 1), this);
                                if (move.getEndLocation() >= board.getBoardSize() * (board.getBoardSize() - 1) || move.getEndLocation() < board.getBoardSize())
                                {
                                    validMoves.Add(new Move(move.getStartLocation(), move.getEndLocation(), move.getPiece(), "promoteToRook"));
                                    validMoves.Add(new Move(move.getStartLocation(), move.getEndLocation(), move.getPiece(), "promoteToQueen"));
                                    validMoves.Add(new Move(move.getStartLocation(), move.getEndLocation(), move.getPiece(), "promoteToKnight"));
                                    validMoves.Add(new Move(move.getStartLocation(), move.getEndLocation(), move.getPiece(), "promoteToBishop"));
                                }
                                else
                                {
                                    validMoves.Add(move);
                                }
                            }
                        }
                        else
                        {
                            if (board.getEnPassantLocation() == nextLocation - (board.getBoardSize() - 1))
                            {
                                Move move = new Move(currentLocation, nextLocation - (board.getBoardSize() - 1), this, "enPassant");
                                validMoves.Add(move);
                            }
                        }
                    }

                }
            }


            return validMoves;
        }

        //only returns the diagonal taking moves used for checkmate and check checking
        public override List<Move> generateAttackingMoves(Board board, int location)
        {
            int currentLocation = location;
            List<Move> validMoves = new List<Move>();

            if (movingUp == true)
            {
                //if not on top rank
                if (currentLocation < (board.getBoardSize() * (board.getBoardSize() - 1)))
                {
                    //if not on left file
                    if (currentLocation % board.getBoardSize() > 0)
                    {
                        Move move = new Move(currentLocation, currentLocation + (board.getBoardSize() - 1), this);
                        validMoves.Add(move);
                    }

                    //if not on Right file
                    if (currentLocation % board.getBoardSize() < board.getBoardSize() - 1)
                    {

                        Move move = new Move(currentLocation, currentLocation + (board.getBoardSize() + 1), this);
                        validMoves.Add(move);
                    }

                }
            }
            else
            {
                //if not on bottom rank
                if (currentLocation > ((board.getBoardSize() - 1)))
                {
                    //if not on left file
                    if (currentLocation % board.getBoardSize() > 0)
                    {

                        Move move = new Move(currentLocation, currentLocation - (board.getBoardSize() + 1), this);
                        validMoves.Add(move);
                    }

                    //if not on Right file
                    if (currentLocation % board.getBoardSize() < board.getBoardSize() - 1)
                    {
                        Move move = new Move(currentLocation, currentLocation - (board.getBoardSize() - 1), this);
                        validMoves.Add(move);
                    }

                }
            }

            return validMoves;
        }

    }
}
