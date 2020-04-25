using System.Collections.Generic;

namespace Self_organizing_map
{
	internal static class Program
	{
		private static void Main()
		{
			const int vectorsNumber = 25;
			const int classesNumber = 16;
			const int vectorLen = 50;
			const int imageDistortionFactor = 2;

			var vectorsGenerator = new VectorsGenerator();
			List<List<double>> vectors = vectorsGenerator.Generate(vectorsNumber: vectorsNumber, classesNumber: classesNumber,
				vectorLen: vectorLen, imageDistortionFactor: imageDistortionFactor);

			var algorithm = new Algorithm();
			algorithm.Run(inputVectors: vectors, neuronsNumber: classesNumber);
		}
	}
}