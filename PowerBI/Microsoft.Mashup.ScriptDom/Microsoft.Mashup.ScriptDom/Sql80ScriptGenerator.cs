using System;
using Microsoft.Mashup.ScriptDom.ScriptGenerator;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200019B RID: 411
	internal sealed class Sql80ScriptGenerator : SqlScriptGenerator
	{
		// Token: 0x060021A0 RID: 8608 RVA: 0x0015E901 File Offset: 0x0015CB01
		public Sql80ScriptGenerator()
			: this(new SqlScriptGeneratorOptions())
		{
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x0015E90E File Offset: 0x0015CB0E
		public Sql80ScriptGenerator(SqlScriptGeneratorOptions options)
			: base(options)
		{
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x0015E917 File Offset: 0x0015CB17
		internal override SqlScriptGeneratorVisitor CreateSqlScriptGeneratorVisitor(SqlScriptGeneratorOptions options, ScriptWriter scriptWriter)
		{
			ScriptGeneratorSupporter.CheckForNullReference(options, "options");
			ScriptGeneratorSupporter.CheckForNullReference(scriptWriter, "scriptWriter");
			return new Sql80ScriptGeneratorVisitor(options, scriptWriter);
		}
	}
}
