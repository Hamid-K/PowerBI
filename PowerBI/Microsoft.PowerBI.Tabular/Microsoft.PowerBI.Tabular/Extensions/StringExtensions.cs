using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.AnalysisServices.Hosting;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001D8 RID: 472
	internal static class StringExtensions
	{
		// Token: 0x06001C0E RID: 7182 RVA: 0x000C3C4C File Offset: 0x000C1E4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string ToJsonCase(this string csName)
		{
			Utils.Verify(csName.Length != 2 || csName != csName.ToUpper(CultureInfo.InvariantCulture), "Identifier '{0}' cannot be converted to Json case and then back to C# case", new KeyValuePair<InfoRestrictionType, object>[]
			{
				new KeyValuePair<InfoRestrictionType, object>(InfoRestrictionType.Unrestricted, csName)
			});
			if (csName == "KPI")
			{
				return "kpi";
			}
			if (csName == "IsAvailableInMDX")
			{
				return "isAvailableInMdx";
			}
			if (!(csName == "MemberID"))
			{
				return ConvertHelper.ConvertToCamelCase(csName);
			}
			return "memberId";
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x000C3CD8 File Offset: 0x000C1ED8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string ToCSharpCase(this string jsonName)
		{
			if (jsonName == "kpi")
			{
				return "KPI";
			}
			if (jsonName == "isAvailableInMdx")
			{
				return "IsAvailableInMDX";
			}
			if (!(jsonName == "memberId"))
			{
				return ConvertHelper.ConvertFromCamelCase(jsonName);
			}
			return "MemberID";
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x000C3D26 File Offset: 0x000C1F26
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasLineSeparators(this string s)
		{
			return s.IndexOfAny(StringExtensions.lineSeparatorChars) != -1;
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x000C3D39 File Offset: 0x000C1F39
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string[] SplitLines(this string s)
		{
			return s.Split(StringExtensions.lineSeparators, StringSplitOptions.None);
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x000C3D47 File Offset: 0x000C1F47
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string SubstringAndTrim(this string s, int startIndex, bool trimStart = true, bool trimEnd = true)
		{
			if (startIndex == s.Length)
			{
				return string.Empty;
			}
			return StringExtensions.SubstringAndTrimImpl(s, startIndex, s.Length - startIndex, trimStart, trimEnd);
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x000C3D69 File Offset: 0x000C1F69
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string SubstringAndTrim(this string s, int startIndex, int length, bool trimStart = true, bool trimEnd = true)
		{
			return StringExtensions.SubstringAndTrimImpl(s, startIndex, length, trimStart, trimEnd);
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x000C3D76 File Offset: 0x000C1F76
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string EscapeString(this string s, char quoteChar)
		{
			return s.EscapeString(quoteChar, 0, s.Length);
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x000C3D88 File Offset: 0x000C1F88
		public static string EscapeString(this string s, char quoteChar, int startIndex, int length)
		{
			int num = s.IndexOf(quoteChar, startIndex, length);
			if (num != -1)
			{
				int num2 = startIndex + length;
				StringBuilder stringBuilder = new StringBuilder(length + 10);
				do
				{
					stringBuilder.Append(s, startIndex, num - startIndex + 1);
					stringBuilder.Append(quoteChar);
					startIndex = num + 1;
					num = s.IndexOf(quoteChar, startIndex, num2 - startIndex);
				}
				while (num != -1);
				if (startIndex < num2)
				{
					stringBuilder.Append(s, startIndex, num2 - startIndex);
				}
				return stringBuilder.ToString();
			}
			if (startIndex != 0 || length != s.Length)
			{
				return s.Substring(startIndex, length);
			}
			return s;
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x000C3E09 File Offset: 0x000C2009
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string UnescapeString(this string s, char quoteChar)
		{
			return s.UnescapeString(quoteChar, 0, s.Length);
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x000C3E1C File Offset: 0x000C201C
		public static string UnescapeString(this string s, char quoteChar, int startIndex, int length)
		{
			int num = s.IndexOf(quoteChar, startIndex, length);
			if (num != -1)
			{
				int num2 = startIndex + length;
				StringBuilder stringBuilder = new StringBuilder(length);
				while (num != num2 - 1 && s[num + 1] == quoteChar)
				{
					stringBuilder.Append(s, startIndex, num - startIndex + 1);
					startIndex = num + 2;
					num = s.IndexOf(quoteChar, startIndex, num2 - startIndex);
					if (num == -1)
					{
						if (startIndex < num2)
						{
							stringBuilder.Append(s, startIndex, num2 - startIndex);
						}
						return stringBuilder.ToString();
					}
				}
				throw new ArgumentException(TomSR.Exception_InvalidEscapedString(s, new string(quoteChar, 1), num.ToString(CultureInfo.InvariantCulture)));
			}
			if (startIndex != 0 || length != s.Length)
			{
				return s.Substring(startIndex, length);
			}
			return s;
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x000C3EC4 File Offset: 0x000C20C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Join(this IEnumerable<string> collection, char separator)
		{
			ICollection<string> collection2 = collection as ICollection<string>;
			if (collection2 != null)
			{
				int count = collection2.Count;
				if (count == 0)
				{
					return string.Empty;
				}
				if (count == 1)
				{
					return collection.First<string>();
				}
			}
			return string.Join(new string(separator, 1), collection);
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x000C3F08 File Offset: 0x000C2108
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string JoinLines(this IEnumerable<string> collection, string separator = null)
		{
			ICollection<string> collection2 = collection as ICollection<string>;
			if (collection2 != null)
			{
				int count = collection2.Count;
				if (count == 0)
				{
					return string.Empty;
				}
				if (count == 1)
				{
					return collection.First<string>();
				}
			}
			if (string.IsNullOrEmpty(separator))
			{
				separator = MetadataFormattingOptions.GetCurrentEndOfLine();
			}
			return string.Join(separator, collection);
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x000C3F52 File Offset: 0x000C2152
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsEmpty(this IReadOnlyCollection<string> collection)
		{
			return collection.Count == 0 || (collection.Count == 1 && string.IsNullOrEmpty(collection.First<string>()));
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x000C3F74 File Offset: 0x000C2174
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Join(this IReadOnlyCollection<string> collection, char separator)
		{
			int count = collection.Count;
			if (count == 0)
			{
				return string.Empty;
			}
			if (count != 1)
			{
				return string.Join(new string(separator, 1), collection);
			}
			return collection.First<string>();
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x000C3FAC File Offset: 0x000C21AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string JoinLines(this IReadOnlyCollection<string> collection, string separator = null)
		{
			int count = collection.Count;
			if (count == 0)
			{
				return string.Empty;
			}
			if (count != 1)
			{
				if (string.IsNullOrEmpty(separator))
				{
					separator = MetadataFormattingOptions.GetCurrentEndOfLine();
				}
				return string.Join(separator, collection);
			}
			return collection.First<string>();
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x000C3FEC File Offset: 0x000C21EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsEmpty(this string[] array)
		{
			return array.Length == 0 || (array.Length == 1 && string.IsNullOrEmpty(array[0]));
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x000C4004 File Offset: 0x000C2204
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Join(this string[] array, char separator)
		{
			int num = array.Length;
			if (num == 0)
			{
				return string.Empty;
			}
			if (num != 1)
			{
				return string.Join(new string(separator, 1), array);
			}
			return array[0];
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x000C4038 File Offset: 0x000C2238
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string JoinLines(this string[] array, string separator = null)
		{
			int num = array.Length;
			if (num == 0)
			{
				return string.Empty;
			}
			if (num != 1)
			{
				if (string.IsNullOrEmpty(separator))
				{
					separator = MetadataFormattingOptions.GetCurrentEndOfLine();
				}
				return string.Join(separator, array);
			}
			return array[0];
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x000C4074 File Offset: 0x000C2274
		private static string SubstringAndTrimImpl(string s, int startIndex, int length, bool trimStart, bool trimEnd)
		{
			int num = startIndex + length;
			if (trimStart)
			{
				while (startIndex < num && char.IsWhiteSpace(s, startIndex))
				{
					startIndex++;
				}
			}
			if (trimEnd)
			{
				while (num > startIndex && char.IsWhiteSpace(s, num - 1))
				{
					num--;
				}
			}
			if (startIndex >= num)
			{
				return string.Empty;
			}
			return s.Substring(startIndex, num - startIndex);
		}

		// Token: 0x04000549 RID: 1353
		private static readonly char[] lineSeparatorChars = new char[] { '\n', '\r' };

		// Token: 0x0400054A RID: 1354
		private static readonly string[] lineSeparators = new string[] { "\r\n", "\n\r", "\n", "\r" };
	}
}
