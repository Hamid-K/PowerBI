using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000261 RID: 609
	[Serializable]
	public class ServerBusyException : ServiceResponseException
	{
		// Token: 0x060015B8 RID: 5560 RVA: 0x0003CE80 File Offset: 0x0003BE80
		public ServerBusyException(ServiceResponse response)
			: base(response)
		{
			if (response.ErrorDetails != null && response.ErrorDetails.ContainsKey("BackOffMilliseconds"))
			{
				int.TryParse(response.ErrorDetails["BackOffMilliseconds"], ref this.backOffMilliseconds);
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x0003CEBF File Offset: 0x0003BEBF
		public int BackOffMilliseconds
		{
			get
			{
				return this.backOffMilliseconds;
			}
		}

		// Token: 0x040012B8 RID: 4792
		private const string BackOffMillisecondsKey = "BackOffMilliseconds";

		// Token: 0x040012B9 RID: 4793
		private int backOffMilliseconds;
	}
}
