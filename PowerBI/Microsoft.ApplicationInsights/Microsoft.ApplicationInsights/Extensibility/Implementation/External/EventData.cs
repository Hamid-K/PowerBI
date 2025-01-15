using System;
using System.CodeDom.Compiler;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.External
{
	// Token: 0x020000B8 RID: 184
	[GeneratedCode("gbc", "0.4.1.0")]
	internal class EventData : Domain, ISerializableWithWriter
	{
		// Token: 0x060005C9 RID: 1481 RVA: 0x0001669C File Offset: 0x0001489C
		public EventData DeepClone()
		{
			EventData eventData = new EventData();
			this.ApplyProperties(eventData);
			return eventData;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x000166B7 File Offset: 0x000148B7
		protected virtual void ApplyProperties(EventData other)
		{
			other.ver = this.ver;
			other.name = this.name;
			Utils.CopyDictionary<string>(this.properties, other.properties);
			Utils.CopyDictionary<double>(this.measurements, other.measurements);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x000166F4 File Offset: 0x000148F4
		public void Serialize(ISerializationWriter serializationWriter)
		{
			serializationWriter.WriteProperty("ver", new int?(this.ver));
			serializationWriter.WriteProperty("name", this.name);
			serializationWriter.WriteProperty("properties", this.properties);
			serializationWriter.WriteProperty("measurements", this.measurements);
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x0001674A File Offset: 0x0001494A
		// (set) Token: 0x060005CD RID: 1485 RVA: 0x00016752 File Offset: 0x00014952
		public int ver { get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x0001675B File Offset: 0x0001495B
		// (set) Token: 0x060005CF RID: 1487 RVA: 0x00016763 File Offset: 0x00014963
		public string name { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0001676C File Offset: 0x0001496C
		// (set) Token: 0x060005D1 RID: 1489 RVA: 0x00016774 File Offset: 0x00014974
		public IDictionary<string, string> properties { get; set; }

		// Token: 0x060005D2 RID: 1490 RVA: 0x0001677D File Offset: 0x0001497D
		public EventData()
			: this("AI.EventData", "EventData")
		{
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0001678F File Offset: 0x0001498F
		protected EventData(string fullName, string name)
		{
			this.ver = 2;
			this.name = "";
			this.properties = new ConcurrentDictionary<string, string>();
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x000167B4 File Offset: 0x000149B4
		// (set) Token: 0x060005D5 RID: 1493 RVA: 0x000167E0 File Offset: 0x000149E0
		public IDictionary<string, double> measurements
		{
			get
			{
				return LazyInitializer.EnsureInitialized<IDictionary<string, double>>(ref this.measurementsInternal, () => new ConcurrentDictionary<string, double>());
			}
			set
			{
				this.measurementsInternal = value;
			}
		}

		// Token: 0x04000259 RID: 601
		private IDictionary<string, double> measurementsInternal;
	}
}
