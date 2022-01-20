using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FLHuaGALibrary
{

    /// <summary>
    ///  Permutation GA Solver.
    /// </summary>
    public class PermutationGA : GenericGASolver<int>
    {
        // data field
        protected int[] m; // scrap memory to facilitate the operation in CrossoverAPairParent()
        protected int[] arr; // scrap memory to facilitate the operation in MutateAParent()
        protected int[] tempArr; // scrap memory to facilitate the operation in MutateAParent()
        protected int[] arrPos;
        protected int index = 0; // scrap memory to facilitate the operation in CrossoverAPairParent()
        protected int i1, i2; // i1 and i2 are cut locations of a Two-Cut PMX crossover
        protected int[] p1;
        protected int[] p2;
        protected int[] c1;
        protected int[] c2;

        // property
        [Category("Permutation GA"), Description("The crossover operator of a permutation GA solver.")]
        public PermutationCrossoverOperator CrossoverOperator { set; get; } = PermutationCrossoverOperator.PartialMappedX;

        [Category("Permutation GA"), Description("The mutation operator of a permutation GA solver.")]
        public PermutationMutationOperator MutationOperator { set; get; } = PermutationMutationOperator.Inversion;

        // constructor
        public PermutationGA(int numberOfVariables, OptimizationType optimizationType,
            ObjectiveFunction<int> objectiveFunction, SplitterPanel hostPanelForMonitor = null) : 
            base(numberOfVariables, optimizationType, objectiveFunction, hostPanelForMonitor)
        {
            m = new int[numberOfVariables]; // Allocate the memory just once in the constructor.
            arr = new int[numberOfVariables]; // Allocate the memory just once in the constructor.
            tempArr = new int[3]; // Allocate the memory just once in the constructor.
            p1 = new int[numberOfVariables];
            p2 = new int[numberOfVariables];
            c1 = new int[numberOfVariables];
            c1 = new int[numberOfVariables];
        }


        public override void CrossoverAPairParent(int father, int mother, int child1, int child2)
        {
            switch (CrossoverOperator)
            {
                case PermutationCrossoverOperator.PartialMappedX:

                    p1 = chromosomes[father];
                    p2 = chromosomes[mother];
                    c1 = chromosomes[child1];
                    c2 = chromosomes[child2];

                    i1 = randomizer.Next(numberOfGenes); // i1 and i2 are cut locations of a Two-Cut PMX crossover
                    i2 = randomizer.Next(numberOfGenes);

                    // if i1 > i2, swap i1 and i2
                    int temp;
                    if (i1 > i2)
                    {
                        temp = i1;
                        i1 = i2;
                        i2 = temp;
                    }

                    for (int i = 0; i < numberOfGenes; i++) m[i] = -1;

                    // construct the map
                    for (int i = i1; i < i2; i++)
                    {
                        if (p1[i] == p2[i]) continue; // no mapping
                        if(m[p1[i]] == -1 && m[p2[i]] == -1)
                        {
                            m[p1[i]] = p2[i];
                            m[p2[i]] = p1[i];
                        }
                        else if(m[p1[i]] == -1)
                        {
                            m[p1[i]] = m[p2[i]];
                            m[m[p2[i]]] = p1[i];
                            m[p2[i]] = -2;
                        }
                        else if(m[p2[i]] == -1)
                        {
                            m[p2[i]] = m[p1[i]];
                            m[m[p1[i]]] = p2[i];
                            m[p1[i]] = -2;
                        }
                        else
                        {
                            m[m[p2[i]]] = m[p1[i]];
                            m[m[p1[i]]] = m[p2[i]];
                            m[p1[i]] = -3;
                            m[p2[i]] = -3;
                        }
                    }

                    // assign genes to children
                    for(int i = 0; i < numberOfGenes; i++)
                    {
                        if(i1 <= i && i < i2)
                        {
                            c1[i] = p2[i];
                            c2[i] = p1[i];
                        }
                        else
                        {
                            if (m[p1[i]] < 0) c1[i] = p1[i];
                            else c1[i] = m[p1[i]];
                            if (m[p2[i]] < 0) c2[i] = p2[i];
                            else c2[i] = m[p2[i]];
                        }
                    }

                    c1.CopyTo(chromosomes[child1], 0);
                    c2.CopyTo(chromosomes[child2], 0);

                    break;
                case PermutationCrossoverOperator.OrderX:

                    // copy by value: hard copy the genes' values one by one to the scrap memory
                    /*
                        The below is copy by "reference".
                        That is, a change in the value of p1 will also change the value in the chromosomes.
                            p1 = chromosomes[father];
                            p2 = chromosomes[mother];
                            c1 = chromosomes[child1];
                            c2 = chromosomes[child2];
                        Since we will mark out some positions in the calculation, we don't want the value to be changed to the original chromosomes.
                    */
                    for (int i = 0; i < numberOfGenes; i++)
                    {
                        p1[i] = chromosomes[father][i];
                        p2[i] = chromosomes[mother][i];
                    }

                    c1 = chromosomes[child1];
                    c2 = chromosomes[child2];

                    i1 = randomizer.Next(numberOfGenes); // i1 and i2 are cut locations of a Two-Cut Order crossover
                    i2 = randomizer.Next(numberOfGenes);

                    // if i1 > i2, swap i1 and i2
                    if (i1 > i2)
                    {
                        temp = i1;
                        i1 = i2;
                        i2 = temp;
                    }

                    for (int i = 0; i < numberOfGenes; i++) m[i] = p2[i];

                    OrderX(i1, i2, p1, p2, c1);
                    OrderX(i1, i2, m, p1, c2);

                    //c1.CopyTo(chromosomes[child1], 0);
                    //c2.CopyTo(chromosomes[child2], 0);

                    break;

                case PermutationCrossoverOperator.PositionBasedX:

                    for (int i = 0; i < numberOfGenes; i++)
                    {
                        p1[i] = chromosomes[father][i]; // copy by value
                        p2[i] = chromosomes[mother][i];
                    }

                    c1 = chromosomes[child1]; // copy by reference
                    c2 = chromosomes[child2];

                    int numberOfPositions = randomizer.Next(1, numberOfGenes); // the randomly generated number of positions to select
                    int min = 0, max = numberOfGenes - 1;
                    int[] arrPos = new int[numberOfPositions];
                    index = 0;

                    // sample without replacement
                    while (numberOfPositions > 0)
                    {
                        double r = randomizer.Next() / (int.MaxValue + 1.0);
                        if (r * (max - min + 1) < numberOfPositions)
                        {
                            arrPos[index] = min;
                            index++;
                            --numberOfPositions;
                        }
                        ++min;
                    }

                    for (int i = 0; i < numberOfGenes; i++) m[i] = p2[i]; // copy the genes to the scrap memory 

                    PositionBasedX(arrPos, p1, p2, c1);
                    PositionBasedX(arrPos, m, p1, c2);

                    //c1.CopyTo(chromosomes[child1], 0);
                    //c2.CopyTo(chromosomes[child2], 0);

                    break;
                case PermutationCrossoverOperator.OrderBasedX:

                    for (int i = 0; i < numberOfGenes; i++)
                    {
                        p1[i] = chromosomes[father][i]; // copy by value
                        p2[i] = chromosomes[mother][i];
                    }

                    c1 = chromosomes[child1]; // copy by reference
                    c2 = chromosomes[child2];

                    numberOfPositions = randomizer.Next(1, numberOfGenes); // the randomly generated number of positions to select
                    min = 0;
                    max = numberOfGenes - 1;
                    arrPos = new int[numberOfPositions];
                    index = 0;

                    // sample without replacement
                    while (numberOfPositions > 0)
                    {
                        double r = randomizer.Next() / (int.MaxValue + 1.0);
                        if (r * (max - min + 1) < numberOfPositions)
                        {
                            arrPos[index] = min;
                            index++;
                            --numberOfPositions;
                        }
                        ++min;
                    }

                    for (int i = 0; i < numberOfGenes; i++) m[i] = p2[i]; // copy the genes to the scrap memory

                    OrderBasedX(arrPos, p1, p2, c1);
                    OrderBasedX(arrPos, m, p1, c2);

                    // test
                    //int[] pTest1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                    //int[] pTest2 = new int[] { 6, 4, 5, 7, 9, 8, 3, 1, 2 };

                    //numberOfPositions = 4;
                    //arr = new int[] { 1, 4, 5, 8 };

                    //numberOfGenes = 9;
                    //m = new int[9];
                    //int[] cTest1 = new int[9];


                    //for (int i = 0; i < numberOfGenes; i++) m[i] = pTest2[i]; // copy the genes to the scrap memory 

                    //for (int i = 0; i < numberOfGenes; i++) cTest1[i] = -1; // mark out all the genes in the child

                    //for (int i = 0; i < arr.Length; i++)
                    //{
                    //    for (int j = 0; j < numberOfGenes; j++)
                    //    {
                    //        if (pTest2[j] == pTest1[arr[i]])
                    //        {
                    //            pTest2[j] = -1; // mark out the corresponding genes in parent 2
                    //            cTest1[j] = -2; // mark out the corresponding genes in child 1
                    //        }
                    //    }
                    //}

                    //index = 0;
                    //for (int i = 0; i < numberOfGenes; i++)
                    //{
                    //    if (cTest1[i] == -2)
                    //    {
                    //        cTest1[i] = pTest1[arr[index]]; // copy the genes from parent 1 to child 1
                    //        index++;
                    //    }
                    //    else
                    //    {
                    //        cTest1[i] = pTest2[i]; // copy the genes from parent 2 to child 1
                    //    }
                    //}

                    break;
                case PermutationCrossoverOperator.CycleX:

                    for (int i = 0; i < numberOfGenes; i++)
                    {
                        p1[i] = chromosomes[father][i]; // copy by value
                        p2[i] = chromosomes[mother][i];
                    }

                    c1 = chromosomes[child1]; // copy by reference
                    c2 = chromosomes[child2];


                    for (int i = 0; i < numberOfGenes; i++) arr[i] = -1;

                    index = temp = 0;
                    arr[index] = 0;
                    while (p2[temp] != p1[0]) // look at the element at the same position in p2
                    {
                        for (int i = 1; i < numberOfGenes; i++)  // go to the position with the same element in p1
                        {
                            if (p1[i] == p2[temp])
                            {
                                index++;
                                arr[index] = i;  // add the position of this element to the cycle
                                temp = i;
                                break;
                            }
                        }
                    }

                    Array.Sort(arr);
                    index = 0;
                    while (arr[index] == -1) index++;

                    for (int i = 0; i < numberOfGenes; i++)
                    {
                        if (i == arr[index])
                        {
                            c1[i] = p1[arr[index]];  // copy the genes from parent 1 to child 1
                            c2[i] = p2[arr[index]];  // copy the genes from parent 2 to child 2
                            index++;
                            if (index > numberOfGenes - 1) index = numberOfGenes - 1; // guarding condition
                        }
                        else
                        {
                            c1[i] = p2[i];
                            c2[i] = p1[i];
                        }
                    }

                    // test
                    //int[] pTest1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                    //int[] pTest2 = new int[] { 6, 4, 5, 7, 9, 8, 3, 1, 2 };

                    //numberOfGenes = 9;
                    //m = new int[9];
                    //int[] cTest1 = new int[9];
                    //int[] cTest2 = new int[9];
                    //arr = new int[9];

                    //for (int i = 0; i < 9; i++) arr[i] = -1;

                    //index = temp = 0;
                    //arr[index] = 0;
                    //while (pTest2[temp] != pTest1[0])
                    //{
                    //    for (int i = 0; i < numberOfGenes; i++)
                    //    {
                    //        if (pTest1[i] == pTest2[temp])
                    //        {
                    //            index++;
                    //            arr[index] = i;
                    //            temp = i;
                    //            break;
                    //        }
                    //    }
                    //}

                    //index = 0;
                    //for (int i = 0; i < numberOfGenes; i++)
                    //{
                    //    if (i == arr[index])
                    //    {
                    //        cTest1[i] = pTest1[arr[index]];  // copy the genes from parent 1 to child 1
                    //        cTest2[i] = pTest2[arr[index]];
                    //        index++;
                    //        if (index > numberOfGenes - 1) index = numberOfGenes - 1;
                    //    }
                    //    else
                    //    {
                    //        cTest1[i] = pTest2[i];
                    //        cTest2[i] = pTest1[i];
                    //    }
                    //}


                    break;
                case PermutationCrossoverOperator.SubtourExchange:

                    int subtourLength = numberOfGenes - 1;

                    while (!(SubtourExchange(father, mother, child1, child2, subtourLength)))
                    {
                        subtourLength--;
                        if (subtourLength == 1) break; // subtour not found
                    }

                    if(subtourLength == 1) // subtour not found, copy the genes from parent to child
                    {
                        for (int i = 0; i < numberOfGenes; i++)
                        {
                            chromosomes[child1][i] = chromosomes[father][i];
                            chromosomes[child2][i] = chromosomes[mother][i];
                        }
                    }



                    break;
            }
        }

        private bool SubtourExchange(int father, int mother, int child1, int child2, int Length)
        {
            List<int> Subtour1 = new List<int> { };
            List<int> Subtour2 = new List<int> { };

            for (int i = 0; i < numberOfGenes - Length; i++)
            {
                Subtour1 = RefillSubtour(i, Length, chromosomes[father]);

                for (int j = 0; j < numberOfGenes - Length; j++)
                {
                    Subtour2 = RefillSubtour(j, Length, chromosomes[mother]);

                    if (Subtour1.Equals(Subtour2))
                    {
                        int temp = 0;

                        // child1
                        while (temp < numberOfGenes)
                        {
                            if (temp != i)
                            {
                                chromosomes[child1][i] = chromosomes[father][i];
                                temp++;
                            }
                            else
                            {
                                for (int t = 0; t < Length; t++)
                                {
                                    chromosomes[child1][i] = Subtour2[t];
                                    temp++;
                                }
                            }
                        }


                        //child2
                        temp = 0;

                        while (temp < numberOfGenes)
                        {
                            if (temp != j)
                            {
                                chromosomes[child2][i] = chromosomes[mother][i];
                                temp++;
                            }
                            else
                            {
                                for (int t = 0; t < Length; t++)
                                {
                                    chromosomes[child2][i] = Subtour1[t];
                                    temp++;
                                }
                            }
                        }

                        return true; // subtour found!
                    }

                }
            }
            return false; // subtour not found
        }

        private List<int> RefillSubtour(int start, int length, int[] data)
        {
            List<int> subtour = new List<int> { };
            for (int k = 0; k < length; k++)
            {
                subtour.Add(data[start + k]);
            }
            return subtour;
        }

        private void OrderBasedX(int[] arr, int[] p1, int[] p2, int[] c1)
        {
            for (int i = 0; i < numberOfGenes; i++) c1[i] = -1; // mark out all the genes in the child

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < numberOfGenes; j++)
                {
                    if (p2[j] == p1[arr[i]])
                    {
                        p2[j] = -1; // mark out the corresponding genes in parent 2
                        c1[j] = -2; // mark out the corresponding genes in child 1
                    }
                }
            }

            index = 0;
            for (int i = 0; i < numberOfGenes; i++)
            {
                if (c1[i] == -2)
                {
                    c1[i] = p1[arr[index]]; // copy the genes from parent 1 to child 1
                    index++;
                }
                else
                {
                    c1[i] = p2[i]; // copy the genes from parent 2 to child 1
                }
            }
        }

        private void PositionBasedX(int[] arr, int[] p1, int[] p2, int[] c1)
        {
            for (int i = 0; i < numberOfGenes; i++) c1[i] = -1; // mark out all the genes in the child


            index = 0;
            for(int j = 0; j < arr.Length; j++)
            {
                c1[arr[index]] = p1[arr[index]]; // copy the genes at the selected positions to the same position of the proto-child
                for (int i = 0; i < numberOfGenes; i++)
                {
                    if (p2[i] == c1[arr[index]]) p2[i] = -1; // mark out the genes with the same value in p2
                }
                index++;
            }

            index = 0;
            for(int i = 0; i < numberOfGenes; i++)
            {
                if(p2[i] != -1)
                {
                    if(c1[index] == -1)
                    {
                        c1[index] = p2[i]; // add the unmarked genes left in p2 sequentially into the void positions at c1

                        if (index < numberOfGenes - 1) index++;
                    }
                    else
                    {
                        while (c1[index] != -1)
                        {
                            if (index == numberOfGenes - 1) break;
                            index++;
                        }
                        c1[index] = p2[i]; // add the unmarked genes left in p2 sequentially into the void positions at c1
                    }
                }
            }          
        }

        private void OrderX(int i1, int i2, int[] p1, int[] p2, int[] c1)
        {
            index = 0;

            for (int i = 0; i < numberOfGenes; i++) c1[i] = -1; // mark out all the genes in the child

            for (int i = i1; i < i2; i++)
            {
                c1[i] = p1[i];
                for (int j = 0; j < numberOfGenes; j++)
                {
                    if (p2[j] == c1[i]) p2[j] = -1;
                }
            }


            index = 0;
            for (int i = 0; i < numberOfGenes; i++)
            {
                if (p2[i] != -1)
                {
                    if (c1[index] == -1)
                    {
                        c1[index] = p2[i]; // add the unmarked genes left in p2 sequentially into the void positions at c1

                        if (index < numberOfGenes - 1) index++;
                    }
                    else
                    {
                        while (c1[index] != -1)
                        {
                            if (index == numberOfGenes - 1) break;
                            index++;
                        }
                        c1[index] = p2[i]; // add the unmarked genes left in p2 sequentially into the void positions at c1
                    }
                }
            }           
        }

        public override void MutateAParent(int parentID, int childID, bool[] mutatedFlag)
        {
            switch (MutationOperator)
            {
                case PermutationMutationOperator.Inversion:

                    i1 = randomizer.Next(numberOfGenes); // i1 and i2 are cut locations of an inversion mutation
                    i2 = randomizer.Next(numberOfGenes);

                    // if i1 > i2, swap i1 and i2
                    int temp;
                    if (i1 > i2)
                    {
                        temp = i1;
                        i1 = i2;
                        i2 = temp;
                    }

                    for (int i = 0; i < numberOfGenes; i++) chromosomes[childID][i] = chromosomes[parentID][i];
                    Array.Reverse(chromosomes[childID], i1, i2 - i1);

                    break;
                case PermutationMutationOperator.Insertion:

                    i1 = randomizer.Next(numberOfGenes); // i1 and i2 are random positions of an insertion mutation
                    do  // ensure that the index is different
                    {
                        i2 = randomizer.Next(numberOfGenes);
                    }
                    while (i2 == i1);

                    index = 0;
                    for (int i = 0; i < numberOfGenes; i++)
                    {
                        if (index == i1) index++;
                        if (i != i1 && i != i2)
                        {
                            chromosomes[childID][i] = chromosomes[parentID][index]; // copy the gene from parent to child
                            index++;
                        }
                        else if (i == i1)
                        {
                            //index++; // skip one position
                            chromosomes[childID][i] = chromosomes[parentID][index];
                            index++;
                        }
                        else
                        {
                            chromosomes[childID][i] = chromosomes[parentID][i1];
                        }
                    }


                    break;
                case PermutationMutationOperator.Displacement:

                    i1 = randomizer.Next(numberOfGenes);

                    do  // ensure that the index is different
                    {
                        i2 = randomizer.Next(numberOfGenes);
                    }
                    while (i2 == i1 && (i2 - i1 + 1) > 4);

                    // if i1 > i2, swap i1 and i2
                    if (i1 > i2)
                    {
                        temp = i1;
                        i1 = i2;
                        i2 = temp;
                    }

                    //temp = i2 - i1 + 1; // length of subtour
                    int insertPos;
                    //do  // ensure that the index is different
                    //{
                        insertPos = randomizer.Next(numberOfGenes - (i2 - i1 + 1));
                    //}
                    //while (insertPos == i1);


                    index = temp = 0;
                    for (int i = 0; i < numberOfGenes; i++)
                    {
                        if (index == i1) index += i2 - i1 + 1;

                        if (i != i1 && !(i >= insertPos && i < insertPos + (i2 - i1 + 1)))
                        {
                            chromosomes[childID][i] = chromosomes[parentID][index]; // copy the gene from parent to child
                            index++;                           
                        }
                        else if (i == i1 && !(i >= insertPos && i <= insertPos + (i2 - i1 + 1) - 1))
                        {
                            //index += (i2 - i1 + 1); // skip the length of the subtour
                            chromosomes[childID][i] = chromosomes[parentID][index];
                            index++;
                        }
                        else if (i >= insertPos && i < insertPos + (i2 - i1 + 1))
                        {
                            chromosomes[childID][i] = chromosomes[parentID][i1 + temp]; // insert subtour to child
                            temp++;
                        }
                        if (index > numberOfGenes - 1) index = numberOfGenes - 1; // guarding condition
                    }

                    //int[] c1 = new int[numberOfGenes];
                    //chromosomes[childID].CopyTo(c1, 0);


                    // test
                    //int[] pTest1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                    //numberOfGenes = 9;
                    //int[] cTest1 = new int[9];

                    //i1 = 2;
                    //i2 = 4;

                    //int insertPos = 1;


                    //index = 0;
                    //temp = 0;
                    //for (int i = 0; i < numberOfGenes; i++)
                    //{
                    //    if (i != i1 && !(i >= insertPos && i <= insertPos + (i2 - i1 + 1) - 1))
                    //    {
                    //        cTest1[i] = pTest1[index]; // copy the gene from parent to child
                    //        index++;
                    //        if (index == i1) index += i2 - i1 + 1;
                    //    }
                    //    else if (i == i1 && !(i >= insertPos && i <= insertPos + (i2 - i1 + 1) - 1))
                    //    {
                    //        index += (i2 - i1 + 1); // skip the length of the subtour
                    //        cTest1[i] = pTest1[index];
                    //        index++;
                    //    }
                    //    else if (i >= insertPos && i < insertPos + (i2 - i1 + 1))
                    //    {
                    //        cTest1[i] = pTest1[i1 + temp]; // insert subtour to child
                    //        temp++;
                    //    }
                    //    if (index > numberOfGenes - 1) index = numberOfGenes - 1;
                    //}

                    break;
                case PermutationMutationOperator.Reciprocal:

                    i1 = randomizer.Next(numberOfGenes);

                    do  // ensure that the index is different
                    {
                        i2 = randomizer.Next(numberOfGenes);
                    }
                    while (i2 == i1);


                    for(int i = 0; i < numberOfGenes; i++)
                    {
                        if (i != i1 || i != i2) chromosomes[childID][i] = chromosomes[parentID][i];
                        else if (i == i1) chromosomes[childID][i] = chromosomes[parentID][i2];
                        else chromosomes[childID][i] = chromosomes[parentID][i1];
                    }


                    break;
                case PermutationMutationOperator.Heuristic:

                    /*  chromosome[parentID] ---> Input Array
                        data[] ---> Temporary array to store current combination
                        start & end ---> Starting and Ending
                                         indexes in arr[]
                        index ---> Current index in data[]
                        r ---> Size of a combination to be operated */

                    
                    i2 = randomizer.Next(numberOfGenes);
                    i1 = randomizer.Next(i2);
                    int i3 = randomizer.Next(i2, numberOfGenes);
                    tempArr[0] = chromosomes[parentID][i1];
                    tempArr[1] = chromosomes[parentID][i2];
                    tempArr[2] = chromosomes[parentID][i3];

                    int r = 3;
                    int n = 3;
                    Combination(childID, m, tempArr, n, r);

                    int numberOfPermutation = fact(n);

                    double maxObjectValue = double.MinValue;

                    for (int j = 0; j < numberOfPermutation; j++)
                    {                    
                        index = 0;
                        for (int i = 0; i < numberOfGenes; i++)
                        {
                            if (i == i1)
                            {
                                arr[i] = m[0];
                            }
                            else if (i == i2)
                            {
                                arr[i] = m[1];
                            }
                            else if (i == i3)
                            {
                                arr[i] = m[2];
                            }
                            else
                            {
                                arr[i] = chromosomes[parentID][index];
                            }
                            index++;
                        }

                        if (objectiveFunction(arr) > maxObjectValue)
                        {
                            maxObjectValue = objectiveFunction(arr);
                            for (int k = 0; k < numberOfGenes; k++)
                            {
                                chromosomes[childID][k] = arr[k];
                            }
                        }
                    }



                    // test
                    //int[] pTest1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                    //numberOfGenes = 9;
                    //int[] cTest1 = new int[9];

                    //i1 = 2;
                    //i2 = 4;
                    //int i3 = 6;
                    //int[] data = new int[] { 3,7,5};


                    //i1 = randomizer.Next(numberOfGenes); // pick up i1 genes
                    //i2 = randomizer.Next(i1);
                    //int i3 = randomizer.Next(i2, numberOfGenes);
                    //int[] data = new int[numberOfGenes];


                    //int[] arr = { 1, 2, 3, 4, 5 };
                    //int r = 3;
                    //int n = data.Length;
                    ////r = numberOfGenes;
                    //printCombination(childID, m, data, n, r);

                    //index = 0;
                    //for(int i = 0; i < numberOfGenes; i++)
                    //{
                    //    if (i == 2)
                    //    {
                    //        cTest1[i] = m[0];
                    //    }
                    //    else if (i == 4)
                    //    {
                    //        cTest1[i] = m[1];
                    //    }
                    //    else if (i == 6)
                    //    {
                    //        cTest1[i] = m[2];
                    //    }
                    //    else
                    //    {
                    //        cTest1[i] = pTest1[index];                        
                    //    }
                    //    index++;
                    //}

                    break;
            }
        }

        public int fact(int n)
        {
            if (n == 0 || n == 1)
                return 1;
            else
                return n * fact(n - 1);
        }


        public void combinationUtil(int childID, int[] arr, int[] data,
                                int start, int end,
                                int index, int r)
        {
            // Current combination is ready
            if (index == r)
            {
                for (int j = 0; j < r; j++)
                    chromosomes[childID][j] = data[j];
                    //Console.Write(data[j] + " ");
                //Console.WriteLine("");
                return;
            }

            // replace index with all possible elements.
            // The condition "end-i+1 >= r-index" makes sure that
            // including one element at index will make a combination 
            // with remaining elements at remaining positions
            for (int i = start; i <= end &&
                      end - i + 1 >= r - index; i++)
            {
                data[index] = arr[i];
                combinationUtil(childID, arr, data, i + 1, end, index + 1, r);
            }
        }

        public void Combination(int childID, int[] m, int[] arr,
                                 int n, int r)
        {
            // get all combination using temporary array 'm[]'
            combinationUtil(childID, arr, m, 0, n - 1, 0, r);
        }

        // population-based mutation (without knowing the information of the genes)
        //public override void MutateAParent(int parentID, int childID)
        //{

        //}

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
                    sb.Append($"{chromosomes[i][j]} ");
                }
                sb.Append($" obj: {objectiveValues[i]:0.00}");
                theMonitor.lbPopulation.Items.Add(sb);

                if (i == (populationSize - 1) || i == (populationSize + numberOfCrossoverChildren - 1)) theMonitor.lbPopulation.Items.Add("\n");
            }

            theMonitor.lbPopulation.Update();
        }

        public override void UpdateInformationOntheMonitor()
        {
            // update so far the best objective value on the text box
            theMonitor.tbxBestObjectiveValue.Text = soFarTheBestObjectiveValue.ToString();

            // update so far the best solution on the list box
            theMonitor.lbBestSolution.Items.Clear();
            sb.Clear();
            for(int i = 0; i < numberOfGenes; i++) sb.Append($"{soFarTheBestSolution[i]} ");
            theMonitor.lbBestSolution.Items.Add(sb);
        }

        public override void InitializePopulation()
        {
            for (int r = 0; r < populationSize; r++)
            {
                //ShuffleAnArray(chromosomes[r], (int)Math.Sqrt(numberOfGenes));
                ShuffleAnArray(chromosomes[r], numberOfGenes);
            }
        }

    }



    /// <summary>
    ///  Canonical crossover operator
    /// </summary>
    public enum PermutationCrossoverOperator
    {
        PartialMappedX, OrderX, PositionBasedX, OrderBasedX, CycleX, SubtourExchange
    }

    /// <summary>
    ///  Canonical mutation operator
    /// </summary>
    public enum PermutationMutationOperator
    {
        Inversion, Insertion, Displacement, Reciprocal, Heuristic
    }
}
