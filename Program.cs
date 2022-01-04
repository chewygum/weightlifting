using System;
using System.Collections.Generic;
using System.Linq;

namespace WeightLifting
{

    /// <summary>
    /// This console application prompts user to input a weight and it figures out which and how many weight plates should be put on the weight bar
    /// </summary>
    class WeightLiftProgram
    {
        #region constants and readonly fields
        public static readonly double[]  PLATE_WEIGHTS = new double[] { 45, 35, 25, 10, 5, 2.5}; // plate weights listed in descending order
        public static readonly int BAR_WEIGHT = 45; 
        private const int MAX_INPUT_WEIGHT = 2000; // max weight a human can lift
        #endregion

        private static Dictionary<double, int> chosenPlatesOnEachSide = new Dictionary<double, int>(); 

        static void Main(string[] args)
        { 
            double inputWeight;
            bool isInputCorrect = false;

            Console.WriteLine("Weight plates are available in " + String.Join(',', PLATE_WEIGHTS) + " lbs.");
            Console.WriteLine($"Weight bar weighs {BAR_WEIGHT} lb.");
            Console.Write("Please input a weight (in lb) and press ENTER: ");

            do
            {
                bool status = double.TryParse(Console.ReadLine(), out inputWeight);

                if (!status)
                    Console.Write("Input weight is in the wrong format. Please enter a numeric value for weight and press ENTER: ");
                
                else if (inputWeight < BAR_WEIGHT)
                    Console.Write($"The input weight is lighter than the weight bar itself. Please enter a weight no less than {BAR_WEIGHT} lb and press ENTER: ");
                    
                else if (inputWeight > MAX_INPUT_WEIGHT)
                    Console.Write($"The input weight is over the limit a human can lift. Please enter a weight no more than {MAX_INPUT_WEIGHT} lb and press ENTER: ");

                else
                    isInputCorrect = true;

            } while (!isInputCorrect);

            double halfRemainWeight = (inputWeight - BAR_WEIGHT)/2;

            CalculatePlateWeightsOnEachSide(halfRemainWeight);

            displayTotalWeightSelected();

            displayPlateWeightsSelected();          
        }

        /// <summary>
        /// function that calculates the plate weights to be put on EACH side of the weight bar
        /// </summary>
        /// <param name="remainWeight"></param>
        private static void CalculatePlateWeightsOnEachSide(double remainWeight)
        {
            foreach (double plateWeight in PLATE_WEIGHTS)
            {
                int numberOfWeights = Convert.ToInt32(Math.Floor(remainWeight / plateWeight));
                if (numberOfWeights > 0)
                {
                    chosenPlatesOnEachSide.Add(plateWeight, numberOfWeights);
                    remainWeight %= plateWeight;
                }
            }
        }

        private static void displayPlateWeightsSelected()
        {
            if (chosenPlatesOnEachSide.Count == 0)
            {
                Console.WriteLine("No weight plates are chosen to be put on the bar.");
            }
            else
            {
                Console.WriteLine("Chosen weight plates to be put on the bar:");

                chosenPlatesOnEachSide.ToList().ForEach(kvp =>
                {
                    Console.WriteLine($"{kvp.Key} lb: {kvp.Value * 2}  ( {kvp.Value} on each side ) ");
                });
            }
        }

        private static void displayTotalWeightSelected()
        {
            double totalWeight  = chosenPlatesOnEachSide.Sum(x => x.Key*x.Value*2) + BAR_WEIGHT;

            Console.WriteLine($"Total weight to lift including the bar is {totalWeight} lb.");
        }
    }
}
