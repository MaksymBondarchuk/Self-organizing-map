namespace Self_organizing_map
{
    internal static class Program
    {
        private static void Main()
        {
            const int m = 3;

            var vectorsGenerator = new VectorsGenerator();
            vectorsGenerator.Generate(vectorsNumber: 10, classesNumber: m, vectorLen: 25);


            var algorithm = new Algorithm();
            algorithm.Run(inputVectors: vectorsGenerator.Generate(10, 8, 25),
                neuronsNumber: m);
        }
    }
}
