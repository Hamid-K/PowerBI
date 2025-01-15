using System;
using Microsoft.Mashup.ScriptDom.ScriptGenerator;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000DC RID: 220
	internal sealed class Sql110ScriptGenerator : SqlScriptGenerator
	{
		// Token: 0x0600141E RID: 5150 RVA: 0x0008EE2B File Offset: 0x0008D02B
		public Sql110ScriptGenerator()
			: this(new SqlScriptGeneratorOptions())
		{
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x0008EE38 File Offset: 0x0008D038
		public Sql110ScriptGenerator(SqlScriptGeneratorOptions options)
			: base(options)
		{
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0008EE41 File Offset: 0x0008D041
		internal override SqlScriptGeneratorVisitor CreateSqlScriptGeneratorVisitor(SqlScriptGeneratorOptions options, ScriptWriter scriptWriter)
		{
			ScriptGeneratorSupporter.CheckForNullReference(options, "options");
			ScriptGeneratorSupporter.CheckForNullReference(scriptWriter, "scriptWriter");
			return new Sql110ScriptGeneratorVisitor(options, scriptWriter);
		}
	}
}
