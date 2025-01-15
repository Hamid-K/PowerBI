using System;

namespace Microsoft.ReportingServices.ReportProcessing.ReportObjectModel
{
	// Token: 0x02000792 RID: 1938
	internal sealed class GlobalsImpl : Globals
	{
		// Token: 0x06006C23 RID: 27683 RVA: 0x001B6D43 File Offset: 0x001B4F43
		internal GlobalsImpl(string reportName, DateTime executionTime, string reportServerUrl, string reportFolder)
		{
			this.m_reportName = reportName;
			this.m_pageNumber = 1;
			this.m_totalPages = 1;
			this.m_executionTime = executionTime;
			this.m_reportServerUrl = reportServerUrl;
			this.m_reportFolder = reportFolder;
		}

		// Token: 0x06006C24 RID: 27684 RVA: 0x001B6D76 File Offset: 0x001B4F76
		internal GlobalsImpl(string reportName, int pageNumber, int totalPages, DateTime executionTime, string reportServerUrl, string reportFolder)
		{
			this.m_reportName = reportName;
			this.m_pageNumber = pageNumber;
			this.m_totalPages = totalPages;
			this.m_executionTime = executionTime;
			this.m_reportServerUrl = reportServerUrl;
			this.m_reportFolder = reportFolder;
		}

		// Token: 0x1700259D RID: 9629
		public override object this[string key]
		{
			get
			{
				if (key != null)
				{
					switch (key.Length)
					{
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
							return new NotSupportedException();
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
					}
				}
				throw new ArgumentOutOfRangeException("key");
			}
		}

		// Token: 0x1700259E RID: 9630
		// (get) Token: 0x06006C26 RID: 27686 RVA: 0x001B6EE5 File Offset: 0x001B50E5
		public override string ReportName
		{
			get
			{
				return this.m_reportName;
			}
		}

		// Token: 0x1700259F RID: 9631
		// (get) Token: 0x06006C27 RID: 27687 RVA: 0x001B6EED File Offset: 0x001B50ED
		public override int PageNumber
		{
			get
			{
				return this.m_pageNumber;
			}
		}

		// Token: 0x170025A0 RID: 9632
		// (get) Token: 0x06006C28 RID: 27688 RVA: 0x001B6EF5 File Offset: 0x001B50F5
		public override int TotalPages
		{
			get
			{
				return this.m_totalPages;
			}
		}

		// Token: 0x170025A1 RID: 9633
		// (get) Token: 0x06006C29 RID: 27689 RVA: 0x001B6EFD File Offset: 0x001B50FD
		public override int OverallPageNumber
		{
			get
			{
				return this.m_pageNumber;
			}
		}

		// Token: 0x170025A2 RID: 9634
		// (get) Token: 0x06006C2A RID: 27690 RVA: 0x001B6F05 File Offset: 0x001B5105
		public override int OverallTotalPages
		{
			get
			{
				return this.m_totalPages;
			}
		}

		// Token: 0x170025A3 RID: 9635
		// (get) Token: 0x06006C2B RID: 27691 RVA: 0x001B6F0D File Offset: 0x001B510D
		public override DateTime ExecutionTime
		{
			get
			{
				return this.m_executionTime;
			}
		}

		// Token: 0x170025A4 RID: 9636
		// (get) Token: 0x06006C2C RID: 27692 RVA: 0x001B6F15 File Offset: 0x001B5115
		public override string ReportServerUrl
		{
			get
			{
				return this.m_reportServerUrl;
			}
		}

		// Token: 0x170025A5 RID: 9637
		// (get) Token: 0x06006C2D RID: 27693 RVA: 0x001B6F1D File Offset: 0x001B511D
		public override string ReportFolder
		{
			get
			{
				return this.m_reportFolder;
			}
		}

		// Token: 0x170025A6 RID: 9638
		// (get) Token: 0x06006C2E RID: 27694 RVA: 0x001B6F25 File Offset: 0x001B5125
		public override string PageName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170025A7 RID: 9639
		// (get) Token: 0x06006C2F RID: 27695 RVA: 0x001B6F28 File Offset: 0x001B5128
		public override RenderFormat RenderFormat
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06006C30 RID: 27696 RVA: 0x001B6F2F File Offset: 0x001B512F
		internal void SetPageNumber(int pageNumber)
		{
			this.m_pageNumber = pageNumber;
		}

		// Token: 0x06006C31 RID: 27697 RVA: 0x001B6F38 File Offset: 0x001B5138
		internal void SetPageNumbers(int pageNumber, int totalPages)
		{
			this.m_pageNumber = pageNumber;
			this.m_totalPages = totalPages;
		}

		// Token: 0x0400364E RID: 13902
		private string m_reportName;

		// Token: 0x0400364F RID: 13903
		private int m_pageNumber;

		// Token: 0x04003650 RID: 13904
		private int m_totalPages;

		// Token: 0x04003651 RID: 13905
		private DateTime m_executionTime;

		// Token: 0x04003652 RID: 13906
		private string m_reportServerUrl;

		// Token: 0x04003653 RID: 13907
		private string m_reportFolder;

		// Token: 0x04003654 RID: 13908
		internal const string Name = "Globals";

		// Token: 0x04003655 RID: 13909
		internal const string FullName = "Microsoft.ReportingServices.ReportProcessing.ReportObjectModel.Globals";
	}
}
