using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.Common;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200018B RID: 395
	internal sealed class NamedRollupGroupItem : IGroupItem, IEquatable<IGroupItem>
	{
		// Token: 0x0600153A RID: 5434 RVA: 0x0003BBAB File Offset: 0x00039DAB
		internal NamedRollupGroupItem(CompositeKeyGroupItem groupKeys, string subtotalIndicatorColumnName, IReadOnlyList<QueryExpression> contextTables)
		{
			this.GroupKeysItem = groupKeys;
			this.SubtotalIndicatorColumnName = subtotalIndicatorColumnName;
			this.ContextTables = contextTables;
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x0003BBC8 File Offset: 0x00039DC8
		public CompositeKeyGroupItem GroupKeysItem { get; }

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x0003BBD0 File Offset: 0x00039DD0
		public string SubtotalIndicatorColumnName { get; }

		// Token: 0x0600153D RID: 5437 RVA: 0x0003BBD8 File Offset: 0x00039DD8
		public IEnumerable<KeyValuePair<string, QueryExpression>> GetGroupKeys()
		{
			return this.GroupKeysItem.GetGroupKeys();
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x0003BBE5 File Offset: 0x00039DE5
		public IReadOnlyList<QueryExpression> ContextTables { get; }

		// Token: 0x0600153F RID: 5439 RVA: 0x0003BBED File Offset: 0x00039DED
		public override bool Equals(object obj)
		{
			return this.Equals(obj as IGroupItem);
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0003BBFC File Offset: 0x00039DFC
		public bool Equals(IGroupItem other)
		{
			NamedRollupGroupItem namedRollupGroupItem = other as NamedRollupGroupItem;
			return namedRollupGroupItem != null && (this.SubtotalIndicatorColumnName == namedRollupGroupItem.SubtotalIndicatorColumnName && namedRollupGroupItem.GroupKeysItem.Equals(this.GroupKeysItem)) && this.ContextTables.SequenceEqual(namedRollupGroupItem.ContextTables);
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x0003BC4E File Offset: 0x00039E4E
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.GroupKeysItem.GetHashCode(), this.SubtotalIndicatorColumnName.GetHashCode(), Hashing.GetHashCode<IReadOnlyList<QueryExpression>>(this.ContextTables, null));
		}
	}
}
