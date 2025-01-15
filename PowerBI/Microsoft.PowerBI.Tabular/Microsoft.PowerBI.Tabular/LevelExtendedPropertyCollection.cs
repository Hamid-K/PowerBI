using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000071 RID: 113
	[CompatibilityRequirement("1400")]
	public sealed class LevelExtendedPropertyCollection : NamedMetadataObjectCollection<ExtendedProperty, Level>
	{
		// Token: 0x06000624 RID: 1572 RVA: 0x0002F5ED File Offset: 0x0002D7ED
		internal LevelExtendedPropertyCollection(Level parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ExtendedProperty, parent, comparer, true)
		{
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0002F5FA File Offset: 0x0002D7FA
		private protected override void CompareWith(MetadataObjectCollection<ExtendedProperty, Level> other, CopyContext context, IList<ExtendedProperty> removedItems, IList<ExtendedProperty> addedItems, IList<KeyValuePair<ExtendedProperty, ExtendedProperty>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<ExtendedProperty>(removedItems, addedItems, matchedItems, ExtendedProperty.CompareExtendedPropertyType);
		}
	}
}
