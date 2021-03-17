using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessB
{
    class King : Piece
    {
        private bool hasMoved = false;
        public King(bool isWhite, int location) : base(isWhite, location)
        {

            this.location = location;
            this.isWhite = isWhite;
            this.strength = 4;
            this.letterRepresentation = 'K';
            if (isWhite == true)
            {
                this.imagePath = "C:/Users/tompo/source/repos/ChessAI/ChessImages/wK.PNG";
            }
            else
            {
                this.imagePath = "C:/Users/tompo/source/repos/ChessAI/ChessImages/bK.PNG";
            }
        }

        public override bool getHasMoved()
        {
            return this.hasMoved;
        }

        public override void setHasMoved(bool hasMoved)
        {
            this.hasMoved = hasMoved;
        }

        public override List<Move> generateValidMoves(Board board, Piece[] piece, List<int> blackAttackingMoves, List<int> whiteAttackingMoves)
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


            /*Castling consists of moving the king two squares towards a rook on the player's first rank, 
             * then moving the rook to the square that the king crossed.
             * Castling may be done only if the king has never moved, 
             * the rook involved has never moved, the squares between the king and the rook involved are unoccupied, 
             * the king is not in check, and the king does not cross over or end on a square attacked by an enemy piece.
            */
            if (hasMoved == false)
            {
                List<int> tilesAttacked;

                if (this.getIsWhite() == true)
                {
                    tilesAttacked = blackAttackingMoves;
                }
                else
                {
                    tilesAttacked = whiteAttackingMoves;
                }

                //if king is not in check
                if (!tilesAttacked.Contains(this.location))
                {
                    int rookShortLocation = 0;
                    int rookLongLocation = 0;
                    //find rook locations
                    if (this.location == 4)
                    {
                        rookLongLocation = 0;
                        rookShortLocation = board.getBoardSize() - 1;
                    }
                    else if (this.location == ((board.getBoardSize() * (board.getBoardSize() - 1)) + 4))
                    {
                        rookLongLocation = (board.getBoardSize() * (board.getBoardSize() - 1));
                        rookShortLocation = (board.getBoardSize() * board.getBoardSize()) - 1;
                    }

                    //if the rook short hasnt moved
                    if (piece[rookLongLocation] is Rook && piece[rookLongLocation].getHasMoved() == false)
                    {
                        bool inBetweenAttacked = false;
                        bool squaresBetweenNotEmpty = false;
                        for (int i = rookLongLocation + 1; i < this.location; i++)
                        {
                            if (i != rookLongLocation + 1)
                            {
                                if (tilesAttacked.Contains(i))
                                {
                                    inBetweenAttacked = true;
                                }
                            }


                            if (piece[i] != null)
                            {
                                squaresBetweenNotEmpty = true;
                            }
                        }

                        if (inBetweenAttacked == false && squaresBetweenNotEmpty == false)
                        {
                            Move move = new Move(currentLocation, currentLocation - 2, this, "CastleLong", piece[rookLongLocation]);
                            validMoves.Add(move);
                        }


                    }

                    if (piece[rookShortLocation] is Rook && piece[rookShortLocation].getHasMoved() == false)
                    {
                        bool inBetweenAttacked = false;
                        bool squaresBetweenNotEmpty = false;
                        for (int i = rookShortLocation - 1; i > this.location; i--)
                        {
                            if (tilesAttacked.Contains(i))
                            {
                                inBetweenAttacked = true;
                            }

                            if (piece[i] != null)
                            {
                                squaresBetweenNotEmpty = true;
                            }
                        }

                        if (inBetweenAttacked == false && squaresBetweenNotEmpty == false)
                        {
                            Move move = new Move(currentLocation, currentLocation + 2, this, "CastleShort", piece[rookShortLocation]);
                            validMoves.Add(move);
                        }
                    }



                }
            }
            return validMoves;
        }
    }
}
