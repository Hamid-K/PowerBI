using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004D7 RID: 1239
	public interface IInScopeEventSource : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IGloballyReferenceable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IGlobalIDOwner
	{
		// Token: 0x17001A95 RID: 6805
		// (get) Token: 0x06003E92 RID: 16018
		Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType { get; }

		// Token: 0x17001A96 RID: 6806
		// (get) Token: 0x06003E93 RID: 16019
		string Name { get; }

		// Token: 0x17001A97 RID: 6807
		// (get) Token: 0x06003E94 RID: 16020
		ReportItem Parent { get; }

		// Token: 0x17001A98 RID: 6808
		// (get) Token: 0x06003E95 RID: 16021
		EndUserSort UserSort { get; }

		// Token: 0x17001A99 RID: 6809
		// (get) Token: 0x06003E96 RID: 16022
		List<InstancePathItem> InstancePath { get; }

		// Token: 0x17001A9A RID: 6810
		// (get) Token: 0x06003E97 RID: 16023
		// (set) Token: 0x06003E98 RID: 16024
		GroupingList ContainingScopes { get; set; }

		// Token: 0x17001A9B RID: 6811
		// (get) Token: 0x06003E99 RID: 16025
		// (set) Token: 0x06003E9A RID: 16026
		string Scope { get; set; }

		// Token: 0x17001A9C RID: 6812
		// (get) Token: 0x06003E9B RID: 16027
		// (set) Token: 0x06003E9C RID: 16028
		bool IsTablixCellScope { get; set; }

		// Token: 0x17001A9D RID: 6813
		// (get) Token: 0x06003E9D RID: 16029
		// (set) Token: 0x06003E9E RID: 16030
		bool IsDetailScope { get; set; }

		// Token: 0x17001A9E RID: 6814
		// (get) Token: 0x06003E9F RID: 16031
		// (set) Token: 0x06003EA0 RID: 16032
		bool IsSubReportTopLevelScope { get; set; }

		// Token: 0x06003EA1 RID: 16033
		List<int> GetPeerSortFilters(bool create);

		// Token: 0x06003EA2 RID: 16034
		InScopeSortFilterHashtable GetSortFiltersInScope(bool create, bool inDetail);

		// Token: 0x17001A9F RID: 6815
		// (get) Token: 0x06003EA3 RID: 16035
		// (set) Token: 0x06003EA4 RID: 16036
		InitializationContext.ScopeChainInfo ScopeChainInfo { get; set; }
	}
}
