using System;
using System.Collections.Generic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Aggregation
{
	// Token: 0x020002AF RID: 687
	internal sealed class AggregateToken : ApplyTransformationToken
	{
		// Token: 0x060017A7 RID: 6055 RVA: 0x00050D8A File Offset: 0x0004EF8A
		public AggregateToken(IEnumerable<AggregateExpressionToken> expressions)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<AggregateExpressionToken>>(expressions, "expressions");
			this.expressions = expressions;
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060017A8 RID: 6056 RVA: 0x00050DA4 File Offset: 0x0004EFA4
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Aggregate;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060017A9 RID: 6057 RVA: 0x00050DA8 File Offset: 0x0004EFA8
		public IEnumerable<AggregateExpressionToken> Expressions
		{
			get
			{
				return this.expressions;
			}
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x00050DB0 File Offset: 0x0004EFB0
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000A30 RID: 2608
		private readonly IEnumerable<AggregateExpressionToken> expressions;
	}
}
