using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace Microsoft.Fabric.Data
{
	// Token: 0x020003B1 RID: 945
	internal class ReplicaSetMap
	{
		// Token: 0x06002167 RID: 8551 RVA: 0x0006709A File Offset: 0x0006529A
		internal ReplicaSetMap(string serviceNamespace, LookupTable.LookupList list)
		{
			this.m_serviceNamespace = serviceNamespace;
			this.m_list = list;
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06002168 RID: 8552 RVA: 0x000670B0 File Offset: 0x000652B0
		public string ServiceNamespace
		{
			get
			{
				return this.m_serviceNamespace;
			}
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x000670B8 File Offset: 0x000652B8
		public ServiceReplicaSet Lookup(int key)
		{
			LookupTable.LookupList list = this.m_list;
			LookupTable.ListType type = list.Type;
			if (type == LookupTable.ListType.Fixed)
			{
				return list.FixedLookup(key).Config;
			}
			if (type == LookupTable.ListType.Variant)
			{
				return list.VariantLookup(key).Config;
			}
			throw new LookupNamespaceNotFoundException();
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x000670FC File Offset: 0x000652FC
		internal PartitionId GetPartitionId(int regionKey)
		{
			LookupTable.ListType type = this.m_list.Type;
			if (type == LookupTable.ListType.Fixed)
			{
				return this.m_list.FixedLookup(regionKey).Pid;
			}
			if (type == LookupTable.ListType.Variant)
			{
				return this.m_list.VariantLookup(regionKey).Pid;
			}
			throw new LookupNamespaceNotFoundException();
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x00067145 File Offset: 0x00065345
		public ReadOnlyCollection<PartitionId> GetPartitionIds()
		{
			if (!this.IsValid)
			{
				throw new LookupNamespaceNotFoundException();
			}
			return new ReadOnlyCollection<PartitionId>(this.m_list.GetPartitionIds());
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x0600216C RID: 8556 RVA: 0x00067165 File Offset: 0x00065365
		public bool IsValid
		{
			get
			{
				return this.m_list != LookupTable.LookupList.Invalid;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x0600216D RID: 8557 RVA: 0x00067177 File Offset: 0x00065377
		// (set) Token: 0x0600216E RID: 8558 RVA: 0x0006717F File Offset: 0x0006537F
		internal LookupTable.LookupList List
		{
			get
			{
				return this.m_list;
			}
			set
			{
				Interlocked.Exchange<LookupTable.LookupList>(ref this.m_list, value);
			}
		}

		// Token: 0x04001551 RID: 5457
		private string m_serviceNamespace;

		// Token: 0x04001552 RID: 5458
		private LookupTable.LookupList m_list;
	}
}
