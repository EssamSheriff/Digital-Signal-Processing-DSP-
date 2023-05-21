using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DiscreteFourierTransform : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public float InputSamplingFrequency { get; set; }
        public Signal OutputFreqDomainSignal { get; set; }

        public override void Run()
        {
            
            double R_result, I_result, PhaseShifts, Amplitudes;
            double theta;
            OutputFreqDomainSignal = new Signal(InputTimeDomainSignal.Samples, false);
            OutputFreqDomainSignal.FrequenciesAmplitudes = new List<float>();
            OutputFreqDomainSignal.FrequenciesPhaseShifts = new List<float>();

            // get X_axis
            double X_point = (2 *Math.PI ) / (InputTimeDomainSignal.Samples.Count * (1/InputSamplingFrequency));
            List<double> X_axis= new List<double>();
            for (int i=0; i< InputTimeDomainSignal.Samples.Count ; i++)
            {
                X_axis.Add(X_point * (i + 1));
            }


            // Problem Code
            for (int i=0; i< InputTimeDomainSignal.Samples.Count; i++)
            {
                R_result = 0;
                I_result = 0;
                for (int j = 0; j < InputTimeDomainSignal.Samples.Count; j++)
                {
                    theta = (2 * Math.PI * i * j) / InputTimeDomainSignal.Samples.Count;    // 2 pi k n  / N
                    R_result +=  InputTimeDomainSignal.Samples[j]*  Math.Cos(theta);
                    I_result += InputTimeDomainSignal.Samples[j] * (-1 *Math.Sin(theta));
                }
                Amplitudes = Math.Sqrt((Math.Pow(R_result, 2) + Math.Pow(I_result, 2)));
                OutputFreqDomainSignal.FrequenciesAmplitudes.Add ((float)Amplitudes);
                PhaseShifts = Math.Atan2(I_result, R_result);
                OutputFreqDomainSignal.FrequenciesPhaseShifts.Add((float)PhaseShifts);
            }
           
           
        }
    }
}
