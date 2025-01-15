using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000192 RID: 402
	internal sealed class QueryImplicitJoinWithProjectionExpression : QueryExtensionExpressionBase
	{
		// Token: 0x06001560 RID: 5472 RVA: 0x0003BF42 File Offset: 0x0003A142
		internal QueryImplicitJoinWithProjectionExpression(ConceptualResultType conceptualResultType, QueryExpressionBinding primaryTable, IReadOnlyList<ImplicitJoinSecondaryTable> secondaryTables)
			: base(conceptualResultType)
		{
			this.PrimaryTable = primaryTable;
			this.SecondaryTables = secondaryTables;
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x0003BF59 File Offset: 0x0003A159
		public QueryExpressionBinding PrimaryTable { get; }

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x0003BF61 File Offset: 0x0003A161
		public IReadOnlyList<ImplicitJoinSecondaryTable> SecondaryTables { get; }

		// Token: 0x06001563 RID: 5475 RVA: 0x0003BF69 File Offset: 0x0003A169
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x0003BF7C File Offset: 0x0003A17C
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryImplicitJoinWithProjectionExpression queryImplicitJoinWithProjectionExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryImplicitJoinWithProjectionExpression>(this, other, out flag, out queryImplicitJoinWithProjectionExpression))
			{
				return flag;
			}
			return this.PrimaryTable.Equals(queryImplicitJoinWithProjectionExpression.PrimaryTable) && this.SecondaryTables.SequenceEqual(queryImplicitJoinWithProjectionExpression.SecondaryTables);
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x0003BFBE File Offset: 0x0003A1BE
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.PrimaryTable.GetHashCode(), Hashing.CombineHashReadonly<ImplicitJoinSecondaryTable>(this.SecondaryTables, null));
		}
	}
}
