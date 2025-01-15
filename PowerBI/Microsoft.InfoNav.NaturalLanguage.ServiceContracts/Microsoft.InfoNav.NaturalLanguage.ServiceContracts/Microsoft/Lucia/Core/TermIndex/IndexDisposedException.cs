using System;

namespace Microsoft.Lucia.Core.TermIndex
{
	// Token: 0x02000169 RID: 361
	public sealed class IndexDisposedException : ObjectDisposedException
	{
		// Token: 0x06000712 RID: 1810 RVA: 0x0000C296 File Offset: 0x0000A496
		public IndexDisposedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
