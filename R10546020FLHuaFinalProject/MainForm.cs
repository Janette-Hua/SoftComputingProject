using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using COP;
using FLHuaGALibrary;

namespace R10546020FLHuaFinalProject
{
    public enum MetaheuristicAlgorithmType
    {
        GWOSFS, GWO, SFS, PSO, GA
    }

    public partial class MainForm : Form
    {
        MetaheuristicAlgorithmType algorithmType = MetaheuristicAlgorithmType.GWOSFS;
        COPBenchmark myProblem;
        ParticleSwarmOptimizer myPSOsolver;
        RealNumberEncodedGA myGAsolver;
        StochasticFractalSearch mySFSsolver;
        GreyWolfOptimizer myGWOsolver;
        GreyWolfOptimizerBasedOnSFS myGWOSFSsolver;

        Stopwatch stopWatch;   // measure elapsed time
        StringBuilder sb = new StringBuilder();  // string will be appended later

        public MainForm()
        {
            InitializeComponent();
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            myProblem = COPBenchmark.LoadAProblemFromAFile();
            myProblem.DisplayOnPanel(splitContainer1.Panel1);
            myProblem.DisplayObjectiveGraphics(splitContainer2.Panel2);

            btnCreatePSOSolver.Enabled = true;

            chtObjective.Series[0].Points.Clear();
            chtObjective.Series[1].Points.Clear();
            chtObjective.Series[2].Points.Clear();
        }

        private void btnCreatePSOSolver_Click(object sender, EventArgs e)
        {
            if (rbtnPSO.Checked)
            {
                //mySolver = new ParticleSwarmOptimizer(myProblem.Dimension, myProblem.LowerBound, myProblem.UpperBound,
                //    myProblem.OptimizationGoal, myProblem.GetObjectiveValue);
                myPSOsolver = new ParticleSwarmOptimizer(myProblem);
                prgSolver.SelectedObject = myPSOsolver;
                myPSOsolver.OnIterationCompleted += MySolver_OnIterationCompleted;
                btnReset.Enabled = true;
                algorithmType = MetaheuristicAlgorithmType.PSO;
            }
            else if (rbtnGA.Checked)
            {
                if (myProblem.OptimizationGoal == COP.OptimizationType.Minimization)
                {
                    myGAsolver = new RealNumberEncodedGA(myProblem.Dimension, myProblem.LowerBound, myProblem.UpperBound, FLHuaGALibrary.OptimizationType.Minimization, myProblem.GetObjectiveValue);                   
                }
                else
                {
                    myGAsolver = new RealNumberEncodedGA(myProblem.Dimension, myProblem.LowerBound, myProblem.UpperBound, FLHuaGALibrary.OptimizationType.Maximization, myProblem.GetObjectiveValue);
                }

                prgSolver.SelectedObject = myGAsolver;
                myGAsolver.OneIterationCompleted += MyGAsolver_OneIterationCompleted;
                btnReset.Enabled = true;
                algorithmType = MetaheuristicAlgorithmType.GA;
            } 
            else if (rbtnSFS.Checked)
            {
                mySFSsolver = new StochasticFractalSearch(myProblem);
                prgSolver.SelectedObject = mySFSsolver;
                mySFSsolver.OnIterationCompleted += MySFSsolver_OnIterationCompleted;
                btnReset.Enabled = true;
                algorithmType = MetaheuristicAlgorithmType.SFS;
            }
            else if (rbtnGWO.Checked)
            {
                myGWOsolver = new GreyWolfOptimizer(myProblem);
                prgSolver.SelectedObject = myGWOsolver;
                myGWOsolver.OnIterationCompleted += MyGWOsolver_OnIterationCompleted;
                btnReset.Enabled = true;
                algorithmType = MetaheuristicAlgorithmType.GWO;
            }
            else if (rbtnGWOSFS.Checked)
            {
                myGWOSFSsolver = new GreyWolfOptimizerBasedOnSFS(myProblem);
                prgSolver.SelectedObject = myGWOSFSsolver;
                myGWOSFSsolver.OnIterationCompleted += MyGWOSFSsolver_OnIterationCompleted;
                btnReset.Enabled = true;
                algorithmType = MetaheuristicAlgorithmType.GWOSFS;
            }
            
        }

