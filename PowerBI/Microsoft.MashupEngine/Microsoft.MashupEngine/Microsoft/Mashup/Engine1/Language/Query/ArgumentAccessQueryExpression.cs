using System;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x0200188A RID: 6282
	internal class ArgumentAccessQueryExpression : QueryExpression
	{
		// Token: 0x06009F5C RID: 40796 RVA: 0x0020EAE0 File Offset: 0x0020CCE0
		private ArgumentAccessQueryExpression()
		{
		}

		// Token: 0x17002921 RID: 10529
		// (get) Token: 0x06009F5D RID: 40797 RVA: 0x00002461 File Offset: 0x00000661
		public override QueryExpressionKind Kind
		{
			get
			{
				return QueryExpressionKind.ArgumentAccess;
			}
		}

		// Token: 0x06009F5E RID: 40798 RVA: 0x0020EA26 File Offset: 0x0020CC26
		public override void Analyze(Func<QueryExpression, bool> analyzer)
		{
			analyzer(this);
		}

		// Token: 0x06009F5F RID: 40799 RVA: 0x0020EA30 File Offset: 0x0020CC30
		public override QueryExpression Rewrite(Func<QueryExpression, QueryExpression> rewrite)
		{
			return rewrite(this);
		}

		// Token: 0x06009F60 RID: 40800 RVA: 0x0003391C File Offset: 0x00031B1C
		public bool Equals(ArgumentAccessQueryExpression other)
		{
			return other != null;
		}

		// Token: 0x06009F61 RID: 40801 RVA: 0x0020EAE8 File Offset: 0x0020CCE8
		public override bool Equals(object other)
		{
			return this.Equals(other as ArgumentAccessQueryExpression);
		}

		// Token: 0x06009F62 RID: 40802 RVA: 0x00002105 File Offset: 0x00000305
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x040053A1 RID: 21409
		public static readonly ArgumentAccessQueryExpression Instance = new ArgumentAccessQueryExpression();
	}
}
