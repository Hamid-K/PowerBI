using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000421 RID: 1057
	public class TableColumn
	{
		// Token: 0x04000EB7 RID: 3767
		public Unit Width;

		// Token: 0x04000EB8 RID: 3768
		public Visibility Visibility;

		// Token: 0x04000EB9 RID: 3769
		[DefaultValue(false)]
		public bool FixedHeader;
	}
}
