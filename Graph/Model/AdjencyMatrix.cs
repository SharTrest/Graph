﻿using System;

namespace Graph.Model
{
    public class AdjencyMatrix
    {
        public double[,] matrix;
        public int[] degree;

        public AdjencyMatrix(GraphModel graph) 
        {
            int lenght = graph.nodes.Count;
            int k = 0;

            int nRows = lenght;
            int nColums = lenght;


            matrix = new double[nRows, nColums];
            degree = new int[graph.vertexCount];

            foreach (var node in graph.nodes) 
            { 
                foreach (var con in node.connections)
                {
                    matrix[node.vertexId - 1, con-1] = 1.0 / node.degree;
                }
                degree[node.vertexId - 1] = node.degree; 
            }

        }

        public void MultiplyMatrix(int t)
        {
            t = 2;
            var newMatrix = matrix;
            var result = new double[newMatrix.GetLength(0), newMatrix.GetLength(1)];
            for (int s = 0; s < t-1; s++)
            { 
                for (int i = 0; i < newMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < newMatrix.GetLength(1); j++)
                    {
                        for (int k = 0; k < newMatrix.GetLength(1); k++)
                        {
                            var r = newMatrix[i, k] * matrix[k, j];
                            result[i, j] += r;
                        }
                    }
                }
                newMatrix = result;
            }

            matrix = newMatrix;
        }

    }
}
