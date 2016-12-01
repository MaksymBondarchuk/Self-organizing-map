using System;
using System.Collections.Generic;

namespace Self_organizing_map
{
	public class VectorsGenerator
	{
		private Random Random { get; } = new Random();

		private static int[] FillArray(int[] array, int number) {
			var i = 0;
			while (i < number)
				array[array.Length - 1 - (i++ % array.Length)]++;
			return array;
		}

		public List<List<double>> Generate(int vectorsNumber, int classesNumber, int vectorLen, int imageDistortionFactor = 1) {
			var vectorInClassLen = FillArray(new int[classesNumber], vectorLen);
			var vectorInClassNumber = FillArray(new int[classesNumber], vectorsNumber);

			var vectors = new List<List<double>>();
			var classStartIdx = 0;
			for (var m = 0; m < classesNumber; m++) {
				var classFirstVector = new double[vectorLen];
				for (var i = classStartIdx; i < classStartIdx + vectorInClassLen[m]; i++)
					classFirstVector[i] = 1;

				vectors.Add(new List<double>(classFirstVector));
				for (var i = 0; i < vectorInClassNumber[m] - 1; i++) {
					var vector = new List<double>(classFirstVector);
					for (var j = 0; j < imageDistortionFactor; j++)
						vector[Random.Next(classStartIdx, classStartIdx + vectorInClassLen[m] - 1)] = 0;
					vectors.Add(vector);
				}

				classStartIdx += vectorInClassLen[m];
			}

			return vectors;
		}
	}
}
