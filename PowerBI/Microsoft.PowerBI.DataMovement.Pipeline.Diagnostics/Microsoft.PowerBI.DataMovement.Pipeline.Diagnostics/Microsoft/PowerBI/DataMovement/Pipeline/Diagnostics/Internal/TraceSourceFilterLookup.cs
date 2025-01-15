using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal
{
	// Token: 0x020000E1 RID: 225
	[NullableContext(1)]
	[Nullable(0)]
	public static class TraceSourceFilterLookup
	{
		// Token: 0x06001105 RID: 4357 RVA: 0x0004680C File Offset: 0x00044A0C
		public static TraceSourceFilter GetFilter(string sourceId)
		{
			if (TraceSourceFilterLookup.s_dictionary == null)
			{
				TraceSourceFilterLookup.Initialize();
			}
			TraceSourceFilter traceSourceFilter;
			if (!TraceSourceFilterLookup.s_dictionary.TryGetValue(sourceId, out traceSourceFilter))
			{
				traceSourceFilter = TraceSourceFilter.GatewayDefault;
			}
			return traceSourceFilter;
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00046837 File Offset: 0x00044A37
		private static void Initialize()
		{
			TraceSourceFilterLookup.s_dictionary = new ConcurrentDictionary<string, TraceSourceFilter>();
			TraceSourceFilterLookup.s_dictionary.TryAdd("DM.NetworkCore", TraceSourceFilter.NetworkCore);
		}

		// Token: 0x0400036E RID: 878
		private static ConcurrentDictionary<string, TraceSourceFilter> s_dictionary;
	}
}
