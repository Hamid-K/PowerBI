using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.External
{
	// Token: 0x020000BF RID: 191
	[GeneratedCode("gbc", "0.4.1.0")]
	internal class RemoteDependencyData : Domain, ISerializableWithWriter
	{
		// Token: 0x0600062A RID: 1578 RVA: 0x000171EC File Offset: 0x000153EC
		public void Serialize(ISerializationWriter serializationWriter)
		{
			serializationWriter.WriteProperty("ver", new int?(this.ver));
			serializationWriter.WriteProperty("name", this.name);
			serializationWriter.WriteProperty("id", this.id);
			serializationWriter.WriteProperty("data", this.data);
			serializationWriter.WriteProperty("duration", new TimeSpan?(this.duration));
			serializationWriter.WriteProperty("resultCode", this.resultCode);
			serializationWriter.WriteProperty("success", new bool?(this.success));
			serializationWriter.WriteProperty("type", this.type);
			serializationWriter.WriteProperty("target", this.target);
			serializationWriter.WriteProperty("properties", this.properties);
			serializationWriter.WriteProperty("measurements", this.measurements);
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x000172C3 File Offset: 0x000154C3
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x000172CB File Offset: 0x000154CB
		public int ver { get; set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x000172D4 File Offset: 0x000154D4
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x000172DC File Offset: 0x000154DC
		public string name { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x000172E5 File Offset: 0x000154E5
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x000172ED File Offset: 0x000154ED
		public string id { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x000172F6 File Offset: 0x000154F6
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x000172FE File Offset: 0x000154FE
		public string resultCode { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00017307 File Offset: 0x00015507
		// (set) Token: 0x06000634 RID: 1588 RVA: 0x0001730F File Offset: 0x0001550F
		public TimeSpan duration { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x00017318 File Offset: 0x00015518
		// (set) Token: 0x06000636 RID: 1590 RVA: 0x00017320 File Offset: 0x00015520
		public bool success { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x00017329 File Offset: 0x00015529
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x00017331 File Offset: 0x00015531
		public string data { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0001733A File Offset: 0x0001553A
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x00017342 File Offset: 0x00015542
		public string target { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0001734B File Offset: 0x0001554B
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x00017353 File Offset: 0x00015553
		public string type { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0001735C File Offset: 0x0001555C
		// (set) Token: 0x0600063E RID: 1598 RVA: 0x00017364 File Offset: 0x00015564
		public IDictionary<string, string> properties { get; set; }

		// Token: 0x0600063F RID: 1599 RVA: 0x0001736D File Offset: 0x0001556D
		public RemoteDependencyData()
			: this("AI.RemoteDependencyData", "RemoteDependencyData")
		{
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00017380 File Offset: 0x00015580
		protected RemoteDependencyData(string fullName, string name)
		{
			this.ver = 2;
			this.name = "";
			this.id = "";
			this.resultCode = "";
			this.duration = TimeSpan.Zero;
			this.success = true;
			this.target = "";
			this.type = "";
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x000173E3 File Offset: 0x000155E3
		// (set) Token: 0x06000642 RID: 1602 RVA: 0x000173EB File Offset: 0x000155EB
		public IDictionary<string, double> measurements { get; set; }
	}
}
