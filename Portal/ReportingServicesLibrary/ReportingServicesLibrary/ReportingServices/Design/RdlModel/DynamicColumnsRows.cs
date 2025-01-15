using System;
using Microsoft.ReportingServices.Design.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003F0 RID: 1008
	public sealed class DynamicColumnsRows
	{
		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x0600200C RID: 8204 RVA: 0x0007F12B File Offset: 0x0007D32B
		// (set) Token: 0x0600200D RID: 8205 RVA: 0x0007F133 File Offset: 0x0007D333
		public Grouping Grouping
		{
			get
			{
				return this.m_grouping;
			}
			set
			{
				this.m_grouping = value;
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x0600200E RID: 8206 RVA: 0x0007F13C File Offset: 0x0007D33C
		// (set) Token: 0x0600200F RID: 8207 RVA: 0x0007F144 File Offset: 0x0007D344
		public Sorting Sorting
		{
			get
			{
				return this.m_sorting;
			}
			set
			{
				this.m_sorting = value;
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06002010 RID: 8208 RVA: 0x0007F14D File Offset: 0x0007D34D
		// (set) Token: 0x06002011 RID: 8209 RVA: 0x0007F155 File Offset: 0x0007D355
		public Visibility Visibility
		{
			get
			{
				return this.m_visibility;
			}
			set
			{
				this.m_visibility = value;
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06002012 RID: 8210 RVA: 0x0007F15E File Offset: 0x0007D35E
		// (set) Token: 0x06002013 RID: 8211 RVA: 0x0007F166 File Offset: 0x0007D366
		public Subtotal Subtotal
		{
			get
			{
				return this.m_subtotal;
			}
			set
			{
				this.m_subtotal = value;
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06002014 RID: 8212 RVA: 0x0007F16F File Offset: 0x0007D36F
		// (set) Token: 0x06002015 RID: 8213 RVA: 0x0007F177 File Offset: 0x0007D377
		[XmlParentElement("ReportItems")]
		public ReportItem ReportItem
		{
			get
			{
				return this.m_reportItem;
			}
			set
			{
				this.m_reportItem = value;
			}
		}

		// Token: 0x04000DF8 RID: 3576
		private Grouping m_grouping;

		// Token: 0x04000DF9 RID: 3577
		private Sorting m_sorting;

		// Token: 0x04000DFA RID: 3578
		private Subtotal m_subtotal;

		// Token: 0x04000DFB RID: 3579
		private Visibility m_visibility;

		// Token: 0x04000DFC RID: 3580
		private ReportItem m_reportItem;
	}
}
