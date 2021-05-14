using KantorLr9.Model.Data;
using KantorLr9.Model.Methods.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KantorLr9.Model.Methods
{
	public class TrapeziumMethod : NumericalIntegrationMethod
	{
		public override AutoStepMethodAnswer GetSolutionWithAutoStep(Func<double, double> function, double left, double right, double precision, double segmentNumber)
		{
			throw new NotImplementedException();
		}

		public override double GetSolutionWithFixedStep(Func<double, double> function, double left, double right, double step)
		{
			double res = 0;
			double x = left + step;
			while (x < right) //должно быть как раз от 1 до n-1 
			{
				res += step * function(x);
				x += step;
			}
			res += (step / 2) * (function(left) + function(right));
			return res;

		}
	}
}
