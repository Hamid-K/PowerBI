using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations
{
	// Token: 0x02000252 RID: 594
	internal sealed class FilterAnnotationAnalyzerResult
	{
		// Token: 0x0600148C RID: 5260 RVA: 0x0004E945 File Offset: 0x0004CB45
		internal FilterAnnotationAnalyzerResult(DataShape contextDataShape, bool isApplyFilter, bool isScopeFilter)
		{
			this.ContextDataShape = contextDataShape;
			this.IsApplyFilter = isApplyFilter;
			this.IsScopeFilter = isScopeFilter;
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x0004E962 File Offset: 0x0004CB62
		internal DataShape ContextDataShape { get; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x0004E96A File Offset: 0x0004CB6A
		internal bool IsApplyFilter { get; }

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x0004E972 File Offset: 0x0004CB72
		internal bool IsScopeFilter { get; }
	}
}
