using System;
using System.Collections.Generic;

namespace Model
{
	// Token: 0x02000013 RID: 19
	public class ExcelWorkbook : CatalogItem
	{
		// Token: 0x06000054 RID: 84 RVA: 0x0000230B File Offset: 0x0000050B
		public ExcelWorkbook()
			: base(CatalogItemType.ExcelWorkbook)
		{
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002315 File Offset: 0x00000515
		public new IList<Comment> Comments
		{
			get
			{
				return base.Comments;
			}
		}
	}
}
