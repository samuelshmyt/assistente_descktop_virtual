using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shmytv1
{
    public class CalcSolver
    {
        public static string solver(string expressao)
        {
            string[] parts = expressao.Split(' ');

            double x = double.Parse(parts[0]);
            double y = double.Parse(parts[2]);
            double z = 0.0;

            switch (parts[1])
            {
                case "mais":
                    z = x + y;
                    break;
                case "menos":
                    z = x - y;
                    break;
                case "vezes":
                    z = x * y;
                    break;
                case "por":
                    if (x >= y)
                    {
                        z = x / y;
                    }
                    else
                    {
                        z = y / x;
                    }
                    break;
            }
            return z.ToString();
        }
    }
}
