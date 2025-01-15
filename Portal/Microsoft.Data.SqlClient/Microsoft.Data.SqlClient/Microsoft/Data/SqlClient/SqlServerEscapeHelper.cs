using System;
using System.Text;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000F8 RID: 248
	internal static class SqlServerEscapeHelper
	{
		// Token: 0x06001459 RID: 5209 RVA: 0x0004FE0F File Offset: 0x0004E00F
		internal static string EscapeIdentifier(string name)
		{
			return "[" + name.Replace("]", "]]") + "]";
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x0004FE30 File Offset: 0x0004E030
		internal static void EscapeIdentifier(StringBuilder builder, string name)
		{
			builder.Append("[");
			builder.Append(name.Replace("]", "]]"));
			builder.Append("]");
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0004FE61 File Offset: 0x0004E061
		internal static string EscapeStringAsLiteral(string input)
		{
			return input.Replace("'", "''");
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x0004FE73 File Offset: 0x0004E073
		internal static string MakeStringLiteral(string input)
		{
			if (ADP.IsEmpty(input))
			{
				return "''";
			}
			return "'" + SqlServerEscapeHelper.EscapeStringAsLiteral(input) + "'";
		}
	}
}
