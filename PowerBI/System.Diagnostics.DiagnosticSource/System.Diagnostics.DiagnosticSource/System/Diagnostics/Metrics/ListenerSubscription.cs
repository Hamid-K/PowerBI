using System;

namespace System.Diagnostics.Metrics
{
	// Token: 0x0200004D RID: 77
	internal readonly struct ListenerSubscription
	{
		// Token: 0x0600025E RID: 606 RVA: 0x0000A4A0 File Offset: 0x000086A0
		internal ListenerSubscription(MeterListener listener, object state = null)
		{
			this.Listener = listener;
			this.State = state;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000A4B0 File Offset: 0x000086B0
		internal MeterListener Listener { get; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000A4B8 File Offset: 0x000086B8
		internal object State { get; }
	}
}
