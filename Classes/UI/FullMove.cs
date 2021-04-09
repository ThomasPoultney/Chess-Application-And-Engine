using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessB
{
    class FullMove
    {
        public string moveNumber { get; set; }
        public string whiteChessNotation { get; set; }
        public string blackChessNotation { get; set; }
        public FullMove(string moveNumber, String whiteChessNotation)
        {
            this.moveNumber = moveNumber;
            this.whiteChessNotation = whiteChessNotation;
        }

        public void setBlackChessNotation(string blackChessNotation)
        {
            this.blackChessNotation = blackChessNotation;
        }


    }
}
