using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001887 RID: 6279
	internal class BinaryQueryExpression : QueryExpression
	{
		// Token: 0x06009F42 RID: 40770 RVA: 0x0020E95A File Offset: 0x0020CB5A
		public BinaryQueryExpression(BinaryOperator2 binaryOperator, QueryExpression left, QueryExpression right)
		{
			this.binaryOperator = binaryOperator;
			this.left = left;
			this.right = right;
		}

		// Token: 0x17002919 RID: 10521
		// (get) Token: 0x06009F43 RID: 40771 RVA: 0x00002105 File Offset: 0x00000305
		public override QueryExpressionKind Kind
		{
			get
			{
				return QueryExpressionKind.Binary;
			}
		}

		// Token: 0x1700291A RID: 10522
		// (get) Token: 0x06009F44 RID: 40772 RVA: 0x0020E977 File Offset: 0x0020CB77
		public BinaryOperator2 Operator
		{
			get
			{
				return this.binaryOperator;
			}
		}

		// Token: 0x1700291B RID: 10523
		// (get) Token: 0x06009F45 RID: 40773 RVA: 0x0020E97F File Offset: 0x0020CB7F
		public QueryExpression Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x1700291C RID: 10524
		// (get) Token: 0x06009F46 RID: 40774 RVA: 0x0020E987 File Offset: 0x0020CB87
		public QueryExpression Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x06009F47 RID: 40775 RVA: 0x0020E98F File Offset: 0x0020CB8F
		public override void Analyze(Func<QueryExpression, bool> analyzer)
		{
			if (analyzer(this))
			{
				this.left.Analyze(analyzer);
				this.right.Analyze(analyzer);
			}
		}

		// Token: 0x06009F48 RID: 40776 RVA: 0x0020E9B4 File Offset: 0x0020CBB4
		public override QueryExpression Rewrite(Func<QueryExpression, QueryExpression> rewrite)
		{
			QueryExpression queryExpression = this.left.Rewrite(rewrite);
			QueryExpression queryExpression2 = this.right.Rewrite(rewrite);
			QueryExpression queryExpression3 = this;
			if (queryExpression != this.left || queryExpression2 != this.right)
			{
				queryExpression3 = new BinaryQueryExpression(this.binaryOperator, queryExpression, queryExpression2);
			}
			return rewrite(queryExpression3);
		}

		// Token: 0x04005399 RID: 21401
		private BinaryOperator2 binaryOperator;

		// Token: 0x0400539A RID: 21402
		private QueryExpression left;

		// Token: 0x0400539B RID: 21403
		private QueryExpression right;
	}
}
