using System;
using Microsoft.DataShaping.Common;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000176 RID: 374
	internal sealed class QueryEarlierExpression : QueryExtensionExpressionBase
	{
		// Token: 0x06001488 RID: 5256 RVA: 0x0003B0E8 File Offset: 0x000392E8
		internal QueryEarlierExpression(QueryExpression column)
			: this(column, 1)
		{
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0003B0F2 File Offset: 0x000392F2
		internal QueryEarlierExpression(QueryExpression column, int number)
			: base(column.ConceptualResultType)
		{
			this._column = column;
			this._number = number;
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x0003B10E File Offset: 0x0003930E
		public QueryExpression Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x0003B116 File Offset: 0x00039316
		public int Number
		{
			get
			{
				return this._number;
			}
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x0003B11E File Offset: 0x0003931E
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x0003B128 File Offset: 0x00039328
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryEarlierExpression queryEarlierExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryEarlierExpression>(this, other, out flag, out queryEarlierExpression))
			{
				return flag;
			}
			return this.Column.Equals(queryEarlierExpression.Column) && this.Number == queryEarlierExpression.Number;
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x0003B168 File Offset: 0x00039368
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Column.GetHashCode(), this.Number.GetHashCode());
		}

		// Token: 0x04000B34 RID: 2868
		private readonly QueryExpression _column;

		// Token: 0x04000B35 RID: 2869
		private readonly int _number;
	}
}
