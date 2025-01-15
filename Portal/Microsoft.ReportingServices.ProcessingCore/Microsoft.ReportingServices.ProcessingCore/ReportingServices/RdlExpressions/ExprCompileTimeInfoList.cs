using System;
using System.Collections;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x02000561 RID: 1377
	internal sealed class ExprCompileTimeInfoList : ArrayList
	{
		// Token: 0x17001DF3 RID: 7667
		public ExprCompileTimeInfo this[int exprCTId]
		{
			get
			{
				return (ExprCompileTimeInfo)base[exprCTId];
			}
		}
	}
}
