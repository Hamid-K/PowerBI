using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x0200120C RID: 4620
	internal abstract class SqlExpression : IScriptable
	{
		// Token: 0x17002154 RID: 8532
		// (get) Token: 0x060079CB RID: 31179
		public abstract int Precedence { get; }

		// Token: 0x060079CC RID: 31180
		public abstract void WriteCreateScript(ScriptWriter writer);
	}
}
