using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KohonenSOM
{
    class Program
    {
        public static List<List<double>> data;
        public static List<List<double>> weights;
        public static Dictionary<int, List<double>> features;
        public static List<string> columns;
        public static List<List<double>> neuron_locs;
        public static int grid_x;
        public static int grid_y;
        public static Dictionary<int, int> index_map = new Dictionary<int, int>();
        static void Main(string[] args)
        {
            grid_x = 10;
            grid_y = 10;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //ReadFile.Read("taurus.csv");
            ReadFile.Read("emlak-veri.csv");
            //ReadFile.Read("diamond.csv");
            Console.WriteLine("Running SOM...");
            SOM.Run(grid_x, grid_y, 100*(grid_x*grid_y), 0.1, (grid_x + grid_y) / 2);
            sw.Stop();
            Console.WriteLine("-----------------------Weights-----------------------");
            for (int i = 0; i < weights.Count; i++)
            {
                string splitter = "";
                for (int j = 0; j < weights[0].Count; j++)
                {
                    Console.Write(splitter + weights[i][j]);
                    splitter = ",";
                }
                Console.WriteLine();
            }
            Console.WriteLine("Total time: " + sw.Elapsed);

            Application.EnableVisualStyles();
            Application.Run(new Visualize());

            //for (int i = 0; i < Program.columns.Count(); i++)
            //{
            //    Console.Write(columns[i] + " ");
            //}
            //Console.WriteLine();
            //for (int i = 0; i < Program.data.Count(); i++)
            //{
            //    Console.Write(i + " ");
            //    for (int j = 0; j < Program.data[i].Count(); j++)
            //    {
            //        Console.Write(Program.data[i][j] + " ");
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine();
        }
    }
}
