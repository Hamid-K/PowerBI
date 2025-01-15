using System;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000B2B RID: 2859
	internal class GoogleAnalyticsServerUrl : IGoogleAnalyticsServerUrl
	{
		// Token: 0x170018C2 RID: 6338
		// (get) Token: 0x06004F51 RID: 20305 RVA: 0x00109173 File Offset: 0x00107373
		public Uri V1ServerBaseUrl
		{
			get
			{
				return GoogleAnalyticsServerUrl.v1Uri;
			}
		}

		// Token: 0x170018C3 RID: 6339
		// (get) Token: 0x06004F52 RID: 20306 RVA: 0x0010917A File Offset: 0x0010737A
		public Uri V2AdminServerBaseUrl
		{
			get
			{
				return GoogleAnalyticsServerUrl.v2AdminUri;
			}
		}

		// Token: 0x170018C4 RID: 6340
		// (get) Token: 0x06004F53 RID: 20307 RVA: 0x00109181 File Offset: 0x00107381
		public Uri V2DataServerBaseUrl
		{
			get
			{
				return GoogleAnalyticsServerUrl.v2DataUri;
			}
		}

		// Token: 0x04002A9B RID: 10907
		private static readonly Uri v1Uri = new Uri("https://analyticsreporting.googleapis.com");

		// Token: 0x04002A9C RID: 10908
		private static readonly Uri v2AdminUri = new Uri("https://analyticsadmin.googleapis.com");

		// Token: 0x04002A9D RID: 10909
		private static readonly Uri v2DataUri = new Uri("https://analyticsdata.googleapis.com");
	}
}
