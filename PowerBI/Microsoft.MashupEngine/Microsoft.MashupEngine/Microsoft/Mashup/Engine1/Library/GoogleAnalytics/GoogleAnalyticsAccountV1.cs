using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000AF2 RID: 2802
	internal class GoogleAnalyticsAccountV1 : GoogleAnalyticsAccount
	{
		// Token: 0x06004DD7 RID: 19927 RVA: 0x00102737 File Offset: 0x00100937
		public GoogleAnalyticsAccountV1(IGoogleAnalyticsService service, string id, string name, string propertiesUrl)
			: base(service, id, name, propertiesUrl)
		{
		}

		// Token: 0x06004DD8 RID: 19928 RVA: 0x00102744 File Offset: 0x00100944
		protected override GoogleAnalyticsProperty MakeProperty(Value record)
		{
			return new GoogleAnalyticsPropertyV1(this.service, record["accountId"].AsString, record["id"].AsString, record["name"].AsString, record["childLink"]["href"].AsString);
		}
	}
}
