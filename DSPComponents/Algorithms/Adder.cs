using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Adder : Algorithm
    {
        public List<Signal> InputSignals { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            float update_sample;
            List<float> result = new List<float>();
            foreach (int i in InputSignals[0].Samples)
            {
                update_sample = InputSignals[0].Samples[i] + InputSignals[1].Samples[i];
                result.Add(update_sample);
            }
            OutputSignal = new Signal(result, false);

        }
    }
}