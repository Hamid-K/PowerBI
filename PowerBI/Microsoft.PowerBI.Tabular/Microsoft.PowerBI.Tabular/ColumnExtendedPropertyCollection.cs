using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000046 RID: 70
	[CompatibilityRequirement("1400")]
	public sealed class ColumnExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, Column>
	{
		// Token: 0x060002FD RID: 765 RVA: 0x0001804C File Offset: 0x0001624C
		internal ColumnExtendedPropertyCollection(Column parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00018059 File Offset: 0x00016259
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, Column> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
