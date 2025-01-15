using System;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000B3 RID: 179
	internal class ComparerInstance
	{
		// Token: 0x060006E4 RID: 1764 RVA: 0x0001EE04 File Offset: 0x0001D004
		public void Reset()
		{
			this.TransientTokenIdProvider.Reset();
			this.leftRecord.Clear();
			this.rightRecord.Clear();
			this.leftRecordContext.Reset();
			this.rightRecordContext.Reset();
			this.intAllocator.Reset();
			this.byteAllocator.Reset();
			this.m_charAllocator.Reset();
			this.m_tranMatchAllocator.Reset();
			if (this.ComparerSession != null)
			{
				this.ComparerSession.Reset();
			}
		}

		// Token: 0x04000295 RID: 661
		public TransientTokenIdProvider TransientTokenIdProvider = new TransientTokenIdProvider();

		// Token: 0x04000296 RID: 662
		public IFuzzyComparer Comparer;

		// Token: 0x04000297 RID: 663
		public ISession ComparerSession;

		// Token: 0x04000298 RID: 664
		public Record leftRecord = new Record
		{
			Values = new object[1]
		};

		// Token: 0x04000299 RID: 665
		public Record rightRecord = new Record
		{
			Values = new object[1]
		};

		// Token: 0x0400029A RID: 666
		public RecordContext leftRecordContext = new RecordContext();

		// Token: 0x0400029B RID: 667
		public RecordContext rightRecordContext = new RecordContext();

		// Token: 0x0400029C RID: 668
		public TemporalHandle DomainManagerHandle;

		// Token: 0x0400029D RID: 669
		public BlockedSegmentArray<int> intAllocator = new BlockedSegmentArray<int>();

		// Token: 0x0400029E RID: 670
		public BlockedSegmentArray<byte> byteAllocator = new BlockedSegmentArray<byte>();

		// Token: 0x0400029F RID: 671
		public BlockedSegmentArray<char> m_charAllocator = new BlockedSegmentArray<char>();

		// Token: 0x040002A0 RID: 672
		public BlockedSegmentArray<WeightedTransformationMatch> m_tranMatchAllocator = new BlockedSegmentArray<WeightedTransformationMatch>();
	}
}
