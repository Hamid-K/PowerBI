using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Threading.DotNet4;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent.DotNet4
{
	// Token: 0x020000BE RID: 190
	[ComVisible(false)]
	[DebuggerDisplay("Count = {Count}")]
	[HostProtection(6, Synchronization = true, ExternalThreading = true)]
	[Serializable]
	public class ConcurrentQueue<T> : IProducerConsumerCollection<T>, IEnumerable<T>, IEnumerable, ICollection
	{
		// Token: 0x06000832 RID: 2098 RVA: 0x0002B4E0 File Offset: 0x000296E0
		public ConcurrentQueue()
		{
			this.m_head = (this.m_tail = new ConcurrentQueue<T>.Segment(0L));
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0002B510 File Offset: 0x00029710
		private void InitializeFromCollection(IEnumerable<T> collection)
		{
			this.m_head = (this.m_tail = new ConcurrentQueue<T>.Segment(0L));
			int num = 0;
			foreach (T t in collection)
			{
				this.m_tail.UnsafeAdd(t);
				num++;
				if (num >= 32)
				{
					this.m_tail = this.m_tail.UnsafeGrow();
					num = 0;
				}
			}
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0002B59C File Offset: 0x0002979C
		public ConcurrentQueue(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.InitializeFromCollection(collection);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0002B5B9 File Offset: 0x000297B9
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			this.m_serializationArray = this.ToArray();
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0002B5C7 File Offset: 0x000297C7
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			this.InitializeFromCollection(this.m_serializationArray);
			this.m_serializationArray = null;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0002B5DC File Offset: 0x000297DC
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			this.ToList().CopyTo(array, index);
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x0002B5F9 File Offset: 0x000297F9
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x0002B5FC File Offset: 0x000297FC
		object ICollection.SyncRoot
		{
			get
			{
				throw new NotSupportedException("ConcurrentCollection_SyncRoot_NotSupported");
			}
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0002B608 File Offset: 0x00029808
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0002B610 File Offset: 0x00029810
		bool IProducerConsumerCollection<T>.TryAdd(T item)
		{
			this.Enqueue(item);
			return true;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0002B61A File Offset: 0x0002981A
		bool IProducerConsumerCollection<T>.TryTake(out T item)
		{
			return this.TryDequeue(out item);
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x0002B624 File Offset: 0x00029824
		public bool IsEmpty
		{
			get
			{
				ConcurrentQueue<T>.Segment segment = this.m_head;
				if (!segment.IsEmpty)
				{
					return false;
				}
				if (segment.Next == null)
				{
					return true;
				}
				SpinWait spinWait = default(SpinWait);
				while (segment.IsEmpty)
				{
					if (segment.Next == null)
					{
						return true;
					}
					spinWait.SpinOnce();
					segment = this.m_head;
				}
				return false;
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0002B67B File Offset: 0x0002987B
		public T[] ToArray()
		{
			return this.ToList().ToArray();
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0002B688 File Offset: 0x00029888
		private List<T> ToList()
		{
			ConcurrentQueue<T>.Segment segment;
			ConcurrentQueue<T>.Segment segment2;
			int num;
			int num2;
			this.GetHeadTailPositions(out segment, out segment2, out num, out num2);
			if (segment == segment2)
			{
				return segment.ToList(num, num2);
			}
			List<T> list = new List<T>(segment.ToList(num, 31));
			for (ConcurrentQueue<T>.Segment segment3 = segment.Next; segment3 != segment2; segment3 = segment3.Next)
			{
				list.AddRange(segment3.ToList(0, 31));
			}
			list.AddRange(segment2.ToList(0, num2));
			return list;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0002B6FC File Offset: 0x000298FC
		private void GetHeadTailPositions(out ConcurrentQueue<T>.Segment head, out ConcurrentQueue<T>.Segment tail, out int headLow, out int tailHigh)
		{
			head = this.m_head;
			tail = this.m_tail;
			headLow = head.Low;
			tailHigh = tail.High;
			SpinWait spinWait = default(SpinWait);
			while (head != this.m_head || tail != this.m_tail || headLow != head.Low || tailHigh != tail.High || head.m_index > tail.m_index)
			{
				spinWait.SpinOnce();
				head = this.m_head;
				tail = this.m_tail;
				headLow = head.Low;
				tailHigh = tail.High;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x0002B7A8 File Offset: 0x000299A8
		public int Count
		{
			get
			{
				ConcurrentQueue<T>.Segment segment;
				ConcurrentQueue<T>.Segment segment2;
				int num;
				int num2;
				this.GetHeadTailPositions(out segment, out segment2, out num, out num2);
				if (segment == segment2)
				{
					return num2 - num + 1;
				}
				return 32 - num + 32 * (int)(segment2.m_index - segment.m_index - 1L) + (num2 + 1);
			}
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0002B7EA File Offset: 0x000299EA
		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			this.ToList().CopyTo(array, index);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0002B807 File Offset: 0x00029A07
		public IEnumerator<T> GetEnumerator()
		{
			return this.ToList().GetEnumerator();
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0002B81C File Offset: 0x00029A1C
		public void Enqueue(T item)
		{
			SpinWait spinWait = default(SpinWait);
			while (!this.m_tail.TryAppend(item, ref this.m_tail))
			{
				spinWait.SpinOnce();
			}
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0002B850 File Offset: 0x00029A50
		public bool TryDequeue(out T result)
		{
			while (!this.IsEmpty)
			{
				if (this.m_head.TryRemove(out result, ref this.m_head))
				{
					return true;
				}
			}
			result = default(T);
			return false;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0002B87C File Offset: 0x00029A7C
		public bool TryPeek(out T result)
		{
			while (!this.IsEmpty)
			{
				if (this.m_head.TryPeek(out result))
				{
					return true;
				}
			}
			result = default(T);
			return false;
		}

		// Token: 0x040001AA RID: 426
		[NonSerialized]
		private volatile ConcurrentQueue<T>.Segment m_head;

		// Token: 0x040001AB RID: 427
		[NonSerialized]
		private volatile ConcurrentQueue<T>.Segment m_tail;

		// Token: 0x040001AC RID: 428
		private T[] m_serializationArray;

		// Token: 0x040001AD RID: 429
		private const int SEGMENT_SIZE = 32;

		// Token: 0x02000153 RID: 339
		private class Segment
		{
			// Token: 0x06000AA5 RID: 2725 RVA: 0x0002FD54 File Offset: 0x0002DF54
			internal Segment(long index)
			{
				this.m_array = new T[32];
				this.m_state = new int[32];
				this.m_high = -1;
				this.m_index = index;
			}

			// Token: 0x170001C8 RID: 456
			// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x0002FD8A File Offset: 0x0002DF8A
			internal ConcurrentQueue<T>.Segment Next
			{
				get
				{
					return this.m_next;
				}
			}

			// Token: 0x170001C9 RID: 457
			// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x0002FD94 File Offset: 0x0002DF94
			internal bool IsEmpty
			{
				get
				{
					return this.Low > this.High;
				}
			}

			// Token: 0x06000AA8 RID: 2728 RVA: 0x0002FDA4 File Offset: 0x0002DFA4
			internal void UnsafeAdd(T value)
			{
				this.m_high++;
				this.m_array[this.m_high] = value;
				this.m_state[this.m_high] = 1;
			}

			// Token: 0x06000AA9 RID: 2729 RVA: 0x0002FDE0 File Offset: 0x0002DFE0
			internal ConcurrentQueue<T>.Segment UnsafeGrow()
			{
				ConcurrentQueue<T>.Segment segment = new ConcurrentQueue<T>.Segment(this.m_index + 1L);
				this.m_next = segment;
				return segment;
			}

			// Token: 0x06000AAA RID: 2730 RVA: 0x0002FE08 File Offset: 0x0002E008
			internal void Grow(ref ConcurrentQueue<T>.Segment tail)
			{
				ConcurrentQueue<T>.Segment segment = new ConcurrentQueue<T>.Segment(this.m_index + 1L);
				this.m_next = segment;
				tail = this.m_next;
			}

			// Token: 0x06000AAB RID: 2731 RVA: 0x0002FE38 File Offset: 0x0002E038
			internal bool TryAppend(T value, ref ConcurrentQueue<T>.Segment tail)
			{
				if (this.m_high >= 31)
				{
					return false;
				}
				int num = 32;
				try
				{
				}
				finally
				{
					num = Interlocked.Increment(ref this.m_high);
					if (num <= 31)
					{
						this.m_array[num] = value;
						this.m_state[num] = 1;
					}
					if (num == 31)
					{
						this.Grow(ref tail);
					}
				}
				return num <= 31;
			}

			// Token: 0x06000AAC RID: 2732 RVA: 0x0002FEA8 File Offset: 0x0002E0A8
			internal bool TryRemove(out T result, ref ConcurrentQueue<T>.Segment head)
			{
				SpinWait spinWait = default(SpinWait);
				int i = this.Low;
				int num = this.High;
				while (i <= num)
				{
					if (Interlocked.CompareExchange(ref this.m_low, i + 1, i) == i)
					{
						SpinWait spinWait2 = default(SpinWait);
						while (this.m_state[i] == 0)
						{
							spinWait2.SpinOnce();
						}
						result = this.m_array[i];
						if (i + 1 >= 32)
						{
							spinWait2 = default(SpinWait);
							while (this.m_next == null)
							{
								spinWait2.SpinOnce();
							}
							head = this.m_next;
						}
						return true;
					}
					spinWait.SpinOnce();
					i = this.Low;
					num = this.High;
				}
				result = default(T);
				return false;
			}

			// Token: 0x06000AAD RID: 2733 RVA: 0x0002FF68 File Offset: 0x0002E168
			internal bool TryPeek(out T result)
			{
				result = default(T);
				int low = this.Low;
				if (low > this.High)
				{
					return false;
				}
				SpinWait spinWait = default(SpinWait);
				while (this.m_state[low] == 0)
				{
					spinWait.SpinOnce();
				}
				result = this.m_array[low];
				return true;
			}

			// Token: 0x06000AAE RID: 2734 RVA: 0x0002FFC0 File Offset: 0x0002E1C0
			internal List<T> ToList(int start, int end)
			{
				List<T> list = new List<T>();
				for (int i = start; i <= end; i++)
				{
					SpinWait spinWait = default(SpinWait);
					while (this.m_state[i] == 0)
					{
						spinWait.SpinOnce();
					}
					list.Add(this.m_array[i]);
				}
				return list;
			}

			// Token: 0x170001CA RID: 458
			// (get) Token: 0x06000AAF RID: 2735 RVA: 0x00030011 File Offset: 0x0002E211
			internal int Low
			{
				get
				{
					return Math.Min(this.m_low, 32);
				}
			}

			// Token: 0x170001CB RID: 459
			// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x00030022 File Offset: 0x0002E222
			internal int High
			{
				get
				{
					return Math.Min(this.m_high, 31);
				}
			}

			// Token: 0x04000396 RID: 918
			internal volatile T[] m_array;

			// Token: 0x04000397 RID: 919
			private volatile int[] m_state;

			// Token: 0x04000398 RID: 920
			private volatile ConcurrentQueue<T>.Segment m_next;

			// Token: 0x04000399 RID: 921
			internal readonly long m_index;

			// Token: 0x0400039A RID: 922
			private volatile int m_low;

			// Token: 0x0400039B RID: 923
			private volatile int m_high;
		}
	}
}
