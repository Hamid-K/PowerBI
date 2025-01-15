using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200000E RID: 14
	internal interface IFromManager
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000029 RID: 41
		IConceptualSchema ConceptualSchema { get; }

		// Token: 0x0600002A RID: 42
		EntitySource GetOrAddEntitySource(string entityName);

		// Token: 0x0600002B RID: 43
		string RewritePropertyName(EntitySource entitySource, string propertyName, out bool isMeasure);

		// Token: 0x0600002C RID: 44
		string RewriteHierarchyName(EntitySource entitySource, string hierarchyName);

		// Token: 0x0600002D RID: 45
		string RewriteHierarchyLevelName(EntitySource entitySource, string hierarchyName, string hierarchyLevelName);
	}
}
