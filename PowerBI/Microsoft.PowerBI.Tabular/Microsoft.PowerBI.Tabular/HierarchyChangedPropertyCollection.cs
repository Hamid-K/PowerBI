using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000065 RID: 101
	[CompatibilityRequirement("1567")]
	public sealed class HierarchyChangedPropertyCollection : MetadataObjectCollection<ChangedProperty, Hierarchy>
	{
		// Token: 0x06000582 RID: 1410 RVA: 0x0002AA4A File Offset: 0x00028C4A
		internal HierarchyChangedPropertyCollection(Hierarchy parent)
			: base(ObjectType.ChangedProperty, parent)
		{
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0002AA55 File Offset: 0x00028C55
		private protected override void CompareWith(MetadataObjectCollection<ChangedProperty, Hierarchy> other, CopyContext context, IList<ChangedProperty> removedItems, IList<ChangedProperty> addedItems, IList<KeyValuePair<ChangedProperty, ChangedProperty>> matchedItems)
		{
			Utils.CompareUniqueObjectCollections<ChangedProperty>(this, other, context, removedItems, addedItems, matchedItems, Utils.IsEquivalentChangedProperty);
		}
	}
}
