using System;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x0200188B RID: 6283
	internal class IfQueryExpression : QueryExpression
	{
		// Token: 0x06009F64 RID: 40804 RVA: 0x0020EB02 File Offset: 0x0020CD02
		public IfQueryExpression(QueryExpression condition, QueryExpression trueCase, QueryExpression falseCase)
		{
			this.condition = condition;
			this.trueCase = trueCase;
			this.falseCase = falseCase;
		}

		// Token: 0x17002922 RID: 10530
		// (get) Token: 0x06009F65 RID: 40805 RVA: 0x0000240C File Offset: 0x0000060C
		public override QueryExpressionKind Kind
		{
			get
			{
				return QueryExpressionKind.If;
			}
		}

		// Token: 0x17002923 RID: 10531
		// (get) Token: 0x06009F66 RID: 40806 RVA: 0x0020EB1F File Offset: 0x0020CD1F
		public QueryExpression Condition
		{
			get
			{
				return this.condition;
			}
		}

		// Token: 0x17002924 RID: 10532
		// (get) Token: 0x06009F67 RID: 40807 RVA: 0x0020EB27 File Offset: 0x0020CD27
		public QueryExpression TrueCase
		{
			get
			{
				return this.trueCase;
			}
		}

		// Token: 0x17002925 RID: 10533
		// (get) Token: 0x06009F68 RID: 40808 RVA: 0x0020EB2F File Offset: 0x0020CD2F
		public QueryExpression FalseCase
		{
			get
			{
				return this.falseCase;
			}
		}

		// Token: 0x06009F69 RID: 40809 RVA: 0x0020EB37 File Offset: 0x0020CD37
		public override void Analyze(Func<QueryExpression, bool> analyzer)
		{
			if (analyzer(this))
			{
				this.condition.Analyze(analyzer);
				this.trueCase.Analyze(analyzer);
				this.falseCase.Analyze(analyzer);
			}
		}

		// Token: 0x06009F6A RID: 40810 RVA: 0x0020EB68 File Offset: 0x0020CD68
		public override QueryExpression Rewrite(Func<QueryExpression, QueryExpression> rewrite)
		{
			QueryExpression queryExpression = this.condition.Rewrite(rewrite);
			QueryExpression queryExpression2 = this.trueCase.Rewrite(rewrite);
			QueryExpression queryExpression3 = this.falseCase.Rewrite(rewrite);
			QueryExpression queryExpression4 = this;
			if (queryExpression != this.condition || queryExpression2 != this.condition || queryExpression3 != this.condition)
			{
				queryExpression4 = new IfQueryExpression(queryExpression, queryExpression2, queryExpression3);
			}
			return rewrite(queryExpression4);
		}

		// Token: 0x040053A2 RID: 21410
		private QueryExpression condition;

		// Token: 0x040053A3 RID: 21411
		private QueryExpression trueCase;

		// Token: 0x040053A4 RID: 21412
		private QueryExpression falseCase;
	}
}
