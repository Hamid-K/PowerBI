using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Caching;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions
{
	// Token: 0x020017CC RID: 6092
	public static class RegexExtension
	{
		// Token: 0x0600C918 RID: 51480 RVA: 0x002B1B9C File Offset: 0x002AFD9C
		public static Regex ToRegex(this string pattern, bool ignoreCase)
		{
			RegexOptions regexOptions = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant;
			if (ignoreCase)
			{
				regexOptions |= RegexOptions.IgnoreCase;
			}
			return pattern.ToRegex(regexOptions);
		}

		// Token: 0x0600C919 RID: 51481 RVA: 0x002B1BC0 File Offset: 0x002AFDC0
		public static Regex ToRegex(this string pattern, RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant)
		{
			object[] array = new object[]
			{
				pattern,
				(int)options
			};
			Regex regex;
			if (RegexExtension._regexCache.Lookup(array, out regex))
			{
				return regex;
			}
			Regex regex2 = new Regex(pattern, options);
			RegexExtension._regexCache.Add(array, regex2);
			return regex2;
		}

		// Token: 0x0600C91A RID: 51482 RVA: 0x002B1C08 File Offset: 0x002AFE08
		public static IEnumerable<Match> FullMatches(this IEnumerable<Regex> regexList, string subject)
		{
			return from regex in regexList
				from match in regex.FullMatches(subject)
				select match;
		}

		// Token: 0x0600C91B RID: 51483 RVA: 0x002B1C54 File Offset: 0x002AFE54
		public static IEnumerable<Match> FullMatches(this Regex regex, string subject)
		{
			return from match in regex.NonCachingMatches(subject)
				where match.IsFullMatch(subject)
				select match;
		}

		// Token: 0x0600C91C RID: 51484 RVA: 0x002B1C8B File Offset: 0x002AFE8B
		public static bool IsFullMatch(this Regex regex, string input)
		{
			return regex.Match(input).IsFullMatch(input);
		}

		// Token: 0x0600C91D RID: 51485 RVA: 0x002B1C9A File Offset: 0x002AFE9A
		public static bool IsFullMatch(this string input, Match match)
		{
			return match.IsFullMatch(input);
		}

		// Token: 0x0600C91E RID: 51486 RVA: 0x002B1CA3 File Offset: 0x002AFEA3
		public static bool IsFullMatch(this Match match, string input)
		{
			return match.Success && match.Index == 0 && match.Length == input.Length && input == match.Value;
		}

		// Token: 0x04004F07 RID: 20231
		private static readonly ConcurrentLruCache<object[], Regex> _regexCache = new ConcurrentLruCache<object[], Regex>(65536, new ArrayEquality<object>(), null, null);
	}
}
