using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.Common;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200018C RID: 396
	internal sealed class RollupAddIsSubtotalGroupItem : IGroupItem, IEquatable<IGroupItem>
	{
		// Token: 0x06001542 RID: 5442 RVA: 0x0003BC77 File Offset: 0x00039E77
		internal RollupAddIsSubtotalGroupItem(IReadOnlyList<NamedRollupGroupItem> groupItems, IReadOnlyList<QueryExpression> contextTables)
		{
			this.GroupItems = groupItems;
			this.ContextTables = contextTables;
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001543 RID: 5443 RVA: 0x0003BC8D File Offset: 0x00039E8D
		public IReadOnlyList<NamedRollupGroupItem> GroupItems { get; }

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001544 RID: 5444 RVA: 0x0003BC95 File Offset: 0x00039E95
		public IReadOnlyList<QueryExpression> ContextTables { get; }

		// Token: 0x06001545 RID: 5445 RVA: 0x0003BC9D File Offset: 0x00039E9D
		public IEnumerable<KeyValuePair<string, QueryExpression>> GetGroupKeys()
		{
			return this.GroupItems.SelectMany((NamedRollupGroupItem g) => g.GetGroupKeys());
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x0003BCC9 File Offset: 0x00039EC9
		public override bool Equals(object obj)
		{
			return this.Equals(obj as IGroupItem);
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x0003BCD8 File Offset: 0x00039ED8
		public bool Equals(IGroupItem other)
		{
			RollupAddIsSubtotalGroupItem rollupAddIsSubtotalGroupItem = other as RollupAddIsSubtotalGroupItem;
			return rollupAddIsSubtotalGroupItem != null && rollupAddIsSubtotalGroupItem.GroupItems.SequenceEqual(this.GroupItems) && rollupAddIsSubtotalGroupItem.ContextTables.SequenceEqual(this.ContextTables);
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x0003BD17 File Offset: 0x00039F17
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.GroupItems.GetHashCode(), Hashing.GetHashCode<IReadOnlyList<QueryExpression>>(this.ContextTables, null));
		}
	}
}
