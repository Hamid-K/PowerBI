using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000096 RID: 150
	public sealed class PerspectiveColumnCollection : NamedMetadataObjectCollection<PerspectiveColumn, PerspectiveTable>
	{
		// Token: 0x0600095C RID: 2396 RVA: 0x00050153 File Offset: 0x0004E353
		internal PerspectiveColumnCollection(PerspectiveTable parent, IEqualityComparer<string> comparer)
			: base(ObjectType.PerspectiveColumn, parent, comparer, true)
		{
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00050160 File Offset: 0x0004E360
		private protected override void CompareWith(MetadataObjectCollection<PerspectiveColumn, PerspectiveTable> other, CopyContext context, IList<PerspectiveColumn> removedItems, IList<PerspectiveColumn> addedItems, IList<KeyValuePair<PerspectiveColumn, PerspectiveColumn>> matchedItems)
		{
			Utils.CompareLinkedObjectCollections<PerspectiveColumn>(this, other, context, true, removedItems, addedItems, matchedItems);
		}
	}
}
