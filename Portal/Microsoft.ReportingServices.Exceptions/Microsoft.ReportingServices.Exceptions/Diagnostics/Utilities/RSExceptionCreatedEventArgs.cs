using System;
using System.Diagnostics;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000CC RID: 204
	public sealed class RSExceptionCreatedEventArgs : EventArgs
	{
		// Token: 0x06000423 RID: 1059 RVA: 0x000071AD File Offset: 0x000053AD
		public RSExceptionCreatedEventArgs(RSException exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this.m_e = exception;
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x000071CA File Offset: 0x000053CA
		public RSException Exception
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_e;
			}
		}

		// Token: 0x04000028 RID: 40
		private readonly RSException m_e;
	}
}
