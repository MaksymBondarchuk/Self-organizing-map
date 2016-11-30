using System;
using System.Collections.Generic;

namespace Self_organizing_map
{
	public class VectorsGenerator
	{
		private Random Random { get; set; } = new Random();

		public List<List<double>> Generate(int vectorsNumber, int classesNumber, int vectorLen) {
			var baseLen = vectorLen / classesNumber;
			var lastLen = baseLen + vectorLen % classesNumber;

			var vectorsInClass = Math.Floor((double)vectorsNumber / classesNumber);
			var vectorsInLastClass = vectorsInClass + vectorsNumber % classesNumber;

			var vectors = new List<List<double>>();
			for (var m = 0; m < classesNumber; m++) {
				var classFirstVector = new List<double>(vectorLen);
				for (var i = 0; i < vectorLen; i++)
					classFirstVector.Add(0);

				var classStartIdx = m * baseLen;
				for (var i = classStartIdx; i < classStartIdx + baseLen; i++)
					classFirstVector[i] = 1;

				vectors.Add(classFirstVector);
				for (var i = 0; i < vectorsInClass - 1; i++)
					vectors.Add(new List<double>(classFirstVector) { [Random.Next(classStartIdx, classStartIdx + baseLen - 1)] = 0 });
			}

			var lastClassFirstVector = new List<double>(vectorLen);
			for (var i = 0; i < vectorLen; i++)
				lastClassFirstVector.Add(0);

			var lastClassStartIdx = vectorLen - lastLen - 1;
			for (var i = lastClassStartIdx; i < lastClassStartIdx + baseLen; i++)
				lastClassFirstVector[i] = 1;

			vectors.Add(lastClassFirstVector);
			for (var i = 0; i < vectorsInLastClass - 1; i++)
				vectors.Add(new List<double>(lastClassFirstVector) { [Random.Next(lastClassStartIdx, vectorLen - 1)] = 0 });


			//vectors.Add(new List<double> { 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 });
			//vectors.Add(new List<double> { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
			//vectors.Add(new List<double> { 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0 });
			//vectors.Add(new List<double> { 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0 });
			//vectors.Add(new List<double> { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1 });
			//vectors.Add(new List<double> { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1 });

			return vectors;
		}
	}
}
