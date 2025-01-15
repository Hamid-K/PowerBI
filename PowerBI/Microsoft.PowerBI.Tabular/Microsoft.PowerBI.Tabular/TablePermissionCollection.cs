using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000C6 RID: 198
	public sealed class TablePermissionCollection : NamedMetadataObjectCollection<TablePermission, ModelRole>
	{
		// Token: 0x06000C7B RID: 3195 RVA: 0x00069279 File Offset: 0x00067479
		internal TablePermissionCollection(ModelRole parent, IEqualityComparer<string> comparer)
			: base(ObjectType.TablePermission, parent, comparer, true)
		{
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00069286 File Offset: 0x00067486
		private protected override void CompareWith(MetadataObjectCollection<TablePermission, ModelRole> other, CopyContext context, IList<TablePermission> removedItems, IList<TablePermission> addedItems, IList<KeyValuePair<TablePermission, TablePermission>> matchedItems)
		{
			Utils.CompareLinkedObjectCollections<TablePermission>(this, other, context, true, removedItems, addedItems, matchedItems);
		}
	}
}
