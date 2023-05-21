using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Derivatives: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal FirstDerivative { get; set; }
        public Signal SecondDerivative { get; set; }

        public override void Run()
        {

            List<float> result1 = new List<float>();
            List<float> result2 = new List<float>();
            for (int i = 1; i < InputSignal.Samples.Count; i++)
            {
                result1.Add(InputSignal.Samples[i] - InputSignal.Samples[i - 1]);
            }
            //Console.WriteLine(result1.Count);

            FirstDerivative = new Signal(result1, false);
            for (int i = 1; i < InputSignal.Samples.Count - 1; i++)
            {

                if (i == InputSignal.Samples.Count - 1)
                    result2.Add(0);
 
                else
                    result2.Add(InputSignal.Samples[i + 1] - 2 * InputSignal.Samples[i] + InputSignal.Samples[i - 1]);
                
            }
            //Console.WriteLine(result2.Count);

            SecondDerivative = new Signal(result2, false);
        }
    }
}
