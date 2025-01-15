using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000422 RID: 1058
	public abstract class GroupingBase
	{
		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06002189 RID: 8585 RVA: 0x0008110C File Offset: 0x0007F30C
		// (set) Token: 0x0600218A RID: 8586 RVA: 0x00081114 File Offset: 0x0007F314
		internal Grouping Grouping
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

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x0600218B RID: 8587 RVA: 0x0008111D File Offset: 0x0007F31D
		// (set) Token: 0x0600218C RID: 8588 RVA: 0x00081125 File Offset: 0x0007F325
		internal Sorting Sorting
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

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x0600218D RID: 8589 RVA: 0x0008112E File Offset: 0x0007F32E
		// (set) Token: 0x0600218E RID: 8590 RVA: 0x00081136 File Offset: 0x0007F336
		internal Visibility Visibility
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

		// Token: 0x04000EBA RID: 3770
		private Grouping m_grouping;

		// Token: 0x04000EBB RID: 3771
		private Sorting m_sorting;

		// Token: 0x04000EBC RID: 3772
		private Visibility m_visibility;
	}
}
