using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000097 RID: 151
	[CompatibilityRequirement("1400")]
	public sealed class PerspectiveColumnExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, PerspectiveColumn>
	{
		// Token: 0x0600095E RID: 2398 RVA: 0x00050170 File Offset: 0x0004E370
		internal PerspectiveColumnExtendedPropertyCollection(PerspectiveColumn parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0005017D File Offset: 0x0004E37D
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, PerspectiveColumn> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
