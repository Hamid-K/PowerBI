using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000189 RID: 393
	internal sealed class QueryGroupAndJoinExpression : QueryExtensionExpressionBase
	{
		// Token: 0x0600152D RID: 5421 RVA: 0x0003B9F5 File Offset: 0x00039BF5
		internal QueryGroupAndJoinExpression(ConceptualResultType conceptualResultType, IEnumerable<IGroupItem> groupItems, IEnumerable<QueryGroupAndJoinAdditionalColumn> aggregates, IEnumerable<QueryExpression> queryExpressions)
			: base(conceptualResultType)
		{
			this.GroupItems = groupItems.ToReadOnlyCollection<IGroupItem>();
			this.AdditionalColumns = aggregates.ToReadOnlyCollection<QueryGroupAndJoinAdditionalColumn>();
			this.ContextTables = queryExpressions.ToReadOnlyCollection<QueryExpression>();
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x0003BA23 File Offset: 0x00039C23
		public ReadOnlyCollection<IGroupItem> GroupItems { get; }

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x0003BA2B File Offset: 0x00039C2B
		public ReadOnlyCollection<QueryGroupAndJoinAdditionalColumn> AdditionalColumns { get; }

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x0003BA33 File Offset: 0x00039C33
		internal ReadOnlyCollection<QueryExpression> ContextTables { get; }

		// Token: 0x06001531 RID: 5425 RVA: 0x0003BA3C File Offset: 0x00039C3C
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryGroupAndJoinExpression queryGroupAndJoinExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryGroupAndJoinExpression>(this, other, out flag, out queryGroupAndJoinExpression))
			{
				return flag;
			}
			return this.GroupItems.SequenceEqual(queryGroupAndJoinExpression.GroupItems) && this.AdditionalColumns.SequenceEqual(queryGroupAndJoinExpression.AdditionalColumns) && this.ContextTables.SequenceEqual(queryGroupAndJoinExpression.ContextTables);
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x0003BA91 File Offset: 0x00039C91
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x0003BAA4 File Offset: 0x00039CA4
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.CombineHash<IGroupItem>(this.GroupItems, null), Hashing.CombineHash<QueryGroupAndJoinAdditionalColumn>(this.AdditionalColumns, null), Hashing.CombineHash<QueryExpression>(this.ContextTables, null));
		}
	}
}
