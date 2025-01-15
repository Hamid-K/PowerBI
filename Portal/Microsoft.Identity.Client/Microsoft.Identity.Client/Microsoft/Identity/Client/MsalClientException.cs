using System;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200016B RID: 363
	public class MsalClientException : MsalException
	{
		// Token: 0x06001203 RID: 4611 RVA: 0x0003DB2E File Offset: 0x0003BD2E
		public MsalClientException(string errorCode)
			: base(errorCode)
		{
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x0003DB37 File Offset: 0x0003BD37
		public MsalClientException(string errorCode, string errorMessage)
			: base(errorCode, errorMessage)
		{
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0003DB41 File Offset: 0x0003BD41
		public MsalClientException(string errorCode, string errorMessage, Exception innerException)
			: base(errorCode, errorMessage, innerException)
		{
		}
	}
}
