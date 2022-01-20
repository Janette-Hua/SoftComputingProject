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
    public class ParticleSwarmOptimizer
    {
        COP.OptimizationType optimizationType = OptimizationType.Minimization; // enumeration

        // data fields
        int numberOfVariables;
        int numberOfParticles = 10;

        double[][] positions; // an array of array
        double[][] selfBestPositions; // particular for PSO

        double[] objectiveValues;
        double[] selfBestObjectiveValues; // particular for PSO

        double[] lowerBounds, upperBounds;

        double soFarTheBestObjectiveValue;
        double[] soFarTheBestPosition;
        double iterationAverageObjective;
        double iterationBestObjective;

        double selfFactor = 0.8;
        double socialFactor = 0.2;
        int iterationID;
        int iterationLimit = 200;
        COPObjectiveFunction theObjectiveFunction; // delegate
        Random rnd = new Random();


        #region PROPERTIES
        // define public properties for those modifiable parameters
        [Category("PSO Parameters"), Description("Self factor (c1) expresses how much confidence a particle has in itself. If c1 >> c2, each particle is much more attracted to its own personal best position, resulting in excessive wandering.")]
        public double SelfFactor { 
            get => selfFactor; 
            set
            {
                if (value > 0) selfFactor = value;
            }
        }

        [Category("PSO Parameters"), Description("Social factor (c2) expresses how much confidence a particle has in its neighbors. If c2 >> c1, particles are more strongly attracted to the global best position, causing particles to rush prematurely towards optima.")]
        public double SocialFactor { 
            get => socialFactor; 
            set
            {
                if (value > 0) socialFactor = value;
            }
        }

        [Category("PSO Parameters"), Description("The number of agents to construct the solution.")]
        public int NumberOfParticles { 
            get => numberOfParticles; 
            set
            {
                if (value > 0) numberOfParticles = value;
            }
        }


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

        // ...

        #endregion

        // constructor
        public ParticleSwarmOptimizer(COP.COPBenchmark theProblem)
        {
            numberOfVariables = theProblem.Dimension;
            lowerBounds = theProblem.LowerBound;
            upperBounds = theProblem.UpperBound;
            optimizationType = theProblem.OptimizationGoal;
            theObjectiveFunction = theProblem.GetObjectiveValue;

            soFarTheBestPosition = new double[numberOfVariables];
        }

        public ParticleSwarmOptimizer(int numberOfVariables, double[] lowerBounds, double[] upperBounds,
            COP.OptimizationType type, COPObjectiveFunction objFun)
        {
            this.numberOfVariables = numberOfVariables;
            this.lowerBounds = lowerBounds;
            this.upperBounds = upperBounds;
            optimizationType = type;
            theObjectiveFunction = objFun;
            soFarTheBestPosition = new double[numberOfVariables];
        }


        // public interfacing functions
        public void Reset()
        {
            iterationID = 0;

            if (positions == null || positions.Length != numberOfParticles)
            {
                // allocate or reallocate memory
                positions = new double[numberOfParticles][];
                selfBestPositions = new double[numberOfParticles][];
                for (int i = 0; i < numberOfParticles; i++)
                {
                    positions[i] = new double[numberOfVariables];
                    selfBestPositions[i] = new double[numberOfVariables];
                }
                    

                selfBestObjectiveValues = new double[numberOfParticles];                
                objectiveValues = new double[numberOfParticles];
            }

            if (optimizationType == OptimizationType.Maximization) soFarTheBestObjectiveValue = double.MinValue;
            else soFarTheBestObjectiveValue = double.MaxValue;


            // Randomly assign initial positions
            for(int i = 0; i < numberOfParticles; i++)
            {
                for(int j = 0; j < numberOfVariables; j++)
                {
                    positions[i][j] = lowerBounds[j] + rnd.NextDouble() * (upperBounds[j] - lowerBounds[j]);
                }

                if (optimizationType == OptimizationType.Minimization)
                    selfBestObjectiveValues[i] = double.MaxValue;
                else
                    selfBestObjectiveValues[i] = double.MinValue;
            }

            UpdateSoFarTheBestPosition();
        }


        public void RunOneIteration()
        {
            MoveAllParticlesToNewPosition();
            UpdateSoFarTheBestPosition();

            // Fire  OneIterationCompleted  event
            if (OnIterationCompleted != null) OnIterationCompleted(this, null); // this 代表訂閱這個 event 的物件

            iterationID++;
        }

        private void UpdateSoFarTheBestPosition()
        {
            // Evaluate the objectives of all new positions, and update
            // self best position and objectiveValue, if new position is better.
            // calculate iteration average and iteration best ID
            // update soFarTheBestSolution and objectives if iteration best is better than soFarTheBest

            // one-pass algorithm to get the average

            double sum = 0;
            int iterBestID = 0;
            double max = double.MinValue;
            double min = double.MaxValue;

            for(int i = 0; i < numberOfParticles; i++)
            {
                objectiveValues[i] = theObjectiveFunction(positions[i]);

                if(optimizationType == OptimizationType.Minimization)
                {
                    // minimization

                    if (objectiveValues[i] < selfBestObjectiveValues[i])
                    {
                        for (int j = 0; j < numberOfVariables; j++)
                        {
                            selfBestPositions[i][j] = positions[i][j];
                        }

                        selfBestObjectiveValues[i] = objectiveValues[i];
                    }

                    if(objectiveValues[i] < min)
                    {
                        min = objectiveValues[i];
                        iterBestID = i;
                    }
                }
                else
                {
                    // maximization

                    if (objectiveValues[i] > selfBestObjectiveValues[i])
                    {
                        for (int j = 0; j < numberOfVariables; j++)
                        {
                            selfBestPositions[i][j] = positions[i][j];
                        }

                        selfBestObjectiveValues[i] = objectiveValues[i];
                    }

                    if (objectiveValues[i] > max)
                    {
                        max = objectiveValues[i];
                        iterBestID = i;
                    }
                }

                sum += objectiveValues[i];
            }

            iterationAverageObjective = sum / numberOfParticles;
            iterationBestObjective = objectiveValues[iterBestID];

            if(optimizationType == OptimizationType.Minimization)
            {
                if (min < soFarTheBestObjectiveValue)
                {
                    soFarTheBestObjectiveValue = min;

                    for (int i = 0; i < numberOfVariables; i++)
                        soFarTheBestPosition[i] = selfBestPositions[iterBestID][i];
                }
            }
            else
            {
                if (max > soFarTheBestObjectiveValue)
                {
                    soFarTheBestObjectiveValue = max;

                    for (int i = 0; i < numberOfVariables; i++)
                        soFarTheBestPosition[i] = selfBestPositions[iterBestID][i];
                }
            }

        }

        private void MoveAllParticlesToNewPosition()
        {
            // Use the two factors and soFarTheBestPosition and selfBestPosition to
            // set new Positions for all particles

            for(int i = 0; i < numberOfParticles; i++)
            {
                double alpha = selfFactor * rnd.NextDouble();
                double beta = socialFactor * rnd.NextDouble();

                for(int j = 0; j < numberOfVariables; j++)
                {
                    positions[i][j] += alpha * (selfBestPositions[i][j] - positions[i][j]) + beta * (soFarTheBestPosition[j] - positions[i][j]);

                    if (positions[i][j] > upperBounds[j]) positions[i][j] = upperBounds[j];
                    else if (positions[i][j] < lowerBounds[j]) positions[i][j] = lowerBounds[j];
                }
            }
        }
        
        public void RunToEnd()
        {
            while (iterationID < iterationLimit) RunOneIteration();
            MessageBox.Show("PSO execution complete!", "PSO Stop", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

    }

    public delegate double COPObjectiveFunction(double[] aSolution);
}
