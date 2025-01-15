using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000062 RID: 98
	[CompatibilityRequirement(Pbi = "1400")]
	public sealed class GroupByColumnCollection : MetadataObjectCollection<GroupByColumn, RelatedColumnDetails>
	{
		// Token: 0x0600053F RID: 1343 RVA: 0x000277C2 File Offset: 0x000259C2
		internal GroupByColumnCollection(RelatedColumnDetails parent)
			: base(ObjectType.GroupByColumn, parent)
		{
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000277CD File Offset: 0x000259CD
		private protected override void CompareWith(MetadataObjectCollection<GroupByColumn, RelatedColumnDetails> other, CopyContext context, IList<GroupByColumn> removedItems, IList<GroupByColumn> addedItems, IList<KeyValuePair<GroupByColumn, GroupByColumn>> matchedItems)
		{
			Utils.CompareLinkedObjectCollections<GroupByColumn>(this, other, context, true, removedItems, addedItems, matchedItems);
		}
	}
}
