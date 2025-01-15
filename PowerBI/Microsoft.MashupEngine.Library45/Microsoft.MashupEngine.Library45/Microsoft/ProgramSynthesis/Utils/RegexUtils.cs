using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004FE RID: 1278
	public static class RegexUtils
	{
		// Token: 0x06001C85 RID: 7301 RVA: 0x0005548F File Offset: 0x0005368F
		public static IEnumerable<Match> NonCachingMatches(this Regex regex, string s)
		{
			int pos = 0;
			bool flag;
			do
			{
				Match match = regex.Match(s, pos);
				if (match.Success)
				{
					pos = match.Index + ((match.Length == 0) ? 1 : match.Length);
					yield return match;
				}
				flag = !match.Success || pos > s.Length;
				match = null;
			}
			while (!flag);
			yield break;
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x000554A6 File Offset: 0x000536A6
		public static IEnumerable<Match> NonCachingMatches(this IReadOnlyList<Regex> regexes, string s)
		{
			int pos = 0;
			bool flag;
			do
			{
				Match match = null;
				foreach (Regex regex in regexes)
				{
					match = regex.Match(s, pos);
					if (match.Success)
					{
						break;
					}
				}
				if (match.Success)
				{
					pos = match.Index + ((match.Length == 0) ? 1 : match.Length);
					yield return match;
				}
				flag = !match.Success || pos > s.Length;
				match = null;
			}
			while (!flag);
			yield break;
		}
	}
}
