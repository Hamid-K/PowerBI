using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003FD RID: 1021
	public class RectangleItem : ReportItem
	{
		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06002050 RID: 8272 RVA: 0x0007F564 File Offset: 0x0007D764
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ReportItemCollection ReportItems
		{
			get
			{
				return this.m_reportItems;
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06002051 RID: 8273 RVA: 0x0007F56C File Offset: 0x0007D76C
		// (set) Token: 0x06002052 RID: 8274 RVA: 0x0007F574 File Offset: 0x0007D774
		[DefaultValue(false)]
		public bool PageBreakAtStart
		{
			get
			{
				return this.m_pageBreakAtStart;
			}
			set
			{
				this.m_pageBreakAtStart = value;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06002053 RID: 8275 RVA: 0x0007F57D File Offset: 0x0007D77D
		// (set) Token: 0x06002054 RID: 8276 RVA: 0x0007F585 File Offset: 0x0007D785
		[DefaultValue(false)]
		public bool PageBreakAtEnd
		{
			get
			{
				return this.m_pageBreakAtEnd;
			}
			set
			{
				this.m_pageBreakAtEnd = value;
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06002055 RID: 8277 RVA: 0x0007F58E File Offset: 0x0007D78E
		// (set) Token: 0x06002056 RID: 8278 RVA: 0x0007F596 File Offset: 0x0007D796
		[DefaultValue("")]
		public string LinkToChild
		{
			get
			{
				return this.m_linkToChild;
			}
			set
			{
				this.m_linkToChild = value;
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06002057 RID: 8279 RVA: 0x0007F59F File Offset: 0x0007D79F
		// (set) Token: 0x06002058 RID: 8280 RVA: 0x0007F5A7 File Offset: 0x0007D7A7
		[DefaultValue(DataElementOutputs.Auto)]
		public new DataElementOutputs DataElementOutput
		{
			get
			{
				return base.DataElementOutput;
			}
			set
			{
				base.DataElementOutput = value;
			}
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x0007F5B0 File Offset: 0x0007D7B0
		public RectangleItem()
		{
			this.m_reportItems = new ReportItemCollection();
		}

		// Token: 0x04000E1C RID: 3612
		private string m_linkToChild;

		// Token: 0x04000E1D RID: 3613
		private bool m_pageBreakAtStart;

		// Token: 0x04000E1E RID: 3614
		private bool m_pageBreakAtEnd;

		// Token: 0x04000E1F RID: 3615
		private ReportItemCollection m_reportItems;
	}
}
