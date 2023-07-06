using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Model
{

    public class GraphModel
    {
        public List<Nodes> nodes;
        public int vertexCount;
    }

    public class Nodes
    {
        public int vertexId;
        public List<int> connections;
        public int degree;
    }       
}
