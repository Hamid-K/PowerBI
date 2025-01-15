using System;
using System.Collections.Generic;

namespace Model
{
	// Token: 0x02000027 RID: 39
	public class ReportModel : DataSource
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x00002772 File Offset: 0x00000972
		public ReportModel()
		{
			base.Type = CatalogItemType.ReportModel;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00002782 File Offset: 0x00000982
		public new IList<Subscription> Subscriptions
		{
			get
			{
				return base.Subscriptions;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000278A File Offset: 0x0000098A
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00002792 File Offset: 0x00000992
		public bool HasDataSources { get; set; }
	}
}
