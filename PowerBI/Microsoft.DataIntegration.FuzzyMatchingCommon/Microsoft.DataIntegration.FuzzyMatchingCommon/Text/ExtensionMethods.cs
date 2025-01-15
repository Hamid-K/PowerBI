using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Text
{
	// Token: 0x02000027 RID: 39
	public static class ExtensionMethods
	{
		// Token: 0x060000DC RID: 220 RVA: 0x0000FB69 File Offset: 0x0000DD69
		public static IEnumerable<int> IndexesOf(this string str, char delimiter)
		{
			int num;
			for (int startIndex = 0; startIndex < str.Length; startIndex = num + 1)
			{
				startIndex = str.IndexOf(delimiter, startIndex);
				if (startIndex < 0)
				{
					break;
				}
				yield return startIndex;
				num = startIndex;
			}
			yield break;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000FB80 File Offset: 0x0000DD80
		public static IEnumerable<int> IndexesOf(this string str, string delimiter)
		{
			int num;
			for (int startIndex = 0; startIndex < str.Length; startIndex = num + 1)
			{
				startIndex = str.IndexOf(delimiter, startIndex);
				if (startIndex < 0)
				{
					break;
				}
				yield return startIndex;
				num = startIndex;
			}
			yield break;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000FB97 File Offset: 0x0000DD97
		public static IEnumerable<string> SplitEnum(this string str, string delimiter = "\r\n", StringSplitOptions splitOptions = 0)
		{
			if (delimiter == null)
			{
				yield return str;
				yield break;
			}
			int delimIndex;
			for (int i = 0; i < str.Length; i = delimIndex + delimiter.Length)
			{
				delimIndex = str.IndexOf(delimiter, i);
				if (delimIndex > i)
				{
					yield return str.Substring(i, delimIndex - i);
				}
				else
				{
					if (delimIndex == -1)
					{
						if (i < str.Length)
						{
							yield return str.Substring(i, str.Length - i);
						}
						yield break;
					}
					if (splitOptions == null)
					{
						yield return string.Empty;
					}
				}
			}
			yield break;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000FBB5 File Offset: 0x0000DDB5
		public static string[] Split(this string str, string delimiter)
		{
			return Enumerable.ToArray<string>(str.SplitEnum(delimiter, 0));
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000FBC4 File Offset: 0x0000DDC4
		public static string Aggregate(this IEnumerable<string> strings)
		{
			return Enumerable.Aggregate<string, StringBuilder, string>(strings, new StringBuilder(), (StringBuilder sb, string s) => sb.Append(s), (StringBuilder sb) => sb.ToString());
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000FC1C File Offset: 0x0000DE1C
		public static string Aggregate(this IEnumerable<string> strings, string delimiter)
		{
			return Enumerable.Aggregate<string, StringBuilder, string>(strings, new StringBuilder(), delegate(StringBuilder sb, string s)
			{
				if (sb.Length > 0)
				{
					sb.Append(delimiter);
				}
				return sb.Append(s);
			}, (StringBuilder sb) => sb.ToString());
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000FC6C File Offset: 0x0000DE6C
		public static string ToSeparatedValues(this IEnumerable<string> strings, string delimiter)
		{
			return strings.Aggregate(delimiter);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000FC75 File Offset: 0x0000DE75
		public static string ToCsv(this IEnumerable<string> strings, string delimiter = ", ")
		{
			return strings.Aggregate(delimiter);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000FC80 File Offset: 0x0000DE80
		public static bool EqualsAny(this string str, params string[] strings)
		{
			return Enumerable.Any<string>(strings, (string s) => s.Equals(str));
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000FCAC File Offset: 0x0000DEAC
		public static bool EqualsAny(this string str, StringComparison comparisonType = 2, params string[] strings)
		{
			return Enumerable.Any<string>(strings, (string s) => s.Equals(str, comparisonType));
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000FCE0 File Offset: 0x0000DEE0
		public static byte[] ToByteArray(this string str)
		{
			byte[] array = new byte[str.Length * 2];
			Buffer.BlockCopy(str.ToCharArray(), 0, array, 0, array.Length);
			return array;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000FD10 File Offset: 0x0000DF10
		public static string ToString(this byte[] bytes)
		{
			char[] array = new char[bytes.Length / 2];
			Buffer.BlockCopy(bytes, 0, array, 0, bytes.Length);
			return new string(array);
		}
	}
}
