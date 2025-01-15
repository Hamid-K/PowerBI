using System;
using System.Diagnostics;

namespace Microsoft.Identity.Extensions
{
	// Token: 0x02000004 RID: 4
	[DebuggerDisplay("{DebuggerDisplay}")]
	internal class InteropException : Exception
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002067 File Offset: 0x00000267
		public InteropException()
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000206F File Offset: 0x0000026F
		public InteropException(string message, int errorCode)
			: base(message + " .Error code: " + errorCode.ToString())
		{
			this.ErrorCode = errorCode;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002090 File Offset: 0x00000290
		public InteropException(string message, int errorCode, Exception innerException)
			: base(message + ". Error code: " + errorCode.ToString(), innerException)
		{
			this.ErrorCode = errorCode;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020B2 File Offset: 0x000002B2
		public int ErrorCode { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020BA File Offset: 0x000002BA
		private string DebuggerDisplay
		{
			get
			{
				return string.Format("{0} [0x{1:x}]", this.Message, this.ErrorCode);
			}
		}
	}
}
