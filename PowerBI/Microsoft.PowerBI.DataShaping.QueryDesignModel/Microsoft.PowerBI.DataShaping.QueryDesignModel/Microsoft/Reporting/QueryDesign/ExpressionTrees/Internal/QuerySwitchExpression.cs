using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001BD RID: 445
	internal sealed class QuerySwitchExpression : QueryExtensionExpressionBase
	{
		// Token: 0x06001631 RID: 5681 RVA: 0x0003D94C File Offset: 0x0003BB4C
		internal QuerySwitchExpression(ConceptualResultType conceptualResultType, QueryExpression input, IReadOnlyList<QuerySwitchCase> cases, QueryExpression defaultResult)
			: base(conceptualResultType)
		{
			this._input = input;
			this._cases = cases;
			this._defaultResult = defaultResult;
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x0003D96B File Offset: 0x0003BB6B
		public QueryExpression Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001633 RID: 5683 RVA: 0x0003D973 File Offset: 0x0003BB73
		public IReadOnlyList<QuerySwitchCase> Cases
		{
			get
			{
				return this._cases;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x0003D97B File Offset: 0x0003BB7B
		public QueryExpression DefaultResult
		{
			get
			{
				return this._defaultResult;
			}
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x0003D983 File Offset: 0x0003BB83
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x0003D98C File Offset: 0x0003BB8C
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QuerySwitchExpression querySwitchExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QuerySwitchExpression>(this, other, out flag, out querySwitchExpression))
			{
				return flag;
			}
			return this.Input.Equals(querySwitchExpression.Input) && this.Cases.SequenceEqual(querySwitchExpression.Cases) && this.DefaultResult != null && this.DefaultResult.Equals(querySwitchExpression.DefaultResult);
		}

		// Token: 0x04000BD8 RID: 3032
		private readonly QueryExpression _input;

		// Token: 0x04000BD9 RID: 3033
		private readonly IReadOnlyList<QuerySwitchCase> _cases;

		// Token: 0x04000BDA RID: 3034
		private readonly QueryExpression _defaultResult;
	}
}
