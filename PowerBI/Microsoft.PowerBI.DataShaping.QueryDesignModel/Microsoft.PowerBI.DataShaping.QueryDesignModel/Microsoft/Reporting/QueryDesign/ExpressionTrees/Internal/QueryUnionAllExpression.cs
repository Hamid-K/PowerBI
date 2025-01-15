using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001C9 RID: 457
	internal sealed class QueryUnionAllExpression : QueryExpression
	{
		// Token: 0x06001673 RID: 5747 RVA: 0x0003E08A File Offset: 0x0003C28A
		internal QueryUnionAllExpression(ConceptualResultType conceptualResultType, IReadOnlyList<QueryExpression> tables, TypeUnionBehavior typeUnionBehavior = TypeUnionBehavior.Upcast)
			: base(conceptualResultType)
		{
			this._tables = ArgumentValidation.CheckNotNull<IReadOnlyList<QueryExpression>>(tables, "tables");
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001674 RID: 5748 RVA: 0x0003E0A4 File Offset: 0x0003C2A4
		public IReadOnlyList<QueryExpression> Tables
		{
			get
			{
				return this._tables;
			}
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x0003E0AC File Offset: 0x0003C2AC
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x0003E0C0 File Offset: 0x0003C2C0
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryUnionAllExpression queryUnionAllExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryUnionAllExpression>(this, other, out flag, out queryUnionAllExpression))
			{
				return flag;
			}
			return this.Tables.SequenceEqual(queryUnionAllExpression.Tables);
		}

		// Token: 0x04000BF8 RID: 3064
		private readonly IReadOnlyList<QueryExpression> _tables;
	}
}
