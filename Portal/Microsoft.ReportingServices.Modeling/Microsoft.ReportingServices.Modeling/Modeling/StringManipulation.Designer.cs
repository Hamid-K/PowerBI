using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Text.RegularExpressions;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000BF RID: 191
	public static class StringManipulation
	{
		// Token: 0x06000B0E RID: 2830 RVA: 0x00024BAC File Offset: 0x00022DAC
		public static string GetPlural(string singular, CultureInfo culture)
		{
			return StringManipulation.GetRegexReplacer(culture, StringManipulation.m_pluralRegexes, "PluralRegexes").ProcessString(singular);
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00024BC4 File Offset: 0x00022DC4
		public static string GetSingular(string noun, CultureInfo culture)
		{
			return StringManipulation.GetRegexReplacer(culture, StringManipulation.m_singularRegexes, "SingularRegexes").ProcessString(noun);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00024BDC File Offset: 0x00022DDC
		public static int? GetDigitSuffix(string value)
		{
			return StringUtil.GetDigitSuffix(value);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00024BE4 File Offset: 0x00022DE4
		public static string SetDigitSuffix(string value, int suffix)
		{
			return StringUtil.SetDigitSuffix(value, suffix);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00024BED File Offset: 0x00022DED
		public static string IncrementDigitSuffix(string value, int defaultSuffix)
		{
			return StringUtil.IncrementDigitSuffix(value, defaultSuffix);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00024BF6 File Offset: 0x00022DF6
		public static string IncrementDigitSuffix(string value)
		{
			return StringUtil.IncrementDigitSuffix(value);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00024BFE File Offset: 0x00022DFE
		public static string TrimToMaxLength(string value, int maxLength)
		{
			return StringUtil.TrimToMaxLength(value, maxLength);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00024C08 File Offset: 0x00022E08
		private static StringManipulation.RegexReplacer GetRegexReplacer(CultureInfo culture, Dictionary<CultureInfo, StringManipulation.RegexReplacer> dict, string resourceName)
		{
			StringManipulation.RegexReplacer regexReplacer;
			lock (dict)
			{
				if (!dict.TryGetValue(culture, out regexReplacer))
				{
					Type typeFromHandle = typeof(StringManipulation);
					string text = StringManipulation.m_rm.GetString(resourceName, culture);
					if (string.IsNullOrEmpty(text))
					{
						CultureInfo cultureInfo = culture;
						ResourceSet resourceSet;
						for (;;)
						{
							resourceSet = new ResourceManager(typeFromHandle.FullName + "_" + cultureInfo.Name, typeFromHandle.Assembly).GetResourceSet(CultureInfo.InvariantCulture, true, false);
							if (resourceSet != null)
							{
								break;
							}
							if (cultureInfo.Parent == CultureInfo.InvariantCulture)
							{
								goto IL_0098;
							}
							cultureInfo = cultureInfo.Parent;
						}
						text = resourceSet.GetString(resourceName);
					}
					IL_0098:
					regexReplacer = new StringManipulation.RegexReplacer(text);
					dict.Add(culture, regexReplacer);
				}
			}
			return regexReplacer;
		}

		// Token: 0x0400048F RID: 1167
		private static readonly ResourceManager m_rm = new ResourceManager(typeof(StringManipulation));

		// Token: 0x04000490 RID: 1168
		private static readonly Dictionary<CultureInfo, StringManipulation.RegexReplacer> m_pluralRegexes = new Dictionary<CultureInfo, StringManipulation.RegexReplacer>();

		// Token: 0x04000491 RID: 1169
		private static readonly Dictionary<CultureInfo, StringManipulation.RegexReplacer> m_singularRegexes = new Dictionary<CultureInfo, StringManipulation.RegexReplacer>();

		// Token: 0x020001BC RID: 444
		private class RegexReplacer
		{
			// Token: 0x0600113B RID: 4411 RVA: 0x00036028 File Offset: 0x00034228
			public RegexReplacer(string regexList)
			{
				if (string.IsNullOrEmpty(regexList))
				{
					return;
				}
				StringReader stringReader = new StringReader(regexList);
				while (stringReader.Peek() != -1)
				{
					string text = stringReader.ReadLine();
					string text2 = stringReader.ReadLine();
					Regex regex = new Regex(text, RegexOptions.Compiled);
					this.m_replacements.Add(new StringManipulation.RegexReplacer.RegexReplacement(regex, text2));
				}
			}

			// Token: 0x0600113C RID: 4412 RVA: 0x0003608C File Offset: 0x0003428C
			public string ProcessString(string value)
			{
				if (string.IsNullOrEmpty(value))
				{
					return value;
				}
				foreach (StringManipulation.RegexReplacer.RegexReplacement regexReplacement in this.m_replacements)
				{
					if (regexReplacement.Regex.IsMatch(value))
					{
						return regexReplacement.Regex.Replace(value, regexReplacement.ReplaceString);
					}
				}
				return value;
			}

			// Token: 0x040007C0 RID: 1984
			private readonly List<StringManipulation.RegexReplacer.RegexReplacement> m_replacements = new List<StringManipulation.RegexReplacer.RegexReplacement>();

			// Token: 0x020001FE RID: 510
			private struct RegexReplacement
			{
				// Token: 0x06001222 RID: 4642 RVA: 0x00037E83 File Offset: 0x00036083
				public RegexReplacement(Regex regex, string replaceString)
				{
					this.Regex = regex;
					this.ReplaceString = replaceString;
				}

				// Token: 0x0400087A RID: 2170
				public Regex Regex;

				// Token: 0x0400087B RID: 2171
				public string ReplaceString;
			}
		}
	}
}
