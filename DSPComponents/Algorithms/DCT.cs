using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DCT: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            //throw new NotImplementedException();
            List<float> result = new List<float>();
            
            double x = (double)2 / (double)InputSignal.Samples.Count;
            double a = Math.Sqrt(x);
            for (int k = 0; k < InputSignal.Samples.Count; k++)
            {
                double sum = 0;
                for (int j = 0; j < InputSignal.Samples.Count; j++)
                {
                    double num = (2 * j - 1) * (2 * k - 1) * Math.PI;
                    double dem = 4 * InputSignal.Samples.Count;
                    double cosPart = Math.Cos(num / dem);
                    sum += InputSignal.Samples[j] * cosPart;
                }
                result.Add((float)(a * sum));
                Console.WriteLine(result[k]);
            }
            OutputSignal = new Signal(result, false);
            //Console.WriteLine("Enter number of coefficient to save: ");
            //string c = Console.ReadLine();
            //int C = Convert.ToInt32(c);
            string filePath = "DCT_components.txt";
            FileStream DCT_file = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            StreamWriter file = new StreamWriter(DCT_file);
            file.BaseStream.Seek(0, SeekOrigin.End);
            for(int i = 0; i < 5;i++)
                file.WriteLine(OutputSignal.Samples[i]);
            file.Close();
        }
    }
}
