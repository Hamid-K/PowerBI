using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000A8 RID: 168
	[CompatibilityRequirement("1400")]
	public sealed class PerspectiveTableExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, PerspectiveTable>
	{
		// Token: 0x06000A43 RID: 2627 RVA: 0x000552F5 File Offset: 0x000534F5
		internal PerspectiveTableExtendedPropertyCollection(PerspectiveTable parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00055302 File Offset: 0x00053502
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, PerspectiveTable> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
