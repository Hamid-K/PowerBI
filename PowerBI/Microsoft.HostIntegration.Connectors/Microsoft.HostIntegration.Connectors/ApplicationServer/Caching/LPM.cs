using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000352 RID: 850
	[DataContract]
	internal class LPM
	{
		// Token: 0x06001E0B RID: 7691 RVA: 0x00059FE7 File Offset: 0x000581E7
		internal LPM(string id, string name, PartitionHealth[] partitions)
		{
			this._nodeId = id;
			this._name = name;
			this._partitions = partitions;
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001E0C RID: 7692 RVA: 0x0005A004 File Offset: 0x00058204
		internal string NodeId
		{
			get
			{
				return this._nodeId;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001E0D RID: 7693 RVA: 0x0005A00C File Offset: 0x0005820C
		internal string HostName
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001E0E RID: 7694 RVA: 0x0005A014 File Offset: 0x00058214
		internal PartitionHealth[] Partitions
		{
			get
			{
				return this._partitions;
			}
		}

		// Token: 0x040010EB RID: 4331
		[DataMember]
		private string _nodeId;

		// Token: 0x040010EC RID: 4332
		[DataMember]
		private string _name;

		// Token: 0x040010ED RID: 4333
		[DataMember]
		private PartitionHealth[] _partitions;
	}
}
