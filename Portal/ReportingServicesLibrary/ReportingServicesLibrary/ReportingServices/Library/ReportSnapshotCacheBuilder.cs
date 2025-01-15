using System;
using System.Text;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000271 RID: 625
	internal sealed class ReportSnapshotCacheBuilder : BaseKeyBuilder
	{
		// Token: 0x06001665 RID: 5733 RVA: 0x00059253 File Offset: 0x00057453
		internal ReportSnapshotCacheBuilder(CatalogItemContext context, ReportSnapshot reportSnapshot)
			: base(context)
		{
			RSTrace.CacheTracer.Assert(reportSnapshot != null);
			this.m_snapshot = reportSnapshot;
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x00059274 File Offset: 0x00057474
		public override bool AppendKeyInformation(StringBuilder key)
		{
			RSTrace.CacheTracer.Assert(key != null, "key != null");
			RSTrace.CacheTracer.Assert(this.m_snapshot != null, "m_snapshot != null");
			key.Append("&");
			key.Append(this.m_snapshot.SnapshotDataID.ToString());
			return true;
		}

		// Token: 0x0400082E RID: 2094
		private readonly ReportSnapshot m_snapshot;
	}
}
