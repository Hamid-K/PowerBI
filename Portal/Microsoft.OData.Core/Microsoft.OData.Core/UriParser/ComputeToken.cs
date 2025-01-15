using System;
using System.Collections.Generic;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000112 RID: 274
	public sealed class ComputeToken : ApplyTransformationToken
	{
		// Token: 0x06000F6E RID: 3950 RVA: 0x0002651A File Offset: 0x0002471A
		public ComputeToken(IEnumerable<ComputeExpressionToken> expressions)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<ComputeExpressionToken>>(expressions, "expressions");
			this.expressions = expressions;
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x000264A4 File Offset: 0x000246A4
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Compute;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x00026535 File Offset: 0x00024735
		public IEnumerable<ComputeExpressionToken> Expressions
		{
			get
			{
				return this.expressions;
			}
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x00026540 File Offset: 0x00024740
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			SyntacticTreeVisitor<T> syntacticTreeVisitor = visitor as SyntacticTreeVisitor<T>;
			if (syntacticTreeVisitor != null)
			{
				return syntacticTreeVisitor.Visit(this);
			}
			throw new NotImplementedException();
		}

		// Token: 0x04000789 RID: 1929
		private readonly IEnumerable<ComputeExpressionToken> expressions;
	}
}
