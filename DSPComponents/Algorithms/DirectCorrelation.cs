using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        public override void Run()
        {
            OutputNonNormalizedCorrelation = new List<float>();
            OutputNormalizedCorrelation = new List<float>();
            
            if (InputSignal2 == null)
            {
                List<double> new_input_signal = new List<double>();

                int N = InputSignal1.Samples.Count;

                for (int i = 0; i < N; i++)
                {
                    new_input_signal.Add(InputSignal1.Samples[i]);
                }


                double norm = 0;
                double summ = 0;

                for (int i = 0; i < N; i++)
                {
                    summ += Math.Pow(new_input_signal[i],2);
                }
                norm = (Math.Sqrt(Math.Pow(summ, 2)))/N;

                if (InputSignal1.Periodic == true)
                {

                    for (int i = 0; i < N; i++)
                    {
                        double sum = 0;
                        if (i == 0)
                        {
                            for (int j = 0; j < N; j++)
                                sum += new_input_signal[j] * InputSignal1.Samples[j];
                        }
                        else
                        {
                            double new_last_element = new_input_signal[0];
                            for (int j = 0; j < N - 1; j++)
                            {
                                new_input_signal[j] = new_input_signal[j + 1];
                                sum += new_input_signal[j] * InputSignal1.Samples[j];
                            }
                            new_input_signal[N - 1] = new_last_element;
                            sum += new_input_signal[N - 1] * InputSignal1.Samples[N - 1];
                        }
                        OutputNonNormalizedCorrelation.Add((float)sum / N);
                    }

                }

                else
                {
                    for (int i = 0; i < N; i++)
                    {
                        double sum = 0;
                        if (i == 0)
                        {
                            for (int j = 0; j < N; j++)
                                sum += new_input_signal[j] * InputSignal1.Samples[j];
                        }
                        else
                        {
                            double new_shift_element = 0;
                            for (int j = 0; j < N - 1; j++)
                            {
                                new_input_signal[j] = new_input_signal[j + 1];
                                sum += new_input_signal[j] * InputSignal1.Samples[j];
                            }
                            new_input_signal[N - 1] = new_shift_element;
                            sum += new_input_signal[N - 1] * InputSignal1.Samples[N - 1];
                        }
                        OutputNonNormalizedCorrelation.Add((float)sum / N);
                    }

                }
                //Console.WriteLine(norm);
                for (int i = 0; i < OutputNonNormalizedCorrelation.Count; i++)
                { 
                    OutputNormalizedCorrelation.Add((float)(OutputNonNormalizedCorrelation[i] / norm));
                    //Console.WriteLine(OutputNormalizedCorrelation[i]);
                }

            }
            
            else
            {
                //List<double> signal2 = new List<double>();

                int N = InputSignal1.Samples.Count;
                /*for (int i = 0; i < N; i++)  
                    signal2.Add(InputSignal2.Samples[i]);
                */
                double norm = 0;

                double summ1 = 0;

                double summ2 = 0;

                for (int i = 0; i < N; i++)
                {
                    summ1 += Math.Pow(InputSignal1.Samples[i],2);
                    summ2+= Math.Pow(InputSignal2.Samples[i],2);
                }
                norm = (Math.Sqrt(summ1 * summ2)) / N;

                if (InputSignal1.Periodic == true)
                {

                    for (int i = 0; i < N; i++)
                    {
                        double sum = 0;
                        if (i == 0)
                        {
                            for (int j = 0; j < N; j++)
                                sum += InputSignal1.Samples[j] * InputSignal2.Samples[j];
                        }
                        else
                        {
                            float first_element = InputSignal2.Samples[0];
                            for (int j = 0; j < N - 1; j++)
                            {
                                InputSignal2.Samples[j] = InputSignal2.Samples[j + 1];
                                sum += InputSignal2.Samples[j] * InputSignal1.Samples[j];
                            }
                            InputSignal2.Samples[N - 1] = first_element;
                            sum += InputSignal2.Samples[N - 1] * InputSignal1.Samples[N - 1];
                        }
                        OutputNonNormalizedCorrelation.Add((float)sum / N);
                    }
                }

                else
                {

                    for (int i = 0; i < N; i++)
                    {
                        double sum = 0;
                        if (i == 0)
                        {
                            for (int j = 0; j < N; j++)
                                sum += InputSignal1.Samples[j] * InputSignal2.Samples[j];
                        }
                        else
                        {

                            float first_element = 0;
                            for (int j = 0; j < N - 1; j++)
                            {
                                InputSignal2.Samples[j] = InputSignal2.Samples[j + 1];
                                sum += InputSignal2.Samples[j] * InputSignal1.Samples[j];
                            }
                            InputSignal2.Samples[N - 1] = first_element;
                            sum += InputSignal2.Samples[N - 1] * InputSignal1.Samples[N - 1];
                        }
                        OutputNonNormalizedCorrelation.Add((float)sum / N);
                    }
                }

                //OutputNonNormalizedCorrelation = cross;
                
                for (int i = 0; i < N; i++)
                    OutputNormalizedCorrelation.Add((float)(OutputNonNormalizedCorrelation[i] / norm));
            }

        }
    }
}