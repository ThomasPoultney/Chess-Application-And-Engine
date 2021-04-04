using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessB
{
    class MoveTree
    {
        private Node rootNode;
        private int numNodes;

        public MoveTree(Node rootNode)
        {
            this.rootNode = rootNode;
        }
    }
}
