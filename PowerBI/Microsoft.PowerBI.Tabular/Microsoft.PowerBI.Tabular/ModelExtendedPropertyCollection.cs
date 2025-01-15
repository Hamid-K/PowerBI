using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200007D RID: 125
	[CompatibilityRequirement("1400")]
	public sealed class ModelExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, Model>
	{
		// Token: 0x06000776 RID: 1910 RVA: 0x0004049C File Offset: 0x0003E69C
		internal ModelExtendedPropertyCollection(Model parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x000404A9 File Offset: 0x0003E6A9
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, Model> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
