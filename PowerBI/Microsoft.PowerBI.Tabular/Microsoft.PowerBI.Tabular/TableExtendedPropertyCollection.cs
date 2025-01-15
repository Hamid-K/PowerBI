using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000C3 RID: 195
	[CompatibilityRequirement("1400")]
	public sealed class TableExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, Table>
	{
		// Token: 0x06000C3C RID: 3132 RVA: 0x000671A4 File Offset: 0x000653A4
		internal TableExtendedPropertyCollection(Table parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x000671B1 File Offset: 0x000653B1
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, Table> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
