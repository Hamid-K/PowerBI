using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000049 RID: 73
	[CompatibilityRequirement("1400")]
	public sealed class ColumnPermissionCollection : NamedMetadataObjectCollection<ColumnPermission, TablePermission>
	{
		// Token: 0x06000335 RID: 821 RVA: 0x0001968C File Offset: 0x0001788C
		internal ColumnPermissionCollection(TablePermission parent, IEqualityComparer<string> comparer)
			: base(ObjectType.ColumnPermission, parent, comparer, true)
		{
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00019699 File Offset: 0x00017899
		private protected override void CompareWith(MetadataObjectCollection<ColumnPermission, TablePermission> other, CopyContext context, IList<ColumnPermission> removedItems, IList<ColumnPermission> addedItems, IList<KeyValuePair<ColumnPermission, ColumnPermission>> matchedItems)
		{
			Utils.CompareLinkedObjectCollections<ColumnPermission>(this, other, context, true, removedItems, addedItems, matchedItems);
		}
	}
}
