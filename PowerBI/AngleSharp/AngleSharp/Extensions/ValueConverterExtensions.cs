using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Css;
using AngleSharp.Css.ValueConverters;
using AngleSharp.Css.Values;
using AngleSharp.Parser.Css;

namespace AngleSharp.Extensions
{
	// Token: 0x020000FA RID: 250
	internal static class ValueConverterExtensions
	{
		// Token: 0x060007F6 RID: 2038 RVA: 0x0003702C File Offset: 0x0003522C
		public static IPropertyValue ConvertDefault(this IValueConverter converter)
		{
			return converter.Convert(Enumerable.Empty<CssToken>());
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00037039 File Offset: 0x00035239
		public static bool HasDefault(this IValueConverter converter)
		{
			return converter.ConvertDefault() != null;
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00037044 File Offset: 0x00035244
		public static IPropertyValue VaryStart(this IValueConverter converter, List<CssToken> list)
		{
			for (int i = list.Count; i > 0; i--)
			{
				if (list[i - 1].Type != CssTokenType.Whitespace)
				{
					IPropertyValue propertyValue = converter.Convert(list.Take(i));
					if (propertyValue != null)
					{
						list.RemoveRange(0, i);
						list.Trim();
						return propertyValue;
					}
				}
			}
			return converter.ConvertDefault();
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0003709C File Offset: 0x0003529C
		public static IPropertyValue VaryAll(this IValueConverter converter, List<CssToken> list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Type != CssTokenType.Whitespace)
				{
					for (int j = list.Count; j > i; j--)
					{
						int num = j - i;
						if (list[j - 1].Type != CssTokenType.Whitespace)
						{
							IPropertyValue propertyValue = converter.Convert(list.Skip(i).Take(num));
							if (propertyValue != null)
							{
								list.RemoveRange(i, num);
								list.Trim();
								return propertyValue;
							}
						}
					}
				}
			}
			return converter.ConvertDefault();
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0003711E File Offset: 0x0003531E
		public static IValueConverter Many(this IValueConverter converter, int min = 1, int max = 65535)
		{
			return new OneOrMoreValueConverter(converter, min, max);
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00037128 File Offset: 0x00035328
		public static IValueConverter FromList(this IValueConverter converter)
		{
			return new ListValueConverter(converter);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00037130 File Offset: 0x00035330
		public static IValueConverter ToConverter<T>(this Dictionary<string, T> values)
		{
			return new DictionaryValueConverter<T>(values);
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00037138 File Offset: 0x00035338
		public static IValueConverter Periodic(this IValueConverter converter, params string[] labels)
		{
			return new PeriodicValueConverter(converter, labels);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00037141 File Offset: 0x00035341
		public static IValueConverter RequiresEnd(this IValueConverter listConverter, IValueConverter endConverter)
		{
			return new EndListValueConverter(listConverter, endConverter);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0003714A File Offset: 0x0003534A
		public static IValueConverter Required(this IValueConverter converter)
		{
			return new RequiredValueConverter(converter);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00037152 File Offset: 0x00035352
		public static IValueConverter Option(this IValueConverter converter)
		{
			return new OptionValueConverter(converter);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0003715A File Offset: 0x0003535A
		public static IValueConverter For(this IValueConverter converter, params string[] labels)
		{
			return new ConstraintValueConverter(converter, labels);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00037163 File Offset: 0x00035363
		public static IValueConverter Option<T>(this IValueConverter converter, T defaultValue)
		{
			return new OptionValueConverter<T>(converter, defaultValue);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0003716C File Offset: 0x0003536C
		public static IValueConverter Or(this IValueConverter primary, IValueConverter secondary)
		{
			return new OrValueConverter(primary, secondary);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00037175 File Offset: 0x00035375
		public static IValueConverter Or(this IValueConverter primary, string keyword)
		{
			return primary.Or(keyword, null);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00037180 File Offset: 0x00035380
		public static IValueConverter Or<T>(this IValueConverter primary, string keyword, T value)
		{
			IdentifierValueConverter<T> identifierValueConverter = new IdentifierValueConverter<T>(keyword, value);
			return new OrValueConverter(primary, identifierValueConverter);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0003719C File Offset: 0x0003539C
		public static IValueConverter OrNone(this IValueConverter primary)
		{
			return primary.Or(Keywords.None);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x000371A9 File Offset: 0x000353A9
		public static IValueConverter OrDefault(this IValueConverter primary)
		{
			return primary.OrInherit().Or(Keywords.Initial);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x000371BB File Offset: 0x000353BB
		public static IValueConverter OrDefault<T>(this IValueConverter primary, T value)
		{
			return primary.OrInherit().Or(Keywords.Initial, value);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x000371CE File Offset: 0x000353CE
		public static IValueConverter OrInherit(this IValueConverter primary)
		{
			return primary.Or(Keywords.Inherit);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x000371DB File Offset: 0x000353DB
		public static IValueConverter OrAuto(this IValueConverter primary)
		{
			return primary.Or(Keywords.Auto);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x000371E8 File Offset: 0x000353E8
		public static IValueConverter StartsWithKeyword(this IValueConverter converter, string keyword)
		{
			return new StartsWithValueConverter(CssTokenType.Ident, keyword, converter);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x000371F2 File Offset: 0x000353F2
		public static IValueConverter StartsWithDelimiter(this IValueConverter converter)
		{
			return new StartsWithValueConverter(CssTokenType.Delim, "/", converter);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00037201 File Offset: 0x00035401
		public static IValueConverter WithCurrentColor(this IValueConverter converter)
		{
			return converter.Or(Keywords.CurrentColor, Color.Transparent);
		}
	}
}
