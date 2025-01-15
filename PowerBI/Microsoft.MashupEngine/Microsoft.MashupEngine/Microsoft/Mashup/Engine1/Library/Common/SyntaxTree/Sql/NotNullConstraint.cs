using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011FD RID: 4605
	internal sealed class NotNullConstraint : SqlColumnConstraint
	{
		// Token: 0x06007973 RID: 31091 RVA: 0x001A3E51 File Offset: 0x001A2051
		private NotNullConstraint()
		{
		}

		// Token: 0x06007974 RID: 31092 RVA: 0x001A3E59 File Offset: 0x001A2059
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.Write(SqlLanguageStrings.NotNullSqlString);
		}

		// Token: 0x04004229 RID: 16937
		public static readonly NotNullConstraint Instance = new NotNullConstraint();
	}
}
