﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Jasiri.Sampling
{
    public class ProbabilisticSampler : ISampler
    {
        readonly Dictionary<string, string> tags;
        readonly ulong upperBound;

        public double Probability { get; }

        public ProbabilisticSampler(double probability)
        {
            if (probability < 0 || probability > 1)
                throw new ArgumentException("Probability must be between 0 and 1. Supplied value is: " + probability.ToString());

            Probability = probability;
            upperBound = (ulong)(ulong.MaxValue * probability);
            tags = new Dictionary<string, string>()
            {
                ["sampler"] = "probabilistic",
                ["sampler-arg"] = probability.ToString()
            };
        }

        public SamplingStatus Sample(string operationName, ulong traceId)
        {
            var sampled = traceId <= upperBound;
            return new SamplingStatus(sampled, tags);
        }
    }
}
