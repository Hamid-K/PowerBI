using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000A3 RID: 163
	[CompatibilityRequirement(Pbi = "1400")]
	public sealed class PerspectiveSetCollection : NamedMetadataObjectCollection<PerspectiveSet, PerspectiveTable>
	{
		// Token: 0x06000A04 RID: 2564 RVA: 0x0005367B File Offset: 0x0005187B
		internal PerspectiveSetCollection(PerspectiveTable parent, IEqualityComparer<string> comparer)
			: base(ObjectType.PerspectiveSet, parent, comparer, true)
		{
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00053688 File Offset: 0x00051888
		private protected override void CompareWith(MetadataObjectCollection<PerspectiveSet, PerspectiveTable> other, CopyContext context, IList<PerspectiveSet> removedItems, IList<PerspectiveSet> addedItems, IList<KeyValuePair<PerspectiveSet, PerspectiveSet>> matchedItems)
		{
			Utils.CompareLinkedObjectCollections<PerspectiveSet>(this, other, context, true, removedItems, addedItems, matchedItems);
		}
	}
}
