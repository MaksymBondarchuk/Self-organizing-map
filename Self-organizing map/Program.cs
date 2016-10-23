namespace Self_organizing_map
{
    internal static class Program
    {
        private static void Main()
        {
            const int m = 3;

            var vectorsGenerator = new VectorsGenerator();
            vectorsGenerator.Generate(q: 10, m: m, n: 25);

            var algorithm = new Algorithm();
            algorithm.Run(inputVectors: vectorsGenerator.Generate(10, 8, 25),
                neuronsNumber: m, iterationsNumber: 100);
        }
    }
}
