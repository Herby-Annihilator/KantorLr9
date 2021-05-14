using System;
using System.Collections.Generic;
using System.Text;

namespace KantorLr9.Model.Data
{
	public class AutoStepTableRow
	{
		public string MethodName { get; set; }
		public double Precision { get; set; }
		public double CalculatedValue { get; set; }
		public int StepsCount { get; set; }

		public AutoStepTableRow(string methodName, double precision, double calculatedValue, int stepsCount)
		{
			MethodName = methodName;
			Precision = precision;
			CalculatedValue = calculatedValue;
			StepsCount = stepsCount;
		}
	}
}
