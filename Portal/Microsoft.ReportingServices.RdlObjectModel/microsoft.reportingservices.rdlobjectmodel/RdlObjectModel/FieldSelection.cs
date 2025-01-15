using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000207 RID: 519
	public class FieldSelection
	{
		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x000280FA File Offset: 0x000262FA
		// (set) Token: 0x06001184 RID: 4484 RVA: 0x00028102 File Offset: 0x00026302
		public string EntityName { get; set; }

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x0002810B File Offset: 0x0002630B
		// (set) Token: 0x06001186 RID: 4486 RVA: 0x00028113 File Offset: 0x00026313
		public string PropertyName { get; set; }

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x0002811C File Offset: 0x0002631C
		// (set) Token: 0x06001188 RID: 4488 RVA: 0x00028124 File Offset: 0x00026324
		public string HierarchyName { get; set; }

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x0002812D File Offset: 0x0002632D
		// (set) Token: 0x0600118A RID: 4490 RVA: 0x00028135 File Offset: 0x00026335
		public string Aggregate { get; set; }

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x0002813E File Offset: 0x0002633E
		// (set) Token: 0x0600118C RID: 4492 RVA: 0x00028146 File Offset: 0x00026346
		public string ParentPropertyName { get; set; }

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x0002814F File Offset: 0x0002634F
		// (set) Token: 0x0600118E RID: 4494 RVA: 0x00028157 File Offset: 0x00026357
		[DefaultValue(false)]
		public bool HideTotal { get; set; }
	}
}
