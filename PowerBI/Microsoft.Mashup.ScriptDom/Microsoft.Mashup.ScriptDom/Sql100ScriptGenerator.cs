using System;
using Microsoft.Mashup.ScriptDom.ScriptGenerator;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200018D RID: 397
	internal sealed class Sql100ScriptGenerator : SqlScriptGenerator
	{
		// Token: 0x0600215F RID: 8543 RVA: 0x0015DD7D File Offset: 0x0015BF7D
		public Sql100ScriptGenerator()
			: this(new SqlScriptGeneratorOptions())
		{
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x0015DD8A File Offset: 0x0015BF8A
		public Sql100ScriptGenerator(SqlScriptGeneratorOptions options)
			: base(options)
		{
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x0015DD93 File Offset: 0x0015BF93
		internal override SqlScriptGeneratorVisitor CreateSqlScriptGeneratorVisitor(SqlScriptGeneratorOptions options, ScriptWriter scriptWriter)
		{
			ScriptGeneratorSupporter.CheckForNullReference(options, "options");
			ScriptGeneratorSupporter.CheckForNullReference(scriptWriter, "scriptWriter");
			return new Sql100ScriptGeneratorVisitor(options, scriptWriter);
		}
	}
}
