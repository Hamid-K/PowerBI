using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x02001205 RID: 4613
	internal sealed class SqlDeclareVariableStatement : SqlStatement
	{
		// Token: 0x060079B2 RID: 31154 RVA: 0x001A4A53 File Offset: 0x001A2C53
		public SqlDeclareVariableStatement(Alias name, SqlDataType type)
		{
			this.name = name;
			this.type = type;
		}

		// Token: 0x1700214B RID: 8523
		// (get) Token: 0x060079B3 RID: 31155 RVA: 0x001A4A69 File Offset: 0x001A2C69
		public Alias Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700214C RID: 8524
		// (get) Token: 0x060079B4 RID: 31156 RVA: 0x001A4A71 File Offset: 0x001A2C71
		public SqlDataType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x060079B5 RID: 31157 RVA: 0x001A4A79 File Offset: 0x001A2C79
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.WriteSpaceAfter(SqlLanguageStrings.DeclareSqlString);
			new VariableReference(this.name).WriteCreateScript(writer);
			writer.WriteSpace();
			writer.WriteSpaceAfter(SqlLanguageStrings.AsSqlString);
			this.type.WriteCreateScript(writer);
		}

		// Token: 0x0400426C RID: 17004
		private readonly Alias name;

		// Token: 0x0400426D RID: 17005
		private readonly SqlDataType type;
	}
}
