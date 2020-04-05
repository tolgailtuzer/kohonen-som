﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KohonenSOM
{
    class SOM
    {

        public static double EuclideanDistance(List<double> node1, List<double> node2)
        {
            double result = 0;
            for (int i = 0; i < node1.Count; i++)
            {
                result += Math.Pow((node1[i] - node2[i]), 2);
            }
            return Math.Sqrt(result);
        }

        public static void Run(int grid_x, int grid_y, int iteration, double learning_rate_init, double sigma_init)
        {
            int neuron_count = grid_x * grid_y;
            int feature_count = Program.data[0].Count;
            Program.weights = new List<List<double>>();
            Random random = new Random();
            Program.neuron_locs = new List<List<double>>();
            for(int i=0;i<grid_x;i++)
            {
                
                for(int j=0;j<grid_y;j++)
                {
                    List<double> temp = new List<double>();
                    temp.Add(i);
                    temp.Add(j);
                    Program.neuron_locs.Add(temp);
                }
            }
            for (int i = 0; i < neuron_count; i++)
            {
                List<double> temp = new List<double>();
                for (int j = 0; j < feature_count; j++)
                {
                    if (Program.index_map.ContainsKey(j)) temp.Add(random.NextDouble() * (Program.features[Program.index_map[j]].Max() - Program.features[Program.index_map[j]].Min()) + Program.features[Program.index_map[j]].Min());
                    else
                    {
                        double rnd = random.NextDouble();
                        if (rnd < 0.5) temp.Add(0);
                        else temp.Add(1);
                    }
                }
                Program.weights.Add(temp);
            }

            //for (int i = 0; i <Program.weights.Count; i++)
            //{
            //    string splitter = "";
            //    for (int j = 0; j < Program.weights[0].Count; j++)
            //    {
            //        Console.Write(splitter + Program.weights[i][j]);
            //        splitter = ",";
            //    }
            //    Console.WriteLine();
            //}
            double t1_init = iteration / Math.Log(sigma_init);
            double t2_init = iteration;
            for (int iter = 0; iter < iteration; iter++)
            {
                int i = random.Next(Program.data.Count);
                double best_node_value = int.MaxValue;
                int best_node_index = 0;
                for (int j = 0; j < neuron_count; j++)
                {
                    double distance = EuclideanDistance(Program.data[i], Program.weights[j]);
                    if (distance < best_node_value)
                    {
                        best_node_value = distance;
                        best_node_index = j;
                    }
                }

                double sigma = sigma_init * Math.Exp(-iter / t1_init);
                double learning_rate = learning_rate_init * Math.Exp(-iter / t2_init);

                for (int j = 0; j < neuron_count; j++)
                {
                    double distance = EuclideanDistance(Program.weights[j], Program.weights[best_node_index]);
                    double h_ji = Math.Exp(-(Math.Pow(distance, 2) / (2 * Math.Pow(sigma, 2))));
                    for (int k = 0; k < Program.weights[j].Count; k++)
                    {
                        Program.weights[j][k] = Program.weights[j][k] + learning_rate * h_ji * (Program.data[i][k] - Program.weights[j][k]);
                    }
                }
            }
        }


    }
}