        private void MyGWOSFSsolver_OnIterationCompleted(object sender, EventArgs e)
        {
            // draw points on the chart
            chtObjective.Series[0].Points.AddXY(myGWOSFSsolver.IterationID, myGWOSFSsolver.IterationAverageObjective);
            chtObjective.Series[1].Points.AddXY(myGWOSFSsolver.IterationID, myGWOSFSsolver.IterationBestObjective);
            chtObjective.Series[2].Points.AddXY(myGWOSFSsolver.IterationID, myGWOSFSsolver.SoFarTheBestObjectiveValue);
            chtObjective.Update();

            // update progress bar
            if (myGWOSFSsolver.IterationID <= toolStripProgressBar1.Maximum) toolStripProgressBar1.Value = myGWOSFSsolver.IterationID;
            toolStrip1.Update();

            // update Tee Chart
            myProblem.DisplaySolutionsOnGraphics(myGWOSFSsolver.Positions);
            splitContainer2.Panel2.Refresh();

            // update soFarTheBestObjectiveValue  &  soFarTheBestSolution
            labSoFarTheBestObjValue.Text = $"So Far The Best Objective Value: {myGWOSFSsolver.SoFarTheBestObjectiveValue:0.000}";
            labSoFarTheBestObjValue.Update();

            sb.Clear();
            sb.Append("So Far the Best Solution: ");
            for (int i = 0; i < myProblem.Dimension; i++) sb.Append($"{myGWOSFSsolver.SoFarTheBestPosition[i]:.00} ");
            rtbSoFarTheBestSolution.Text = sb.ToString();
            rtbSoFarTheBestSolution.Update();
        }

        private void MyGWOsolver_OnIterationCompleted(object sender, EventArgs e)
        {
            // draw points on the chart
            chtObjective.Series[0].Points.AddXY(myGWOsolver.IterationID, myGWOsolver.IterationAverageObjective);
            chtObjective.Series[1].Points.AddXY(myGWOsolver.IterationID, myGWOsolver.IterationBestObjective);
            chtObjective.Series[2].Points.AddXY(myGWOsolver.IterationID, myGWOsolver.SoFarTheBestObjectiveValue);
            chtObjective.Update();

            // update progress bar
            if (myGWOsolver.IterationID <= toolStripProgressBar1.Maximum) toolStripProgressBar1.Value = myGWOsolver.IterationID;
            toolStrip1.Update();

            // update Tee Chart
            myProblem.DisplaySolutionsOnGraphics(myGWOsolver.Positions);
            splitContainer2.Panel2.Refresh();

            // update soFarTheBestObjectiveValue  &  soFarTheBestSolution
            labSoFarTheBestObjValue.Text = $"So Far The Best Objective Value: {myGWOsolver.SoFarTheBestObjectiveValue:0.000}";
            labSoFarTheBestObjValue.Update();

            sb.Clear();
            sb.Append("So Far the Best Solution: ");
            for (int i = 0; i < myProblem.Dimension; i++) sb.Append($"{myGWOsolver.SoFarTheBestPosition[i]:.00} ");
            rtbSoFarTheBestSolution.Text = sb.ToString();
            rtbSoFarTheBestSolution.Update();
        }

        private void MySFSsolver_OnIterationCompleted(object sender, EventArgs e)
        {
            // draw points on the chart
            chtObjective.Series[0].Points.AddXY(mySFSsolver.IterationID, mySFSsolver.IterationAverageObjective);
            chtObjective.Series[1].Points.AddXY(mySFSsolver.IterationID, mySFSsolver.IterationBestObjective);
            chtObjective.Series[2].Points.AddXY(mySFSsolver.IterationID, mySFSsolver.SoFarTheBestObjectiveValue);
            chtObjective.Update();

            // update progress bar
            if (mySFSsolver.IterationID <= toolStripProgressBar1.Maximum) toolStripProgressBar1.Value = mySFSsolver.IterationID;
            toolStrip1.Update();

            // update Tee Chart
            myProblem.DisplaySolutionsOnGraphics(mySFSsolver.Positions);
            splitContainer2.Panel2.Refresh();

            // update soFarTheBestObjectiveValue  &  soFarTheBestSolution
            labSoFarTheBestObjValue.Text = $"So Far The Best Objective Value: {mySFSsolver.SoFarTheBestObjectiveValue:0.000}";
            labSoFarTheBestObjValue.Update();

            sb.Clear();
            sb.Append("So Far the Best Solution: ");
            for (int i = 0; i < myProblem.Dimension; i++) sb.Append($"{mySFSsolver.SoFarTheBestPosition[i]:.00} ");
            rtbSoFarTheBestSolution.Text = sb.ToString();
            rtbSoFarTheBestSolution.Update();
        }

        private void MyGAsolver_OneIterationCompleted(object sender, EventArgs e)
        {
            // draw points on the chart
            chtObjective.Series[0].Points.AddXY(myGAsolver.IterationID + 1, myGAsolver.IterationAverageObjective);
            chtObjective.Series[1].Points.AddXY(myGAsolver.IterationID + 1, myGAsolver.IterationBestObjective);
            chtObjective.Series[2].Points.AddXY(myGAsolver.IterationID + 1, myGAsolver.SoFarTheBestObjectiveValue);
            chtObjective.Update();

            // update progress bar
            if(myGAsolver.IterationID <= toolStripProgressBar1.Maximum) toolStripProgressBar1.Value = myGAsolver.IterationID;
            toolStrip1.Update();

            // update Tee Chart
            myProblem.DisplaySolutionsOnGraphics(myGAsolver.Chromosomes);
            splitContainer2.Panel2.Refresh();

            // update soFarTheBestObjectiveValue
            labSoFarTheBestObjValue.Text = $"So Far The Best Objective Value: {myGAsolver.SoFarTheBestObjectiveValue:0.000}";
            labSoFarTheBestObjValue.Update();

            sb.Clear();
            sb.Append("So Far the Best Solution: ");
            for (int i = 0; i < myProblem.Dimension; i++) sb.Append($"{myGAsolver.CurrentBestSolution[i]:.00} ");
            rtbSoFarTheBestSolution.Text = sb.ToString();
            rtbSoFarTheBestSolution.Update();
        }

