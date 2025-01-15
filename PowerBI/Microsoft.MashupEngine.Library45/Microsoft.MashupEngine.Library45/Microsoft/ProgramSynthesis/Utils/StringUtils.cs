using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000513 RID: 1299
	public static class StringUtils
	{
		// Token: 0x06001CDA RID: 7386 RVA: 0x00055EB4 File Offset: 0x000540B4
		public static string Slice(this string str, int? start = null, int? end = null, int step = 1)
		{
			if (step == 0)
			{
				throw new ArgumentException("Step size cannot be zero.", "step");
			}
			int? num;
			if (start == null)
			{
				start = new int?((step > 0) ? 0 : (str.Length - 1));
			}
			else
			{
				num = start;
				int num2 = 0;
				if ((num.GetValueOrDefault() < num2) & (num != null))
				{
					num = start;
					num2 = -str.Length;
					start = (((num.GetValueOrDefault() < num2) & (num != null)) ? new int?(0) : (str.Length + start));
				}
				else
				{
					num = start;
					num2 = str.Length;
					if ((num.GetValueOrDefault() > num2) & (num != null))
					{
						start = new int?(str.Length);
					}
				}
			}
			if (end == null)
			{
				end = new int?((step > 0) ? str.Length : (-1));
			}
			else
			{
				num = end;
				int num2 = 0;
				if ((num.GetValueOrDefault() < num2) & (num != null))
				{
					num = end;
					num2 = -str.Length;
					end = (((num.GetValueOrDefault() < num2) & (num != null)) ? new int?(0) : (str.Length + end));
				}
				else
				{
					num = end;
					num2 = str.Length;
					if ((num.GetValueOrDefault() > num2) & (num != null))
					{
						end = new int?(str.Length);
					}
				}
			}
			num = start;
			int? num3 = end;
			if (!((num.GetValueOrDefault() == num3.GetValueOrDefault()) & (num != null == (num3 != null))))
			{
				num3 = start;
				num = end;
				if (!((num3.GetValueOrDefault() < num.GetValueOrDefault()) & ((num3 != null) & (num != null))) || step >= 0)
				{
					num = start;
					num3 = end;
					if (!((num.GetValueOrDefault() > num3.GetValueOrDefault()) & ((num != null) & (num3 != null))) || step <= 0)
					{
						num3 = start;
						num = end;
						if (((num3.GetValueOrDefault() < num.GetValueOrDefault()) & ((num3 != null) & (num != null))) && step == 1)
						{
							return str.Substring(start.Value, end.Value - start.Value);
						}
						StringBuilder stringBuilder = new StringBuilder((int)Math.Ceiling((double)((float)(end - start).Value / (float)step)));
						int num4 = start.Value;
						for (;;)
						{
							if (step <= 0)
							{
								goto IL_02D9;
							}
							int num5 = num4;
							num = end;
							if (!((num5 < num.GetValueOrDefault()) & (num != null)))
							{
								goto IL_02D9;
							}
							IL_02A9:
							stringBuilder.Append(str[num4]);
							num4 += step;
							continue;
							IL_02D9:
							if (step >= 0)
							{
								break;
							}
							int num6 = num4;
							num = end;
							if (!((num6 > num.GetValueOrDefault()) & (num != null)))
							{
								break;
							}
							goto IL_02A9;
						}
						return stringBuilder.ToString();
					}
				}
			}
			return "";
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x000561BB File Offset: 0x000543BB
		public static string Capitalize(this string s)
		{
			return s.Substring(0, 1).ToUpperInvariant() + s.Substring(1).ToLowerInvariant();
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x000561DB File Offset: 0x000543DB
		public static int Right(this Match m)
		{
			return m.Index + m.Length;
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x000561EC File Offset: 0x000543EC
		public static IEnumerable<string> LongestCommonSubstrings(IEnumerable<string> strings, int minLength = 2, bool splitLines = true, int maxLength = 10)
		{
			string[] array = (strings as string[]) ?? strings.ToArray<string>();
			if (array.Count((string x) => x.Length < minLength) > 0)
			{
				return new HashSet<string>();
			}
			if (array.Length == 1)
			{
				return array;
			}
			HashSet<string> hashSet = null;
			foreach (string text in array)
			{
				List<string> list = new List<string>();
				string[] array3;
				if (!splitLines)
				{
					(array3 = new string[1])[0] = text;
				}
				else
				{
					array3 = text.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
				}
				foreach (string text2 in array3)
				{
					for (int k = 0; k < text2.Length - 1; k++)
					{
						for (int l = minLength; l <= Math.Min(text2.Length - k, maxLength); l++)
						{
							list.Add(text2.Substring(k, l));
						}
					}
				}
				if (hashSet == null)
				{
					hashSet = new HashSet<string>(list);
				}
				else
				{
					hashSet.IntersectWith(list);
				}
				if (!hashSet.IsAny<string>())
				{
					return hashSet;
				}
			}
			return hashSet.MaxBy((string x) => x.Length);
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x00056340 File Offset: 0x00054540
		public static string ReverseUnicodeString(this string s)
		{
			List<object> list = new List<object>();
			TextElementEnumerator textElementEnumerator = StringInfo.GetTextElementEnumerator(s);
			while (textElementEnumerator.MoveNext())
			{
				object obj = textElementEnumerator.Current;
				list.Add(obj);
			}
			list.Reverse();
			return string.Join<object>("", list);
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x00056381 File Offset: 0x00054581
		public static bool SubstringEquals(this string str, string needle, int start)
		{
			return str.IndexOf(needle, start, needle.Length, StringComparison.Ordinal) >= 0;
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x00056398 File Offset: 0x00054598
		public static string AddSuffix(this string str0, string str1)
		{
			return str0 + str1;
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x000563A1 File Offset: 0x000545A1
		public static string AddPrefix(this string str0, string str1)
		{
			return str1 + str0;
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x000563AA File Offset: 0x000545AA
		public static IEnumerable<string> SplitIntoLines(this string str)
		{
			using (StringReader reader = new StringReader(str))
			{
				while (reader.Peek() != -1)
				{
					yield return reader.ReadLine();
				}
			}
			StringReader reader = null;
			yield break;
			yield break;
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x000563BC File Offset: 0x000545BC
		public static string BuildLimitedListLiteral<T>(IEnumerable<T> xs, int elementLimit = 5)
		{
			IReadOnlyList<T> readOnlyList = (xs as IReadOnlyList<T>) ?? xs.ToList<T>();
			string text;
			if (readOnlyList.Count > elementLimit)
			{
				text = string.Join<T>(", ", readOnlyList.Take(elementLimit)) + string.Format(" ... and {0} more ", readOnlyList.Count - elementLimit);
			}
			else
			{
				text = string.Join<T>(", ", readOnlyList);
			}
			return "[ " + text + " ]";
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x0005642F File Offset: 0x0005462F
		public static Optional<char> MaybeLastChar(this string str)
		{
			if (str.Length <= 0)
			{
				return Optional<char>.Nothing;
			}
			return str[str.Length - 1].Some<char>();
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x00056453 File Offset: 0x00054653
		public static string NormalizeNewlines(this string str)
		{
			return str.Replace("\r\n", "\n");
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x00056465 File Offset: 0x00054665
		public static IEnumerable<int> AllIndexesOf(this string str, string substring, StringComparison comparison = StringComparison.Ordinal)
		{
			for (int index = str.IndexOf(substring, comparison); index >= 0; index = str.IndexOf(substring, index + 1, comparison))
			{
				yield return index;
				if (index + substring.Length >= str.Length)
				{
					yield break;
				}
			}
			yield break;
		}
	}
}
