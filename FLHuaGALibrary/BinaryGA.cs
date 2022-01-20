using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FLHuaGALibrary
{
    public enum BinaryCrossoverOperator
    {
        OnePointCut, TwoPointCut, NPointCut 
    }

    public class BinaryGA : GenericGASolver<byte>  // inherit from GenericGASolver<>
    {

        //   GA monitor control should NOT have any controls that display the encoded solution of the problem(in particular this job assignment problem).
        // job assignment problem-specific data
        //protected byte[,] solutionMatrix; // Two-dimensional array
        //protected int[] rowViolationCounts;
        //protected int[] colViolationCounts;
        //protected double shortestTime;

        // PROPERTIES
        int[] poss;

        [Category("Binary Encoded GA"), Description("The crossover operator of a binary-encoded GA solver.")]
        public BinaryCrossoverOperator CrossoverOperator { set; get; } = BinaryCrossoverOperator.OnePointCut;

        /// <summary>
        /// Create a Binary GA Solver
        /// </summary>
        /// <param name="numberOfVariables"></param>
        /// <param name="optimizationType"></param>
        /// <param name="objectiveFunction"> The delegate to the objective function </param>

        public BinaryGA(int numberOfVariables, OptimizationType optimizationType, ObjectiveFunction<byte> objectiveFunction,
            SplitterPanel hostPanelForMonitor) : 
            base(numberOfVariables, optimizationType, objectiveFunction, hostPanelForMonitor)
        {
            //int b = soFarTheBestSolution[0] * 8;
            poss = new int[numberOfVariables];
        }

        public override void InitializePopulation()
        {
            for(int r = 0; r < populationSize; r++)
            {
                for(int c = 0; c < numberOfGenes; c++)
                {
                    chromosomes[r][c] = (byte)randomizer.Next(2);
                }
            }
           
        }

        public override void CrossoverAPairParent(int father, int mother, int child1, int child2)
        {
            //chromosomes[father] = ;
            // base.CrossoverAPairParent(father, mother, child1, child2);

            switch (CrossoverOperator)
            {
                case BinaryCrossoverOperator.OnePointCut:
                    int cutPos = randomizer.Next(numberOfGenes);
                    for (int i = 0; i < numberOfGenes; i++)
                    {
                        if (i < cutPos)
                        {
                            chromosomes[child1][i] = chromosomes[father][i];
                            chromosomes[child2][i] = chromosomes[mother][i];
                        }
                        else
                        {
                            chromosomes[child1][i] = chromosomes[mother][i];
                            chromosomes[child2][i] = chromosomes[father][i];
                        }
                    }
                    break;
                case BinaryCrossoverOperator.TwoPointCut:
                    int cutPos1 = randomizer.Next(numberOfGenes);
                    if (cutPos1 == 0) cutPos1++;
                    int cutPos2 = randomizer.Next(cutPos1);

                    for (int i = 0; i < numberOfGenes; i++)
                    {
                        if (i <= cutPos2 || i >= cutPos1)
                        {
                            chromosomes[child1][i] = chromosomes[father][i];
                            chromosomes[child2][i] = chromosomes[mother][i];
                        }
                        else
                        {
                            chromosomes[child1][i] = chromosomes[mother][i];
                            chromosomes[child2][i] = chromosomes[father][i];
                        }
                    }

                    break;
                case BinaryCrossoverOperator.NPointCut:
                    int n = 1 + randomizer.Next(numberOfGenes / 2);  // n number of cutpoints
                    for(int i = 0; i < n; i++)
                    {
                        poss[i] = randomizer.Next(numberOfGenes);
                    }
                    Array.Sort(poss, 0, n);

                    // Do the gene assignments.
                    for(int i = 0; i < numberOfGenes; i++)
                    {
                        chromosomes[child1][i] = chromosomes[father][i];
                        chromosomes[child2][i] = chromosomes[mother][i];
                    }

                    foreach(int i in poss)
                    {
                        chromosomes[child1][i] = chromosomes[mother][i];
                        chromosomes[child2][i] = chromosomes[father][i];
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
                    if (chromosomes[parentID][i] == 1) chromosomes[childID][i] = 0;
                    else chromosomes[childID][i] = 1;
                }
                else
                {
                    chromosomes[childID][i] = chromosomes[parentID][i];
                }
            }
        }


        public override void UpdateChromosomesOnListBox()
        {
            theMonitor.lbPopulation.Items.Clear();
            
            sb.Clear();
            for (int i = 0; i < populationSize + numberOfCrossoverChildren + numberOfMutatedChildren; i++)
            {
                sb.Clear();

                if (i < 9)
                {
                    if (i < populationSize)
                    {
                        sb.Append($"P00{i + 1} ");
                    }
                    else if (i >= populationSize && i < populationSize + numberOfCrossoverChildren)
                    {
                        sb.Append($"C00{i + 1} ");
                    }
                    else
                    {
                        sb.Append($"M00{i + 1} ");
                    }
                }
                else if (i >= 9 && i < 100)
                {
                    if (i < populationSize)
                    {
                        sb.Append($"P0{i + 1} ");
                    }
                    else if (i >= populationSize && i < populationSize + numberOfCrossoverChildren)
                    {
                        sb.Append($"C0{i + 1} ");
                    }
                    else
                    {
                        sb.Append($"M0{i + 1} ");
                    }
                }

                for (int j = 0; j < numberOfGenes; j++)
                {
                    if (j % (int)Math.Sqrt(numberOfGenes) == 0) sb.Append(" ");
                    sb.Append($"{chromosomes[i][j]} ");
                }
                sb.Append($" obj: {objectiveValues[i]}");
                theMonitor.lbPopulation.Items.Add(sb);

                if (i == (populationSize - 1) || i == (populationSize + numberOfCrossoverChildren - 1)) theMonitor.lbPopulation.Items.Add("\n");
            }
        }

        public override void UpdateInformationOntheMonitor()
        {
            // update so far the best objective value on the text box
            theMonitor.tbxBestObjectiveValue.Text = soFarTheBestObjectiveValue.ToString();

            // update the current best solution on the list box
            UpdateCurrentBestSolutionOnListBox();

            // update hard constraint violations on the list box
            //UpdateHardConstraintViolations(soFarTheBestSolution);

            // update shortest time
            //UpdateShortestTime(soFarTheBestSolution);

        }


        public void UpdateCurrentBestSolutionOnListBox()
        {
            theMonitor.lbBestSolution.Items.Clear();
            sb.Clear();
            int numberOfJobs = (int)(Math.Sqrt(numberOfGenes));
            for (int j = 0; j < numberOfGenes; j++)
            {
                sb.Append(soFarTheBestSolution[j]);
                if (j % numberOfJobs == numberOfJobs - 1)
                {
                    sb.Append("\n");
                    theMonitor.lbBestSolution.Items.Add(sb);
                    sb.Clear();
                }
            }
        }

        

        //public void UpdateShortestTime(byte[] soFarTheBestSolution)
        //{
        //    int numberOfVariables = (int)Math.Sqrt(numberOfGenes);

        //    for (int i = 0; i < numberOfVariables; i++)
        //    {
        //        for (int j = 0; j < numberOfVariables; j++)
        //        {
        //            solutionMatrix[i, j] = soFarTheBestSolution[(i * numberOfVariables) + j];                  
        //        }
        //    }
        //}
    }
}
