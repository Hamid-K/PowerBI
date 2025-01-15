using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Fabric.Common;

namespace Microsoft.Fabric.Data
{
	// Token: 0x020003B6 RID: 950
	internal abstract class PartitionTable
	{
		// Token: 0x06002192 RID: 8594 RVA: 0x0006786C File Offset: 0x00065A6C
		protected PartitionTable(object lockObject)
		{
			this.m_sortedEntries = new SortedDictionary<long, LookupTableEntry>(PartitionTable.LookupVersionComparer.Singleton);
			this.m_entries = new SortedList<PartitionId, LookupTableEntry>();
			this.m_lockObject = lockObject;
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06002193 RID: 8595
		protected abstract GenerationNumber GenerationNumber { get; }

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06002194 RID: 8596 RVA: 0x00067896 File Offset: 0x00065A96
		public object LockObject
		{
			get
			{
				return this.m_lockObject;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06002195 RID: 8597 RVA: 0x0006789E File Offset: 0x00065A9E
		protected IEnumerable<LookupTableEntry> Entries
		{
			get
			{
				return this.m_entries.Values;
			}
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x000678AC File Offset: 0x00065AAC
		protected void UpdateEntry(LookupTableEntry newEntry)
		{
			LookupTableEntry lookupTableEntry;
			if (!this.m_entries.TryGetValue(newEntry.Pid, out lookupTableEntry))
			{
				this.m_entries[newEntry.Pid] = newEntry;
				for (int i = this.m_entries.IndexOfKey(newEntry.Pid); i > 0; i--)
				{
					LookupTableEntry lookupTableEntry2 = this.m_entries.Values[i - 1];
					if (lookupTableEntry2.ServiceNamespace != newEntry.ServiceNamespace || lookupTableEntry2.Pid.HighKey < newEntry.Pid.LowKey)
					{
						IL_018C:
						while (i < this.m_entries.Count - 1)
						{
							LookupTableEntry lookupTableEntry3 = this.m_entries.Values[i + 1];
							if (lookupTableEntry3.ServiceNamespace != newEntry.ServiceNamespace || lookupTableEntry3.Pid.LowKey > newEntry.Pid.HighKey)
							{
								break;
							}
							if (lookupTableEntry3.Version >= newEntry.Version)
							{
								this.m_entries.Remove(newEntry.Pid);
								return;
							}
							this.m_entries.RemoveAt(i + 1);
							this.m_sortedEntries.Remove(lookupTableEntry3.Version);
						}
						goto Block_10;
					}
					if (lookupTableEntry2.Version >= newEntry.Version)
					{
						this.m_entries.Remove(newEntry.Pid);
						return;
					}
					this.m_entries.RemoveAt(i - 1);
					this.m_sortedEntries.Remove(lookupTableEntry2.Version);
				}
				goto IL_018C;
			}
			if (lookupTableEntry.Version >= newEntry.Version)
			{
				return;
			}
			this.m_sortedEntries.Remove(lookupTableEntry.Version);
			this.m_entries[newEntry.Pid] = newEntry;
			Block_10:
			try
			{
				this.m_sortedEntries.Add(newEntry.Version, newEntry);
			}
			catch (ArgumentException)
			{
				EventLogWriter.WriteError("PartitionTable", "Duplicate entry {0} and {1}", new object[]
				{
					newEntry,
					this.m_sortedEntries[newEntry.Version]
				});
				throw;
			}
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x00067AB4 File Offset: 0x00065CB4
		protected List<LookupTableEntry> GetUpdatedEntries(LookupTableRequest request)
		{
			List<LookupTableEntry> list = new List<LookupTableEntry>(this.m_sortedEntries.Count);
			List<VersionRange> ranges = request.Ranges.Ranges;
			int num = request.GenerationNumber.CompareTo(this.GenerationNumber);
			if (num > 0)
			{
				return list;
			}
			if (num < 0)
			{
				ranges.Clear();
			}
			int num2 = ranges.Count - 1;
			foreach (LookupTableEntry lookupTableEntry in this.m_sortedEntries.Values)
			{
				long version = lookupTableEntry.Version;
				while (num2 >= 0 && ranges[num2].StartVersion > version)
				{
					num2--;
				}
				if (num2 >= 0 && version < ranges[num2].EndVersion)
				{
					if (ranges[num2].StartVersion == 1L)
					{
						break;
					}
				}
				else if (request.InterestedApps == null || request.InterestedApps.Contains(lookupTableEntry.ServiceNamespace))
				{
					list.Add(lookupTableEntry);
				}
			}
			return list;
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x00067BC0 File Offset: 0x00065DC0
		public void RemoveEntry(PartitionId pid)
		{
			LookupTableEntry lookupTableEntry;
			if (this.m_entries.TryGetValue(pid, out lookupTableEntry))
			{
				this.m_entries.Remove(pid);
				this.m_sortedEntries.Remove(lookupTableEntry.Version);
			}
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x00067BFC File Offset: 0x00065DFC
		protected void Clear()
		{
			this.m_entries.Clear();
			this.m_sortedEntries.Clear();
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x00067C14 File Offset: 0x00065E14
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.m_sortedEntries.Count << 7);
			lock (this.m_lockObject)
			{
				foreach (LookupTableEntry lookupTableEntry in this.m_sortedEntries.Values)
				{
					stringBuilder.AppendFormat("{0}", lookupTableEntry).AppendLine();
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400156A RID: 5482
		private SortedDictionary<long, LookupTableEntry> m_sortedEntries;

		// Token: 0x0400156B RID: 5483
		private SortedList<PartitionId, LookupTableEntry> m_entries;

		// Token: 0x0400156C RID: 5484
		private object m_lockObject;

		// Token: 0x020003B7 RID: 951
		private class LookupVersionComparer : IComparer<long>
		{
			// Token: 0x0600219B RID: 8603 RVA: 0x00002061 File Offset: 0x00000261
			private LookupVersionComparer()
			{
			}

			// Token: 0x0600219C RID: 8604 RVA: 0x00067CB4 File Offset: 0x00065EB4
			public int Compare(long x, long y)
			{
				return y.CompareTo(x);
			}

			// Token: 0x0400156D RID: 5485
			public static readonly PartitionTable.LookupVersionComparer Singleton = new PartitionTable.LookupVersionComparer();
		}
	}
}
