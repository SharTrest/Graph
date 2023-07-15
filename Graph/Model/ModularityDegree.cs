using Graph.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Model
{
    public class ModularityDegree : IModularityDegree
    {

        public double[] MetricForAVertex(GraphModel graph)
        {
            Random random = new Random();
            AdjencyMatrix adjencyMatrix = new AdjencyMatrix(graph);


            int t = random.Next(101) > 50 ? 2 : 5;
            t = 2; 
            double r = 0;
            double[] modularity = new double[graph.vertexCount];

            adjencyMatrix.MultiplyMatrix(t);

            foreach (var node in graph.nodes)
            {
                r = 0;
                foreach (var con in node.connections)
                {
                    r += DistanceBetweenVertices(adjencyMatrix, node.vertexId - 1, con - 1, graph.vertexCount);
                }
                modularity[node.vertexId - 1] = r;
            }

            //for (int i = 0; i < graph.vertexCount; i++)
            //{
            //    for (int j = 0; j < graph.vertexCount; j++)
            //        if (i != j)
            //            r += DistanceBetweenVertices(adjencyMatrix, i, j, graph.vertexCount);
            //    modularity[i] = r;
            //    r = 0;
            //}

            return modularity;
        }

        public double DistanceBetweenVertices(AdjencyMatrix adjencyMatrix, int i, int j, int n)
        {
            var matrix = adjencyMatrix.matrix;
            var degree = adjencyMatrix.degree;
            double r = 0;
            

            for (int k = 0; k < n; k++)
            {
                var d = matrix[i, k] - matrix[j, k];
                r +=  d * d / degree[k];
            }

            return Math.Sqrt(r);
        }
    }
}
