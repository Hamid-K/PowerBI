using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200004B RID: 75
	internal class ExternalResourceAbortHelper : IAbortHelper
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000B981 File Offset: 0x00009B81
		public bool IsAborted
		{
			get
			{
				return this.m_isAborted;
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000B989 File Offset: 0x00009B89
		public bool Abort(ProcessingStatus status)
		{
			this.m_isAborted = true;
			return true;
		}

		// Token: 0x0400021E RID: 542
		private bool m_isAborted;
	}
}
