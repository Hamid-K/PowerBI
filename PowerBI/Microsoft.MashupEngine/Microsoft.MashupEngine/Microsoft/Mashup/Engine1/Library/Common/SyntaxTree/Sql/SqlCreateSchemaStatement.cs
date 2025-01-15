using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x02001201 RID: 4609
	internal sealed class SqlCreateSchemaStatement : SqlStatement
	{
		// Token: 0x06007987 RID: 31111 RVA: 0x001A4237 File Offset: 0x001A2437
		public SqlCreateSchemaStatement(Alias schema)
		{
			this.schema = schema;
		}

		// Token: 0x1700212E RID: 8494
		// (get) Token: 0x06007988 RID: 31112 RVA: 0x001A4246 File Offset: 0x001A2446
		public Alias Schema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x06007989 RID: 31113 RVA: 0x001A424E File Offset: 0x001A244E
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.WriteSpaceAfter(SqlLanguageStrings.CreateSchemaSqlString);
			writer.WriteIdentifier(this.schema);
		}

		// Token: 0x0400424F RID: 16975
		private readonly Alias schema;
	}
}
