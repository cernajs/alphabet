using System;
using System.Collections.Generic;

namespace findWord
{
    public class Coords
    {
        public readonly int x, y;
        public readonly int length;

        public Coords(int coord_x, int coord_y, int len)
        {
            x = coord_x;
            y = coord_y;
            length = len;
        }
    }
    class Program
    {
        static List<Coords> findNeighborous(int width, int height, int x, int y, int length)
        {
            int[][] moves = new int[4][];
            moves[0] = new int[2] {x + 1, y};
            moves[1] = new int[2] {x - 1, y};
            moves[2] = new int[2] {x, y + 1};
            moves[3] = new int[2] {x, y - 1};

            var neighborous = new List<Coords>();
            foreach (var neighbour in moves)
            {
                int x1 = neighbour[0];
                int y1 = neighbour[1];
                if ((x1 >= 0 && x1 < width) && (y1 >= 0 && y1 < height))
                {
                    neighborous.Add(new Coords(x1,y1,length+1));
                }
            }
            return neighborous;
        }
        static int bfs(string[,] table,string word,int[] pos)
        {
            if (word.Length == 0)
            {
                return 0;
            }
            string wantToFind = word.Substring(0, 1);

            int width = 3;
            int height = 3;
            var visited = new HashSet<string>();
            var queue = new Queue<Coords>();
            int[] position = new[] {pos[0], pos[1]};

            var current = new Coords(position[1], position[0], 0);
            
            queue.Enqueue(current);
            visited.Add("00");

            while (queue.Count > 0)
            {
                var location = queue.Dequeue();
                
                int x_temp = location.x, y_temp = location.y, length_temp = location.length;

                if (table[x_temp,y_temp] == wantToFind)
                {
                    
                    return length_temp + 1 + bfs(table, word.Substring(1),new []{x_temp,y_temp});
                    
                }
                
                var neighbours = findNeighborous(3,3,x_temp,y_temp,length_temp);
                foreach (var neighbor in neighbours)
                {
                    //Console.WriteLine();
                    string hashLocation = neighbor.x.ToString() + neighbor.y.ToString();
                    if (!visited.Contains(hashLocation))
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(hashLocation);
                    }
                }
            }
            return 0;
        }
        static void Main(string[] args)
        {
            /*
             [vyska,sirka]
             */
            //int width = Console.Read();
            //int height = Console.Read();
            //var text = Console.ReadLine().ToString().ToCharArray();
            //string fin_word = Console.ReadLine().ToString();
            
            
            int[] start_position = new[] {0, 0};
            /*
            string[,] table = new string[height, width];

            int k = 0;
            while (k<height)
            {
                int l = 0;
                while ((l<width))
                {
                    table[k, l] = text[l].ToString();
                    Console.WriteLine(l);
                    l++;
                }
                k++;
            }

            foreach (var row in table)
            {
                Console.WriteLine(row);
            }
            */
            string word = "CU";
            
            string[,] table = new string[,]
            {
                {"A","B","C"},
                {"C","D","A"},
                {"Z","G","F"}
            };
            
            Console.WriteLine(bfs(table,word,start_position));

        }
    }
}