using Graph.Command;
using Graph.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Threading;

namespace Graph.ViewModel
{
    public class MainWindowViewModel:ViewModelBase
    {

        private readonly string _connectionstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=..\\..\\..\\Data\\DesisionBD.accdb";
        private readonly OleDbConnection dbConnection;
        private ModularityDegree _degree;
        private GraphModel _graph;
        private GraphInit _graphInit;
        private List<int> _gridId;
        private List<GraphModel> graphs;
        private int _allCount = 0;
        private int _saveCount = 0;
        private int _count = 0;

        public GraphModel Graph
        {
            get { return _graph; }
            set { _graph = value; }
        }

        public int Count
        {
            get => _saveCount; 
            set 
            { 
                _saveCount = value;
                OnPropertyChanged(nameof(Count));
            }
        }

        public int CalculateCouunt
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged(nameof(CalculateCouunt));
            }
        }

        public int AllCount
        { 
            get=> _allCount;
            set { _allCount = value;}
        }

        public ICommand CalculationCommand { get; set; }
        public ICommand SaveCommand { get; }


        public MainWindowViewModel()
        {
            graphs = new List<GraphModel>();
            dbConnection = new OleDbConnection(_connectionstring);
            _graphInit = new GraphInit();
            _degree = new ModularityDegree();
            _gridId = _graphInit.InitGridIds(_gridId, dbConnection);


            foreach (int id in _gridId)
            {
                _graph = _graphInit.Initial(Graph, dbConnection, id);
                graphs.Add(_graph);
                _allCount += _graph.vertexCount;
            }

            CalculationCommand = new RelayCommand(ExecutedCalculationCommand, CanExecuteCalculationCommand);
            SaveCommand = new RelayCommand(ExecutedSaveCommand,CanExecuteSaveCommand);

        }

        private bool CanExecuteSaveCommand(object arg)
        {
            if (_graph.degrees !=  null) return true;
            return false;
        }

        private void ExecutedSaveCommand(object obj)
        {
           foreach (var graph in graphs) {
           
                foreach (var node in graph.nodes)
                {
                    using (OleDbConnection connection = new OleDbConnection(_connectionstring))
                    {

                        var cmd = new OleDbCommand();
                        cmd.Connection = connection;
                        cmd.Parameters.Clear();
                        cmd.CommandText = $"INSERT INTO GrCharF ([IdGr],[IdChar],[IdVrtx],[CharVal]) VALUES (@Gr,@Char,@Vrtx,@Val)";
                        cmd.Parameters.AddWithValue("@Gr", graph.grid);
                        cmd.Parameters.AddWithValue("@Char", 8);
                        cmd.Parameters.AddWithValue("@Vrtx", node.vertexId);
                        cmd.Parameters.AddWithValue("@Val", graph.degrees[node.vertexId - 1]);
                        connection.Open();
                        cmd.ExecuteNonQueryAsync();
                        connection.Close();
                    }
                    Count+=1;
                }
            }
   
            MessageBox.Show($"{Count} записей сохранено");
        }

        private bool CanExecuteCalculationCommand(object arg)
        {
            return _graph != null;
        }

        private void ExecutedCalculationCommand(object obj)
        {

            foreach (GraphModel graph in graphs) 
            {
                graph.degrees = _degree.MetricForAVertex(graph);
                CalculateCouunt += graph.vertexCount;
            }
                
                MessageBox.Show($"Посчитано расстояние для {AllCount} вершин");


        }

    }
    }
