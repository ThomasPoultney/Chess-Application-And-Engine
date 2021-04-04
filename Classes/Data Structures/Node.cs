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
        private List<Node> children = new List<Node>();
    }
}
