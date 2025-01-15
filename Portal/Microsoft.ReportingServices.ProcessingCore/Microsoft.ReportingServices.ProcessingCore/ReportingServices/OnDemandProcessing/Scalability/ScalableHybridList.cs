using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000898 RID: 2200
	internal sealed class ScalableHybridList<T> : IEnumerable<T>, IEnumerable, IDisposable
	{
		// Token: 0x06007878 RID: 30840 RVA: 0x001F05A2 File Offset: 0x001EE7A2
		internal ScalableHybridList(int scalabilityPriority, IScalabilityCache cache, int segmentSize, int initialCapacity)
		{
			this.m_entries = new ScalableList<ScalableHybridListEntry>(scalabilityPriority, cache, segmentSize, initialCapacity);
		}

		// Token: 0x06007879 RID: 30841 RVA: 0x001F05D0 File Offset: 0x001EE7D0
		internal int Add(T item)
		{
			int num = -1;
			if (this.m_firstFree != -1)
			{
				num = this.m_firstFree;
				ScalableHybridListEntry scalableHybridListEntry;
				using (this.m_entries.GetAndPin(this.m_firstFree, out scalableHybridListEntry))
				{
					this.m_firstFree = scalableHybridListEntry.Next;
					this.SetupLastNode(scalableHybridListEntry, item);
					goto IL_006C;
				}
			}
			num = this.m_entries.Count;
			ScalableHybridListEntry scalableHybridListEntry2 = new ScalableHybridListEntry();
			this.SetupLastNode(scalableHybridListEntry2, item);
			this.m_entries.Add(scalableHybridListEntry2);
			IL_006C:
			if (this.m_count == 0)
			{
				this.m_first = num;
			}
			else
			{
				ScalableHybridListEntry scalableHybridListEntry3;
				using (this.m_entries.GetAndPin(this.m_last, out scalableHybridListEntry3))
				{
					scalableHybridListEntry3.Next = num;
				}
			}
			this.m_last = num;
			this.m_count++;
			return num;
		}

		// Token: 0x0600787A RID: 30842 RVA: 0x001F06B4 File Offset: 0x001EE8B4
		internal void Remove(int index)
		{
			ScalableHybridListEntry scalableHybridListEntry;
			using (this.m_entries.GetAndPin(index, out scalableHybridListEntry))
			{
				this.CheckNonFreeEntry(scalableHybridListEntry, index);
				if (scalableHybridListEntry.Previous == -1)
				{
					this.m_first = scalableHybridListEntry.Next;
				}
				else
				{
					ScalableHybridListEntry scalableHybridListEntry2;
					using (this.m_entries.GetAndPin(scalableHybridListEntry.Previous, out scalableHybridListEntry2))
					{
						scalableHybridListEntry2.Next = scalableHybridListEntry.Next;
					}
				}
				if (scalableHybridListEntry.Next == -1)
				{
					this.m_last = scalableHybridListEntry.Previous;
				}
				else
				{
					ScalableHybridListEntry scalableHybridListEntry3;
					using (this.m_entries.GetAndPin(scalableHybridListEntry.Next, out scalableHybridListEntry3))
					{
						scalableHybridListEntry3.Previous = scalableHybridListEntry.Previous;
					}
				}
				scalableHybridListEntry.Next = this.m_firstFree;
				this.m_firstFree = index;
				scalableHybridListEntry.Item = null;
				scalableHybridListEntry.Previous = -1;
				this.m_count--;
			}
		}

		// Token: 0x17002804 RID: 10244
		// (get) Token: 0x0600787B RID: 30843 RVA: 0x001F07C4 File Offset: 0x001EE9C4
		internal int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x17002805 RID: 10245
		internal T this[int index]
		{
			get
			{
				return (T)((object)this.GetAndCheckEntry(index).Item);
			}
		}

		// Token: 0x17002806 RID: 10246
		// (get) Token: 0x0600787D RID: 30845 RVA: 0x001F07DF File Offset: 0x001EE9DF
		internal int First
		{
			get
			{
				return this.m_first;
			}
		}

		// Token: 0x17002807 RID: 10247
		// (get) Token: 0x0600787E RID: 30846 RVA: 0x001F07E7 File Offset: 0x001EE9E7
		internal int Last
		{
			get
			{
				return this.m_last;
			}
		}

		// Token: 0x0600787F RID: 30847 RVA: 0x001F07EF File Offset: 0x001EE9EF
		internal int Next(int index)
		{
			return this.GetAndCheckEntry(index).Next;
		}

		// Token: 0x06007880 RID: 30848 RVA: 0x001F07FD File Offset: 0x001EE9FD
		internal int Previous(int index)
		{
			return this.GetAndCheckEntry(index).Previous;
		}

		// Token: 0x06007881 RID: 30849 RVA: 0x001F080B File Offset: 0x001EEA0B
		public void Dispose()
		{
			this.Clear();
		}

		// Token: 0x06007882 RID: 30850 RVA: 0x001F0813 File Offset: 0x001EEA13
		internal void Clear()
		{
			this.m_entries.Clear();
			this.m_count = 0;
			this.m_first = -1;
			this.m_last = -1;
			this.m_firstFree = -1;
		}

		// Token: 0x06007883 RID: 30851 RVA: 0x001F083C File Offset: 0x001EEA3C
		public IEnumerator<T> GetEnumerator()
		{
			return new ScalableHybridList<T>.HybridListEnumerator(this);
		}

		// Token: 0x06007884 RID: 30852 RVA: 0x001F0844 File Offset: 0x001EEA44
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06007885 RID: 30853 RVA: 0x001F084C File Offset: 0x001EEA4C
		private void SetupLastNode(ScalableHybridListEntry entry, T item)
		{
			entry.Item = item;
			entry.Next = -1;
			entry.Previous = this.m_last;
		}

		// Token: 0x06007886 RID: 30854 RVA: 0x001F0870 File Offset: 0x001EEA70
		private ScalableHybridListEntry GetAndCheckEntry(int index)
		{
			ScalableHybridListEntry scalableHybridListEntry = this.m_entries[index];
			this.CheckNonFreeEntry(scalableHybridListEntry, index);
			return scalableHybridListEntry;
		}

		// Token: 0x06007887 RID: 30855 RVA: 0x001F0893 File Offset: 0x001EEA93
		private void CheckNonFreeEntry(ScalableHybridListEntry entry, int index)
		{
			if (entry.Previous == -1 && index != this.m_first)
			{
				Global.Tracer.Assert(false, "Cannot use index: {0}. It points to a free item", new object[] { index });
			}
		}

		// Token: 0x04003C9B RID: 15515
		private int m_count;

		// Token: 0x04003C9C RID: 15516
		private ScalableList<ScalableHybridListEntry> m_entries;

		// Token: 0x04003C9D RID: 15517
		private int m_first = -1;

		// Token: 0x04003C9E RID: 15518
		private int m_last = -1;

		// Token: 0x04003C9F RID: 15519
		private int m_firstFree = -1;

		// Token: 0x04003CA0 RID: 15520
		private int m_version;

		// Token: 0x04003CA1 RID: 15521
		internal const int InvalidIndex = -1;

		// Token: 0x02000D0F RID: 3343
		private sealed class HybridListEnumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06008EB9 RID: 36537 RVA: 0x00245B05 File Offset: 0x00243D05
			internal HybridListEnumerator(ScalableHybridList<T> list)
			{
				this.m_list = list;
				this.m_version = this.m_list.m_version;
			}

			// Token: 0x17002BC5 RID: 11205
			// (get) Token: 0x06008EBA RID: 36538 RVA: 0x00245B2C File Offset: 0x00243D2C
			public T Current
			{
				get
				{
					return this.m_list[this.m_currentIndex];
				}
			}

			// Token: 0x06008EBB RID: 36539 RVA: 0x00245B3F File Offset: 0x00243D3F
			public void Dispose()
			{
			}

			// Token: 0x17002BC6 RID: 11206
			// (get) Token: 0x06008EBC RID: 36540 RVA: 0x00245B41 File Offset: 0x00243D41
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06008EBD RID: 36541 RVA: 0x00245B50 File Offset: 0x00243D50
			public bool MoveNext()
			{
				if (this.m_version != this.m_list.m_version)
				{
					Global.Tracer.Assert(false, "Cannot continue enumeration, backing list was modified");
				}
				if (this.m_afterLast)
				{
					return false;
				}
				if (this.m_currentIndex == -1)
				{
					this.m_currentIndex = this.m_list.First;
				}
				else
				{
					this.m_currentIndex = this.m_list.Next(this.m_currentIndex);
				}
				if (this.m_currentIndex == -1)
				{
					this.m_afterLast = true;
				}
				return !this.m_afterLast;
			}

			// Token: 0x06008EBE RID: 36542 RVA: 0x00245BD6 File Offset: 0x00243DD6
			public void Reset()
			{
				this.m_currentIndex = -1;
				this.m_afterLast = false;
			}

			// Token: 0x04005039 RID: 20537
			private ScalableHybridList<T> m_list;

			// Token: 0x0400503A RID: 20538
			private int m_currentIndex = -1;

			// Token: 0x0400503B RID: 20539
			private bool m_afterLast;

			// Token: 0x0400503C RID: 20540
			private int m_version;
		}
	}
}
