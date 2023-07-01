using System;

namespace Graph.Model
{
    internal class AdjencyMatrix
    {
        public decimal[,] matrix;

        AdjencyMatrix(GraphModel graph) 
        {
            int lenght = graph.nodes.Count;
            int k = 0;

            int nRows = (int)Math.Sqrt(lenght);
            int nColums = nRows;


            matrix = new decimal[nRows, nColums];

            foreach (var node in graph.nodes) 
            { 
                foreach (var con in node.connections)
                {
                    matrix[k, con] = 1 / node.degree;
                }
                k++;
            }

        }

        public AdjencyMatrix MultiplyMatrix(AdjencyMatrix matrix, int t)
        {
            AdjencyMatrix newMatrix = matrix;


            for (int i = 0; i < t - 1; i++)
            {


            }

            return newMatrix;
        }

    }
}
