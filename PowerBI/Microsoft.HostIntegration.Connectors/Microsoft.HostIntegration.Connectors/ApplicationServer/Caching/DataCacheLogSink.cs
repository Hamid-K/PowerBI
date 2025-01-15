using System;
using System.Diagnostics;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000334 RID: 820
	internal sealed class DataCacheLogSink
	{
		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001D94 RID: 7572 RVA: 0x0005925E File Offset: 0x0005745E
		internal DataCacheSinkType Type
		{
			get
			{
				return this.sinkType;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001D95 RID: 7573 RVA: 0x00059266 File Offset: 0x00057466
		internal TraceLevel Level
		{
			get
			{
				return this.logLevel;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001D96 RID: 7574 RVA: 0x0005926E File Offset: 0x0005746E
		internal string Param
		{
			get
			{
				return this.sinkParam;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001D97 RID: 7575 RVA: 0x00059276 File Offset: 0x00057476
		// (set) Token: 0x06001D98 RID: 7576 RVA: 0x0005927E File Offset: 0x0005747E
		internal bool IsConfigEntryPresent { get; set; }

		// Token: 0x06001D99 RID: 7577 RVA: 0x00059288 File Offset: 0x00057488
		public DataCacheLogSink(DataCacheSinkType sinkType)
		{
			this.IsConfigEntryPresent = false;
			this.sinkType = sinkType;
			this.logLevel = TraceLevel.Warning;
			switch (this.sinkType)
			{
			case DataCacheSinkType.Console:
				this.sinkParam = null;
				return;
			case DataCacheSinkType.File:
				this.sinkParam = "DCacheTrace[$]/dd-HH";
				return;
			case DataCacheSinkType.Etw:
				this.sinkParam = null;
				return;
			case DataCacheSinkType.DiagnosticsTraceSink:
				this.sinkParam = null;
				return;
			default:
				return;
			}
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x000592F1 File Offset: 0x000574F1
		public DataCacheLogSink(DataCacheSinkType sinkType, TraceLevel logLevel)
			: this(sinkType)
		{
			this.logLevel = logLevel;
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x00059301 File Offset: 0x00057501
		public DataCacheLogSink(DataCacheSinkType sinkType, TraceLevel logLevel, string sinkParameter)
		{
			this.IsConfigEntryPresent = false;
			this.sinkType = sinkType;
			this.logLevel = logLevel;
			this.sinkParam = sinkParameter;
		}

		// Token: 0x04001055 RID: 4181
		private DataCacheSinkType sinkType;

		// Token: 0x04001056 RID: 4182
		private TraceLevel logLevel;

		// Token: 0x04001057 RID: 4183
		private string sinkParam;
	}
}
