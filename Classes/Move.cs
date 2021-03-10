using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessB
{
    public struct Move
    {
        private readonly int startLoction;
        private readonly int endLocation;
        public Piece piece;

        public Move(int startLocation, int endLocation, Piece piece)
        {
            this.startLoction = startLocation;
            this.endLocation = endLocation;
            this.piece = piece;
        }

        public int getStartLocation()
        {
            return this.startLoction;
        }

        public int getEndLocation()
        {
            return this.endLocation;
        }

        public Piece getPiece()
        {
            return this.piece;
        }
    }
}
