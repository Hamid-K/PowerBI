using System;

namespace Microsoft.Reporting.Packaging.Internal
{
	// Token: 0x02000004 RID: 4
	public sealed class ReportArchiveException : Exception
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x0000619A File Offset: 0x0000439A
		internal ReportArchiveException(string message)
			: base(message)
		{
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000061A3 File Offset: 0x000043A3
		internal ReportArchiveException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
