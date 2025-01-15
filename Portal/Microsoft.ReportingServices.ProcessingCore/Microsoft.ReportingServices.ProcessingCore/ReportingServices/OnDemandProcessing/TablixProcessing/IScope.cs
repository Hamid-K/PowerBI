using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x02000900 RID: 2304
	public interface IScope : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x17002935 RID: 10549
		// (get) Token: 0x06007ECC RID: 32460
		bool TargetForNonDetailSort { get; }

		// Token: 0x17002936 RID: 10550
		// (get) Token: 0x06007ECD RID: 32461
		int[] SortFilterExpressionScopeInfoIndices { get; }

		// Token: 0x17002937 RID: 10551
		// (get) Token: 0x06007ECE RID: 32462
		int Depth { get; }

		// Token: 0x17002938 RID: 10552
		// (get) Token: 0x06007ECF RID: 32463
		IRIFReportScope RIFReportScope { get; }

		// Token: 0x06007ED0 RID: 32464
		bool IsTargetForSort(int index, bool detailSort);

		// Token: 0x06007ED1 RID: 32465
		bool TargetScopeMatched(int index, bool detailSort);

		// Token: 0x06007ED2 RID: 32466
		void GetScopeValues(IReference<IHierarchyObj> targetScopeObj, List<object>[] scopeValues, ref int index);

		// Token: 0x06007ED3 RID: 32467
		void CalculatePreviousAggregates();

		// Token: 0x06007ED4 RID: 32468
		void ReadRow(DataActions dataAction, ITraversalContext context);

		// Token: 0x06007ED5 RID: 32469
		bool InScope(string scope);

		// Token: 0x06007ED6 RID: 32470
		IReference<IScope> GetOuterScope(bool includeSubReportContainingScope);

		// Token: 0x06007ED7 RID: 32471
		string GetScopeName();

		// Token: 0x06007ED8 RID: 32472
		int RecursiveLevel(string scope);

		// Token: 0x06007ED9 RID: 32473
		void GetGroupNameValuePairs(Dictionary<string, object> pairs);

		// Token: 0x06007EDA RID: 32474
		void UpdateAggregates(AggregateUpdateContext context);

		// Token: 0x06007EDB RID: 32475
		void SetupEnvironment();
	}
}
