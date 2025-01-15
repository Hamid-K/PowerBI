using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200002F RID: 47
	[CompatibilityRequirement("1400")]
	public sealed class AttributeHierarchyExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, AttributeHierarchy>
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00005D9A File Offset: 0x00003F9A
		internal AttributeHierarchyExtendedPropertyCollection(AttributeHierarchy parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005DA7 File Offset: 0x00003FA7
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, AttributeHierarchy> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