        private void MySolver_OnIterationCompleted(object sender, EventArgs e)
        {
            // draw points on the chart
            chtObjective.Series[0].Points.AddXY(myPSOsolver.IterationID + 1, myPSOsolver.IterationAverageObjective);
            chtObjective.Series[1].Points.AddXY(myPSOsolver.IterationID + 1, myPSOsolver.IterationBestObjective);
            chtObjective.Series[2].Points.AddXY(myPSOsolver.IterationID + 1, myPSOsolver.SoFarTheBestObjectiveValue);
            chtObjective.Update();

            // update progress bar
            if (myPSOsolver.IterationID <= toolStripProgressBar1.Maximum) toolStripProgressBar1.Value = myPSOsolver.IterationID;
            toolStrip1.Update();

            // update Tee Chart
            myProblem.DisplaySolutionsOnGraphics(myPSOsolver.Positions);
            splitContainer2.Panel2.Refresh();

            // update soFarTheBestObjectiveValue
            labSoFarTheBestObjValue.Text = $"So Far The Best Objective Value: {myPSOsolver.SoFarTheBestObjectiveValue:0.000}";
            labSoFarTheBestObjValue.Update();

            sb.Clear();
            sb.Append("So Far the Best Solution: ");
            for (int i = 0; i < myProblem.Dimension; i++) sb.Append($"{myPSOsolver.SoFarTheBestPosition[i]:.00} ");
            rtbSoFarTheBestSolution.Text = sb.ToString();
            rtbSoFarTheBestSolution.Update();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            stopWatch = new Stopwatch();  // renew the stopwatch

            switch (algorithmType)
            {
                case MetaheuristicAlgorithmType.GWOSFS:
                    myGWOSFSsolver.Reset();
                    myProblem.DisplaySolutionsOnGraphics(myGWOSFSsolver.Positions);
                    break;
                case MetaheuristicAlgorithmType.GWO:
                    myGWOsolver.Reset();
                    myProblem.DisplaySolutionsOnGraphics(myGWOsolver.Positions);
                    break;
                case MetaheuristicAlgorithmType.SFS:
                    mySFSsolver.Reset();
                    myProblem.DisplaySolutionsOnGraphics(mySFSsolver.Positions);
                    break;
                case MetaheuristicAlgorithmType.PSO:
                    myPSOsolver.Reset();
                    myProblem.DisplaySolutionsOnGraphics(myPSOsolver.Positions);
                    break;
                case MetaheuristicAlgorithmType.GA:
                    myGAsolver.Reset();
                    myProblem.DisplaySolutionsOnGraphics(myGAsolver.Chromosomes);
                    break;
            }

            
            btnRunOneIteration.Enabled = btnRunToEnd.Enabled = true;

            chtObjective.Series[0].Points.Clear();
            chtObjective.Series[1].Points.Clear();
            chtObjective.Series[2].Points.Clear();
        }

        private void btnRunOneIteration_Click(object sender, EventArgs e)
        {
            switch (algorithmType)
            {
                case MetaheuristicAlgorithmType.GWOSFS:
                    myGWOSFSsolver.RunOneIteration();
                    break;
                case MetaheuristicAlgorithmType.GWO:
                    myGWOsolver.RunOneIteration();
                    break;
                case MetaheuristicAlgorithmType.SFS:
                    mySFSsolver.RunOneIteration();
                    break;
                case MetaheuristicAlgorithmType.PSO:
                    myPSOsolver.RunOneIteration();
                    break;
                case MetaheuristicAlgorithmType.GA:
                    myGAsolver.RunOneIteration();
                    break;
            }
        }

        private void btnRunToEnd_Click(object sender, EventArgs e)
        {
            stopWatch.Start();

            switch (algorithmType)
            {
                case MetaheuristicAlgorithmType.GWOSFS:
                    myGWOSFSsolver.RunToEnd();
                    break;
                case MetaheuristicAlgorithmType.GWO:
                    myGWOsolver.RunToEnd();
                    break;
                case MetaheuristicAlgorithmType.SFS:
                    mySFSsolver.RunToEnd();
                    break;
                case MetaheuristicAlgorithmType.PSO:
                    myPSOsolver.RunToEnd();
                    break;
                case MetaheuristicAlgorithmType.GA:
                    myGAsolver.RunToEnd();
                    break;
            }


            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}.{0:000}", ts.Seconds, ts.Milliseconds / 10);
            labElapsedTime.Text = "Run Time: " + elapsedTime + " s";
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            COPBenchmark.CreateANewProblemAndShowEditor();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            // Create a new instance of the form.
            EvaluationForm form = new EvaluationForm();

            form.ShowDialog();


        }
    }
}
