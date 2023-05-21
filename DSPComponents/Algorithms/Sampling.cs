﻿using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Sampling : Algorithm
    {
        public int L { get; set; } //upsampling factor
        public int M { get; set; } //downsampling factor
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            // throw new NotImplementedException();
            int count = 0;
            bool flag = true;
            List<float> ModifiedListSamples = new List<float>();
            List<int> ModifiedListIndcies = new List<int>();

            Signal tmp = InputSignal;
            if(L > 0)
            {
                int N = InputSignal.Samples.Count - 1;
                int index = InputSignal.SamplesIndices[0];
                for (int i = 0; i < N ;i++)
                {
                    ModifiedListSamples.Add(InputSignal.Samples[i]);
                    ModifiedListIndcies.Add(index++);
                    for (int j =0; j < (L-1);j++)
                    {
                        ModifiedListSamples.Add(0);
                        ModifiedListIndcies.Add(index++);
                    }
                    count++;
                }
                ModifiedListSamples.Add(InputSignal.Samples[N]);
                ModifiedListIndcies.Add(index++);

                tmp.Samples = ModifiedListSamples;
                tmp.SamplesIndices = ModifiedListIndcies;

                FIR FIR = new FIR();

                FIR.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
                FIR.InputFS = 8000;
                FIR.InputStopBandAttenuation = 50;
                FIR.InputCutOffFrequency = 1500;
                FIR.InputTransitionBand = 500;
                FIR.InputTimeDomainSignal = tmp;

                FIR.Run();
                tmp = FIR.OutputYn;
                flag = false;
                ModifiedListSamples.Clear();
                ModifiedListIndcies.Clear();
            }
            if (M > 0)
            {
                if (flag)
                {
                    FIR FIR = new FIR();

                    FIR.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
                    FIR.InputFS = 8000;
                    FIR.InputStopBandAttenuation = 50;
                    FIR.InputCutOffFrequency = 1500;
                    FIR.InputTransitionBand = 500;
                    FIR.InputTimeDomainSignal = tmp;

                    FIR.Run();
                    tmp = FIR.OutputYn;
                }

                int N = tmp.Samples.Count - 1;
                int index = tmp.SamplesIndices[0];
                for (int i = 0; i < N; i++)
                {
                    ModifiedListSamples.Add(tmp.Samples[i]);
                    ModifiedListIndcies.Add(index++);
                    for (int j = 0; j < (M - 1); j++)
                    {
                        i++;
                    }
                    count++;
                }

                tmp.Samples = ModifiedListSamples;
                tmp.SamplesIndices = ModifiedListIndcies;

            }
            OutputSignal = new Signal( tmp.Samples , tmp.SamplesIndices , false);
        }
    }

}