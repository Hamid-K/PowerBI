using System;
using System.Text.RegularExpressions;

namespace NLog.Conditions
{
	// Token: 0x020001AC RID: 428
	[ConditionMethods]
	public static class ConditionMethods
	{
		// Token: 0x06001319 RID: 4889 RVA: 0x00033D51 File Offset: 0x00031F51
		[ConditionMethod("equals")]
		public static bool Equals2(object firstValue, object secondValue)
		{
			return firstValue.Equals(secondValue);
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00033D5C File Offset: 0x00031F5C
		[ConditionMethod("strequals")]
		public static bool Equals2(string firstValue, string secondValue, bool ignoreCase = false)
		{
			return firstValue.Equals(secondValue, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x00033D7C File Offset: 0x00031F7C
		[ConditionMethod("contains")]
		public static bool Contains(string haystack, string needle, bool ignoreCase = true)
		{
			return haystack.IndexOf(needle, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) >= 0;
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00033DA0 File Offset: 0x00031FA0
		[ConditionMethod("starts-with")]
		public static bool StartsWith(string haystack, string needle, bool ignoreCase = true)
		{
			return haystack.StartsWith(needle, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00033DC0 File Offset: 0x00031FC0
		[ConditionMethod("ends-with")]
		public static bool EndsWith(string haystack, string needle, bool ignoreCase = true)
		{
			return haystack.EndsWith(needle, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00033DDD File Offset: 0x00031FDD
		[ConditionMethod("length")]
		public static int Length(string text)
		{
			return text.Length;
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00033DE8 File Offset: 0x00031FE8
		[ConditionMethod("regex-matches")]
		public static bool RegexMatches(string input, string pattern, string options = "")
		{
			RegexOptions regexOptions = ConditionMethods.ParseRegexOptions(options);
			return Regex.IsMatch(input, pattern, regexOptions);
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x00033E04 File Offset: 0x00032004
		private static RegexOptions ParseRegexOptions(string options)
		{
			if (string.IsNullOrEmpty(options))
			{
				return RegexOptions.None;
			}
			return (RegexOptions)Enum.Parse(typeof(RegexOptions), options, true);
		}
	}
}
