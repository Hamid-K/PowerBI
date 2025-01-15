using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.ApplicationInsights.Metrics.ConcurrentDatastructures
{
	// Token: 0x02000035 RID: 53
	internal class GrowingCollection<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x060001EB RID: 491 RVA: 0x0000A96D File Offset: 0x00008B6D
		public GrowingCollection()
		{
			this.dataHead = new GrowingCollection<T>.Segment(null);
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000A981 File Offset: 0x00008B81
		public int Count
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Volatile.Read<GrowingCollection<T>.Segment>(ref this.dataHead).GlobalCount;
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000A994 File Offset: 0x00008B94
		public void Add(T item)
		{
			GrowingCollection<T>.Segment segment = Volatile.Read<GrowingCollection<T>.Segment>(ref this.dataHead);
			bool flag = segment.TryAdd(item);
			while (!flag)
			{
				GrowingCollection<T>.Segment segment2 = new GrowingCollection<T>.Segment(segment);
				GrowingCollection<T>.Segment segment3 = Interlocked.CompareExchange<GrowingCollection<T>.Segment>(ref this.dataHead, segment2, segment);
				flag = ((segment3 == segment) ? segment2 : segment3).TryAdd(item);
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000A9DE File Offset: 0x00008BDE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public GrowingCollection<T>.Enumerator GetEnumerator()
		{
			return new GrowingCollection<T>.Enumerator(this.dataHead);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000A9EB File Offset: 0x00008BEB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000A9F3 File Offset: 0x00008BF3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040000E2 RID: 226
		private const int SegmentSize = 32;

		// Token: 0x040000E3 RID: 227
		private GrowingCollection<T>.Segment dataHead;

		// Token: 0x020000F5 RID: 245
		public class Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x060008A4 RID: 2212 RVA: 0x0001BA74 File Offset: 0x00019C74
			internal Enumerator(GrowingCollection<T>.Segment head)
			{
				Util.ValidateNotNull(head, "head");
				this.currentSegment = head;
				this.head = head;
				this.headOffset = (this.currentSegmentOffset = head.LocalCount);
				this.count = this.headOffset + ((this.head.NextSegment == null) ? 0 : this.head.NextSegment.GlobalCount);
			}

			// Token: 0x17000279 RID: 633
			// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0001BAE4 File Offset: 0x00019CE4
			public int Count
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.count;
				}
			}

			// Token: 0x1700027A RID: 634
			// (get) Token: 0x060008A6 RID: 2214 RVA: 0x0001BAEC File Offset: 0x00019CEC
			public T Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.currentSegment[this.currentSegmentOffset];
				}
			}

			// Token: 0x1700027B RID: 635
			// (get) Token: 0x060008A7 RID: 2215 RVA: 0x0001BAFF File Offset: 0x00019CFF
			object IEnumerator.Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060008A8 RID: 2216 RVA: 0x0001BB0C File Offset: 0x00019D0C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Dispose()
			{
				GrowingCollection<T>.Enumerator.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x060008A9 RID: 2217 RVA: 0x0001BB1C File Offset: 0x00019D1C
			public bool MoveNext()
			{
				if (this.currentSegmentOffset != 0)
				{
					this.currentSegmentOffset--;
					return true;
				}
				if (this.currentSegment.NextSegment == null)
				{
					return false;
				}
				this.currentSegment = this.currentSegment.NextSegment;
				this.currentSegmentOffset = this.currentSegment.LocalCount - 1;
				return true;
			}

			// Token: 0x060008AA RID: 2218 RVA: 0x0001BB75 File Offset: 0x00019D75
			public void Reset()
			{
				this.currentSegment = this.head;
				this.currentSegmentOffset = this.headOffset;
			}

			// Token: 0x060008AB RID: 2219 RVA: 0x0001BB8F File Offset: 0x00019D8F
			private static void Dispose(bool disposing)
			{
			}

			// Token: 0x04000362 RID: 866
			private readonly GrowingCollection<T>.Segment head;

			// Token: 0x04000363 RID: 867
			private readonly int headOffset;

			// Token: 0x04000364 RID: 868
			private readonly int count;

			// Token: 0x04000365 RID: 869
			private GrowingCollection<T>.Segment currentSegment;

			// Token: 0x04000366 RID: 870
			private int currentSegmentOffset;
		}

		// Token: 0x020000F6 RID: 246
		internal class Segment
		{
			// Token: 0x060008AC RID: 2220 RVA: 0x0001BB93 File Offset: 0x00019D93
			public Segment(GrowingCollection<T>.Segment nextSegment)
			{
				this.nextSegment = nextSegment;
				this.nextSegmentGlobalCount = ((nextSegment == null) ? 0 : nextSegment.GlobalCount);
			}

			// Token: 0x1700027C RID: 636
			// (get) Token: 0x060008AD RID: 2221 RVA: 0x0001BBC4 File Offset: 0x00019DC4
			public int LocalCount
			{
				get
				{
					int num = Volatile.Read(ref this.localCount);
					if (num > 32)
					{
						return 32;
					}
					return num;
				}
			}

			// Token: 0x1700027D RID: 637
			// (get) Token: 0x060008AE RID: 2222 RVA: 0x0001BBE6 File Offset: 0x00019DE6
			public GrowingCollection<T>.Segment NextSegment
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.nextSegment;
				}
			}

			// Token: 0x1700027E RID: 638
			// (get) Token: 0x060008AF RID: 2223 RVA: 0x0001BBEE File Offset: 0x00019DEE
			public int GlobalCount
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.LocalCount + this.nextSegmentGlobalCount;
				}
			}

			// Token: 0x1700027F RID: 639
			public T this[int index]
			{
				get
				{
					if (index < 0 || this.localCount <= index || 32 <= index)
					{
						throw new ArgumentOutOfRangeException("index", global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("Invalid index ({0})", new object[] { index })));
					}
					return this.data[index];
				}
			}

			// Token: 0x060008B1 RID: 2225 RVA: 0x0001BC54 File Offset: 0x00019E54
			internal bool TryAdd(T item)
			{
				int num = Interlocked.Increment(ref this.localCount) - 1;
				if (num >= 32)
				{
					Interlocked.Decrement(ref this.localCount);
					return false;
				}
				this.data[num] = item;
				return true;
			}

			// Token: 0x04000367 RID: 871
			private readonly GrowingCollection<T>.Segment nextSegment;

			// Token: 0x04000368 RID: 872
			private readonly int nextSegmentGlobalCount;

			// Token: 0x04000369 RID: 873
			private readonly T[] data = new T[32];

			// Token: 0x0400036A RID: 874
			private int localCount;
		}
	}
}
