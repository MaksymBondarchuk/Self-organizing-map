using System;
using System.Collections.Generic;
using System.Linq;

namespace Self_organizing_map
{
    public class Neuron
    {
        /// <summary>
        /// Weights
        /// </summary>
        public List<double> W { get; set; } = new List<double>();

        public double DistanceToVector(List<double> vector)
        {
            return Math.Sqrt(W.Select((t, i) => (t - vector[i])*(t - vector[i])).Sum());
        }

    }
}
