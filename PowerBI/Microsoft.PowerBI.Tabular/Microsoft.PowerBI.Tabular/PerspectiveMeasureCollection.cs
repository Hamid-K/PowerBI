using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200009F RID: 159
	public sealed class PerspectiveMeasureCollection : NamedMetadataObjectCollection<PerspectiveMeasure, PerspectiveTable>
	{
		// Token: 0x060009CC RID: 2508 RVA: 0x00052403 File Offset: 0x00050603
		internal PerspectiveMeasureCollection(PerspectiveTable parent, IEqualityComparer<string> comparer)
			: base(ObjectType.PerspectiveMeasure, parent, comparer, true)
		{
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00052410 File Offset: 0x00050610
		private protected override void CompareWith(MetadataObjectCollection<PerspectiveMeasure, PerspectiveTable> other, CopyContext context, IList<PerspectiveMeasure> removedItems, IList<PerspectiveMeasure> addedItems, IList<KeyValuePair<PerspectiveMeasure, PerspectiveMeasure>> matchedItems)
		{
			Utils.CompareLinkedObjectCollections<PerspectiveMeasure>(this, other, context, true, removedItems, addedItems, matchedItems);
		}
	}
}
