using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

        /*This is the number of halfmoves since the last capture or pawn advance. 
        The field is used in the fifty-move rule.*/
        private int fiftyMoveRule = 50;

        private int moveNumber = 0;
        //The number of the full move. It starts at 1, and is incremented after Black's move.
        private int fullMoveNumber = 1;

        private int enPassantLocation;

        //represents current state of board
        private Piece[] piece = new Piece[boardSize * boardSize];

        //stores each move that is made
        public List<Move> moves = new List<Move>();

        //stores all tiles being attacked by a player, used for checking for checkmate. 
        private List<int> whiteAttacking = new List<int>();
        private List<int> blackAttacking = new List<int>();

        private List<MoveableImage> whiteCapturedImages = new List<MoveableImage>();
        private List<MoveableImage> blackCapturedImages = new List<MoveableImage>();

        private List<int> whiteCapturedPieceValue = new List<int>();
        private List<int> blackCapturedPieceValue = new List<int>();
        private List<Move> validMoves = new List<Move>();

        private bool whiteInCheck = false;
        private bool blackInCheck = false;
        public bool whiteWins;
        public bool blackWins;
        public bool draw;
        public static int boardSize = 8;

        public List<Piece> pawnsThatHaveMoved = new List<Piece>();
        public List<Piece> rooksThatHaveMoved = new List<Piece>();
        public List<Piece> kingThatHaveMoved = new List<Piece>();



        public Board()
        {
            piece = new Piece[boardSize * boardSize];

        }
        //creates a new board with another boards values;
        public Board(Board board)
        {
            //creates a copy of piece array so that we lose the reference to currents boards piece array
            this.piece = (Piece[])board.getPiece().Clone();
            this.isWhiteTurn = board.isWhiteTurn;
            this.fiftyMoveRule = board.fiftyMoveRule;
            this.blackKingLocation = board.blackKingLocation;
            this.whiteKingLocation = board.whiteKingLocation;
            this.moveNumber = board.moveNumber;
            this.enPassantLocation = board.enPassantLocation;

            this.blackAttacking = board.blackAttacking;
            this.whiteAttacking = board.whiteAttacking;

            this.whiteCapturedImages = board.whiteCapturedImages.ToList();
            this.blackCapturedImages = board.blackCapturedImages.ToList();
            this.blackCapturedPieceValue = board.blackCapturedPieceValue.ToList();
            this.whiteCapturedPieceValue = board.whiteCapturedPieceValue.ToList();

            this.pawnsThatHaveMoved = board.pawnsThatHaveMoved.ToList();
            this.rooksThatHaveMoved = board.rooksThatHaveMoved.ToList();
            this.kingThatHaveMoved = board.kingThatHaveMoved.ToList();

            this.moves = board.moves.ToList();


        }

        private List<Move> Moves { get => moves; set => moves = value; }

        public Piece[] getPiece()
        {
            return this.piece;
        }

        public List<MoveableImage> getWhiteCapturedImages()
        {
            return this.whiteCapturedImages;
        }

        public List<MoveableImage> getBlackCapturedImages()
        {
            return this.blackCapturedImages;
        }

        public List<int> getWhiteCaptureValues()
        {
            return this.whiteCapturedPieceValue;
        }

        public List<Move> getValidMoves()
        {
            return this.validMoves;
        }

        public void setValidMoves(List<Move> validMoves)
        {
            this.validMoves = validMoves;
        }
        public List<int> getBlackCaptureValues()
        {
            return this.blackCapturedPieceValue;
        }

        public int getWhiteKingLocation()
        {
            return this.whiteKingLocation;
        }

        public int getBlackKingLocation()
        {
            return this.blackKingLocation;
        }

        public void setPiece(Piece[] piece)
        {
            this.piece = piece;
        }

        public void setEnPassantLocation(int location)
        {
            this.enPassantLocation = location;
        }

        public int getEnPassantLocation()
        {
            return this.enPassantLocation;
        }

        public (int, int) generateBoardStrengths()
        {
            (int blackBoardStrength, int whiteBoardStrength) boardStrengths = (0, 0);
            foreach (Piece piece in this.piece)
            {
                if (piece != null)
                {
                    if (piece.getIsWhite() == true)
                    {
                        boardStrengths.whiteBoardStrength += piece.getStrength();

                    }
                    else
                    {
                        boardStrengths.blackBoardStrength += piece.getStrength();
                    }
                }
            }

            return boardStrengths;
        }
        public void printBoard()
        {

            for (int i = 0; i < (boardSize * boardSize); i++)
            {
                if (this.getPiece()[i] == null)
                {
                    Console.Write('0');
                }
                else
                {
                    Console.Write(this.getPiece()[i].getLetterRepresentation());
                }

                if (i % boardSize == boardSize - 1)
                {
                    Console.Write("\n");
                }
            }
        }


        public int moveGenerationTest(int depth)
        {

            if (depth == 0)
            {
                return 1;
            }


            int numPositions = 0;
            foreach (Move move in this.validMoves)
            {

                Board board = makeMoveOnNewBoard(move);
                numPositions += board.moveGenerationTest(depth - 1);
                move.getPiece().setLocation(move.getStartLocation());

            }


            return numPositions;
        }



        public void setPieceAtLocation(int location, Piece piece)
        {
            if (piece != null)
            {
                piece.setLocation(location);
            }

            this.piece[location] = piece;
        }

        public List<Move> getMoves()
        {
            return this.moves;
        }

        public bool getIsWhiteInCheck()
        {
            return this.whiteInCheck;
        }

        public bool getIsBlackInCheck()
        {
            return this.blackInCheck;
        }

        public List<int> getBlackAttackingMoves()
        {
            return this.blackAttacking;
        }

        public List<int> getWhiteAttackingMoves()
        {
            return this.whiteAttacking;
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

        //Checks if board state after the move would leave you in checks, if it does it is removed
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
                if (validMove.getTag() == "CastleShort")
                {
                    Piece rook = validMove.getSecondaryPiece();
                    boardStateAfterMove[pieceStartLocation + 3] = null;
                    boardStateAfterMove[pieceStartLocation - 1] = rook;
                }

                if (validMove.getTag() == "CastleLong")
                {
                    Piece rook = validMove.getSecondaryPiece();
                    boardStateAfterMove[pieceStartLocation - 4] = null;
                    boardStateAfterMove[pieceStartLocation + 1] = rook;
                }

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



        public Board makeMoveOnNewBoard(Move move)
        {
            //creates a new board with duplicate values 
            Board boardAfterMove = new Board(this);

            string chessNotation = "";
            //resets enpassantLocation
            boardAfterMove.setEnPassantLocation(-50);

            int pieceEndLocation = move.getEndLocation();
            int pieceStartLocation = move.getStartLocation();
            Piece piece = move.getPiece();
            String tag = move.getTag();
            Piece secondaryPiece = null;

            bool moveIsValid = false;

            //check if the move is valid
            foreach (Move validmove in generateValidMoves(this))
            {
                if (validmove.getStartLocation() == move.getStartLocation() & validmove.getEndLocation() == move.getEndLocation() & validmove.getPiece() == move.getPiece())
                {
                    moveIsValid = true;
                    tag = validmove.getTag();
                    //Console.WriteLine(tag);
                    secondaryPiece = validmove.getSecondaryPiece();
                }
            }


            if (moveIsValid == false)
            {
                Console.WriteLine("Invalid Move");
                return null;
            }
            else
            {

                //add initial of piece to notation if it is not a pawn
                if (!(move.getPiece() is Pawn))
                {
                    chessNotation += move.getPiece().getLetterRepresentation();

                }
                int endLocation = move.getEndLocation();
                int endXLocation = (endLocation) % ((boardAfterMove.getBoardSize())); ;
                int endYLocation = boardSize - 1 - (int)(endLocation / boardAfterMove.getBoardSize());

                //if the piece is a capture
                if (boardAfterMove.getPiece()[pieceEndLocation] != null)
                {
                    chessNotation += "x";
                    //removes captured piece from board+
                    if (boardAfterMove.getIsWhiteTurn())
                    {
                        boardAfterMove.blackCapturedImages.Add(boardAfterMove.getPiece()[pieceEndLocation].getImage());
                        boardAfterMove.blackCapturedPieceValue.Add(boardAfterMove.getPiece()[pieceEndLocation].getStrength());
                    }
                    else
                    {
                        boardAfterMove.whiteCapturedImages.Add(boardAfterMove.getPiece()[pieceEndLocation].getImage());
                        boardAfterMove.whiteCapturedPieceValue.Add(boardAfterMove.getPiece()[pieceEndLocation].getStrength());
                    }

                    boardAfterMove.getPiece()[pieceEndLocation] = null;
                    //

                }


                if (piece is ChessB.Pawn)
                {
                    if (!boardAfterMove.pawnsThatHaveMoved.Contains(piece))
                    {
                        boardAfterMove.pawnsThatHaveMoved.Add(piece);
                    }

                    //set enpassant squares
                    if (tag == "enPassant")
                    {
                        chessNotation += GetColumnName(pieceStartLocation % boardSize);
                        chessNotation += "x";
                        if (pieceStartLocation < pieceEndLocation)
                        {
                            Piece capturedPawn = getPiece()[endLocation - boardSize];
                            if (boardAfterMove.getIsWhiteTurn())
                            {
                                boardAfterMove.blackCapturedImages.Add(capturedPawn.getImage());
                                boardAfterMove.blackCapturedPieceValue.Add(capturedPawn.getStrength());

                            }
                            else
                            {
                                boardAfterMove.whiteCapturedImages.Add(capturedPawn.getImage());
                                boardAfterMove.whiteCapturedPieceValue.Add(capturedPawn.getStrength());
                            }
                            //removes the piece under the enpassant
                            boardAfterMove.setPieceAtLocation(endLocation - boardSize, null);



                        }
                        else
                        {
                            Piece capturedPawn = getPiece()[endLocation + boardSize];
                            if (boardAfterMove.getIsWhiteTurn())
                            {
                                boardAfterMove.blackCapturedImages.Add(capturedPawn.getImage());
                                boardAfterMove.blackCapturedPieceValue.Add(capturedPawn.getStrength());
                            }
                            else
                            {
                                boardAfterMove.whiteCapturedImages.Add(capturedPawn.getImage());
                                boardAfterMove.whiteCapturedPieceValue.Add(capturedPawn.getStrength());
                            }
                            //removes the piece above the enpassant
                            boardAfterMove.setPieceAtLocation(endLocation + boardSize, null);


                        }
                    }
                    else
                    {
                        //sets the enpassant square
                        if (move.getStartLocation() - move.getEndLocation() == -(this.getBoardSize() * 2))
                        {
                            boardAfterMove.setEnPassantLocation(move.getStartLocation() + this.getBoardSize());
                        }

                        if (move.getStartLocation() - move.getEndLocation() == (this.getBoardSize() * 2))
                        {
                            boardAfterMove.setEnPassantLocation(move.getStartLocation() - this.getBoardSize());
                        }
                    }

                }
                chessNotation += GetColumnName(endXLocation).ToLower();
                chessNotation += (boardSize - endYLocation).ToString();
            }


            if (piece is ChessB.King)
            {
                if (piece.getIsWhite() == true)
                {
                    boardAfterMove.whiteKingLocation = pieceEndLocation;
                    if (!boardAfterMove.kingThatHaveMoved.Contains(piece))
                    {
                        boardAfterMove.kingThatHaveMoved.Add(piece);
                    }

                }
                else
                {
                    boardAfterMove.blackKingLocation = pieceEndLocation;
                    if (!boardAfterMove.kingThatHaveMoved.Contains(piece))
                    {
                        boardAfterMove.kingThatHaveMoved.Add(piece);
                    }

                }
            }



            if (piece is ChessB.Rook)
            {
                boardAfterMove.rooksThatHaveMoved.Add(piece);
            }

            boardAfterMove.setPieceAtLocation(pieceEndLocation, piece);
            boardAfterMove.setPieceAtLocation(pieceStartLocation, null);


            //if the move is castleing
            if (tag == "CastleShort")
            {

                int kinglocation = move.getStartLocation();
                Piece rook = boardAfterMove.getPiece()[kinglocation + 3];
                //moves castle
                boardAfterMove.setPieceAtLocation(kinglocation + 1, rook);
                //moves king
                boardAfterMove.setPieceAtLocation(kinglocation + 3, null);
                //add kings that have moved to board
                if (!boardAfterMove.kingThatHaveMoved.Contains(move.getPiece()))
                {
                    boardAfterMove.kingThatHaveMoved.Add(move.getPiece());
                }

                chessNotation = "O-O";
            }
            else if (tag == "CastleLong")
            {


                int kinglocation = move.getStartLocation();
                Piece rook = boardAfterMove.getPiece()[kinglocation - 4];
                //add castle
                boardAfterMove.setPieceAtLocation(kinglocation - 1, rook);
                //remove castle
                boardAfterMove.setPieceAtLocation(kinglocation - 4, null);

                if (!boardAfterMove.kingThatHaveMoved.Contains(move.getPiece()))
                {
                    boardAfterMove.kingThatHaveMoved.Add(move.getPiece());
                }

                chessNotation = "O-O-O";
            }
            else if (move.getTag() == "promoteToRook")
            {
                int location = move.getStartLocation();
                //creates a new rook to replace pawn
                Rook promotedPiece = new Rook(move.getPiece().getIsWhite(), move.getEndLocation());
                //stops this piece from being used in castleing
                rooksThatHaveMoved.Add(promotedPiece);
                //replaces promoting pawn with rook
                boardAfterMove.setPieceAtLocation(move.getEndLocation(), promotedPiece);
                //adds promotion tag to end of notation
                chessNotation += "R";
            }
            else if (move.getTag() == "promoteToBishop")
            {

                int location = move.getStartLocation();
                //creates a new bishop to replace pawn
                Bishop promotedPiece = new Bishop(move.getPiece().getIsWhite(), move.getEndLocation());
                //replaces promoting pawn with bishop
                boardAfterMove.setPieceAtLocation(move.getEndLocation(), promotedPiece);
                //adds promotion tag to end of notation
                chessNotation += "B";
            }
            else if (move.getTag() == "promoteToQueen")
            {

                int location = move.getStartLocation();
                //creates a new Queen to replace pawn
                Queen promotedPiece = new Queen(move.getPiece().getIsWhite(), move.getEndLocation());
                //replaces promoting pawn with Queen
                boardAfterMove.setPieceAtLocation(move.getEndLocation(), promotedPiece);
                //adds promotion tag to end of notation
                chessNotation += "Q";

            }
            else if (move.getTag() == "promoteToKnight")
            {
                int location = move.getStartLocation();
                //creates a new Knight to replace pawn
                Knight promotedPiece = new Knight(move.getPiece().getIsWhite(), move.getEndLocation());
                //replaces promoting pawn with Knight
                boardAfterMove.setPieceAtLocation(move.getEndLocation(), promotedPiece);
                //adds promotion tag to end of notation
                chessNotation += "N";
            }



            boardAfterMove.moveNumber++;
            boardAfterMove.setIsWhiteTurn(!boardAfterMove.getIsWhiteTurn());


            bool isEnPassant = false;
            if (tag == "enPassant")
            {
                isEnPassant = true;
            }

            boardAfterMove.setUpNextTurn(boardAfterMove, move, chessNotation, isEnPassant);

            return boardAfterMove;

        }
        public List<Move> generateValidMoves(Board board)
        {

            List<Move> validMovesAfterCheck = new List<Move>();

            //genearates all valid moves for each piece
            for (int i = 0; i < boardSize * boardSize; i++)
            {
                if (piece[i] != null)
                {
                    if (piece[i].getIsWhite() == board.isWhiteTurn)
                    {
                        if (piece[i] is ChessB.King)
                        {
                            validMovesAfterCheck.AddRange(removeMovesThatPutInCheck(piece[i].generateValidMoves(board, board.getPiece(), i, board.getBlackAttackingMoves(), board.getWhiteAttackingMoves())));
                        }
                        else
                        {
                            validMovesAfterCheck.AddRange(removeMovesThatPutInCheck(piece[i].generateValidMoves(board, board.getPiece(), i)));
                        }
                    }
                }

            }

            return validMovesAfterCheck;
        }
        private void setUpNextTurn(Board board, Move move, string chessNotation, bool isEnPassant)
        {

            bool check = false;
            bool staleMate = false;
            //reset enpassant square
            //generate all current players moves
            //generate all opponenets attacking moves, check if we are in check// if in check play check sounds
            //if current player has no valid moves and we are in check then oppenent wins
            //if current player has valid moves and not in check it is stalemate
            List<Move> validMovesAfterCheck = new List<Move>();
            if (this.isWhiteTurn == true)
            {
                board.blackAttacking = board.generateBlackAttackingMoves(board.getPiece());
                if (board.blackAttacking.Contains(board.whiteKingLocation))
                {
                    check = true;
                    board.whiteInCheck = true;

                }
            }
            else
            {
                board.whiteAttacking = board.generateWhiteAttackingMoves(board.getPiece());
                if (board.whiteAttacking.Contains(board.blackKingLocation))
                {
                    board.blackInCheck = true;
                    check = true;
                }
            }

            //generate all valid moves
            validMovesAfterCheck = generateValidMoves(board);
            board.validMoves = validMovesAfterCheck;

            //removes moves that would leave king in check.

            if (validMovesAfterCheck.Count == 0)
            {
                if (board.isWhiteTurn == true)
                {
                    if (board.blackAttacking.Contains(board.whiteKingLocation))
                    {
                        board.blackWins = true;
                        Console.WriteLine("Black Wins");
                    }
                    else
                    {
                        staleMate = true;
                        board.draw = true;
                        Console.WriteLine("Stalemate");
                    }
                }
                else
                {
                    if (board.whiteAttacking.Contains(board.blackKingLocation))
                    {
                        board.whiteWins = true;
                        Console.WriteLine("White Wins");
                    }
                    else
                    {
                        staleMate = true;
                        board.draw = true;
                        Console.WriteLine("Stalemate");
                    }
                }

            }



            if (board.blackWins == true || board.whiteWins == true)
            {
                chessNotation += "#";
            }
            else if (staleMate == true)
            {
                chessNotation += "=";
            }
            else if (check == true)
            {
                chessNotation += "+";
            }

            if (isEnPassant)
            {
                chessNotation += " e.p.";
            }


            move.setChessNotation(chessNotation);
            board.moves.Add(move);

        }





        public List<int> generateWhiteAttackingMoves(Piece[] pieceArray)
        {
            List<Move> whiteAttackingMoves = new List<Move>();

            for (int i = 0; i < boardSize * boardSize; i++)
            {
                if (piece[i] != null)
                {

                    if (piece[i].GetType() != typeof(Pawn) && piece[i].GetType() != typeof(King))
                    {
                        if (piece[i].getIsWhite() == true)
                        {
                            whiteAttackingMoves.AddRange(piece[i].generateValidMoves(this, pieceArray, i));
                        }

                    }
                    else if (piece[i].GetType() == typeof(Pawn))
                    {
                        if (piece[i].getIsWhite() == true)
                        {
                            whiteAttackingMoves.AddRange(piece[i].generateAttackingMoves(this, i));
                        }
                    }
                    else if (piece.GetType() == typeof(King))
                    {
                        if (piece[i].getIsWhite() == true)
                        {
                            whiteAttackingMoves.AddRange(piece[i].generateValidMoves(this, pieceArray, i, this.getBlackAttackingMoves(), this.getWhiteAttackingMoves()));
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

            for (int i = 0; i < boardSize * boardSize; i++)
            {
                if (piece[i] != null)
                {

                    if (piece[i].GetType() != typeof(Pawn) && piece[i].GetType() != typeof(King))
                    {
                        if (piece[i].getIsWhite() == false)
                        {
                            blackAttackingMoves.AddRange(piece[i].generateValidMoves(this, pieceArray, i));
                        }

                    }
                    else if (piece[i].GetType() == typeof(Pawn))
                    {
                        if (piece[i].getIsWhite() == false)
                        {
                            blackAttackingMoves.AddRange(piece[i].generateAttackingMoves(this, i));
                        }

                    }
                    else if (piece[i].GetType() == typeof(King))
                    {
                        if (piece[i].getIsWhite() == false)
                        {
                            blackAttackingMoves.AddRange(piece[i].generateValidMoves(this, pieceArray, i, this.getBlackAttackingMoves(), this.getWhiteAttackingMoves()));

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

            this.blackAttacking = generateBlackAttackingMoves(piece);
            this.whiteAttacking = generateWhiteAttackingMoves(piece);
        }

        private static string GetColumnName(int index)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var value = "";

            if (index >= letters.Length)
                value += letters[index / letters.Length - 1];

            value += letters[index % letters.Length];

            return value;
        }
    }
}
