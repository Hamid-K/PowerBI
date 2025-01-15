using System;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BC5 RID: 3013
	internal class ExchangeUserSettings
	{
		// Token: 0x06005230 RID: 21040 RVA: 0x001158C4 File Offset: 0x00113AC4
		public ExchangeUserSettings(string ewsUrl, ExchangeVersion exchangeVersion)
		{
			this.ewsUrl = ewsUrl;
			this.exchangeVersion = exchangeVersion;
		}

		// Token: 0x17001961 RID: 6497
		// (get) Token: 0x06005231 RID: 21041 RVA: 0x001158DA File Offset: 0x00113ADA
		public string EwsUrl
		{
			get
			{
				return this.ewsUrl;
			}
		}

		// Token: 0x17001962 RID: 6498
		// (get) Token: 0x06005232 RID: 21042 RVA: 0x001158E2 File Offset: 0x00113AE2
		public ExchangeVersion ExchangeVersion
		{
			get
			{
				return this.exchangeVersion;
			}
		}

		// Token: 0x04002D33 RID: 11571
		private readonly string ewsUrl;

		// Token: 0x04002D34 RID: 11572
		private readonly ExchangeVersion exchangeVersion;
	}
}
