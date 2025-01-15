using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000C2 RID: 194
	[EditorBrowsable(EditorBrowsableState.Never)]
	[CompatibilityRequirement("Preview")]
	public sealed class TableExcludedArtifactCollection : MetadataObjectCollection<ExcludedArtifact, Table>
	{
		// Token: 0x06000C3A RID: 3130 RVA: 0x00067185 File Offset: 0x00065385
		internal TableExcludedArtifactCollection(Table parent)
			: base(ObjectType.ExcludedArtifact, parent)
		{
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x00067190 File Offset: 0x00065390
		private protected override void CompareWith(MetadataObjectCollection<ExcludedArtifact, Table> other, CopyContext context, IList<ExcludedArtifact> removedItems, IList<ExcludedArtifact> addedItems, IList<KeyValuePair<ExcludedArtifact, ExcludedArtifact>> matchedItems)
		{
			Utils.CompareUniqueObjectCollections<ExcludedArtifact>(this, other, context, removedItems, addedItems, matchedItems, Utils.IsEquivalentExcludedArtifact);
		}
	}
}
