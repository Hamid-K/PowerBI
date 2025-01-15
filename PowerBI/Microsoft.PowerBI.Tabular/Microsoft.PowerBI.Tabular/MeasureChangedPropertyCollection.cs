using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000077 RID: 119
	[CompatibilityRequirement("1567")]
	public sealed class MeasureChangedPropertyCollection : MetadataObjectCollection<ChangedProperty, Measure>
	{
		// Token: 0x060006B0 RID: 1712 RVA: 0x00034F14 File Offset: 0x00033114
		internal MeasureChangedPropertyCollection(Measure parent)
			: base(ObjectType.ChangedProperty, parent)
		{
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00034F1F File Offset: 0x0003311F
		private protected override void CompareWith(MetadataObjectCollection<ChangedProperty, Measure> other, CopyContext context, IList<ChangedProperty> removedItems, IList<ChangedProperty> addedItems, IList<KeyValuePair<ChangedProperty, ChangedProperty>> matchedItems)
		{
			Utils.CompareUniqueObjectCollections<ChangedProperty>(this, other, context, removedItems, addedItems, matchedItems, Utils.IsEquivalentChangedProperty);
		}
	}
}
