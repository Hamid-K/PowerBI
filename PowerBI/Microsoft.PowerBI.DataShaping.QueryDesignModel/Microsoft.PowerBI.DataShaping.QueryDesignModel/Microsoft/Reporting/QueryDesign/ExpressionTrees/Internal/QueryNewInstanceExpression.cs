using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001A7 RID: 423
	internal sealed class QueryNewInstanceExpression : QueryExpression
	{
		// Token: 0x060015C1 RID: 5569 RVA: 0x0003CAF0 File Offset: 0x0003ACF0
		internal QueryNewInstanceExpression(ConceptualResultType conceptualResultType, IEnumerable<KeyValuePair<string, QueryExpression>> arguments)
			: base(conceptualResultType)
		{
			this._arguments = ArgumentValidation.CheckNotNull<IEnumerable<KeyValuePair<string, QueryExpression>>>(arguments, "arguments").ToReadOnlyCollection<KeyValuePair<string, QueryExpression>>();
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x0003CB0F File Offset: 0x0003AD0F
		public ReadOnlyCollection<KeyValuePair<string, QueryExpression>> Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x0003CB17 File Offset: 0x0003AD17
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x0003CB2C File Offset: 0x0003AD2C
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryNewInstanceExpression queryNewInstanceExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryNewInstanceExpression>(this, other, out flag, out queryNewInstanceExpression))
			{
				return flag;
			}
			return this.Arguments.SequenceEqual(queryNewInstanceExpression.Arguments);
		}

		// Token: 0x04000BA3 RID: 2979
		private readonly ReadOnlyCollection<KeyValuePair<string, QueryExpression>> _arguments;
	}
}
