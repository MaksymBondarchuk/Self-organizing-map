using System;
using System.Collections.Generic;

namespace Self_organizing_map
{
    public class VectorsGenerator
    {
        private Random Random { get; set; } = new Random();

        public List<List<double>> Generate(int q, int m, int n)
        {
            var vectors = new List<List<double>>();

            vectors.Add(new List<double> { 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 });
            vectors.Add(new List<double> { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            vectors.Add(new List<double> { 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0 });
            vectors.Add(new List<double> { 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0 });
            vectors.Add(new List<double> { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1 });
            vectors.Add(new List<double> { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1 });

            //for (var v = 0; v < q; v++)
            //{
            //    var vector = new List<double>();
            //    for (var i = 0; i < n; i++)
            //        vector.Add(Random.Next(1));
            //    vectors.Add(vector);
            //}

            return vectors;
        }
    }
}
