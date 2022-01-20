
namespace R10546020FLHuaFinalProject
{
    partial class EvaluationForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.nudIterationLimit = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudNumberOfRuns = new System.Windows.Forms.NumericUpDown();
            this.nudPopulationSize = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEvaluate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxGA = new System.Windows.Forms.CheckBox();
            this.cbxPSO = new System.Windows.Forms.CheckBox();
            this.cbxSFS = new System.Windows.Forms.CheckBox();
            this.cbxGWO = new System.Windows.Forms.CheckBox();
            this.cbxGWOSFS = new System.Windows.Forms.CheckBox();
            this.btnDeleteABenchmark = new System.Windows.Forms.Button();
            this.lbxBenchmarkTitle = new System.Windows.Forms.ListBox();
            this.btnSelectBenchmark = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chtConvergence = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnPlotConvergence = new System.Windows.Forms.Button();
            this.labGlobalBestObjective = new System.Windows.Forms.Label();
            this.cbxBenchmark = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvEvaluation = new System.Windows.Forms.DataGridView();
            this.Algorithm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Benchmark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mean = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Std = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reusltToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.summaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.labStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudIterationLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumberOfRuns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPopulationSize)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtConvergence)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEvaluation)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvEvaluation);
            this.splitContainer1.Size = new System.Drawing.Size(800, 525);
            this.splitContainer1.SplitterDistance = 437;
            this.splitContainer1.TabIndex = 1;
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
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Panel2.Controls.Add(this.labGlobalBestObjective);
            this.splitContainer2.Panel2.Controls.Add(this.cbxBenchmark);
            this.splitContainer2.Panel2.Controls.Add(this.label4);
            this.splitContainer2.Size = new System.Drawing.Size(437, 525);
            this.splitContainer2.SplitterDistance = 235;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer4.Panel1.Controls.Add(this.labStatus);
            this.splitContainer4.Panel1.Controls.Add(this.statusStrip1);
            this.splitContainer4.Panel1.Controls.Add(this.nudIterationLimit);
            this.splitContainer4.Panel1.Controls.Add(this.label3);
            this.splitContainer4.Panel1.Controls.Add(this.nudNumberOfRuns);
            this.splitContainer4.Panel1.Controls.Add(this.nudPopulationSize);
            this.splitContainer4.Panel1.Controls.Add(this.label2);
            this.splitContainer4.Panel1.Controls.Add(this.label1);
            this.splitContainer4.Panel1.Controls.Add(this.btnEvaluate);
            this.splitContainer4.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.btnDeleteABenchmark);
            this.splitContainer4.Panel2.Controls.Add(this.lbxBenchmarkTitle);
            this.splitContainer4.Panel2.Controls.Add(this.btnSelectBenchmark);
            this.splitContainer4.Size = new System.Drawing.Size(437, 235);
            this.splitContainer4.SplitterDistance = 233;
            this.splitContainer4.TabIndex = 0;
            // 
            // nudIterationLimit
            // 
            this.nudIterationLimit.Location = new System.Drawing.Point(11, 179);
            this.nudIterationLimit.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudIterationLimit.Name = "nudIterationLimit";
            this.nudIterationLimit.Size = new System.Drawing.Size(120, 20);
            this.nudIterationLimit.TabIndex = 7;
            this.nudIterationLimit.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Iteration Limit";
            // 
            // nudNumberOfRuns
            // 
            this.nudNumberOfRuns.Location = new System.Drawing.Point(11, 136);
            this.nudNumberOfRuns.Name = "nudNumberOfRuns";
            this.nudNumberOfRuns.Size = new System.Drawing.Size(120, 20);
            this.nudNumberOfRuns.TabIndex = 5;
            this.nudNumberOfRuns.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // nudPopulationSize
            // 
            this.nudPopulationSize.Location = new System.Drawing.Point(11, 94);
            this.nudPopulationSize.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudPopulationSize.Name = "nudPopulationSize";
            this.nudPopulationSize.Size = new System.Drawing.Size(120, 20);
            this.nudPopulationSize.TabIndex = 4;
            this.nudPopulationSize.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Number of Runs";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Population Size";
            // 
            // btnEvaluate
            // 
            this.btnEvaluate.Enabled = false;
            this.btnEvaluate.Location = new System.Drawing.Point(144, 178);
            this.btnEvaluate.Name = "btnEvaluate";
            this.btnEvaluate.Size = new System.Drawing.Size(75, 23);
            this.btnEvaluate.TabIndex = 1;
            this.btnEvaluate.Text = "Evaluate";
            this.btnEvaluate.UseVisualStyleBackColor = true;
            this.btnEvaluate.Click += new System.EventHandler(this.btnEvaluate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxGA);
            this.groupBox1.Controls.Add(this.cbxPSO);
            this.groupBox1.Controls.Add(this.cbxSFS);
            this.groupBox1.Controls.Add(this.cbxGWO);
            this.groupBox1.Controls.Add(this.cbxGWOSFS);
            this.groupBox1.Location = new System.Drawing.Point(11, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(217, 66);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Algorithm";
            // 
            // cbxGA
            // 
            this.cbxGA.AutoSize = true;
            this.cbxGA.Checked = true;
            this.cbxGA.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxGA.Location = new System.Drawing.Point(88, 42);
            this.cbxGA.Name = "cbxGA";
            this.cbxGA.Size = new System.Drawing.Size(41, 17);
            this.cbxGA.TabIndex = 4;
            this.cbxGA.Text = "GA";
            this.cbxGA.UseVisualStyleBackColor = true;
            // 
            // cbxPSO
            // 
            this.cbxPSO.AutoSize = true;
            this.cbxPSO.Checked = true;
            this.cbxPSO.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxPSO.Location = new System.Drawing.Point(6, 42);
            this.cbxPSO.Name = "cbxPSO";
            this.cbxPSO.Size = new System.Drawing.Size(48, 17);
            this.cbxPSO.TabIndex = 3;
            this.cbxPSO.Text = "PSO";
            this.cbxPSO.UseVisualStyleBackColor = true;
            // 
            // cbxSFS
            // 
            this.cbxSFS.AutoSize = true;
            this.cbxSFS.Checked = true;
            this.cbxSFS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxSFS.Location = new System.Drawing.Point(162, 18);
            this.cbxSFS.Name = "cbxSFS";
            this.cbxSFS.Size = new System.Drawing.Size(46, 17);
            this.cbxSFS.TabIndex = 2;
            this.cbxSFS.Text = "SFS";
            this.cbxSFS.UseVisualStyleBackColor = true;
            // 
            // cbxGWO
            // 
            this.cbxGWO.AutoSize = true;
            this.cbxGWO.Checked = true;
            this.cbxGWO.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxGWO.Location = new System.Drawing.Point(88, 18);
            this.cbxGWO.Name = "cbxGWO";
            this.cbxGWO.Size = new System.Drawing.Size(53, 17);
            this.cbxGWO.TabIndex = 1;
            this.cbxGWO.Text = "GWO";
            this.cbxGWO.UseVisualStyleBackColor = true;
            // 
            // cbxGWOSFS
            // 
            this.cbxGWOSFS.AutoSize = true;
            this.cbxGWOSFS.Checked = true;
            this.cbxGWOSFS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxGWOSFS.Location = new System.Drawing.Point(6, 19);
            this.cbxGWOSFS.Name = "cbxGWOSFS";
            this.cbxGWOSFS.Size = new System.Drawing.Size(76, 17);
            this.cbxGWOSFS.TabIndex = 0;
            this.cbxGWOSFS.Text = "GWO-SFS";
            this.cbxGWOSFS.UseVisualStyleBackColor = true;
            // 
            // btnDeleteABenchmark
            // 
            this.btnDeleteABenchmark.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteABenchmark.Enabled = false;
            this.btnDeleteABenchmark.Location = new System.Drawing.Point(-1, 24);
            this.btnDeleteABenchmark.Name = "btnDeleteABenchmark";
            this.btnDeleteABenchmark.Size = new System.Drawing.Size(202, 26);
            this.btnDeleteABenchmark.TabIndex = 2;
            this.btnDeleteABenchmark.Text = "Delete A Benchmark";
            this.btnDeleteABenchmark.UseVisualStyleBackColor = true;
            this.btnDeleteABenchmark.Click += new System.EventHandler(this.btnDeleteABenchmark_Click);
            // 
            // lbxBenchmarkTitle
            // 
            this.lbxBenchmarkTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxBenchmarkTitle.FormattingEnabled = true;
            this.lbxBenchmarkTitle.Location = new System.Drawing.Point(-1, 48);
            this.lbxBenchmarkTitle.Name = "lbxBenchmarkTitle";
            this.lbxBenchmarkTitle.Size = new System.Drawing.Size(201, 186);
            this.lbxBenchmarkTitle.TabIndex = 1;
            // 
            // btnSelectBenchmark
            // 
            this.btnSelectBenchmark.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectBenchmark.Location = new System.Drawing.Point(-3, -1);
            this.btnSelectBenchmark.Name = "btnSelectBenchmark";
            this.btnSelectBenchmark.Size = new System.Drawing.Size(202, 26);
            this.btnSelectBenchmark.TabIndex = 0;
            this.btnSelectBenchmark.Text = "Select Benchmark";
            this.btnSelectBenchmark.UseVisualStyleBackColor = true;
            this.btnSelectBenchmark.Click += new System.EventHandler(this.btnSelectBenchmark_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chtConvergence);
            this.groupBox2.Controls.Add(this.btnPlotConvergence);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 34);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(434, 249);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Comparison for One Run";
            // 
            // chtConvergence
            // 
            this.chtConvergence.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.Title = "Iteration";
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.Title = "Objective Value";
            chartArea1.Name = "ChartArea1";
            this.chtConvergence.ChartAreas.Add(chartArea1);
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Name = "Legend1";
            this.chtConvergence.Legends.Add(legend1);
            this.chtConvergence.Location = new System.Drawing.Point(64, 19);
            this.chtConvergence.Name = "chtConvergence";
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.Name = "GWOSFS";
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.IsVisibleInLegend = false;
            series2.Legend = "Legend1";
            series2.Name = "GWO";
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.IsVisibleInLegend = false;
            series3.Legend = "Legend1";
            series3.Name = "SFS";
            series4.BorderWidth = 2;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.IsVisibleInLegend = false;
            series4.Legend = "Legend1";
            series4.Name = "PSO";
            series5.BorderWidth = 2;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.IsVisibleInLegend = false;
            series5.Legend = "Legend1";
            series5.Name = "GA";
            this.chtConvergence.Series.Add(series1);
            this.chtConvergence.Series.Add(series2);
            this.chtConvergence.Series.Add(series3);
            this.chtConvergence.Series.Add(series4);
            this.chtConvergence.Series.Add(series5);
            this.chtConvergence.Size = new System.Drawing.Size(371, 230);
            this.chtConvergence.TabIndex = 0;
            this.chtConvergence.Text = "chart1";
            title1.BackColor = System.Drawing.Color.White;
            title1.BorderColor = System.Drawing.Color.Transparent;
            title1.Name = "Convergence Curve for Each Algorithm";
            title1.Text = "Convergence Curve for Each Algorithm";
            this.chtConvergence.Titles.Add(title1);
            // 
            // btnPlotConvergence
            // 
            this.btnPlotConvergence.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnPlotConvergence.Location = new System.Drawing.Point(6, 28);
            this.btnPlotConvergence.Name = "btnPlotConvergence";
            this.btnPlotConvergence.Size = new System.Drawing.Size(52, 23);
            this.btnPlotConvergence.TabIndex = 4;
            this.btnPlotConvergence.Text = "Plot";
            this.btnPlotConvergence.UseVisualStyleBackColor = true;
            this.btnPlotConvergence.Click += new System.EventHandler(this.btnPlotConvergence_Click);
            // 
            // labGlobalBestObjective
            // 
            this.labGlobalBestObjective.AutoSize = true;
            this.labGlobalBestObjective.Location = new System.Drawing.Point(232, 11);
            this.labGlobalBestObjective.Name = "labGlobalBestObjective";
            this.labGlobalBestObjective.Size = new System.Drawing.Size(81, 13);
            this.labGlobalBestObjective.TabIndex = 3;
            this.labGlobalBestObjective.Text = "Global Optimum";
            // 
            // cbxBenchmark
            // 
            this.cbxBenchmark.BackColor = System.Drawing.SystemColors.MenuBar;
            this.cbxBenchmark.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cbxBenchmark.FormattingEnabled = true;
            this.cbxBenchmark.Location = new System.Drawing.Point(76, 7);
            this.cbxBenchmark.Name = "cbxBenchmark";
            this.cbxBenchmark.Size = new System.Drawing.Size(121, 21);
            this.cbxBenchmark.TabIndex = 2;
            this.cbxBenchmark.SelectedIndexChanged += new System.EventHandler(this.cbxBenchmark_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Benchmark";
            // 
            // dgvEvaluation
            // 
            this.dgvEvaluation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEvaluation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Algorithm,
            this.Benchmark,
            this.Dim,
            this.Mean,
            this.Std});
            this.dgvEvaluation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEvaluation.Location = new System.Drawing.Point(0, 0);
            this.dgvEvaluation.Name = "dgvEvaluation";
            this.dgvEvaluation.ReadOnly = true;
            this.dgvEvaluation.RowHeadersVisible = false;
            this.dgvEvaluation.Size = new System.Drawing.Size(359, 525);
            this.dgvEvaluation.TabIndex = 0;
            // 
            // Algorithm
            // 
            this.Algorithm.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Algorithm.HeaderText = "Algorithm";
            this.Algorithm.Name = "Algorithm";
            this.Algorithm.ReadOnly = true;
            this.Algorithm.Width = 75;
            // 
            // Benchmark
            // 
            this.Benchmark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Benchmark.HeaderText = "Benchmark";
            this.Benchmark.Name = "Benchmark";
            this.Benchmark.ReadOnly = true;
            this.Benchmark.Width = 86;
            // 
            // Dim
            // 
            this.Dim.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Dim.HeaderText = "Dim";
            this.Dim.Name = "Dim";
            this.Dim.ReadOnly = true;
            this.Dim.Width = 50;
            // 
            // Mean
            // 
            this.Mean.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Mean.HeaderText = "Mean";
            this.Mean.Name = "Mean";
            this.Mean.ReadOnly = true;
            this.Mean.Width = 59;
            // 
            // Std
            // 
            this.Std.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Std.HeaderText = "Std.";
            this.Std.Name = "Std";
            this.Std.ReadOnly = true;
            this.Std.Width = 51;
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tableToolStripMenuItem,
            this.chartToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // tableToolStripMenuItem
            // 
            this.tableToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reusltToolStripMenuItem,
            this.summaryToolStripMenuItem});
            this.tableToolStripMenuItem.Name = "tableToolStripMenuItem";
            this.tableToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.tableToolStripMenuItem.Text = "Table";
            // 
            // reusltToolStripMenuItem
            // 
            this.reusltToolStripMenuItem.Name = "reusltToolStripMenuItem";
            this.reusltToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.reusltToolStripMenuItem.Text = "Reuslt";
            this.reusltToolStripMenuItem.Click += new System.EventHandler(this.reusltToolStripMenuItem_Click);
            // 
            // summaryToolStripMenuItem
            // 
            this.summaryToolStripMenuItem.Name = "summaryToolStripMenuItem";
            this.summaryToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.summaryToolStripMenuItem.Text = "Summary";
            // 
            // chartToolStripMenuItem
            // 
            this.chartToolStripMenuItem.Name = "chartToolStripMenuItem";
            this.chartToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.chartToolStripMenuItem.Text = "Chart";
            this.chartToolStripMenuItem.Click += new System.EventHandler(this.chartToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(5, 209);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(147, 25);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.AutoSize = false;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(140, 19);
            // 
            // labStatus
            // 
            this.labStatus.AutoSize = true;
            this.labStatus.Location = new System.Drawing.Point(155, 214);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(16, 13);
            this.labStatus.TabIndex = 9;
            this.labStatus.Text = "...";
            // 
            // EvaluationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 549);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "EvaluationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EvaluationForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudIterationLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumberOfRuns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPopulationSize)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chtConvergence)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEvaluation)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbxGWOSFS;
        private System.Windows.Forms.CheckBox cbxPSO;
        private System.Windows.Forms.CheckBox cbxSFS;
        private System.Windows.Forms.CheckBox cbxGWO;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.CheckBox cbxGA;
        private System.Windows.Forms.Button btnSelectBenchmark;
        private System.Windows.Forms.ListBox lbxBenchmarkTitle;
        private System.Windows.Forms.NumericUpDown nudIterationLimit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudNumberOfRuns;
        private System.Windows.Forms.NumericUpDown nudPopulationSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEvaluate;
        private System.Windows.Forms.DataGridView dgvEvaluation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Algorithm;
        private System.Windows.Forms.DataGridViewTextBoxColumn Benchmark;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dim;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mean;
        private System.Windows.Forms.DataGridViewTextBoxColumn Std;
        private System.Windows.Forms.Button btnDeleteABenchmark;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxBenchmark;
        private System.Windows.Forms.Label labGlobalBestObjective;
        private System.Windows.Forms.Button btnPlotConvergence;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtConvergence;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reusltToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem summaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chartToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Label labStatus;
    }
}