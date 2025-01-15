using System;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal
{
	// Token: 0x02000155 RID: 341
	internal sealed class QueryFunctionInvocationException : ArgumentException
	{
		// Token: 0x06001371 RID: 4977 RVA: 0x00037AF9 File Offset: 0x00035CF9
		internal QueryFunctionInvocationException(string message)
			: base(message)
		{
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00037B02 File Offset: 0x00035D02
		internal QueryFunctionInvocationException(Exception innerException)
			: base("The query function invocation failed.", innerException)
		{
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00037B10 File Offset: 0x00035D10
		internal QueryFunctionInvocationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
