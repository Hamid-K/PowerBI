using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000067 RID: 103
	[EditorBrowsable(EditorBrowsableState.Never)]
	[CompatibilityRequirement("Preview")]
	public sealed class HierarchyExcludedArtifactCollection : MetadataObjectCollection<ExcludedArtifact, Hierarchy>
	{
		// Token: 0x06000590 RID: 1424 RVA: 0x0002AC6D File Offset: 0x00028E6D
		internal HierarchyExcludedArtifactCollection(Hierarchy parent)
			: base(ObjectType.ExcludedArtifact, parent)
		{
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0002AC78 File Offset: 0x00028E78
		private protected override void CompareWith(MetadataObjectCollection<ExcludedArtifact, Hierarchy> other, CopyContext context, IList<ExcludedArtifact> removedItems, IList<ExcludedArtifact> addedItems, IList<KeyValuePair<ExcludedArtifact, ExcludedArtifact>> matchedItems)
		{
			Utils.CompareUniqueObjectCollections<ExcludedArtifact>(this, other, context, removedItems, addedItems, matchedItems, Utils.IsEquivalentExcludedArtifact);
		}
	}
}
