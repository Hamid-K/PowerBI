using System;
using System.Configuration;
using System.Diagnostics;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000CC RID: 204
	internal class ClientTraceSettings : ConfigurationElement
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00017B55 File Offset: 0x00015D55
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x00017B67 File Offset: 0x00015D67
		[ConfigurationProperty("sinkType", IsRequired = false, DefaultValue = DataCacheTraceSink.EtwSink)]
		public DataCacheTraceSink SinkType
		{
			get
			{
				return (DataCacheTraceSink)base["sinkType"];
			}
			set
			{
				base["sinkType"] = value;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x00017B7A File Offset: 0x00015D7A
		// (set) Token: 0x0600057F RID: 1407 RVA: 0x00017B8C File Offset: 0x00015D8C
		[ConfigurationProperty("traceLevel", IsRequired = false, DefaultValue = TraceLevel.Verbose)]
		public TraceLevel ClientTraceLevel
		{
			get
			{
				return (TraceLevel)base["traceLevel"];
			}
			set
			{
				base["traceLevel"] = value;
			}
		}

		// Token: 0x040003BC RID: 956
		internal const string SINK_TYPE = "sinkType";

		// Token: 0x040003BD RID: 957
		internal const string TRACE_LEVEL = "traceLevel";
	}
}
