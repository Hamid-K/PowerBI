using System;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000015 RID: 21
	internal static class DateTimeFunctions
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public static void Add(FunctionFactory factory)
		{
			IFunctionEvaluator functionEvaluator = EvaluatorFactory.CreateDateTimeFunctionEvaluator();
			factory.Add(typeof(DateTime), new NoArgumentValidator("NOW"), functionEvaluator);
			factory.Add(typeof(DateTime), new NoArgumentValidator("TODAY"), functionEvaluator);
			factory.Add(typeof(DateTime), new NoArgumentValidator("TIMEOFDAY"), functionEvaluator);
			factory.Add(typeof(DateTime), new FixedNumberArgumentValidator("DATEADD", 3), functionEvaluator);
		}
	}
}
