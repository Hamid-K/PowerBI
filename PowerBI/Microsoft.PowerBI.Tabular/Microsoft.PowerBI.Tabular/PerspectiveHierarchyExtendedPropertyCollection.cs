using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200009C RID: 156
	[CompatibilityRequirement("1400")]
	public sealed class PerspectiveHierarchyExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, PerspectiveHierarchy>
	{
		// Token: 0x06000997 RID: 2455 RVA: 0x000512E8 File Offset: 0x0004F4E8
		internal PerspectiveHierarchyExtendedPropertyCollection(PerspectiveHierarchy parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x000512F5 File Offset: 0x0004F4F5
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, PerspectiveHierarchy> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
