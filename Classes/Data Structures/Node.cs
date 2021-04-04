using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessB
{
    class Node
    {
        private Board board;
        private Node parent;
        //Number of Children
        private int degree;
        private List<Node> children;

        public Node(Board board, Node parent)
        {
            this.board = board;
            this.parent = parent;
            this.children = new List<Node>();
            this.degree = 0;
        }
    }
}
