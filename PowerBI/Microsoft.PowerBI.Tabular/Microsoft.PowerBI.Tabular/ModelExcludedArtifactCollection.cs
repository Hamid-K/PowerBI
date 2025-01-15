using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200007C RID: 124
	[EditorBrowsable(EditorBrowsableState.Never)]
	[CompatibilityRequirement("Preview")]
	public sealed class ModelExcludedArtifactCollection : MetadataObjectCollection<ExcludedArtifact, Model>
	{
		// Token: 0x06000774 RID: 1908 RVA: 0x0004047D File Offset: 0x0003E67D
		internal ModelExcludedArtifactCollection(Model parent)
			: base(ObjectType.ExcludedArtifact, parent)
		{
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00040488 File Offset: 0x0003E688
		private protected override void CompareWith(MetadataObjectCollection<ExcludedArtifact, Model> other, CopyContext context, IList<ExcludedArtifact> removedItems, IList<ExcludedArtifact> addedItems, IList<KeyValuePair<ExcludedArtifact, ExcludedArtifact>> matchedItems)
		{
			Utils.CompareUniqueObjectCollections<ExcludedArtifact>(this, other, context, removedItems, addedItems, matchedItems, Utils.IsEquivalentExcludedArtifact);
		}
	}
}
