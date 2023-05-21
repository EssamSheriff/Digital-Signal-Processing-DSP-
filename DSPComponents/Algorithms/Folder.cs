using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Folder : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputFoldedSignal { get; set; }

        public override void Run()
        {
            List<float> output_Samples = new List<float>();
            int N = InputSignal.Samples.Count;
            for (int i = N-1; i >= 0; i--)
            {
                output_Samples.Add(InputSignal.Samples[i]);
            }

            OutputFoldedSignal = new Signal(output_Samples, false);
            OutputFoldedSignal.SamplesIndices = InputSignal.SamplesIndices;
        }
    }
}
