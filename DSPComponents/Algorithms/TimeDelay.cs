using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class TimeDelay:Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public float InputSamplingPeriod { get; set; }
        public float OutputTimeDelay { get; set; }

        public override void Run()
        {
            //throw new NotImplementedException();
            DirectCorrelation direct = new DirectCorrelation();
            List<float> copy = new List<float>();
            direct.InputSignal1 = InputSignal1;
            direct.InputSignal2 = InputSignal2;
            direct.Run();
            copy = direct.OutputNonNormalizedCorrelation;
            float find_max = 0;
            for(int i = 0; i < copy.Count; i++)
                find_max = Math.Max(find_max, copy[i]);
            
            OutputTimeDelay = find_max * InputSamplingPeriod;
        }
    }
}
