using Graph.Model;
using System.Data;
using System.Data.OleDb;

namespace Graph.ViewModel
{
    public class MainWindowViewModel
    {
        private readonly string _connectionstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\snetk\\OneDrive\\Документы\\GitHub\\12345\\Aviasales\\Graph\\Graph\\Data\\DesisionBD.accdb";

        private GraphModel _graph;
        private GraphInit _graphInit;
        public GraphModel Graph
        {
            get { return _graph; }
            set { _graph = value; }
        }

        public MainWindowViewModel()
        {
            OleDbConnection dbConnection = new OleDbConnection(_connectionstring);
            _graphInit = new GraphInit();
            _graph = _graphInit.Initial(_graph, dbConnection, 1);

        }


    }
}
