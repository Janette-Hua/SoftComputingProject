using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using COP;

namespace R10546020FLHuaFinalProject
{
    public class GreyWolfOptimizer
    {
        // data fields
        protected int populationSize = 20;           // size n (number of wolves in a pack)
        protected int numberOfVariables;

        protected double[][] positions;              // [][]: an array of array.
                                                     //   0: alpha (best)  1: beta (2nd best)  2: delta (3rd best)  3~N-1: omega (others)
        protected double[] positionOfPrey;           // the position of prey
        protected int secondBestID = 0;              // the index of G_{\beta}
        protected int thirdBestID = 0;               // the index of G_{\delta}
        protected double secondBestObjective;
        protected double thirdBestObjective;
        protected double[] lowerBounds, upperBounds; // the lower bound and upper bound of each variable
        protected double[][] A;                      // coefficient vectors
        protected double[][] C;                      // coefficient vectors
        protected double a;                          // a scalar that decreases linearly from 2 to 0 throughout iterations
        protected double[][] r1;                     // random vectors whose components are random values \in [0,1]
        protected double[][] r2;                     // random vectors whose components are random values \in [0,1]

        protected double[] objectiveValues;
        protected double soFarTheBestObjectiveValue;
        protected double[] soFarTheBestPosition;
        protected double iterationAverageObjective;
        protected double iterationBestObjective;
        protected int iterationBestID;

        protected int iterationID;
        protected int iterationLimit = 200;
        protected COPObjectiveFunction theObjectiveFunction; // delegate

        protected Random rnd = new Random();

        #region PROPERTIES

        [Category("GWO Parameters"), Description("The number of agents to construct the solution.")]
        public int PopulationSize { get => populationSize; set => populationSize = value; }

        [Category("Execution"), Description("The upper limit of iteration.")]
        public int IterationLimit { get => iterationLimit; set => iterationLimit = value; }

        [Browsable(false)]
        public double SoFarTheBestObjectiveValue { get => soFarTheBestObjectiveValue; set => soFarTheBestObjectiveValue = value; }

        [Browsable(false)]
        public double[] SoFarTheBestPosition { get => soFarTheBestPosition; set => soFarTheBestPosition = value; }

        [Browsable(false)]
        public double IterationAverageObjective { get => iterationAverageObjective; set => iterationAverageObjective = value; }

        [Browsable(false)]
        public double IterationBestObjective { get => iterationBestObjective; set => iterationBestObjective = value; }

        [Browsable(false)]
        public int IterationID { get => iterationID; set => iterationID = value; }

        [Browsable(false)]
        public double[][] Positions { get => positions; set => positions = value; }

        

        public event EventHandler OnIterationCompleted; // on initialization, EventHandler is "null"

        #endregion

        // constructor
        public GreyWolfOptimizer(COP.COPBenchmark theProblem)
        {
            numberOfVariables = theProblem.Dimension;
            lowerBounds = theProblem.LowerBound;
            upperBounds = theProblem.UpperBound;
            optimizationType = theProblem.OptimizationGoal;
            theObjectiveFunction = theProblem.GetObjectiveValue;

            soFarTheBestPosition = new double[numberOfVariables];
        }

        // public interfacing functions
        public void Reset()
        {
            iterationID = 0;

            if (positions == null || positions.Length != populationSize)
            {
                // allocate or reallocate memory

                // initialize GWO population  &   parameters
                positions = new double[populationSize][];
                A = new double[populationSize][];
                C = new double[populationSize][];
                r1 = new double[populationSize][];
                r2 = new double[populationSize][];
                for (int i = 0; i < populationSize; i++)
                {
                    positions[i] = new double[numberOfVariables];
                    A[i] = new double[numberOfVariables];
                    C[i] = new double[numberOfVariables];
                    r1[i] = new double[numberOfVariables];
                    r2[i] = new double[numberOfVariables];
                }

                objectiveValues = new double[populationSize];
                positionOfPrey = new double[numberOfVariables];
            }

            if (optimizationType == OptimizationType.Maximization) soFarTheBestObjectiveValue = double.MinValue;
            else soFarTheBestObjectiveValue = double.MaxValue;


            // Randomly assign initial positions & parameters
            for (int i = 0; i < populationSize; i++)
            {
                for (int j = 0; j < numberOfVariables; j++)
                {
                    positions[i][j] = lowerBounds[j] + rnd.NextDouble() * (upperBounds[j] - lowerBounds[j]);
                    r1[i][j] = rnd.NextDouble();
                    r2[i][j] = rnd.NextDouble();
                }
            }

            a = 2 - 2 * iterationID / iterationLimit;
            for (int i = 0; i < populationSize; i++)
            {
                for (int j = 0; j < numberOfVariables; j++)
                {
                    A[i][j] = 2 * a * r1[i][j] - a;
                    C[i][j] = 2 * r2[i][j];
                }
            }

            // Update soFarTheBestPosition  &  calculate the fitness function for each wolf G_i
            UpdateSoFarTheBestPosition();
        }

