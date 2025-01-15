using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000079 RID: 121
	[CompatibilityRequirement("1400")]
	public sealed class MeasureExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, Measure>
	{
		// Token: 0x060006BE RID: 1726 RVA: 0x00035139 File Offset: 0x00033339
		internal MeasureExtendedPropertyCollection(Measure parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00035146 File Offset: 0x00033346
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, Measure> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
