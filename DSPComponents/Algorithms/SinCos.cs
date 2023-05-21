using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class SinCos: Algorithm
    {
        public string type { get; set; }
        public float A { get; set; }
        public float PhaseShift { get; set; }
        public float AnalogFrequency { get; set; }
        public float SamplingFrequency { get; set; }
        public List<float> samples { get; set; }
        public override void Run()
        {
            float update_sample = 0;
            samples = new List<float>();

            if (type == "sin")
            {
                for (int i = 0; i < SamplingFrequency; i++)
                {
                    update_sample = A * (float)(Math.Sin(((2 * Math.PI * i *  AnalogFrequency) / SamplingFrequency) + PhaseShift));
                    samples.Add(update_sample);
                }
            }
            else
            {
                for (int i = 0; i < SamplingFrequency; i++)
                {
                    update_sample = A * (float)(Math.Cos(((2 * Math.PI * i * AnalogFrequency) / SamplingFrequency) + PhaseShift));
                    samples.Add(update_sample);

                }
            }
        }
    }
}
