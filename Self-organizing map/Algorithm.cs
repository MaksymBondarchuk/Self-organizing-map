﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Self_organizing_map
{
	public class Algorithm
	{
		private Map Map { get; } = new Map();

		private Random Random { get; } = new Random();

		public void Run(List<List<double>> inputVectors, int neuronsNumber, int iterationsNumber) {
			if (inputVectors.Count == 0)
				return;

			Initialize(inputVectors.First().Count, neuronsNumber);
			Educate(inputVectors, iterationsNumber);
			Check(inputVectors);
		}

		private void Educate(List<List<double>> inputVectors, int iterationsNumber) {
			// Vectors normalization
			foreach (var vector in inputVectors) {
				var vectorSum = vector.Sum();
				for (var i = 0; i < vector.Count; i++)
					vector[i] /= vectorSum;
			}

			// Algorithm
			const int t1 = 1000;
			const int t2 = 1000;
			const double η0 = .6;
			const double ηMin = .01;
			const double decayRate = .99;
			const double σ0 = .5;
			var iter = 0;
			var η = 1.0;
			do {

				//for (var iter = 0; iter < iterationsNumber; iter++)
				//{
				Console.WriteLine($"{iter}");
				//var η = η0 * Math.Exp(-(double)iter / t1);
				var σ = σ0 * Math.Exp(-(double)iter / t2);

				foreach (var vector in inputVectors) {
					var minDist = double.MaxValue;
					var minNeuronIdx = -1;
					for (var n = 0; n < Map.Neurons.Count; n++) {
						var dist = Map.Neurons[n].DistanceToVector(vector);
						if (dist < minDist) {
							minDist = dist;
							minNeuronIdx = n;
						}
					}

					for (var i = 0; i < Map.Neurons[minNeuronIdx].W.Count; i++)
						//Map.Neurons[minNeuronIdx].W[i] += η * minDist;
						Map.Neurons[minNeuronIdx].W[i] += η * (vector[i] - Map.Neurons[minNeuronIdx].W[i]);

					foreach (var neuron in Map.Neurons) {
						if (neuron == Map.Neurons[minNeuronIdx])
							continue;

						var dist = neuron.DistanceToVector(vector);
						for (var i = 0; i < neuron.W.Count; i++)
							neuron.W[i] += η * Math.Exp(-dist * dist / (2 * σ * σ)) * (vector[i] - neuron.W[i]);
					}
				}
				iter++;
				η *= decayRate;
			} while (ηMin < η);
		}

		private void Check(List<List<double>> inputVectors) {
			Console.WriteLine();
			//var result = new List<int>();
			foreach (var neuron in Map.Neurons) {
				foreach (var w in neuron.W)
					Console.Write($"{w,-6:0.00}");
				Console.WriteLine();
			}
			Console.WriteLine();


			foreach (var vector in inputVectors) {
				var minDist = double.MaxValue;
				var minNeuronIdx = -1;
				for (var n = 0; n < Map.Neurons.Count; n++) {
					var dist = Map.Neurons[n].DistanceToVector(vector);
					if (dist < minDist) {
						minDist = dist;
						minNeuronIdx = n;
					}
				}

				//result.Add(minNeuronIdx);
				Console.Write($"{minNeuronIdx,4}");
			}
			Console.WriteLine();
		}

		private void Initialize(int vectorSize, int neuronsNumber) {
			for (var n = 0; n < neuronsNumber; n++) {
				var neuron = new Neuron();
				for (var v = 0; v < vectorSize; v++)
					neuron.W.Add(Random.NextDouble());
				Map.Neurons.Add(neuron);
			}
		}
	}
}
