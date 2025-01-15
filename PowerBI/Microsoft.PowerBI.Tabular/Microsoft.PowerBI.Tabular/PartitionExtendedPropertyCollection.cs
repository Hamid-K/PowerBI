using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000090 RID: 144
	[CompatibilityRequirement("1400")]
	public sealed class PartitionExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, Partition>
	{
		// Token: 0x060008FA RID: 2298 RVA: 0x0004DEFE File Offset: 0x0004C0FE
		internal PartitionExtendedPropertyCollection(Partition parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0004DF0B File Offset: 0x0004C10B
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, Partition> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
