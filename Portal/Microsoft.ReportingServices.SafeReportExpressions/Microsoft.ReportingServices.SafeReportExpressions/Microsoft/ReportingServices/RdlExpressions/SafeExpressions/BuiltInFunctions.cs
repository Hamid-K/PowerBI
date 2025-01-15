using System;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000014 RID: 20
	internal static class BuiltInFunctions
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00002A10 File Offset: 0x00000C10
		public static void Add(FunctionFactory factory, ISafeExpressionsReportContext safeExpressionsReportContext)
		{
			factory.Add(null, new NoValidationArgumentValidator("AVG"), null);
			factory.Add(null, new NoValidationArgumentValidator("COUNT"), null);
			factory.Add(null, new NoValidationArgumentValidator("COUNTDISTINCT"), null);
			factory.Add(null, new NoValidationArgumentValidator("COUNTROWS"), null);
			factory.Add(null, new NoValidationArgumentValidator("FIRST"), null);
			factory.Add(null, new NoValidationArgumentValidator("LAST"), null);
			factory.Add(null, new NoValidationArgumentValidator("MAX"), null);
			factory.Add(null, new NoValidationArgumentValidator("MIN"), null);
			factory.Add(null, new NoValidationArgumentValidator("STDEV"), null);
			factory.Add(null, new NoValidationArgumentValidator("STDEVP"), null);
			factory.Add(null, new NoValidationArgumentValidator("SUM"), null);
			factory.Add(null, new NoValidationArgumentValidator("VAR"), null);
			factory.Add(null, new NoValidationArgumentValidator("VARP"), null);
			factory.Add(null, new NoValidationArgumentValidator("RUNNINGVALUE"), null);
			factory.Add(null, new NoValidationArgumentValidator("AGGREGATE"), null);
			factory.Add(null, new NoValidationArgumentValidator("LOOKUP"), null);
			factory.Add(null, new NoValidationArgumentValidator("LOOKUPSET"), null);
			factory.Add(null, new NoValidationArgumentValidator("MULTILOOKUP"), null);
			factory.Add(null, new NoValidationArgumentValidator("PREVIOUS"), null);
			factory.Add(null, new NoValidationArgumentValidator("ROWNUMBER"), null);
			BuiltInFunctionEvaluator builtInFunctionEvaluator = new BuiltInFunctionEvaluator(safeExpressionsReportContext);
			factory.Add(typeof(bool), new FixedNumberArgumentValidator("INSCOPE", 1), builtInFunctionEvaluator);
			factory.Add(typeof(int), new VariableNumberArgumentValidator("LEVEL", 0, 1), builtInFunctionEvaluator);
		}
	}
}
