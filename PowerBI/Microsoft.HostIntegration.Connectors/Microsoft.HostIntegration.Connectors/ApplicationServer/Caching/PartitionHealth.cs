using System;
using System.Runtime.Serialization;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000351 RID: 849
	[DataContract]
	internal class PartitionHealth
	{
		// Token: 0x06001E03 RID: 7683 RVA: 0x00059F28 File Offset: 0x00058128
		internal PartitionHealth(PartitionId id, RegionReplicaState role, PartitionState state)
		{
			this._id = id;
			this._role = role;
			this._state = state;
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x00059F48 File Offset: 0x00058148
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Partition ID=",
				this._id.ToString(),
				", Role=",
				this._role.ToString(),
				", State=",
				this._state.ToString()
			});
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001E05 RID: 7685 RVA: 0x00059FAE File Offset: 0x000581AE
		internal PartitionId Pid
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001E06 RID: 7686 RVA: 0x00059FB6 File Offset: 0x000581B6
		internal RegionReplicaState Role
		{
			get
			{
				return this._role;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001E07 RID: 7687 RVA: 0x00059FBE File Offset: 0x000581BE
		// (set) Token: 0x06001E08 RID: 7688 RVA: 0x00059FC6 File Offset: 0x000581C6
		internal PartitionState State
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001E09 RID: 7689 RVA: 0x00059FCF File Offset: 0x000581CF
		internal bool IsPrimary
		{
			get
			{
				return this._role == RegionReplicaState.Primary;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001E0A RID: 7690 RVA: 0x00059FDA File Offset: 0x000581DA
		internal string NamedCache
		{
			get
			{
				return this._id.ServiceNamespace;
			}
		}

		// Token: 0x040010E8 RID: 4328
		[DataMember]
		private PartitionId _id;

		// Token: 0x040010E9 RID: 4329
		[DataMember]
		private RegionReplicaState _role;

		// Token: 0x040010EA RID: 4330
		[DataMember]
		private PartitionState _state;
	}
}
