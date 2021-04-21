using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessB
{
    public static class Game
    {
        public static Board activeBoard;
        public static List<Move> validMoves;
        public static bool whitesTurn = true;
        private static Player whitePlayer;
        private static Player blackPlayer;

        public static DateTime blackStartTime;
        public static TimeSpan blackTotalTime = TimeSpan.FromMinutes(10);
        public static TimeSpan blackTimeLeft = blackTotalTime;

        public static DateTime whiteStartTime;
        public static TimeSpan whiteTotalTime = TimeSpan.FromMinutes(0.1);
        public static TimeSpan whiteTimeLeft = whiteTotalTime;


        public static void startBlackTimer()
        {
            Game.blackStartTime = DateTime.Now;
            TimeSpan timeElapsed = (Game.whiteStartTime - DateTime.Now);
            Game.whiteTimeLeft += timeElapsed;
        }

        public static void startWhiteTimer()
        {
            Game.whiteStartTime = DateTime.Now;
            TimeSpan timeElapsed = (Game.blackStartTime - DateTime.Now);
            Game.blackTimeLeft += timeElapsed;
        }
    }
}
