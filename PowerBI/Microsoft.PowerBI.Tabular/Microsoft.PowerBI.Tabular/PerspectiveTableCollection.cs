using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000A7 RID: 167
	public sealed class PerspectiveTableCollection : NamedMetadataObjectCollection<PerspectiveTable, Perspective>
	{
		// Token: 0x06000A41 RID: 2625 RVA: 0x000552D8 File Offset: 0x000534D8
		internal PerspectiveTableCollection(Perspective parent, IEqualityComparer<string> comparer)
			: base(ObjectType.PerspectiveTable, parent, comparer, true)
		{
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x000552E5 File Offset: 0x000534E5
		private protected override void CompareWith(MetadataObjectCollection<PerspectiveTable, Perspective> other, CopyContext context, IList<PerspectiveTable> removedItems, IList<PerspectiveTable> addedItems, IList<KeyValuePair<PerspectiveTable, PerspectiveTable>> matchedItems)
		{
			Utils.CompareLinkedObjectCollections<PerspectiveTable>(this, other, context, true, removedItems, addedItems, matchedItems);
		}
	}
}
