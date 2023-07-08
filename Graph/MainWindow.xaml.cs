using Graph.Model;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Documents;

namespace Graph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string _connectionstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\snetk\\OneDrive\\Документы\\GitHub\\12345\\Aviasales\\Graph\\Graph\\Data\\DesisionBD.accdb";

        private ModularityDegree _degree;
        private GraphModel _graph;
        private GraphInit _graphInit;
        private List<int> _gridId;
        private List<GraphModel> graphs;

        public GraphModel Graph
        {
            get { return _graph; }
            set { _graph = value; }
        }
        public MainWindow()
        {
            InitializeComponent();
            graphs = new List<GraphModel>();
            OleDbConnection dbConnection = new OleDbConnection(_connectionstring);
            _graphInit = new GraphInit();
            _degree = new ModularityDegree();
            _gridId = _graphInit.InitGridIds(_gridId, dbConnection);




            foreach (int id in _gridId)
            {
                _graph = _graphInit.Initial(Graph, dbConnection, id);
                graphs.Add(_graph);
            }


            foreach (GraphModel graph in graphs)
                    graph.degrees = _degree.MetricForAVertex(graph);
            



            int summ = 0;
        }
    }
}
