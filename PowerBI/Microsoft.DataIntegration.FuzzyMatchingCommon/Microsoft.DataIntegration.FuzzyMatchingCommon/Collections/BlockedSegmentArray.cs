using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x0200009B RID: 155
	[Serializable]
	public class BlockedSegmentArray<T> : IMemoryUsage, ISegmentAllocator<T>
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x000238C6 File Offset: 0x00021AC6
		// (set) Token: 0x0600069B RID: 1691 RVA: 0x000238CE File Offset: 0x00021ACE
		public int SegmentLength { get; private set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x000238D7 File Offset: 0x00021AD7
		// (set) Token: 0x0600069D RID: 1693 RVA: 0x000238DF File Offset: 0x00021ADF
		public int SegmentsPerBlock { get; private set; }

		// Token: 0x0600069E RID: 1694 RVA: 0x000238E8 File Offset: 0x00021AE8
		public BlockedSegmentArray()
			: this(1)
		{
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x000238F1 File Offset: 0x00021AF1
		public BlockedSegmentArray(int segmentLength)
			: this(segmentLength, 84000 / (segmentLength * Utilities.SizeOf(typeof(T))))
		{
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00023911 File Offset: 0x00021B11
		public BlockedSegmentArray(int segmentLength, int segmentsPerBlock)
		{
			this.SegmentLength = segmentLength;
			this.SegmentsPerBlock = segmentsPerBlock;
			this.m_blocks.Add(new T[this.SegmentsPerBlock * this.SegmentLength]);
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0002394F File Offset: 0x00021B4F
		public int Length
		{
			get
			{
				return this.m_nextSegment;
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00023957 File Offset: 0x00021B57
		public void Reset()
		{
			this.m_nextSegment = 0;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00023960 File Offset: 0x00021B60
		public ArraySegment<T> Add()
		{
			if (this.m_nextSegment / this.SegmentsPerBlock == this.m_blocks.Count)
			{
				this.m_blocks.Add(new T[this.SegmentsPerBlock * this.SegmentLength]);
			}
			int nextSegment = this.m_nextSegment;
			this.m_nextSegment = nextSegment + 1;
			return this[nextSegment];
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x000239BC File Offset: 0x00021BBC
		public void Resize(ref ArraySegment<T> segment, int newLength)
		{
			if (newLength <= segment.Count)
			{
				segment = new ArraySegment<T>(segment.Array, segment.Offset, newLength);
				return;
			}
			ArraySegment<T> arraySegment = this.New(newLength);
			if (segment.Count > 0)
			{
				Array.ConstrainedCopy(arraySegment.Array, arraySegment.Offset, segment.Array, segment.Offset, segment.Count);
			}
			segment = arraySegment;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00023A28 File Offset: 0x00021C28
		public ArraySegment<T> New(int numSegments)
		{
			if (numSegments > this.SegmentsPerBlock)
			{
				throw new ArgumentOutOfRangeException(string.Format("Number of requested segments {0} can be at most {1}.", numSegments, this.SegmentsPerBlock));
			}
			if (numSegments == 0)
			{
				return new ArraySegment<T>(BlockedSegmentArray<T>.EmptyArray, 0, 0);
			}
			int num = this.m_nextSegment % this.SegmentsPerBlock + numSegments;
			if (num >= this.SegmentsPerBlock)
			{
				int num2 = this.m_nextSegment / this.SegmentsPerBlock + 1;
				if (num2 == this.m_blocks.Count)
				{
					this.m_blocks.Add(new T[this.SegmentsPerBlock * this.SegmentLength]);
				}
				if (num > this.SegmentsPerBlock)
				{
					this.m_nextSegment = num2 * this.SegmentsPerBlock;
				}
			}
			int nextSegment = this.m_nextSegment;
			this.m_nextSegment += numSegments;
			return new ArraySegment<T>(this.m_blocks[nextSegment / this.SegmentsPerBlock], nextSegment % this.SegmentsPerBlock * this.SegmentLength, numSegments * this.SegmentLength);
		}

		// Token: 0x1700010E RID: 270
		public ArraySegment<T> this[int i]
		{
			get
			{
				return new ArraySegment<T>(this.m_blocks[i / this.SegmentsPerBlock], i % this.SegmentsPerBlock * this.SegmentLength, this.SegmentLength);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x00023B4D File Offset: 0x00021D4D
		public long MemoryUsage
		{
			get
			{
				return (long)(this.m_blocks.Count * this.SegmentsPerBlock * this.SegmentLength);
			}
		}

		// Token: 0x0400014A RID: 330
		private static readonly T[] EmptyArray = new T[0];

		// Token: 0x0400014D RID: 333
		public List<T[]> m_blocks = new List<T[]>();

		// Token: 0x0400014E RID: 334
		private int m_nextSegment;
	}
}
