using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006C7 RID: 1735
	internal interface ISortFilterScope
	{
		// Token: 0x1700209C RID: 8348
		// (get) Token: 0x06005D00 RID: 23808
		int ID { get; }

		// Token: 0x1700209D RID: 8349
		// (get) Token: 0x06005D01 RID: 23809
		string ScopeName { get; }

		// Token: 0x1700209E RID: 8350
		// (get) Token: 0x06005D02 RID: 23810
		// (set) Token: 0x06005D03 RID: 23811
		bool[] IsSortFilterTarget { get; set; }

		// Token: 0x1700209F RID: 8351
		// (get) Token: 0x06005D04 RID: 23812
		// (set) Token: 0x06005D05 RID: 23813
		bool[] IsSortFilterExpressionScope { get; set; }

		// Token: 0x170020A0 RID: 8352
		// (get) Token: 0x06005D06 RID: 23814
		// (set) Token: 0x06005D07 RID: 23815
		ExpressionInfoList UserSortExpressions { get; set; }

		// Token: 0x170020A1 RID: 8353
		// (get) Token: 0x06005D08 RID: 23816
		IndexedExprHost UserSortExpressionsHost { get; }
	}
}
