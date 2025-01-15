using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200004E RID: 78
	[CompatibilityRequirement("1400")]
	public sealed class CultureExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, Culture>
	{
		// Token: 0x06000384 RID: 900 RVA: 0x0001C167 File Offset: 0x0001A367
		internal CultureExtendedPropertyCollection(Culture parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001C174 File Offset: 0x0001A374
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, Culture> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
