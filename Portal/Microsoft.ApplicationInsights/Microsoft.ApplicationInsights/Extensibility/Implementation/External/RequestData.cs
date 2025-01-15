using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.External
{
	// Token: 0x020000C0 RID: 192
	[GeneratedCode("gbc", "0.4.1.0")]
	internal class RequestData : Domain, ISerializableWithWriter
	{
		// Token: 0x06000643 RID: 1603 RVA: 0x000173F4 File Offset: 0x000155F4
		public void Serialize(ISerializationWriter serializationWriter)
		{
			serializationWriter.WriteProperty("ver", new int?(this.ver));
			serializationWriter.WriteProperty("id", this.id);
			serializationWriter.WriteProperty("source", this.source);
			serializationWriter.WriteProperty("name", this.name);
			serializationWriter.WriteProperty("duration", new TimeSpan?(this.duration));
			serializationWriter.WriteProperty("success", new bool?(this.success));
			serializationWriter.WriteProperty("responseCode", this.responseCode);
			serializationWriter.WriteProperty("url", this.url);
			serializationWriter.WriteProperty("properties", this.properties);
			serializationWriter.WriteProperty("measurements", this.measurements);
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x000174BA File Offset: 0x000156BA
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x000174C2 File Offset: 0x000156C2
		public int ver { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x000174CB File Offset: 0x000156CB
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x000174D3 File Offset: 0x000156D3
		public string id { get; set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x000174DC File Offset: 0x000156DC
		// (set) Token: 0x06000649 RID: 1609 RVA: 0x000174E4 File Offset: 0x000156E4
		public string source { get; set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x000174ED File Offset: 0x000156ED
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x000174F5 File Offset: 0x000156F5
		public string name { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x000174FE File Offset: 0x000156FE
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x00017506 File Offset: 0x00015706
		public TimeSpan duration { get; set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x0001750F File Offset: 0x0001570F
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x00017517 File Offset: 0x00015717
		public string responseCode { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x00017520 File Offset: 0x00015720
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x00017528 File Offset: 0x00015728
		public bool success { get; set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x00017531 File Offset: 0x00015731
		// (set) Token: 0x06000653 RID: 1619 RVA: 0x00017539 File Offset: 0x00015739
		public string url { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x00017542 File Offset: 0x00015742
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x0001754A File Offset: 0x0001574A
		public IDictionary<string, string> properties { get; set; }

		// Token: 0x06000656 RID: 1622 RVA: 0x00017553 File Offset: 0x00015753
		public RequestData()
			: this("AI.RequestData", "RequestData")
		{
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00017568 File Offset: 0x00015768
		protected RequestData(string fullName, string name)
		{
			this.ver = 2;
			this.id = "";
			this.source = "";
			this.name = "";
			this.duration = TimeSpan.Zero;
			this.responseCode = "";
			this.success = true;
			this.url = "";
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x000175CB File Offset: 0x000157CB
		// (set) Token: 0x06000659 RID: 1625 RVA: 0x000175D3 File Offset: 0x000157D3
		public IDictionary<string, double> measurements { get; set; }
	}
}
