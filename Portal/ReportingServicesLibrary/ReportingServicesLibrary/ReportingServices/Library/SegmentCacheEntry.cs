using System;
using System.Web.Caching;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200027C RID: 636
	internal sealed class SegmentCacheEntry : CachedItemBase
	{
		// Token: 0x060016A0 RID: 5792 RVA: 0x0005A24F File Offset: 0x0005844F
		internal static string BuildCacheKey(Guid segmentId)
		{
			return "ChunkSegmentCache:" + segmentId.ToString();
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x0005A268 File Offset: 0x00058468
		internal SegmentCacheEntry(Guid segmentId, byte[] segmentBytes)
			: base(SegmentCacheEntry.BuildCacheKey(segmentId), Cache.NoAbsoluteExpiration)
		{
			this.m_segmentBytes = segmentBytes;
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x0005A282 File Offset: 0x00058482
		public override long SizeEstimateKb
		{
			get
			{
				return (long)(this.m_segmentBytes.Length / 1024);
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x0005A293 File Offset: 0x00058493
		internal byte[] SegmentBytes
		{
			get
			{
				return this.m_segmentBytes;
			}
		}

		// Token: 0x04000842 RID: 2114
		private readonly byte[] m_segmentBytes;
	}
}
