using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x02001206 RID: 4614
	internal sealed class SqlDefault : ScalarExpression
	{
		// Token: 0x060079B6 RID: 31158 RVA: 0x001A0E38 File Offset: 0x0019F038
		private SqlDefault()
		{
		}

		// Token: 0x1700214D RID: 8525
		// (get) Token: 0x060079B7 RID: 31159 RVA: 0x00002105 File Offset: 0x00000305
		public override int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060079B8 RID: 31160 RVA: 0x001A4AB4 File Offset: 0x001A2CB4
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.Write(SqlLanguageStrings.DefaultSqlString);
		}

		// Token: 0x0400426E RID: 17006
		public static readonly SqlDefault Instance = new SqlDefault();
	}
}
