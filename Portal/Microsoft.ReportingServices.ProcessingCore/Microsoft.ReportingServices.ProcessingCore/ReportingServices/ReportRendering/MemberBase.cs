using System;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200000B RID: 11
	internal class MemberBase
	{
		// Token: 0x0600030E RID: 782 RVA: 0x00007593 File Offset: 0x00005793
		internal MemberBase(bool isCustomControl)
		{
			this.m_customControl = isCustomControl;
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600030F RID: 783 RVA: 0x000075A2 File Offset: 0x000057A2
		internal bool IsCustomControl
		{
			get
			{
				return this.m_customControl;
			}
		}

		// Token: 0x0400001F RID: 31
		private bool m_customControl;
	}
}
