using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000256 RID: 598
	[Serializable]
	internal abstract class FunctionAggrStandard : FunctionAggr
	{
		// Token: 0x06001387 RID: 4999 RVA: 0x0002ECB0 File Offset: 0x0002CEB0
		public FunctionAggrStandard(List<IInternalExpression> args)
			: base(args)
		{
		}
	}
}
