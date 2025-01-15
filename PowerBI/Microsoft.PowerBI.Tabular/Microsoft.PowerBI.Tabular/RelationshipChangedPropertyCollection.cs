using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000B3 RID: 179
	[CompatibilityRequirement("1567")]
	public sealed class RelationshipChangedPropertyCollection : MetadataObjectCollection<ChangedProperty, Relationship>
	{
		// Token: 0x06000B39 RID: 2873 RVA: 0x0005C3F9 File Offset: 0x0005A5F9
		internal RelationshipChangedPropertyCollection(Relationship parent)
			: base(ObjectType.ChangedProperty, parent)
		{
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0005C404 File Offset: 0x0005A604
		private protected override void CompareWith(MetadataObjectCollection<ChangedProperty, Relationship> other, CopyContext context, IList<ChangedProperty> removedItems, IList<ChangedProperty> addedItems, IList<KeyValuePair<ChangedProperty, ChangedProperty>> matchedItems)
		{
			Utils.CompareUniqueObjectCollections<ChangedProperty>(this, other, context, removedItems, addedItems, matchedItems, Utils.IsEquivalentChangedProperty);
		}
	}
}
