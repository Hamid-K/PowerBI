using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library.Soap2005
{
	// Token: 0x020002FC RID: 764
	public struct PageCountModeValue
	{
		// Token: 0x06001AF8 RID: 6904 RVA: 0x0006D361 File Offset: 0x0006B561
		internal PageCountModeValue(PageCountMode mode)
		{
			this.m_mode = mode;
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x0006D36A File Offset: 0x0006B56A
		internal PageCountModeValue(PaginationMode processingPageMode)
		{
			if (processingPageMode == PaginationMode.TotalPages)
			{
				this.m_mode = PageCountMode.Actual;
				return;
			}
			this.m_mode = PageCountMode.Estimate;
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x0006D37F File Offset: 0x0006B57F
		internal PageCountModeValue(string stringRepresentation)
		{
			if (string.IsNullOrEmpty(stringRepresentation))
			{
				this.m_mode = PageCountMode.Actual;
			}
			if (string.Compare(stringRepresentation, "Estimate", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_mode = PageCountMode.Estimate;
				return;
			}
			this.m_mode = PageCountMode.Actual;
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x0006D3B0 File Offset: 0x0006B5B0
		internal PaginationMode ToProcessingPaginationMode()
		{
			PageCountMode mode = this.Mode;
			if (mode == PageCountMode.Estimate)
			{
				return PaginationMode.Progressive;
			}
			return PaginationMode.TotalPages;
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001AFC RID: 6908 RVA: 0x0006D3CB File Offset: 0x0006B5CB
		public PageCountMode Mode
		{
			get
			{
				return this.m_mode;
			}
		}

		// Token: 0x04000A2A RID: 2602
		private PageCountMode m_mode;
	}
}
