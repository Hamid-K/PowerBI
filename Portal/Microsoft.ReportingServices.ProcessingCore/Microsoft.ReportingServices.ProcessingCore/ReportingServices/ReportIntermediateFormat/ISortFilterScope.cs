using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000504 RID: 1284
	internal interface ISortFilterScope : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable
	{
		// Token: 0x17001C38 RID: 7224
		// (get) Token: 0x06004333 RID: 17203
		int ID { get; }

		// Token: 0x17001C39 RID: 7225
		// (get) Token: 0x06004334 RID: 17204
		string ScopeName { get; }

		// Token: 0x17001C3A RID: 7226
		// (get) Token: 0x06004335 RID: 17205
		// (set) Token: 0x06004336 RID: 17206
		bool[] IsSortFilterTarget { get; set; }

		// Token: 0x17001C3B RID: 7227
		// (get) Token: 0x06004337 RID: 17207
		// (set) Token: 0x06004338 RID: 17208
		bool[] IsSortFilterExpressionScope { get; set; }

		// Token: 0x17001C3C RID: 7228
		// (get) Token: 0x06004339 RID: 17209
		// (set) Token: 0x0600433A RID: 17210
		List<ExpressionInfo> UserSortExpressions { get; set; }

		// Token: 0x17001C3D RID: 7229
		// (get) Token: 0x0600433B RID: 17211
		IndexedExprHost UserSortExpressionsHost { get; }
	}
}
