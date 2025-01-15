using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011FC RID: 4604
	internal abstract class SqlColumnConstraint : IScriptable
	{
		// Token: 0x06007971 RID: 31089
		public abstract void WriteCreateScript(ScriptWriter writer);
	}
}
