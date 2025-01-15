using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000B4 RID: 180
	public sealed class RelationshipCollection : NamedMetadataObjectCollection<Relationship, Model>
	{
		// Token: 0x06000B3B RID: 2875 RVA: 0x0005C418 File Offset: 0x0005A618
		internal RelationshipCollection(Model parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Relationship, parent, comparer, true)
		{
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0005C424 File Offset: 0x0005A624
		private protected override void CompareWith(MetadataObjectCollection<Relationship, Model> other, CopyContext context, IList<Relationship> removedItems, IList<Relationship> addedItems, IList<KeyValuePair<Relationship, Relationship>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<Relationship>(removedItems, addedItems, matchedItems, Relationship.CompareRelationshipType);
		}
	}
}
