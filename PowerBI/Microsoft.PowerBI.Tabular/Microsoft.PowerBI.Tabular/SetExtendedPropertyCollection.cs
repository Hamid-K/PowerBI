using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000BA RID: 186
	[CompatibilityRequirement(Pbi = "1400")]
	public sealed class SetExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, Set>
	{
		// Token: 0x06000B80 RID: 2944 RVA: 0x0005E74E File Offset: 0x0005C94E
		internal SetExtendedPropertyCollection(Set parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0005E75B File Offset: 0x0005C95B
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, Set> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
