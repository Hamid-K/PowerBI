using System;
using System.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002D1 RID: 721
	internal abstract class CompositeStorageLayer : SegmentStorageLayer
	{
		// Token: 0x060019DD RID: 6621 RVA: 0x000685C2 File Offset: 0x000667C2
		protected CompositeStorageLayer(SegmentStorageLayer wrappedLayer)
		{
			this.m_wrappedLayer = wrappedLayer;
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x000685D1 File Offset: 0x000667D1
		public override void Close()
		{
			this.WrappedLayer.Close();
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x000685DE File Offset: 0x000667DE
		public override SegmentedChunkStorage.SegmentData CreateSegment(SegmentChunkDbInterface storage, SegmentStorageLayer.ReadWriteParameters parameters, ref SegmentStorageLayer.ReadWriteStatistics statistics)
		{
			return this.WrappedLayer.CreateSegment(storage, parameters, ref statistics);
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x000685EE File Offset: 0x000667EE
		public override void Read(SegmentChunkDbInterface storage, SegmentStorageLayer.ReadWriteParameters parameters, ref SegmentStorageLayer.ReadWriteStatistics statistics)
		{
			this.WrappedLayer.Read(storage, parameters, ref statistics);
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x000685FE File Offset: 0x000667FE
		public override Guid VersionChunk(SegmentChunkDbInterface storage, Guid snapshotId, Guid chunkId, bool isPermanent)
		{
			return this.WrappedLayer.VersionChunk(storage, snapshotId, chunkId, isPermanent);
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x00068610 File Offset: 0x00066810
		public override void Write(SegmentChunkDbInterface storage, SegmentStorageLayer.ReadWriteParameters parameters, ref SegmentStorageLayer.ReadWriteStatistics statistics)
		{
			this.WrappedLayer.Write(storage, parameters, ref statistics);
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x060019E3 RID: 6627 RVA: 0x00068620 File Offset: 0x00066820
		protected SegmentStorageLayer WrappedLayer
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_wrappedLayer;
			}
		}

		// Token: 0x04000968 RID: 2408
		private readonly SegmentStorageLayer m_wrappedLayer;
	}
}
