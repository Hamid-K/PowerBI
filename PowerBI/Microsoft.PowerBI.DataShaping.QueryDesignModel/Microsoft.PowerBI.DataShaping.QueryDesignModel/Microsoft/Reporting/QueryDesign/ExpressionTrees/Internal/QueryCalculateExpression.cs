using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000167 RID: 359
	internal sealed class QueryCalculateExpression : QueryUnaryExtensionExpression
	{
		// Token: 0x06001447 RID: 5191 RVA: 0x0003AAD7 File Offset: 0x00038CD7
		internal QueryCalculateExpression(ConceptualResultType conceptualResultType, QueryExpression argument, IEnumerable<QueryExpression> filters)
			: base(conceptualResultType, argument)
		{
			this._filters = filters.ToReadOnlyCollection<QueryExpression>();
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x0003AAED File Offset: 0x00038CED
		public ReadOnlyCollection<QueryExpression> Filters
		{
			get
			{
				return this._filters;
			}
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0003AAF5 File Offset: 0x00038CF5
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x0003AB08 File Offset: 0x00038D08
		public override bool Equals(QueryExpression other)
		{
			if (!base.Equals(other))
			{
				return false;
			}
			QueryCalculateExpression queryCalculateExpression = (QueryCalculateExpression)other;
			return this.Filters.SequenceEqual(queryCalculateExpression.Filters);
		}

		// Token: 0x04000B16 RID: 2838
		private readonly ReadOnlyCollection<QueryExpression> _filters;
	}
}
