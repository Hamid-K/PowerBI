using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000B25 RID: 2853
	internal sealed class GoogleAnalyticsPropertyV1 : GoogleAnalyticsProperty
	{
		// Token: 0x06004EFD RID: 20221 RVA: 0x00105C02 File Offset: 0x00103E02
		public GoogleAnalyticsPropertyV1(IGoogleAnalyticsService service, string accountId, string id, string name, string viewsUrl)
			: base(service, accountId, id, name)
		{
			this.viewsUrl = viewsUrl;
		}

		// Token: 0x06004EFE RID: 20222 RVA: 0x00105C17 File Offset: 0x00103E17
		public override IGoogleAnalyticsQueryCompiler CreateCompiler(GoogleAnalyticsCube cube)
		{
			return new GoogleAnalyticsQueryCompilerV1(cube);
		}

		// Token: 0x06004EFF RID: 20223 RVA: 0x00105C20 File Offset: 0x00103E20
		protected override IList<GoogleAnalyticsCube> CreateViews()
		{
			List<Value> list = this.service.DownloadList(new UriBuilder(this.viewsUrl));
			IList<GoogleAnalyticsCube> list2 = new GoogleAnalyticsCube[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				Value value = list[i];
				string asString = value["created"].AsString;
				int num = int.Parse(asString.Substring(0, 4), CultureInfo.InvariantCulture);
				int num2 = int.Parse(asString.Substring(5, 2), CultureInfo.InvariantCulture);
				int num3 = int.Parse(asString.Substring(8, 2), CultureInfo.InvariantCulture);
				list2[i] = new GoogleAnalyticsCube(this.service, this, value["id"].AsString, value["name"].AsString, new DateTime(num, num2, num3), new Func<GoogleAnalyticsCube, GoogleAnalyticsQueryExpression, CubeExpression, Keys, IEnumerator<IValueReference>>(GoogleAnalyticsCubeContextProvider.CreateV1ResultEnumerator));
			}
			return list2;
		}

		// Token: 0x04002A85 RID: 10885
		private readonly string viewsUrl;
	}
}
