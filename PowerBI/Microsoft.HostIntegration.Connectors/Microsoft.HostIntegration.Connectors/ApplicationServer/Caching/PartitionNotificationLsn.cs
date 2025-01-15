using System;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000023 RID: 35
	internal class PartitionNotificationLsn
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x00005791 File Offset: 0x00003991
		public PartitionNotificationLsn(PartitionId partitionId, NotificationLsn lastLsnResp)
		{
			this._partitionId = partitionId;
			this._lastLsnResp = lastLsnResp;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x000057A7 File Offset: 0x000039A7
		public PartitionId PartitionId
		{
			get
			{
				return this._partitionId;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x000057AF File Offset: 0x000039AF
		public NotificationLsn LastLsn
		{
			get
			{
				return this._lastLsnResp;
			}
		}

		// Token: 0x04000088 RID: 136
		private PartitionId _partitionId;

		// Token: 0x04000089 RID: 137
		private NotificationLsn _lastLsnResp;
	}
}
