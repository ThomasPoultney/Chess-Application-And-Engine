using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessB
{
    public enum MoveTag
    {
        none,
        enPassant,
        promoteToRook,
        promoteToQueen,
        promoteToKnight,
        promoteToBishop,
        castleShort,
        castleLong,
    }
    public struct Move
    {
        private readonly int startLocation;
        private readonly int endLocation;
        private Piece piece;
        private MoveTag tag;
        //if another piece is involved e.g. castleing
        private Piece secondaryPiece;
        public string chessNotation { get; set; }
        public int moveNumber { get; set; }

        public Move(int startLocation, int endLocation, Piece piece, MoveTag tag = MoveTag.none, Piece secondaryPiece = null)
        {
            this.startLocation = startLocation;
            this.endLocation = endLocation;
            this.piece = piece;
            this.tag = tag;
            this.secondaryPiece = secondaryPiece;
            this.chessNotation = "";
            this.moveNumber = 0;
        }

        public Move(int startLocation, int endLocation, Piece piece, MoveTag tag = MoveTag.none)
        {
            this.startLocation = startLocation;
            this.endLocation = endLocation;
            this.piece = piece;
            this.tag = tag;
            this.secondaryPiece = null;
            this.chessNotation = "";
            this.moveNumber = 0;

        }

        public Move(int startLocation, int endLocation, Piece piece)
        {
            this.startLocation = startLocation;
            this.endLocation = endLocation;
            this.piece = piece;
            this.tag = MoveTag.none;
            this.secondaryPiece = null;
            this.chessNotation = "";
            this.moveNumber = 0;

        }

        public int getStartLocation()
        {
            return this.startLocation;
        }

        public void setChessNotation(string chessNotation)
        {
            this.chessNotation = chessNotation;
        }

        public string getChessNotation()
        {
            return this.chessNotation;
        }

        public int getEndLocation()
        {
            return this.endLocation;
        }

        public Piece getPiece()
        {
            return this.piece;
        }

        public MoveTag getTag()
        {
            return this.tag;
        }

        public void setTag(MoveTag tag)
        {
            this.tag = tag;
        }

        public Piece getSecondaryPiece()
        {
            return this.secondaryPiece;
        }
    }


}
