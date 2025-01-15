using System;
using Microsoft.Mashup.ScriptDom.ScriptGenerator;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200019C RID: 412
	internal sealed class Sql90ScriptGenerator : SqlScriptGenerator
	{
		// Token: 0x060021A3 RID: 8611 RVA: 0x0015E936 File Offset: 0x0015CB36
		public Sql90ScriptGenerator()
			: this(new SqlScriptGeneratorOptions())
		{
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x0015E943 File Offset: 0x0015CB43
		public Sql90ScriptGenerator(SqlScriptGeneratorOptions options)
			: base(options)
		{
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x0015E94C File Offset: 0x0015CB4C
		internal override SqlScriptGeneratorVisitor CreateSqlScriptGeneratorVisitor(SqlScriptGeneratorOptions options, ScriptWriter scriptWriter)
		{
			ScriptGeneratorSupporter.CheckForNullReference(options, "options");
			ScriptGeneratorSupporter.CheckForNullReference(scriptWriter, "scriptWriter");
			return new Sql90ScriptGeneratorVisitor(options, scriptWriter);
		}
	}
}
