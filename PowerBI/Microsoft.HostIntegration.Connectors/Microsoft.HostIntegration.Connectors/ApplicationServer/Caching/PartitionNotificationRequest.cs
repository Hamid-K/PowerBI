using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000267 RID: 615
	[DataContract(Name = "PartitionNotificationRequest", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal class PartitionNotificationRequest : IPartitionRequest
	{
		// Token: 0x0600148A RID: 5258 RVA: 0x000403BB File Offset: 0x0003E5BB
		public PartitionNotificationRequest(PartitionId partitionId, bool isCacheLevel, List<string> regionList, NotificationLsn lastLsn, int countNotificationToPoll)
		{
			this._partitionId = partitionId;
			this._isCacheLevel = isCacheLevel;
			if (!isCacheLevel)
			{
				this._regionList = regionList;
			}
			this._lastLsn = lastLsn;
			this._countNotificationToPoll = countNotificationToPoll;
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x000403EB File Offset: 0x0003E5EB
		public PartitionId PartitionId
		{
			get
			{
				return this._partitionId;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x000403F3 File Offset: 0x0003E5F3
		public bool IsCacheLevel
		{
			get
			{
				return this._isCacheLevel;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x000403FB File Offset: 0x0003E5FB
		public List<string> RegionList
		{
			get
			{
				return this._regionList;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x00040403 File Offset: 0x0003E603
		internal NotificationLsn LastLsn
		{
			get
			{
				return this._lastLsn;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x0004040B File Offset: 0x0003E60B
		internal int CountNotificationToPoll
		{
			get
			{
				return this._countNotificationToPoll;
			}
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x00040414 File Offset: 0x0003E614
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.ToString());
			stringBuilder.Append(":" + this.PartitionId.ToString());
			stringBuilder.Append(":" + this._lastLsn.ToString());
			stringBuilder.Append(":" + this._countNotificationToPoll.ToString(CultureInfo.InvariantCulture));
			if (this._isCacheLevel)
			{
				stringBuilder.Append(":CacheLevel");
			}
			else
			{
				stringBuilder.Append(":RegionLevel");
				if (this._regionList != null)
				{
					for (int i = 0; i < this._regionList.Count; i++)
					{
						stringBuilder.Append(":" + this._regionList[i]);
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000C46 RID: 3142
		public const int MinimumLengthVwFormat = 28;

		// Token: 0x04000C47 RID: 3143
		[DataMember]
		private PartitionId _partitionId;

		// Token: 0x04000C48 RID: 3144
		[DataMember]
		private bool _isCacheLevel;

		// Token: 0x04000C49 RID: 3145
		[DataMember]
		private List<string> _regionList;

		// Token: 0x04000C4A RID: 3146
		[DataMember]
		private NotificationLsn _lastLsn;

		// Token: 0x04000C4B RID: 3147
		[DataMember]
		private int _countNotificationToPoll;
	}
}
