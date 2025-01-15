using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003AD RID: 941
	public class CategoryGrouping
	{
		// Token: 0x06001EB6 RID: 7862 RVA: 0x000025F4 File Offset: 0x000007F4
		public CategoryGrouping()
		{
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x0007D834 File Offset: 0x0007BA34
		public CategoryGrouping(List<StaticMember> labels)
		{
			this.StaticCategories = labels;
		}

		// Token: 0x04000D24 RID: 3364
		public DynamicCategories DynamicCategories;

		// Token: 0x04000D25 RID: 3365
		public List<StaticMember> StaticCategories;
	}
}
