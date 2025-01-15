using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x02001208 RID: 4616
	internal sealed class SqlDropSchemaStatement : SqlStatement
	{
		// Token: 0x060079BF RID: 31167 RVA: 0x001A4B77 File Offset: 0x001A2D77
		public SqlDropSchemaStatement(Alias schema)
		{
			this.schema = schema;
		}

		// Token: 0x17002151 RID: 8529
		// (get) Token: 0x060079C0 RID: 31168 RVA: 0x001A4B86 File Offset: 0x001A2D86
		public Alias Schema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x060079C1 RID: 31169 RVA: 0x001A4B8E File Offset: 0x001A2D8E
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.WriteSpaceAfter(SqlLanguageStrings.DropSchemaSqlString);
			writer.WriteIdentifier(this.schema);
		}

		// Token: 0x04004272 RID: 17010
		private readonly Alias schema;
	}
}
