using System;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000019 RID: 25
	internal static class StringFunctions
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00002D48 File Offset: 0x00000F48
		public static void Add(FunctionFactory factory)
		{
			IFunctionEvaluator functionEvaluator = EvaluatorFactory.CreateStringFunctionEvaluator();
			factory.Add(typeof(int), new FixedNumberArgumentValidator("ASC", 1), functionEvaluator);
			factory.Add(typeof(int), new FixedNumberArgumentValidator("ASCW", 1), functionEvaluator);
			factory.Add(typeof(char), new FixedNumberArgumentValidator("CHR", 1), functionEvaluator);
			factory.Add(typeof(char), new FixedNumberArgumentValidator("CHRW", 1), functionEvaluator);
			factory.Add(typeof(string), new VariableNumberArgumentValidator("FORMAT", 1, 2), functionEvaluator);
			factory.Add(typeof(string), new VariableNumberArgumentValidator("FORMATDATETIME", 1, 2), functionEvaluator);
			factory.Add(typeof(char), new FixedNumberArgumentValidator("UCASE", 1), functionEvaluator);
			factory.Add(typeof(char), new FixedNumberArgumentValidator("LCASE", 1), functionEvaluator);
			factory.Add(typeof(string), new FixedNumberArgumentValidator("LTRIM", 1), functionEvaluator);
			factory.Add(typeof(string), new FixedNumberArgumentValidator("RTRIM", 1), functionEvaluator);
			factory.Add(typeof(string), new FixedNumberArgumentValidator("TRIM", 1), functionEvaluator);
			factory.Add(typeof(string), new VariableNumberArgumentValidator("REPLACE", 3, 6), functionEvaluator);
			factory.Add(typeof(string), new VariableNumberArgumentValidator("JOIN", 1, 2), functionEvaluator);
			factory.Add(typeof(string), new FixedNumberArgumentValidator("LEFT", 2), functionEvaluator);
			factory.Add(typeof(string), new FixedNumberArgumentValidator("RIGHT", 2), functionEvaluator);
			factory.Add(typeof(string), new VariableNumberArgumentValidator("MID", 2, 3), functionEvaluator);
			factory.Add(typeof(string), new FixedNumberArgumentValidator("SPACE", 1), functionEvaluator);
			factory.Add(typeof(int), new FixedNumberArgumentValidator("LEN", 1), functionEvaluator);
		}
	}
}
