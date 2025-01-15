using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x020008A0 RID: 2208
	internal sealed class DynamicBucketedHeapSpaceManager : ISpaceManager
	{
		// Token: 0x06007900 RID: 30976 RVA: 0x001F2B51 File Offset: 0x001F0D51
		internal DynamicBucketedHeapSpaceManager()
			: this(50, 10, 2500, 35)
		{
		}

		// Token: 0x06007901 RID: 30977 RVA: 0x001F2B64 File Offset: 0x001F0D64
		internal DynamicBucketedHeapSpaceManager(int splitThreshold, int maxBucketCount, int maxSpacesPerBucket, int minTrackedSizeBytes)
		{
			this.m_bucketSplitThreshold = splitThreshold;
			this.m_maxBucketCount = maxBucketCount;
			this.m_maxSpacesPerBucket = maxSpacesPerBucket;
			this.m_minimumTrackedSize = minTrackedSizeBytes;
			this.m_buckets = new SortedBucket[this.m_maxBucketCount];
			SortedBucket sortedBucket = new SortedBucket(this.m_maxSpacesPerBucket);
			sortedBucket.Limit = 0;
			this.m_buckets[0] = sortedBucket;
			this.m_bucketCount++;
		}

		// Token: 0x06007902 RID: 30978 RVA: 0x001F2BD6 File Offset: 0x001F0DD6
		public void Seek(long offset, SeekOrigin origin)
		{
		}

		// Token: 0x06007903 RID: 30979 RVA: 0x001F2BD8 File Offset: 0x001F0DD8
		public void Free(long offset, long size)
		{
			Space space = new Space(offset, size);
			this.InsertSpace(space);
		}

		// Token: 0x06007904 RID: 30980 RVA: 0x001F2BF8 File Offset: 0x001F0DF8
		private void InsertSpace(Space space)
		{
			if (space.Size < (long)this.m_minimumTrackedSize)
			{
				this.m_unuseableBytes += space.Size;
				return;
			}
			int bucketIndex = this.GetBucketIndex(space.Size);
			SortedBucket sortedBucket = this.m_buckets[bucketIndex];
			if (sortedBucket.Count != this.m_maxSpacesPerBucket)
			{
				sortedBucket.Insert(space);
				return;
			}
			if (this.m_bucketCount < this.m_maxBucketCount && sortedBucket.Maximum - sortedBucket.Minimum > this.m_bucketSplitThreshold)
			{
				SortedBucket sortedBucket2 = sortedBucket.Split(this.m_maxSpacesPerBucket);
				for (int i = this.m_bucketCount; i > bucketIndex + 1; i--)
				{
					this.m_buckets[i] = this.m_buckets[i - 1];
				}
				this.m_buckets[bucketIndex + 1] = sortedBucket2;
				this.m_bucketCount++;
				this.InsertSpace(space);
				return;
			}
			if (sortedBucket.Peek().Size < space.Size)
			{
				Space space2 = sortedBucket.ExtractMax();
				this.m_unuseableBytes += space2.Size;
				sortedBucket.Insert(space);
				return;
			}
			this.m_unuseableBytes += space.Size;
		}

		// Token: 0x06007905 RID: 30981 RVA: 0x001F2D18 File Offset: 0x001F0F18
		private int GetBucketIndex(long size)
		{
			for (int i = 1; i < this.m_bucketCount; i++)
			{
				if ((long)this.m_buckets[i].Limit > size)
				{
					return i - 1;
				}
			}
			return this.m_bucketCount - 1;
		}

		// Token: 0x06007906 RID: 30982 RVA: 0x001F2D54 File Offset: 0x001F0F54
		public long AllocateSpace(long size)
		{
			long num = -1L;
			int num2 = this.GetBucketIndex(size);
			while (num2 < this.m_bucketCount && num == -1L)
			{
				SortedBucket sortedBucket = this.m_buckets[num2];
				if (sortedBucket.Count > 0)
				{
					Space space = sortedBucket.Peek();
					if (space.Size >= size)
					{
						sortedBucket.ExtractMax();
						num = space.Offset;
						space.Offset += size;
						space.Size -= size;
						if (space.Size > 0L)
						{
							this.InsertSpace(space);
						}
						if (sortedBucket.Count == 0 && num2 != 0)
						{
							Array.Copy(this.m_buckets, num2 + 1, this.m_buckets, num2, this.m_bucketCount - num2 - 1);
							this.m_bucketCount--;
						}
					}
				}
				num2++;
			}
			if (num == -1L && this.m_allowEndAllocation)
			{
				num = this.m_end;
				this.m_end += size;
			}
			return num;
		}

		// Token: 0x06007907 RID: 30983 RVA: 0x001F2E3A File Offset: 0x001F103A
		public long Resize(long offset, long oldSize, long newSize)
		{
			this.Free(offset, oldSize);
			return this.AllocateSpace(newSize);
		}

		// Token: 0x1700281D RID: 10269
		// (get) Token: 0x06007908 RID: 30984 RVA: 0x001F2E4B File Offset: 0x001F104B
		// (set) Token: 0x06007909 RID: 30985 RVA: 0x001F2E53 File Offset: 0x001F1053
		public long StreamEnd
		{
			get
			{
				return this.m_end;
			}
			set
			{
				this.m_end = value;
			}
		}

		// Token: 0x1700281E RID: 10270
		// (get) Token: 0x0600790A RID: 30986 RVA: 0x001F2E5C File Offset: 0x001F105C
		// (set) Token: 0x0600790B RID: 30987 RVA: 0x001F2E64 File Offset: 0x001F1064
		public bool AllowEndAllocation
		{
			get
			{
				return this.m_allowEndAllocation;
			}
			set
			{
				this.m_allowEndAllocation = value;
			}
		}

		// Token: 0x1700281F RID: 10271
		// (get) Token: 0x0600790C RID: 30988 RVA: 0x001F2E6D File Offset: 0x001F106D
		public long UnuseableBytes
		{
			get
			{
				return this.m_unuseableBytes;
			}
		}

		// Token: 0x17002820 RID: 10272
		// (get) Token: 0x0600790D RID: 30989 RVA: 0x001F2E75 File Offset: 0x001F1075
		internal SortedBucket[] Buckets
		{
			get
			{
				return this.m_buckets;
			}
		}

		// Token: 0x17002821 RID: 10273
		// (get) Token: 0x0600790E RID: 30990 RVA: 0x001F2E7D File Offset: 0x001F107D
		internal int BucketCount
		{
			get
			{
				return this.m_bucketCount;
			}
		}

		// Token: 0x0600790F RID: 30991 RVA: 0x001F2E88 File Offset: 0x001F1088
		public void TraceStats()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.m_bucketCount; i++)
			{
				SortedBucket sortedBucket = this.m_buckets[i];
				stringBuilder.AppendFormat("\r\n\t\tBucket: Limit: {0} Count: {1}", sortedBucket.Limit, sortedBucket.Count);
			}
			Global.Tracer.Trace(TraceLevel.Verbose, "DynamicBucketedHeapSpaceManager Stats. StreamSize: {0} MB. UnusableSpace: {1} KB. \r\n\tBucketInfo: {2}", new object[]
			{
				this.m_end / 1048576L,
				this.m_unuseableBytes / 1024L,
				stringBuilder.ToString()
			});
		}

		// Token: 0x04003CC0 RID: 15552
		private bool m_allowEndAllocation = true;

		// Token: 0x04003CC1 RID: 15553
		private SortedBucket[] m_buckets;

		// Token: 0x04003CC2 RID: 15554
		private int m_bucketCount;

		// Token: 0x04003CC3 RID: 15555
		private long m_end;

		// Token: 0x04003CC4 RID: 15556
		private long m_unuseableBytes;

		// Token: 0x04003CC5 RID: 15557
		private int m_maxSpacesPerBucket;

		// Token: 0x04003CC6 RID: 15558
		private int m_maxBucketCount;

		// Token: 0x04003CC7 RID: 15559
		private int m_bucketSplitThreshold;

		// Token: 0x04003CC8 RID: 15560
		private int m_minimumTrackedSize;

		// Token: 0x04003CC9 RID: 15561
		private const int DefaultSpacesPerBucket = 2500;

		// Token: 0x04003CCA RID: 15562
		private const int DefaultMaxBucketCount = 10;

		// Token: 0x04003CCB RID: 15563
		private const int DefaultBucketSplitThreshold = 50;

		// Token: 0x04003CCC RID: 15564
		private const int DefaultMinimumTrackedSize = 35;
	}
}
