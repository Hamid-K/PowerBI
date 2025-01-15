using System;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000265 RID: 613
	internal interface IPartitionReply
	{
		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600147B RID: 5243
		PartitionId PartitionId { get; }

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x0600147C RID: 5244
		PartitionRespStatus RespStatus { get; }
	}
}
