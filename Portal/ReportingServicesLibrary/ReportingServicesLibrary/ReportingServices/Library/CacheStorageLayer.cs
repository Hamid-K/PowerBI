using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002D3 RID: 723
	internal sealed class CacheStorageLayer : CompositeStorageLayer
	{
		// Token: 0x060019EB RID: 6635 RVA: 0x00068898 File Offset: 0x00066A98
		public CacheStorageLayer(SegmentStorageLayer durableStorage)
			: base(durableStorage)
		{
			this.m_cache = new SegmentCache();
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x000688AC File Offset: 0x00066AAC
		public override void Read(SegmentChunkDbInterface storage, SegmentStorageLayer.ReadWriteParameters parameters, ref SegmentStorageLayer.ReadWriteStatistics statistics)
		{
			Guid segmentId = parameters.Segment.SegmentId;
			byte[] array;
			if (this.m_cache.TryGetCache(segmentId, out array))
			{
				if (RSTrace.ChunkTracer.TraceVerbose)
				{
					RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "Retrieved segment {0} for chunk {1} from the segment cache", new object[] { segmentId, parameters.ChunkId });
				}
			}
			else
			{
				array = new byte[parameters.Segment.LogicalSegmentLength];
				base.Read(storage, new SegmentStorageLayer.ReadWriteParameters
				{
					AbsolutePosition = parameters.AbsolutePosition,
					Buffer = array,
					ChunkId = parameters.ChunkId,
					DataIndex = 0,
					IsPermanent = parameters.IsPermanent,
					Length = parameters.Segment.LogicalSegmentLength,
					Offset = 0,
					Segment = parameters.Segment
				}, ref statistics);
				this.m_cache.SetCache(segmentId, array);
			}
			RSTrace.ChunkTracer.Assert(array != null, "cachedBuffer");
			Array.Copy(array, 0, parameters.Buffer, parameters.Offset, parameters.Length);
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x000689CF File Offset: 0x00066BCF
		public override void Write(SegmentChunkDbInterface storage, SegmentStorageLayer.ReadWriteParameters parameters, ref SegmentStorageLayer.ReadWriteStatistics statistics)
		{
			base.Write(storage, parameters, ref statistics);
			this.m_cache.ClearEntry(parameters.Segment.SegmentId);
		}

		// Token: 0x0400096A RID: 2410
		private readonly SegmentCache m_cache;
	}
}
