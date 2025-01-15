using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011DB RID: 4571
	internal sealed class GroupByItem : IScriptable
	{
		// Token: 0x17002102 RID: 8450
		// (get) Token: 0x0600789A RID: 30874 RVA: 0x001A17F7 File Offset: 0x0019F9F7
		// (set) Token: 0x0600789B RID: 30875 RVA: 0x001A17FF File Offset: 0x0019F9FF
		public SqlExpression Expression { get; set; }

		// Token: 0x0600789C RID: 30876 RVA: 0x001A1808 File Offset: 0x0019FA08
		public void WriteCreateScript(ScriptWriter writer)
		{
			writer.WriteSubexpression(SqlQueryExpression.QueryPrecedence, this.Expression);
		}
	}
}
