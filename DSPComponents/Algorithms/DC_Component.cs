using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DC_Component: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            
            float update_sample = 0f;
            List<float> result = new List<float>();
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                update_sample += InputSignal.Samples[i];
                result.Add(InputSignal.Samples[i]);
            }
            update_sample = update_sample / result.Count;
            for (int i = 0; i < result.Count; i++)
            {
                result[i] = result[i] - update_sample;
            }
            OutputSignal = new Signal(result, false);


        }
    }
}
