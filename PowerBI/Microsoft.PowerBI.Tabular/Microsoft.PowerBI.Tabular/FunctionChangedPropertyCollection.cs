using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200005E RID: 94
	[EditorBrowsable(EditorBrowsableState.Never)]
	[CompatibilityRequirement("Internal")]
	public sealed class FunctionChangedPropertyCollection : MetadataObjectCollection<ChangedProperty, Function>
	{
		// Token: 0x06000503 RID: 1283 RVA: 0x00026A58 File Offset: 0x00024C58
		internal FunctionChangedPropertyCollection(Function parent)
			: base(ObjectType.ChangedProperty, parent)
		{
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00026A63 File Offset: 0x00024C63
		private protected override void CompareWith(MetadataObjectCollection<ChangedProperty, Function> other, CopyContext context, IList<ChangedProperty> removedItems, IList<ChangedProperty> addedItems, IList<KeyValuePair<ChangedProperty, ChangedProperty>> matchedItems)
		{
			Utils.CompareUniqueObjectCollections<ChangedProperty>(this, other, context, removedItems, addedItems, matchedItems, Utils.IsEquivalentChangedProperty);
		}
	}
}
