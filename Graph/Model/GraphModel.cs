using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Model
{

    public class GraphModel// модель графа
    {
        public List<Nodes> nodes; // список узлов
        public int vertexCount; // количество вершие
        public double[] degrees; // массив значений модулярности вершин
        public int grid; // id графа
    }

    public class Nodes// модель узла графа
    {
        public int vertexId; // номер вершины
        public List<int> connections; // список связей с другими вершинами
        public int degree; // степень вершины графа
    }       
}
