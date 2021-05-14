using KantorLr9.Model.Data;
using KantorLr9.Model.Methods.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KantorLr9.Model.Methods
{
	public class MediumRectangleMethod : NumericalIntegrationMethod
	{
		public override AutoStepMethodAnswer GetSolutionWithAutoStep(Func<double, double> function, double left, double right, double precision, double segmentNumber)
		{
			double intervalSize = Math.Abs(right - left) / segmentNumber;
			double currentLeft = left, currentRight = left + intervalSize;
			int stepsOnOneSegment;
			int steps = 0;
			double resultValue = 0;
			for (int i = 0; i < segmentNumber; i++)
			{
				resultValue += Recalcuate(function, currentLeft, currentRight, precision, out stepsOnOneSegment);
				steps += stepsOnOneSegment;
				currentLeft = currentRight;
				currentRight += intervalSize;
			}
			AutoStepMethodAnswer answer = new AutoStepMethodAnswer(ToString(), resultValue, steps);
			return answer;
		}

		private double Recalcuate(Func<double, double> function, double left, double right, double precision, out int iters)
		{
			double step = Math.Abs(left - right);
			double result = -1, previousResult = 0;
			iters = 0;
			while (Math.Abs(result - previousResult) > precision)
			{
				iters = 0;
				previousResult = result;
				result = 0;
				double x = left + (step / 2);
				while (x < right) 
				{
					result += function(x);
					x += step;
					iters++;
				}
				result *= step;
				step /= 2;
			}
			return result;
		}


		public override double GetSolutionWithFixedStep(Func<double, double> function, double left, double right, double step)
		{
			double res = 0;
			double x = left + (step / 2);
			while (x < right) 
			{
				res += function(x);
				x += step;
			}
			res *= step;
			return res;
		}

		public override string ToString() => "Метод средних прямоугольников";
	}
}
