using System;
using System.Collections.Generic;
using System.Linq;

namespace Self_organizing_map
{
    public class Algorithm
    {
        private Map Map { get; } = new Map();

        private Random Random { get; } = new Random();

        private List<List<double>> InputVectors { get; set; } = new List<List<double>>();

        public void Run(List<List<double>> inputVectors, int neuronsNumber)
        {
            if (inputVectors.Count == 0)
                return;

            Initialize(inputVectors, inputVectors.First().Count, neuronsNumber);
            Train();
            Check();
        }

        private void Train()
        {
            PrintWeights("Weights before training:");

            // Vectors normalization
            foreach (var vector in InputVectors)
            {
                var vectorSum = vector.Sum();
                for (var i = 0; i < vector.Count; i++)
                    vector[i] /= vectorSum;
            }

            // Algorithm
            const double fridge = .01;
            var iter = 0;
            var learningRate = .99;
            const double decayRate = .99;

            do
            {
                foreach (var vector in InputVectors)
                {
                    var bmuIdx = GetBestMatchingUnit(vector);

                    var bestNeuron = Map.Neurons[bmuIdx];
                    for (var i = 0; i < Map.Neurons[bmuIdx].W.Count; i++)
                        bestNeuron.W[i] += learningRate * (vector[i] - bestNeuron.W[i]);

                    foreach (var neuron in Map.Neurons)
                    {
                        if (neuron == bestNeuron)
                            continue;

                        var dist = neuron.DistanceToVector(vector);
                        for (var i = 0; i < neuron.W.Count; i++)
                            neuron.W[i] += learningRate * Math.Exp(-dist * dist / (2 * learningRate * learningRate)) * (vector[i] - neuron.W[i]);
                    }
                }
                iter++;
                learningRate *= decayRate;
            } while (fridge < learningRate);
            Console.WriteLine($"After {iter} iterations\n");
        }

        private void Check()
        {
            PrintWeights("Weights after training:");

            foreach (var vector in InputVectors)
            {
                var bmuIdx = GetBestMatchingUnit(vector);
                foreach (var w in vector)
                    Console.Write($"{w,-6:0.00}");

                Console.Write($" fits into {bmuIdx,4}");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private void Initialize(IEnumerable<List<double>> inputVectors, int vectorSize, int neuronsNumber)
        {
            InputVectors = new List<List<double>>(inputVectors);

            for (var n = 0; n < neuronsNumber; n++)
            {
                var neuron = new Neuron();
                for (var v = 0; v < vectorSize; v++)
                    neuron.W.Add(Random.NextDouble());
                Map.Neurons.Add(neuron);
            }
        }

        private void PrintWeights(string message)
        {
            Console.WriteLine(message);
            foreach (var neuron in Map.Neurons)
            {
                foreach (var w in neuron.W)
                    Console.Write($"{w,-6:0.00}");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private int GetBestMatchingUnit(List<double> vector)
        {
            var minDist = double.MaxValue;
            var minNeuronIdx = -1;
            for (var n = 0; n < Map.Neurons.Count; n++)
            {
                var dist = Map.Neurons[n].DistanceToVector(vector);
                if (!(dist < minDist))
                    continue;
                minDist = dist;
                minNeuronIdx = n;
            }

            return minNeuronIdx;
        }
    }
}
