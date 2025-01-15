using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Microsoft.DataShaping
{
	// Token: 0x0200000D RID: 13
	internal static class StringUtil
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00003279 File Offset: 0x00001479
		[DebuggerStepThrough]
		internal static string FormatInvariant(string format, params object[] args)
		{
			return string.Format(CultureInfo.InvariantCulture, format, args);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003287 File Offset: 0x00001487
		[DebuggerStepThrough]
		internal static string Format(CultureInfo culture, string format, params object[] args)
		{
			return string.Format(culture, format, args);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003294 File Offset: 0x00001494
		public static int? GetDigitSuffix(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				Match match = StringUtil.DigitSuffixRegex.Match(value);
				if (match.Success)
				{
					return new int?(int.Parse(match.Groups["suffix"].Value, CultureInfo.InvariantCulture));
				}
			}
			return null;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000032EC File Offset: 0x000014EC
		public static string SetDigitSuffix(string value, int suffix)
		{
			int? digitSuffix = StringUtil.GetDigitSuffix(value);
			string text = suffix.ToString(CultureInfo.InvariantCulture);
			if (digitSuffix == null)
			{
				return value + text;
			}
			return StringUtil.DigitSuffixRegex.Replace(value, text);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000332C File Offset: 0x0000152C
		public static string IncrementDigitSuffix(string value, int defaultSuffix)
		{
			int? digitSuffix = StringUtil.GetDigitSuffix(value);
			if (digitSuffix == null)
			{
				return StringUtil.SetDigitSuffix(value, defaultSuffix);
			}
			return StringUtil.SetDigitSuffix(value, digitSuffix.Value + 1);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003360 File Offset: 0x00001560
		public static string IncrementDigitSuffix(string value)
		{
			return StringUtil.IncrementDigitSuffix(value, 2);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003369 File Offset: 0x00001569
		internal static string MakeUniqueId(string candidate, IEnumerable<string> usedIds)
		{
			return StringUtil.MakeUniqueId(candidate, new HashSet<string>(usedIds, StringComparer.Ordinal));
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000337C File Offset: 0x0000157C
		internal static string MakeUniqueId(string candidate, HashSet<string> usedIds)
		{
			while (!usedIds.Add(candidate))
			{
				candidate = StringUtil.IncrementDigitSuffix(candidate);
			}
			return candidate;
		}

		// Token: 0x0400003F RID: 63
		private const string SuffixGroup = "suffix";

		// Token: 0x04000040 RID: 64
		private static readonly Regex DigitSuffixRegex = new Regex("(?<suffix>\\d{1,9})$");
	}
}
