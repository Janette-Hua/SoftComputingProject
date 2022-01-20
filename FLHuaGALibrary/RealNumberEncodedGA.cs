using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FLHuaGALibrary
{
    public enum CrossoverType
    {
        Convex, Affine, Linear, LVD, SVD, MOES
    }


    /// <summary>
    ///  Real-number-encoded GA Solver.
    /// </summary>
    public class RealNumberEncodedGA: GenericGASolver<double>
    {
        // data field
        double[] lowerBounds;
        double[] upperBounds;
        double alpha; // for crossover
        double beta; // for crossover
        double b = .9; // for uniform mutation
        protected Random randomizer = new Random();


        // PROPERTIES

        [Category("Real-number Encoded GA"), Description("The crossover type of a real-number-encoded GA solver.")]
        public CrossoverType CrossoverType { set; get; } = CrossoverType.Linear;

        [Browsable(false)]
        public double B { get => b; set => b = value; }



        // constructor
        /// <summary>
        ///  The user need to provide the lower bound and upper bound to the constructor.
        /// </summary>
        /// <param name="numberOfVariables"></param>
        /// <param name="lowerBounds"></param>
        /// <param name="upperBounds"></param>
        /// <param name="optimizationType"></param>
        /// <param name="objectiveFunction"></param>
        /// <param name="hostPanelForMonitor"></param>
        public RealNumberEncodedGA(int numberOfVariables, double[] lowerBounds, double[] upperBounds, OptimizationType optimizationType,
            ObjectiveFunction<double> objectiveFunction, SplitterPanel hostPanelForMonitor = null) :
            base(numberOfVariables, optimizationType, objectiveFunction, hostPanelForMonitor = null)
        {
            this.lowerBounds = lowerBounds;
            this.upperBounds = upperBounds;
        }


        public override void InitializePopulation()
        {
            for(int p = 0; p < populationSize; p++)
            {
                for(int i = 0; i < numberOfGenes; i++)
                {
                    chromosomes[p][i] = lowerBounds[i] + randomizer.NextDouble() * (upperBounds[i] - lowerBounds[i]);
                }
            }
        }


        public override void CrossoverAPairParent(int father, int mother, int child1, int child2)
        {

            switch (CrossoverType)
            {
                case CrossoverType.Convex:

                    int pos = randomizer.Next(numberOfGenes);
                    double alpha = randomizer.NextDouble();

                    for (int i = 0; i < pos; i++)
                    {
                        chromosomes[child1][i] = chromosomes[father][i];
                        chromosomes[child2][i] = chromosomes[mother][i];
                    }

                    for (int i = pos; i < numberOfGenes; i++)
                    {
                        chromosomes[child1][i] = alpha * chromosomes[father][i] + (1 - alpha) * chromosomes[mother][i];
                        chromosomes[child2][i] = (1 - alpha) * chromosomes[father][i] + alpha * chromosomes[mother][i];
                    }

                    break;

                case CrossoverType.Affine:

                    pos = randomizer.Next(numberOfGenes);
                    alpha = -5 + randomizer.NextDouble() * 10;

                    for (int i = 0; i < pos; i++)
                    {
                        chromosomes[child1][i] = chromosomes[father][i];
                        chromosomes[child2][i] = chromosomes[father][i];
                    }

                    for (int i = pos; i < numberOfGenes; i++)
                    {
                        chromosomes[child1][i] = alpha * chromosomes[father][i] + (1 - alpha) * chromosomes[mother][i];
                        if (chromosomes[child1][i] > upperBounds[i])
                            chromosomes[child1][i] = upperBounds[i];
                        else if (chromosomes[child1][i] < lowerBounds[i])
                            chromosomes[child1][i] = lowerBounds[i];

                        chromosomes[child2][i] = (1 - alpha) * chromosomes[father][i] + alpha * chromosomes[mother][i];
                        if (chromosomes[child2][i] > upperBounds[i])
                            chromosomes[child2][i] = upperBounds[i];
                        else if (chromosomes[child2][i] < lowerBounds[i])
                            chromosomes[child2][i] = lowerBounds[i];
                    }

                    break;

                case CrossoverType.Linear:

                    pos = randomizer.Next(numberOfGenes);
                    do { alpha = randomizer.NextDouble(); } while (alpha == 0);
                    double beta;
                    do { beta = randomizer.NextDouble(); } while (beta == 0);

                    for (int i = 0; i < pos; i++)
                    {
                        chromosomes[child1][i] = chromosomes[father][i];
                        chromosomes[child2][i] = chromosomes[mother][i];
                    }

                    for (int i = pos; i < numberOfGenes; i++)
                    {
                        chromosomes[child1][i] = alpha * chromosomes[father][i] + beta * chromosomes[mother][i];
                        if (chromosomes[child1][i] > upperBounds[i])
                            chromosomes[child1][i] = upperBounds[i];
                        else if (chromosomes[child1][i] < lowerBounds[i])
                            chromosomes[child1][i] = lowerBounds[i];

                        chromosomes[child2][i] = beta * chromosomes[father][i] + alpha * chromosomes[mother][i];
                        if (chromosomes[child2][i] > upperBounds[i])
                            chromosomes[child2][i] = upperBounds[i];
                        else if (chromosomes[child2][i] < lowerBounds[i])
                            chromosomes[child2][i] = lowerBounds[i];
                    }

                    break;

                case CrossoverType.LVD:

                    double min;
                    double max;

                    for (int i = 0; i < numberOfGenes; i++)
                    {
                        alpha = randomizer.NextDouble();
                        if (chromosomes[father][i] >= chromosomes[mother][i])
                        {
                            max = chromosomes[father][i];
                            min = chromosomes[mother][i];
                        }
                        else
                        {
                            max = chromosomes[mother][i];
                            min = chromosomes[father][i];
                        }
                        chromosomes[child1][i] = alpha * lowerBounds[i] + (1 - alpha) * max;
                        chromosomes[child2][i] = alpha * max + (1 - alpha) * upperBounds[i];
                    }

                    break;

                case CrossoverType.SVD:

                    for (int i = 0; i < numberOfGenes; i++)
                    {
                        alpha = randomizer.NextDouble();

                        if (chromosomes[father][i] >= chromosomes[mother][i])
                        {
                            max = chromosomes[father][i];
                            min = chromosomes[mother][i];
                        }
                        else
                        {
                            max = chromosomes[mother][i];
                            min = chromosomes[father][i];
                        }
                        chromosomes[child1][i] = alpha * lowerBounds[i] + (1 - alpha) * min;
                        chromosomes[child2][i] = alpha * min + (1 - alpha) * upperBounds[i];
                    }

                    break;

                case CrossoverType.MOES:

                    for (int i = 0; i < numberOfGenes; i++)
                    {
                        alpha = randomizer.NextDouble();
                        double rand = randomizer.NextDouble();

                        if (chromosomes[father][i] >= chromosomes[mother][i])
                        {
                            max = chromosomes[father][i];
                            min = chromosomes[mother][i];
                        }
                        else
                        {
                            max = chromosomes[mother][i];
                            min = chromosomes[father][i];
                        }
                        chromosomes[child1][i] = alpha * min + (1 - alpha) * max;
                        if (rand > 0.5)
                        {
                            chromosomes[child2][i] = alpha * max + (1 - alpha) * upperBounds[i];
                        }
                        else
                        {
                            chromosomes[child2][i] = alpha * lowerBounds[i] + (1 - alpha) * min;
                        }
                    }

                    break;

            }

        }


        public override void MutateAParent(int parentID, int childID, bool[] mutatedFlag)
        {
            for (int i = 0; i < numberOfGenes; i++)
            {

                if (mutatedFlag[i])
                {
                    double temp1 = randomizer.NextDouble();
                    double temp2 = randomizer.NextDouble();

                    if(temp1 < 0.5)
                    {
                        chromosomes[childID][i] = chromosomes[parentID][i] - temp2 * (chromosomes[parentID][i] - lowerBounds[i]) * Math.Pow(1 - IterationID / IterationLimit, b);
                    }
                    else
                    {
                        chromosomes[childID][i] = chromosomes[parentID][i] + temp2 * (upperBounds[i] - chromosomes[parentID][i]) * Math.Pow(1 - IterationID / IterationLimit, b);
                    }

                }
                else
                {
                    chromosomes[childID][i] = chromosomes[parentID][i];
                }
            }
        }
    }
}
