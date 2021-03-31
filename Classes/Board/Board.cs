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

        private bool blackCanCastle = true;
        private bool whiteCanCastle = true;

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
        public static int boardSize = 8;


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
            this.whiteCapturedImages = board.whiteCapturedImages;
            this.blackCapturedImages = board.blackCapturedImages;
            this.blackCapturedPieceValue = board.blackCapturedPieceValue;
            this.whiteCapturedPieceValue = board.whiteCapturedPieceValue;
            this.moves = board.moves;


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
                    boardStateAfterMove[rook.getLocation()] = null;
                    boardStateAfterMove[validMove.getPiece().getLocation() - 1] = rook;
                }

                if (validMove.getTag() == "CastleLong")
                {
                    Piece rook = validMove.getSecondaryPiece();
                    boardStateAfterMove[rook.getLocation()] = null;
                    boardStateAfterMove[validMove.getPiece().getLocation() + 1] = rook;
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

        public bool makeMoveOnThisBoard(Move move)
        {
            string chessNotation = "";
            setEnPassantLocation(-50);
            Ui.removeArrows();
            Ui.removeMarkedTiles();
            int pieceEndLocation = move.getEndLocation();
            int pieceStartLocation = move.getStartLocation();
            Piece piece = move.getPiece();
            String tag = move.getTag();
            Piece secondaryPiece = null;
            Ui.removeHighlightTile();

            bool moveIsValid = false;
            foreach (Move validmove in Game.validMoves)
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
                return false;
            }
            else
            {
                if (!(move.getPiece() is Pawn))
                {
                    chessNotation += move.getPiece().getLetterRepresentation();

                }
                int endLocation = move.getEndLocation();
                int endXLocation = (endLocation) % ((this.getBoardSize())); ;
                int endYLocation = boardSize - 1 - (int)(endLocation / this.getBoardSize());
                Canvas.SetTop(move.getPiece().getImage(), endYLocation * Ui.squareSize);




                Canvas.SetLeft(move.getPiece().getImage(), endXLocation * Ui.squareSize);


                if (this.getPiece()[pieceEndLocation] != null)
                {
                    Ui.canvas.Children.Remove(Game.activeBoard.getPiece()[pieceEndLocation].getImage());
                    chessNotation += "x";

                    if (this.getPiece()[pieceEndLocation].getIsWhite())
                    {
                        MoveableImage image = Game.activeBoard.getPiece()[pieceEndLocation].getImage();
                        Ui.addBlackCaptureImage(image, this.getPiece()[pieceEndLocation].getStrength());
                    }
                    else
                    {
                        MoveableImage image = Game.activeBoard.getPiece()[pieceEndLocation].getImage();
                        Ui.addWhiteCaptureImage(image, this.getPiece()[pieceEndLocation].getStrength());
                    }

                    this.getPiece()[pieceEndLocation] = null;

                }


                if (piece is ChessB.Pawn)
                {
                    piece.setCanMoveTwice(false);

                    //set enpassant squares
                    if (tag == "enPassant")
                    {
                        chessNotation += GetColumnName(pieceStartLocation % boardSize);
                        chessNotation += "x";
                        Console.WriteLine("EnPassant");
                        if (pieceStartLocation < pieceEndLocation)
                        {
                            Piece capturedPawn = getPiece()[endLocation - boardSize];
                            Ui.canvas.Children.Remove(capturedPawn.getImage());
                            if (capturedPawn.getIsWhite() == true)
                            {
                                Ui.addBlackCaptureImage(capturedPawn.getImage(), capturedPawn.getStrength());

                            }
                            else
                            {
                                Ui.addWhiteCaptureImage(capturedPawn.getImage(), capturedPawn.getStrength());

                            }

                            setPieceAtLocation(endLocation - boardSize, null);

                        }
                        else
                        {
                            Piece capturedPawn = getPiece()[endLocation + boardSize];
                            Ui.canvas.Children.Remove(capturedPawn.getImage());
                            if (capturedPawn.getIsWhite() == true)
                            {
                                Ui.addBlackCaptureImage(capturedPawn.getImage(), capturedPawn.getStrength());
                            }
                            else
                            {
                                Ui.addWhiteCaptureImage(capturedPawn.getImage(), capturedPawn.getStrength());
                            }

                            setPieceAtLocation(endLocation + boardSize, null);
                        }
                    }
                    else
                    {
                        if (move.getStartLocation() - move.getEndLocation() == -(this.getBoardSize() * 2))
                        {
                            setEnPassantLocation(move.getStartLocation() + this.getBoardSize());
                        }

                        if (move.getStartLocation() - move.getEndLocation() == (this.getBoardSize() * 2))
                        {
                            setEnPassantLocation(move.getStartLocation() - this.getBoardSize());
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
                    this.whiteKingLocation = pieceEndLocation;
                    piece.setHasMoved(true);
                    this.whiteCanCastle = false;
                }
                else
                {
                    this.blackKingLocation = pieceEndLocation;
                    piece.setHasMoved(true);
                    this.blackCanCastle = false;
                }
            }



            if (piece is ChessB.Rook)
            {
                piece.setHasMoved(true);
            }

            this.setPieceAtLocation(pieceEndLocation, piece);
            this.setPieceAtLocation(pieceStartLocation, null);



            if (tag == "CastleShort")
            {
                Piece rook = move.getSecondaryPiece();
                int location = move.getPiece().getLocation() - 1;
                int xLocationForRook = (location) % ((this.getBoardSize())); ;
                int yLocationForRook = boardSize - 1 - (int)(location / this.getBoardSize()); ;
                Canvas.SetTop(secondaryPiece.getImage(), yLocationForRook * Ui.squareSize);
                Canvas.SetLeft(secondaryPiece.getImage(), xLocationForRook * Ui.squareSize);
                this.setPieceAtLocation(secondaryPiece.getLocation(), null);
                this.setPieceAtLocation(move.getPiece().getLocation() - 1, secondaryPiece);
                if (move.getPiece().getIsWhite() == true)
                {
                    this.whiteCanCastle = false;
                }
                else
                {
                    this.blackCanCastle = false;
                }
                chessNotation = "O-O";
            }

            if (tag == "CastleLong")
            {
                Piece rook = move.getSecondaryPiece();
                int location = move.getPiece().getLocation() + 1;
                int xLocationForRook = (location) % ((this.getBoardSize())); ;
                int yLocationForRook = boardSize - 1 - (int)(location / this.getBoardSize()); ;
                Canvas.SetTop(secondaryPiece.getImage(), yLocationForRook * Ui.squareSize);
                Canvas.SetLeft(secondaryPiece.getImage(), xLocationForRook * Ui.squareSize);
                this.setPieceAtLocation(secondaryPiece.getLocation(), null);
                this.setPieceAtLocation(move.getPiece().getLocation() + 1, secondaryPiece);

                if (move.getPiece().getIsWhite() == true)
                {
                    this.whiteCanCastle = false;
                }
                else
                {
                    this.blackCanCastle = false;
                }

                chessNotation = "O-O-O";
            }

            if (move.getTag() == "promoteToRook")
            {
                Ui.canvas.Children.Remove(move.getPiece().getImage());
                int location = move.getPiece().getLocation();
                int xLocation = (location) % ((this.getBoardSize())); ;
                int yLocation = boardSize - 1 - (int)(location / this.getBoardSize());
                Rook promotedPiece = new Rook(move.getPiece().getIsWhite(), move.getEndLocation());
                MoveableImage promotedImage = new MoveableImage();
                promotedPiece.setImage(promotedImage);
                String promotedImageURL = promotedPiece.getImagePath();

                ImageSource promotedPieceSource = new BitmapImage(new Uri(promotedImageURL));
                promotedImage.Source = promotedPieceSource;
                promotedImage.Width = Ui.squareSize;
                promotedImage.Height = Ui.squareSize;
                setPieceAtLocation(move.getEndLocation(), promotedPiece);

                Canvas.SetTop(promotedImage, yLocation * Ui.squareSize);
                Canvas.SetLeft(promotedImage, xLocation * Ui.squareSize);
                Canvas.SetZIndex(promotedImage, 1100);
                if (move.getPiece().getIsWhite() == true)
                {
                    Ui.addWhiteScore(4);
                }
                else
                {
                    Ui.addBlackScore(4);
                }
                Ui.canvas.Children.Add(promotedImage);
                chessNotation += "R";
            }
            else if (move.getTag() == "promoteToBishop")
            {
                Ui.canvas.Children.Remove(move.getPiece().getImage());
                int location = move.getPiece().getLocation();
                int xLocation = (location) % ((this.getBoardSize())); ;
                int yLocation = boardSize - 1 - (int)(location / this.getBoardSize());
                Bishop promotedPiece = new Bishop(move.getPiece().getIsWhite(), move.getEndLocation());

                MoveableImage promotedImage = new MoveableImage();
                promotedPiece.setImage(promotedImage);
                String promotedImageURL = promotedPiece.getImagePath();

                ImageSource promotedPieceSource = new BitmapImage(new Uri(promotedImageURL));
                promotedImage.Source = promotedPieceSource;
                promotedImage.Width = Ui.squareSize;
                promotedImage.Height = Ui.squareSize;
                setPieceAtLocation(move.getEndLocation(), promotedPiece);

                Canvas.SetTop(promotedImage, yLocation * Ui.squareSize);
                Canvas.SetLeft(promotedImage, xLocation * Ui.squareSize);
                Canvas.SetZIndex(promotedImage, 1100);
                Ui.canvas.Children.Add(promotedImage);

                if (move.getPiece().getIsWhite() == true)
                {
                    Ui.addWhiteScore(2);
                }
                else
                {
                    Ui.addBlackScore(2);
                }

                chessNotation += "B";
            }
            else if (move.getTag() == "promoteToQueen")
            {
                Ui.canvas.Children.Remove(move.getPiece().getImage());
                int location = move.getPiece().getLocation();
                int xLocation = (location) % ((this.getBoardSize())); ;
                int yLocation = boardSize - 1 - (int)(location / this.getBoardSize());
                Queen promotedPiece = new Queen(move.getPiece().getIsWhite(), move.getEndLocation());
                MoveableImage promotedImage = new MoveableImage();
                promotedPiece.setImage(promotedImage);
                String promotedImageURL = promotedPiece.getImagePath();


                ImageSource promotedPieceSource = new BitmapImage(new Uri(promotedImageURL));
                promotedImage.Source = promotedPieceSource;
                promotedImage.Width = Ui.squareSize;
                promotedImage.Height = Ui.squareSize;
                setPieceAtLocation(move.getEndLocation(), promotedPiece);

                Canvas.SetTop(promotedImage, yLocation * Ui.squareSize);
                Canvas.SetLeft(promotedImage, xLocation * Ui.squareSize);
                Canvas.SetZIndex(promotedImage, 1100);

                Ui.canvas.Children.Add(promotedImage);
                if (move.getPiece().getIsWhite() == true)
                {
                    Ui.addWhiteScore(7);
                }
                else
                {
                    Ui.addBlackScore(7);
                }

                chessNotation += "Q";

            }
            else if (move.getTag() == "promoteToKnight")
            {
                Ui.canvas.Children.Remove(move.getPiece().getImage());
                int location = move.getPiece().getLocation();
                int xLocation = (location) % ((this.getBoardSize())); ;
                int yLocation = boardSize - 1 - (int)(location / this.getBoardSize());
                Knight promotedPiece = new Knight(move.getPiece().getIsWhite(), move.getEndLocation());
                MoveableImage promotedImage = new MoveableImage();
                promotedPiece.setImage(promotedImage);
                String promotedImageURL = promotedPiece.getImagePath();

                ImageSource promotedPieceSource = new BitmapImage(new Uri(promotedImageURL));
                promotedImage.Source = promotedPieceSource;
                promotedImage.Width = Ui.squareSize;
                promotedImage.Height = Ui.squareSize;
                setPieceAtLocation(move.getEndLocation(), promotedPiece);

                Canvas.SetTop(promotedImage, yLocation * Ui.squareSize);
                Canvas.SetLeft(promotedImage, xLocation * Ui.squareSize);
                Canvas.SetZIndex(promotedImage, 1100);
                Ui.canvas.Children.Add(promotedImage);

                if (move.getPiece().getIsWhite() == true)
                {
                    Ui.addWhiteScore(2);
                }
                else
                {
                    Ui.addBlackScore(2);
                }

                chessNotation += "N";
            }



            this.moveNumber++;
            int moveCount = moveNumber;
            Console.WriteLine(moveNumber);
            this.setIsWhiteTurn(!this.getIsWhiteTurn());
            this.moves.Add(move);
            Ui.drawHighlightTile(pieceStartLocation);
            Ui.drawHighlightTile(pieceEndLocation);

            bool isEnPassant = false;
            if (tag == "enPassant")
            {
                isEnPassant = true;
            }

            this.setUpNextTurn(this, move, chessNotation, isEnPassant);

            return true;

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
            foreach (Move validmove in Game.validMoves)
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
                        blackCapturedImages.Add(boardAfterMove.getPiece()[pieceEndLocation].getImage());
                        blackCapturedPieceValue.Add(boardAfterMove.getPiece()[pieceEndLocation].getStrength());
                    }
                    else
                    {
                        whiteCapturedImages.Add(boardAfterMove.getPiece()[pieceEndLocation].getImage());
                        whiteCapturedPieceValue.Add(boardAfterMove.getPiece()[pieceEndLocation].getStrength());
                    }

                    boardAfterMove.getPiece()[pieceEndLocation] = null;
                    //

                }


                if (piece is ChessB.Pawn)
                {
                    piece.setCanMoveTwice(false);

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
                                blackCapturedImages.Add(capturedPawn.getImage());
                                blackCapturedPieceValue.Add(capturedPawn.getStrength());

                            }
                            else
                            {
                                whiteCapturedImages.Add(capturedPawn.getImage());
                                whiteCapturedPieceValue.Add(capturedPawn.getStrength());
                            }
                            //removes the piece under the enpassant
                            boardAfterMove.setPieceAtLocation(endLocation - boardSize, null);



                        }
                        else
                        {
                            Piece capturedPawn = getPiece()[endLocation + boardSize];
                            if (boardAfterMove.getIsWhiteTurn())
                            {
                                blackCapturedImages.Add(capturedPawn.getImage());
                                blackCapturedPieceValue.Add(capturedPawn.getStrength());
                            }
                            else
                            {
                                whiteCapturedImages.Add(capturedPawn.getImage());
                                whiteCapturedPieceValue.Add(capturedPawn.getStrength());
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
                    piece.setHasMoved(true);
                    boardAfterMove.whiteCanCastle = false;
                }
                else
                {
                    boardAfterMove.blackKingLocation = pieceEndLocation;
                    piece.setHasMoved(true);
                    boardAfterMove.blackCanCastle = false;
                }
            }



            if (piece is ChessB.Rook)
            {
                piece.setHasMoved(true);
            }

            boardAfterMove.setPieceAtLocation(pieceEndLocation, piece);
            boardAfterMove.setPieceAtLocation(pieceStartLocation, null);


            //if the move is castleing
            if (tag == "CastleShort")
            {
                Piece rook = move.getSecondaryPiece();
                int location = move.getPiece().getLocation() - 1;

                //moves castle
                boardAfterMove.setPieceAtLocation(secondaryPiece.getLocation(), null);
                //moves king
                boardAfterMove.setPieceAtLocation(move.getPiece().getLocation() - 1, secondaryPiece);

                if (move.getPiece().getIsWhite() == true)
                {
                    //remove right for white to castle
                    boardAfterMove.whiteCanCastle = false;
                }
                else
                {
                    //remove right for white to castle
                    boardAfterMove.blackCanCastle = false;
                }
                chessNotation = "O-O";
            }
            else if (tag == "CastleLong")
            {
                Piece rook = move.getSecondaryPiece();
                int location = move.getPiece().getLocation() + 1;
                //moves castle
                boardAfterMove.setPieceAtLocation(secondaryPiece.getLocation(), null);
                //moves the king
                boardAfterMove.setPieceAtLocation(move.getPiece().getLocation() + 1, secondaryPiece);

                if (move.getPiece().getIsWhite() == true)
                {
                    //remove right for white to castle
                    boardAfterMove.whiteCanCastle = false;
                }
                else
                {
                    //remove right for black to castle
                    boardAfterMove.blackCanCastle = false;
                }

                chessNotation = "O-O-O";
            }
            else if (move.getTag() == "promoteToRook")
            {
                int location = move.getPiece().getLocation();
                //creates a new rook to replace pawn
                Rook promotedPiece = new Rook(move.getPiece().getIsWhite(), move.getEndLocation());
                //stops this piece from being used in castleing
                promotedPiece.setHasMoved(true);
                //replaces promoting pawn with rook
                boardAfterMove.setPieceAtLocation(move.getEndLocation(), promotedPiece);
                //adds promotion tag to end of notation
                chessNotation += "R";
            }
            else if (move.getTag() == "promoteToBishop")
            {

                int location = move.getPiece().getLocation();
                //creates a new bishop to replace pawn
                Bishop promotedPiece = new Bishop(move.getPiece().getIsWhite(), move.getEndLocation());
                //replaces promoting pawn with bishop
                boardAfterMove.setPieceAtLocation(move.getEndLocation(), promotedPiece);
                //adds promotion tag to end of notation
                chessNotation += "B";
            }
            else if (move.getTag() == "promoteToQueen")
            {

                int location = move.getPiece().getLocation();
                //creates a new Queen to replace pawn
                Queen promotedPiece = new Queen(move.getPiece().getIsWhite(), move.getEndLocation());
                //replaces promoting pawn with Queen
                boardAfterMove.setPieceAtLocation(move.getEndLocation(), promotedPiece);
                //adds promotion tag to end of notation
                chessNotation += "Q";

            }
            else if (move.getTag() == "promoteToKnight")
            {
                int location = move.getPiece().getLocation();
                //creates a new Knight to replace pawn
                Knight promotedPiece = new Knight(move.getPiece().getIsWhite(), move.getEndLocation());
                //replaces promoting pawn with Knight
                boardAfterMove.setPieceAtLocation(move.getEndLocation(), promotedPiece);
                //adds promotion tag to end of notation
                chessNotation += "N";
            }



            boardAfterMove.moveNumber++;
            boardAfterMove.setIsWhiteTurn(!boardAfterMove.getIsWhiteTurn());
            boardAfterMove.moves.Add(move);

            bool isEnPassant = false;
            if (tag == "enPassant")
            {
                isEnPassant = true;
            }

            boardAfterMove.setUpNextTurn(boardAfterMove, move, chessNotation, isEnPassant);

            return boardAfterMove;

        }
        private List<Move> generateValidMoves(Board board)
        {

            List<Move> validMovesAfterCheck = new List<Move>();

            //genearates all valid moves for each piece
            foreach (Piece piece in board.getPiece())
            {
                if (piece != null)
                {
                    if (piece.getIsWhite() == board.isWhiteTurn)
                    {
                        if (piece is ChessB.King)
                        {
                            validMovesAfterCheck.AddRange(removeMovesThatPutInCheck(piece.generateValidMoves(board, board.getPiece(), board.getBlackAttackingMoves(), board.getWhiteAttackingMoves())));
                        }
                        else
                        {
                            validMovesAfterCheck.AddRange(removeMovesThatPutInCheck(piece.generateValidMoves(board, board.getPiece())));
                        }
                    }
                }

            }

            return validMovesAfterCheck;
        }
        private void setUpNextTurn(Board board, Move move, string chessNotation, bool isEnPassant)
        {

            bool blackWins = false;
            bool whiteWins = false;
            bool draw = false;
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
                blackAttacking = board.generateBlackAttackingMoves(board.getPiece());
                if (blackAttacking.Contains(board.whiteKingLocation))
                {
                    check = true;
                    board.whiteInCheck = true;

                }
            }
            else
            {
                whiteAttacking = board.generateWhiteAttackingMoves(board.getPiece());
                if (whiteAttacking.Contains(board.blackKingLocation))
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
                        blackWins = true;
                        Console.WriteLine("Black Wins");
                    }
                    else
                    {
                        staleMate = true;
                        draw = true;
                        Console.WriteLine("Stalemate");
                    }
                }
                else
                {
                    if (board.whiteAttacking.Contains(board.blackKingLocation))
                    {
                        whiteWins = true;
                        Console.WriteLine("White Wins");
                    }
                    else
                    {
                        staleMate = true;
                        draw = true;
                        Console.WriteLine("Stalemate");
                    }
                }

            }



            if (blackWins == true || whiteWins == true)
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
            Ui.moveListBox.Items.Add(moveNumber + "\t" + chessNotation);

            if (draw == true)
            {
                Ui.moveListBox.Items.Add("1/2 - 1/2");
                return;
            }
            else if (whiteWins == true)
            {
                Ui.moveListBox.Items.Add("1 - 0");
                return;
            }
            else if (blackWins == true)
            {
                Ui.moveListBox.Items.Add("0 - 1");
                return;
            }
        }



        public void makeRandomMove()
        {
            whiteAttacking = generateWhiteAttackingMoves(this.getPiece());
            blackAttacking = generateBlackAttackingMoves(this.getPiece());
            List<Move> validMovesAfterCheck = new List<Move>();
            foreach (Piece piece in this.getPiece())
            {
                if (piece != null)
                {
                    if (piece.getIsWhite() == this.isWhiteTurn)
                    {
                        if (piece is ChessB.King)
                        {
                            validMovesAfterCheck.AddRange(removeMovesThatPutInCheck(piece.generateValidMoves(this, this.getPiece(), this.getBlackAttackingMoves(), this.getWhiteAttackingMoves())));
                        }
                        else
                        {
                            validMovesAfterCheck.AddRange(removeMovesThatPutInCheck(piece.generateValidMoves(this, this.getPiece())));
                        }
                    }
                }
            }
            var random = new Random();
            int randomIndex = random.Next(validMovesAfterCheck.Count - 1);
            Game.validMoves = validMovesAfterCheck;

            makeMoveOnNewBoard(validMovesAfterCheck[randomIndex]);
        }

        public List<int> generateWhiteAttackingMoves(Piece[] pieceArray)
        {
            List<Move> whiteAttackingMoves = new List<Move>();

            foreach (Piece piece in pieceArray)
            {
                if (piece != null)
                {

                    if (piece.GetType() != typeof(Pawn) && piece.GetType() != typeof(King))
                    {
                        if (piece.getIsWhite() == true)
                        {
                            whiteAttackingMoves.AddRange(piece.generateValidMoves(this, pieceArray));
                        }

                    }
                    else if (piece.GetType() == typeof(Pawn))
                    {
                        if (piece.getIsWhite() == true)
                        {
                            whiteAttackingMoves.AddRange(piece.generateAttackingMoves(this));
                        }
                    }
                    else if (piece.GetType() == typeof(King))
                    {
                        if (piece.getIsWhite() == true)
                        {
                            whiteAttackingMoves.AddRange(piece.generateValidMoves(this, pieceArray, this.getBlackAttackingMoves(), this.getWhiteAttackingMoves()));
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

                    if (piece.GetType() != typeof(Pawn) && piece.GetType() != typeof(King))
                    {
                        if (piece.getIsWhite() == false)
                        {
                            blackAttackingMoves.AddRange(piece.generateValidMoves(this, pieceArray));
                        }

                    }
                    else if (piece.GetType() == typeof(Pawn))
                    {
                        if (piece.getIsWhite() == false)
                        {
                            blackAttackingMoves.AddRange(piece.generateAttackingMoves(this));
                        }

                    }
                    else if (piece.GetType() == typeof(King))
                    {
                        if (piece.getIsWhite() == false)
                        {
                            blackAttackingMoves.AddRange(piece.generateValidMoves(this, pieceArray, this.getBlackAttackingMoves(), this.getWhiteAttackingMoves()));

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
