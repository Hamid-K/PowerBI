using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000C0 RID: 192
	[CompatibilityRequirement("1567")]
	public sealed class TableChangedPropertyCollection : MetadataObjectCollection<ChangedProperty, Table>
	{
		// Token: 0x06000C2C RID: 3116 RVA: 0x00066F61 File Offset: 0x00065161
		internal TableChangedPropertyCollection(Table parent)
			: base(ObjectType.ChangedProperty, parent)
		{
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x00066F6C File Offset: 0x0006516C
		private protected override void CompareWith(MetadataObjectCollection<ChangedProperty, Table> other, CopyContext context, IList<ChangedProperty> removedItems, IList<ChangedProperty> addedItems, IList<KeyValuePair<ChangedProperty, ChangedProperty>> matchedItems)
		{
			Utils.CompareUniqueObjectCollections<ChangedProperty>(this, other, context, removedItems, addedItems, matchedItems, Utils.IsEquivalentChangedProperty);
		}
	}
}
