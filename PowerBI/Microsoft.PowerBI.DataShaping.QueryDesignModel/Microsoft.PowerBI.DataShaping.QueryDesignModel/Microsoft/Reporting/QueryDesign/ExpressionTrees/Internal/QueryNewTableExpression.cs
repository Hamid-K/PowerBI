using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001A8 RID: 424
	internal sealed class QueryNewTableExpression : QueryExtensionExpressionBase
	{
		// Token: 0x060015C5 RID: 5573 RVA: 0x0003CB59 File Offset: 0x0003AD59
		internal QueryNewTableExpression(ConceptualResultType conceptualResultType, IEnumerable<KeyValuePair<string, QueryExpression>> columns)
			: base(conceptualResultType)
		{
			this._columns = columns.ToReadOnlyCollection<KeyValuePair<string, QueryExpression>>();
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060015C6 RID: 5574 RVA: 0x0003CB6E File Offset: 0x0003AD6E
		public ReadOnlyCollection<KeyValuePair<string, QueryExpression>> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x0003CB76 File Offset: 0x0003AD76
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x0003CB8C File Offset: 0x0003AD8C
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryNewTableExpression queryNewTableExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryNewTableExpression>(this, other, out flag, out queryNewTableExpression))
			{
				return flag;
			}
			return this.Columns.SequenceEqual(queryNewTableExpression.Columns);
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x0003CBB9 File Offset: 0x0003ADB9
		public override int GetHashCode()
		{
			return Hashing.CombineHash<KeyValuePair<string, QueryExpression>>(this._columns, null);
		}

		// Token: 0x04000BA4 RID: 2980
		private readonly ReadOnlyCollection<KeyValuePair<string, QueryExpression>> _columns;
	}
}
