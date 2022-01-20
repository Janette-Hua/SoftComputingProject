using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using COP;
using MathNet.Numerics.Distributions;

namespace R10546020FLHuaFinalProject
{
    public class GreyWolfOptimizerBasedOnSFS : GreyWolfOptimizer
    {
        // data fields
        int maximumDiffusionNumber = 1;
        double[] G1;
        double[] G2;
        double[] G3;
        double[] G1_prime;
        double[] G2_prime;
        double[] G3_prime;


        #region PROPERTIES

        public event EventHandler OnIterationCompleted; // on initialization, EventHandler is "null"

        #endregion

        // constructor
        public GreyWolfOptimizerBasedOnSFS(COP.COPBenchmark theProblem) : base(theProblem)
        {
            G1 = new double[numberOfVariables];
            G2 = new double[numberOfVariables];
            G3 = new double[numberOfVariables];
            G1_prime = new double[numberOfVariables];
            G2_prime = new double[numberOfVariables];
            G3_prime = new double[numberOfVariables];
        }

        // public interfacing functions
        public override void RunOneIteration()
        {
            // Find first, second, and third best individual: O(n)
            //   Note: we already know the first best ID from "iterationBestID"
            FindSecondAndThirdBest();

            UpdatePositions();

            // Diffusion process
            for(int i = 0; i < populationSize; i++)
            {
                for (int k = 0; k < numberOfVariables; k++)
                {
                    double g = 2;
                    double mean = soFarTheBestPosition[k];
                    double stdDev = Math.Abs((positions[i][k] - soFarTheBestPosition[k]));
                    double e1 = 1.0 - rnd.NextDouble(); // uniform(0,1] random doubles
                    double e2 = 1.0 - rnd.NextDouble();

                    Normal normalDist = new Normal(mean, stdDev);
                    positions[i][k] = normalDist.Sample() + (e1 * soFarTheBestPosition[k] - e2 * positions[i][k]);

                    if (positions[i][k] < lowerBounds[k]) positions[i][k] = lowerBounds[k];  // guarding conditions
                    if (positions[i][k] > upperBounds[k]) positions[i][k] = upperBounds[k];
                }
            }

            // Update parameters: a, A, C
            UpdateParameters();

            // Calculate the fitness function for each wolf
            // Update G_{\alpha}, G_{\beta}, G_{\delta}
            UpdateSoFarTheBestPosition();
            FindSecondAndThirdBest();


            // Fire  OneIterationCompleted  event
            if (OnIterationCompleted != null) OnIterationCompleted(this, null); // this 代表訂閱這個 event 的物件

            iterationID++;
        }



        public void RunToEnd()
        {
            while (iterationID < iterationLimit) RunOneIteration();
            MessageBox.Show("GWO-SFS execution complete!", "GWO-SFS Stop", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }


        // helping functions
        protected override void UpdatePositions()
        {
            for (int i = 0; i < populationSize; i++)
            {
                if (i == iterationBestID || i == secondBestID || i == thirdBestID) continue;

                int cutPoint = rnd.Next(numberOfVariables);

                for (int j = 0; j < numberOfVariables; j++)
                {
                    G1[j] = positions[iterationBestID][j] - A[iterationBestID][j] * Math.Abs(C[iterationBestID][j] * positions[iterationBestID][j] - positions[i][j]);
                    G2[j] = positions[secondBestID][j] - A[secondBestID][j] * Math.Abs(C[secondBestID][j] * positions[secondBestID][j] - positions[i][j]);
                    G3[j] = positions[thirdBestID][j] - A[thirdBestID][j] * Math.Abs(C[thirdBestID][j] * positions[thirdBestID][j] - positions[i][j]);

                    // Apply crossover process
                    if (j < cutPoint)
                    {
                        G1_prime[j] = G1[j];
                        G2_prime[j] = G2[j];
                        G3_prime[j] = G3[j];
                    }
                    else
                    {
                        G1_prime[j] = G2[j];
                        G2_prime[j] = G3[j];
                        G3_prime[j] = G1[j];
                    }

                    positions[i][j] = (G1_prime[j] + G2_prime[j] + G3_prime[j]) / 3;

                    if (positions[i][j] < lowerBounds[j]) positions[i][j] = lowerBounds[j];  // guarding conditions
                    if (positions[i][j] > upperBounds[j]) positions[i][j] = upperBounds[j];

                    //&  mutation process...
                }
            }

              
        }

        protected override void UpdateParameters()
        {
            a = 2 * (1 - (iterationID/iterationLimit) * (iterationID / iterationLimit));

            for (int i = 0; i < populationSize; i++)
            {
                for (int j = 0; j < numberOfVariables; j++)
                {
                    A[i][j] = 2 * a * r1[i][j] - a;
                    C[i][j] = 2 * r2[i][j];
                }
            }
        }


        COP.OptimizationType optimizationType = OptimizationType.Minimization; // enumeration
        public delegate double COPObjectiveFunction(double[] aSolution);
    }
}
