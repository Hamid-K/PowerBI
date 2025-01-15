using System;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000004 RID: 4
	internal sealed class SyntaxErrorException : Exception
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
		public SyntaxErrorException(string message)
			: base(message)
		{
		}
	}
}
