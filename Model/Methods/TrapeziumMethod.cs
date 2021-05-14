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
			int steps = 0;
			int stepsOnOneSegment;
			double resultValue = 0;
			double intervalSize = Math.Abs(right - left) / segmentNumber;
			double currentLeft = left, currentRight = left + intervalSize;
			for (int i = 0; i < segmentNumber; i++)
			{
				resultValue += Recalculate(function, currentLeft, currentRight, precision, out stepsOnOneSegment);
				steps += stepsOnOneSegment;
				currentLeft = currentRight;
				currentRight += intervalSize;
			}
			AutoStepMethodAnswer answer = new AutoStepMethodAnswer(ToString(), resultValue, steps);
			return answer;
		}

		private double Recalculate(Func<double, double> function, double left, double right, double precision, out int iters) 
		{
			double step = Math.Abs(left - right);
			double result = -1, previousResult = 0;
			iters = 0;
			while (Math.Abs(result - previousResult) > precision)
			{
				iters = 0;
				previousResult = result;
				result = 0;
				double x = left + step;
				while (x < right)
				{
					result += step * function(x);
					x += step;
					iters++;
				}
				result += (step / 2) * (function(left) + function(right));
				step /= 2;
			}
			return result;
		}


		public override double GetSolutionWithFixedStep(Func<double, double> function, double left, double right, double step)
		{
			double res = 0;
			double x = left + step;
			while (x < right) 
			{
				res += step * function(x);
				x += step;
			}
			res += (step / 2) * (function(left) + function(right));
			return res;

		}

		public override string ToString() => "Метод трапеций";
	}
}
