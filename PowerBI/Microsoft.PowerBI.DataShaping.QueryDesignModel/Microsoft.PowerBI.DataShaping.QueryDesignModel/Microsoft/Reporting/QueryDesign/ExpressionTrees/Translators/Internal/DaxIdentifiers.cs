using System;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000134 RID: 308
	internal static class DaxIdentifiers
	{
		// Token: 0x06001129 RID: 4393 RVA: 0x0002FC80 File Offset: 0x0002DE80
		internal static string EscapeSquareBracketedIdentifier(string name)
		{
			return name.Replace("]", "]]");
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x0002FC92 File Offset: 0x0002DE92
		internal static string CreateSquareBracketedIdentifierWithPrefix(string prefix, string name)
		{
			return prefix + "[" + DaxIdentifiers.EscapeSquareBracketedIdentifier(name) + "]";
		}
	}
}
