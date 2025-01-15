using System;
using Microsoft.InfoNav.Common;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x02000247 RID: 583
	public interface IConceptualModel
	{
		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x060019C0 RID: 6592
		EdmMeasureInstance? DefaultMeasure { get; }

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x060019C1 RID: 6593
		DaxCapabilities DaxCapabilities { get; }

		// Token: 0x060019C2 RID: 6594
		IDirectedGraph<EntitySet> GetAssociationsFromOneWithBidirCrossFilteringGraph(bool includeDirectManyToMany);

		// Token: 0x060019C3 RID: 6595
		IDirectedGraph<EntitySet> GetAssociationsFromOneGraph(bool includeDirectManyToMany);

		// Token: 0x060019C4 RID: 6596
		bool DiscourageCountRowsOverTables();
	}
}
