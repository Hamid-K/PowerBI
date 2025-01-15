using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011D6 RID: 4566
	internal sealed class StoredFunctionReference : FunctionReferenceBase
	{
		// Token: 0x0600788A RID: 30858 RVA: 0x001A15D4 File Offset: 0x0019F7D4
		public StoredFunctionReference(Alias schema, Alias name)
		{
			this.schema = schema;
			this.name = name;
		}

		// Token: 0x0600788B RID: 30859 RVA: 0x001A15EA File Offset: 0x0019F7EA
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.WriteIdentifier(this.schema, this.name);
			writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
			base.WriteParameters(writer);
			writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
		}

		// Token: 0x040041B5 RID: 16821
		private Alias name;

		// Token: 0x040041B6 RID: 16822
		private Alias schema;
	}
}
