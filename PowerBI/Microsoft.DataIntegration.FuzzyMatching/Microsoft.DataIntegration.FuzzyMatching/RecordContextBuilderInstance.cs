using System;
using System.IO;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000CC RID: 204
	internal class RecordContextBuilderInstance
	{
		// Token: 0x060007AA RID: 1962 RVA: 0x00022DF0 File Offset: 0x00020FF0
		public void Reset()
		{
			this.TransientTokenIdProvider.Reset();
			this.LookupUpdateContext.Reset();
			this.BinaryWriter.BaseStream.Seek(0L, 0);
			this.BinaryWriter.Seek(0, 0);
			this.Record.Clear();
			this.m_charSegmentAllocator.Reset();
		}

		// Token: 0x04000333 RID: 819
		public TransientTokenIdProvider TransientTokenIdProvider;

		// Token: 0x04000334 RID: 820
		public LookupUpdateContext LookupUpdateContext;

		// Token: 0x04000335 RID: 821
		public BinaryWriter BinaryWriter = new BinaryWriter(new MemoryStream());

		// Token: 0x04000336 RID: 822
		public Record Record = new Record();

		// Token: 0x04000337 RID: 823
		public BlockedSegmentArray<char> m_charSegmentAllocator = new BlockedSegmentArray<char>();
	}
}
