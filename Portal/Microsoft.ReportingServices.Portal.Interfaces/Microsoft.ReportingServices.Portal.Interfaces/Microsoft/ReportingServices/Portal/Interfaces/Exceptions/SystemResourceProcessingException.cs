using System;

namespace Microsoft.ReportingServices.Portal.Interfaces.Exceptions
{
	// Token: 0x020000B5 RID: 181
	public sealed class SystemResourceProcessingException : Exception
	{
		// Token: 0x06000585 RID: 1413 RVA: 0x00004B5A File Offset: 0x00002D5A
		public SystemResourceProcessingException(string message)
			: base(message)
		{
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00004B63 File Offset: 0x00002D63
		public SystemResourceProcessingException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
