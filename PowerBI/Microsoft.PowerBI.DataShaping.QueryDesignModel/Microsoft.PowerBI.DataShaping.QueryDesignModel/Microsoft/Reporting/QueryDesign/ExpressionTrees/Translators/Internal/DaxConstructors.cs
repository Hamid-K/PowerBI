using System;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000125 RID: 293
	internal static class DaxConstructors
	{
		// Token: 0x06001049 RID: 4169 RVA: 0x0002CA2E File Offset: 0x0002AC2E
		internal static string RowConstructor(string arguments, int argsCount)
		{
			if (argsCount > 1)
			{
				return DaxConstructors.Invoke("(", ")", arguments);
			}
			return DaxConstructors.Invoke(null, null, arguments);
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0002CA4D File Offset: 0x0002AC4D
		internal static string TableConstructor(string arguments)
		{
			return DaxConstructors.Invoke("{", "}", arguments);
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0002CA5F File Offset: 0x0002AC5F
		private static string Invoke(string begin, string end, string arguments)
		{
			return begin + arguments + end;
		}
	}
}
