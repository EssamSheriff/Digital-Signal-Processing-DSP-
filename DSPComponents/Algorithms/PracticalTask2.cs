﻿﻿using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace DSPAlgorithms.Algorithms
{
    public class PracticalTask2 : Algorithm
    {
        public String SignalPath { get; set; }
        public float Fs { get; set; }
        public float miniF { get; set; }
        public float maxF { get; set; }
        public float newFs { get; set; }
        public int L { get; set; } //upsampling factor
        public int M { get; set; } //downsampling factor
        public Signal OutputFreqDomainSignal { get; set; }

        public override void Run()
        {
            Signal InputSignal = LoadSignal(SignalPath);

            OutputFreqDomainSignal = new Signal(InputSignal.Samples, false);
            Signal signal = InputSignal;

            FIR fIR = new FIR();
            fIR.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.BAND_PASS;

            fIR.InputFS = Fs;
            fIR.InputStopBandAttenuation = 50;
            fIR.InputTransitionBand = 500;
            fIR.InputF1 = miniF;
            fIR.InputF2 = maxF;
            fIR.InputTimeDomainSignal = InputSignal;
            fIR.Run();
            signal = fIR.OutputYn;


            if (newFs >= 2 * maxF)
            {
                Sampling sampling = new Sampling();
                sampling.InputSignal = signal;
                sampling.L = L;
                sampling.M = M;
                sampling.Run();
                signal = sampling.OutputSignal;
            }
            else
                Console.WriteLine("newFs is not valid");

            DC_Component dC = new DC_Component();
            dC.InputSignal = signal;
            dC.Run();
            signal = dC.OutputSignal;

            Normalizer norm = new Normalizer();
            norm.InputSignal = signal;
            norm.InputMinRange = -1;
            norm.InputMaxRange = 1;
            norm.Run();
            signal = norm.OutputNormalizedSignal;

            DiscreteFourierTransform dft = new DiscreteFourierTransform();
            dft.InputTimeDomainSignal = signal;
            dft.InputSamplingFrequency = Fs;
            dft.Run();
            signal = dft.OutputFreqDomainSignal;

            OutputFreqDomainSignal = signal;

        }

        public Signal LoadSignal(string filePath)
        {
            Stream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var sr = new StreamReader(stream);

            var sigType = byte.Parse(sr.ReadLine());
            var isPeriodic = byte.Parse(sr.ReadLine());
            long N1 = long.Parse(sr.ReadLine());

            List<float> SigSamples = new List<float>(unchecked((int)N1));
            List<int> SigIndices = new List<int>(unchecked((int)N1));
            List<float> SigFreq = new List<float>(unchecked((int)N1));
            List<float> SigFreqAmp = new List<float>(unchecked((int)N1));
            List<float> SigPhaseShift = new List<float>(unchecked((int)N1));

            if (sigType == 1)
            {
                SigSamples = null;
                SigIndices = null;
            }

            for (int i = 0; i < N1; i++)
            {
                if (sigType == 0 || sigType == 2)
                {
                    var timeIndex_SampleAmplitude = sr.ReadLine().Split();
                    SigIndices.Add(int.Parse(timeIndex_SampleAmplitude[0]));
                    SigSamples.Add(float.Parse(timeIndex_SampleAmplitude[1]));
                }
                else
                {
                    var Freq_Amp_PhaseShift = sr.ReadLine().Split();
                    SigFreq.Add(float.Parse(Freq_Amp_PhaseShift[0]));
                    SigFreqAmp.Add(float.Parse(Freq_Amp_PhaseShift[1]));
                    SigPhaseShift.Add(float.Parse(Freq_Amp_PhaseShift[2]));
                }
            }

            if (!sr.EndOfStream)
            {
                long N2 = long.Parse(sr.ReadLine());

                for (int i = 0; i < N2; i++)
                {
                    var Freq_Amp_PhaseShift = sr.ReadLine().Split();
                    SigFreq.Add(float.Parse(Freq_Amp_PhaseShift[0]));
                    SigFreqAmp.Add(float.Parse(Freq_Amp_PhaseShift[1]));
                    SigPhaseShift.Add(float.Parse(Freq_Amp_PhaseShift[2]));
                }
            }

            stream.Close();
            return new Signal(SigSamples, SigIndices, isPeriodic == 1, SigFreq, SigFreqAmp, SigPhaseShift);
        }
    }
}
