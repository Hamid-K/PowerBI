using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000089 RID: 137
	[EditorBrowsable(EditorBrowsableState.Never)]
	[CompatibilityRequirement("Preview")]
	public sealed class NamedExpressionExcludedArtifactCollection : MetadataObjectCollection<ExcludedArtifact, NamedExpression>
	{
		// Token: 0x0600083B RID: 2107 RVA: 0x0004702D File Offset: 0x0004522D
		internal NamedExpressionExcludedArtifactCollection(NamedExpression parent)
			: base(ObjectType.ExcludedArtifact, parent)
		{
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00047038 File Offset: 0x00045238
		private protected override void CompareWith(MetadataObjectCollection<ExcludedArtifact, NamedExpression> other, CopyContext context, IList<ExcludedArtifact> removedItems, IList<ExcludedArtifact> addedItems, IList<KeyValuePair<ExcludedArtifact, ExcludedArtifact>> matchedItems)
		{
			Utils.CompareUniqueObjectCollections<ExcludedArtifact>(this, other, context, removedItems, addedItems, matchedItems, Utils.IsEquivalentExcludedArtifact);
		}
	}
}
