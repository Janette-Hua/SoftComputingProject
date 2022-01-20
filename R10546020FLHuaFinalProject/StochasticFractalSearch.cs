using COP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics.Distributions;
using System.ComponentModel;

namespace R10546020FLHuaFinalProject
{
    class StochasticFractalSearch
    {
        // data fields
        int numberOfVariables;
        int numberOfParticles = 100;
        int maximumDiffusionNumber = 1;
        GaussianType gaussianType = GaussianType.GaussianWalkWithBestPosition;

        double[][] positions;              // [][]: an array of array
        double[] GaussianWalk;             // temporary storage for the particle generated in the diffusion process
        double[] bestParticlePosition;     // the best particle generated in the diffusion process
        int[] indexArr;                    // index array to store the indices of the positions

        double[] lowerBounds, upperBounds; // the lower bound and upper bound of each variable

        double[] objectiveValues;
        double soFarTheBestObjectiveValue;
        double[] soFarTheBestPosition;
        double iterationAverageObjective;
        double iterationBestObjective;

        int iterationID;
        int iterationLimit = 200;
        COPObjectiveFunction theObjectiveFunction; // delegate
        Random rnd = new Random();

        #region PROPERTIES
        [Category("SFS Parameters"), Description("The number of agents to construct the solution.")]
        public int NumberOfParticles {
            get => numberOfParticles; 
            set
            {
                if (value > 0) numberOfParticles = value;
            }
        }


        [Category("SFS Parameters"), Description("The maximum number of diffusion.")]
        public int MaximumDiffusionNumber { 
            get => maximumDiffusionNumber; 
            set
            {
                if (value > 0) maximumDiffusionNumber = value;
            }
        }

        [Category("SFS Parameters"), Description("There are two kinds of Gaussain random walks participating in the diffusion process, users may choose either one.")]
        public GaussianType GaussianType1 { get => gaussianType; set => gaussianType = value; }

        [Category("Execution"), Description("The upper limit of iteration.")]
        public int IterationLimit { get => iterationLimit; set => iterationLimit = value; }

        [Browsable(false)]
        public double[][] Positions { get => positions; set => positions = value; }

        [Browsable(false)]
        public int IterationID { get => iterationID; set => iterationID = value; }

        [Browsable(false)]
        public double IterationAverageObjective { get => iterationAverageObjective; set => iterationAverageObjective = value; }

        [Browsable(false)]
        public double SoFarTheBestObjectiveValue { get => soFarTheBestObjectiveValue; set => soFarTheBestObjectiveValue = value; }

        [Browsable(false)]
        public double IterationBestObjective { get => iterationBestObjective; set => iterationBestObjective = value; }

        [Browsable(false)]
        public double[] SoFarTheBestPosition { get => soFarTheBestPosition; set => soFarTheBestPosition = value; }

        public event EventHandler OnIterationCompleted; // on initialization, EventHandler is "null"

        #endregion

        // constructor
        public StochasticFractalSearch(COP.COPBenchmark theProblem)
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

            if (positions == null || positions.Length != numberOfParticles + maximumDiffusionNumber)
            {
                // allocate or reallocate memory
                positions = new double[numberOfParticles + maximumDiffusionNumber][];
                indexArr = new int[numberOfParticles + maximumDiffusionNumber];
                for (int i = 0; i < (numberOfParticles + maximumDiffusionNumber); i++)
                {
                    positions[i] = new double[numberOfVariables];
                    indexArr[i] = i;
                }
                GaussianWalk = new double[numberOfVariables];
                bestParticlePosition = new double[numberOfVariables];               

                objectiveValues = new double[numberOfParticles + maximumDiffusionNumber];
            }

            if (optimizationType == OptimizationType.Maximization) soFarTheBestObjectiveValue = double.MinValue;
            else soFarTheBestObjectiveValue = double.MaxValue;


            // Randomly assign initial positions
            for (int i = 0; i < numberOfParticles + maximumDiffusionNumber; i++)
            {
                for (int j = 0; j < numberOfVariables; j++)
                {
                    positions[i][j] = lowerBounds[j] + rnd.NextDouble() * (upperBounds[j] - lowerBounds[j]);
                }
            }

