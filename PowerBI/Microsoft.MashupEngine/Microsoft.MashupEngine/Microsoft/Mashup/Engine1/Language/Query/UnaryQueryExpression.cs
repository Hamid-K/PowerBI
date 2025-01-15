using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x0200188D RID: 6285
	internal class UnaryQueryExpression : QueryExpression
	{
		// Token: 0x06009F74 RID: 40820 RVA: 0x0020EDDE File Offset: 0x0020CFDE
		public UnaryQueryExpression(UnaryOperator2 unaryOperator, QueryExpression expression)
		{
			this.unaryOperator = unaryOperator;
			this.expression = expression;
		}

		// Token: 0x17002929 RID: 10537
		// (get) Token: 0x06009F75 RID: 40821 RVA: 0x00075E2C File Offset: 0x0007402C
		public override QueryExpressionKind Kind
		{
			get
			{
				return QueryExpressionKind.Unary;
			}
		}

		// Token: 0x1700292A RID: 10538
		// (get) Token: 0x06009F76 RID: 40822 RVA: 0x0020EDF4 File Offset: 0x0020CFF4
		public UnaryOperator2 Operator
		{
			get
			{
				return this.unaryOperator;
			}
		}

		// Token: 0x1700292B RID: 10539
		// (get) Token: 0x06009F77 RID: 40823 RVA: 0x0020EDFC File Offset: 0x0020CFFC
		public QueryExpression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x06009F78 RID: 40824 RVA: 0x0020EE04 File Offset: 0x0020D004
		public override void Analyze(Func<QueryExpression, bool> analyzer)
		{
			if (analyzer(this))
			{
				this.expression.Analyze(analyzer);
			}
		}

		// Token: 0x06009F79 RID: 40825 RVA: 0x0020EE1C File Offset: 0x0020D01C
		public override QueryExpression Rewrite(Func<QueryExpression, QueryExpression> rewrite)
		{
			QueryExpression queryExpression = this.expression.Rewrite(rewrite);
			QueryExpression queryExpression2 = this;
			if (queryExpression != this.expression)
			{
				queryExpression2 = new UnaryQueryExpression(this.unaryOperator, queryExpression);
			}
			return rewrite(queryExpression2);
		}

		// Token: 0x040053A7 RID: 21415
		private UnaryOperator2 unaryOperator;

		// Token: 0x040053A8 RID: 21416
		private QueryExpression expression;
	}
}
