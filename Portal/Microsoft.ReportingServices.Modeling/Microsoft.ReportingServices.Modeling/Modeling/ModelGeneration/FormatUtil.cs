using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000CA RID: 202
	internal static class FormatUtil
	{
		// Token: 0x06000B61 RID: 2913 RVA: 0x0002562C File Offset: 0x0002382C
		public static bool ParseFormatString(string format, CultureInfo culture, out char specifier, out int decimalPlaces, out bool decimalPlacesSpecified)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			culture = culture ?? CultureInfo.CurrentCulture;
			Match match = FormatUtil.m_formatStringRegex.Match(format);
			if (!match.Groups["s"].Success)
			{
				specifier = '\0';
				decimalPlaces = -1;
				decimalPlacesSpecified = false;
				return false;
			}
			if (match.Groups["s"].Value.Length < 1)
			{
				throw new InternalModelingException("formatStringRegex 's' should be one char");
			}
			specifier = match.Groups["s"].Value[0];
			if (match.Groups["d"].Success)
			{
				decimalPlaces = Convert.ToInt32(match.Groups["d"].Value, CultureInfo.InvariantCulture);
				decimalPlacesSpecified = true;
			}
			else
			{
				char c = char.ToLowerInvariant(specifier);
				switch (c)
				{
				case 'c':
					decimalPlaces = culture.NumberFormat.CurrencyDecimalDigits;
					goto IL_012E;
				case 'd':
					goto IL_012B;
				case 'e':
					decimalPlaces = 6;
					goto IL_012E;
				case 'f':
					break;
				default:
					if (c != 'n')
					{
						if (c != 'p')
						{
							goto IL_012B;
						}
						decimalPlaces = culture.NumberFormat.PercentDecimalDigits;
						goto IL_012E;
					}
					break;
				}
				decimalPlaces = culture.NumberFormat.NumberDecimalDigits;
				goto IL_012E;
				IL_012B:
				decimalPlaces = -1;
				IL_012E:
				decimalPlacesSpecified = false;
			}
			return true;
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0002576C File Offset: 0x0002396C
		public static int EstimateWidthChars(string format, int beforeDecimalWidth, CultureInfo culture)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			culture = culture ?? CultureInfo.CurrentCulture;
			NumberFormatInfo numberFormat = culture.NumberFormat;
			char c;
			int num;
			bool flag;
			if (FormatUtil.ParseFormatString(format, culture, out c, out num, out flag))
			{
				char c2 = char.ToLowerInvariant(c);
				if (c2 <= 'n')
				{
					switch (c2)
					{
					case 'c':
						return numberFormat.CurrencySymbol.Length + FormatUtil.GetGroupWidthChars(numberFormat.CurrencyGroupSeparator, numberFormat.CurrencyGroupSizes, beforeDecimalWidth) + beforeDecimalWidth + num;
					case 'd':
						break;
					case 'e':
						return 6 + num + 1;
					case 'f':
					case 'g':
						return beforeDecimalWidth + num + 1;
					default:
						if (c2 != 'n')
						{
							return beforeDecimalWidth;
						}
						return FormatUtil.GetGroupWidthChars(numberFormat.NumberGroupSeparator, numberFormat.NumberGroupSizes, beforeDecimalWidth) + beforeDecimalWidth + num;
					}
				}
				else
				{
					if (c2 == 'p')
					{
						return FormatUtil.GetGroupWidthChars(numberFormat.PercentGroupSeparator, numberFormat.PercentGroupSizes, beforeDecimalWidth + 2) + beforeDecimalWidth + 2 + num;
					}
					if (c2 != 'x')
					{
						return beforeDecimalWidth;
					}
				}
				if (flag)
				{
					return num;
				}
				return beforeDecimalWidth;
			}
			return beforeDecimalWidth;
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00025858 File Offset: 0x00023A58
		private static int GetGroupWidthChars(string groupSep, int[] groupSizes, int rawWidth)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			while (num3 < groupSizes.Length && groupSizes[num3] != 0)
			{
				if (groupSizes[num3] + num < rawWidth)
				{
					num += groupSizes[num3];
					num2++;
					if (num3 == groupSizes.Length - 1)
					{
						num3--;
					}
				}
				num3++;
			}
			return num2 * groupSep.Length;
		}

		// Token: 0x040004A8 RID: 1192
		private static readonly Regex m_formatStringRegex = new Regex("^(?<s>c|d|e|f|g|n|p|r|x)(?<d>\\d+)?$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
	}
}
