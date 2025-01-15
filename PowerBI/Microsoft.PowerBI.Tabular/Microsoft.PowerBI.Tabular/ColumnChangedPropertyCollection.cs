using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000044 RID: 68
	[CompatibilityRequirement("1567")]
	public sealed class ColumnChangedPropertyCollection : MetadataObjectCollection<ChangedProperty, Column>
	{
		// Token: 0x060002EE RID: 750 RVA: 0x00017965 File Offset: 0x00015B65
		internal ColumnChangedPropertyCollection(Column parent)
			: base(ObjectType.ChangedProperty, parent)
		{
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00017970 File Offset: 0x00015B70
		private protected override void CompareWith(MetadataObjectCollection<ChangedProperty, Column> other, CopyContext context, IList<ChangedProperty> removedItems, IList<ChangedProperty> addedItems, IList<KeyValuePair<ChangedProperty, ChangedProperty>> matchedItems)
		{
			Utils.CompareUniqueObjectCollections<ChangedProperty>(this, other, context, removedItems, addedItems, matchedItems, Utils.IsEquivalentChangedProperty);
		}
	}
}
