using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace NLog.LogReceiverService
{
	// Token: 0x02000097 RID: 151
	[DataContract(Name = "e", Namespace = "http://nlog-project.org/ws/")]
	[XmlType(Namespace = "http://nlog-project.org/ws/")]
	[DebuggerDisplay("Event ID = {Id} Level={LevelName} Values={Values.Count}")]
	public class NLogEvent
	{
		// Token: 0x060009D5 RID: 2517 RVA: 0x0001A2C2 File Offset: 0x000184C2
		public NLogEvent()
		{
			this.ValueIndexes = new List<int>();
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x0001A2D5 File Offset: 0x000184D5
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x0001A2DD File Offset: 0x000184DD
		[DataMember(Name = "id", Order = 0)]
		[XmlElement("id", Order = 0)]
		public int Id { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x0001A2E6 File Offset: 0x000184E6
		// (set) Token: 0x060009D9 RID: 2521 RVA: 0x0001A2EE File Offset: 0x000184EE
		[DataMember(Name = "lv", Order = 1)]
		[XmlElement("lv", Order = 1)]
		public int LevelOrdinal { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x0001A2F7 File Offset: 0x000184F7
		// (set) Token: 0x060009DB RID: 2523 RVA: 0x0001A2FF File Offset: 0x000184FF
		[DataMember(Name = "lg", Order = 2)]
		[XmlElement("lg", Order = 2)]
		public int LoggerOrdinal { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x0001A308 File Offset: 0x00018508
		// (set) Token: 0x060009DD RID: 2525 RVA: 0x0001A310 File Offset: 0x00018510
		[DataMember(Name = "ts", Order = 3)]
		[XmlElement("ts", Order = 3)]
		public long TimeDelta { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x0001A319 File Offset: 0x00018519
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x0001A321 File Offset: 0x00018521
		[DataMember(Name = "m", Order = 4)]
		[XmlElement("m", Order = 4)]
		public int MessageOrdinal { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0001A32C File Offset: 0x0001852C
		// (set) Token: 0x060009E1 RID: 2529 RVA: 0x0001A3A4 File Offset: 0x000185A4
		[DataMember(Name = "val", Order = 100)]
		[XmlElement("val", Order = 100)]
		public string Values
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				string text = string.Empty;
				if (this.ValueIndexes != null)
				{
					foreach (int num in this.ValueIndexes)
					{
						stringBuilder.Append(text);
						stringBuilder.Append(num);
						text = "|";
					}
				}
				return stringBuilder.ToString();
			}
			set
			{
				if (this.ValueIndexes != null)
				{
					this.ValueIndexes.Clear();
				}
				else
				{
					this.ValueIndexes = new List<int>();
				}
				if (!string.IsNullOrEmpty(value))
				{
					foreach (string text in value.Split(new char[] { '|' }))
					{
						this.ValueIndexes.Add(Convert.ToInt32(text, CultureInfo.InvariantCulture));
					}
				}
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x0001A413 File Offset: 0x00018613
		// (set) Token: 0x060009E3 RID: 2531 RVA: 0x0001A41B File Offset: 0x0001861B
		[IgnoreDataMember]
		[XmlIgnore]
		internal IList<int> ValueIndexes { get; private set; }

		// Token: 0x060009E4 RID: 2532 RVA: 0x0001A424 File Offset: 0x00018624
		internal LogEventInfo ToEventInfo(NLogEvents context, string loggerNamePrefix)
		{
			LogEventInfo logEventInfo = new LogEventInfo(LogLevel.FromOrdinal(this.LevelOrdinal), loggerNamePrefix + context.Strings[this.LoggerOrdinal], context.Strings[this.MessageOrdinal]);
			logEventInfo.TimeStamp = new DateTime(context.BaseTimeUtc + this.TimeDelta, DateTimeKind.Utc).ToLocalTime();
			for (int i = 0; i < context.LayoutNames.Count; i++)
			{
				string text = context.LayoutNames[i];
				string text2 = context.Strings[this.ValueIndexes[i]];
				logEventInfo.Properties[text] = text2;
			}
			return logEventInfo;
		}
	}
}
