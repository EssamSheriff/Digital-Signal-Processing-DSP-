using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MovingAverage : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int InputWindowSize { get; set; }
        public Signal OutputAverageSignal { get; set; }
 
        public override void Run()
        {
            List<float> OusputSamples = new List<float>();
            float sum;
            float avg;
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                sum = 0;
                for (int j = 0; j < InputWindowSize; j++)
                {
                    if( (i+j) >= InputSignal.Samples.Count)
                    {
                        OutputAverageSignal = new Signal(OusputSamples, false);
                        return;
                    }
                    sum += InputSignal.Samples[j+i];
                }
                avg = sum / InputWindowSize;
                OusputSamples.Add(avg);
            }
        }
    }
}
