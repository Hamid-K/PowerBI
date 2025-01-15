using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000098 RID: 152
	[CompatibilityRequirement("1400")]
	public sealed class PerspectiveExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, Perspective>
	{
		// Token: 0x06000960 RID: 2400 RVA: 0x0005019B File Offset: 0x0004E39B
		internal PerspectiveExtendedPropertyCollection(Perspective parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x000501A8 File Offset: 0x0004E3A8
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, Perspective> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
