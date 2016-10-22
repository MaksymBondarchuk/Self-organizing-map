namespace Self_organizing_map
{
    internal static class Program
    {
        private static void Main()
        {
            var vectorsGenerator = new VectorsGenerator();
            vectorsGenerator.Generate(10, 8, 25);

            var algorithm = new Algorithm();
            algorithm.Run(vectorsGenerator.Generate(10, 8, 25), 10, 1000);
        }
    }
}
