using System;
using System.CodeDom.Compiler;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.External
{
	// Token: 0x020000BD RID: 189
	[GeneratedCode("gbc", "0.4.1.0")]
	internal class PageViewData : EventData, ISerializableWithWriter
	{
		// Token: 0x06000610 RID: 1552 RVA: 0x00016E74 File Offset: 0x00015074
		public new PageViewData DeepClone()
		{
			PageViewData pageViewData = new PageViewData();
			this.ApplyProperties(pageViewData);
			return pageViewData;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00016E90 File Offset: 0x00015090
		protected override void ApplyProperties(EventData other)
		{
			base.ApplyProperties(other);
			PageViewData pageViewData = other as PageViewData;
			if (pageViewData != null)
			{
				pageViewData.url = this.url;
				pageViewData.duration = this.duration;
				pageViewData.id = this.id;
			}
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00016ED4 File Offset: 0x000150D4
		public new void Serialize(ISerializationWriter serializationWriter)
		{
			serializationWriter.WriteProperty("ver", new int?(base.ver));
			serializationWriter.WriteProperty("name", base.name);
			serializationWriter.WriteProperty("url", this.url);
			serializationWriter.WriteProperty("duration", new TimeSpan?(Utils.ValidateDuration(this.duration)));
			serializationWriter.WriteProperty("properties", base.properties);
			serializationWriter.WriteProperty("measurements", base.measurements);
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x00016F56 File Offset: 0x00015156
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x00016F5E File Offset: 0x0001515E
		public string url { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00016F67 File Offset: 0x00015167
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x00016F6F File Offset: 0x0001516F
		public string duration { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x00016F78 File Offset: 0x00015178
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x00016F80 File Offset: 0x00015180
		public string id { get; set; }

		// Token: 0x06000619 RID: 1561 RVA: 0x00016F89 File Offset: 0x00015189
		public PageViewData()
			: this("AI.PageViewData", "PageViewData")
		{
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00016F9B File Offset: 0x0001519B
		protected PageViewData(string fullName, string name)
		{
			this.url = "";
			this.duration = "";
			this.id = "";
		}
	}
}
