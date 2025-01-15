using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000423 RID: 1059
	public class TableGroup : GroupingBase
	{
		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06002191 RID: 8593 RVA: 0x0008113F File Offset: 0x0007F33F
		// (set) Token: 0x06002192 RID: 8594 RVA: 0x00081147 File Offset: 0x0007F347
		public new Grouping Grouping
		{
			get
			{
				return base.Grouping;
			}
			set
			{
				base.Grouping = value;
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06002193 RID: 8595 RVA: 0x00081150 File Offset: 0x0007F350
		// (set) Token: 0x06002194 RID: 8596 RVA: 0x00081158 File Offset: 0x0007F358
		public new Sorting Sorting
		{
			get
			{
				return base.Sorting;
			}
			set
			{
				base.Sorting = value;
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06002195 RID: 8597 RVA: 0x00081161 File Offset: 0x0007F361
		// (set) Token: 0x06002196 RID: 8598 RVA: 0x00081169 File Offset: 0x0007F369
		public new Visibility Visibility
		{
			get
			{
				return base.Visibility;
			}
			set
			{
				base.Visibility = value;
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06002197 RID: 8599 RVA: 0x00081172 File Offset: 0x0007F372
		// (set) Token: 0x06002198 RID: 8600 RVA: 0x00081185 File Offset: 0x0007F385
		public GroupHeader Header
		{
			get
			{
				return new GroupHeader(this.m_headerRows, this.m_repeatHeader);
			}
			set
			{
				this.m_repeatHeader = value.RepeatOnNewPage;
				this.m_headerRows = value.TableRows;
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06002199 RID: 8601 RVA: 0x0008119F File Offset: 0x0007F39F
		// (set) Token: 0x0600219A RID: 8602 RVA: 0x000811B2 File Offset: 0x0007F3B2
		public GroupHeader Footer
		{
			get
			{
				return new GroupHeader(this.m_footerRows, this.m_repeatFooter);
			}
			set
			{
				this.m_repeatFooter = value.RepeatOnNewPage;
				this.m_footerRows = value.TableRows;
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x0600219B RID: 8603 RVA: 0x000811CC File Offset: 0x0007F3CC
		// (set) Token: 0x0600219C RID: 8604 RVA: 0x000811D4 File Offset: 0x0007F3D4
		[XmlIgnore]
		public bool RepeatHeaderOnNewPage
		{
			get
			{
				return this.m_repeatHeader;
			}
			set
			{
				this.m_repeatHeader = value;
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x0600219D RID: 8605 RVA: 0x000811DD File Offset: 0x0007F3DD
		// (set) Token: 0x0600219E RID: 8606 RVA: 0x000811E5 File Offset: 0x0007F3E5
		[XmlIgnore]
		internal bool RepeatFooterOnNewPage
		{
			get
			{
				return this.m_repeatFooter;
			}
			set
			{
				this.m_repeatFooter = value;
			}
		}

		// Token: 0x04000EBD RID: 3773
		private List<TableRow> m_headerRows;

		// Token: 0x04000EBE RID: 3774
		private List<TableRow> m_footerRows;

		// Token: 0x04000EBF RID: 3775
		private bool m_repeatHeader;

		// Token: 0x04000EC0 RID: 3776
		private bool m_repeatFooter;
	}
}
