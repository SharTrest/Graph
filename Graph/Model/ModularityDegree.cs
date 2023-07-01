using Graph.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Model
{
    internal class ModularityDegree : IModularityDegree
    {
        public AdjencyMatrix adjencyMatrix;
        public Graph graph;

        public decimal MetricForAVertex(AdjencyMatrix matrix, int[] degrees)
        {
            throw new NotImplementedException();
        }
    }
}
