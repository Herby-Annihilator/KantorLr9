using KantorLr9.Model.Data;
using KantorLr9.Model.Methods.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KantorLr9.Model.Methods
{
	public class SimpsonMethod : NumericalIntegrationMethod
	{
		public override AutoStepMethodAnswer GetSolutionWithAutoStep(Func<double, double> function, double left, double right, double precision, double segmentNumber)
		{
			throw new NotImplementedException();
		}

		public override double GetSolutionWithFixedStep(Func<double, double> function, double left, double right, double step)
		{
            double res = 0;
            double odd = 0, even = 0;
            int i = 1;
            double x = left + step;
            while (x < right) //должно быть как раз от 1 до n-1 
            {
                if (i % 2 == 1) odd += function(x);
                else even += function(x);
                x += step;
                i++;
            }
            res += 4 * odd + 2 * even + function(left) + function(right);
            res *= (step / 3);
            return res;

        }
    }
}
