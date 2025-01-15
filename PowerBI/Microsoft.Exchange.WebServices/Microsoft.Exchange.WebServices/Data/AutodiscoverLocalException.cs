using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000256 RID: 598
	[Serializable]
	public class AutodiscoverLocalException : ServiceLocalException
	{
		// Token: 0x0600159C RID: 5532 RVA: 0x0003CC60 File Offset: 0x0003BC60
		public AutodiscoverLocalException()
		{
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x0003CC68 File Offset: 0x0003BC68
		public AutodiscoverLocalException(string message)
			: base(message)
		{
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x0003CC71 File Offset: 0x0003BC71
		public AutodiscoverLocalException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
