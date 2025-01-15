using System;
using System.Text.RegularExpressions;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x020007AE RID: 1966
	public class Wildcard : Regex
	{
		// Token: 0x06003ED8 RID: 16088 RVA: 0x000D2898 File Offset: 0x000D0A98
		public Wildcard(string pattern)
			: base(Wildcard.WildcardToRegex(pattern))
		{
		}

		// Token: 0x06003ED9 RID: 16089 RVA: 0x000D28A6 File Offset: 0x000D0AA6
		public Wildcard(string pattern, RegexOptions options)
			: base(Wildcard.WildcardToRegex(pattern), options)
		{
		}

		// Token: 0x06003EDA RID: 16090 RVA: 0x000D28B5 File Offset: 0x000D0AB5
		public static string WildcardToRegex(string pattern)
		{
			return "^" + Regex.Escape(pattern).Replace("\\*", ".*").Replace("\\?", ".") + "\\s*$";
		}
	}
}
