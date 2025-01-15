using System;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x020007B4 RID: 1972
	internal sealed class GlobalsImpl : Globals
	{
		// Token: 0x06007001 RID: 28673 RVA: 0x001D2C38 File Offset: 0x001D0E38
		internal GlobalsImpl(OnDemandProcessingContext odpContext)
		{
			this.m_reportName = odpContext.ReportContext.ItemName;
			this.m_executionTime = odpContext.ExecutionTime;
			this.m_reportServerUrl = odpContext.ReportContext.HostRootUri;
			this.m_reportFolder = odpContext.ReportFolder;
			this.m_pageNumber = 1;
			this.m_totalPages = 1;
			this.m_overallPageNumber = 1;
			this.m_overallTotalPages = 1;
			this.m_pageName = null;
			this.m_renderFormat = new RenderFormat(new RenderFormatImpl(odpContext));
		}

		// Token: 0x06007002 RID: 28674 RVA: 0x001D2CBC File Offset: 0x001D0EBC
		internal GlobalsImpl(string reportName, int pageNumber, int totalPages, int overallPageNumber, int overallTotalPages, DateTime executionTime, string reportServerUrl, string reportFolder, string pageName, OnDemandProcessingContext odpContext)
		{
			this.m_reportName = reportName;
			this.m_pageNumber = pageNumber;
			this.m_totalPages = totalPages;
			this.m_overallPageNumber = overallPageNumber;
			this.m_overallTotalPages = overallTotalPages;
			this.m_executionTime = executionTime;
			this.m_reportServerUrl = reportServerUrl;
			this.m_reportFolder = reportFolder;
			this.m_pageName = pageName;
			this.m_renderFormat = new RenderFormat(new RenderFormatImpl(odpContext));
		}

		// Token: 0x17002627 RID: 9767
		public override object this[string key]
		{
			get
			{
				if (key != null)
				{
					switch (key.Length)
					{
					case 8:
						if (key == "PageName")
						{
							return this.m_pageName;
						}
						break;
					case 10:
						switch (key[0])
						{
						case 'P':
							if (key == "PageNumber")
							{
								return this.m_pageNumber;
							}
							break;
						case 'R':
							if (key == "ReportName")
							{
								return this.m_reportName;
							}
							break;
						case 'T':
							if (key == "TotalPages")
							{
								return this.m_totalPages;
							}
							break;
						}
						break;
					case 12:
					{
						char c = key[2];
						if (c != 'n')
						{
							if (c == 'p')
							{
								if (key == "ReportFolder")
								{
									return this.m_reportFolder;
								}
							}
						}
						else if (key == "RenderFormat")
						{
							return this.m_renderFormat;
						}
						break;
					}
					case 13:
						if (key == "ExecutionTime")
						{
							return this.m_executionTime;
						}
						break;
					case 15:
						if (key == "ReportServerUrl")
						{
							return this.m_reportServerUrl;
						}
						break;
					case 17:
					{
						char c = key[7];
						if (c != 'P')
						{
							if (c == 'T')
							{
								if (key == "OverallTotalPages")
								{
									return this.m_overallTotalPages;
								}
							}
						}
						else if (key == "OverallPageNumber")
						{
							return this.m_overallPageNumber;
						}
						break;
					}
					}
				}
				throw new ReportProcessingException_NonExistingGlobalReference(key);
			}
		}

		// Token: 0x17002628 RID: 9768
		// (get) Token: 0x06007004 RID: 28676 RVA: 0x001D2EF4 File Offset: 0x001D10F4
		public override string ReportName
		{
			get
			{
				return this.m_reportName;
			}
		}

		// Token: 0x17002629 RID: 9769
		// (get) Token: 0x06007005 RID: 28677 RVA: 0x001D2EFC File Offset: 0x001D10FC
		public override int PageNumber
		{
			get
			{
				return this.m_pageNumber;
			}
		}

		// Token: 0x1700262A RID: 9770
		// (get) Token: 0x06007006 RID: 28678 RVA: 0x001D2F04 File Offset: 0x001D1104
		public override int TotalPages
		{
			get
			{
				return this.m_totalPages;
			}
		}

		// Token: 0x1700262B RID: 9771
		// (get) Token: 0x06007007 RID: 28679 RVA: 0x001D2F0C File Offset: 0x001D110C
		public override int OverallPageNumber
		{
			get
			{
				return this.m_overallPageNumber;
			}
		}

		// Token: 0x1700262C RID: 9772
		// (get) Token: 0x06007008 RID: 28680 RVA: 0x001D2F14 File Offset: 0x001D1114
		public override int OverallTotalPages
		{
			get
			{
				return this.m_overallTotalPages;
			}
		}

		// Token: 0x1700262D RID: 9773
		// (get) Token: 0x06007009 RID: 28681 RVA: 0x001D2F1C File Offset: 0x001D111C
		public override DateTime ExecutionTime
		{
			get
			{
				return this.m_executionTime;
			}
		}

		// Token: 0x1700262E RID: 9774
		// (get) Token: 0x0600700A RID: 28682 RVA: 0x001D2F24 File Offset: 0x001D1124
		public override string ReportServerUrl
		{
			get
			{
				return this.m_reportServerUrl;
			}
		}

		// Token: 0x1700262F RID: 9775
		// (get) Token: 0x0600700B RID: 28683 RVA: 0x001D2F2C File Offset: 0x001D112C
		public override string ReportFolder
		{
			get
			{
				return this.m_reportFolder;
			}
		}

		// Token: 0x17002630 RID: 9776
		// (get) Token: 0x0600700C RID: 28684 RVA: 0x001D2F34 File Offset: 0x001D1134
		public override RenderFormat RenderFormat
		{
			get
			{
				return this.m_renderFormat;
			}
		}

		// Token: 0x17002631 RID: 9777
		// (get) Token: 0x0600700D RID: 28685 RVA: 0x001D2F3C File Offset: 0x001D113C
		public override string PageName
		{
			get
			{
				return this.m_pageName;
			}
		}

		// Token: 0x0600700E RID: 28686 RVA: 0x001D2F44 File Offset: 0x001D1144
		internal void SetPageNumbers(int pageNumber, int totalPages, int overallPageNumber, int overallTotalPages)
		{
			this.m_pageNumber = pageNumber;
			this.m_totalPages = totalPages;
			this.m_overallPageNumber = overallPageNumber;
			this.m_overallTotalPages = overallTotalPages;
		}

		// Token: 0x0600700F RID: 28687 RVA: 0x001D2F63 File Offset: 0x001D1163
		internal void SetPageName(string pageName)
		{
			this.m_pageName = pageName;
		}

		// Token: 0x040039DB RID: 14811
		private string m_reportName;

		// Token: 0x040039DC RID: 14812
		private int m_pageNumber;

		// Token: 0x040039DD RID: 14813
		private int m_totalPages;

		// Token: 0x040039DE RID: 14814
		private int m_overallPageNumber;

		// Token: 0x040039DF RID: 14815
		private int m_overallTotalPages;

		// Token: 0x040039E0 RID: 14816
		private DateTime m_executionTime;

		// Token: 0x040039E1 RID: 14817
		private string m_reportServerUrl;

		// Token: 0x040039E2 RID: 14818
		private string m_reportFolder;

		// Token: 0x040039E3 RID: 14819
		private RenderFormat m_renderFormat;

		// Token: 0x040039E4 RID: 14820
		private string m_pageName;
	}
}
