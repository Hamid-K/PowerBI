using System;
using System.Collections.Generic;
using System.Text;

namespace NLog.Internal
{
	// Token: 0x02000147 RID: 327
	internal static class StringSplitter
	{
		// Token: 0x06000FC9 RID: 4041 RVA: 0x00028A1E File Offset: 0x00026C1E
		public static IEnumerable<string> SplitWithSelfEscape(this string text, char splitChar)
		{
			return StringSplitter.SplitWithSelfEscape2(text, splitChar);
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x00028A27 File Offset: 0x00026C27
		public static IEnumerable<string> SplitWithEscape(this string text, char splitChar, char escapeChar)
		{
			if (splitChar == escapeChar)
			{
				return StringSplitter.SplitWithSelfEscape2(text, splitChar);
			}
			return StringSplitter.SplitWithEscape2(text, splitChar, escapeChar);
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x00028A3D File Offset: 0x00026C3D
		private static IEnumerable<string> SplitWithEscape2(string text, char splitChar, char escapeChar)
		{
			if (!string.IsNullOrEmpty(text))
			{
				bool prevWasEscape = false;
				StringBuilder sb = new StringBuilder();
				int num;
				for (int i = 0; i < text.Length; i = num + 1)
				{
					char c = text[i];
					bool flag = c == splitChar;
					if (prevWasEscape)
					{
						if (flag)
						{
							if (sb.Length > 0)
							{
								StringBuilder stringBuilder = sb;
								num = stringBuilder.Length;
								stringBuilder.Length = num - 1;
							}
							sb.Append(c);
							prevWasEscape = false;
						}
						else
						{
							sb.Append(c);
							prevWasEscape = c == escapeChar;
						}
					}
					else if (flag)
					{
						string text2 = sb.ToString();
						sb.Length = 0;
						yield return text2;
						if (text.Length - 1 == i)
						{
							yield return string.Empty;
							break;
						}
					}
					else
					{
						sb.Append(c);
						prevWasEscape = c == escapeChar;
					}
					num = i;
				}
				string lastPart = StringSplitter.GetLastPart(sb);
				if (lastPart != null)
				{
					yield return lastPart;
				}
				sb = null;
			}
			yield break;
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x00028A5B File Offset: 0x00026C5B
		private static IEnumerable<string> SplitWithSelfEscape2(string text, char splitAndEscapeChar)
		{
			if (!string.IsNullOrEmpty(text))
			{
				bool prevWasEscape = false;
				StringBuilder sb = new StringBuilder();
				int num;
				for (int i = 0; i < text.Length; i = num + 1)
				{
					char c = text[i];
					bool flag = c == splitAndEscapeChar;
					bool flag2 = i == text.Length - 1;
					if (prevWasEscape)
					{
						if (flag)
						{
							prevWasEscape = false;
						}
						else
						{
							StringBuilder stringBuilder = sb;
							num = stringBuilder.Length;
							stringBuilder.Length = num - 1;
							string text2 = sb.ToString();
							sb.Length = 0;
							prevWasEscape = false;
							sb.Append(c);
							yield return text2;
						}
					}
					else if (flag2 && flag)
					{
						string text3 = sb.ToString();
						sb.Length = 0;
						yield return text3;
						yield return string.Empty;
					}
					else
					{
						sb.Append(c);
						if (flag)
						{
							prevWasEscape = true;
						}
					}
					num = i;
				}
				string lastPart = StringSplitter.GetLastPart(sb);
				if (lastPart != null)
				{
					yield return lastPart;
				}
				sb = null;
			}
			yield break;
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x00028A74 File Offset: 0x00026C74
		public static IEnumerable<string> SplitQuoted(this string text, char splitChar, char quoteChar, char escapeChar)
		{
			if (string.IsNullOrEmpty(text))
			{
				return new List<string>();
			}
			if (splitChar == quoteChar)
			{
				throw new NotSupportedException("Quote character should different from split character");
			}
			if (splitChar == escapeChar)
			{
				throw new NotSupportedException("Escape character should different from split character");
			}
			if (quoteChar == escapeChar)
			{
				return StringSplitter.SplitSelfQuoted2(text, splitChar, quoteChar);
			}
			return StringSplitter.SplitQuoted2(text, splitChar, quoteChar, escapeChar);
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00028AC3 File Offset: 0x00026CC3
		private static IEnumerable<string> SplitSelfQuoted2(string text, char splitChar, char quoteAndEscapeChar)
		{
			bool inQuotedMode = false;
			StringBuilder sb = new StringBuilder();
			bool isNewPart = true;
			int num;
			for (int i = 0; i < text.Length; i = num + 1)
			{
				char c = text[i];
				bool flag = c == splitChar;
				bool flag2 = c == quoteAndEscapeChar;
				bool isLastChar = i == text.Length - 1;
				if (isNewPart)
				{
					isNewPart = false;
					flag2 = c == quoteAndEscapeChar;
					if (flag2)
					{
						if (isLastChar)
						{
							sb.Append(c);
							break;
						}
						num = i;
						i = num + 1;
						c = text[i];
						if (c == quoteAndEscapeChar)
						{
							sb.Append(quoteAndEscapeChar);
						}
						else
						{
							sb.Append(c);
							inQuotedMode = true;
						}
					}
					else if (flag)
					{
						string text2 = sb.ToString();
						sb.Length = 0;
						yield return text2;
						if (isLastChar)
						{
							yield return string.Empty;
							break;
						}
						isNewPart = true;
					}
					else
					{
						sb.Append(c);
					}
				}
				else if (inQuotedMode)
				{
					if (flag2)
					{
						num = i;
						i = num + 1;
						inQuotedMode = false;
						string text3 = sb.ToString();
						sb.Length = 0;
						yield return text3;
					}
					else
					{
						sb.Append(c);
					}
				}
				else if (flag)
				{
					string text4 = sb.ToString();
					sb.Length = 0;
					yield return text4;
					if (isLastChar)
					{
						yield return string.Empty;
						break;
					}
					isNewPart = true;
				}
				else
				{
					sb.Append(c);
				}
				num = i;
			}
			string text5 = StringSplitter.GetLastPart(sb);
			if (inQuotedMode)
			{
				text5 = quoteAndEscapeChar.ToString() + text5;
			}
			if (text5 != null)
			{
				yield return text5;
			}
			yield break;
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x00028AE1 File Offset: 0x00026CE1
		private static IEnumerable<string> SplitQuoted2(string text, char splitChar, char quoteChar, char escapeChar)
		{
			bool inQuotedMode = false;
			StringBuilder sb = new StringBuilder();
			bool isNewPart = true;
			bool prevIsEscape = false;
			int num;
			for (int i = 0; i < text.Length; i = num + 1)
			{
				char c = text[i];
				bool flag = c == splitChar;
				bool flag2 = c == quoteChar;
				bool flag3 = c == escapeChar;
				bool isLastChar = i == text.Length - 1;
				if (isNewPart)
				{
					isNewPart = false;
					flag2 = c == quoteChar;
					flag3 = c == escapeChar;
					if (flag3)
					{
						if (isLastChar)
						{
							sb.Append(c);
							break;
						}
						num = i;
						i = num + 1;
						c = text[i];
						if (c == quoteChar)
						{
							sb.Append(quoteChar);
						}
						else
						{
							sb.Append(escapeChar);
							sb.Append(c);
						}
					}
					else if (flag)
					{
						string text2 = sb.ToString();
						sb.Length = 0;
						yield return text2;
						if (isLastChar)
						{
							yield return string.Empty;
							break;
						}
						isNewPart = true;
					}
					else if (flag2)
					{
						if (sb.Length > 0)
						{
							StringBuilder stringBuilder = sb;
							num = stringBuilder.Length;
							stringBuilder.Length = num - 1;
						}
						inQuotedMode = true;
					}
					else
					{
						sb.Append(c);
					}
				}
				else if (inQuotedMode)
				{
					if (flag2)
					{
						if (prevIsEscape)
						{
							if (sb.Length > 0)
							{
								StringBuilder stringBuilder2 = sb;
								num = stringBuilder2.Length;
								stringBuilder2.Length = num - 1;
							}
							sb.Append(c);
							break;
						}
						num = i;
						i = num + 1;
						inQuotedMode = false;
						string text3 = sb.ToString();
						sb.Length = 0;
						yield return text3;
					}
					else
					{
						prevIsEscape = flag3;
						sb.Append(c);
					}
				}
				else if (flag)
				{
					string text4 = sb.ToString();
					sb.Length = 0;
					yield return text4;
					if (isLastChar)
					{
						yield return string.Empty;
						break;
					}
					isNewPart = true;
				}
				else
				{
					sb.Append(c);
				}
				num = i;
			}
			string text5 = StringSplitter.GetLastPart(sb);
			if (inQuotedMode)
			{
				text5 = quoteChar.ToString() + text5;
			}
			if (text5 != null)
			{
				yield return text5;
			}
			yield break;
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x00028B06 File Offset: 0x00026D06
		private static string GetLastPart(StringBuilder sb)
		{
			if (sb.Length > 0)
			{
				return sb.ToString();
			}
			return null;
		}
	}
}
