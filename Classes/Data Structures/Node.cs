using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessB
{
    public class Node
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

        public List<Node> getChilden()
        {
            return this.children;
        }

        public Board getBoard()
        {
            return this.board;
        }
        public void addChild(Node node)
        {
            this.children.Add(node);
        }

        public void removeChild(Node node)
        {
            this.children.Remove(node);
        }
    }
}
