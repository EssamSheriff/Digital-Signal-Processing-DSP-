using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Shifter : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int ShiftingValue { get; set; }
        public Signal OutputShiftedSignal { get; set; }

        public override void Run()
        {
            List<int> output_SamplesIndices = new List<int>();
            OutputShiftedSignal = new Signal(InputSignal.Samples, false);
            bool fold = false;
            if (InputSignal.Samples[0] == 1001)
            {
                fold = true;
            }

            if (fold && ShiftingValue < 0)
            {
                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {
                    output_SamplesIndices.Add(InputSignal.SamplesIndices[i] + ShiftingValue);
                }
            }
            else if (fold && ShiftingValue >= 0)
            {
                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {
                    output_SamplesIndices.Add(InputSignal.SamplesIndices[i] + ShiftingValue);
                }
            }
            else if (ShiftingValue >= 0 && !fold)
            {
                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {
                    output_SamplesIndices.Add(InputSignal.SamplesIndices[i] - ShiftingValue);
                }
            }
            else
            {
                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {
                    output_SamplesIndices.Add(InputSignal.SamplesIndices[i] - ShiftingValue);
                }
            }
            OutputShiftedSignal.SamplesIndices = output_SamplesIndices;

        }
    }
}
