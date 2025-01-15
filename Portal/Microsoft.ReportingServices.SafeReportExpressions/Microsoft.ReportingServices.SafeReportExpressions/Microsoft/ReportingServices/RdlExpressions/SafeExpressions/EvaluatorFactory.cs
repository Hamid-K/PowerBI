using System;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions.VisualBasic;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x0200000B RID: 11
	internal static class EvaluatorFactory
	{
		// Token: 0x06000027 RID: 39 RVA: 0x000023D2 File Offset: 0x000005D2
		public static IBinaryExpressionEvaluator CreateBinaryExpressionEvaluator()
		{
			return new BinaryExpressionEvaluator();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000023D9 File Offset: 0x000005D9
		public static IUnaryExpressionEvaluator CreateUnaryExpressionEvaluator()
		{
			return new UnaryExpressionEvaluator();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000023E0 File Offset: 0x000005E0
		public static IFunctionEvaluator CreateExecutionFlowFunctionEvaluator()
		{
			return new ExecutionFlowFunctionEvaluator();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000023E7 File Offset: 0x000005E7
		public static IFunctionEvaluator CreateStringFunctionEvaluator()
		{
			return new StringFunctionEvaluator();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000023EE File Offset: 0x000005EE
		public static IFunctionEvaluator CreateInspectionFunctionEvaluator()
		{
			return new InspectionFunctionEvaluator();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000023F5 File Offset: 0x000005F5
		public static IVisualBasicConstantEvaluator CreateVbConstantIdentifierEvaluator()
		{
			return new VbConstantIdentifierEvaluator();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000023FC File Offset: 0x000005FC
		public static IConversionEvaluator CreateConversionEvaluator()
		{
			return new ConversionEvaluator();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002403 File Offset: 0x00000603
		public static IFunctionEvaluator CreateDateTimeFunctionEvaluator()
		{
			return new DateTimeFunctionEvaluator();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000240A File Offset: 0x0000060A
		public static IFunctionEvaluator CreateMathFunctionEvaluator()
		{
			return new MathFunctionEvaluator();
		}
	}
}
