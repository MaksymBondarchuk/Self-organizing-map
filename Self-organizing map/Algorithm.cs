using System;
using System.Collections.Generic;
using System.Linq;

namespace Self_organizing_map
{
    public class Algorithm
    {
        private Map Map { get; set; } = new Map();

        private Random Random { get; set; } = new Random();

        public void Run(List<List<double>> inputVectors, int neuronsNumber, int iterationsNumber)
        {
            if (inputVectors.Count == 0)
                return;

            // Vectors normalization
            foreach (var vector in inputVectors)
            {
                var vectorSum = vector.Sum();
                for (var i = 0; i < vector.Count; i++)
                    vector[i] /= vectorSum;
            }

            for (var iter = 0; iter < iterationsNumber; iter++)
            {
                foreach (var vector in inputVectors)
                {
                    
                }
            }

            Initialize(inputVectors.First().Count, neuronsNumber);
        }

        private void Initialize(int vectorSize, int neuronsNumber)
        {
            for (var n = 0; n < neuronsNumber; n++)
            {
                var neuron = new Neuron();
                for (var v = 0; v < vectorSize; v++)
                    neuron.W.Add(Random.NextDouble()*Random.Next(10));
                Map.Neurons.Add(neuron);
            }
        }
    }
}
