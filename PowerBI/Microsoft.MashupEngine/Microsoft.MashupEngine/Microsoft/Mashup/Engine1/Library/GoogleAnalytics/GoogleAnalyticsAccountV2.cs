using System;
using System.Globalization;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000AF3 RID: 2803
	internal class GoogleAnalyticsAccountV2 : GoogleAnalyticsAccount
	{
		// Token: 0x06004DD9 RID: 19929 RVA: 0x00102737 File Offset: 0x00100937
		public GoogleAnalyticsAccountV2(IGoogleAnalyticsService service, string id, string name, string propertiesUrl)
			: base(service, id, name, propertiesUrl)
		{
		}

		// Token: 0x06004DDA RID: 19930 RVA: 0x001027A8 File Offset: 0x001009A8
		protected override GoogleAnalyticsProperty MakeProperty(Value record)
		{
			string asString = record["createTime"].AsString;
			int num = int.Parse(asString.Substring(0, 4), CultureInfo.InvariantCulture);
			int num2 = int.Parse(asString.Substring(5, 2), CultureInfo.InvariantCulture);
			int num3 = int.Parse(asString.Substring(8, 2), CultureInfo.InvariantCulture);
			string asString2 = record["name"].AsString;
			return new GoogleAnalyticsPropertyV2(this.service, record["account"].AsString, asString2, record["displayName"].AsString, new DateTime(num, num2, num3));
		}
	}
}
