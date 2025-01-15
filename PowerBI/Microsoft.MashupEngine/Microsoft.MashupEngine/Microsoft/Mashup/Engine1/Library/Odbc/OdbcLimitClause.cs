using System;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000603 RID: 1539
	internal abstract class OdbcLimitClause : IScriptable
	{
		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x06003098 RID: 12440
		public abstract OdbcLimitClauseLocation Location { get; }

		// Token: 0x06003099 RID: 12441
		public abstract void WriteCreateScript(ScriptWriter scriptWriter);
	}
}
