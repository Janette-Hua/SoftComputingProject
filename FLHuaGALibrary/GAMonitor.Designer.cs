
namespace FLHuaGALibrary
{
    partial class GAMonitor<S>
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnRunToEnd = new System.Windows.Forms.Button();
            this.btnRunOneIteration = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.prgGA = new System.Windows.Forms.PropertyGrid();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lbPopulation = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbBestSolution = new System.Windows.Forms.ListBox();
            this.tbxBestObjectiveValue = new System.Windows.Forms.TextBox();
            this.labSoFarTheBestObjective = new System.Windows.Forms.Label();
            this.chtGA = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtGA)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(702, 512);
            this.splitContainer1.SplitterDistance = 232;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btnRunToEnd);
            this.splitContainer2.Panel1.Controls.Add(this.btnRunOneIteration);
            this.splitContainer2.Panel1.Controls.Add(this.btnReset);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.prgGA);
            this.splitContainer2.Size = new System.Drawing.Size(232, 512);
            this.splitContainer2.SplitterDistance = 146;
            this.splitContainer2.TabIndex = 0;
            // 
            // btnRunToEnd
            // 
            this.btnRunToEnd.Enabled = false;
            this.btnRunToEnd.Location = new System.Drawing.Point(19, 97);
            this.btnRunToEnd.Name = "btnRunToEnd";
            this.btnRunToEnd.Size = new System.Drawing.Size(101, 36);
            this.btnRunToEnd.TabIndex = 17;
            this.btnRunToEnd.Text = "Run To End";
            this.btnRunToEnd.UseVisualStyleBackColor = true;
            this.btnRunToEnd.Click += new System.EventHandler(this.btnRunToEnd_Click);
            // 
            // btnRunOneIteration
            // 
            this.btnRunOneIteration.AccessibleDescription = "";
            this.btnRunOneIteration.Enabled = false;
            this.btnRunOneIteration.Location = new System.Drawing.Point(19, 55);
            this.btnRunOneIteration.Name = "btnRunOneIteration";
            this.btnRunOneIteration.Size = new System.Drawing.Size(101, 36);
            this.btnRunOneIteration.TabIndex = 16;
            this.btnRunOneIteration.Text = "Run One Iteration";
            this.btnRunOneIteration.UseVisualStyleBackColor = true;
            this.btnRunOneIteration.Click += new System.EventHandler(this.btnRunOneIteration_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(19, 13);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(101, 36);
            this.btnReset.TabIndex = 15;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // prgGA
            // 
            this.prgGA.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgGA.Location = new System.Drawing.Point(3, 3);
            this.prgGA.Name = "prgGA";
            this.prgGA.Size = new System.Drawing.Size(226, 356);
            this.prgGA.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer3.Panel1.Controls.Add(this.lbPopulation);
            this.splitContainer3.Panel1.Controls.Add(this.label1);
            this.splitContainer3.Panel1.Controls.Add(this.lbBestSolution);
            this.splitContainer3.Panel1.Controls.Add(this.tbxBestObjectiveValue);
            this.splitContainer3.Panel1.Controls.Add(this.labSoFarTheBestObjective);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.chtGA);
            this.splitContainer3.Size = new System.Drawing.Size(466, 512);
            this.splitContainer3.SplitterDistance = 255;
            this.splitContainer3.TabIndex = 1;
            // 
            // lbPopulation
            // 
            this.lbPopulation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPopulation.FormattingEnabled = true;
            this.lbPopulation.Location = new System.Drawing.Point(158, 28);
            this.lbPopulation.Name = "lbPopulation";
            this.lbPopulation.Size = new System.Drawing.Size(296, 212);
            this.lbPopulation.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(155, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 28);
            this.label1.TabIndex = 8;
            this.label1.Text = "Population";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbBestSolution
            // 
            this.lbBestSolution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbBestSolution.FormattingEnabled = true;
            this.lbBestSolution.Location = new System.Drawing.Point(13, 55);
            this.lbBestSolution.Name = "lbBestSolution";
            this.lbBestSolution.Size = new System.Drawing.Size(131, 186);
            this.lbBestSolution.TabIndex = 3;
            // 
            // tbxBestObjectiveValue
            // 
            this.tbxBestObjectiveValue.Location = new System.Drawing.Point(12, 28);
            this.tbxBestObjectiveValue.Name = "tbxBestObjectiveValue";
            this.tbxBestObjectiveValue.Size = new System.Drawing.Size(132, 20);
            this.tbxBestObjectiveValue.TabIndex = 2;
            // 
            // labSoFarTheBestObjective
            // 
            this.labSoFarTheBestObjective.Location = new System.Drawing.Point(4, 1);
            this.labSoFarTheBestObjective.Name = "labSoFarTheBestObjective";
            this.labSoFarTheBestObjective.Size = new System.Drawing.Size(121, 28);
            this.labSoFarTheBestObjective.TabIndex = 0;
            this.labSoFarTheBestObjective.Text = "Best Objective Value";
            this.labSoFarTheBestObjective.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chtGA
            // 
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea2.AxisX.Title = "Iteration";
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea2.AxisY.Title = "Objective Value";
            chartArea2.Name = "ChartArea1";
            this.chtGA.ChartAreas.Add(chartArea2);
            this.chtGA.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend2.Name = "Legend1";
            this.chtGA.Legends.Add(legend2);
            this.chtGA.Location = new System.Drawing.Point(0, 0);
            this.chtGA.Name = "chtGA";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.Lime;
            series4.Legend = "Legend1";
            series4.LegendText = "Iteration Average";
            series4.Name = "Series1";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Color = System.Drawing.Color.Blue;
            series5.Legend = "Legend1";
            series5.LegendText = "Iteration Best";
            series5.Name = "Series2";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Color = System.Drawing.Color.Red;
            series6.Legend = "Legend1";
            series6.LegendText = "So Far The Best";
            series6.Name = "Series3";
            this.chtGA.Series.Add(series4);
            this.chtGA.Series.Add(series5);
            this.chtGA.Series.Add(series6);
            this.chtGA.Size = new System.Drawing.Size(466, 253);
            this.chtGA.TabIndex = 0;
            this.chtGA.Text = "chart1";
            // 
            // GAMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Controls.Add(this.splitContainer1);
            this.Name = "GAMonitor";
            this.Size = new System.Drawing.Size(702, 512);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chtGA)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.DataVisualization.Charting.Chart chtGA;
        private System.Windows.Forms.SplitContainer splitContainer2;
        public System.Windows.Forms.PropertyGrid prgGA;
        private System.Windows.Forms.SplitContainer splitContainer3;
        public System.Windows.Forms.Label labSoFarTheBestObjective;
        public System.Windows.Forms.TextBox tbxBestObjectiveValue;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.ListBox lbPopulation;
        public System.Windows.Forms.Button btnRunToEnd;
        public System.Windows.Forms.Button btnRunOneIteration;
        public System.Windows.Forms.Button btnReset;
        public System.Windows.Forms.ListBox lbBestSolution;
    }
}
