using System;
using System.Collections.Generic;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000260 RID: 608
	[Serializable]
	internal class NotificationReply : IBinarySerializable
	{
		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x0003FF8E File Offset: 0x0003E18E
		internal List<PartitionNotificationReply> PartitionNotificationReplyList
		{
			get
			{
				return this._partitionNotificationReply;
			}
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x0003FF98 File Offset: 0x0003E198
		internal PartitionNotificationReply GetPartitionNotificationReply(PartitionId partitionId)
		{
			foreach (object obj in this._partitionNotificationReply)
			{
				PartitionNotificationReply partitionNotificationReply = (PartitionNotificationReply)obj;
				if (partitionNotificationReply.PartitionId.Equals(partitionId))
				{
					return partitionNotificationReply;
				}
			}
			return null;
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x0003FFDD File Offset: 0x0003E1DD
		public NotificationReply()
		{
			this._partitionNotificationReply = new List<PartitionNotificationReply>();
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x0003FFF0 File Offset: 0x0003E1F0
		public NotificationReply(List<PartitionNotificationReply> list)
		{
			this._partitionNotificationReply = list;
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x00040000 File Offset: 0x0003E200
		void IBinarySerializable.ReadStream(ISerializationReader reader)
		{
			int num = reader.ReadInt32();
			if (num != 0)
			{
				this._partitionNotificationReply = new List<PartitionNotificationReply>(num);
				for (int i = 0; i < num; i++)
				{
					PartitionNotificationReply partitionNotificationReply = new PartitionNotificationReply();
					((IBinarySerializable)partitionNotificationReply).ReadStream(reader);
					this._partitionNotificationReply.Add(partitionNotificationReply);
				}
			}
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x00040048 File Offset: 0x0003E248
		void IBinarySerializable.WriteStream(ISerializationWriter writer)
		{
			int num = 0;
			if (this._partitionNotificationReply != null)
			{
				num = this._partitionNotificationReply.Count;
			}
			writer.Write(num);
			for (int i = 0; i < num; i++)
			{
				((IBinarySerializable)this._partitionNotificationReply[i]).WriteStream(writer);
			}
		}

		// Token: 0x04000C38 RID: 3128
		private List<PartitionNotificationReply> _partitionNotificationReply;
	}
}
