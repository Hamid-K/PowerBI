using System;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000018 RID: 24
	internal static class MathFunctions
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00002D18 File Offset: 0x00000F18
		public static void Add(FunctionFactory factory)
		{
			IFunctionEvaluator functionEvaluator = EvaluatorFactory.CreateMathFunctionEvaluator();
			factory.Add(typeof(double), new VariableNumberArgumentValidator("ROUND", 1, 3), functionEvaluator);
		}
	}
}
