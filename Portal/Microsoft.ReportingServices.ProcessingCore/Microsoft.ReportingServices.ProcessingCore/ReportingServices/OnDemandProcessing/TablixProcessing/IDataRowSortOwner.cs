using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x02000903 RID: 2307
	internal interface IDataRowSortOwner
	{
		// Token: 0x06007EEE RID: 32494
		void PostDataRowSortNextRow();

		// Token: 0x06007EEF RID: 32495
		void DataRowSortTraverse();

		// Token: 0x17002941 RID: 10561
		// (get) Token: 0x06007EF0 RID: 32496
		OnDemandProcessingContext OdpContext { get; }

		// Token: 0x17002942 RID: 10562
		// (get) Token: 0x06007EF1 RID: 32497
		Sorting SortingDef { get; }

		// Token: 0x06007EF2 RID: 32498
		object EvaluateDataRowSortExpression(RuntimeExpressionInfo expression);
	}
}
