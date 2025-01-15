using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000CD RID: 205
	[CompatibilityRequirement("1400")]
	public sealed class VariationExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, Variation>
	{
		// Token: 0x06000CF4 RID: 3316 RVA: 0x0006C3A1 File Offset: 0x0006A5A1
		internal VariationExtendedPropertyCollection(Variation parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x0006C3AE File Offset: 0x0006A5AE
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, Variation> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
