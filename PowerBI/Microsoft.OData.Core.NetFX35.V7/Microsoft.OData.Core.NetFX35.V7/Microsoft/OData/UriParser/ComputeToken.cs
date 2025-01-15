using System;
using System.Collections.Generic;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200016A RID: 362
	public sealed class ComputeToken : ApplyTransformationToken
	{
		// Token: 0x06000F53 RID: 3923 RVA: 0x0002BC03 File Offset: 0x00029E03
		public ComputeToken(IEnumerable<ComputeExpressionToken> expressions)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<ComputeExpressionToken>>(expressions, "expressions");
			this.expressions = expressions;
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x0002BC1E File Offset: 0x00029E1E
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Compute;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x0002BC22 File Offset: 0x00029E22
		public IEnumerable<ComputeExpressionToken> Expressions
		{
			get
			{
				return this.expressions;
			}
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0002BC2C File Offset: 0x00029E2C
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			SyntacticTreeVisitor<T> syntacticTreeVisitor = visitor as SyntacticTreeVisitor<T>;
			if (syntacticTreeVisitor != null)
			{
				return syntacticTreeVisitor.Visit(this);
			}
			throw new NotImplementedException();
		}

		// Token: 0x040007B2 RID: 1970
		private readonly IEnumerable<ComputeExpressionToken> expressions;
	}
}
