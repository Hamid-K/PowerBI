using System;
using System.CodeDom.Compiler;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.External
{
	// Token: 0x020000BB RID: 187
	[GeneratedCode("gbc", "0.4.1.0")]
	internal class MessageData : Domain, ISerializableWithWriter
	{
		// Token: 0x060005F8 RID: 1528 RVA: 0x00016BC0 File Offset: 0x00014DC0
		public MessageData DeepClone()
		{
			MessageData messageData = new MessageData();
			messageData.ver = this.ver;
			messageData.message = this.message;
			messageData.severityLevel = this.severityLevel;
			Utils.CopyDictionary<string>(this.properties, messageData.properties);
			return messageData;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00016C0C File Offset: 0x00014E0C
		public void Serialize(ISerializationWriter serializationWriter)
		{
			serializationWriter.WriteProperty("ver", new int?(this.ver));
			serializationWriter.WriteProperty("message", this.message);
			serializationWriter.WriteProperty("severityLevel", (this.severityLevel != null) ? this.severityLevel.Value.ToString() : null);
			serializationWriter.WriteProperty("properties", this.properties);
			serializationWriter.WriteProperty("measurements", this.measurements);
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x00016C9C File Offset: 0x00014E9C
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x00016CA4 File Offset: 0x00014EA4
		public int ver { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x00016CAD File Offset: 0x00014EAD
		// (set) Token: 0x060005FD RID: 1533 RVA: 0x00016CB5 File Offset: 0x00014EB5
		public string message { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x00016CBE File Offset: 0x00014EBE
		// (set) Token: 0x060005FF RID: 1535 RVA: 0x00016CC6 File Offset: 0x00014EC6
		public SeverityLevel? severityLevel { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x00016CCF File Offset: 0x00014ECF
		// (set) Token: 0x06000601 RID: 1537 RVA: 0x00016CD7 File Offset: 0x00014ED7
		public IDictionary<string, string> properties { get; set; }

		// Token: 0x06000602 RID: 1538 RVA: 0x00016CE0 File Offset: 0x00014EE0
		public MessageData()
			: this("AI.MessageData", "MessageData")
		{
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00016CF2 File Offset: 0x00014EF2
		protected MessageData(string fullName, string name)
		{
			this.ver = 2;
			this.message = "";
			this.properties = new ConcurrentDictionary<string, string>();
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x00016D17 File Offset: 0x00014F17
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x00016D43 File Offset: 0x00014F43
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

		// Token: 0x0400026B RID: 619
		private IDictionary<string, double> measurementsInternal;
	}
}
