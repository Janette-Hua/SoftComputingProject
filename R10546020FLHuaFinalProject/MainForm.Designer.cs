
namespace R10546020FLHuaFinalProject
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.chtObjective = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.rtbSoFarTheBestSolution = new System.Windows.Forms.RichTextBox();
            this.labElapsedTime = new System.Windows.Forms.Label();
            this.labSoFarTheBestObjValue = new System.Windows.Forms.Label();
            this.rbtnSFS = new System.Windows.Forms.RadioButton();
            this.rbtnGWO = new System.Windows.Forms.RadioButton();
            this.rbtnGWOSFS = new System.Windows.Forms.RadioButton();
            this.rbtnGA = new System.Windows.Forms.RadioButton();
            this.rbtnPSO = new System.Windows.Forms.RadioButton();
            this.btnRunToEnd = new System.Windows.Forms.Button();
            this.btnRunOneIteration = new System.Windows.Forms.Button();
            this.btnCreatePSOSolver = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.prgSolver = new System.Windows.Forms.PropertyGrid();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtObjective)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOpen,
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(896, 39);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbOpen
            // 
            this.tsbOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpen.Image")));
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(146, 36);
            this.tsbOpen.Text = "Open A Benchmark";
            this.tsbOpen.Click += new System.EventHandler(this.tsbOpen_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(151, 36);
            this.toolStripButton1.Text = "Add A New Problem";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(87, 36);
            this.toolStripButton2.Text = "Evaluate";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(12, 316);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(220, 25);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.AutoSize = false;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(220, 19);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 39);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(896, 615);
            this.splitContainer1.SplitterDistance = 219;
            this.splitContainer1.TabIndex = 3;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.rtbSoFarTheBestSolution);
            this.splitContainer3.Panel2.Controls.Add(this.labElapsedTime);
            this.splitContainer3.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer3.Panel2.Controls.Add(this.labSoFarTheBestObjValue);
            this.splitContainer3.Panel2.Controls.Add(this.rbtnSFS);
            this.splitContainer3.Panel2.Controls.Add(this.rbtnGWO);
            this.splitContainer3.Panel2.Controls.Add(this.rbtnGWOSFS);
            this.splitContainer3.Panel2.Controls.Add(this.rbtnGA);
            this.splitContainer3.Panel2.Controls.Add(this.rbtnPSO);
            this.splitContainer3.Panel2.Controls.Add(this.btnRunToEnd);
            this.splitContainer3.Panel2.Controls.Add(this.btnRunOneIteration);
            this.splitContainer3.Panel2.Controls.Add(this.btnCreatePSOSolver);
            this.splitContainer3.Panel2.Controls.Add(this.btnReset);
            this.splitContainer3.Panel2.Controls.Add(this.prgSolver);
            this.splitContainer3.Size = new System.Drawing.Size(673, 615);
            this.splitContainer3.SplitterDistance = 418;
            this.splitContainer3.TabIndex = 0;
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
            this.splitContainer2.Panel1.Controls.Add(this.chtObjective);
            this.splitContainer2.Size = new System.Drawing.Size(418, 615);
            this.splitContainer2.SplitterDistance = 248;
            this.splitContainer2.TabIndex = 0;
            // 
            // chtObjective
            // 
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.Title = "Iteration";
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.Title = "Objective Value";
            chartArea1.Name = "ChartArea1";
            this.chtObjective.ChartAreas.Add(chartArea1);
            this.chtObjective.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Name = "Legend1";
            this.chtObjective.Legends.Add(legend1);
            this.chtObjective.Location = new System.Drawing.Point(0, 0);
            this.chtObjective.Name = "chtObjective";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            series1.Legend = "Legend1";
            series1.Name = "Iteration Average";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.PowderBlue;
            series2.Legend = "Legend1";
            series2.Name = "Iteration Best";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.Red;
            series3.Legend = "Legend1";
            series3.Name = "So Far The Best";
            this.chtObjective.Series.Add(series1);
            this.chtObjective.Series.Add(series2);
            this.chtObjective.Series.Add(series3);
            this.chtObjective.Size = new System.Drawing.Size(418, 248);
            this.chtObjective.TabIndex = 0;
            this.chtObjective.Text = "chart1";
            // 
            // rtbSoFarTheBestSolution
            // 
            this.rtbSoFarTheBestSolution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbSoFarTheBestSolution.ForeColor = System.Drawing.Color.SteelBlue;
            this.rtbSoFarTheBestSolution.Location = new System.Drawing.Point(12, 212);
            this.rtbSoFarTheBestSolution.Name = "rtbSoFarTheBestSolution";
            this.rtbSoFarTheBestSolution.Size = new System.Drawing.Size(227, 70);
            this.rtbSoFarTheBestSolution.TabIndex = 12;
            this.rtbSoFarTheBestSolution.Text = "So Far the Best Solution: ";
            // 
            // labElapsedTime
            // 
            this.labElapsedTime.AutoSize = true;
            this.labElapsedTime.Location = new System.Drawing.Point(9, 296);
            this.labElapsedTime.Name = "labElapsedTime";
            this.labElapsedTime.Size = new System.Drawing.Size(53, 13);
            this.labElapsedTime.TabIndex = 11;
            this.labElapsedTime.Text = "Run Time";
            // 
            // labSoFarTheBestObjValue
            // 
            this.labSoFarTheBestObjValue.AutoSize = true;
            this.labSoFarTheBestObjValue.ForeColor = System.Drawing.Color.DarkRed;
            this.labSoFarTheBestObjValue.Location = new System.Drawing.Point(9, 193);
            this.labSoFarTheBestObjValue.Name = "labSoFarTheBestObjValue";
            this.labSoFarTheBestObjValue.Size = new System.Drawing.Size(161, 13);
            this.labSoFarTheBestObjValue.TabIndex = 10;
            this.labSoFarTheBestObjValue.Text = "So Far the Best Objective Value:";
            // 
            // rbtnSFS
            // 
            this.rbtnSFS.AutoSize = true;
            this.rbtnSFS.Location = new System.Drawing.Point(151, 13);
            this.rbtnSFS.Name = "rbtnSFS";
            this.rbtnSFS.Size = new System.Drawing.Size(45, 17);
            this.rbtnSFS.TabIndex = 9;
            this.rbtnSFS.Text = "SFS";
            this.rbtnSFS.UseVisualStyleBackColor = true;
            // 
            // rbtnGWO
            // 
            this.rbtnGWO.AutoSize = true;
            this.rbtnGWO.Location = new System.Drawing.Point(93, 13);
            this.rbtnGWO.Name = "rbtnGWO";
            this.rbtnGWO.Size = new System.Drawing.Size(52, 17);
            this.rbtnGWO.TabIndex = 8;
            this.rbtnGWO.Text = "GWO";
            this.rbtnGWO.UseVisualStyleBackColor = true;
            // 
            // rbtnGWOSFS
            // 
            this.rbtnGWOSFS.AutoSize = true;
            this.rbtnGWOSFS.Checked = true;
            this.rbtnGWOSFS.Location = new System.Drawing.Point(12, 13);
            this.rbtnGWOSFS.Name = "rbtnGWOSFS";
            this.rbtnGWOSFS.Size = new System.Drawing.Size(75, 17);
            this.rbtnGWOSFS.TabIndex = 7;
            this.rbtnGWOSFS.TabStop = true;
            this.rbtnGWOSFS.Text = "GWO-SFS";
            this.rbtnGWOSFS.UseVisualStyleBackColor = true;
            // 
            // rbtnGA
            // 
            this.rbtnGA.AutoSize = true;
            this.rbtnGA.Location = new System.Drawing.Point(65, 36);
            this.rbtnGA.Name = "rbtnGA";
            this.rbtnGA.Size = new System.Drawing.Size(40, 17);
            this.rbtnGA.TabIndex = 6;
            this.rbtnGA.Text = "GA";
            this.rbtnGA.UseVisualStyleBackColor = true;
            // 
            // rbtnPSO
            // 
            this.rbtnPSO.AutoSize = true;
            this.rbtnPSO.Location = new System.Drawing.Point(12, 36);
            this.rbtnPSO.Name = "rbtnPSO";
            this.rbtnPSO.Size = new System.Drawing.Size(47, 17);
            this.rbtnPSO.TabIndex = 5;
            this.rbtnPSO.Text = "PSO";
            this.rbtnPSO.UseVisualStyleBackColor = true;
            // 
            // btnRunToEnd
            // 
            this.btnRunToEnd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunToEnd.Enabled = false;
            this.btnRunToEnd.Location = new System.Drawing.Point(40, 157);
            this.btnRunToEnd.Name = "btnRunToEnd";
            this.btnRunToEnd.Size = new System.Drawing.Size(171, 28);
            this.btnRunToEnd.TabIndex = 4;
            this.btnRunToEnd.Text = "Run To End";
            this.btnRunToEnd.UseVisualStyleBackColor = true;
            this.btnRunToEnd.Click += new System.EventHandler(this.btnRunToEnd_Click);
            // 
            // btnRunOneIteration
            // 
            this.btnRunOneIteration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunOneIteration.Enabled = false;
            this.btnRunOneIteration.Location = new System.Drawing.Point(40, 122);
            this.btnRunOneIteration.Name = "btnRunOneIteration";
            this.btnRunOneIteration.Size = new System.Drawing.Size(171, 29);
            this.btnRunOneIteration.TabIndex = 1;
            this.btnRunOneIteration.Text = "Run One Iteration";
            this.btnRunOneIteration.UseVisualStyleBackColor = true;
            this.btnRunOneIteration.Click += new System.EventHandler(this.btnRunOneIteration_Click);
            // 
            // btnCreatePSOSolver
            // 
            this.btnCreatePSOSolver.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreatePSOSolver.Enabled = false;
            this.btnCreatePSOSolver.Location = new System.Drawing.Point(40, 60);
            this.btnCreatePSOSolver.Name = "btnCreatePSOSolver";
            this.btnCreatePSOSolver.Size = new System.Drawing.Size(171, 26);
            this.btnCreatePSOSolver.TabIndex = 2;
            this.btnCreatePSOSolver.Text = "Create A Solver";
            this.btnCreatePSOSolver.UseVisualStyleBackColor = true;
            this.btnCreatePSOSolver.Click += new System.EventHandler(this.btnCreatePSOSolver_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Enabled = false;
            this.btnReset.Location = new System.Drawing.Point(40, 92);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(171, 24);
            this.btnReset.TabIndex = 0;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // prgSolver
            // 
            this.prgSolver.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgSolver.Location = new System.Drawing.Point(12, 350);
            this.prgSolver.Name = "prgSolver";
            this.prgSolver.Size = new System.Drawing.Size(227, 262);
            this.prgSolver.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 654);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Continuous Optimization Solver (GWO-SFS, GWO, SFS, PSO, GA)";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chtObjective)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PropertyGrid prgSolver;
        private System.Windows.Forms.Button btnCreatePSOSolver;
        private System.Windows.Forms.Button btnRunOneIteration;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button btnRunToEnd;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtObjective;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.RadioButton rbtnGA;
        private System.Windows.Forms.RadioButton rbtnPSO;
        private System.Windows.Forms.RadioButton rbtnSFS;
        private System.Windows.Forms.RadioButton rbtnGWO;
        private System.Windows.Forms.RadioButton rbtnGWOSFS;
        private System.Windows.Forms.Label labSoFarTheBestObjValue;
        private System.Windows.Forms.Label labElapsedTime;
        private System.Windows.Forms.RichTextBox rtbSoFarTheBestSolution;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
    }
}

