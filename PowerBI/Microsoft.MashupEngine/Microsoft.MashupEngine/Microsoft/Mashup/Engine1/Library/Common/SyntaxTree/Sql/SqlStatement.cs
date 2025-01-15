using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x0200121D RID: 4637
	internal abstract class SqlStatement : IScriptable
	{
		// Token: 0x06007A97 RID: 31383
		public abstract void WriteCreateScript(ScriptWriter writer);
	}
}
