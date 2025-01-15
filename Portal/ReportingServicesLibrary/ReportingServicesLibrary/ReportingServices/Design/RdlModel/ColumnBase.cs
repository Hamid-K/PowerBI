using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000420 RID: 1056
	public abstract class ColumnBase
	{
		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06002185 RID: 8581 RVA: 0x000810FB File Offset: 0x0007F2FB
		// (set) Token: 0x06002186 RID: 8582 RVA: 0x00081103 File Offset: 0x0007F303
		public Unit Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				this.m_width = value;
			}
		}

		// Token: 0x04000EB6 RID: 3766
		private Unit m_width;
	}
}
