using System;
using System.Collections.Generic;
using System.Linq;

namespace Self_organizing_map
{
    public class Algorithm
    {
        private Map Map { get; } = new Map();

        private Random Random { get; } = new Random();

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

            // Algorithm
            const int t1 = 1000;
            const int t2 = 1000;
            const double η0 = .5;
            const double σ0 = .5;
            for (var iter = 0; iter < iterationsNumber; iter++)
            {
                var η = η0 * Math.Exp(-(double)iter / t1);
                var σ = σ0 * Math.Exp(-(double)iter / t2);

                foreach (var vector in inputVectors)
                {
                    var minDist = double.MaxValue;
                    var minNeuronIdx = -1;
                    for (var n = 0; n < Map.Neurons.Count; n++)
                    {
                        var dist = Map.Neurons[n].DistanceToVector(vector);
                        if (dist < minDist)
                        {
                            minDist = dist;
                            minNeuronIdx = n;
                        }
                    }

                    for (var i = 0; i < Map.Neurons[minNeuronIdx].W.Count; i++)
                        Map.Neurons[minNeuronIdx].W[i] += η * minDist;

                    foreach (var neuron in Map.Neurons)
                    {
                        if (neuron == Map.Neurons[minNeuronIdx])
                            continue;

                        for (var i = 0; i < neuron.W.Count; i++)
                        {
                            var dist = neuron.DistanceToVector(vector);
                            neuron.W[i] += η * Math.Exp(-dist * dist / (2 * σ * σ)) * minDist;
                        }
                    }
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
                    neuron.W.Add(Random.NextDouble() * Random.Next(10));
                Map.Neurons.Add(neuron);
            }
        }
    }
}
