using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011D9 RID: 4569
	internal sealed class OdbcScalarFunctionReference : BuiltInFunctionReference
	{
		// Token: 0x06007895 RID: 30869 RVA: 0x001A170F File Offset: 0x0019F90F
		public OdbcScalarFunctionReference(ConstantSqlString name)
			: base(name)
		{
		}

		// Token: 0x06007896 RID: 30870 RVA: 0x001A1718 File Offset: 0x0019F918
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.Write(SqlLanguageSymbols.OpenCurlyBraceSqlString);
			writer.WriteSpace();
			writer.Write(SqlLanguageStrings.FnSqlString);
			writer.WriteSpace();
			base.WriteCreateScript(writer);
			writer.WriteSpace();
			writer.Write(SqlLanguageSymbols.CloseCurlyBraceSqlString);
		}
	}
}
