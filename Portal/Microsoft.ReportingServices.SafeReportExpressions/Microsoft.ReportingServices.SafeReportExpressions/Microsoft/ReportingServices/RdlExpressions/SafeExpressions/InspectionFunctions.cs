using System;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000017 RID: 23
	internal static class InspectionFunctions
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00002C94 File Offset: 0x00000E94
		public static void Add(FunctionFactory factory)
		{
			IFunctionEvaluator functionEvaluator = EvaluatorFactory.CreateInspectionFunctionEvaluator();
			factory.Add(typeof(bool), new FixedNumberArgumentValidator("ISARRAY", 1), functionEvaluator);
			factory.Add(typeof(bool), new FixedNumberArgumentValidator("ISDATE", 1), functionEvaluator);
			factory.Add(typeof(bool), new FixedNumberArgumentValidator("ISNOTHING", 1), functionEvaluator);
			factory.Add(typeof(bool), new FixedNumberArgumentValidator("ISNUMERIC", 1), functionEvaluator);
		}
	}
}
