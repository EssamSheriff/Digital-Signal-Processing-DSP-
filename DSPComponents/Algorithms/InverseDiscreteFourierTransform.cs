using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;


namespace DSPAlgorithms.Algorithms
{
    public class InverseDiscreteFourierTransform : Algorithm
    {
        public Signal InputFreqDomainSignal { get; set; }
        public Signal OutputTimeDomainSignal { get; set; }

        public override void Run()
        {
            double result, Result_of_R, Result_of_i;
            double theta;
            double Real ;
            double Im;
            Complex c ,c2;
            List<Complex> complices = new List<Complex>();

            for (int i = 0; i < InputFreqDomainSignal.FrequenciesAmplitudes.Count; i++)
            {
                Real = InputFreqDomainSignal.FrequenciesAmplitudes[i] * Math.Cos(InputFreqDomainSignal.FrequenciesPhaseShifts[i]);
                Im = InputFreqDomainSignal.FrequenciesAmplitudes[i] * Math.Sin(InputFreqDomainSignal.FrequenciesPhaseShifts[i]);
                complices.Add(new Complex(Real,Im));
            }

            OutputTimeDomainSignal = new Signal(new List<float>() , false);
            for (int i = 0; i < InputFreqDomainSignal.FrequenciesAmplitudes.Count; i++) // n
            {
                Result_of_R = 0; 
                Result_of_i = 0;
                
                for (int j = 0; j < InputFreqDomainSignal.FrequenciesAmplitudes.Count; j++) // K
                {
                    theta = (2 * Math.PI * i * j) / InputFreqDomainSignal.FrequenciesAmplitudes.Count;    // 2 pi k n  / N
                   
                    c = new Complex(Math.Cos(theta), Math.Sin(theta));
                    c2 = complices[j] * c;
                    Result_of_R += c2.Real;
                    Result_of_i += c2.Imaginary;
                }
                result = Math.Round( Result_of_R / InputFreqDomainSignal.FrequenciesAmplitudes.Count );

                OutputTimeDomainSignal.Samples.Add((float)result);
            }
        }
    }
}
