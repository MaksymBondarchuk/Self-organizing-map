namespace Self_organizing_map
{
    internal static class Program
    {
        private static void Main()
        {
            const int vectorsNumber = 25;
            const int classesNumber = 8;
            const int vectorLen = 50;
	        const int imageDistortionFactor = 2;

            var vectorsGenerator = new VectorsGenerator();
            var vectors = vectorsGenerator.Generate(vectorsNumber: vectorsNumber, classesNumber: classesNumber,
				vectorLen: vectorLen, imageDistortionFactor: imageDistortionFactor);


            var algorithm = new Algorithm();
            algorithm.Run(inputVectors: vectors, neuronsNumber: classesNumber);
        }
    }
}
