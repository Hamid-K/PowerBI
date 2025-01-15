using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000B5 RID: 181
	[CompatibilityRequirement("1400")]
	public sealed class RelationshipExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, Relationship>
	{
		// Token: 0x06000B3D RID: 2877 RVA: 0x0005C442 File Offset: 0x0005A642
		internal RelationshipExtendedPropertyCollection(Relationship parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0005C44F File Offset: 0x0005A64F
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, Relationship> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