            UpdateSoFarTheBestPosition();
        }

        public void RunOneIteration()
        {
            iterationID++;
            
            Diffuse();
            UpdatePositions();

            // Fire  OneIterationCompleted  event
            if (OnIterationCompleted != null) OnIterationCompleted(this, null); // this 代表訂閱這個 event 的物件
            
        }
        public void RunToEnd()
        {
            while (iterationID < iterationLimit) RunOneIteration();
            MessageBox.Show("SFS execution complete!", "SFS Stop", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }


        // helping functions
        private void Diffuse()
        {
            // Static diffusion process
            // only "the best generated particle" from the diffusing process will be considered

            double bestObjectiveValue;

            if(optimizationType == OptimizationType.Minimization)
            {
                // minimization

                for (int j = 0; j < maximumDiffusionNumber; j++)
                {
                    bestObjectiveValue = double.MaxValue;

                    for (int i = 0; i < numberOfParticles; i++)
                    {
                        if (gaussianType == GaussianType.GaussianWalkWithBestPosition)
                        {
                            // create a new point for each point in the system based on Eq. (11)
                            //   GW_1 = Gaussian(mu_{BP}, sigma) + (e1 * BP - e2 * P_i)

                            GaussianWalk1(i);
                        }
                        else
                        {
                            // create a new point for each point in the system based on Eq. (12)
                            //   GW_2 = Gaussian(mu_{P}, sigma)

                            GaussianWalk2(i);
                        }

                        objectiveValues[numberOfParticles + j] = theObjectiveFunction(GaussianWalk);
                        if (objectiveValues[numberOfParticles + j] < bestObjectiveValue)
                        {
                            bestObjectiveValue = objectiveValues[numberOfParticles + j];
                            for (int l = 0; l < numberOfVariables; l++) bestParticlePosition[l] = GaussianWalk[l];
                        }
                    }

                    objectiveValues[numberOfParticles + j] = bestObjectiveValue;
                    for(int m = 0; m < numberOfVariables; m++) positions[numberOfParticles + j][m] = bestParticlePosition[m];
                }
            }
            else
            {
                // maximization

                for (int j = 0; j < maximumDiffusionNumber; j++)
                {
                    bestObjectiveValue = double.MinValue;

                    for (int i = 0; i < numberOfParticles; i++)
                    {
                        if (gaussianType == GaussianType.GaussianWalkWithBestPosition)
                        {
                            // create a new point for each point in the system based on Eq. (11)
                            //   GW_1 = Gaussian(mu_{BP}, sigma) + (e1 * BP - e2 * P_i)

                            GaussianWalk1(i);
                        }
                        else
                        {
                            // create a new point for each point in the system based on Eq. (12)
                            //   GW_2 = Gaussian(mu_{P}, sigma)

                            GaussianWalk2(i);
                        }

                        objectiveValues[numberOfParticles + j] = theObjectiveFunction(GaussianWalk);
                        if (objectiveValues[numberOfParticles + j] > bestObjectiveValue)
                        {
                            bestObjectiveValue = objectiveValues[numberOfParticles + j];
                            for (int l = 0; l < numberOfVariables; l++) bestParticlePosition[l] = GaussianWalk[l];
                        }
                    }

                    objectiveValues[numberOfParticles + j] = bestObjectiveValue;
                    for (int m = 0; m < numberOfVariables; m++) positions[numberOfParticles + j][m] = bestParticlePosition[m];
                }
            }
                     
        }

        


        private void GaussianWalk2(int i)
        {
            for (int k = 0; k < numberOfVariables; k++)
            {
                double g = 2;
                double mean = positions[i][k];
                double stdDev = Math.Abs((positions[i][k] - soFarTheBestPosition[k]) * Math.Log(g) / g); // sigma: abs((log(g)/g) * (P_i - BP))
                                                                                                         //      log(g)/g is used in order to decrease the size of Gaussian jumps
                                                                                                         //      arbitrarily set g to 2, then log(g)/g returns 0.15
                Normal normalDist = new Normal(mean, stdDev);
                GaussianWalk[k] = normalDist.Sample();
                if (GaussianWalk[k] < lowerBounds[k]) GaussianWalk[k] = lowerBounds[k];  // guarding conditions
                if (GaussianWalk[k] > upperBounds[k]) GaussianWalk[k] = upperBounds[k];
            }      
        }

        private void GaussianWalk1(int i)
        {
            for (int k = 0; k < numberOfVariables; k++)
            {
                double g = 2;
                double mean = soFarTheBestPosition[k];
                double stdDev = Math.Abs((positions[i][k] - soFarTheBestPosition[k]) * Math.Log(g) / g); // sigma: abs((log(g)/g) * (P_i - BP))
                                                                                                         //      log(g)/g is used in order to decrease the size of Gaussian jumps
                                                                                                         //      arbitrarily set g to 2, then log(g)/g returns 0.15
                double e1 = 1.0 - rnd.NextDouble(); // uniform(0,1] random doubles
                double e2 = 1.0 - rnd.NextDouble();

                Normal normalDist = new Normal(mean, stdDev);
                GaussianWalk[k] = normalDist.Sample() + (e1 * soFarTheBestPosition[k] - e2 * positions[i][k]);
                if (GaussianWalk[k] < lowerBounds[k]) GaussianWalk[k] = lowerBounds[k];  // guarding conditions
                if (GaussianWalk[k] > upperBounds[k]) GaussianWalk[k] = upperBounds[k];
            }

            // Box-Muller transform
            //double mean = 10;
            //double stdDev = 1.0;
            //double u1 = 1.0 - rnd.NextDouble(); // uniform(0,1] random doubles
            //double u2 = 1.0 - rnd.NextDouble();
            //double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); // random normal(0,1)
            //double randNormal = mean + stdDev * randStdNormal; // random normal(mean, stdDev^2)
        }

        private void UpdatePositions()
        {
            // First updating process
            // rank all the points in the system
            // for each point, update the position by eq. (16) if Pa_i < epsilon
            //   where Pa_i = rank(P_i) / N
            //         N = numberOfParticles + maximumDiffusionNumber, rank(P_i) = the rank of point P_i among the other points in the group

            int N = numberOfParticles + maximumDiffusionNumber;

            Array.Sort(objectiveValues, indexArr);  // sort array from smallest to largest according to the objectiveValues
            //if (optimizationType == OptimizationType.Minimization) Array.Reverse(indexArr);

            double epsilon = rnd.NextDouble();
            int t = rnd.Next(N); // random selected index from the group
            int r;
            do{ r = rnd.Next(N); } while (t == r);
            for(int i = 0; i < N; i++)
            {
                for (int j = 0; j < numberOfVariables; j++)
                {
                    if(((double)(N-i) / N) < epsilon)
                    {
                        positions[indexArr[i]][j] = positions[r][j] - epsilon * (positions[t][j] - positions[indexArr[i]][j]);
                        if (positions[indexArr[i]][j] < lowerBounds[j]) positions[indexArr[i]][j] = lowerBounds[j];  // guarding conditions
                        if (positions[indexArr[i]][j] > upperBounds[j]) positions[indexArr[i]][j] = upperBounds[j];
                    }                  
                }
            }
            //int index = 0;
            //while(((double)index/N) < epsilon)
            //{
            //    for (int j = 0; j < numberOfVariables; j++)
            //    {
            //        positions[index][j] = positions[r][j] - epsilon * (positions[t][j] - positions[index][j]);
            //        if (positions[index][j] < lowerBounds[j]) positions[index][j] = lowerBounds[j];  // guarding conditions
            //        if (positions[index][j] > upperBounds[j]) positions[index][j] = upperBounds[j];
            //    }

            //    index++;
            //}

            Array.Sort(indexArr);
            UpdateSoFarTheBestPosition();


            // Second updating process
            // rank the new positions as in the first updating process
            // for each point, update the position if Pa_i < epsilon by eq. (17) and (18)
            Array.Sort(objectiveValues, indexArr);  // sort array from smallest to largest according to the objectiveValues
            //if (optimizationType == OptimizationType.Minimization) Array.Reverse(indexArr);

            epsilon = rnd.NextDouble();
            for(int i = 0; i < numberOfParticles; i++)
            {
                if(((double)(N-i) / N) < epsilon)
                {
                    if (epsilon <= 0.5)
                    {
                        for (int j = 0; j < numberOfVariables; j++)
                        {
                            GaussianWalk[j] = positions[indexArr[i]][j] - epsilon * (positions[t][j] - soFarTheBestPosition[j]);
                            if (positions[indexArr[i]][j] < lowerBounds[j]) positions[indexArr[i]][j] = lowerBounds[j];  // guarding conditions
                            if (positions[indexArr[i]][j] > upperBounds[j]) positions[indexArr[i]][j] = upperBounds[j];
                        }
                    }
                    else
                    {
                        for (int j = 0; j < numberOfVariables; j++)
                        {
                            GaussianWalk[j] = positions[indexArr[i]][j] + epsilon * (positions[t][j] - positions[r][j]);
                            if (positions[indexArr[i]][j] < lowerBounds[j]) positions[indexArr[i]][j] = lowerBounds[j];  // guarding conditions
                            if (positions[indexArr[i]][j] > upperBounds[j]) positions[indexArr[i]][j] = upperBounds[j];
                        }
                    }

                    if(optimizationType == OptimizationType.Minimization)
                    {
                        if (theObjectiveFunction(GaussianWalk) < objectiveValues[i])
                        {
                            objectiveValues[i] = theObjectiveFunction(GaussianWalk);
                            for (int k = 0; k < numberOfVariables; k++) positions[indexArr[i]][k] = GaussianWalk[k];
                        }
                    }
                    else
                    {
                        if (theObjectiveFunction(GaussianWalk) > objectiveValues[i])
                        {
                            objectiveValues[i] = theObjectiveFunction(GaussianWalk);
                            for (int k = 0; k < numberOfVariables; k++) positions[indexArr[i]][k] = GaussianWalk[k];
                        }
                    }
                }
            }
            //while (((double)index / (numberOfParticles + maximumDiffusionNumber)) < epsilon)
            //{
            //    if(epsilon <= 0.5)
            //    {
            //        for (int j = 0; j < numberOfVariables; j++)
            //        {
            //            positions[index][j] -= epsilon * (positions[t][j] - soFarTheBestPosition[j]);
            //            if (positions[index][j] < lowerBounds[j]) positions[index][j] = lowerBounds[j];  // guarding conditions
            //            if (positions[index][j] > upperBounds[j]) positions[index][j] = upperBounds[j];
            //        }
            //    }
            //    else
            //    {
            //        for (int j = 0; j < numberOfVariables; j++)
            //        {
            //            positions[index][j] += epsilon * (positions[t][j] - positions[r][j]);
            //            if (positions[index][j] < lowerBounds[j]) positions[index][j] = lowerBounds[j];  // guarding conditions
            //            if (positions[index][j] > upperBounds[j]) positions[index][j] = upperBounds[j];
            //        }
            //    }

            //    index++;
            //}

            Array.Sort(indexArr);
            UpdateSoFarTheBestPosition();
        }



        private void UpdateSoFarTheBestPosition()
        {
            // Evaluate the objectives of all new positions, and update objectiveValue.
            // Calculate iteration average and iteration best ID
            // Update soFarTheBestSolution and objectives if iteration best is better than soFarTheBest

            // one-pass algorithm to get the average

            double sum = 0;
            double max = double.MinValue;
            double min = double.MaxValue;
            int iterationBestID = 0;
            int N = numberOfParticles + maximumDiffusionNumber; // number of particles in the system (when iterationID = 0, N = numberOfParticles; when iterationID > 0, N = numberOfParticles + maximumDiffusionNumber)

            if (iterationID == 0) N = NumberOfParticles;

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

        



        COP.OptimizationType optimizationType = OptimizationType.Minimization; // enumeration
        public enum GaussianType { GaussianWalk, GaussianWalkWithBestPosition }
        public delegate double COPObjectiveFunction(double[] aSolution);
    }
}
