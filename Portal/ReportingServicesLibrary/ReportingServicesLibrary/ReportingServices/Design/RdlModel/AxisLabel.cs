using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003A9 RID: 937
	public class AxisLabel
	{
		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06001EA6 RID: 7846 RVA: 0x0007D794 File Offset: 0x0007B994
		// (set) Token: 0x06001EA7 RID: 7847 RVA: 0x0007D79C File Offset: 0x0007B99C
		[DefaultValue("")]
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

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x0007D7A5 File Offset: 0x0007B9A5
		// (set) Token: 0x06001EA9 RID: 7849 RVA: 0x0007D7AD File Offset: 0x0007B9AD
		public Style Style
		{
			get
			{
				return this.m_style;
			}
			set
			{
				this.m_style = value;
			}
		}

		// Token: 0x04000D1C RID: 3356
		public string m_label;

		// Token: 0x04000D1D RID: 3357
		private Style m_style;
	}
}
