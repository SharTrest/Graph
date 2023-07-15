using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows;

namespace Graph.Model
{
    class GraphInit
    {
        public GraphModel Initial(GraphModel graph, OleDbConnection dbConnection, int id)// инициализация графов
        {
            dbConnection.Open();
            string query = $"SELECT * FROM GrLink WHERE Grid = {id}";
            OleDbCommand oleDbCommand = new OleDbCommand(query,dbConnection);
            OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();

            if (oleDbDataReader.HasRows == false)
            {
                MessageBox.Show("Не удалось присоединиться к БД");
            }

            else
            {
                bool stop = false;
                var newGraph = new GraphModel();
                int sourceNumber = 1;
                graph = new GraphModel
                {
                    nodes = new List<Nodes>(),
                    vertexCount = 0
                };


                while (oleDbDataReader.Read()) // считывание данных
                {
                    
                    int source = oleDbDataReader.GetInt32(1);
                    int destination = oleDbDataReader.GetInt32(2);
                    bool isAdd = true;
                    int newConnection = destination;
                    int newSource = source;

                    if (graph.nodes.Count != 0) // проверка на наличие вершины
                        foreach (var n in graph.nodes)
                            if (n.vertexId == source)// добавляем связь
                            {
                                n.connections.Add(destination);
                                isAdd = false;
                                n.degree++;
                            }

                    if (isAdd)// если вершины нет, то создаем новую
                    {
                        var newNode = new Nodes
                        {
                            vertexId = newSource,
                            connections = new List<int> { newConnection },
                            degree = 1
                        };
                        graph.nodes.Add(newNode);
                    }

                    isAdd = true;

                    if (graph.nodes.Count != 0) // // проверка на наличие вершины
                        foreach (var n in graph.nodes)
                            if (n.vertexId == destination)// добавляем связь
                            {
                                n.connections.Add(source);
                                isAdd = false;
                                newSource = destination;
                                newConnection = source;
                                n.degree++;
                            }
                    else
                            {
                                newSource = destination;
                                newConnection = source;
                            }

                    if (isAdd) // если вершины нет, то создаем новую
                    {
                        var newNode = new Nodes
                        {
                            vertexId = newSource,
                            connections = new List<int> { newConnection },
                            degree = 1
                        };
                        graph.nodes.Add(newNode);
                    }

                }

            }
            dbConnection.Close();
            graph.vertexCount = graph.nodes.Count;
            graph.grid = id;
            return graph;
        }
        public List<int> InitGridIds(List<int> grids, OleDbConnection dbConnection)// создаем список с id графов
        {
            dbConnection.Open();

            string query = "SELECT * FROM GrProperties";
            OleDbCommand oleDbCommand = new OleDbCommand(query, dbConnection);
            OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();

            if (oleDbDataReader.HasRows == false)
            {
                MessageBox.Show("Данные украли бобры");
            }

            grids = new List<int>();

            while (oleDbDataReader.Read()) 
            { 
                grids.Add(oleDbDataReader.GetInt32(0));
            }
            dbConnection.Close();
            return grids;

        }
    }
}
