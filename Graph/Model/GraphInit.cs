using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows;

namespace Graph.Model
{
    class GraphInit
    {
        public GraphModel Initial(GraphModel graph, OleDbConnection dbConnection, int id)
        {
            dbConnection.Open();
            string query = $"SELECT * FROM GrLink WHERE Grid = {id}";
            OleDbCommand oleDbCommand = new OleDbCommand(query,dbConnection);
            OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();

            if (oleDbDataReader.HasRows == false)
            {
                MessageBox.Show("Данные украли бобры");
            }
            else
            {
                bool stop = false;
                var newGraph = new GraphModel();
                int sourceNumber = 1;
                graph = new GraphModel
                {
                    nodes = new List<Nodes>(),
                    vertexCount = 6
                };


                while (oleDbDataReader.Read()) 
                { 
                    
                    int source = oleDbDataReader.GetInt32(1);
                    int destination = oleDbDataReader.GetInt32(2);
                    bool isAdd = true;
                    int newConnection = destination;
                    int newSource = source;

                    if (graph.nodes.Count != 0)
                        foreach (var n in graph.nodes)
                            if (n.vertexId == source )
                            {
                                n.connections.Add(destination);
                                isAdd = false;
                                n.degree++;
                            }

                    if (isAdd)
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
                    if (graph.nodes.Count != 0)
                        foreach (var n in graph.nodes)
                            if (n.vertexId == destination)
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

                    if (isAdd)
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
            return graph;
        }
        public List<int> InitGridIds(List<int> grids, OleDbConnection dbConnection)
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
