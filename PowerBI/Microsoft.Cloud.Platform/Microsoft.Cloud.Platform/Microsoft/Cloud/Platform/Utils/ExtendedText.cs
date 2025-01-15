using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200029F RID: 671
	public static class ExtendedText
	{
		// Token: 0x06001227 RID: 4647 RVA: 0x0003F744 File Offset: 0x0003D944
		public static string[] Split([NotNull] string input, [NotNull] string delimiters)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(input, "input");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(delimiters, "delimiters");
			List<string> list = new List<string>();
			string text = "([\"'])(?:\\\\?.)*?\\1|([^" + delimiters + "]+)";
			foreach (object obj in Regex.Matches(input, text, RegexOptions.Compiled))
			{
				Match match = (Match)obj;
				string value = match.Groups[0].Value;
				string value2 = match.Groups[2].Value;
				if (value2 != null && value2.Length > 0)
				{
					list.Add(value2);
				}
				else
				{
					list.Add(value.Substring(1, value.Length - 2));
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x0003F820 File Offset: 0x0003DA20
		public static string SplitFirst(string what, string delimiter, out string remaining)
		{
			int num = what.IndexOf(delimiter, StringComparison.Ordinal);
			if (num < 0)
			{
				remaining = null;
				return what;
			}
			string text = what.Substring(0, num);
			remaining = what.Substring(num + delimiter.Length);
			return text;
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x0003F858 File Offset: 0x0003DA58
		public static IList<string> SplitByIndices(this string input, params int[] indices)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<int[]>(indices, "indices");
			List<string> list = new List<string>(indices.Length);
			int num = -1;
			for (int i = 0; i < indices.Length; i++)
			{
				string text = null;
				if (num == -1)
				{
					num = i;
				}
				else
				{
					text = input.Substring(indices[num], indices[i] - indices[num]);
					num = i;
				}
				if (text != null)
				{
					list.Add(text);
				}
				if (i == indices.Length - 1)
				{
					list.Add(input.Substring(indices[i]));
				}
			}
			return list;
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0003F8C8 File Offset: 0x0003DAC8
		public static string FirstLine(this string what)
		{
			if (what == null)
			{
				return what;
			}
			return what.Substring(0, what.IndexOfAny(ExtendedText.c_newline));
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0003F8E4 File Offset: 0x0003DAE4
		public static string FitTextIntoLimitedBox(string text, int width, int height)
		{
			ExtendedDiagnostics.EnsureArgumentIsBetween(width, "...".Length, int.MaxValue, "width");
			ExtendedDiagnostics.EnsureArgumentIsPositive(height, "height");
			int num = 0;
			int num2 = 0;
			StringBuilder stringBuilder = new StringBuilder((width + 2) * height);
			foreach (char c in text)
			{
				if (c == '\n')
				{
					num2++;
					if (num2 == height)
					{
						return stringBuilder.ToString();
					}
					stringBuilder.Append(Environment.NewLine);
					num = 0;
				}
				else if (c != '\r' && c != '\t' && c != '\v' && c != '\f' && c != '\u0085')
				{
					if (num2 == height - 1)
					{
						if (num >= width - "...".Length)
						{
							stringBuilder.Append("...");
							return stringBuilder.ToString();
						}
						stringBuilder.Append(c);
						num++;
					}
					else
					{
						stringBuilder.Append(c);
						num++;
						if (num == width)
						{
							stringBuilder.Append(Environment.NewLine);
							stringBuilder.Append("  ");
							num = "  ".Length;
							num2++;
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x0003FA10 File Offset: 0x0003DC10
		public static string ChopTextByLength(string message, int length)
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive(length, "length");
			if (message.Length <= length)
			{
				return message;
			}
			bool flag = true;
			int num = length - "...".Length;
			if ("...".Length >= length)
			{
				flag = false;
				num = length;
			}
			StringBuilder stringBuilder = new StringBuilder(message.Substring(0, num));
			if (flag)
			{
				stringBuilder.Append("...");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0003FA76 File Offset: 0x0003DC76
		public static string Chop(this string s, int maxLength)
		{
			return ExtendedText.ChopTextByLength(s, maxLength);
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0003FA7F File Offset: 0x0003DC7F
		public static string CalculateHash(string s)
		{
			return BitConverter.ToString(ExtendedText.GetChecksum(s)).Replace("-", "");
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x0003FA9C File Offset: 0x0003DC9C
		public static byte[] GetChecksum(string s)
		{
			byte[] array;
			using (MD5 md = new MD5CryptoServiceProvider())
			{
				array = md.ComputeHash(Encoding.UTF8.GetBytes(s));
			}
			return array;
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0003FAE0 File Offset: 0x0003DCE0
		public static IEnumerable<string> SplitTextByLength(string message, int length)
		{
			List<string> list = new List<string>();
			string text = message;
			while (text.Length > length)
			{
				string text2 = ExtendedText.ChopTextByLength(text, length);
				list.Add(text2);
				int num = (text2.EndsWith("...", StringComparison.Ordinal) ? (length - "...".Length) : length);
				text = text.Substring(num);
			}
			if (text.Length > 0)
			{
				list.Add(text);
			}
			return list;
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x0003FB46 File Offset: 0x0003DD46
		public static IEnumerable<string> SplitByLength([NotNull] this string input, int length)
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive(length, "length");
			for (int i = 0; i < input.Length; i += length)
			{
				yield return input.Substring(i, Math.Min(length, input.Length - i));
			}
			yield break;
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0003FB5D File Offset: 0x0003DD5D
		public static string StringJoin<T>(this IEnumerable<T> sequence, string separator, Func<T, string> converter)
		{
			return string.Join(separator, sequence.Select(converter));
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x0003FB6C File Offset: 0x0003DD6C
		public static string StringJoin<T>(this IEnumerable<T> sequence, string separator)
		{
			return sequence.StringJoin(separator, delegate(T item)
			{
				if (item == null)
				{
					return null;
				}
				return item.ToString();
			});
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0003FB94 File Offset: 0x0003DD94
		public static string Pack(IEnumerable<string> parts, char delimiter, char quote)
		{
			string d = new string(delimiter, 1);
			string q = new string(quote, 1);
			string qq = new string(quote, 2);
			string qd = q + d;
			return parts.StringJoin(d, (string p) => p.Replace(q, qq).Replace(d, qd));
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0003FC00 File Offset: 0x0003DE00
		public static IEnumerable<string> Unpack(string s, char delimiter, char quote)
		{
			List<string> list = new List<string>();
			string d = new string(delimiter, 1);
			string q = new string(quote, 1);
			string qq = new string(quote, 2);
			string qd = q + d;
			char c = ((quote == 'a') ? 'b' : 'a');
			int num = 0;
			for (int i = 0; i < s.Length; i++)
			{
				char c2 = ((i > 0) ? s[i - 1] : c);
				if (s[i] == delimiter && c2 != quote)
				{
					list.Add(s.Substring(num, i - num));
					num = i + 1;
				}
			}
			list.Add(s.Substring(num, s.Length - num));
			return list.Select((string p) => p.Replace(qd, d).Replace(qq, q));
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x0003FCDC File Offset: 0x0003DEDC
		public static string Shorten(string s, string delimiter, int limit)
		{
			if (s.Length <= limit)
			{
				return s;
			}
			ExtendedText.Part[] array = (from p in s.Split(new string[] { delimiter }, StringSplitOptions.None).Select((string p, int i) => new ExtendedText.Part
				{
					Text = p,
					Length = p.Length,
					Index = i
				})
				orderby p.Length descending
				select p).ToArray<ExtendedText.Part>();
			ExtendedDiagnostics.EnsureOperation(array.None((ExtendedText.Part p) => p.Length == 0), "empty parts are not allowed");
			limit -= (array.Length - 1) * delimiter.Length;
			int num = limit / array.Length;
			if (num == 0)
			{
				throw new InvalidOperationException("String cannot be shortened");
			}
			int k = array.Sum((ExtendedText.Part p) => p.Length);
			while (k > limit)
			{
				foreach (ExtendedText.Part part in array)
				{
					if (k <= limit)
					{
						break;
					}
					if (part.Length > num)
					{
						part.Length--;
						k--;
					}
				}
			}
			return array.OrderBy((ExtendedText.Part p) => p.Index).StringJoin(delimiter, (ExtendedText.Part p) => p.Text.Substring(0, p.Length));
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x0003FE57 File Offset: 0x0003E057
		[StringFormatMethod("format")]
		public static string FormatWithCurrentCulture([NotNull] this string format, params object[] args)
		{
			return format.FormatWith(CultureInfo.CurrentCulture, args);
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x0003FE65 File Offset: 0x0003E065
		[StringFormatMethod("format")]
		public static string FormatWithInvariantCulture([NotNull] this string format, params object[] args)
		{
			return format.FormatWith(CultureInfo.InvariantCulture, args);
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0003FE73 File Offset: 0x0003E073
		[StringFormatMethod("format")]
		public static string FormatWith([NotNull] this string format, IFormatProvider formatProvider, params object[] args)
		{
			return string.Format(formatProvider, format, args);
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x0003FE7D File Offset: 0x0003E07D
		public static int ParseInt32WithInvariantCulture(this string value)
		{
			return value.ParseInt32With(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x0003FE8A File Offset: 0x0003E08A
		public static int ParseInt32With(this string value, IFormatProvider formatProvider)
		{
			return int.Parse(value, formatProvider);
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00037439 File Offset: 0x00035639
		public static string ByteArrayToStringHex(byte[] buffer)
		{
			return BitConverter.ToString(buffer).Replace("-", string.Empty);
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00037450 File Offset: 0x00035650
		public static string ByteArrayToStringHex(byte[] buffer, int startIndex, int length)
		{
			return BitConverter.ToString(buffer, startIndex, length).Replace("-", string.Empty);
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x0003FE94 File Offset: 0x0003E094
		public static byte[] ByteArrayFromStringHex([NotNull] string s)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(s, "s");
			ExtendedDiagnostics.EnsureArgument("s", s.Length % 2 == 0, "|s| must be an even number");
			byte[] array = new byte[s.Length / 2];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Convert.ToByte(s.Substring(i * 2, 2), 16);
			}
			return array;
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0003FEF8 File Offset: 0x0003E0F8
		public static string RemoveCharacters(this string source, char[] toRemove)
		{
			StringBuilder stringBuilder = new StringBuilder(source.Length);
			for (int i = 0; i < source.Length; i++)
			{
				if (!toRemove.Contains(source[i]))
				{
					stringBuilder.Append(source[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0003FF48 File Offset: 0x0003E148
		public static string RemovePrefixes(this string source, IEnumerable<string> prefixesToRemove, StringComparison stringComparison)
		{
			string text = prefixesToRemove.OrderByDescending((string prfx) => prfx.Length).FirstOrDefault((string prfx) => source.StartsWith(prfx, stringComparison));
			if (text == null)
			{
				return source;
			}
			return source.Remove(0, text.Length);
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0003FFBC File Offset: 0x0003E1BC
		public static string RemovePostfixes(this string source, IEnumerable<string> postfixesToRemove, StringComparison stringComparison)
		{
			string text = postfixesToRemove.OrderByDescending((string pstfx) => pstfx.Length).FirstOrDefault((string pstfx) => source.EndsWith(pstfx, stringComparison));
			if (text == null)
			{
				return source;
			}
			return source.Substring(0, source.Length - text.Length);
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x0004003C File Offset: 0x0003E23C
		public static bool IsLiterallyEqual(this string source, string second, StringComparison stringComparison)
		{
			return (string.IsNullOrWhiteSpace(source) && string.IsNullOrWhiteSpace(second)) || (!string.IsNullOrWhiteSpace(source) && !string.IsNullOrWhiteSpace(second) && string.Equals(source.Trim(), second.Trim(), stringComparison));
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00040079 File Offset: 0x0003E279
		public static bool IsLiterallyEqual(this string source, string second)
		{
			return source.IsLiterallyEqual(second, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00040083 File Offset: 0x0003E283
		public static string GetAlphanumericOnlyVersion(this string source)
		{
			return Regex.Replace(source, "[^A-Za-z0-9]+", "");
		}

		// Token: 0x040006C6 RID: 1734
		public const char c_nullChar = '\0';

		// Token: 0x040006C7 RID: 1735
		public const char c_carriageReturnChar = '\r';

		// Token: 0x040006C8 RID: 1736
		public const char c_newlineChar = '\n';

		// Token: 0x040006C9 RID: 1737
		private static readonly char[] c_newline = new char[] { '\r', '\n' };

		// Token: 0x040006CA RID: 1738
		private const string c_postfix = "...";

		// Token: 0x040006CB RID: 1739
		private const string c_wrap = "  ";

		// Token: 0x040006CC RID: 1740
		private const string c_alphaNumRegex = "[^A-Za-z0-9]+";

		// Token: 0x0200076B RID: 1899
		private class Part
		{
			// Token: 0x040015F5 RID: 5621
			public string Text;

			// Token: 0x040015F6 RID: 5622
			public int Length;

			// Token: 0x040015F7 RID: 5623
			public int Index;
		}
	}
}
