using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200017C RID: 380
	internal sealed class QueryExpressionWithLocalVariables : QueryExtensionExpressionBase
	{
		// Token: 0x060014EF RID: 5359 RVA: 0x0003B397 File Offset: 0x00039597
		internal QueryExpressionWithLocalVariables(ConceptualResultType conceptualResultType, IReadOnlyList<QueryVariableDeclarationExpression> declarations, QueryExpression result)
			: base(conceptualResultType)
		{
			this._declarations = declarations;
			this._result = result;
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x0003B3AE File Offset: 0x000395AE
		public IReadOnlyList<QueryVariableDeclarationExpression> Declarations
		{
			get
			{
				return this._declarations;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x0003B3B6 File Offset: 0x000395B6
		public QueryExpression Result
		{
			get
			{
				return this._result;
			}
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x0003B3C0 File Offset: 0x000395C0
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryExpressionWithLocalVariables queryExpressionWithLocalVariables;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryExpressionWithLocalVariables>(this, other, out flag, out queryExpressionWithLocalVariables))
			{
				return flag;
			}
			return this.Declarations.SequenceEqual(queryExpressionWithLocalVariables.Declarations) && this.Result.Equals(queryExpressionWithLocalVariables.Result);
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0003B402 File Offset: 0x00039602
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x04000B3B RID: 2875
		private readonly IReadOnlyList<QueryVariableDeclarationExpression> _declarations;

		// Token: 0x04000B3C RID: 2876
		private readonly QueryExpression _result;
	}
}
