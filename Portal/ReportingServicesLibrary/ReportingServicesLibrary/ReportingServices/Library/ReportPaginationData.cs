using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000125 RID: 293
	internal sealed class ReportPaginationData
	{
		// Token: 0x06000BC1 RID: 3009 RVA: 0x0002B898 File Offset: 0x00029A98
		public ReportPaginationData(int pageCount, PaginationMode mode)
		{
			this.m_pageCount = pageCount;
			this.m_paginationMode = mode;
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x0002B8AE File Offset: 0x00029AAE
		public int PageCount
		{
			get
			{
				return this.m_pageCount;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x0002B8B6 File Offset: 0x00029AB6
		public PaginationMode Mode
		{
			get
			{
				return this.m_paginationMode;
			}
		}

		// Token: 0x040004BF RID: 1215
		private readonly int m_pageCount;

		// Token: 0x040004C0 RID: 1216
		private readonly PaginationMode m_paginationMode;
	}
}
