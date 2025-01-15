using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000266 RID: 614
	[DataContract(Name = "PartitionNotificationReply", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal class PartitionNotificationReply : IPartitionReply, IBinarySerializable
	{
		// Token: 0x0600147D RID: 5245 RVA: 0x000400BE File Offset: 0x0003E2BE
		public PartitionNotificationReply(PartitionId partitionId, NotificationLsn lastLsnResp, PartitionRespStatus respStatus)
		{
			this._partitionId = partitionId;
			this._lastLsnResp = lastLsnResp;
			this._respStatus = respStatus;
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x00002061 File Offset: 0x00000261
		public PartitionNotificationReply()
		{
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x000400DC File Offset: 0x0003E2DC
		void IBinarySerializable.ReadStream(ISerializationReader reader)
		{
			string text = reader.ReadString();
			int num = reader.ReadInt32();
			int num2 = reader.ReadInt32();
			this._partitionId = new PartitionId(text, num, num2);
			this._respStatus = (PartitionRespStatus)reader.ReadByte();
			this._notificationRespStatus = (NotificationRespStatus)reader.ReadByte();
			if (!reader.ReadBoolean())
			{
				this._lastLsnResp = new NotificationLsn();
				((IBinarySerializable)this._lastLsnResp).ReadStream(reader);
			}
			int num3 = reader.ReadInt32();
			if (num3 != 0)
			{
				this._rcvdEventList = new List<BaseOperationNotification>(num3);
			}
			for (int i = 0; i < num3; i++)
			{
				short num4 = reader.ReadInt16();
				BaseOperationNotification baseOperationNotification;
				if (num4 == 1)
				{
					baseOperationNotification = new DataCacheOperationDescriptor();
					baseOperationNotification.ReadStreamNoCacheName(text, reader);
				}
				else
				{
					baseOperationNotification = new BulkOpNotificationEvent();
					((IBinarySerializable)baseOperationNotification).ReadStream(reader);
				}
				this._rcvdEventList.Add(baseOperationNotification);
			}
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x000401B0 File Offset: 0x0003E3B0
		void IBinarySerializable.WriteStream(ISerializationWriter writer)
		{
			writer.Write(this.PartitionId.ServiceNamespace);
			writer.Write(this.PartitionId.LowKey);
			writer.Write(this.PartitionId.HighKey);
			writer.Write((byte)this._respStatus);
			writer.Write((byte)this._notificationRespStatus);
			bool flag = false;
			if (this._lastLsnResp == null)
			{
				flag = true;
			}
			writer.Write(flag);
			if (!flag)
			{
				((IBinarySerializable)this._lastLsnResp).WriteStream(writer);
			}
			int num = 0;
			if (this._rcvdEventList != null)
			{
				num = this._rcvdEventList.Count;
			}
			writer.Write(num);
			for (int i = 0; i < num; i++)
			{
				BaseOperationNotification baseOperationNotification = this._rcvdEventList[i];
				if (baseOperationNotification is DataCacheOperationDescriptor)
				{
					writer.Write(1);
					this._rcvdEventList[i].WriteStreamNoCacheName(writer);
				}
				else
				{
					writer.Write(2);
					((IBinarySerializable)this._rcvdEventList[i]).WriteStream(writer);
				}
			}
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x000402A4 File Offset: 0x0003E4A4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.ToString());
			stringBuilder.Append(":" + this._partitionId.ToString());
			stringBuilder.Append(":status=" + this._respStatus.ToString());
			stringBuilder.Append(":nstatus=" + this._notificationRespStatus.ToString());
			stringBuilder.Append(":" + ((this._lastLsnResp != null) ? this._lastLsnResp.ToString() : null));
			if (this._rcvdEventList != null)
			{
				stringBuilder.Append(":EventCount=" + this._rcvdEventList.Count);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x00040378 File Offset: 0x0003E578
		public PartitionId PartitionId
		{
			get
			{
				return this._partitionId;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x00040380 File Offset: 0x0003E580
		// (set) Token: 0x06001484 RID: 5252 RVA: 0x00040388 File Offset: 0x0003E588
		public NotificationLsn LastLsnResp
		{
			get
			{
				return this._lastLsnResp;
			}
			set
			{
				this._lastLsnResp = value;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x00040391 File Offset: 0x0003E591
		// (set) Token: 0x06001486 RID: 5254 RVA: 0x00040399 File Offset: 0x0003E599
		public List<BaseOperationNotification> RcvdEventList
		{
			get
			{
				return this._rcvdEventList;
			}
			set
			{
				this._rcvdEventList = value;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x000403A2 File Offset: 0x0003E5A2
		public PartitionRespStatus RespStatus
		{
			get
			{
				return this._respStatus;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001488 RID: 5256 RVA: 0x000403AA File Offset: 0x0003E5AA
		// (set) Token: 0x06001489 RID: 5257 RVA: 0x000403B2 File Offset: 0x0003E5B2
		internal NotificationRespStatus NotificationRespStatus
		{
			get
			{
				return this._notificationRespStatus;
			}
			set
			{
				this._notificationRespStatus = value;
			}
		}

		// Token: 0x04000C41 RID: 3137
		private NotificationRespStatus _notificationRespStatus;

		// Token: 0x04000C42 RID: 3138
		[DataMember]
		private PartitionId _partitionId;

		// Token: 0x04000C43 RID: 3139
		[DataMember]
		private NotificationLsn _lastLsnResp;

		// Token: 0x04000C44 RID: 3140
		[DataMember]
		private PartitionRespStatus _respStatus;

		// Token: 0x04000C45 RID: 3141
		[DataMember]
		private List<BaseOperationNotification> _rcvdEventList;
	}
}
