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
		private List<List<double>> OriginalInputVectors { get; set; } = new List<List<double>>();

		public void Run(List<List<double>> inputVectors, int neuronsNumber)
		{
			if (inputVectors.Count == 0)
			{
				return;
			}

			Initialize(inputVectors, inputVectors.First().Count, neuronsNumber);
			Train();
			Check();
		}

		private void Train()
		{
			PrintWeights("Weights before training:");

			// Vectors normalization
			foreach (List<double> vector in InputVectors)
			{
				double vectorSum = vector.Sum();
				for (int i = 0; i < vector.Count; i++)
				{
					vector[i] /= vectorSum;
				}
			}

			// Algorithm
			const double fridge = .01;
			int iter = 0;
			double learningRate = .99;
			const double decayRate = .99;

			do
			{
				foreach (List<double> vector in InputVectors)
				{
					int bmuIdx = GetBestMatchingUnit(vector);

					Neuron bestNeuron = Map.Neurons[bmuIdx];
					for (int i = 0; i < Map.Neurons[bmuIdx].W.Count; i++)
					{
						bestNeuron.W[i] += learningRate * (vector[i] - bestNeuron.W[i]);
					}

					foreach (Neuron neuron in Map.Neurons)
					{
						if (neuron == bestNeuron)
						{
							continue;
						}

						double dist = neuron.DistanceToVector(vector);
						for (int i = 0; i < neuron.W.Count; i++)
						{
							neuron.W[i] += learningRate * Math.Exp(-dist * dist / (2 * learningRate * learningRate)) * (vector[i] - neuron.W[i]);
						}
					}
				}

				iter++;
				learningRate *= decayRate;
			} while (fridge < learningRate);

			Console.WriteLine($"Trained for {iter} iterations\n");
		}

		private void Check()
		{
			PrintWeights("Weights after training:");

			for (int i = 0; i < InputVectors.Count; i++)
			{
				int bmuIdx = GetBestMatchingUnit(InputVectors[i]);
				foreach (double w in OriginalInputVectors[i])
				{
					Console.Write($"{w,-1}");
				}

				Console.Write($" fits into {bmuIdx,4}");
				Console.WriteLine();
			}

			Console.WriteLine();
		}

		private void Initialize(IEnumerable<List<double>> inputVectors, int vectorSize, int neuronsNumber)
		{
			List<double>[] collection = inputVectors as List<double>[] ?? inputVectors.ToArray();
			InputVectors = new List<List<double>>(collection);
			OriginalInputVectors = collection.Select(x => x.ToList()).ToList();

			for (int n = 0; n < neuronsNumber; n++)
			{
				var neuron = new Neuron();
				for (int v = 0; v < vectorSize; v++)
				{
					neuron.W.Add(Random.NextDouble());
				}

				Map.Neurons.Add(neuron);
			}
		}

		private void PrintWeights(string message)
		{
			Console.WriteLine(message);
			foreach (Neuron neuron in Map.Neurons)
			{
				foreach (double w in neuron.W)
				{
					Console.Write($"{w,-6:0.00}");
				}

				Console.WriteLine();
			}

			Console.WriteLine();
		}

		private int GetBestMatchingUnit(List<double> vector)
		{
			double minDist = double.MaxValue;
			int minNeuronIdx = -1;
			for (int n = 0; n < Map.Neurons.Count; n++)
			{
				double dist = Map.Neurons[n].DistanceToVector(vector);
				if (!(dist < minDist))
				{
					continue;
				}

				minDist = dist;
				minNeuronIdx = n;
			}

			return minNeuronIdx;
		}
	}
}