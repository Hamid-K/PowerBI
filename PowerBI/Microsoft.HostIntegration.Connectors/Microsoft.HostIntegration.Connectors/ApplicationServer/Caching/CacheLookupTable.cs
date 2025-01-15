using System;
using System.Collections.Generic;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000B0 RID: 176
	internal class CacheLookupTable
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x00014437 File Offset: 0x00012637
		public VersionRanges Ranges
		{
			get
			{
				return this.m_ranges;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0001443F File Offset: 0x0001263F
		public GenerationNumber GenNumber
		{
			get
			{
				return this.m_generationNumber;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x00014447 File Offset: 0x00012647
		public string CacheName
		{
			get
			{
				return this.m_cacheName;
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001444F File Offset: 0x0001264F
		public CacheLookupTable(string cacheName)
		{
			this.m_cacheName = cacheName;
			this.m_entries = null;
			this.m_ranges = new VersionRanges(0L, 0L);
			this.m_generationNumber = GenerationNumber.Zero;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00014480 File Offset: 0x00012680
		public CacheLookupTable(CacheLookupTableTransfer lookupTransfer, string cacheName)
		{
			this.m_cacheName = cacheName;
			this.m_ranges = lookupTransfer.Ranges;
			if (lookupTransfer.Entries != null && lookupTransfer.Entries.Count > 0)
			{
				if (CacheLookupTable.IsFixed(lookupTransfer.Entries))
				{
					this.m_type = CacheLookupTable.CacheLookupType.Fixed;
					this.m_rangeSize = CacheLookupTable.GetCachePartitionSize(lookupTransfer.Entries[0]);
				}
				else
				{
					this.m_type = CacheLookupTable.CacheLookupType.Variant;
					this.m_rangeSize = 0U;
				}
			}
			this.m_entries = lookupTransfer.Entries.ToArray();
			this.m_generationNumber = lookupTransfer.GenerationNumber;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00014514 File Offset: 0x00012714
		public string LookupEndpointAddress(int key)
		{
			CacheLookupTableEntry cacheLookupTableEntry = this.LookupTableEntry(key);
			if (cacheLookupTableEntry != null)
			{
				return cacheLookupTableEntry.Config.Primary;
			}
			return null;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0001453C File Offset: 0x0001273C
		public CachePartitionId LookupPartitionId(int key)
		{
			CacheLookupTableEntry cacheLookupTableEntry = this.LookupTableEntry(key);
			if (cacheLookupTableEntry != null)
			{
				return cacheLookupTableEntry.Pid;
			}
			return null;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001455C File Offset: 0x0001275C
		private CacheLookupTableEntry LookupTableEntry(int key)
		{
			if (this.m_entries != null)
			{
				CacheLookupTableEntry cacheLookupTableEntry = null;
				if (this.m_type == CacheLookupTable.CacheLookupType.Fixed)
				{
					cacheLookupTableEntry = this.FixedLookup(key);
				}
				else if (this.m_type == CacheLookupTable.CacheLookupType.Variant)
				{
					cacheLookupTableEntry = this.VariantLookup(key);
				}
				if (cacheLookupTableEntry != null)
				{
					return cacheLookupTableEntry;
				}
			}
			return null;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0001459C File Offset: 0x0001279C
		public CacheLookupTable(List<CacheLookupTableEntry> entries, string cacheName)
		{
			this.m_cacheName = cacheName;
			entries.Sort(CacheLookupTableEntry.s_comparison);
			int count = entries.Count;
			this.m_entries = new CacheLookupTableEntry[entries.Count + 1];
			for (int i = 0; i < count; i++)
			{
				this.m_entries[i] = entries[i];
			}
			this.m_entries[count] = entries[count - 1];
			this.m_rangeSize = CacheLookupTable.GetPartitionSize(entries[0]);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001461C File Offset: 0x0001281C
		public bool IsNewer(CacheLookupTableTransfer newLookupTable)
		{
			GenerationNumber generationNumber = newLookupTable.GenerationNumber;
			int num = generationNumber.CompareTo(this.m_generationNumber);
			return num >= 0 && newLookupTable.Count > 0;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00014650 File Offset: 0x00012850
		public List<CachePartitionId> GetCachePartitionIds()
		{
			if (this.m_entries != null)
			{
				List<CachePartitionId> list = new List<CachePartitionId>(this.m_entries.Length);
				for (int i = 0; i < this.m_entries.Length; i++)
				{
					list.Add(this.m_entries[i].Pid);
				}
				return list;
			}
			return null;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0001469C File Offset: 0x0001289C
		private CacheLookupTableEntry FixedLookup(int key)
		{
			if (this.m_rangeSize > 0U)
			{
				uint num = (uint)((key - int.MinValue) / (int)this.m_rangeSize);
				return this.m_entries[(int)((UIntPtr)num)];
			}
			return null;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000146CC File Offset: 0x000128CC
		private CacheLookupTableEntry VariantLookup(int key)
		{
			int i = 0;
			int num = this.m_entries.Length - 1;
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
			return null;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00014734 File Offset: 0x00012934
		private static uint GetCachePartitionSize(CacheLookupTableEntry entry)
		{
			CachePartitionId pid = entry.Pid;
			return (uint)(pid.HighKey - pid.LowKey + 1);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00014758 File Offset: 0x00012958
		private static uint GetPartitionSize(CacheLookupTableEntry entry)
		{
			CachePartitionId pid = entry.Pid;
			return (uint)(pid.HighKey - pid.LowKey + 1);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001477C File Offset: 0x0001297C
		private static bool IsFixed(List<CacheLookupTableEntry> entries)
		{
			if (entries.Count <= 1 || entries[0].Pid.LowKey != -2147483648 || entries[entries.Count - 1].Pid.HighKey != 2147483647)
			{
				return false;
			}
			uint partitionSize = CacheLookupTable.GetPartitionSize(entries[0]);
			for (int i = 1; i < entries.Count; i++)
			{
				if (entries[i].Pid.LowKey != entries[i - 1].Pid.HighKey + 1)
				{
					return false;
				}
				uint partitionSize2 = CacheLookupTable.GetPartitionSize(entries[i]);
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

		// Token: 0x0400032C RID: 812
		private string m_cacheName;

		// Token: 0x0400032D RID: 813
		private CacheLookupTableEntry[] m_entries;

		// Token: 0x0400032E RID: 814
		private VersionRanges m_ranges;

		// Token: 0x0400032F RID: 815
		private GenerationNumber m_generationNumber;

		// Token: 0x04000330 RID: 816
		private CacheLookupTable.CacheLookupType m_type;

		// Token: 0x04000331 RID: 817
		private uint m_rangeSize;

		// Token: 0x020000B1 RID: 177
		internal enum CacheLookupType
		{
			// Token: 0x04000333 RID: 819
			Fixed,
			// Token: 0x04000334 RID: 820
			Variant,
			// Token: 0x04000335 RID: 821
			Invalid
		}
	}
}
