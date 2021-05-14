using System;
using System.Collections.Generic;
using System.Text;

namespace KantorLr9.Model.Data
{
	public class AutoStepMethodAnswer
	{
		public string MethodName { get; set; }
		public double Value { get; set; }
		public int CountOfSteps { get; set; }

		public AutoStepMethodAnswer(string methodName, double value, int countOfSteps)
		{
			MethodName = methodName;
			Value = value;
			CountOfSteps = countOfSteps;
		}
	}
}
