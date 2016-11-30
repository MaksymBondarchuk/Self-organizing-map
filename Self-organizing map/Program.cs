namespace Self_organizing_map
{
    internal static class Program
    {
        private static void Main()
        {
            const int vectorsNumber = 25;
            const int classesNumber = 3;
            const int vectorLen = 50;

            var vectorsGenerator = new VectorsGenerator();
            var vectors = vectorsGenerator.Generate(vectorsNumber: vectorsNumber, classesNumber: classesNumber, vectorLen: vectorLen);


            var algorithm = new Algorithm();
            algorithm.Run(inputVectors: vectors, neuronsNumber: classesNumber);
        }
    }
}
