using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChessB
{
    public class Piece
    {
        protected bool isWhite;
        //The relative point value of each piece e.g. 5 for rook
        protected int strength;
        protected int location;
        protected string imagePath;
        private MoveableImage image;
        protected char letterRepresentation;


        public Piece(bool isWhite, int location)
        {
            this.isWhite = isWhite;
            this.location = location;

        }

        public Piece(bool isWhite, int location, MoveableImage image)
        {
            this.isWhite = isWhite;
            this.location = location;
            this.image = image;

        }

        public void setImage(MoveableImage image)
        {
            this.image = image;
        }

        public MoveableImage getImage()
        {
            return this.image;
        }

        public bool getIsWhite()
        {
            return this.isWhite;
        }

        public int getLocation()
        {
            return this.location;
        }

        public int getStrength()
        {
            return this.strength;
        }



        public string getImagePath()
        {
            return this.imagePath;
        }

        public virtual List<Move> generateValidMoves(Board board, Piece[] piece, int location)
        {
            return new List<Move>();
        }

        public virtual char getLetterRepresentation()
        {
            return this.letterRepresentation;
        }

        public virtual List<Move> generateValidMoves(Board board, Piece[] piece, int location, List<int> blackAttackingMoves, List<int> whiteAttackingMoves)
        {
            return new List<Move>();
        }


        public virtual void setCanMoveTwice(bool canMoveTwice)
        {

        }

        public virtual List<Move> generateAttackingMoves(Board board, int location)
        {
            return new List<Move>();
        }

        public virtual bool getHasMoved()
        {
            return true;
        }
        public virtual void setHasMoved(bool hasMoved)
        {

        }

        public void setLocation(int location)
        {
            this.location = location;
        }

    }
}
