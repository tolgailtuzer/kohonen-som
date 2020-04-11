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
        public static List<List<double>> data;//all data read from the file
        public static List<List<double>> weights;//SOM network weights
        public static Dictionary<int, List<double>> features;//data features and elements
        public static List<string> columns;//Column names
        public static List<List<double>> neuron_locs;//output layer neuron coordinates
        public static int grid_x;//output layer x axis size
        public static int grid_y;//output layer y axis size
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
            SOM.Run(grid_x, grid_y, 100 * (grid_x * grid_y), 0.1, (grid_x + grid_y) / 2);
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
        }
    }
}
