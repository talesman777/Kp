using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_10_01
{



    class Programm
    {
        private const string V = "Inputs";
        public static int[,] arr;
        public static int[] used;
        public static int n;
        public static int m;
        public static int flag;

        public static void funct(int v, int prev)
        {
            used[v] = 1;
            for (var i = 1; i <= n; i++)
            {
                if ((i != prev) && arr[v, i] == 1)
                {
                    if (used[i] == 1)
                    {
                        flag = 1;
                    }
                    else
                    {
                        funct(i, v);
                    }
                }
            }
        }
        public static void Main(String[] args)
        {
            string path = @"input.txt";
            bool first = true;

            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (first)
                    {
                        string [] parameters = line.Split(' ');
                        if (parameters.Length == 2)
                        {
                            n = Convert.ToInt32(parameters[0]);
                            m = Convert.ToInt32(parameters[1]);

                            var con = V;

                            arr = new int[n + 1, n + 1];
                            used = new int[n + 1];
                        }
                        first = false;
                    }
                    else
                    {

                        
                        string[] parameters = line.Split(' ');

                            var u = Convert.ToInt64(parameters[0]);
                            var v = Convert.ToInt64(parameters[0]);
                            arr[u, v] = arr[v, u] = 1;
                        
                    }
                    Console.WriteLine(line);
                }
            }

   
        
            for (var i = 1; i <= n; i++)
            {
                if (used[i] == 0)
                {
                    funct(i, -1);
                }
            }
            if (flag == 1)
            {
                Console.WriteLine("YES");
                printFile("YES");
            }
            else
            {
                Console.WriteLine("NO");
                printFile("NO");

            }
            Console.ReadLine();
        }
        public static void printFile(string v)
        {
            string writePath = @"output.txt";

            try
            {


                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(v);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

}

