using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200006C RID: 108
	[CompatibilityRequirement("1400")]
	public sealed class KPIExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, KPI>
	{
		// Token: 0x060005D8 RID: 1496 RVA: 0x0002D1A6 File Offset: 0x0002B3A6
		internal KPIExtendedPropertyCollection(KPI parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0002D1B3 File Offset: 0x0002B3B3
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, KPI> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
