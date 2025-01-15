using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000037 RID: 55
	internal interface IRdlReportItemConverter
	{
		// Token: 0x06000182 RID: 386
		bool SupportsTargetedScopeFilters(PVVisual visual);

		// Token: 0x06000183 RID: 387
		void Load(IReportDeserializationContext ctx, ReportItem reportItem, PVVisual visual);

		// Token: 0x06000184 RID: 388
		void SetProperties(PVVisual visual);

		// Token: 0x06000185 RID: 389
		void SetOutputValues(IReportDeserializationContext ctx, PVVisual visual, ReportItem reportItem);

		// Token: 0x06000186 RID: 390
		PVVisual GetParentForFilter(PVVisual visual, Filter filter);

		// Token: 0x06000187 RID: 391
		void SetDrill(PVVisual topLevelVisual, List<Filter> drillDownFilters);

		// Token: 0x06000188 RID: 392
		bool IsEnabled();
	}
}
