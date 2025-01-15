using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000C7 RID: 199
	[CompatibilityRequirement("1400")]
	public sealed class TablePermissionExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, TablePermission>
	{
		// Token: 0x06000C7D RID: 3197 RVA: 0x00069296 File Offset: 0x00067496
		internal TablePermissionExtendedPropertyCollection(TablePermission parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x000692A3 File Offset: 0x000674A3
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, TablePermission> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
