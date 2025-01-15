using System;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001889 RID: 6281
	internal class ColumnAccessQueryExpression : QueryExpression
	{
		// Token: 0x06009F53 RID: 40787 RVA: 0x0020EA9B File Offset: 0x0020CC9B
		public ColumnAccessQueryExpression(int column)
		{
			this.column = column;
		}

		// Token: 0x1700291F RID: 10527
		// (get) Token: 0x06009F54 RID: 40788 RVA: 0x000023C4 File Offset: 0x000005C4
		public override QueryExpressionKind Kind
		{
			get
			{
				return QueryExpressionKind.ColumnAccess;
			}
		}

		// Token: 0x17002920 RID: 10528
		// (get) Token: 0x06009F55 RID: 40789 RVA: 0x0020EAAA File Offset: 0x0020CCAA
		public int Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x06009F56 RID: 40790 RVA: 0x0020EAB2 File Offset: 0x0020CCB2
		public override bool TryGetColumnAccess(out int column)
		{
			column = this.column;
			return true;
		}

		// Token: 0x06009F57 RID: 40791 RVA: 0x0020EA26 File Offset: 0x0020CC26
		public override void Analyze(Func<QueryExpression, bool> analyzer)
		{
			analyzer(this);
		}

		// Token: 0x06009F58 RID: 40792 RVA: 0x0020EA30 File Offset: 0x0020CC30
		public override QueryExpression Rewrite(Func<QueryExpression, QueryExpression> rewrite)
		{
			return rewrite(this);
		}

		// Token: 0x06009F59 RID: 40793 RVA: 0x0020EABD File Offset: 0x0020CCBD
		public bool Equals(ColumnAccessQueryExpression other)
		{
			return other != null && this.column == other.column;
		}

		// Token: 0x06009F5A RID: 40794 RVA: 0x0020EAD2 File Offset: 0x0020CCD2
		public override bool Equals(object other)
		{
			return this.Equals(other as ColumnAccessQueryExpression);
		}

		// Token: 0x06009F5B RID: 40795 RVA: 0x0020EAAA File Offset: 0x0020CCAA
		public override int GetHashCode()
		{
			return this.column;
		}

		// Token: 0x040053A0 RID: 21408
		private int column;
	}
}
