using System;

namespace Microsoft.ReportingServices.Hybrid.OAuth
{
	// Token: 0x02000008 RID: 8
	internal class AadCommunicationException : Exception
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002AAA File Offset: 0x00000CAA
		public AadCommunicationException(string msg, Exception innerException = null)
			: base(msg, innerException)
		{
		}
	}
}
