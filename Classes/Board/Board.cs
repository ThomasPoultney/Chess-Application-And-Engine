using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChessB
{
    public class Board
    {
        /* Forsyth–Edwards Notation(FEN) describing the current state of the board. 
        By default the starting position of the board */
        private String fenString = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

        private bool isWhiteTurn = true;

        private int blackKingLocation;
        private int whiteKingLocation;

        private bool blackCanCastle = true;
        private bool whiteCanCastle = true;

        /*This is the number of halfmoves since the last capture or pawn advance. 
        The field is used in the fifty-move rule.*/
        private int numHalfMoves = 50;

        //The number of the full move. It starts at 1, and is incremented after Black's move.
        private int fullMoveNumber = 1;


        //represents current state of board
        private Piece[] piece;

        //stores each move that is made
        private List<Move> moves = new List<Move>();

        //stores all tiles being attacked by a player, used for checking for checkmate. 
        private List<int> whiteAttacking;
        private List<int> blackAttacking;

        public static int boardSize = 8;

        public Board()
        {
            piece = new Piece[boardSize * boardSize];
        }

        private List<Move> Moves { get => moves; set => moves = value; }

        public Piece[] getPiece()
        {
            return this.piece;
        }

        public void setPiece(Piece[] piece)
        {
            this.piece = piece;
        }

        public void setPieceAtLocation(int location, Piece piece)
        {
            if (piece != null)
            {
                piece.setLocation(location);
            }

            this.piece[location] = piece;

        }

        public int getBoardSize()
        {
            return boardSize;
        }

        public bool getIsWhiteTurn()
        {
            return isWhiteTurn;
        }

        public void setIsWhiteTurn(bool isWhiteTurn)
        {
            this.isWhiteTurn = isWhiteTurn;
        }

        public List<Move> removeMovesThatPutInCheck(List<Move> validMoves)
        {
            List<Move> finalvalidMoves = new List<Move>();


            foreach (Move validMove in validMoves)
            {
                Piece[] boardStateAfterMove = this.getPiece().ToArray();
                int pieceEndLocation = validMove.getEndLocation();
                int pieceStartLocation = validMove.getStartLocation();
                Piece piece = validMove.getPiece();

                boardStateAfterMove[pieceEndLocation] = piece;
                boardStateAfterMove[pieceStartLocation] = null;
                int bKingLocation = this.blackKingLocation;
                int wKingLocation = this.whiteKingLocation;

                if (piece is ChessB.King)
                {
                    if (piece.getIsWhite() == true)
                    {
                        wKingLocation = pieceEndLocation;
                    }
                    else
                    {
                        bKingLocation = pieceEndLocation;
                    }
                }

                List<int> attackingMoves;

                if (piece.getIsWhite() == true)
                {
                    attackingMoves = generateBlackAttackingMoves(boardStateAfterMove);
                    if (!attackingMoves.Contains(wKingLocation))
                    {
                        finalvalidMoves.Add(validMove);
                    }
                }
                else
                {
                    attackingMoves = generateWhiteAttackingMoves(boardStateAfterMove);
                    if (!attackingMoves.Contains(bKingLocation))
                    {
                        finalvalidMoves.Add(validMove);
                    }
                }
            }

            return finalvalidMoves;
        }
        public bool makeMove(Move move)
        {
            int pieceEndLocation = move.getEndLocation();
            int pieceStartLocation = move.getStartLocation();
            Piece piece = move.getPiece();
            bool moveIsValid = false;
            foreach (Move validmove in Game.validMoves)
            {
                if (validmove.Equals(move))
                {
                    moveIsValid = true;
                }
            }

            if (moveIsValid == false)
            {
                return false;
            }
            else
            {

                if (this.getPiece()[pieceEndLocation] != null)
                {
                    Ui.canvas.Children.Remove(Game.activeBoard.getPiece()[pieceEndLocation].getImage());

                    if (this.getPiece()[pieceEndLocation].getIsWhite())
                    {
                        Image image = Game.activeBoard.getPiece()[pieceEndLocation].getImage();
                        Ui.addBlackCaptureImage(image, this.getPiece()[pieceEndLocation].getStrength());
                    }
                    else
                    {
                        Image image = Game.activeBoard.getPiece()[pieceEndLocation].getImage();
                        Ui.addWhiteCaptureImage(image, this.getPiece()[pieceEndLocation].getStrength());
                    }

                    this.getPiece()[pieceEndLocation] = null;

                }

                if (piece is ChessB.Pawn)
                {
                    piece.setCanMoveTwice(false);
                }

                if (piece is ChessB.King)
                {
                    if (piece.getIsWhite() == true)
                    {
                        this.whiteKingLocation = pieceEndLocation;
                    }
                    else
                    {
                        this.blackKingLocation = pieceEndLocation;
                    }
                }

                this.setPieceAtLocation(pieceEndLocation, piece);
                this.setPieceAtLocation(pieceStartLocation, null);
                this.setIsWhiteTurn(!this.getIsWhiteTurn());
                this.setUpNextTurn();
                moves.Add(move);
                return true;
            }


        }

        private void setUpNextTurn()
        {
            //generate all current players moves
            //generate all openents moves, check if we are in check// if in check play check sounds
            //if current player has no valid moves and we are in check then oppenent wins
            //if current player has valid moves and not in check it is stalemate

        }

        public List<int> generateWhiteAttackingMoves(Piece[] pieceArray)
        {
            List<Move> whiteAttackingMoves = new List<Move>();

            foreach (Piece piece in pieceArray)
            {
                if (piece != null)
                {

                    if (piece.GetType() != typeof(Pawn))
                    {
                        if (piece.getIsWhite() == true)
                        {
                            whiteAttackingMoves.AddRange(piece.generateValidMoves(this, pieceArray));
                        }

                    }
                    else
                    {
                        if (piece.getIsWhite() == true)
                        {
                            whiteAttackingMoves.AddRange(piece.generateAttackingMoves(this));
                        }
                    }

                }
            }

            List<int> tilesAttacked = new List<int>();

            foreach (Move move in whiteAttackingMoves)
            {
                tilesAttacked.Add(move.getEndLocation());
            }

            return tilesAttacked.Distinct().ToList();
        }


        public List<int> generateBlackAttackingMoves(Piece[] pieceArray)
        {
            List<Move> blackAttackingMoves = new List<Move>();

            foreach (Piece piece in pieceArray)
            {
                if (piece != null)
                {

                    if (piece.GetType() != typeof(Pawn))
                    {
                        if (piece.getIsWhite() == false)
                        {
                            blackAttackingMoves.AddRange(piece.generateValidMoves(this, pieceArray));
                        }

                    }
                    else
                    {
                        if (piece.getIsWhite() == false)
                        {
                            blackAttackingMoves.AddRange(piece.generateAttackingMoves(this));
                        }

                    }
                }


            }

            List<int> tilesAttacked = new List<int>();

            foreach (Move move in blackAttackingMoves)
            {
                tilesAttacked.Add(move.getEndLocation());
            }

            return tilesAttacked.Distinct().ToList();
        }


        public void generateBoardFromFEN()
        {
            char[] charFenArray = this.fenString.ToCharArray();
            //Fen string starts from top left of board
            int currentFile = 0;
            int currentRank = boardSize - 1;
            //Capital letter = White, lower case = black

            foreach (char symbol in charFenArray)
            {
                int location = currentRank * boardSize + currentFile;
                if (symbol == '/')
                {
                    currentFile = 0;
                    currentRank--;
                }
                else if (symbol == 'R')
                {
                    Rook rook = new Rook(true, location);
                    piece[location] = rook;
                    currentFile++;
                }
                else if (symbol == 'r')
                {
                    Rook rook = new Rook(false, location);
                    piece[location] = rook;
                    currentFile++;
                }
                else if (symbol == 'N')
                {
                    Knight knight = new Knight(true, location);
                    piece[location] = knight;
                    currentFile++;
                }
                else if (symbol == 'n')
                {
                    Knight knight = new Knight(false, location);
                    piece[location] = knight;
                    currentFile++;
                }
                else if (symbol == 'B')
                {
                    Bishop bishop = new Bishop(true, location);
                    piece[location] = bishop;
                    currentFile++;
                }
                else if (symbol == 'b')
                {
                    Bishop bishop = new Bishop(false, location);
                    piece[location] = bishop;
                    currentFile++;
                }

                else if (symbol == 'Q')
                {
                    Queen queen = new Queen(true, location);
                    piece[location] = queen;
                    currentFile++;
                }
                else if (symbol == 'q')
                {
                    Queen queen = new Queen(false, location);
                    piece[location] = queen;
                    currentFile++;
                }
                else if (symbol == 'K')
                {
                    King king = new King(true, location);
                    piece[location] = king;
                    this.whiteKingLocation = location;
                    currentFile++;
                }
                else if (symbol == 'k')
                {
                    King king = new King(false, location);
                    piece[location] = king;
                    this.blackKingLocation = location;
                    currentFile++;
                }
                else if (symbol == 'P')
                {
                    Pawn pawn = new Pawn(true, location);
                    piece[location] = pawn;
                    currentFile++;
                }
                else if (symbol == 'p')
                {
                    Pawn pawn = new Pawn(false, location);
                    pawn.setMovingUp(false);
                    piece[location] = pawn;
                    currentFile++;
                }
                else if (char.IsNumber(symbol))
                {
                    currentFile += (int)symbol;
                }
                else if (symbol == 'w')
                {
                    isWhiteTurn = true;
                    return;
                }
                else if (symbol == 'b')
                {
                    isWhiteTurn = false;
                    return;
                }
            }
        }
    }
}
