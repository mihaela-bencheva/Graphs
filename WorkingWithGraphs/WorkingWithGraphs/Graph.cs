using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WorkingWithGraphs
{
    public class Graph
    {
        private int[,] adjMatrix;
        private int adjMatrixLength;

        public Graph(int M)
        {
            adjMatrixLength = M;
            adjMatrix = new int[M,M];

            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    adjMatrix[i, j] = 0;
                }
            }
        }

        public void ReadFromEdgesFile()
        {
            List<string> edges = new List<string>();
            StreamReader sr = new StreamReader("edges.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                edges.Add(line);
            }
            sr.Close();

            ConvertEdgesToAdjacencyMatrix(edges);
        }

        public void ConvertEdgesToAdjacencyMatrix(List<string> edges)
        {
            for (int i = 0; i < edges.Count; i++)
            {
                string[] numbers = edges[i].Split(" ");
                adjMatrix[Convert.ToInt32(numbers[0]) - 1, Convert.ToInt32(numbers[1]) - 1] = Convert.ToInt32(numbers[2]);
                adjMatrix[Convert.ToInt32(numbers[1]) - 1, Convert.ToInt32(numbers[0]) - 1] = Convert.ToInt32(numbers[2]);
            }
        }

        public List<string> ConvertAdjacencyMatrixToEdges()
        {
            List<string> edges = new List<string>();
            for (int i = 0; i < adjMatrixLength; i++)
            {
                for (int j = i; j < adjMatrixLength; j++)
                {
                    if (adjMatrix[i,j] != 0)
                    {
                        edges.Add((i + 1) + " " + (j + 1) + " " + adjMatrix[i, j]);
                    }
                }
            }
            return edges;
        }

        public void WriteEdgesToFile()
        {
            List<string> edges = ConvertAdjacencyMatrixToEdges();
            StreamWriter sw = new StreamWriter("edges.txt");
            for (int i = 0; i < edges.Count; i++)
            {
                sw.WriteLine(edges[i]);
            }
            sw.Close();
        }

        public void ReadFromAdjacencyMatrixFile()
        {
            StreamReader sr = new StreamReader("adjancency_matrix.txt");
            string line;
            int countLines = 0;
            while ((line = sr.ReadLine()) != null)
            {
                string[] numbers = line.Split(" ");

                for (int i = 0; i < numbers.Length; i++)
                {
                    adjMatrix[countLines, i] = Convert.ToInt32(numbers[i]);
                }
                countLines++;
            }
            sr.Close();
        }

        public void WriteAdjacencyMatrixToFile()
        {
            StreamWriter sw = new StreamWriter("adjancency_matrix.txt");
            for (int i = 0; i < adjMatrixLength - 1; i++)
            {
                for (int j = 0; j < adjMatrixLength - 1; j++)
                {
                    sw.Write(adjMatrix[i, j] + " ");
                }
                sw.WriteLine();
            }
            sw.Close();
        }

        public void ReadIncidenceMatrixFromFile()
        {
            StreamReader sr = new StreamReader("incidence_matrix.txt");
            string line;
            List<string> lines = new List<string>();
            while ((line = sr.ReadLine()) != null)
            {
                lines.Add(line);
            }
            ConvertIncidenceToAdjancencyMatrix(lines);
            sr.Close();
        }

        public void ConvertIncidenceToAdjancencyMatrix(List<string> lines)
        {
            string[][] numbers = new string[lines.Count][];

            for (int i = 0; i < lines.Count; i++)
            {
                numbers[i] = lines[i].Split("\t");
            }

            for (int i = 0; i < numbers[0].Length; i++)
            {
                int nodeOne = 0;
                int nodeTwo = 0;
                int weight = 0;
                for (int j = 0; j < numbers.Length; j++)
                {
                    if (numbers[j][i] != "0")
                    {
                        if (nodeOne == 0)
                        {
                            nodeOne = j;
                            weight = Convert.ToInt32(numbers[j][i]);
                        }
                        else
                        {
                            nodeTwo = j;
                        }
                    }
                }
                adjMatrix[nodeOne, nodeTwo] = weight;
                adjMatrix[nodeTwo, nodeOne] = weight;
            }
        }

        public List<string> ConvertAdjacencyToIncidenceMatrix()
        {
            List<string> lines = new List<string>();
            int edges = 0;
            for (int i = 0; i < adjMatrixLength; i++)
            {
                for (int j = 1; j < adjMatrixLength; j++)
                {
                    if (adjMatrix[i,j] != 0)
                    {
                        edges++;
                    }
                }
            }

            int[,] incidenceMatrix = new int[adjMatrixLength, edges];
            int currentEdge = 0;

            for (int i = 0; i < adjMatrixLength; i++)
            {
                for (int j = 1; j < adjMatrixLength; j++)
                {
                    if (adjMatrix[i, j] != 0)
                    {
                        incidenceMatrix[i, currentEdge] = adjMatrix[i,j];
                        incidenceMatrix[j, currentEdge] = adjMatrix[i, j];
                        currentEdge++;
                    }
                }
            }

            for (int i = 0; i < adjMatrixLength; i++)
            {
                string line = "";
                line += incidenceMatrix[i, 0];
                for (int j = 1; j < edges; j++)
                {
                    line += " " + incidenceMatrix[i, j];
                }
                lines.Add(line);
            }
            
            return lines;
        }

        public void WriteIncidenceMatrixToFile()
        {
            List<string> lines = ConvertAdjacencyToIncidenceMatrix();
            StreamWriter sw = new StreamWriter("incidence_matrix.txt");

            foreach (var item in lines)
            {
                sw.WriteLine(item);
            }
            sw.Close();
        }

        public void PrintMatrix()
        {
            for (int i = 0; i < adjMatrixLength - 1; i++)
            {
                for (int j = 0; j < adjMatrixLength - 1; j++)
                {
                    Console.Write(adjMatrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
