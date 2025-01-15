using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002D4 RID: 724
	internal sealed class SegmentList : IEnumerable<SegmentedChunkStorage.SegmentData>, IEnumerable
	{
		// Token: 0x060019EE RID: 6638 RVA: 0x000689F0 File Offset: 0x00066BF0
		public SegmentedChunkStorage.SegmentData MapPositionToSegment(long absolutePosition, out int startingIndex)
		{
			object syncRoot = this.SyncRoot;
			SegmentedChunkStorage.SegmentData segmentData2;
			lock (syncRoot)
			{
				startingIndex = 0;
				int segmentIndex = this.GetSegmentIndex(absolutePosition, out startingIndex);
				SegmentedChunkStorage.SegmentData segmentData;
				if (segmentIndex >= this.m_segmentIds.Count)
				{
					segmentData = null;
				}
				else
				{
					segmentData = this.m_segmentIds[segmentIndex];
				}
				segmentData2 = segmentData;
			}
			return segmentData2;
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x00068A5C File Offset: 0x00066C5C
		public void AddSegmentData(SegmentedChunkStorage.SegmentData segmentData)
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.m_segmentIds.Add(segmentData);
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x060019F0 RID: 6640 RVA: 0x00068AA4 File Offset: 0x00066CA4
		public object SyncRoot
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_syncRoot;
			}
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x00068AAC File Offset: 0x00066CAC
		private int GetSegmentIndex(long position, out int startingIndex)
		{
			long num = 0L;
			int num2 = 0;
			startingIndex = 0;
			foreach (SegmentedChunkStorage.SegmentData segmentData in this)
			{
				long num3 = num + (long)segmentData.LogicalSegmentLength;
				if (num3 > position && position >= num)
				{
					startingIndex = (int)(position - num);
					break;
				}
				if (position == num3 && num2 == this.m_segmentIds.Count - 1 && segmentData.LogicalSegmentLength < Global.ChunkSegmentSize)
				{
					startingIndex = segmentData.LogicalSegmentLength;
					break;
				}
				num = num3;
				num2++;
			}
			return num2;
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x00068B48 File Offset: 0x00066D48
		public IEnumerator<SegmentedChunkStorage.SegmentData> GetEnumerator()
		{
			return this.m_segmentIds.GetEnumerator();
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x00068B48 File Offset: 0x00066D48
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_segmentIds.GetEnumerator();
		}

		// Token: 0x0400096B RID: 2411
		private List<SegmentedChunkStorage.SegmentData> m_segmentIds = new List<SegmentedChunkStorage.SegmentData>();

		// Token: 0x0400096C RID: 2412
		private readonly object m_syncRoot = new object();
	}
}
