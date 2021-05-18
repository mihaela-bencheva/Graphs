using System;

namespace WorkingWithGraphs
{
    class Program
    {
        static void Main()
        {
            Graph graph = new Graph(9);
            graph.ReadFromEdgesFile();
            graph.ReadFromAdjacencyMatrixFile();
            graph.WriteEdgesToFile();
            graph.WriteAdjacencyMatrixToFile();
            graph.ReadIncidenceMatrixFromFile();
            graph.WriteIncidenceMatrixToFile();
        }
    }
}
