using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using AngleSharp.Attributes;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Dom;
using AngleSharp.Network;

namespace AngleSharp.Extensions
{
	// Token: 0x020000F6 RID: 246
	internal static class StringExtensions
	{
		// Token: 0x060007B8 RID: 1976 RVA: 0x00035FB5 File Offset: 0x000341B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Has(this string value, char chr, int index = 0)
		{
			return value != null && value.Length > index && value[index] == chr;
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00035FCF File Offset: 0x000341CF
		public static string GetCompatiblity(this QuirksMode mode)
		{
			DomDescriptionAttribute customAttribute = typeof(QuirksMode).GetTypeInfo().GetDeclaredField(mode.ToString()).GetCustomAttribute<DomDescriptionAttribute>();
			return ((customAttribute != null) ? customAttribute.Description : null) ?? "CSS1Compat";
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0003600C File Offset: 0x0003420C
		public static string HtmlLower(this string value)
		{
			int length = value.Length;
			for (int i = 0; i < length; i++)
			{
				char c = value[i];
				if (c.IsUppercaseAscii())
				{
					char[] array = new char[length];
					for (int j = 0; j < i; j++)
					{
						array[j] = value[j];
					}
					array[i] = char.ToLowerInvariant(c);
					for (int k = i + 1; k < length; k++)
					{
						c = value[k];
						if (c.IsUppercaseAscii())
						{
							c = char.ToLowerInvariant(c);
						}
						array[k] = c;
					}
					return new string(array);
				}
			}
			return value;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x000360A0 File Offset: 0x000342A0
		public static Sandboxes ParseSecuritySettings(this string value, bool allowFullscreen = false)
		{
			string[] array = value.SplitSpaces();
			Sandboxes sandboxes = Sandboxes.Navigation | Sandboxes.Plugins | Sandboxes.DocumentDomain;
			if (!array.Contains("allow-popups", StringComparison.OrdinalIgnoreCase))
			{
				sandboxes |= Sandboxes.AuxiliaryNavigation;
			}
			if (!array.Contains("allow-top-navigation", StringComparison.OrdinalIgnoreCase))
			{
				sandboxes |= Sandboxes.TopLevelNavigation;
			}
			if (!array.Contains("allow-same-origin", StringComparison.OrdinalIgnoreCase))
			{
				sandboxes |= Sandboxes.Origin;
			}
			if (!array.Contains("allow-forms", StringComparison.OrdinalIgnoreCase))
			{
				sandboxes |= Sandboxes.Forms;
			}
			if (!array.Contains("allow-pointer-lock", StringComparison.OrdinalIgnoreCase))
			{
				sandboxes |= Sandboxes.PointerLock;
			}
			if (!array.Contains("allow-scripts", StringComparison.OrdinalIgnoreCase))
			{
				sandboxes |= Sandboxes.Scripts;
			}
			if (!array.Contains("allow-scripts", StringComparison.OrdinalIgnoreCase))
			{
				sandboxes |= Sandboxes.AutomaticFeatures;
			}
			if (!allowFullscreen)
			{
				sandboxes |= Sandboxes.Fullscreen;
			}
			return sandboxes;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00036150 File Offset: 0x00034350
		public static T ToEnum<T>(this string value, T defaultValue) where T : struct, IComparable
		{
			T t = default(T);
			if (!string.IsNullOrEmpty(value) && Enum.TryParse<T>(value, true, out t))
			{
				return t;
			}
			return defaultValue;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0003617C File Offset: 0x0003437C
		public static double ToDouble(this string value, double defaultValue = 0.0)
		{
			double num = 0.0;
			if (!string.IsNullOrEmpty(value) && double.TryParse(value, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out num))
			{
				return num;
			}
			return defaultValue;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x000361B4 File Offset: 0x000343B4
		public static int ToInteger(this string value, int defaultValue = 0)
		{
			int num = 0;
			if (!string.IsNullOrEmpty(value) && int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
			{
				return num;
			}
			return defaultValue;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x000361E4 File Offset: 0x000343E4
		public static uint ToInteger(this string value, uint defaultValue = 0U)
		{
			uint num = 0U;
			if (!string.IsNullOrEmpty(value) && uint.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
			{
				return num;
			}
			return defaultValue;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00036214 File Offset: 0x00034414
		public static bool ToBoolean(this string value, bool defaultValue = false)
		{
			bool flag = false;
			if (!string.IsNullOrEmpty(value) && bool.TryParse(value, out flag))
			{
				return flag;
			}
			return defaultValue;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00036238 File Offset: 0x00034438
		public static string ReplaceFirst(this string text, string search, string replace)
		{
			int num = text.IndexOf(search);
			if (num < 0)
			{
				return text;
			}
			return text.Substring(0, num) + replace + text.Substring(num + search.Length);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00036270 File Offset: 0x00034470
		public static string CollapseAndStrip(this string str)
		{
			char[] array = new char[str.Length];
			bool flag = true;
			int num = 0;
			for (int i = 0; i < str.Length; i++)
			{
				if (str[i].IsSpaceCharacter())
				{
					if (!flag)
					{
						flag = true;
						array[num++] = ' ';
					}
				}
				else
				{
					flag = false;
					array[num++] = str[i];
				}
			}
			if (flag && num > 0)
			{
				num--;
			}
			return new string(array, 0, num);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x000362E0 File Offset: 0x000344E0
		public static string Collapse(this string str)
		{
			List<char> list = new List<char>();
			bool flag = false;
			for (int i = 0; i < str.Length; i++)
			{
				if (str[i].IsSpaceCharacter())
				{
					if (!flag)
					{
						list.Add(' ');
						flag = true;
					}
				}
				else
				{
					flag = false;
					list.Add(str[i]);
				}
			}
			return new string(list.ToArray());
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00036340 File Offset: 0x00034540
		public static bool Contains(this string[] list, string element, StringComparison comparison = StringComparison.Ordinal)
		{
			for (int i = 0; i < list.Length; i++)
			{
				if (list[i].Equals(element, comparison))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0003636A File Offset: 0x0003456A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is(this string current, string other)
		{
			return string.Equals(current, other, StringComparison.Ordinal);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00036374 File Offset: 0x00034574
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Isi(this string current, string other)
		{
			return string.Equals(current, other, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0003637E File Offset: 0x0003457E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsOneOf(this string element, string item1, string item2)
		{
			return element.Is(item1) || element.Is(item2);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00036392 File Offset: 0x00034592
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsOneOf(this string element, string item1, string item2, string item3)
		{
			return element.Is(item1) || element.Is(item2) || element.Is(item3);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x000363AF File Offset: 0x000345AF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsOneOf(this string element, string item1, string item2, string item3, string item4)
		{
			return element.Is(item1) || element.Is(item2) || element.Is(item3) || element.Is(item4);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x000363D6 File Offset: 0x000345D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsOneOf(this string element, string item1, string item2, string item3, string item4, string item5)
		{
			return element.Is(item1) || element.Is(item2) || element.Is(item3) || element.Is(item4) || element.Is(item5);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00036408 File Offset: 0x00034608
		public static string StripLineBreaks(this string str)
		{
			char[] array = str.ToCharArray();
			int num = 0;
			int num2 = array.Length;
			int i = 0;
			while (i < num2)
			{
				array[i] = array[i + num];
				if (array[i].IsLineBreak())
				{
					num++;
					num2--;
				}
				else
				{
					i++;
				}
			}
			return new string(array, 0, num2);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00036452 File Offset: 0x00034652
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string StripLeadingTrailingSpaces(this string str)
		{
			return str.ToCharArray().StripLeadingTrailingSpaces();
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00036460 File Offset: 0x00034660
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string StripLeadingTrailingSpaces(this char[] array)
		{
			int i = 0;
			int num = array.Length - 1;
			while (i < array.Length)
			{
				if (!array[i].IsSpaceCharacter())
				{
					break;
				}
				i++;
			}
			while (num > i && array[num].IsSpaceCharacter())
			{
				num--;
			}
			return new string(array, i, 1 + num - i);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x000364AB File Offset: 0x000346AB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string[] SplitWithoutTrimming(this string str, char c)
		{
			return str.ToCharArray().SplitWithoutTrimming(c);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x000364BC File Offset: 0x000346BC
		public static string[] SplitWithoutTrimming(this char[] chars, char c)
		{
			List<string> list = new List<string>();
			int num = 0;
			for (int i = 0; i < chars.Length; i++)
			{
				if (chars[i] == c)
				{
					if (i > num)
					{
						list.Add(new string(chars, num, i - num));
					}
					num = i + 1;
				}
			}
			if (chars.Length > num)
			{
				list.Add(new string(chars, num, chars.Length - num));
			}
			return list.ToArray();
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0003651B File Offset: 0x0003471B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string[] SplitCommas(this string str)
		{
			return str.SplitWithTrimming(',');
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00036525 File Offset: 0x00034725
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasHyphen(this string str, string value, StringComparison comparison = StringComparison.Ordinal)
		{
			return string.Equals(str, value, comparison) || (str.Length > value.Length && str.StartsWith(value, comparison) && str[value.Length] == '-');
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00036560 File Offset: 0x00034760
		public static string[] SplitSpaces(this string str)
		{
			List<string> list = new List<string>();
			List<char> list2 = new List<char>();
			char[] array = str.ToCharArray();
			for (int i = 0; i <= array.Length; i++)
			{
				if (i == array.Length || array[i].IsSpaceCharacter())
				{
					if (list2.Count > 0)
					{
						string text = list2.ToArray().StripLeadingTrailingSpaces();
						if (text.Length != 0)
						{
							list.Add(text);
						}
						list2.Clear();
					}
				}
				else
				{
					list2.Add(array[i]);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x000365DC File Offset: 0x000347DC
		public static string[] SplitWithTrimming(this string str, char c)
		{
			List<string> list = new List<string>();
			List<char> list2 = new List<char>();
			char[] array = str.ToCharArray();
			for (int i = 0; i <= array.Length; i++)
			{
				if (i == array.Length || array[i] == c)
				{
					if (list2.Count > 0)
					{
						string text = list2.ToArray().StripLeadingTrailingSpaces();
						if (text.Length != 0)
						{
							list.Add(text);
						}
						list2.Clear();
					}
				}
				else
				{
					list2.Add(array[i]);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00036654 File Offset: 0x00034854
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int FromHex(this string s)
		{
			return int.Parse(s, NumberStyles.HexNumber);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00036661 File Offset: 0x00034861
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int FromDec(this string s)
		{
			return int.Parse(s, NumberStyles.Integer);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0000D9F4 File Offset: 0x0000BBF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string HtmlEncode(this string value, Encoding encoding)
		{
			return value;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0003666C File Offset: 0x0003486C
		public static string CssString(this string value)
		{
			StringBuilder stringBuilder = Pool.NewStringBuilder();
			stringBuilder.Append('"');
			if (!string.IsNullOrEmpty(value))
			{
				for (int i = 0; i < value.Length; i++)
				{
					char c = value[i];
					if (c == '\0')
					{
						throw new DomException(DomError.InvalidCharacter);
					}
					if (c == '"' || c == '\\')
					{
						stringBuilder.Append('\\').Append(c);
					}
					else if (c.IsInRange(1, 31) || c == '{')
					{
						stringBuilder.Append('\\').Append(c.ToHex()).Append((i + 1 != value.Length) ? " " : "");
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
			}
			stringBuilder.Append('"');
			return stringBuilder.ToPool();
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0003672D File Offset: 0x0003492D
		public static string CssFunction(this string value, string argument)
		{
			return value + "(" + argument + ")";
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00036740 File Offset: 0x00034940
		public static string CssUrl(this string value)
		{
			string text = value.CssString();
			return FunctionNames.Url.CssFunction(text);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00036760 File Offset: 0x00034960
		public static string CssColor(this string value)
		{
			Color color = default(Color);
			if (Color.TryFromHex(value, out color))
			{
				return color.ToString(null, CultureInfo.InvariantCulture);
			}
			return value;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00036790 File Offset: 0x00034990
		public static string CssUnit(this string value, out float result)
		{
			if (!string.IsNullOrEmpty(value))
			{
				int num = value.Length;
				while (!value[num - 1].IsDigit() && --num > 0)
				{
				}
				if (num > 0 && float.TryParse(value.Substring(0, num), NumberStyles.Any, CultureInfo.InvariantCulture, out result))
				{
					return value.Substring(num);
				}
			}
			result = 0f;
			return null;
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x000367F4 File Offset: 0x000349F4
		public static string UrlEncode(this byte[] content)
		{
			StringBuilder stringBuilder = Pool.NewStringBuilder();
			for (int i = 0; i < content.Length; i++)
			{
				char c = (char)content[i];
				if (c == ' ')
				{
					stringBuilder.Append('+');
				}
				else if (c == '*' || c == '-' || c == '.' || c == '_' || c == '~' || c.IsAlphanumericAscii())
				{
					stringBuilder.Append(c);
				}
				else
				{
					stringBuilder.Append('%').Append(content[i].ToString("X2"));
				}
			}
			return stringBuilder.ToPool();
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0003687C File Offset: 0x00034A7C
		public static byte[] UrlDecode(this string value)
		{
			MemoryStream memoryStream = new MemoryStream();
			for (int i = 0; i < value.Length; i++)
			{
				char c = value[i];
				if (c == '+')
				{
					byte b = 32;
					memoryStream.WriteByte(b);
				}
				else if (c == '%')
				{
					if (i + 2 >= value.Length)
					{
						throw new FormatException();
					}
					byte b2 = (byte)(16 * value[++i].FromHex() + value[++i].FromHex());
					memoryStream.WriteByte(b2);
				}
				else
				{
					byte b3 = (byte)c;
					memoryStream.WriteByte(b3);
				}
			}
			return memoryStream.ToArray();
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00036914 File Offset: 0x00034B14
		public static string NormalizeLineEndings(this string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				StringBuilder stringBuilder = Pool.NewStringBuilder();
				bool flag = false;
				foreach (char c in value)
				{
					bool flag2 = c == '\n';
					if (flag && !flag2)
					{
						stringBuilder.Append('\n');
					}
					else if (!flag && flag2)
					{
						stringBuilder.Append('\r');
					}
					flag = c == '\r';
					stringBuilder.Append(c);
				}
				if (flag)
				{
					stringBuilder.Append('\n');
				}
				return stringBuilder.ToPool();
			}
			return value;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00036997 File Offset: 0x00034B97
		public static string ToEncodingType(this string encType)
		{
			if (!encType.Isi(MimeTypeNames.Plain) && !encType.Isi(MimeTypeNames.MultipartForm) && !encType.Isi(MimeTypeNames.ApplicationJson))
			{
				return MimeTypeNames.UrlencodedForm;
			}
			return encType;
		}
	}
}
