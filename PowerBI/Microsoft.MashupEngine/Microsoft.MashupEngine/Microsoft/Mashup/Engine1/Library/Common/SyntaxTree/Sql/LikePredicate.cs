using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011E1 RID: 4577
	internal sealed class LikePredicate : Condition
	{
		// Token: 0x060078B1 RID: 30897 RVA: 0x001A1A98 File Offset: 0x0019FC98
		public LikePredicate(SqlExpression value, SqlExpression substring, SqlExpression escape)
		{
			this.value = value;
			this.substring = substring;
			this.escape = escape;
		}

		// Token: 0x17002108 RID: 8456
		// (get) Token: 0x060078B2 RID: 30898 RVA: 0x000024ED File Offset: 0x000006ED
		public override int Precedence
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x060078B3 RID: 30899 RVA: 0x001A1AB8 File Offset: 0x0019FCB8
		public override void WriteCreateScript(ScriptWriter writer)
		{
			this.value.WriteCreateScript(writer);
			writer.WriteSpace();
			writer.Write(SqlLanguageStrings.LikeSqlString);
			writer.WriteSpace();
			this.substring.WriteCreateScript(writer);
			writer.WriteSpace();
			writer.Write(SqlLanguageStrings.EscapeSqlString);
			writer.WriteSpace();
			this.escape.WriteCreateScript(writer);
		}

		// Token: 0x040041CA RID: 16842
		private readonly SqlExpression value;

		// Token: 0x040041CB RID: 16843
		private readonly SqlExpression substring;

		// Token: 0x040041CC RID: 16844
		private readonly SqlExpression escape;
	}
}
