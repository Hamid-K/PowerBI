using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces
{
	// Token: 0x02000004 RID: 4
	internal interface IFunctionEvaluator
	{
		// Token: 0x06000003 RID: 3
		object Evaluate(string functionname, List<object> arguments);
	}
}
