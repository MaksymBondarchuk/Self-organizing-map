using System;
using System.Collections.Generic;

namespace Self_organizing_map
{
	public class VectorsGenerator
	{
		private Random Random { get; } = new Random();

		private static int[] FillArray(int[] array, int number)
		{
			int i = 0;
			while (i < number)
			{
				array[array.Length - 1 - i++ % array.Length]++;
			}
			return array;
		}

		public List<List<double>> Generate(int vectorsNumber, int classesNumber, int vectorLen, int imageDistortionFactor = 1)
		{
			int[] vectorInClassLen = FillArray(new int[classesNumber], vectorLen);
			int[] vectorInClassNumber = FillArray(new int[classesNumber], vectorsNumber);

			var vectors = new List<List<double>>();
			int classStartIdx = 0;
			for (int m = 0; m < classesNumber; m++)
			{
				var classFirstVector = new double[vectorLen];
				for (int i = classStartIdx; i < classStartIdx + vectorInClassLen[m]; i++)
				{
					classFirstVector[i] = 1;
				}

				vectors.Add(new List<double>(classFirstVector));
				for (int i = 0; i < vectorInClassNumber[m] - 1; i++)
				{
					var vector = new List<double>(classFirstVector);
					for (int j = 0; j < imageDistortionFactor; j++)
					{
						vector[Random.Next(classStartIdx, classStartIdx + vectorInClassLen[m] - 1)] = 0;
					}
					vectors.Add(vector);
				}

				classStartIdx += vectorInClassLen[m];
			}

			return vectors;
		}
	}
}