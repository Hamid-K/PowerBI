using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000AF RID: 175
	[CompatibilityRequirement("1450")]
	public sealed class RefreshPolicyExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, RefreshPolicy>
	{
		// Token: 0x06000ABC RID: 2748 RVA: 0x00058644 File Offset: 0x00056844
		internal RefreshPolicyExtendedPropertyCollection(RefreshPolicy parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00058651 File Offset: 0x00056851
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, RefreshPolicy> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
