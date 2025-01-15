using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000406 RID: 1030
	public abstract class PageHeaderFooter : ReportSection
	{
		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x060020CF RID: 8399 RVA: 0x0007FBE1 File Offset: 0x0007DDE1
		// (set) Token: 0x060020D0 RID: 8400 RVA: 0x0007FBE9 File Offset: 0x0007DDE9
		[DefaultValue(false)]
		public bool PrintOnFirstPage
		{
			get
			{
				return this.m_printOnFirstPage;
			}
			set
			{
				this.m_printOnFirstPage = value;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x060020D1 RID: 8401 RVA: 0x0007FBF2 File Offset: 0x0007DDF2
		// (set) Token: 0x060020D2 RID: 8402 RVA: 0x0007FBFA File Offset: 0x0007DDFA
		[DefaultValue(false)]
		public bool PrintOnLastPage
		{
			get
			{
				return this.m_printOnLastPage;
			}
			set
			{
				this.m_printOnLastPage = value;
			}
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x0007FC03 File Offset: 0x0007DE03
		protected PageHeaderFooter()
		{
			base.Height = new Unit(0.25, UnitType.Inch);
		}

		// Token: 0x04000E58 RID: 3672
		private bool m_printOnFirstPage;

		// Token: 0x04000E59 RID: 3673
		private bool m_printOnLastPage;
	}
}
