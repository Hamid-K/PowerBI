using System;
using System.Runtime.Serialization;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000264 RID: 612
	[DataContract(Name = "PartitionNotificationLsnRequest", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal class PartitionNotificationLsnRequest : IPartitionRequest
	{
		// Token: 0x06001479 RID: 5241 RVA: 0x000400A7 File Offset: 0x0003E2A7
		public PartitionNotificationLsnRequest(PartitionId partitionId)
		{
			this._partitionId = partitionId;
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x000400B6 File Offset: 0x0003E2B6
		public PartitionId PartitionId
		{
			get
			{
				return this._partitionId;
			}
		}

		// Token: 0x04000C3F RID: 3135
		public const int MinimumLengthVwFormat = 8;

		// Token: 0x04000C40 RID: 3136
		[DataMember]
		private PartitionId _partitionId;
	}
}
