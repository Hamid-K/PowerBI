using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200011B RID: 283
	internal abstract class RenderWithSeperatePagination : RenderFromSnapshot
	{
		// Token: 0x06000B7B RID: 2939 RVA: 0x0002AAE4 File Offset: 0x00028CE4
		protected RenderWithSeperatePagination(ReportExecutionBase executionContext)
			: base(executionContext)
		{
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0002AAF0 File Offset: 0x00028CF0
		protected void SetPaginationDataFromSnapshot(ReportSnapshot snapshot)
		{
			RSTrace.CatalogTrace.Assert(this.m_paginationData == null, "attempt to set pagination data multiple times");
			bool flag;
			PaginationMode paginationMode;
			ReportProcessingFlags reportProcessingFlags;
			int snapshotPromotedInfo = base.ExecutionContext.DataProvider.Storage.GetSnapshotPromotedInfo(snapshot, out flag, out paginationMode, out reportProcessingFlags);
			this.m_paginationData = new ReportPaginationData(snapshotPromotedInfo, paginationMode);
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x0002AB3F File Offset: 0x00028D3F
		public override int PageCount
		{
			get
			{
				this.VerifyPaginationData();
				return this.m_paginationData.PageCount;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x0002AB52 File Offset: 0x00028D52
		public override PaginationMode PaginationMode
		{
			get
			{
				this.VerifyPaginationData();
				return this.m_paginationData.Mode;
			}
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0002AB65 File Offset: 0x00028D65
		private void VerifyPaginationData()
		{
			RSTrace.CatalogTrace.Assert(this.m_paginationData != null, "pagination data has not been retrieved");
		}

		// Token: 0x040004BA RID: 1210
		private ReportPaginationData m_paginationData;
	}
}
