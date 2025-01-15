using System;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000016 RID: 22
	internal static class ExecutionFlowFunctions
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00002C48 File Offset: 0x00000E48
		public static void Add(FunctionFactory factory)
		{
			IFunctionEvaluator functionEvaluator = EvaluatorFactory.CreateExecutionFlowFunctionEvaluator();
			factory.Add(typeof(object), new FixedNumberArgumentValidator("IIF", 3), functionEvaluator);
			factory.Add(typeof(object), new NoValidationArgumentValidator("SWITCH"), functionEvaluator);
		}
	}
}