        public virtual void RunOneIteration()
        {
            // Find first, second, and third best individual: O(n)
            //   Note: we already know the first best ID from "iterationBestID"
            FindSecondAndThirdBest();

            UpdatePositions();

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
            MessageBox.Show("GWO execution complete!", "GWO Stop", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        // helping functions
        protected void UpdateSoFarTheBestPosition()
        {
            // Evaluate the objectives of all new positions, and update and objectiveValue.
            // Calculate iteration average and iteration best ID
            // Update soFarTheBestSolution and objectives if iteration best is better than soFarTheBest

            // one-pass algorithm to get the average

            double sum = 0;
            double max = double.MinValue;
            double min = double.MaxValue;
            int N = populationSize;



            for (int i = 0; i < N; i++)
            {
                objectiveValues[i] = theObjectiveFunction(positions[i]);

                if (optimizationType == OptimizationType.Minimization)
                {
                    // minimization

                    if (objectiveValues[i] < min)
                    {
                        min = objectiveValues[i];
                        iterationBestID = i;
                    }
                }
                else
                {
                    // maximization

                    if (objectiveValues[i] > max)
                    {
                        max = objectiveValues[i];
                        iterationBestID = i;
                    }
                }

                sum += objectiveValues[i];
            }

            iterationAverageObjective = sum / N;
            iterationBestObjective = objectiveValues[iterationBestID];

            if (optimizationType == OptimizationType.Minimization)
            {
                if (min < soFarTheBestObjectiveValue)
                {
                    soFarTheBestObjectiveValue = min;

                    for (int i = 0; i < numberOfVariables; i++)
                        soFarTheBestPosition[i] = positions[iterationBestID][i];
                }
            }
            else
            {
                if (max > soFarTheBestObjectiveValue)
                {
                    soFarTheBestObjectiveValue = max;

                    for (int i = 0; i < numberOfVariables; i++)
                        soFarTheBestPosition[i] = positions[iterationBestID][i];
                }
            }

        }

        protected virtual void UpdatePositions()
        {
            // Positions updating

            // Calculate the updated positions for each wolf G(t+1) except the first three best wolves.
            //   G(t+1) can be expressed as an average of the three solutions from Eq. (7)
            //      G_1 = G_{\alpha} - A_1 \cdot D_{\alpha}
            //      G_2 = G_{\beta} - A_2 \cdot D_{\beta}
            //      G_3 = G_{\delta} - A_3 \cdot D_{\delta}
            // Note: the three best solutions, G_{\alpha}, G_{\beta}, G_{\delta}, guide other individuals G_{\omega}
            //       to change their positions toward the estimated position of the prey
            for(int i = 0; i < populationSize; i++)
            {
                for(int j = 0; j < numberOfVariables; j++)
                {
                    A[i][j] = 2 * a * rnd.NextDouble() - a;
                    C[i][j] = 2 * rnd.NextDouble();
                }
            }

            for (int i = 0; i < populationSize; i++)
            {
                if (i == iterationBestID || i == secondBestID || i == thirdBestID) continue;

                for (int j = 0; j < numberOfVariables; j++)
                {
                    positions[i][j] = (positions[iterationBestID][j] - A[iterationBestID][j] * Math.Abs(C[iterationBestID][j] * positions[iterationBestID][j] - positions[i][j])
                        + positions[secondBestID][j] - A[secondBestID][j] * Math.Abs(C[secondBestID][j] * positions[secondBestID][j] - positions[i][j])
                        + positions[thirdBestID][j] - A[thirdBestID][j] * Math.Abs(C[thirdBestID][j] * positions[thirdBestID][j] - positions[i][j])) / 3;
                    //positions[i][j] = (positions[iterationBestID][j] - A[iterationBestID][j] * Math.Abs(C[iterationBestID][j] * positions[iterationBestID][j] - positions[i][j])
                    //    + positions[secondBestID][j] - A[secondBestID][j] * Math.Abs(C[secondBestID][j] * positions[secondBestID][j] - positions[i][j])
                    //    + positions[thirdBestID][j] - A[thirdBestID][j] * Math.Abs(C[thirdBestID][j] * positions[thirdBestID][j] - positions[i][j])) / 3;
                    if (positions[i][j] < lowerBounds[j]) positions[i][j] = lowerBounds[j];  // guarding conditions
                    if (positions[i][j] > upperBounds[j]) positions[i][j] = upperBounds[j];
                }
            }
        }

        protected virtual void UpdateParameters()
        {
            a = 2 - 2 * (iterationID + 1) / iterationLimit;
            for (int i = 0; i < populationSize; i++)
            {
                for (int j = 0; j < numberOfVariables; j++)
                {
                    A[i][j] = 2 * a * r1[i][j] - a;
                    C[i][j] = 2 * r2[i][j];
                }
            }
        }

        protected void FindSecondAndThirdBest()
        {
            if (optimizationType == OptimizationType.Minimization) secondBestObjective = thirdBestObjective = double.MaxValue;
            else secondBestObjective = thirdBestObjective = double.MinValue;
            for (int i = 0; i < populationSize; i++)
            {
                if (i == iterationBestID) continue;

                if (optimizationType == OptimizationType.Minimization)
                {
                    if (objectiveValues[i] < secondBestObjective)
                    {
                        thirdBestObjective = secondBestObjective;
                        secondBestObjective = objectiveValues[i];
                        secondBestID = i;
                    }
                    else if (objectiveValues[i] < thirdBestObjective && objectiveValues[i] != secondBestObjective)
                    {
                        thirdBestObjective = objectiveValues[i];
                        thirdBestID = i;
                    }
                }
                else
                {
                    if (objectiveValues[i] > secondBestObjective)
                    {
                        thirdBestObjective = secondBestObjective;
                        secondBestObjective = objectiveValues[i];
                        secondBestID = i;
                    }
                    else if (objectiveValues[i] > thirdBestObjective && objectiveValues[i] != secondBestObjective)
                    {
                        thirdBestObjective = objectiveValues[i];
                        thirdBestID = i;
                    }
                }
            }
        }

        COP.OptimizationType optimizationType = OptimizationType.Minimization; // enumeration

        

        public delegate double COPObjectiveFunction(double[] aSolution);
    }
}
