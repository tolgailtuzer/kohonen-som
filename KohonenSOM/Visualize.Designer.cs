namespace KohonenSOM
{
    partial class Visualize
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.visualize_data = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.low_color = new System.Windows.Forms.Button();
            this.high_color = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridView.Enabled = false;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.Location = new System.Drawing.Point(-3, 43);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(632, 485);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            // 
            // visualize_data
            // 
            this.visualize_data.BackColor = System.Drawing.Color.DodgerBlue;
            this.visualize_data.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.visualize_data.FlatAppearance.BorderSize = 0;
            this.visualize_data.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.visualize_data.ForeColor = System.Drawing.Color.Black;
            this.visualize_data.Location = new System.Drawing.Point(402, 6);
            this.visualize_data.Name = "visualize_data";
            this.visualize_data.Size = new System.Drawing.Size(199, 23);
            this.visualize_data.TabIndex = 3;
            this.visualize_data.Text = "Visualize";
            this.visualize_data.UseVisualStyleBackColor = false;
            this.visualize_data.Click += new System.EventHandler(this.visualize_data_Click);
            // 
            // low_color
            // 
            this.low_color.BackColor = System.Drawing.Color.DodgerBlue;
            this.low_color.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.low_color.FlatAppearance.BorderSize = 0;
            this.low_color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.low_color.ForeColor = System.Drawing.Color.Black;
            this.low_color.Location = new System.Drawing.Point(12, 6);
            this.low_color.Name = "low_color";
            this.low_color.Size = new System.Drawing.Size(189, 23);
            this.low_color.TabIndex = 4;
            this.low_color.Text = "Low Value Color";
            this.low_color.UseVisualStyleBackColor = false;
            this.low_color.Click += new System.EventHandler(this.low_color_Click);
            // 
            // high_color
            // 
            this.high_color.BackColor = System.Drawing.Color.DodgerBlue;
            this.high_color.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.high_color.FlatAppearance.BorderSize = 0;
            this.high_color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.high_color.ForeColor = System.Drawing.Color.Black;
            this.high_color.Location = new System.Drawing.Point(207, 6);
            this.high_color.Name = "high_color";
            this.high_color.Size = new System.Drawing.Size(189, 23);
            this.high_color.TabIndex = 5;
            this.high_color.Text = "High Value Color";
            this.high_color.UseVisualStyleBackColor = false;
            this.high_color.Click += new System.EventHandler(this.high_color_Click);
            // 
            // Visualize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(623, 518);
            this.Controls.Add(this.high_color);
            this.Controls.Add(this.low_color);
            this.Controls.Add(this.visualize_data);
            this.Controls.Add(this.dataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(639, 557);
            this.MinimumSize = new System.Drawing.Size(639, 557);
            this.Name = "Visualize";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kohonen SOM";
            this.Load += new System.EventHandler(this.Visualize_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button visualize_data;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button low_color;
        private System.Windows.Forms.Button high_color;
    }
}