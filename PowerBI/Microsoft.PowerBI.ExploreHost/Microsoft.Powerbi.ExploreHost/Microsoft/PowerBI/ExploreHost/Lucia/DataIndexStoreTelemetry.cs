using System;
using Microsoft.Lucia.Core;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x02000054 RID: 84
	internal sealed class DataIndexStoreTelemetry : DataIndexTelemetry
	{
		// Token: 0x0600027E RID: 638 RVA: 0x00008566 File Offset: 0x00006766
		public DataIndexStoreTelemetry(DataIndexStoreActionId actionId, LuciaSessionOptions options)
		{
			this.ActionId = actionId;
			base.Status = DataIndexOperationStatus.Succeeded;
			base.Options = options;
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00008583 File Offset: 0x00006783
		// (set) Token: 0x06000280 RID: 640 RVA: 0x0000858B File Offset: 0x0000678B
		public DataIndexStoreActionId ActionId { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00008594 File Offset: 0x00006794
		// (set) Token: 0x06000282 RID: 642 RVA: 0x0000859C File Offset: 0x0000679C
		public OpenDataIndexWarnings Warnings { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000283 RID: 643 RVA: 0x000085A5 File Offset: 0x000067A5
		// (set) Token: 0x06000284 RID: 644 RVA: 0x000085AD File Offset: 0x000067AD
		public TimeSpan Duration { get; set; }
	}
}
