using System;
using System.Collections.Generic;

namespace Microsoft.Fabric.Data
{
	// Token: 0x020003BD RID: 957
	internal class LookupTable
	{
		// Token: 0x060021B6 RID: 8630 RVA: 0x00067FA2 File Offset: 0x000661A2
		internal LookupTable()
		{
			this.m_appTable = new Dictionary<string, ReplicaSetMap>();
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x00067FB8 File Offset: 0x000661B8
		internal void ReplaceLookupLists(IEnumerable<LookupTableEntry> entries)
		{
			if (entries == null)
			{
				return;
			}
			List<LookupTableEntry> list = new List<LookupTableEntry>(entries);
			list.Sort(LookupTable.s_comparison);
			HashSet<string> hashSet = new HashSet<string>();
			Dictionary<string, ReplicaSetMap> dictionary = new Dictionary<string, ReplicaSetMap>(this.m_appTable);
			int num;
			for (int i = 0; i < list.Count; i = num)
			{
				string serviceNamespace = list[i].ServiceNamespace;
				num = i + 1;
				while (num < list.Count && list[num].ServiceNamespace == serviceNamespace)
				{
					num++;
				}
				List<LookupTableEntry> range = list.GetRange(i, num - i);
				LookupTable.LookupList lookupList = new LookupTable.LookupList(range);
				ReplicaSetMap replicaSetMap;
				if (dictionary.TryGetValue(serviceNamespace, out replicaSetMap))
				{
					replicaSetMap.List = lookupList;
				}
				else
				{
					replicaSetMap = new ReplicaSetMap(serviceNamespace, lookupList);
					dictionary[serviceNamespace] = replicaSetMap;
				}
				hashSet.Add(serviceNamespace);
			}
			foreach (KeyValuePair<string, ReplicaSetMap> keyValuePair in dictionary)
			{
				if (!hashSet.Contains(keyValuePair.Key))
				{
					keyValuePair.Value.List = LookupTable.LookupList.Invalid;
				}
			}
			this.m_appTable = dictionary;
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x000680EC File Offset: 0x000662EC
		public ReplicaSetMap GetApplicationLookupTable(string serviceNamespace)
		{
			ReplicaSetMap replicaSetMap;
			if (!this.m_appTable.TryGetValue(serviceNamespace, out replicaSetMap) || !replicaSetMap.IsValid)
			{
				throw new LookupNamespaceNotFoundException();
			}
			return replicaSetMap;
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x00068118 File Offset: 0x00066318
		private static int ComparePartitionConfig(LookupTableEntry x, LookupTableEntry y)
		{
			int num = string.Compare(x.ServiceNamespace, y.ServiceNamespace, StringComparison.Ordinal);
			if (num == 0)
			{
				num = x.Pid.CompareTo(y.Pid);
			}
			return num;
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x00068150 File Offset: 0x00066350
		private static uint GetPartitionSize(LookupTableEntry entry)
		{
			PartitionId pid = entry.Pid;
			return (uint)(pid.HighKey - pid.LowKey + 1);
		}

		// Token: 0x04001574 RID: 5492
		private Dictionary<string, ReplicaSetMap> m_appTable;

		// Token: 0x04001575 RID: 5493
		private static readonly Comparison<LookupTableEntry> s_comparison = new Comparison<LookupTableEntry>(LookupTable.ComparePartitionConfig);

		// Token: 0x020003BE RID: 958
		internal enum ListType
		{
			// Token: 0x04001577 RID: 5495
			Fixed,
			// Token: 0x04001578 RID: 5496
			Variant,
			// Token: 0x04001579 RID: 5497
			Invalid
		}

		// Token: 0x020003BF RID: 959
		internal class LookupList
		{
			// Token: 0x060021BC RID: 8636 RVA: 0x00068186 File Offset: 0x00066386
			private LookupList()
			{
				this.m_entries = null;
				this.m_rangeSize = 0U;
				this.m_count = 0;
				this.m_type = LookupTable.ListType.Invalid;
			}

			// Token: 0x060021BD RID: 8637 RVA: 0x000681AC File Offset: 0x000663AC
			public LookupList(List<LookupTableEntry> entries)
			{
				this.m_count = entries.Count;
				this.m_entries = new LookupTableEntry[this.m_count + 1];
				for (int i = 0; i < this.m_count; i++)
				{
					this.m_entries[i] = entries[i];
				}
				this.m_entries[this.m_count] = entries[this.m_count - 1];
				if (LookupTable.LookupList.IsFixed(entries))
				{
					this.m_type = LookupTable.ListType.Fixed;
					this.m_rangeSize = LookupTable.GetPartitionSize(entries[0]);
					return;
				}
				this.m_type = LookupTable.ListType.Variant;
				this.m_rangeSize = 0U;
			}

			// Token: 0x060021BE RID: 8638 RVA: 0x0006824C File Offset: 0x0006644C
			private static bool IsFixed(List<LookupTableEntry> entries)
			{
				if (entries.Count <= 1 || entries[0].Pid.LowKey != -2147483648 || entries[entries.Count - 1].Pid.HighKey != 2147483647)
				{
					return false;
				}
				uint partitionSize = LookupTable.GetPartitionSize(entries[0]);
				for (int i = 1; i < entries.Count; i++)
				{
					if (entries[i].Pid.LowKey != entries[i - 1].Pid.HighKey + 1)
					{
						return false;
					}
					uint partitionSize2 = LookupTable.GetPartitionSize(entries[i]);
					if (i < entries.Count - 1)
					{
						if (partitionSize != partitionSize2)
						{
							return false;
						}
					}
					else if (partitionSize2 < partitionSize || partitionSize2 >> 1 >= partitionSize)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x170006CD RID: 1741
			// (get) Token: 0x060021BF RID: 8639 RVA: 0x0006830E File Offset: 0x0006650E
			public LookupTable.ListType Type
			{
				get
				{
					return this.m_type;
				}
			}

			// Token: 0x060021C0 RID: 8640 RVA: 0x00068318 File Offset: 0x00066518
			public LookupTableEntry FixedLookup(int key)
			{
				uint num = (uint)((key - int.MinValue) / (int)this.m_rangeSize);
				return this.m_entries[(int)((UIntPtr)num)];
			}

			// Token: 0x060021C1 RID: 8641 RVA: 0x00068340 File Offset: 0x00066540
			public LookupTableEntry VariantLookup(int key)
			{
				int i = 0;
				int num = this.m_count - 1;
				while (i <= num)
				{
					int num2 = i + (num - i) / 2;
					if (key >= this.m_entries[num2].Pid.LowKey)
					{
						if (key <= this.m_entries[num2].Pid.HighKey)
						{
							return this.m_entries[num2];
						}
						i = num2 + 1;
					}
					else
					{
						num = num2 - 1;
					}
				}
				throw new LookupKeyNotFoundException();
			}

			// Token: 0x060021C2 RID: 8642 RVA: 0x000683A8 File Offset: 0x000665A8
			public List<PartitionId> GetPartitionIds()
			{
				List<PartitionId> list = new List<PartitionId>(this.m_count);
				for (int i = 0; i < this.m_count; i++)
				{
					list.Add(this.m_entries[i].Pid);
				}
				return list;
			}

			// Token: 0x0400157A RID: 5498
			private LookupTable.ListType m_type;

			// Token: 0x0400157B RID: 5499
			private LookupTableEntry[] m_entries;

			// Token: 0x0400157C RID: 5500
			private int m_count;

			// Token: 0x0400157D RID: 5501
			private uint m_rangeSize;

			// Token: 0x0400157E RID: 5502
			public static readonly LookupTable.LookupList Invalid = new LookupTable.LookupList();
		}
	}
}
