using System;
using System.CodeDom.Compiler;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.External
{
	// Token: 0x020000BE RID: 190
	[GeneratedCode("gbc", "0.4.1.0")]
	internal class PageViewPerfData : PageViewData, ISerializableWithWriter
	{
		// Token: 0x0600061B RID: 1563 RVA: 0x00016FC4 File Offset: 0x000151C4
		public new PageViewPerfData DeepClone()
		{
			PageViewPerfData pageViewPerfData = new PageViewPerfData();
			this.ApplyProperties(pageViewPerfData);
			return pageViewPerfData;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00016FE0 File Offset: 0x000151E0
		protected override void ApplyProperties(EventData other)
		{
			base.ApplyProperties(other);
			PageViewPerfData pageViewPerfData = other as PageViewPerfData;
			if (pageViewPerfData != null)
			{
				pageViewPerfData.domProcessing = this.domProcessing;
				pageViewPerfData.perfTotal = this.perfTotal;
				pageViewPerfData.networkConnect = this.networkConnect;
				pageViewPerfData.sentRequest = this.sentRequest;
				pageViewPerfData.receivedResponse = this.receivedResponse;
			}
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001703C File Offset: 0x0001523C
		public new void Serialize(ISerializationWriter serializationWriter)
		{
			serializationWriter.WriteProperty("ver", new int?(base.ver));
			serializationWriter.WriteProperty("name", base.name);
			serializationWriter.WriteProperty("url", base.url);
			serializationWriter.WriteProperty("duration", new TimeSpan?(Utils.ValidateDuration(base.duration)));
			serializationWriter.WriteProperty("domProcessing", new TimeSpan?(Utils.ValidateDuration(this.domProcessing)));
			serializationWriter.WriteProperty("perfTotal", new TimeSpan?(Utils.ValidateDuration(this.perfTotal)));
			serializationWriter.WriteProperty("networkConnect", new TimeSpan?(Utils.ValidateDuration(this.networkConnect)));
			serializationWriter.WriteProperty("sentRequest", new TimeSpan?(Utils.ValidateDuration(this.sentRequest)));
			serializationWriter.WriteProperty("receivedResponse", new TimeSpan?(Utils.ValidateDuration(this.receivedResponse)));
			serializationWriter.WriteProperty("properties", base.properties);
			serializationWriter.WriteProperty("measurements", base.measurements);
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00017145 File Offset: 0x00015345
		// (set) Token: 0x0600061F RID: 1567 RVA: 0x0001714D File Offset: 0x0001534D
		public string perfTotal { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x00017156 File Offset: 0x00015356
		// (set) Token: 0x06000621 RID: 1569 RVA: 0x0001715E File Offset: 0x0001535E
		public string networkConnect { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00017167 File Offset: 0x00015367
		// (set) Token: 0x06000623 RID: 1571 RVA: 0x0001716F File Offset: 0x0001536F
		public string sentRequest { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x00017178 File Offset: 0x00015378
		// (set) Token: 0x06000625 RID: 1573 RVA: 0x00017180 File Offset: 0x00015380
		public string receivedResponse { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00017189 File Offset: 0x00015389
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x00017191 File Offset: 0x00015391
		public string domProcessing { get; set; }

		// Token: 0x06000628 RID: 1576 RVA: 0x0001719A File Offset: 0x0001539A
		public PageViewPerfData()
			: this("AI.PageViewPerfData", "PageViewPerfData")
		{
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x000171AC File Offset: 0x000153AC
		protected PageViewPerfData(string fullName, string name)
		{
			this.perfTotal = "";
			this.networkConnect = "";
			this.sentRequest = "";
			this.receivedResponse = "";
			this.domProcessing = "";
		}
	}
}
