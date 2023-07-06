using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Model.Interfaces
{
    internal interface IModularityDegree
    {
        public decimal MetricForAVertex(AdjencyMatrix matrix);//значение "расстояния" для вершины


    }
}
