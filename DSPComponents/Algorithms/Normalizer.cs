using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Normalizer : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputMinRange { get; set; }
        public float InputMaxRange { get; set; }
        public Signal OutputNormalizedSignal { get; set; }

        public override void Run()
        {
            float first_norm = 0;
            float second_norm = 0;
            List<float> Range_0 = new List<float>();                    // list for range 0 to 1
            List<float> Range_1 = new List<float>();                    // list for range -1 to 1

            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {

                first_norm = ((InputSignal.Samples[i] - InputSignal.Samples.Min()) / (InputSignal.Samples.Max() - InputSignal.Samples.Min()));      // 0 to 1

                second_norm = (((InputMaxRange - InputMinRange) * (InputSignal.Samples[i] - InputSignal.Samples.Min())) / (InputSignal.Samples.Max() - InputSignal.Samples.Min())) + InputMinRange;     // -1 to 1


                Range_0.Add(first_norm);            // list of samples in range 0 to 1
                Range_1.Add(second_norm);           // list of samples in range -1 to 1
            }
            OutputNormalizedSignal = new Signal(Range_0, false);
            OutputNormalizedSignal = new Signal(Range_1, false);

        }
    }
}
