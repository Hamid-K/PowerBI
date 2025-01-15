using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000AF1 RID: 2801
	internal abstract class GoogleAnalyticsAccount
	{
		// Token: 0x06004DD2 RID: 19922 RVA: 0x00102693 File Offset: 0x00100893
		public GoogleAnalyticsAccount(IGoogleAnalyticsService service, string id, string name, string propertiesUrl)
		{
			this.service = service;
			this.id = id;
			this.name = name;
			this.propertiesUrl = propertiesUrl;
		}

		// Token: 0x17001850 RID: 6224
		// (get) Token: 0x06004DD3 RID: 19923 RVA: 0x001026B8 File Offset: 0x001008B8
		public string ID
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17001851 RID: 6225
		// (get) Token: 0x06004DD4 RID: 19924 RVA: 0x001026C0 File Offset: 0x001008C0
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06004DD5 RID: 19925
		protected abstract GoogleAnalyticsProperty MakeProperty(Value record);

		// Token: 0x17001852 RID: 6226
		// (get) Token: 0x06004DD6 RID: 19926 RVA: 0x001026C8 File Offset: 0x001008C8
		public IList<GoogleAnalyticsProperty> Properties
		{
			get
			{
				if (this.properties == null)
				{
					List<Value> list = this.service.DownloadList(new UriBuilder(this.propertiesUrl));
					this.properties = new GoogleAnalyticsProperty[list.Count];
					for (int i = 0; i < list.Count; i++)
					{
						Value value = list[i];
						this.properties[i] = this.MakeProperty(value);
					}
				}
				return this.properties;
			}
		}

		// Token: 0x040029C6 RID: 10694
		protected readonly IGoogleAnalyticsService service;

		// Token: 0x040029C7 RID: 10695
		private readonly string id;

		// Token: 0x040029C8 RID: 10696
		private readonly string name;

		// Token: 0x040029C9 RID: 10697
		private readonly string propertiesUrl;

		// Token: 0x040029CA RID: 10698
		private IList<GoogleAnalyticsProperty> properties;
	}
}
