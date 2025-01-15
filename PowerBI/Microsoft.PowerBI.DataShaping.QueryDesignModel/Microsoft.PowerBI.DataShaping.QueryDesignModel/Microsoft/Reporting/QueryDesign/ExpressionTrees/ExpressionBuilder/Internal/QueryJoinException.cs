using System;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal
{
	// Token: 0x02000156 RID: 342
	internal sealed class QueryJoinException : ArgumentException
	{
		// Token: 0x06001374 RID: 4980 RVA: 0x00037B1A File Offset: 0x00035D1A
		internal QueryJoinException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
