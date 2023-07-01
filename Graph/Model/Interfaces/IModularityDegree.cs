using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Model.Interfaces
{
    internal interface IModularityDegree
    {

        public AdjencyMatrix MultiplyMatrix(AdjencyMatrix matrix, int t);
        public decimal MetricForAVertex(AdjencyMatrix matrix, int[] degrees);
        

    }
}
