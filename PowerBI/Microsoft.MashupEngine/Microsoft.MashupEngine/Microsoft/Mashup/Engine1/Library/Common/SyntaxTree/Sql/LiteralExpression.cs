using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011E2 RID: 4578
	internal sealed class LiteralExpression : ScalarExpression
	{
		// Token: 0x060078B4 RID: 30900 RVA: 0x001A1B17 File Offset: 0x0019FD17
		public LiteralExpression(ConstantSqlString literal)
		{
			this.literal = literal;
		}

		// Token: 0x17002109 RID: 8457
		// (get) Token: 0x060078B5 RID: 30901 RVA: 0x00002105 File Offset: 0x00000305
		public override int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700210A RID: 8458
		// (get) Token: 0x060078B6 RID: 30902 RVA: 0x001A1B26 File Offset: 0x0019FD26
		public ConstantSqlString Value
		{
			get
			{
				return this.literal;
			}
		}

		// Token: 0x060078B7 RID: 30903 RVA: 0x001A1B2E File Offset: 0x0019FD2E
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.Write(this.literal);
		}

		// Token: 0x040041CD RID: 16845
		private readonly ConstantSqlString literal;
	}
}
