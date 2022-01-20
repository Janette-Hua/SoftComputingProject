using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FLHuaGALibrary
{
    // template class
    /// <summary>
    ///  Generic GA solver.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericGASolver<T>
    {

        #region DATA FIELD
        protected GAMonitor<T> theMonitor = null;
        protected Random randomizer = new Random();

        protected T[][] chromosomes; // 1st parameter: population size, 2nd parameter: number of variables (genes)
        protected double[] objectiveValues;
        protected double[] fitnessValues;  // for selection operation

        protected T[] soFarTheBestSolution;
        protected double soFarTheBestObjectiveValue;

        double iterationBestObjective;
        double iterationAverageObjective; // will be drawn on the chart to allow us investigate the convergence behavior of our GA
        int iterationBestID = 0; // best chromosome in one iteration

        protected int[] indices;
        bool[][] mutatedGenes;

        protected T[][] selectedChromosomes;
        protected double[] selectedObjectives;

        protected int numberOfGenes;
        protected OptimizationType optimizationType = OptimizationType.Minimization;
        protected ObjectiveFunction<T> objectiveFunction = null;
        protected double leastFitnessFraction = 0.1;

        protected int populationSize = 10;
        protected double crossoverRate = 0.8;
        protected double mutationRate = 0.05; // Gene number-based; not population size-based
        MutationMode mutationType = MutationMode.GeneNumberBased;
        SelectionMode selectionType = SelectionMode.Deterministic;
        int iterationLimit = 200;

        protected int numberOfCrossoverChildren; // populationSize * crossoverRate
        protected int numberOfMutatedChildren;

        // run time data
        int iterationID = 0;
        protected StringBuilder sb; // to update the list box

        #endregion


        #region PROPERTIES & EVENTS
        /// <summary>
        ///  User need to specify the number of chromosomes evolved by the GA.
        /// </summary>
        [Category("GA Parameters"), Description("Number of chromosome employed.")]
        public int PopulationSize { 
            get => populationSize;
            set 
            {
                if(value > 1) populationSize = value;
            } 
        }

        [Category("GA Parameters"), Description("Portion of the parent participating crossover operation.")]
        public double CrossoverRate { 
            get => crossoverRate;
            set { 
                if(value >= 0 && value <= 1.0) crossoverRate = value;
            }
        }

        [Category("GA Parameters"), Description("Ratio of genes out of the total number of genes in the population to be mutated.")]
        public double MutationRate { 
            get => mutationRate;
            set {
                if (value >= 0 && value <= 1.0) mutationRate = value;
            }
        }

        [Category("GA Parameters"), Description("Iteration limit for GA evolution.")]
        public int IterationLimit { get => iterationLimit; set => iterationLimit = value; }

        [Category("GA Parameters"), Description("Fraction of fitness range assigned for the worst chromosome.")]
        public double LeastFitnessFraction { get => leastFitnessFraction; set => leastFitnessFraction = value; }


        [Category("Operation Mode"), Description("Gene number based mutation or population size based mutation mode.")]
        public MutationMode MutationType { get => mutationType; set => mutationType = value; }

        [Category("Operation Mode"), Description("Genetic selection mode setting: deterministic selection selects the best population size chromosome; stochastic selection selects each chromosome proportional to the selection probability.")]
        public SelectionMode SelectionType { get => selectionType; set => selectionType = value; }



        [Browsable(false)]
        public T[][] Chromosomes { get => chromosomes; }

        [Browsable(false)]
        public double CurrentBestObjectiveValue { get => soFarTheBestObjectiveValue; }

        [Browsable(false)]
        public T[] CurrentBestSolution { get => soFarTheBestSolution; }

        [Browsable(false)]
        public Button ResetButton
        {
            get
            {
                if (theMonitor == null) return null;
                else return theMonitor.btnReset;
            }
        }

        [Browsable(false)]
        public Button RunOneIterationButton
        {
            get
            {
                if (theMonitor == null) return null;
                else return theMonitor.btnRunOneIteration;
            }
        }

        [Browsable(false)]
        public Button RunToEndButton
        {
            get
            {
                if (theMonitor == null) return null;
                else return theMonitor.btnRunToEnd;
            }
        }

        [Browsable(false)]
        public int IterationID { get => iterationID; set => iterationID = value; }

        [Browsable(false)]
        public double IterationBestObjective { get => iterationBestObjective; set => iterationBestObjective = value; }

        [Browsable(false)]
        public double IterationAverageObjective { get => iterationAverageObjective; set => iterationAverageObjective = value; }

        [Browsable(false)]
        public double SoFarTheBestObjectiveValue { get => soFarTheBestObjectiveValue; set => soFarTheBestObjectiveValue = value; }

        [Browsable(false)]
        public int NumberOfCrossoverChildren { get => numberOfCrossoverChildren; set => numberOfCrossoverChildren = value; }

        [Browsable(false)]
        public int NumberOfMutatedChildren { get => numberOfMutatedChildren; set => numberOfMutatedChildren = value; }

        [Browsable(false)]
        public int NumberOfGenes { get => numberOfGenes; set => numberOfGenes = value; }

        [Browsable(false)]
        public double[] ObjectiveValues { get => objectiveValues; set => objectiveValues = value; }

        [Browsable(false)]
        public OptimizationType OptimizationType { get => optimizationType; set => optimizationType = value; }

        public event EventHandler AfterInitialization; // delegate: a collection of function pointers
        public event EventHandler OneIterationCompleted;
        public event EventHandler SoFarTheBestSolutionUpdated;

        /// <summary>
        ///  Fire the AfterInitialization event;
        /// </summary>
        protected void OnAfterInitialization()
        {
            if (AfterInitialization != null) AfterInitialization(this, null);
        }

        protected void OnOneIterationCompleted()
        {
            if (OneIterationCompleted != null) OneIterationCompleted(this, null);
        }

        protected void OnSoFarTheBestSolutionUpdated()
        {
            if (SoFarTheBestSolutionUpdated != null) SoFarTheBestSolutionUpdated(this, null);
        }

        #endregion

        // constructor
        /// <summary>
        ///  Generic GA solver to be inherited from other classes which may use different encoding methods, e.g., binary-encoded, integer-encoded...
        /// </summary>
        /// <param name="numberOfVariables"></param>
        /// <param name="optimizationType"></param>
        /// <param name="objectiveFunction"></param>
        /// <param name="hostPanelForMonitor"></param>
        public GenericGASolver(int numberOfVariables, OptimizationType optimizationType, 
            ObjectiveFunction<T> objectiveFunction, SplitterPanel hostPanelForMonitor = null)
        {
            numberOfGenes = numberOfVariables;
            this.optimizationType = optimizationType;
            this.objectiveFunction = objectiveFunction;

            if(hostPanelForMonitor != null)
            {
                // create monitor object and add it to the host panel
                theMonitor = new GAMonitor<T>(this);
                hostPanelForMonitor.Controls.Add(theMonitor);
                theMonitor.Dock = DockStyle.Fill;
                theMonitor.prgGA.SelectedObject = this;
            }

            soFarTheBestSolution = new T[numberOfVariables];
        }

        // Helping functions
        void SetFitnessFromObjectives()
        {
            // leastFitnessFraction
            int total = populationSize + numberOfCrossoverChildren + numberOfMutatedChildren;

            //  Avoid this call objectiveValues. Skip(0).Take(total).ToArray(); it involves dynamical memory allocation.
            //double[] tempObjValues = objectiveValues.Skip(0).Take(total).ToArray(); // get a subset of the objectiveValues array
            //double o_max = tempObjValues.Max();
            //double o_min = tempObjValues.Min();

            int counter = 0;
            double o_max = double.MinValue;
            double o_min = double.MaxValue;
            while(counter < total)
            {
                if (objectiveValues[counter] > o_max) o_max = objectiveValues[counter];
                if (objectiveValues[counter] < o_min) o_min = objectiveValues[counter];

                counter++;
            }


            double b = (leastFitnessFraction * (o_max - o_min)) > Math.Pow(10, -5) ? (leastFitnessFraction * (o_max - o_min)) : Math.Pow(10, -5);

            if (optimizationType == OptimizationType.Maximization)
            {
                // maximization

                for (int i = 0; i < total; i++)
                {
                    fitnessValues[i] = b + (objectiveValues[i] - o_min);
                }
            }
            else
            {
                // minimization

                for (int i = 0; i < total; i++)
                {
                    fitnessValues[i] = b + (o_max - objectiveValues[i]);
                }
            }
        }

        
        public void ShuffleAnArray(int[] array, int limit)
        {
            for (int i = 0; i < limit; i++) array[i] = i;

            int pos;   // pick a random number and store it in "pos"
            int temp;  // temporary storage for swapping two numbers
            int n = limit - 1;     // starting index of shuffling

            // shuffle indices for 0 to limit - 1
            for(int i = 0; i < limit; i++)
            {
                pos = randomizer.Next(n);
                if (pos == n) n--;
                else
                {
                    temp = array[n];
                    array[n] = array[pos];
                    array[pos] = temp;
                    n--;
                }
            }
        }


        /// <summary>
        ///  Reallocated memory for the GA operations and do the chromosome initialization.
        /// </summary>
        public void Reset()
        {
            // variable reset
            iterationID = 0;
            if (optimizationType == OptimizationType.Maximization) soFarTheBestObjectiveValue = double.MinValue;
            else soFarTheBestObjectiveValue = double.MaxValue;

            // memory reallocation
            int ThreeTimeSize = populationSize * 3;
            chromosomes = new T[ThreeTimeSize][];
            for (int r = 0; r < ThreeTimeSize; r++)
            {
                chromosomes[r] = new T[numberOfGenes];
            }
            indices = new int[ThreeTimeSize];
            objectiveValues = new double[ThreeTimeSize];
            fitnessValues = new double[ThreeTimeSize];
            selectedObjectives = new double[populationSize];
            selectedChromosomes = new T[populationSize][];
            for (int r = 0; r < populationSize; r++)
            {
                selectedChromosomes[r] = new T[numberOfGenes];
            }

            sb = new StringBuilder();

            mutatedGenes = new bool[populationSize][];
            for (int r = 0; r < populationSize; r++) mutatedGenes[r] = new bool[numberOfGenes];

            // ...

            // set initial solution
            InitializePopulation();  // will be overridden by the children

            // Fire AfterInitialization
            OnAfterInitialization();
 
            // evaluate objectives of the population
            for(int i = 0; i < populationSize; i++)
            {
                objectiveValues[i] = objectiveFunction(chromosomes[i]);
            }

            if(theMonitor != null)
            {
                // reset to initial condition
                theMonitor.btnRunToEnd.Enabled = theMonitor.btnRunOneIteration.Enabled = true;
                foreach (Series s in theMonitor.chtGA.Series) s.Points.Clear();
            }
        }

        public virtual void InitializePopulation()
        {
            throw new NotImplementedException();
        }

        public void RunOneIteration()
        {
            PerformCrossoverOperation();
            PerformMutationOperation();
            UpdateSoFarTheBestSolutionAndObjective();
            PerformSelectionOperation();

            // Fire OneIterationCompleted event
            OnOneIterationCompleted();

            iterationID++;

            if(theMonitor != null)
            {
                theMonitor.chtGA.Series[0].Points.AddXY(iterationID, iterationAverageObjective); // iterationAverageObjective);
                theMonitor.chtGA.Series[1].Points.AddXY(iterationID, iterationBestObjective); // iterationBestObjective);
                theMonitor.chtGA.Series[2].Points.AddXY(iterationID, soFarTheBestObjectiveValue); // soFarTheBestObjectiveValue);

                theMonitor.chtGA.Update();

                // update all the chromosomes in this iteration on the list box
                UpdateChromosomesOnListBox();
            }  
        }

        public virtual void UpdateChromosomesOnListBox()
        {
            throw new NotImplementedException();
        }

        public void UpdateSoFarTheBestSolutionAndObjective()
        {
            // loop through objectiveValue array for parents and children chromosomes
            // find the iteration best solution id first
            // check whether its value is better than the current best objective
            // if it does, then update the best objective value and do gene-wise copy the iteration best
            // chromosomes to so-far-the-best solution.
            // calculate    iterationBestObjective   and   iterationAverageObjective
            int total = populationSize + numberOfCrossoverChildren + numberOfMutatedChildren;
            bool sofarthebestisupdated = false; // initialize to false, i.e., no value update in this iteration
            double sum = 0; // to keep track of the objective values in this iteration

            if (optimizationType == OptimizationType.Maximization)
            {
                // maximization

                iterationBestObjective = double.MinValue;
                for (int i = 0; i < total; i++)
                {                  
                    if(objectiveValues[i] > iterationBestObjective)
                    {
                        iterationBestObjective = objectiveValues[i];
                        iterationBestID = i;
                    }

                    sum += objectiveValues[i];
                }

                if (iterationBestObjective > soFarTheBestObjectiveValue) sofarthebestisupdated = true;
                iterationAverageObjective = sum / total;
            }
            else
            {
                // minimization

                iterationBestObjective = double.MaxValue;
                for (int i = 0; i < total; i++)
                {                  
                    if (objectiveValues[i] < iterationBestObjective)
                    {
                        iterationBestObjective = objectiveValues[i];
                        iterationBestID = i;
                    }

                    sum += objectiveValues[i];
                }

                if (iterationBestObjective < soFarTheBestObjectiveValue) sofarthebestisupdated = true;
                iterationAverageObjective = sum / total;
            }

            // If so far the best is updated in this iteration, update objective value and the solution
            if (sofarthebestisupdated) // if true
            {
                // update so far the best objective value
                soFarTheBestObjectiveValue = iterationBestObjective;
                for (int i = 0; i < numberOfGenes; i++)
                    soFarTheBestSolution[i] = chromosomes[iterationBestID][i];

                // Fire so far the best solution is updated event
                OnSoFarTheBestSolutionUpdated();

                // update the information on theMonitor
                if(theMonitor != null) UpdateInformationOntheMonitor();
            }
        }

        public virtual void UpdateInformationOntheMonitor()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        ///  Gene-based or Population-based
        ///   Gene-based: randomly generate number to select the gene to be mutated 
        ///     total number of mutated genes = (mutation rate) * (total number of genes)
        ///     total number of genes = (number of chromosomes) * (number of genes in a chromosone)
        ///   Poplation-based: randomly select a chromosome to do mutation
        /// </summary>
        protected void PerformMutationOperation()
        {
            for (int i = 0; i < populationSize; i++)
            {
                indices[i] = 0;
                for (int j = 0; j < numberOfGenes; j++)
                {
                    mutatedGenes[i][j] = false; // clean up
                }
            }

            // Gene-based
            if (MutationType == MutationMode.GeneNumberBased)
            {
                int limit = populationSize * numberOfGenes;
                int numberOfMutatedGenes = (int)(mutationRate * limit);              

                for(int i = 0; i < numberOfMutatedGenes; i++)
                {
                    int serialPosition = randomizer.Next(limit); // randomly generate number to select the gene to be mutated
                    int parentID, geneID;
                    parentID = serialPosition / numberOfGenes;
                    geneID = serialPosition % numberOfGenes;
                    indices[parentID] = int.MinValue;      // mark the parent chromosome
                    mutatedGenes[parentID][geneID] = true; // mark the gene to be mutated
                }              
            }
            else
            {
                // Population-based

                numberOfMutatedChildren = (int)(populationSize * 0.2);  // mutationRate = 0.2

                for (int i = 0; i < numberOfMutatedChildren; i++)
                {
                    int parentID = randomizer.Next(populationSize); // randomly generate number to select the chromosome to be mutated
                    indices[parentID] = int.MinValue;  // mark the parent chromosome
                    for(int j = 0; j < numberOfGenes; j++)
                    {
                        mutatedGenes[parentID][j] = true; // mark the gene to be mutated
                    }                
                }
            }

            numberOfMutatedChildren = 0;
            int childID = populationSize + numberOfCrossoverChildren;
            for (int i = 0; i < populationSize; i++)
            {
                if (indices[i] == int.MinValue)
                {
                    MutateAParent(i, childID, mutatedGenes[i]);
                    objectiveValues[childID] = objectiveFunction(chromosomes[childID]);
                    childID++;
                    numberOfMutatedChildren++;
                }
            }
        }

        public virtual void MutateAParent(int parentID, int childID, bool[] mutatedFlag)
        {
            throw new NotImplementedException();
        }

        protected void PerformCrossoverOperation()
        {
            numberOfCrossoverChildren = (int)(populationSize * crossoverRate);
            if (numberOfCrossoverChildren % 2 == 1) numberOfCrossoverChildren--; // prefer the number of children to be an even number since we generate a "pair" of children

            //indices = new int[populationSize * 3];
            ShuffleAnArray(indices, populationSize);

            int father, mother, child1 = populationSize, child2 = populationSize + 1;
            for (int i = 0; i < numberOfCrossoverChildren; i+=2)
            {
                
                father = indices[i];
                mother = indices[i + 1];
                CrossoverAPairParent(father, mother, child1, child2);
                objectiveValues[child1] = objectiveFunction(chromosomes[child1]);
                objectiveValues[child2] = objectiveFunction(chromosomes[child2]);
                child1 += 2;
                child2 += 2;
            }
        }

        public virtual void CrossoverAPairParent(int father, int mother, int child1, int child2)
        {
            throw new NotImplementedException();
        }

        public void PerformSelectionOperation()
        {
            // calculate fitness for all chromosomes
            SetFitnessFromObjectives();
            //
            int total = populationSize + numberOfCrossoverChildren + numberOfMutatedChildren;


            // do the selection
            if (SelectionType == SelectionMode.Deterministic)
            {
                for (int i = 0; i < total; i++) indices[i] = i;
                Array.Sort(fitnessValues, indices, 0, total); // sort two arrays according to the values in the first array (from smallest to largest)
                Array.Reverse(indices, 0, total);
            }
            else
            {
                // Stochastic selection
                // use fitness as relative probability for each chromosome and to
                // spin roulette wheel populationSize times to set the first populationSize indices

                double fitnessTotal = 0;
                for (int i = 0; i < total; i++)
                {
                    fitnessTotal += fitnessValues[i];
                }


                int counter = 0;
                while (counter < populationSize)
                {

                    double randomselect = randomizer.Next((int)(fitnessTotal / 1));
                    fitnessTotal = 0;

                    for (int j = 0; j < total; j++)
                    {

                        fitnessTotal += fitnessValues[j];

                        if (randomselect <= fitnessTotal)
                        {
                            indices[counter] = j;
                            counter += 1;

                            break;
                        }

                    }
                }

                //for (int i = 0; i < total; i++)
                //{
                //    // reset indices
                //    indices[i] = i;

                //    // calculate the threshold
                //    if (i == 0) threshold[i] = fitnessValues[i];
                //    else threshold[i] = fitnessValues[i] + threshold[i - 1];
                //}

                //double rdmNumber;
                //for(int i = 0; i < populationSize; i++)
                //{
                //    rdmNumber = randomizer.NextDouble() * threshold[total - 1];

                //    for(int j = 0; j < total; j++)
                //    {
                //        if (rdmNumber > threshold[j]) continue;
                //        else
                //        {
                //            indices[i] = j;
                //            break;
                //        }
                //    }
                //}
            }


            // Gene-wise copy genes from selected parents and children to temporary matrix
            for (int i = 0; i < populationSize; i++)
            {
                for (int j = 0; j < numberOfGenes; j++)
                    selectedChromosomes[i][j] = chromosomes[indices[i]][j];
                selectedObjectives[i] = objectiveValues[indices[i]];
            }

            // Gene-wise copy genes back to our population
            for (int i = 0; i < populationSize; i++)
            {
                for (int j = 0; j < numberOfGenes; j++)
                    chromosomes[i][j] = selectedChromosomes[i][j];
                objectiveValues[i] = selectedObjectives[i];
            }
        }

        public void RunToEnd()
        {
            while (iterationID <= iterationLimit) RunOneIteration();
            MessageBox.Show("GA execution complete!", "GA Stop", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

    }

    /// <summary>
    ///  The mutation operation mode.
    /// </summary>
    public enum MutationMode { 
        /// <summary>
        ///  The mutation rate is based on the total number of genes.
        /// </summary>
        GeneNumberBased,

        /// <summary>
        ///  The mutation rate is based on the total number of populations.
        /// </summary>
        PopulationSizeBased
    }
    public enum OptimizationType { Minimization, Maximization }
    public enum SelectionMode { Deterministic, Stochastic }
    public delegate double ObjectiveFunction<T>(T[] aSolution);
}
