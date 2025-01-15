using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000A4 RID: 164
	[CompatibilityRequirement(Pbi = "1400")]
	public sealed class PerspectiveSetExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, PerspectiveSet>
	{
		// Token: 0x06000A06 RID: 2566 RVA: 0x00053698 File Offset: 0x00051898
		internal PerspectiveSetExtendedPropertyCollection(PerspectiveSet parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x000536A5 File Offset: 0x000518A5
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, PerspectiveSet> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
