using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KohonenSOM
{
    class FileOperations
    {
        public static void Read(string path)
        {
            Console.WriteLine("Reading data...");
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");//for separating decimal numbers with dots
            StreamReader sr = new StreamReader(path);
            Program.data = new List<List<double>>();
            Program.features = new Dictionary<int, List<double>>();
            Dictionary<int, List<string>> categorical_columns = new Dictionary<int, List<string>>();
            Dictionary<int, HashSet<string>> categorical_column_names = new Dictionary<int, HashSet<string>>();
            int i = 0, j;
            double temp;
            Program.columns = new List<string>();
            string[] temp_columns = sr.ReadLine().Split(',');
            foreach (string str in temp_columns)//Reads column names
            {
                Program.columns.Add(str);
            }
            int index = 0;
            while (!sr.EndOfStream)
            {
                string[] line = sr.ReadLine().Split(',');
                List<double> temp_data = new List<double>();
                for (j = 0; j < line.Length; j++)
                {
                    if (Double.TryParse(line[j], out temp))//Checks for categorical features
                    {
                        temp_data.Add(temp);
                        if (i == 0)//In the beginning creates lists for features
                        {
                            List<double> temp_features = new List<double>();
                            temp_features.Add(temp);
                            Program.features.Add(j, temp_features);
                            Program.index_map.Add(index, j);//Index map keeps non-categorical features indices
                        }
                        else//If there is a list for current feature, value adds to this list
                        {
                            Program.features[j].Add(temp);
                        }
                        index++;
                    }
                    else
                    {
                        if (i == 0)//In the beginning creates lists for categorical features
                        {
                            List<string> temp_categorical = new List<string>();
                            HashSet<string> temp_categorical_names = new HashSet<string>();
                            temp_categorical.Add(line[j]);
                            temp_categorical_names.Add(line[j]);
                            categorical_columns.Add(j, temp_categorical);
                            categorical_column_names.Add(j, temp_categorical_names);
                        }
                        else// If there is a list for current categorical feature, value adds to this list
                        {
                            categorical_columns[j].Add(line[j]);
                            categorical_column_names[j].Add(line[j]);
                        }

                    }
                }
                Program.data.Add(temp_data);
                i++;
            }
            sr.Close();
            //DataPreprocess.NormalizeData(features);
            Console.WriteLine("Applying OneHotEncoder for categorical features...");
            DataPreprocess.OneHotEncoding(categorical_columns, categorical_column_names);
        }
        public static void Write(string path)
        {
            StreamWriter sw = new StreamWriter(path);
            Console.WriteLine("-----------------------Weights-----------------------");
            sw.WriteLine("-----------------------Weights-----------------------");
            for (int i = 0; i < Program.weights.Count; i++)
            {
                string splitter = "";
                for (int j = 0; j < Program.weights[0].Count; j++)
                {
                    Console.Write(splitter + Math.Round(Program.weights[i][j],4));
                    sw.Write(splitter + Math.Round(Program.weights[i][j], 4));
                    splitter = ",";
                }
                Console.WriteLine();
                sw.WriteLine();
            }
            sw.Close();
        }

    }
}
