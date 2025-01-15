using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000241 RID: 577
	internal interface IIdentifiable : IReportLink, ICloneable
	{
		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001348 RID: 4936
		// (set) Token: 0x06001349 RID: 4937
		string Name { get; set; }

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x0600134A RID: 4938
		// (set) Token: 0x0600134B RID: 4939
		string DisplayName { get; set; }
	}
}
