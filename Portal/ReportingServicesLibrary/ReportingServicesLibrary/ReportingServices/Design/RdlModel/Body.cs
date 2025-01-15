using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000405 RID: 1029
	public sealed class Body : ReportSection
	{
		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x060020C9 RID: 8393 RVA: 0x0007FB95 File Offset: 0x0007DD95
		// (set) Token: 0x060020CA RID: 8394 RVA: 0x0007FB9D File Offset: 0x0007DD9D
		[DefaultValue(1)]
		public int Columns
		{
			get
			{
				return this.m_columns;
			}
			set
			{
				Utils.ValidateValueRange("Columns", value, 1, 1000);
				this.m_columns = value;
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x060020CB RID: 8395 RVA: 0x0007FBC1 File Offset: 0x0007DDC1
		// (set) Token: 0x060020CC RID: 8396 RVA: 0x0007FBC9 File Offset: 0x0007DDC9
		[DefaultValue(typeof(Unit), "0.5in")]
		public Unit ColumnSpacing
		{
			get
			{
				return this.m_columnSpacing;
			}
			set
			{
				this.m_columnSpacing = value;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x060020CD RID: 8397 RVA: 0x000053DC File Offset: 0x000035DC
		public override SectionType Type
		{
			get
			{
				return SectionType.Body;
			}
		}

		// Token: 0x04000E56 RID: 3670
		private int m_columns = 1;

		// Token: 0x04000E57 RID: 3671
		private Unit m_columnSpacing;
	}
}
