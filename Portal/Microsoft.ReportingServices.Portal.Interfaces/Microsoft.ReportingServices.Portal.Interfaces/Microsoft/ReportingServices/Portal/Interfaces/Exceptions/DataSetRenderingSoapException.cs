using System;

namespace Microsoft.ReportingServices.Portal.Interfaces.Exceptions
{
	// Token: 0x020000AC RID: 172
	public sealed class DataSetRenderingSoapException : Exception
	{
		// Token: 0x06000572 RID: 1394 RVA: 0x00004B5A File Offset: 0x00002D5A
		public DataSetRenderingSoapException(string message)
			: base(message)
		{
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00004B63 File Offset: 0x00002D63
		public DataSetRenderingSoapException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
