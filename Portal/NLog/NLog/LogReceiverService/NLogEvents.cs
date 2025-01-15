using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace NLog.LogReceiverService
{
	// Token: 0x02000098 RID: 152
	[DataContract(Name = "events", Namespace = "http://nlog-project.org/ws/")]
	[XmlType(Namespace = "http://nlog-project.org/ws/")]
	[XmlRoot("events", Namespace = "http://nlog-project.org/ws/")]
	[DebuggerDisplay("Count = {Events.Length}")]
	public class NLogEvents
	{
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x0001A4D6 File Offset: 0x000186D6
		// (set) Token: 0x060009E6 RID: 2534 RVA: 0x0001A4DE File Offset: 0x000186DE
		[DataMember(Name = "cli", Order = 0)]
		[XmlElement("cli", Order = 0)]
		public string ClientName { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x0001A4E7 File Offset: 0x000186E7
		// (set) Token: 0x060009E8 RID: 2536 RVA: 0x0001A4EF File Offset: 0x000186EF
		[DataMember(Name = "bts", Order = 1)]
		[XmlElement("bts", Order = 1)]
		public long BaseTimeUtc { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x0001A4F8 File Offset: 0x000186F8
		// (set) Token: 0x060009EA RID: 2538 RVA: 0x0001A500 File Offset: 0x00018700
		[DataMember(Name = "lts", Order = 100)]
		[XmlArray("lts", Order = 100)]
		[XmlArrayItem("l")]
		public StringCollection LayoutNames { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x0001A509 File Offset: 0x00018709
		// (set) Token: 0x060009EC RID: 2540 RVA: 0x0001A511 File Offset: 0x00018711
		[DataMember(Name = "str", Order = 200)]
		[XmlArray("str", Order = 200)]
		[XmlArrayItem("l")]
		public StringCollection Strings { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x0001A51A File Offset: 0x0001871A
		// (set) Token: 0x060009EE RID: 2542 RVA: 0x0001A522 File Offset: 0x00018722
		[DataMember(Name = "ev", Order = 1000)]
		[XmlArray("ev", Order = 1000)]
		[XmlArrayItem("e")]
		public NLogEvent[] Events { get; set; }

		// Token: 0x060009EF RID: 2543 RVA: 0x0001A52C File Offset: 0x0001872C
		public IList<LogEventInfo> ToEventInfo(string loggerNamePrefix)
		{
			LogEventInfo[] array = new LogEventInfo[this.Events.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.Events[i].ToEventInfo(this, loggerNamePrefix);
			}
			return array;
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0001A568 File Offset: 0x00018768
		public IList<LogEventInfo> ToEventInfo()
		{
			return this.ToEventInfo(string.Empty);
		}
	}
}
