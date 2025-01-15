using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003AA RID: 938
	public class DynamicSeries
	{
		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06001EAB RID: 7851 RVA: 0x0007D7B6 File Offset: 0x0007B9B6
		// (set) Token: 0x06001EAC RID: 7852 RVA: 0x0007D7BE File Offset: 0x0007B9BE
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

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06001EAD RID: 7853 RVA: 0x0007D7C7 File Offset: 0x0007B9C7
		// (set) Token: 0x06001EAE RID: 7854 RVA: 0x0007D7CF File Offset: 0x0007B9CF
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

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06001EAF RID: 7855 RVA: 0x0007D7D8 File Offset: 0x0007B9D8
		// (set) Token: 0x06001EB0 RID: 7856 RVA: 0x0007D7E0 File Offset: 0x0007B9E0
		public string Label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
			}
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x0007D7E9 File Offset: 0x0007B9E9
		public DynamicSeries()
		{
			this.Label = "";
		}

		// Token: 0x04000D1E RID: 3358
		private Grouping m_grouping;

		// Token: 0x04000D1F RID: 3359
		private Sorting m_sorting;

		// Token: 0x04000D20 RID: 3360
		private string m_label;
	}
}
