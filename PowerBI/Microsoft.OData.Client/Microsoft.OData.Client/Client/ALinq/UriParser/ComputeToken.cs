using System;
using System.Collections.Generic;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x02000119 RID: 281
	public sealed class ComputeToken : ApplyTransformationToken
	{
		// Token: 0x06000BD0 RID: 3024 RVA: 0x0002C963 File Offset: 0x0002AB63
		public ComputeToken(IEnumerable<ComputeExpressionToken> expressions)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<ComputeExpressionToken>>(expressions, "expressions");
			this.expressions = expressions;
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x0002C97E File Offset: 0x0002AB7E
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Compute;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0002C982 File Offset: 0x0002AB82
		public IEnumerable<ComputeExpressionToken> Expressions
		{
			get
			{
				return this.expressions;
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0002C98C File Offset: 0x0002AB8C
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			SyntacticTreeVisitor<T> syntacticTreeVisitor = visitor as SyntacticTreeVisitor<T>;
			if (syntacticTreeVisitor != null)
			{
				return syntacticTreeVisitor.Visit(this);
			}
			throw new NotImplementedException();
		}

		// Token: 0x04000655 RID: 1621
		private readonly IEnumerable<ComputeExpressionToken> expressions;
	}
}
