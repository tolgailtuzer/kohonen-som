using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KohonenSOM
{
    public partial class Visualize : Form
    {
        public Visualize()
        {
            InitializeComponent();
        }
        public Color firstColour = Color.DarkBlue;
        //public Color firstColour = Color.RoyalBlue;
        public Color secondColour = Color.LightSkyBlue;
        public double Max_Value = double.MinValue;
        public double Min_Value = double.MaxValue;
        private void Visualize_Load(object sender, EventArgs e)
        {
            visualize_data.PerformClick();//Click for default visualization
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            this.dataGridView.ClearSelection();
        }

        void FillGrid(double[,] data)//Fills grids with CreateHeatMapForSOM data
        {
            int maxRow = 2*Program.grid_x-1;
            int maxCol = 2*Program.grid_y-1;

            dataGridView.RowHeadersVisible = false;
            dataGridView.ColumnHeadersVisible = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToOrderColumns = false;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;

            int rowHeight = dataGridView.ClientSize.Height / maxRow;
            int colWidth = dataGridView.ClientSize.Width / maxCol;

            for (int c = 0; c < maxRow; c++) dataGridView.Columns.Add(c.ToString(), "");
            for (int c = 0; c < maxRow; c++) dataGridView.Columns[c].Width = colWidth;
            dataGridView.Rows.Add(maxRow);
            for (int r = 0; r < maxRow; r++) dataGridView.Rows[r].Height = rowHeight;


            for (int r = 0; r < maxRow; r++)
            {
                for (int c = 0; c < maxRow; c++)
                {
                    dataGridView[r, c].Style.BackColor = HeatMapColor(data[r, c], Min_Value, Max_Value);
                    //dataGridView[r, c].Value = data[r,c].ToString();
                }
            }

        }
        private Color HeatMapColor(double value, double min, double max)//Determines value of color according to min and max
        {
            int rOffset = Math.Max(firstColour.R, secondColour.R);
            int gOffset = Math.Max(firstColour.G, secondColour.G);
            int bOffset = Math.Max(firstColour.B, secondColour.B);

            int deltaR = Math.Abs(firstColour.R - secondColour.R);
            int deltaG = Math.Abs(firstColour.G - secondColour.G);
            int deltaB = Math.Abs(firstColour.B - secondColour.B);

            double val = (value - min) / (max - min);
            int r = rOffset - Convert.ToByte(deltaR * (1 - val));
            int g = gOffset - Convert.ToByte(deltaG * (1 - val));
            int b = bOffset - Convert.ToByte(deltaB * (1 - val));

            return Color.FromArgb(255, r, g, b);
        }

        private void visualize_data_Click(object sender, EventArgs e)
        {
            this.dataGridView.Rows.Clear();
            this.dataGridView.Columns.Clear();
            FillGrid(CreateHeatMapForSOM());

        }

        private void low_color_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            low_color.BackColor = colorDialog1.Color;
            firstColour = colorDialog1.Color;
        }

        private void high_color_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            high_color.BackColor = colorDialog1.Color;
            secondColour = colorDialog1.Color;
        }

        public double[,] CreateHeatMapForSOM()//Creates heatmap with creating u-matrix
        {
            List<List<int>> neighbours = new List<List<int>>();
            for (int i = 0; i < Program.neuron_locs.Count; i++)//find neighbours of each neuron
            {
                List<int> temp = new List<int>();
                for (int j = 0; j < Program.neuron_locs.Count; j++)
                {
                    if (i != j && SOM.EuclideanDistance(Program.neuron_locs[i], Program.neuron_locs[j]) <= 2)
                    {
                        temp.Add(j);
                    }
                }
                neighbours.Add(temp);
            }

            double[,] heat_map = new double[2 * Program.grid_x - 1, 2 * Program.grid_y - 1];

            for (int i = 0; i < Program.neuron_locs.Count; i++)// fill cells between neurons with distances
            {
                for (int j = 0; j < neighbours[i].Count; j++)
                {
                    if (Program.neuron_locs[i][0] == Program.neuron_locs[neighbours[i][j]][0])//if they are in same x axis
                    {
                        if (Program.neuron_locs[neighbours[i][j]][1] < Program.neuron_locs[i][1])//left-side check
                        {
                            if (heat_map[Convert.ToInt32(Program.neuron_locs[i][0]), Convert.ToInt32(Program.neuron_locs[i][1])] == 0)
                                heat_map[Convert.ToInt32(Program.neuron_locs[i][0]), Convert.ToInt32(Program.neuron_locs[i][1]) - 1] = SOM.EuclideanDistance(Program.weights[i], Program.weights[neighbours[i][j]]);
                        }
                        else if (Program.neuron_locs[neighbours[i][j]][1] > Program.neuron_locs[i][1])//right-side check
                        {
                            if (heat_map[Convert.ToInt32(Program.neuron_locs[neighbours[i][j]][0]), Convert.ToInt32(Program.neuron_locs[neighbours[i][j]][1])] == 0)
                                heat_map[Convert.ToInt32(Program.neuron_locs[neighbours[i][j]][0]), Convert.ToInt32(Program.neuron_locs[neighbours[i][j]][1]) - 1] = SOM.EuclideanDistance(Program.weights[i], Program.weights[neighbours[i][j]]);
                        }
                    }
                    else//if they are in same y axis
                    {
                        if (Program.neuron_locs[neighbours[i][j]][0] < Program.neuron_locs[i][0])//up-side check
                        {
                            if (heat_map[Convert.ToInt32(Program.neuron_locs[i][0]), Convert.ToInt32(Program.neuron_locs[i][1])] == 0)
                                heat_map[Convert.ToInt32(Program.neuron_locs[i][0]) - 1, Convert.ToInt32(Program.neuron_locs[i][1])] = SOM.EuclideanDistance(Program.weights[i], Program.weights[neighbours[i][j]]);
                        }
                        else if (Program.neuron_locs[neighbours[i][j]][0] > Program.neuron_locs[i][0])//down-side check
                        {
                            if (heat_map[Convert.ToInt32(Program.neuron_locs[neighbours[i][j]][0]), Convert.ToInt32(Program.neuron_locs[neighbours[i][j]][1])] == 0)
                                heat_map[Convert.ToInt32(Program.neuron_locs[neighbours[i][j]][0]) - 1, Convert.ToInt32(Program.neuron_locs[neighbours[i][j]][1])] = SOM.EuclideanDistance(Program.weights[i], Program.weights[neighbours[i][j]]);
                        }
                    }
                }
            }
            for (int i = 0; i < 2 * Program.grid_x - 1; i++)//fills neuron cells with surrounded cell averages
            {
                for (int j = 0; j < 2 * Program.grid_y - 1; j++)
                {
                    if (heat_map[i, j]==0)
                    {
                        double sum = 0;
                        int count = 0;
                        if (j - 1 >= 0)
                        {
                            sum += heat_map[i, j - 1];
                            count++;
                        }
                        if (i - 1 >= 0)
                        {
                            sum += heat_map[i - 1, j];
                            count++;
                        }
                        if (i + 1 <= 2 * Program.grid_x - 2)
                        {
                            sum += heat_map[i + 1, j];
                            count++;
                        }
                        if (j + 1 <= 2 * Program.grid_y - 2)
                        {
                            sum += heat_map[i, j + 1];
                            count++;
                        }
                        heat_map[i, j] = sum / count;
                    }                  
                }
            }
            for (int i = 0; i < 2 * Program.grid_x - 1; i++)
            {
                for (int j = 0; j < 2 * Program.grid_y - 1; j++)
                {
                    if (heat_map[i, j] < Min_Value) Min_Value = heat_map[i, j];
                    if (heat_map[i, j] > Max_Value) Max_Value = heat_map[i, j];
                }
            }
            return heat_map;
        }
    }
}
