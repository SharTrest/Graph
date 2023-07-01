using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Model
{

    public class Graph
    {
        public List<Nodes> nodes;
    }

    public class Nodes
    {
        public List<int> connections;
        public int degree;
    }       
}
