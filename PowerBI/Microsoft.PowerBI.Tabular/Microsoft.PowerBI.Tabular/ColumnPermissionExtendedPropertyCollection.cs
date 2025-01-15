using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200004A RID: 74
	[CompatibilityRequirement("1400")]
	public sealed class ColumnPermissionExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, ColumnPermission>
	{
		// Token: 0x06000337 RID: 823 RVA: 0x000196A9 File Offset: 0x000178A9
		internal ColumnPermissionExtendedPropertyCollection(ColumnPermission parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x06000338 RID: 824 RVA: 0x000196B6 File Offset: 0x000178B6
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, ColumnPermission> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
