using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000165 RID: 357
	internal sealed class QueryBatchRootExpression : QueryExtensionExpressionBase
	{
		// Token: 0x0600143C RID: 5180 RVA: 0x0003A9A2 File Offset: 0x00038BA2
		internal QueryBatchRootExpression(ConceptualResultType conceptualResultType, IReadOnlyList<QueryParameterDeclarationExpression> queryParameters, IReadOnlyList<QueryBaseDeclarationExpression> declarations, IReadOnlyList<QueryExpression> outputTables)
			: base(conceptualResultType)
		{
			this.QueryParameters = queryParameters;
			this.Declarations = declarations;
			this.OutputTables = outputTables;
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x0003A9C1 File Offset: 0x00038BC1
		public IReadOnlyList<QueryParameterDeclarationExpression> QueryParameters { get; }

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x0003A9C9 File Offset: 0x00038BC9
		public IReadOnlyList<QueryBaseDeclarationExpression> Declarations { get; }

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x0003A9D1 File Offset: 0x00038BD1
		public IReadOnlyList<QueryExpression> OutputTables { get; }

		// Token: 0x06001440 RID: 5184 RVA: 0x0003A9DC File Offset: 0x00038BDC
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryBatchRootExpression queryBatchRootExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryBatchRootExpression>(this, other, out flag, out queryBatchRootExpression))
			{
				return flag;
			}
			return this.QueryParameters.SequenceEqual(queryBatchRootExpression.QueryParameters) && this.Declarations.SequenceEqual(queryBatchRootExpression.Declarations) && this.OutputTables.SequenceEqual(queryBatchRootExpression.OutputTables);
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0003AA31 File Offset: 0x00038C31
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
