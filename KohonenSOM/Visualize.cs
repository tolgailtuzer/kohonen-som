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
        public Color firstColour = Color.RoyalBlue;
        public Color secondColour = Color.LightSkyBlue;
        public double Max_Value = double.MinValue;
        public double Min_Value = double.MaxValue;
        private void Visualize_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Program.columns.Count; i++)
            {
                this.columns.Items.Add(Program.columns[i]);
            }
            this.columns.SelectedIndex = 0;
            visualize_data.PerformClick();

        }
        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            this.dataGridView.ClearSelection();
        }
        void fillData(/*List<List<double>>*/double [,] data)
        {
            int maxRow = Program.grid_x;
            int maxCol = Program.grid_y;

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
                    dataGridView[r, c].Style.BackColor = HeatMapColor(data[r,c], Min_Value, Max_Value);
                    //dataGridView[r, c].Style.BackColor = HeatMapColor(data[r][c], Program.features[this.columns.SelectedIndex].Min(), Program.features[this.columns.SelectedIndex].Max());
                    //dataGridView[r, c].Value = data[r][c].ToString();
                }
            }

        }
        private Color HeatMapColor(double value, double min, double max)
        {

            // Example: Take the RGB
            //135-206-250 // Light Sky Blue
            // 65-105-225 // Royal Blue
            // 70-101-25 // Delta

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
            //List<List<double>> data = new List<List<double>>();
            //List<double> temp = new List<double>();
            //for (int i = 1; i <= Program.weights.Count; i++)
            //{
            //    temp.Add(Program.weights[i - 1][this.columns.SelectedIndex]);
            //    if (i % Program.grid_x == 0)
            //    {
            //        data.Add(temp);
            //        temp = new List<double>();
            //    }
            //}
            ////data[0][0] = Program.features[this.columns.SelectedIndex].Max();
            //fillData(data);
            fillData(CreateHeatMapForSOM());

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

        public double[,] CreateHeatMapForSOM()
        {
            List<List<int>> neighbours = new List<List<int>>();
            for (int i = 0; i < Program.neuron_locs.Count; i++)
            {
                List<int> temp = new List<int>();
                for (int j = 0; j < Program.neuron_locs.Count; j++)
                {
                    if (i != j && SOM.EuclideanDistance(Program.neuron_locs[i], Program.neuron_locs[j]) <= 1)
                    {
                        temp.Add(j);
                    }
                }
                neighbours.Add(temp);
            }
            double[,] heat_map = new double[Program.grid_x, Program.grid_y];
            for (int i = 0; i < Program.neuron_locs.Count; i++)
            {
                double sum = 0;
                for (int j = 0; j < neighbours[i].Count; j++)
                {
                    sum += SOM.EuclideanDistance(Program.weights[i], Program.weights[neighbours[i][j]]);
                }
                if(sum / neighbours[i].Count<Min_Value)
                {
                    Min_Value = sum / neighbours[i].Count;
                }
                if(sum / neighbours[i].Count>Max_Value)
                {
                    Max_Value = sum / neighbours[i].Count;
                }
                heat_map[Convert.ToInt32(Program.neuron_locs[i][0]), Convert.ToInt32(Program.neuron_locs[i][1])] = sum / neighbours[i].Count;
            }
            return heat_map;
        }
    }
}
