using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessB
{
    public struct Move
    {
        private readonly int startLocation;
        private readonly int endLocation;
        private Piece piece;
        private string tag;
        //if another piece is involved e.g. castleing
        private Piece secondaryPiece;
        private string chessNotation;

        public Move(int startLocation, int endLocation, Piece piece, string tag = "None", Piece secondaryPiece = null)
        {
            this.startLocation = startLocation;
            this.endLocation = endLocation;
            this.piece = piece;
            this.tag = tag;
            this.secondaryPiece = secondaryPiece;
            this.chessNotation = "";
        }

        public Move(int startLocation, int endLocation, Piece piece, string tag = "None")
        {
            this.startLocation = startLocation;
            this.endLocation = endLocation;
            this.piece = piece;
            this.tag = tag;
            this.secondaryPiece = null;
            this.chessNotation = "";
        }

        public Move(int startLocation, int endLocation, Piece piece)
        {
            this.startLocation = startLocation;
            this.endLocation = endLocation;
            this.piece = piece;
            this.tag = "None";
            this.secondaryPiece = null;
            this.chessNotation = "";
        }

        public int getStartLocation()
        {
            return this.startLocation;
        }

        public void setChessNotation(string chessNotation)
        {
            this.chessNotation = chessNotation;
        }

        public int getEndLocation()
        {
            return this.endLocation;
        }

        public Piece getPiece()
        {
            return this.piece;
        }

        public string getTag()
        {
            return this.tag;
        }

        public void setTag(String tag)
        {
            this.tag = tag;
        }

        public Piece getSecondaryPiece()
        {
            return this.secondaryPiece;
        }
    }


}
