using COP;
using FLHuaGALibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace R10546020FLHuaFinalProject
{
    public partial class EvaluationForm : Form
    {
        public EvaluationForm()
        {
            InitializeComponent();
            OnIterationCompleted += EvaluationForm_OnIterationCompleted;
        }

        List<COPBenchmark> myProblemList;
        COPBenchmark myProblem;
        StringBuilder sb;

        MetaheuristicAlgorithmType algorithmType = MetaheuristicAlgorithmType.GWOSFS;
        ParticleSwarmOptimizer myPSOsolver;
        RealNumberEncodedGA myGAsolver;
        StochasticFractalSearch mySFSsolver;
        GreyWolfOptimizer myGWOsolver;
        GreyWolfOptimizerBasedOnSFS myGWOSFSsolver;

        double mean = 0;
        double StDev = 0;
        double sum = 0;
        int counter = 0;
        int run = 1;

        public event EventHandler OnIterationCompleted; // on initialization, EventHandler is "null"



        private void btnSelectBenchmark_Click(object sender, EventArgs e)
        {
            //myProblem = COPBenchmark.LoadAProblemFromAFile();
            myProblem = new COPBenchmark();
            myProblemList = new List<COPBenchmark>();
            sb = new StringBuilder();

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;  //Equal to true means that multiple files can be selected

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string folderPath = Path.GetDirectoryName(dlg.FileName);

                foreach (string file in dlg.FileNames)
                {
                    
                    StreamReader sr = new StreamReader(file);
                    string line;
                    string[] items;
                    //while ((line = sr.ReadLine()) != null)
                    //{
                    //    //Add here the code that needs to process each line of data in the file

                    //}

                    items = sr.ReadLine().Split(':');
                    //myProblem.Title = items[1].Trim();
                    lbxBenchmarkTitle.Items.Add(items[1].Trim() + ".cop");
                    cbxBenchmark.Items.Add(items[1].Trim() + ".cop");
                    //myProblem.FilePath = dlg.FileName;

                    
                    string filepath = Path.GetDirectoryName(dlg.FileName) + "\\" + items[1].Trim() + ".cop";

                    myProblem = COPBenchmark.LoadAProblemFromAFile(filepath);


                    //myProblem = COPBenchmark.LoadAProblemFromAFile(@"C:\Users\zixin\Desktop\R10546020FLHuaFinalProject\COP\Ackley(2).cop");

                    //items = sr.ReadLine().Split(':');
                    //if (items[1].Trim() == "Minimization") myProblem.OptimizationGoal = COP.OptimizationType.Minimization;
                    //else if (items[1].Trim() == "Maximization") myProblem.OptimizationGoal = COP.OptimizationType.Maximization;

                    //items = sr.ReadLine().Split(':');
                    //myProblem.Dimension = Convert.ToInt32(items[1].Trim());
                    //myProblem.LowerBound = new double[myProblem.Dimension];
                    //myProblem.UpperBound = new double[myProblem.Dimension];

                    //line = sr.ReadLine();

                    //for(int i = 0; i < myProblem.Dimension; i++)
                    //{
                    //    items = sr.ReadLine().Split(' ');
                    //    myProblem.LowerBound[i] = Convert.ToDouble(items[0]);
                    //    myProblem.UpperBound[i] = Convert.ToDouble(items[1]);
                    //}

                    //line = sr.ReadLine();
                    ////if (Convert.ToInt32(line.Split(':')) > 0) myProblem.OptimalSolutionsKnown = true;
                    //line = sr.ReadLine();
                    //items = sr.ReadLine().Split(':');
                    //myProblem.BestObjectiveValue = Convert.ToDouble(items[1]);

                    //for (int i = 0; i < 7; i++) line = sr.ReadLine();

                    //while (line != "GradientFunction: ")
                    //{
                    //    line = sr.ReadLine();
                    //    if (String.Equals(line, "GradientFunction: ")) break;
                    //    sb.Append(line);
                    //    sb.Append("\n");
                    //}

                    //sb.Remove(sb.Length - 1, 1);
                    //myProblem.ObjectiveEvaluationCode = sb.ToString();

                    myProblemList.Add(myProblem);

                    sr.Close();
                }

            }

            btnEvaluate.Enabled = btnDeleteABenchmark.Enabled = true;
            cbxBenchmark.SelectedIndex = cbxBenchmark.FindStringExact(lbxBenchmarkTitle.Items[0].ToString());
        }

        private void btnEvaluate_Click(object sender, EventArgs e)
        {
            //double min = double.MaxValue;
            //double max = double.MinValue;
            //double sum = 0;

            dgvEvaluation.Rows.Clear();

            double[] soFarTheBestObjectiveForEachRun = new double[(int)nudNumberOfRuns.Value];

            counter = 0;
            if (cbxGA.Checked) counter++;
            if (cbxGWO.Checked) counter++;
            if (cbxGWOSFS.Checked) counter++;
            if (cbxPSO.Checked) counter++;
            if (cbxSFS.Checked) counter++;

            for (int i = 0; i < counter * lbxBenchmarkTitle.Items.Count; i++) dgvEvaluation.Rows.Add();
            cbxBenchmark.SelectedIndex = cbxBenchmark.FindStringExact(lbxBenchmarkTitle.Items[0].ToString());
            toolStripProgressBar1.Maximum = lbxBenchmarkTitle.Items.Count;

            counter = 0;
            run = 1;

            foreach (COPBenchmark benchmark in myProblemList)
            {

                if (cbxGWOSFS.Checked)
                {
                    myGWOSFSsolver = new GreyWolfOptimizerBasedOnSFS(benchmark);

                    sum = 0;
                    for(int i = 0; i < nudNumberOfRuns.Value; i++)
                    {
                        myGWOSFSsolver.PopulationSize = Convert.ToInt32(nudPopulationSize.Value);
                        myGWOSFSsolver.Reset();
                        
                        while (myGWOSFSsolver.IterationID < nudIterationLimit.Value) myGWOSFSsolver.RunOneIteration();

                        sum += myGWOSFSsolver.SoFarTheBestObjectiveValue;
                        soFarTheBestObjectiveForEachRun[i] = myGWOSFSsolver.SoFarTheBestObjectiveValue;
                    }

                    // Calculate mean & StDev
                    GetMeanAndstd(sum, soFarTheBestObjectiveForEachRun);                  


                    // Update the results on dgvEvaluation
                    UpdateDGV("GWO-SFS", benchmark, mean, StDev);

                }
                if (cbxGWO.Checked)
                {
                    myGWOsolver = new GreyWolfOptimizer(benchmark);
                    sum = 0;
                    for (int i = 0; i < nudNumberOfRuns.Value; i++)
                    {
                        myGWOsolver.PopulationSize = Convert.ToInt32(nudPopulationSize.Value);
                        myGWOsolver.Reset();
                        while (myGWOsolver.IterationID < nudIterationLimit.Value) myGWOsolver.RunOneIteration();
                            

                        sum += myGWOsolver.SoFarTheBestObjectiveValue;
                        soFarTheBestObjectiveForEachRun[i] = myGWOsolver.SoFarTheBestObjectiveValue;
                    }

                    // Calculate mean & StDev
                    GetMeanAndstd(sum, soFarTheBestObjectiveForEachRun);

                    // Update the results on dgvEvaluation
                    UpdateDGV("GWO", benchmark, mean, StDev);

                }
                if (cbxSFS.Checked)
                {
                    mySFSsolver = new StochasticFractalSearch(benchmark);

                    sum = 0;
                    for (int i = 0; i < nudNumberOfRuns.Value; i++)
                    {
                        mySFSsolver.NumberOfParticles = Convert.ToInt32(nudPopulationSize.Value);
                        mySFSsolver.Reset();
                        while (mySFSsolver.IterationID < nudIterationLimit.Value) mySFSsolver.RunOneIteration();

                        sum += mySFSsolver.SoFarTheBestObjectiveValue;
                        soFarTheBestObjectiveForEachRun[i] = mySFSsolver.SoFarTheBestObjectiveValue;
                    }

                    // Calculate mean & StDev
                    GetMeanAndstd(sum, soFarTheBestObjectiveForEachRun);


                    // Update the results on dgvEvaluation
                    UpdateDGV("SFS", benchmark, mean, StDev);
                }
                    
                if (cbxPSO.Checked)
                {
                    myPSOsolver = new ParticleSwarmOptimizer(benchmark);

                    sum = 0;
                    for (int i = 0; i < nudNumberOfRuns.Value; i++)
                    {
                        myPSOsolver.NumberOfParticles = Convert.ToInt32(nudPopulationSize.Value);
                        myPSOsolver.Reset();
                        while (myPSOsolver.IterationID < nudIterationLimit.Value) myPSOsolver.RunOneIteration();

                        sum += myPSOsolver.SoFarTheBestObjectiveValue;
                        soFarTheBestObjectiveForEachRun[i] = myPSOsolver.SoFarTheBestObjectiveValue;
                    }

                    // Calculate mean & StDev
                    GetMeanAndstd(sum, soFarTheBestObjectiveForEachRun);


                    // Update the results on dgvEvaluation
                    UpdateDGV("PSO", benchmark, mean, StDev);

                }

                if (cbxGA.Checked)
                {
                    if (benchmark.OptimizationGoal == COP.OptimizationType.Minimization)
                    {
                        myGAsolver = new RealNumberEncodedGA(benchmark.Dimension, benchmark.LowerBound, benchmark.UpperBound, FLHuaGALibrary.OptimizationType.Minimization, benchmark.GetObjectiveValue);
                    }
                    else
                    {
                        myGAsolver = new RealNumberEncodedGA(benchmark.Dimension, benchmark.LowerBound, benchmark.UpperBound, FLHuaGALibrary.OptimizationType.Maximization, benchmark.GetObjectiveValue);
                    }

                    sum = 0;
                    for (int i = 0; i < nudNumberOfRuns.Value; i++)
                    {
                        myGAsolver.PopulationSize = Convert.ToInt32(nudPopulationSize.Value);
                        myGAsolver.Reset();
                        while (myGAsolver.IterationID < nudIterationLimit.Value) myGAsolver.RunOneIteration();

                        sum += myGAsolver.SoFarTheBestObjectiveValue;
                        soFarTheBestObjectiveForEachRun[i] = myGAsolver.SoFarTheBestObjectiveValue;
                    }

                    // Calculate mean & StDev
                    GetMeanAndstd(sum, soFarTheBestObjectiveForEachRun);


                    // Update the results on dgvEvaluation
                    UpdateDGV("GA", benchmark, mean, StDev);
                }

                // Fire  OneIterationCompleted  event
                if (OnIterationCompleted != null) OnIterationCompleted(this, null); // this 代表訂閱這個 event 的物件
            }

            
            if (myProblemList[0].OptimizationGoal == COP.OptimizationType.Minimization) labGlobalBestObjective.Text = $"Global Minimum: {myProblemList[0].BestObjectiveValue}";
            if (myProblemList[0].OptimizationGoal == COP.OptimizationType.Maximization) labGlobalBestObjective.Text = $"Global Maximum: {myProblemList[0].BestObjectiveValue}";

        }

        private void EvaluationForm_OnIterationCompleted(object sender, EventArgs e)
        {
            toolStripProgressBar1.Value = run;
            labStatus.Text = $"{run}/{toolStripProgressBar1.Maximum}";
            statusStrip1.Update();
            labStatus.Update();
            run++;
        }

        private void GetMeanAndstd(double sum, double[] soFarTheBestObjectiveList)
        {
            mean = sum / (double)nudNumberOfRuns.Value;
            
            double temp = 0;
            for (int i = 0; i < nudNumberOfRuns.Value; i++)
            {
                temp += (soFarTheBestObjectiveList[i] - mean) * (soFarTheBestObjectiveList[i] - mean);
            }
            StDev = Math.Sqrt(temp / (double)nudNumberOfRuns.Value);
        }

        private void UpdateDGV(string algo, COPBenchmark benchmark, double mean, double stDev)
        {
            dgvEvaluation.Rows[counter].Cells[0].Value = algo;                       // algo
            dgvEvaluation.Rows[counter].Cells[1].Value = benchmark.Title + ".cop";   // benchmark
            dgvEvaluation.Rows[counter].Cells[2].Value = benchmark.Dimension;        // dim
            dgvEvaluation.Rows[counter].Cells[3].Value = (mean).ToString("E5");      // mean
            dgvEvaluation.Rows[counter].Cells[4].Value = $"{stDev:0.00}";            // standard deviation

            if(algo == "GWO-SFS") dgvEvaluation.Rows[counter].DefaultCellStyle.BackColor = Color.PaleTurquoise;

            counter++;

            //if (counter == 0)
            //{
            //    dgvEvaluation.Rows[0].Cells[0].Value = algo;                       // algo
            //    dgvEvaluation.Rows[0].Cells[1].Value = benchmark.Title + ".cop";   // benchmark
            //    dgvEvaluation.Rows[0].Cells[2].Value = benchmark.Dimension;        // dim
            //    dgvEvaluation.Rows[0].Cells[3].Value = (mean).ToString("E5");      // mean
            //    dgvEvaluation.Rows[0].Cells[4].Value = $"{stDev}.00";              // standard deviation

            //    counter++;
            //}
            //else
            //{
            //    int dgvRowID = dgvEvaluation.Rows.Count;
            //    dgvEvaluation.Rows.Add();
            //    DataGridViewRow row = (DataGridViewRow)dgvEvaluation.Rows[0].Clone();
            //    row.Cells[0].Value = algo;
            //    row.Cells[1].Value = benchmark.Title + ".cop";
            //    row.Cells[2].Value = benchmark.Dimension;        // dim
            //    row.Cells[3].Value = (mean).ToString("E5");      // mean
            //    row.Cells[4].Value = $"{stDev}.00";              // standard deviation              
            //    dgvEvaluation.Rows.Insert(0, row);

            //int dgvRowID = dgvEvaluation.Rows.Count - 1;
            //dgvEvaluation.Rows.Insert(dgvRowID, new DataGridViewRow());
            //dgvEvaluation.Rows[dgvRowID].Cells[0].Value = algo;                       // algo
            //dgvEvaluation.Rows[dgvRowID].Cells[1].Value = benchmark.Title + ".cop";   // benchmark
            //dgvEvaluation.Rows[dgvRowID].Cells[2].Value = benchmark.Dimension;        // dim
            //dgvEvaluation.Rows[dgvRowID].Cells[3].Value = (mean).ToString("E5");      // mean
            //dgvEvaluation.Rows[dgvRowID].Cells[4].Value = $"{stDev}.00";              // standard deviation

        }

        private void btnDeleteABenchmark_Click(object sender, EventArgs e)
        {
            int index = lbxBenchmarkTitle.SelectedIndex;
            lbxBenchmarkTitle.Items.RemoveAt(index);
            cbxBenchmark.Items.RemoveAt(index);

            if (lbxBenchmarkTitle.Items.Count == 0)
            {
                btnDeleteABenchmark.Enabled = false;
                cbxBenchmark.Items.Clear();
                cbxBenchmark.Text = "- select a benchmark -";
            }

            myProblemList.Clear();
        }

        private void cbxBenchmark_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (myProblemList[cbxBenchmark.SelectedIndex].OptimizationGoal == COP.OptimizationType.Minimization) labGlobalBestObjective.Text = $"Global Minimum: {myProblemList[cbxBenchmark.SelectedIndex].BestObjectiveValue}";
            if (myProblemList[cbxBenchmark.SelectedIndex].OptimizationGoal == COP.OptimizationType.Maximization) labGlobalBestObjective.Text = $"Global Maximum: {myProblemList[cbxBenchmark.SelectedIndex].BestObjectiveValue}";

            //for (int i = 0; i < 5; i++) chtConvergence.Series[i].Points.Clear();

            //// Update chtConvergence
            //for(int i = 0; i < nudIterationLimit.Value; i++)
            //{
            //    chtConvergence.Series[0].Points.AddXY(i, soFarTheBestObjectiveForEachIterAndAlgo[0][i]);
            //    chtConvergence.Series[1].Points.AddXY(i, soFarTheBestObjectiveForEachIterAndAlgo[1][i]);
            //    chtConvergence.Series[2].Points.AddXY(i, soFarTheBestObjectiveForEachIterAndAlgo[2][i]);
            //    chtConvergence.Series[3].Points.AddXY(i, soFarTheBestObjectiveForEachIterAndAlgo[3][i]);
            //    chtConvergence.Series[4].Points.AddXY(i, soFarTheBestObjectiveForEachIterAndAlgo[4][i]);
            //}
            
        }

        private void btnPlotConvergence_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++) chtConvergence.Series[i].Points.Clear();

            if (cbxGA.Checked) chtConvergence.Series[0].IsVisibleInLegend = true;
            else chtConvergence.Series[0].IsVisibleInLegend = false;
            if (cbxGWO.Checked) chtConvergence.Series[1].IsVisibleInLegend = true;
            else chtConvergence.Series[1].IsVisibleInLegend = false;
            if (cbxGWOSFS.Checked) chtConvergence.Series[2].IsVisibleInLegend = true;
            else chtConvergence.Series[2].IsVisibleInLegend = false;
            if (cbxPSO.Checked) chtConvergence.Series[3].IsVisibleInLegend = true;
            else chtConvergence.Series[3].IsVisibleInLegend = false;
            if (cbxSFS.Checked) chtConvergence.Series[4].IsVisibleInLegend = true;
            else chtConvergence.Series[3].IsVisibleInLegend = false;


            myProblem = myProblemList[cbxBenchmark.SelectedIndex];

            if (cbxGWOSFS.Checked)
            {
                myGWOSFSsolver = new GreyWolfOptimizerBasedOnSFS(myProblem);

                myGWOSFSsolver.PopulationSize = Convert.ToInt32(nudPopulationSize.Value);
                myGWOSFSsolver.Reset();
                while (myGWOSFSsolver.IterationID < nudIterationLimit.Value)
                {
                    myGWOSFSsolver.RunOneIteration();

                    // Update chtConvergence
                    chtConvergence.Series[0].Points.AddXY(myGWOSFSsolver.IterationID, myGWOSFSsolver.SoFarTheBestObjectiveValue);
                }
            }

            if (cbxGWO.Checked)
            {
                myGWOsolver = new GreyWolfOptimizer(myProblem);

                myGWOsolver.PopulationSize = Convert.ToInt32(nudPopulationSize.Value);
                myGWOsolver.Reset();
                while (myGWOsolver.IterationID < nudIterationLimit.Value)
                {
                    myGWOsolver.RunOneIteration();

                    // Update chtConvergence
                    chtConvergence.Series[1].Points.AddXY(myGWOsolver.IterationID, myGWOsolver.SoFarTheBestObjectiveValue);
                }
            }

            if (cbxSFS.Checked)
            {
                mySFSsolver = new StochasticFractalSearch(myProblem);

                mySFSsolver.NumberOfParticles = Convert.ToInt32(nudPopulationSize.Value);
                mySFSsolver.Reset();
                while (mySFSsolver.IterationID < nudIterationLimit.Value)
                {
                    mySFSsolver.RunOneIteration();

                    // Update chtConvergence
                    chtConvergence.Series[2].Points.AddXY(mySFSsolver.IterationID, mySFSsolver.SoFarTheBestObjectiveValue);
                }
            }

            if (cbxPSO.Checked)
            {
                myPSOsolver = new ParticleSwarmOptimizer(myProblem);

                myPSOsolver.NumberOfParticles = Convert.ToInt32(nudPopulationSize.Value);
                myPSOsolver.Reset();
                while (myPSOsolver.IterationID < nudIterationLimit.Value)
                {
                    myPSOsolver.RunOneIteration();

                    // Update chtConvergence
                    chtConvergence.Series[3].Points.AddXY(myPSOsolver.IterationID + 1, myPSOsolver.SoFarTheBestObjectiveValue);
                }
            }

            if (cbxGA.Checked)
            {
                if (myProblem.OptimizationGoal == COP.OptimizationType.Minimization)
                {
                    myGAsolver = new RealNumberEncodedGA(myProblem.Dimension, myProblem.LowerBound, myProblem.UpperBound, FLHuaGALibrary.OptimizationType.Minimization, myProblem.GetObjectiveValue);
                }
                else
                {
                    myGAsolver = new RealNumberEncodedGA(myProblem.Dimension, myProblem.LowerBound, myProblem.UpperBound, FLHuaGALibrary.OptimizationType.Maximization, myProblem.GetObjectiveValue);
                }

                myGAsolver.PopulationSize = Convert.ToInt32(nudPopulationSize.Value);
                myGAsolver.Reset();
                while (myGAsolver.IterationID < nudIterationLimit.Value)
                {
                    myGAsolver.RunOneIteration();

                    // Update chtConvergence
                    chtConvergence.Series[4].Points.AddXY(myGAsolver.IterationID + 1, myGAsolver.SoFarTheBestObjectiveValue);
                }
            }
        }

        private void reusltToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog a1 = new SaveFileDialog();

            //if (a1.ShowDialog() == DialogResult.OK) a1.ShowDialog();
            a1.ShowDialog();

            if (a1.FileName != "")
            {
                //using (TextWriter tw = new StreamWriter("C:\\Users\\zixin\\Desktop\\R10546020FLHuaFinalProject\\example.txt"))
                using (TextWriter tw = new StreamWriter(a1.FileName))
                {
                    for (int i = 0; i < dgvEvaluation.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dgvEvaluation.Columns.Count; j++)
                        {
                            tw.Write($"{dgvEvaluation.Rows[i].Cells[j].Value.ToString()}");

                            if (j != dgvEvaluation.Columns.Count - 1)
                            {
                                tw.Write(",");
                            }
                        }
                        tw.WriteLine();
                    }
                }
            }

            MessageBox.Show("File Saved!", "File saved", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

        }

        private void chartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string folderPath = Path.GetDirectoryName(dlg.FileName);
                chtConvergence.SaveImage(folderPath, ChartImageFormat.Png);
            }


            MessageBox.Show("Image saved, you can find the file c:\\csharp.net-informations.png");
        }

    }
}
