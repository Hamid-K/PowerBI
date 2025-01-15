using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Css.Values;
using AngleSharp.Dom.Css;
using AngleSharp.Parser.Css;

namespace AngleSharp.Extensions
{
	// Token: 0x020000FB RID: 251
	internal static class ValueExtensions
	{
		// Token: 0x0600080E RID: 2062 RVA: 0x00037214 File Offset: 0x00035414
		public static CssToken OnlyOrDefault(this IEnumerable<CssToken> value)
		{
			CssToken cssToken = null;
			foreach (CssToken cssToken2 in value)
			{
				if (cssToken != null)
				{
					cssToken = null;
					break;
				}
				cssToken = cssToken2;
			}
			return cssToken;
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00037264 File Offset: 0x00035464
		public static bool Is(this IEnumerable<CssToken> value, string expected)
		{
			string text = value.ToIdentifier();
			return text != null && text.Isi(expected);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00037284 File Offset: 0x00035484
		public static string ToUri(this IEnumerable<CssToken> value)
		{
			CssToken cssToken = value.OnlyOrDefault();
			if (cssToken != null && cssToken.Type == CssTokenType.Url)
			{
				return cssToken.Data;
			}
			return null;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x000372AC File Offset: 0x000354AC
		public static Length? ToDistance(this IEnumerable<CssToken> value)
		{
			Percent? percent = value.ToPercent();
			if (percent != null)
			{
				return new Length?(new Length(percent.Value.Value, Length.Unit.Percent));
			}
			return value.ToLength();
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x000372EC File Offset: 0x000354EC
		public static Length ToLength(this FontSize fontSize)
		{
			switch (fontSize)
			{
			case FontSize.Tiny:
				return new Length(0.6f, Length.Unit.Em);
			case FontSize.Little:
				return new Length(0.75f, Length.Unit.Em);
			case FontSize.Smaller:
				return new Length(80f, Length.Unit.Percent);
			case FontSize.Small:
				return new Length(0.8888889f, Length.Unit.Em);
			case FontSize.Large:
				return new Length(1.2f, Length.Unit.Em);
			case FontSize.Larger:
				return new Length(120f, Length.Unit.Percent);
			case FontSize.Big:
				return new Length(1.5f, Length.Unit.Em);
			case FontSize.Huge:
				return new Length(2f, Length.Unit.Em);
			}
			return new Length(1f, Length.Unit.Em);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00037394 File Offset: 0x00035594
		public static Percent? ToPercent(this IEnumerable<CssToken> value)
		{
			CssToken cssToken = value.OnlyOrDefault();
			if (cssToken != null && cssToken.Type == CssTokenType.Percentage)
			{
				return new Percent?(new Percent(((CssUnitToken)cssToken).Value));
			}
			return null;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x000373D4 File Offset: 0x000355D4
		public static string ToCssString(this IEnumerable<CssToken> value)
		{
			CssToken cssToken = value.OnlyOrDefault();
			if (cssToken != null && cssToken.Type == CssTokenType.String)
			{
				return cssToken.Data;
			}
			return null;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x000373FC File Offset: 0x000355FC
		public static string ToLiterals(this IEnumerable<CssToken> value)
		{
			List<string> list = new List<string>();
			IEnumerator<CssToken> enumerator = value.GetEnumerator();
			if (enumerator.MoveNext())
			{
				while (enumerator.Current.Type == CssTokenType.Ident)
				{
					list.Add(enumerator.Current.Data);
					if (enumerator.MoveNext() && enumerator.Current.Type != CssTokenType.Whitespace)
					{
						return null;
					}
					if (!enumerator.MoveNext())
					{
						return string.Join(" ", list);
					}
				}
				return null;
			}
			return null;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00037470 File Offset: 0x00035670
		public static string ToIdentifier(this IEnumerable<CssToken> value)
		{
			CssToken cssToken = value.OnlyOrDefault();
			if (cssToken != null && cssToken.Type == CssTokenType.Ident)
			{
				return cssToken.Data.ToLowerInvariant();
			}
			return null;
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x000374A0 File Offset: 0x000356A0
		public static string ToAnimatableIdentifier(this IEnumerable<CssToken> value)
		{
			string text = value.ToIdentifier();
			if (text != null && (text.Isi(Keywords.All) || Factory.Properties.IsAnimatable(text)))
			{
				return text;
			}
			return null;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x000374D4 File Offset: 0x000356D4
		public static float? ToSingle(this IEnumerable<CssToken> value)
		{
			CssToken cssToken = value.OnlyOrDefault();
			if (cssToken != null && cssToken.Type == CssTokenType.Number)
			{
				return new float?(((CssNumberToken)cssToken).Value);
			}
			return null;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00037510 File Offset: 0x00035710
		public static float? ToNaturalSingle(this IEnumerable<CssToken> value)
		{
			float? num = value.ToSingle();
			if (num == null || num.Value < 0f)
			{
				return null;
			}
			return num;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00037548 File Offset: 0x00035748
		public static float? ToGreaterOrEqualOneSingle(this IEnumerable<CssToken> value)
		{
			float? num = value.ToSingle();
			if (num == null || num.Value < 1f)
			{
				return null;
			}
			return num;
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00037580 File Offset: 0x00035780
		public static int? ToInteger(this IEnumerable<CssToken> value)
		{
			CssToken cssToken = value.OnlyOrDefault();
			if (cssToken != null && cssToken.Type == CssTokenType.Number && ((CssNumberToken)cssToken).IsInteger)
			{
				return new int?(((CssNumberToken)cssToken).IntegerValue);
			}
			return null;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x000375C8 File Offset: 0x000357C8
		public static int? ToNaturalInteger(this IEnumerable<CssToken> value)
		{
			int? num = value.ToInteger();
			if (num == null || num.Value < 0)
			{
				return null;
			}
			return num;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x000375FC File Offset: 0x000357FC
		public static int? ToPositiveInteger(this IEnumerable<CssToken> value)
		{
			int? num = value.ToInteger();
			if (num == null || num.Value <= 0)
			{
				return null;
			}
			return num;
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00037630 File Offset: 0x00035830
		public static int? ToWeightInteger(this IEnumerable<CssToken> value)
		{
			int? num = value.ToPositiveInteger();
			if (num == null || !ValueExtensions.IsWeight(num.Value))
			{
				return null;
			}
			return num;
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00037668 File Offset: 0x00035868
		public static int? ToBinary(this IEnumerable<CssToken> value)
		{
			int? num = value.ToInteger();
			if (num == null || (num.Value != 0 && num.Value != 1))
			{
				return null;
			}
			return num;
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x000376A4 File Offset: 0x000358A4
		public static float? ToAlphaValue(this IEnumerable<CssToken> value)
		{
			float? num = value.ToNaturalSingle();
			if (num != null)
			{
				return new float?(Math.Min(num.Value, 1f));
			}
			Percent? percent = value.ToPercent();
			if (percent == null)
			{
				return null;
			}
			return new float?(percent.Value.NormalizedValue);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00037708 File Offset: 0x00035908
		public static byte? ToRgbComponent(this IEnumerable<CssToken> value)
		{
			int? num = value.ToNaturalInteger();
			if (num != null)
			{
				return new byte?((byte)Math.Min(num.Value, 255));
			}
			Percent? percent = value.ToPercent();
			if (percent == null)
			{
				return null;
			}
			return new byte?((byte)(255f * percent.Value.NormalizedValue));
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00037774 File Offset: 0x00035974
		public static Angle? ToAngle(this IEnumerable<CssToken> value)
		{
			CssToken cssToken = value.OnlyOrDefault();
			if (cssToken != null && cssToken.Type == CssTokenType.Dimension)
			{
				CssUnitToken cssUnitToken = (CssUnitToken)cssToken;
				Angle.Unit unit = Angle.GetUnit(cssUnitToken.Unit);
				if (unit != Angle.Unit.None)
				{
					return new Angle?(new Angle(cssUnitToken.Value, unit));
				}
			}
			return null;
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x000377C8 File Offset: 0x000359C8
		public static Angle? ToAngleNumber(this IEnumerable<CssToken> value)
		{
			Angle? angle = value.ToAngle();
			if (angle != null)
			{
				return new Angle?(angle.Value);
			}
			float? num = value.ToSingle();
			if (num == null)
			{
				return null;
			}
			return new Angle?(new Angle(num.Value, Angle.Unit.Deg));
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00037820 File Offset: 0x00035A20
		public static Frequency? ToFrequency(this IEnumerable<CssToken> value)
		{
			CssToken cssToken = value.OnlyOrDefault();
			if (cssToken != null && cssToken.Type == CssTokenType.Dimension)
			{
				CssUnitToken cssUnitToken = (CssUnitToken)cssToken;
				Frequency.Unit unit = Frequency.GetUnit(cssUnitToken.Unit);
				if (unit != Frequency.Unit.None)
				{
					return new Frequency?(new Frequency(cssUnitToken.Value, unit));
				}
			}
			return null;
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00037874 File Offset: 0x00035A74
		public static Length? ToLength(this IEnumerable<CssToken> value)
		{
			CssToken cssToken = value.OnlyOrDefault();
			if (cssToken != null)
			{
				if (cssToken.Type == CssTokenType.Dimension)
				{
					CssUnitToken cssUnitToken = (CssUnitToken)cssToken;
					Length.Unit unit = Length.GetUnit(cssUnitToken.Unit);
					if (unit != Length.Unit.None)
					{
						return new Length?(new Length(cssUnitToken.Value, unit));
					}
				}
				else if (cssToken.Type == CssTokenType.Number && ((CssNumberToken)cssToken).Value == 0f)
				{
					return new Length?(Length.Zero);
				}
			}
			return null;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x000378EC File Offset: 0x00035AEC
		public static Resolution? ToResolution(this IEnumerable<CssToken> value)
		{
			CssToken cssToken = value.OnlyOrDefault();
			if (cssToken != null && cssToken.Type == CssTokenType.Dimension)
			{
				CssUnitToken cssUnitToken = (CssUnitToken)cssToken;
				Resolution.Unit unit = Resolution.GetUnit(cssUnitToken.Unit);
				if (unit != Resolution.Unit.None)
				{
					return new Resolution?(new Resolution(cssUnitToken.Value, unit));
				}
			}
			return null;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00037940 File Offset: 0x00035B40
		public static Time? ToTime(this IEnumerable<CssToken> value)
		{
			CssToken cssToken = value.OnlyOrDefault();
			if (cssToken != null && cssToken.Type == CssTokenType.Dimension)
			{
				CssUnitToken cssUnitToken = (CssUnitToken)cssToken;
				Time.Unit unit = Time.GetUnit(cssUnitToken.Unit);
				if (unit != Time.Unit.None)
				{
					return new Time?(new Time(cssUnitToken.Value, unit));
				}
			}
			return null;
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00037994 File Offset: 0x00035B94
		public static Length? ToBorderWidth(this IEnumerable<CssToken> value)
		{
			Length? length = value.ToLength();
			if (length != null)
			{
				return length;
			}
			if (value.Is(Keywords.Thin))
			{
				return new Length?(Length.Thin);
			}
			if (value.Is(Keywords.Medium))
			{
				return new Length?(Length.Medium);
			}
			if (value.Is(Keywords.Thick))
			{
				return new Length?(Length.Thick);
			}
			return length;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x000379FC File Offset: 0x00035BFC
		public static List<List<CssToken>> ToItems(this IEnumerable<CssToken> value)
		{
			List<List<CssToken>> list = new List<List<CssToken>>();
			List<CssToken> list2 = new List<CssToken>();
			int num = 0;
			list.Add(list2);
			foreach (CssToken cssToken in value)
			{
				bool flag = cssToken.Type == CssTokenType.Whitespace;
				bool flag2 = cssToken.Type == CssTokenType.String || cssToken.Type == CssTokenType.Url || cssToken.Type == CssTokenType.Function;
				if (num == 0 && (flag || flag2))
				{
					if (list2.Count != 0)
					{
						list2 = new List<CssToken>();
						list.Add(list2);
					}
					if (flag)
					{
						continue;
					}
				}
				else if (cssToken.Type == CssTokenType.RoundBracketOpen)
				{
					num++;
				}
				else if (cssToken.Type == CssTokenType.RoundBracketClose)
				{
					num--;
				}
				list2.Add(cssToken);
			}
			return list;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00037AD8 File Offset: 0x00035CD8
		public static void Trim(this List<CssToken> value)
		{
			int i = 0;
			int num = value.Count - 1;
			while (i < num)
			{
				if (value[i].Type == CssTokenType.Whitespace)
				{
					i++;
				}
				else
				{
					if (value[num].Type != CssTokenType.Whitespace)
					{
						break;
					}
					num--;
				}
			}
			value.RemoveRange(++num, value.Count - num);
			value.RemoveRange(0, i);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00037B3C File Offset: 0x00035D3C
		public static List<List<CssToken>> ToList(this IEnumerable<CssToken> value)
		{
			List<List<CssToken>> list = new List<List<CssToken>>();
			List<CssToken> list2 = new List<CssToken>();
			int num = 0;
			list.Add(list2);
			foreach (CssToken cssToken in value)
			{
				if (num == 0 && cssToken.Type == CssTokenType.Comma)
				{
					list2 = new List<CssToken>();
					list.Add(list2);
				}
				else
				{
					if (cssToken.Type == CssTokenType.RoundBracketOpen)
					{
						num++;
					}
					else if (cssToken.Type == CssTokenType.RoundBracketClose)
					{
						num--;
					}
					else if (cssToken.Type == CssTokenType.Whitespace && list2.Count == 0)
					{
						continue;
					}
					list2.Add(cssToken);
				}
			}
			for (int i = 0; i < list.Count; i++)
			{
				list[i].Trim();
			}
			return list;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00037C10 File Offset: 0x00035E10
		public static string ToText(this IEnumerable<CssToken> value)
		{
			return string.Join(string.Empty, value.Select((CssToken m) => m.ToValue()));
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00037C44 File Offset: 0x00035E44
		public static Color? ToColor(this IEnumerable<CssToken> value)
		{
			CssToken cssToken = value.OnlyOrDefault();
			if (cssToken != null && cssToken.Type == CssTokenType.Ident)
			{
				return Color.FromName(cssToken.Data);
			}
			if (cssToken != null && cssToken.Type == CssTokenType.Color && !((CssColorToken)cssToken).IsBad)
			{
				return new Color?(Color.FromHex(cssToken.Data));
			}
			return null;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00037CA4 File Offset: 0x00035EA4
		private static bool IsWeight(int value)
		{
			return value == 100 || value == 200 || value == 300 || value == 400 || value == 500 || value == 600 || value == 700 || value == 800 || value == 900;
		}
	}
}
