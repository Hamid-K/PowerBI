using System;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Aggregation
{
	// Token: 0x020002AD RID: 685
	internal sealed class AggregateExpressionToken : QueryToken
	{
		// Token: 0x060017A0 RID: 6048 RVA: 0x00050D2A File Offset: 0x0004EF2A
		public AggregateExpressionToken(QueryToken expression, AggregationMethod withVerb, string alias)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(expression, "expression");
			ExceptionUtils.CheckArgumentNotNull<string>(alias, "alias");
			this.expression = expression;
			this.method = withVerb;
			this.alias = alias;
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060017A1 RID: 6049 RVA: 0x00050D5D File Offset: 0x0004EF5D
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.AggregateExpression;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060017A2 RID: 6050 RVA: 0x00050D61 File Offset: 0x0004EF61
		public AggregationMethod Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060017A3 RID: 6051 RVA: 0x00050D69 File Offset: 0x0004EF69
		public QueryToken Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x00050D71 File Offset: 0x0004EF71
		public string Alias
		{
			get
			{
				return this.alias;
			}
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x00050D79 File Offset: 0x0004EF79
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000A2D RID: 2605
		private readonly QueryToken expression;

		// Token: 0x04000A2E RID: 2606
		private readonly AggregationMethod method;

		// Token: 0x04000A2F RID: 2607
		private readonly string alias;
	}
}
