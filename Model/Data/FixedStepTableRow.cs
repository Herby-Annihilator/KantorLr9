using System;
using System.Collections.Generic;
using System.Text;

namespace KantorLr9.Model.Data
{
	public class FixedStepTableRow
	{
		public string MethodName { get; set; }
		public double CalculatedValue { get; set; }
		public double RealValue { get; set; }
		public double Difference { get => Math.Abs(CalculatedValue - RealValue); }

		public FixedStepTableRow(string methodName, double calculatedValue, double realValue)
		{
			MethodName = methodName;
			CalculatedValue = calculatedValue;
			RealValue = realValue;
		}
	}
}
