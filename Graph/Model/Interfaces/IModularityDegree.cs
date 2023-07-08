using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Model.Interfaces
{
    internal interface IModularityDegree
    {
        public double[] MetricForAVertex(GraphModel graph);//значение "расстояния" для вершины
        public double DistanceBetweenVertices(AdjencyMatrix adjencyMatrix, int i, int j, int n);// расчет расстояния между вершинами

    }
}
