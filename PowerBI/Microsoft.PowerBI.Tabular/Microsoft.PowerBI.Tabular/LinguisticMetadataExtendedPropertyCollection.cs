using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000074 RID: 116
	[CompatibilityRequirement("1400")]
	public sealed class LinguisticMetadataExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, LinguisticMetadata>
	{
		// Token: 0x06000658 RID: 1624 RVA: 0x00030B31 File Offset: 0x0002ED31
		internal LinguisticMetadataExtendedPropertyCollection(LinguisticMetadata parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00030B3E File Offset: 0x0002ED3E
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, LinguisticMetadata> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
