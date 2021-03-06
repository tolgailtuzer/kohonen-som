﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KohonenSOM
{
    class DataPreprocess
    {
        public static void OneHotEncoding(Dictionary<int, List<string>> categorical_columns, Dictionary<int, HashSet<string>> categorical_column_names)
        {
            foreach (var a in categorical_column_names)//Replace column names for categorical features appropriately for OneHotEncoder
            {
                List<string> list = a.Value.ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    Program.columns.Add(Program.columns[a.Key] + "_" + list[i]);
                }
                Program.columns.RemoveAt(a.Key);
            }
            foreach (var a in categorical_columns)//for each categorical feature
            {
                for (int i = 0; i < a.Value.Count; i++)//for this feature's each sample
                {
                    List<double> temp = new List<double>();
                    foreach (var b in categorical_column_names)
                    {
                        if (a.Key == b.Key)//finds current categorical feature name in list
                        {
                            List<string> list = b.Value.ToList();
                            for (int j = 0; j < list.Count; j++)//transforms it to encoded form
                            {
                                if (a.Value[i] == list[j])
                                {
                                    temp.Add(1);
                                }
                                else
                                {
                                    temp.Add(0);
                                }
                            }
                        }
                    }
                    for (int l = 0; l < temp.Count; l++)
                    {
                        Program.data[i].Add(temp[l]);
                    }
                }
            }
        }

        public static void NormalizeData(Dictionary<int, List<double>> features)//Normalize data with using (X-Min)/(Max-Min) formula
        {
            for (int i = 0; i < Program.data.Count(); i++)
            {
                for (int j = 0; j < Program.data[i].Count(); j++)
                {
                    Program.data[i][j] = (Program.data[i][j] - features[Program.index_map[j]].Min()) / (features[Program.index_map[j]].Max() - features[Program.index_map[j]].Min());
                }
            }

        }
    }
}
