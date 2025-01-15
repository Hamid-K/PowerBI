using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200009B RID: 155
	public sealed class PerspectiveHierarchyCollection : NamedMetadataObjectCollection<PerspectiveHierarchy, PerspectiveTable>
	{
		// Token: 0x06000995 RID: 2453 RVA: 0x000512CB File Offset: 0x0004F4CB
		internal PerspectiveHierarchyCollection(PerspectiveTable parent, IEqualityComparer<string> comparer)
			: base(ObjectType.PerspectiveHierarchy, parent, comparer, true)
		{
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x000512D8 File Offset: 0x0004F4D8
		private protected override void CompareWith(MetadataObjectCollection<PerspectiveHierarchy, PerspectiveTable> other, CopyContext context, IList<PerspectiveHierarchy> removedItems, IList<PerspectiveHierarchy> addedItems, IList<KeyValuePair<PerspectiveHierarchy, PerspectiveHierarchy>> matchedItems)
		{
			Utils.CompareLinkedObjectCollections<PerspectiveHierarchy>(this, other, context, true, removedItems, addedItems, matchedItems);
		}
	}
}
