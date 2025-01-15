using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000068 RID: 104
	[CompatibilityRequirement("1400")]
	public sealed class HierarchyExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, Hierarchy>
	{
		// Token: 0x06000592 RID: 1426 RVA: 0x0002AC8C File Offset: 0x00028E8C
		internal HierarchyExtendedPropertyCollection(Hierarchy parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0002AC99 File Offset: 0x00028E99
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, Hierarchy> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
