using System;
using System.Collections.Generic;
using System.Text;
using KantorLr9.Model.Data;

namespace KantorLr9.Model.Methods.Base
{
	public abstract class NumericalIntegrationMethod
	{
		public abstract double GetSolutionWithFixedStep(Func<double, double> function, double left, double right, double step);
		public abstract AutoStepMethodAnswer GetSolutionWithAutoStep(Func<double, double> function, double left, double right, double precision, double segmentNumber);
	}
}
