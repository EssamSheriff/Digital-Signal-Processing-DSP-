using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FIR : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public FILTER_TYPES InputFilterType { get; set; }
        public float InputFS { get; set; }
        public float? InputCutOffFrequency { get; set; }
        public float? InputF1 { get; set; }
        public float? InputF2 { get; set; }
        public float InputStopBandAttenuation { get; set; }
        public float InputTransitionBand { get; set; }
        public Signal OutputHn { get; set; }
        public Signal OutputYn { get; set; }

        List<int> inds = new List<int>();
        List<float> vals = new List<float>();

        public override void Run()
        {

            if (InputFilterType == FILTER_TYPES.LOW)
            {
                double F_C = (double)(InputCutOffFrequency + (InputTransitionBand / 2)) / InputFS;

                if (InputStopBandAttenuation >= 0 && InputStopBandAttenuation < 21)
                {
                    float Delta_F = InputTransitionBand / InputFS;
                    Double N = Math.Ceiling(0.9 / Delta_F);
                    if (N % 2 == 0)
                    {
                        N = N + 1;
                    }
                    int count = -1 * (int)N / 2;
                    for (int i = 0; i < N; i++)
                    {
                        double W = 1;
                        double H_d = 0;
                        if (count == 0)
                        {
                            H_d = 2 * F_C;
                        }
                        else
                        {
                            double Wc = 2 * Math.PI * F_C;
                            H_d = (2 * F_C) * (Math.Sin(count * Wc) / (count * Wc));
                        }
                        float H = (float)(H_d * W);
                        vals.Add(H);
                        inds.Add(count);
                        count++;
                    }
                    OutputHn = new Signal(vals, inds, false);

                    FastConvolution fc = new FastConvolution();
                    fc.InputSignal1 = InputTimeDomainSignal;

                    fc.InputSignal2 = new Signal(new List<float>(vals), new List<int>(inds), false);
                    fc.Run();

                    OutputYn = fc.OutputConvolvedSignal;
                }
                else if (InputStopBandAttenuation >= 21 && InputStopBandAttenuation < 44)
                {
                    float Delta_F = InputTransitionBand / InputFS;
                    Double N = Math.Ceiling(3.1 / Delta_F);
                    if (N % 2 == 0)
                    {
                        N = N + 1;
                    }
                    int count = -1 * (int)N / 2;
                    for (int i = 0; i < N; i++)
                    {
                        double theta = (2 * Math.PI * count) / N;
                        double W = 0.5 + 0.5 * (Math.Cos(theta));
                        double H_d = 0;
                        if (count == 0)
                        {
                            H_d = 2 * F_C;
                        }
                        else
                        {
                            double Wc = 2 * Math.PI * F_C;
                            H_d = (2 * F_C) * (Math.Sin(count * Wc) / (count * Wc));
                        }
                        float H = (float)(H_d * W);
                        vals.Add(H);
                        inds.Add(count);
                        count++;
                    }
                    OutputHn = new Signal(vals, inds, false);

                    FastConvolution fc = new FastConvolution();
                    fc.InputSignal1 = InputTimeDomainSignal;

                    fc.InputSignal2 = new Signal(new List<float>(vals), new List<int>(inds), false);
                    fc.Run();

                    OutputYn = fc.OutputConvolvedSignal;

                }
                else if (InputStopBandAttenuation >= 44 && InputStopBandAttenuation < 53)
                {
                    float Delta_F = InputTransitionBand / InputFS;
                    Double N = Math.Ceiling(3.3 / Delta_F);
                    if (N % 2 == 0)
                    {
                        N = N + 1;
                    }
                    int count = -1 * (int)N / 2;
                    for (int i = 0; i < N; i++)
                    {
                        double theta = (2 * Math.PI * count) / N;
                        double W = 0.54 + 0.46 * (Math.Cos(theta));
                        double H_d = 0;
                        if (count == 0)
                        {
                            H_d = 2 * F_C;
                        }
                        else
                        {
                            double Wc = 2 * Math.PI * F_C;
                            H_d = (2 * F_C) * (Math.Sin(count * Wc) / (count * Wc));
                        }
                        float H = (float)(H_d * W);
                        vals.Add(H);
                        inds.Add(count);
                        count++;
                    }
                    OutputHn = new Signal(vals, inds, false);

                    FastConvolution fc = new FastConvolution();
                    fc.InputSignal1 = InputTimeDomainSignal;

                    fc.InputSignal2 = new Signal(new List<float>(vals), new List<int>(inds), false);
                    fc.Run();

                    OutputYn = fc.OutputConvolvedSignal;

                }
                else if (InputStopBandAttenuation >= 53 && InputStopBandAttenuation < 74)
                {
                    float Delta_F = InputTransitionBand / InputFS;
                    Double N = Math.Ceiling(5.5 / Delta_F);
                    if (N % 2 == 0)
                    {
                        N = N + 1;
                    }
                    int count = -1 * (int)N / 2;
                    for (int i = 0; i < N; i++)
                    {
                        double theta1 = (2 * Math.PI * count) / (N - 1);
                        double theta2 = (4 * Math.PI * count) / (N - 1);
                        double W = 0.42 + 0.5 * (Math.Cos(theta1)) + 0.08 * (Math.Cos(theta2));
                        double H_d = 0;
                        if (count == 0)
                        {
                            H_d = 2 * F_C;
                        }
                        else
                        {
                            double Wc = 2 * Math.PI * F_C;
                            H_d = (2 * F_C) * (Math.Sin(count * Wc) / (count * Wc));
                        }
                        float H = (float)(H_d * W);
                        vals.Add(H);
                        inds.Add(count);
                        count++;
                    }
                    OutputHn = new Signal(vals, inds, false);

                    FastConvolution fc = new FastConvolution();
                    fc.InputSignal1 = InputTimeDomainSignal;

                    fc.InputSignal2 = new Signal(new List<float>(vals), new List<int>(inds), false);
                    fc.Run();

                    OutputYn = fc.OutputConvolvedSignal;
                }

            }
            else if (InputFilterType == FILTER_TYPES.HIGH)
            {
                double F_C = (double)(InputCutOffFrequency - (InputTransitionBand / 2)) / InputFS;
                if (InputStopBandAttenuation >= 0 && InputStopBandAttenuation < 21)
                {
                    float Delta_F = InputTransitionBand / InputFS;
                    Double N = Math.Ceiling(0.9 / Delta_F);
                    if (N % 2 == 0)
                    {
                        N = N + 1;
                    }
                    int count = -1 * (int)N / 2;
                    for (int i = 0; i < N; i++)
                    {
                        double W = 1;
                        double H_d = 0;
                        if (count == 0)
                        {
                            H_d = 1 - (2 * F_C);
                        }
                        else
                        {
                            double Wc = 2 * Math.PI * F_C;
                            H_d = (-2 * F_C) * (Math.Sin(count * Wc) / (count * Wc));
                        }
                        float H = (float)(H_d * W);
                        vals.Add(H);
                        inds.Add(count);
                        count++;
                    }
                    OutputHn = new Signal(vals, inds, false);

                    FastConvolution fc = new FastConvolution();
                    fc.InputSignal1 = InputTimeDomainSignal;

                    fc.InputSignal2 = new Signal(new List<float>(vals), new List<int>(inds), false);
                    fc.Run();

                    OutputYn = fc.OutputConvolvedSignal;
                }
                else if (InputStopBandAttenuation >= 21 && InputStopBandAttenuation < 44)
                {
                    float Delta_F = InputTransitionBand / InputFS;
                    Double N = Math.Ceiling(3.1 / Delta_F);
                    if (N % 2 == 0)
                    {
                        N = N + 1;
                    }
                    int count = -1 * (int)N / 2;
                    for (int i = 0; i < N; i++)
                    {
                        double theta = (2 * Math.PI * count) / N;
                        double W = 0.5 + 0.5 * (Math.Cos(theta));
                        double H_d = 0;
                        if (count == 0)
                        {
                            H_d = 1 - (2 * F_C);
                        }
                        else
                        {
                            double Wc = 2 * Math.PI * F_C;
                            H_d = (-2 * F_C) * (Math.Sin(count * Wc) / (count * Wc));
                        }
                        float H = (float)(H_d * W);
                        vals.Add(H);
                        inds.Add(count);
                        count++;
                    }
                    OutputHn = new Signal(vals, inds, false);

                    FastConvolution fc = new FastConvolution();
                    fc.InputSignal1 = InputTimeDomainSignal;

                    fc.InputSignal2 = new Signal(new List<float>(vals), new List<int>(inds), false);
                    fc.Run();

                    OutputYn = fc.OutputConvolvedSignal;

                }
                else if (InputStopBandAttenuation >= 44 && InputStopBandAttenuation < 53)
                {
                    float Delta_F = InputTransitionBand / InputFS;
                    Double N = Math.Ceiling(3.3 / Delta_F);
                    if (N % 2 == 0)
                    {
                        N = N + 1;
                    }
                    int count = -1 * (int)N / 2;
                    for (int i = 0; i < N; i++)
                    {
                        double theta = (2 * Math.PI * count) / N;
                        double W = 0.54 + 0.46 * (Math.Cos(theta));
                        double H_d = 0;
                        if (count == 0)
                        {
                            H_d = 1 - (2 * F_C);
                        }
                        else
                        {
                            double Wc = 2 * Math.PI * F_C;
                            H_d = (-2 * F_C) * (Math.Sin(count * Wc) / (count * Wc));
                        }
                        float H = (float)(H_d * W);
                        vals.Add(H);
                        inds.Add(count);
                        count++;
                    }
                    OutputHn = new Signal(vals, inds, false);

                    FastConvolution fc = new FastConvolution();
                    fc.InputSignal1 = InputTimeDomainSignal;

                    fc.InputSignal2 = new Signal(new List<float>(vals), new List<int>(inds), false);
                    fc.Run();

                    OutputYn = fc.OutputConvolvedSignal;

                }
                else if (InputStopBandAttenuation >= 53 && InputStopBandAttenuation < 74)
                {
                    float Delta_F = InputTransitionBand / InputFS;
                    Double N = Math.Ceiling(5.5 / Delta_F);
                    if (N % 2 == 0)
                    {
                        N = N + 1;
                    }
                    int count = -1 * (int)N / 2;
                    for (int i = 0; i < N; i++)
                    {
                        double theta1 = (2 * Math.PI * count) / (N - 1);
                        double theta2 = (4 * Math.PI * count) / (N - 1);
                        double W = 0.42 + 0.5 * (Math.Cos(theta1)) + 0.08 * (Math.Cos(theta2));
                        double H_d = 0;
                        if (count == 0)
                        {
                            H_d = 1 - (2 * F_C);
                        }
                        else
                        {
                            double Wc = 2 * Math.PI * F_C;
                            H_d = (-2 * F_C) * (Math.Sin(count * Wc) / (count * Wc));
                        }
                        float H = (float)(H_d * W);
                        vals.Add(H);
                        inds.Add(count);
                        count++;
                    }
                    OutputHn = new Signal(vals, inds, false);

                    FastConvolution fc = new FastConvolution();
                    fc.InputSignal1 = InputTimeDomainSignal;

                    fc.InputSignal2 = new Signal(new List<float>(vals), new List<int>(inds), false);
                    fc.Run();

                    OutputYn = fc.OutputConvolvedSignal;
                }

            }
            else if (InputFilterType == FILTER_TYPES.BAND_STOP)
            {
                double F_C_1 = (double)(InputF1 + (InputTransitionBand / 2)) / InputFS;
                double F_C_2 = (double)(InputF2 - (InputTransitionBand / 2)) / InputFS;
                if (InputStopBandAttenuation >= 0 && InputStopBandAttenuation < 21)
                {
                    float Delta_F = InputTransitionBand / InputFS;
                    Double N = Math.Ceiling(0.9 / Delta_F);
                    if (N % 2 == 0)
                    {
                        N = N + 1;
                    }
                    int count = -1 * (int)N / 2;

                    for (int i = 0; i < N; i++)
                    {
                        double W = 1;
                        double H_d = 0;
                        if (count == 0)
                        {
                            H_d = 1 - (2 * (F_C_2 - F_C_1));
                        }
                        else
                        {
                            double Wc1 = 2 * Math.PI * F_C_1;
                            double Wc2 = 2 * Math.PI * F_C_2;
                            double H_d_1 = 2 * F_C_1 * (Math.Sin(count * Wc1) / (count * Wc1));
                            double H_d_2 = 2 * F_C_2 * (Math.Sin(count * Wc2) / (count * Wc2));
                            H_d = H_d_1 - H_d_2;
                        }
                        float H = (float)(H_d * W);
                        vals.Add(H);
                        inds.Add(count);
                        count++;
                    }
                    OutputHn = new Signal(vals, inds, false);

                    FastConvolution fc = new FastConvolution();
                    fc.InputSignal1 = InputTimeDomainSignal;

                    fc.InputSignal2 = new Signal(new List<float>(vals), new List<int>(inds), false);
                    fc.Run();

                    OutputYn = fc.OutputConvolvedSignal;
                }
                else if (InputStopBandAttenuation >= 21 && InputStopBandAttenuation < 44)
                {
                    float Delta_F = InputTransitionBand / InputFS;
                    Double N = Math.Ceiling(3.1 / Delta_F);
                    if (N % 2 == 0)
                    {
                        N = N + 1;
                    }
                    int count = -1 * (int)N / 2;
                    for (int i = 0; i < N; i++)
                    {
                        double theta = (2 * Math.PI * count) / N;
                        double W = 0.5 + 0.5 * (Math.Cos(theta));
                        double H_d = 0;
                        if (count == 0)
                        {
                            H_d = 1 - (2 * (F_C_2 - F_C_1));
                        }
                        else
                        {
                            double Wc1 = 2 * Math.PI * F_C_1;
                            double Wc2 = 2 * Math.PI * F_C_2;
                            double H_d_1 = 2 * F_C_1 * (Math.Sin(count * Wc1) / (count * Wc1));
                            double H_d_2 = 2 * F_C_2 * (Math.Sin(count * Wc2) / (count * Wc2));
                            H_d = H_d_1 - H_d_2;
                        }
                        float H = (float)(H_d * W);
                        vals.Add(H);
                        inds.Add(count);
                        count++;
                    }
                    OutputHn = new Signal(vals, inds, false);

                    FastConvolution fc = new FastConvolution();
                    fc.InputSignal1 = InputTimeDomainSignal;

                    fc.InputSignal2 = new Signal(new List<float>(vals), new List<int>(inds), false);
                    fc.Run();

                    OutputYn = fc.OutputConvolvedSignal;

                }
                else if (InputStopBandAttenuation >= 44 && InputStopBandAttenuation < 53)
                {
                    float Delta_F = InputTransitionBand / InputFS;
                    Double N = Math.Ceiling(3.3 / Delta_F);
                    if (N % 2 == 0)
                    {
                        N = N + 1;
                    }
                    int count = -1 * (int)N / 2;
                    for (int i = 0; i < N; i++)
                    {
                        double theta = (2 * Math.PI * count) / N;
                        double W = 0.54 + 0.46 * (Math.Cos(theta));
                        double H_d = 0;
                        if (count == 0)
                        {
                            H_d = 1 - (2 * (F_C_2 - F_C_1));
                        }
                        else
                        {
                            double Wc1 = 2 * Math.PI * F_C_1;
                            double Wc2 = 2 * Math.PI * F_C_2;
                            double H_d_1 = 2 * F_C_1 * (Math.Sin(count * Wc1) / (count * Wc1));
                            double H_d_2 = 2 * F_C_2 * (Math.Sin(count * Wc2) / (count * Wc2));
                            H_d = H_d_1 - H_d_2;
                        }
                        float H = (float)(H_d * W);
                        vals.Add(H);
                        inds.Add(count);
                        count++;
                    }
                    OutputHn = new Signal(vals, inds, false);

                    FastConvolution fc = new FastConvolution();
                    fc.InputSignal1 = InputTimeDomainSignal;

                    fc.InputSignal2 = new Signal(new List<float>(vals), new List<int>(inds), false);
                    fc.Run();

                    OutputYn = fc.OutputConvolvedSignal;

                }
                else if (InputStopBandAttenuation >= 53 && InputStopBandAttenuation < 74)
                {
                    float Delta_F = InputTransitionBand / InputFS;
                    Double N = Math.Ceiling(5.5 / Delta_F);
                    if (N % 2 == 0)
                    {
                        N = N + 1;
                    }
                    int count = -1 * (int)N / 2;
                    for (int i = 0; i < N; i++)
                    {
                        double theta1 = (2 * Math.PI * count) / (N - 1);
                        double theta2 = (4 * Math.PI * count) / (N - 1);
                        double W = 0.42 + 0.5 * (Math.Cos(theta1)) + 0.08 * (Math.Cos(theta2));
                        double H_d = 0;
                        if (count == 0)
                        {
                            H_d = 1 - (2 * (F_C_2 - F_C_1));
                        }
                        else
                        {
                            double Wc1 = 2 * Math.PI * F_C_1;
                            double Wc2 = 2 * Math.PI * F_C_2;
                            double H_d_1 = 2 * F_C_1 * (Math.Sin(count * Wc1) / (count * Wc1));
                            double H_d_2 = 2 * F_C_2 * (Math.Sin(count * Wc2) / (count * Wc2));
                            H_d = H_d_1 - H_d_2;
                        }
                        float H = (float)(H_d * W);
                        vals.Add(H);
                        inds.Add(count);
                        count++;
                    }
                    OutputHn = new Signal(vals, inds, false);

                    FastConvolution fc = new FastConvolution();
                    fc.InputSignal1 = InputTimeDomainSignal;

                    fc.InputSignal2 = new Signal(new List<float>(vals), new List<int>(inds), false);
                    fc.Run();

                    OutputYn = fc.OutputConvolvedSignal;
                }
            }
            else if (InputFilterType == FILTER_TYPES.BAND_PASS)
            {
                double F_C_1 = (double)(InputF1 - (InputTransitionBand / 2)) / InputFS;
                double F_C_2 = (double)(InputF2 + (InputTransitionBand / 2)) / InputFS;
                if (InputStopBandAttenuation >= 0 && InputStopBandAttenuation < 21)
                {
                    float Delta_F = InputTransitionBand / InputFS;
                    Double N = Math.Ceiling(0.9 / Delta_F);
                    if (N % 2 == 0)
                    {
                        N = N + 1;
                    }
                    int count = -1 * (int)N / 2;

                    for (int i = 0; i < N; i++)
                    {
                        double W = 1;
                        double H_d = 0;
                        if (count == 0)
                        {
                            H_d = 2 * (F_C_2 - F_C_1);
                        }
                        else
                        {
                            double Wc1 = 2 * Math.PI * F_C_1;
                            double Wc2 = 2 * Math.PI * F_C_2;
                            double H_d_1 = 2 * F_C_1 * (Math.Sin(count * Wc1) / (count * Wc1));
                            double H_d_2 = 2 * F_C_2 * (Math.Sin(count * Wc2) / (count * Wc2));
                            H_d = H_d_2 - H_d_1;
                        }
                        float H = (float)(H_d * W);
                        vals.Add(H);
                        inds.Add(count);
                        count++;
                    }
                    OutputHn = new Signal(vals, inds, false);

                    FastConvolution fc = new FastConvolution();
                    fc.InputSignal1 = InputTimeDomainSignal;

                    fc.InputSignal2 = new Signal(new List<float>(vals), new List<int>(inds), false);
                    fc.Run();

                    OutputYn = fc.OutputConvolvedSignal;
                }
                else if (InputStopBandAttenuation >= 21 && InputStopBandAttenuation < 44)
                {
                    float Delta_F = InputTransitionBand / InputFS;
                    Double N = Math.Ceiling(3.1 / Delta_F);
                    if (N % 2 == 0)
                    {
                        N = N + 1;
                    }
                    int count = -1 * (int)N / 2;
                    for (int i = 0; i < N; i++)
                    {
                        double theta = (2 * Math.PI * count) / N;
                        double W = 0.5 + 0.5 * (Math.Cos(theta));
                        double H_d = 0;
                        if (count == 0)
                        {
                            H_d = 2 * (F_C_2 - F_C_1);
                        }
                        else
                        {
                            double Wc1 = 2 * Math.PI * F_C_1;
                            double Wc2 = 2 * Math.PI * F_C_2;
                            double H_d_1 = 2 * F_C_1 * (Math.Sin(count * Wc1) / (count * Wc1));
                            double H_d_2 = 2 * F_C_2 * (Math.Sin(count * Wc2) / (count * Wc2));
                            H_d = H_d_2 - H_d_1;
                        }
                        float H = (float)(H_d * W);
                        vals.Add(H);
                        inds.Add(count);
                        count++;
                    }
                    OutputHn = new Signal(vals, inds, false);

                    FastConvolution fc = new FastConvolution();
                    fc.InputSignal1 = InputTimeDomainSignal;

                    fc.InputSignal2 = new Signal(new List<float>(vals), new List<int>(inds), false);
                    fc.Run();

                    OutputYn = fc.OutputConvolvedSignal;

                }
                else if (InputStopBandAttenuation >= 44 && InputStopBandAttenuation < 53)
                {
                    float Delta_F = InputTransitionBand / InputFS;
                    Double N = Math.Ceiling(3.3 / Delta_F);
                    if (N % 2 == 0)
                    {
                        N = N + 1;
                    }
                    int count = -1 * (int)N / 2;
                    for (int i = 0; i < N; i++)
                    {
                        double theta = (2 * Math.PI * count) / N;
                        double W = 0.54 + 0.46 * (Math.Cos(theta));
                        double H_d = 0;
                        if (count == 0)
                        {
                            H_d = 2 * (F_C_2 - F_C_1);
                        }
                        else
                        {
                            double Wc1 = 2 * Math.PI * F_C_1;
                            double Wc2 = 2 * Math.PI * F_C_2;
                            double H_d_1 = 2 * F_C_1 * (Math.Sin(count * Wc1) / (count * Wc1));
                            double H_d_2 = 2 * F_C_2 * (Math.Sin(count * Wc2) / (count * Wc2));
                            H_d = H_d_2 - H_d_1;
                        }
                        float H = (float)(H_d * W);
                        vals.Add(H);
                        inds.Add(count);
                        count++;
                    }
                    OutputHn = new Signal(vals, inds, false);

                    FastConvolution fc = new FastConvolution();
                    fc.InputSignal1 = InputTimeDomainSignal;

                    fc.InputSignal2 = new Signal(new List<float>(vals), new List<int>(inds), false);
                    fc.Run();

                    OutputYn = fc.OutputConvolvedSignal;

                }
                else if (InputStopBandAttenuation >= 53 && InputStopBandAttenuation < 74)
                {
                    float Delta_F = InputTransitionBand / InputFS;
                    Double N = Math.Ceiling(5.5 / Delta_F);
                    if (N % 2 == 0)
                    {
                        N = N + 1;
                    }
                    int count = -1 * (int)N / 2;
                    for (int i = 0; i < N; i++)
                    {
                        double theta1 = (2 * Math.PI * count) / (N - 1);
                        double theta2 = (4 * Math.PI * count) / (N - 1);
                        double W = 0.42 + 0.5 * (Math.Cos(theta1)) + 0.08 * (Math.Cos(theta2));
                        double H_d = 0;
                        if (count == 0)
                        {
                            H_d = 2 * (F_C_2 - F_C_1);
                        }
                        else
                        {
                            double Wc1 = 2 * Math.PI * F_C_1;
                            double Wc2 = 2 * Math.PI * F_C_2;
                            double H_d_1 = 2 * F_C_1 * (Math.Sin(count * Wc1) / (count * Wc1));
                            double H_d_2 = 2 * F_C_2 * (Math.Sin(count * Wc2) / (count * Wc2));
                            H_d = H_d_2 - H_d_1;
                        }
                        float H = (float)(H_d * W);
                        vals.Add(H);
                        inds.Add(count);
                        count++;
                    }
                    OutputHn = new Signal(vals, inds, false);

                    FastConvolution fc = new FastConvolution();
                    fc.InputSignal1 = InputTimeDomainSignal;

                    fc.InputSignal2 = new Signal(new List<float>(vals), new List<int>(inds), false);
                    fc.Run();

                    OutputYn = fc.OutputConvolvedSignal;
                }
            }
        }
    }
}
