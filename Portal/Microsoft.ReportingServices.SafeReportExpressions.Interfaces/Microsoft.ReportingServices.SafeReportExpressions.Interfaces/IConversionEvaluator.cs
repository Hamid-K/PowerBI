using System;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces
{
	// Token: 0x02000003 RID: 3
	internal interface IConversionEvaluator
	{
		// Token: 0x06000002 RID: 2
		object Evaluate(string conversionName, object conversionValue);
	}
}
