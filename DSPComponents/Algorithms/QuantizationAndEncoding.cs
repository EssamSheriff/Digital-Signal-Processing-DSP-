using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class QuantizationAndEncoding : Algorithm
    {
        // You will have only one of (InputLevel or InputNumBits), the other property will take a negative value
        // If InputNumBits is given, you need to calculate and set InputLevel value and vice versa
        public int InputLevel { get; set; }
        public int InputNumBits { get; set; }
        public Signal InputSignal { get; set; }
        public Signal OutputQuantizedSignal { get; set; }
        public List<int> OutputIntervalIndices { get; set; }
        public List<string> OutputEncodedSignal { get; set; }
        public List<float> OutputSamplesError { get; set; }

        public override void Run()
        {
            OutputIntervalIndices = new List<int>();
            OutputSamplesError = new List<float>();
            OutputEncodedSignal = new List<string>();



            List<float> MidPoints = new List<float>();
            float MaxNumber = float.MinValue, MinNumber = float.MaxValue, NewDiff, OldDiff;
            string binary;



            if (InputLevel <= 0)
            {
                InputLevel = (int)Math.Pow(2, InputNumBits);
            }
            else
            {
                InputNumBits = (int)Math.Log(InputLevel, 2);
            }




            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                if (InputSignal.Samples[i] > MaxNumber) MaxNumber = InputSignal.Samples[i];
                else if (InputSignal.Samples[i] < MinNumber) MinNumber = InputSignal.Samples[i];
            }
            float delta = (MaxNumber - MinNumber) / InputLevel;



            for (int i = 0; i < InputLevel; i++)
            {
                MidPoints.Add((float)Math.Round((MinNumber + (MinNumber + delta)) / 2, 3));
                MinNumber = MinNumber + delta;
            }



            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {



                OldDiff = (float)Math.Round(Math.Abs(InputSignal.Samples[i] - MidPoints[0]), 2);
                for (int j = 1; j < MidPoints.Count; j++)
                {
                    NewDiff = (float)Math.Round(Math.Abs(InputSignal.Samples[i] - MidPoints[j]), 2);
                    if (NewDiff <= OldDiff && j != MidPoints.Count - 1)
                    {
                        OldDiff = NewDiff;
                    }
                    else if (j == MidPoints.Count - 1 && NewDiff < OldDiff)
                    {
                        OutputIntervalIndices.Add(j + 1);
                        binary = Convert.ToString(j, 2).PadLeft(InputNumBits, '0');
                        OutputEncodedSignal.Add(binary);
                        OutputSamplesError.Add((float)Math.Round((MidPoints[j] - InputSignal.Samples[i]), 3));
                        InputSignal.Samples[i] = MidPoints[j];
                        break;
                    }
                    else
                    {
                        OutputIntervalIndices.Add(j);
                        binary = Convert.ToString(j - 1, 2).PadLeft(InputNumBits, '0');
                        OutputEncodedSignal.Add(binary);
                        OutputSamplesError.Add((float)Math.Round((MidPoints[j - 1] - InputSignal.Samples[i]), 3));
                        InputSignal.Samples[i] = MidPoints[j - 1];
                        break;
                    }
                }
            }



            OutputQuantizedSignal = new Signal(InputSignal.Samples, false);
        }
    }
}
