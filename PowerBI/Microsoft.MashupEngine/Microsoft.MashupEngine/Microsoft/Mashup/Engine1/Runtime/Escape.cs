using System;
using System.Globalization;
using System.Text;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012EB RID: 4843
	public static class Escape
	{
		// Token: 0x06008042 RID: 32834 RVA: 0x001B583C File Offset: 0x001B3A3C
		public static string AsQuotedCharacter(char character)
		{
			return Escape.AsQuotedCharacter(character, false);
		}

		// Token: 0x06008043 RID: 32835 RVA: 0x001B5848 File Offset: 0x001B3A48
		public static string AsQuotedCharacter(char character, bool unescapeNewLine)
		{
			Escape.NewLineFormatting newLineFormatting = (unescapeNewLine ? Escape.NewLineFormatting.Unescape : Escape.NewLineFormatting.None);
			return string.Format(CultureInfo.InvariantCulture, "\"{0}\"", Escape.GetString(character.ToString(CultureInfo.InvariantCulture), Escape.LiteralKind.String, newLineFormatting));
		}

		// Token: 0x06008044 RID: 32836 RVA: 0x001B587F File Offset: 0x001B3A7F
		public static string AsQuotedFormattedString(string value)
		{
			return string.Format(CultureInfo.InvariantCulture, "\"{0}\"", Escape.GetString(value, Escape.LiteralKind.String, Escape.NewLineFormatting.SplitLiteral));
		}

		// Token: 0x06008045 RID: 32837 RVA: 0x001B5898 File Offset: 0x001B3A98
		public static string AsQuotedString(string value)
		{
			return Escape.AsQuotedString(value, false);
		}

		// Token: 0x06008046 RID: 32838 RVA: 0x001B58A4 File Offset: 0x001B3AA4
		public static string AsQuotedString(string value, bool unescapeNewLine)
		{
			Escape.NewLineFormatting newLineFormatting = (unescapeNewLine ? Escape.NewLineFormatting.Unescape : Escape.NewLineFormatting.None);
			return string.Format(CultureInfo.InvariantCulture, "\"{0}\"", Escape.GetString(value, Escape.LiteralKind.String, newLineFormatting));
		}

		// Token: 0x06008047 RID: 32839 RVA: 0x001B58D0 File Offset: 0x001B3AD0
		public static string AsUnquotedString(string value)
		{
			return Escape.GetString(value, Escape.LiteralKind.String, Escape.NewLineFormatting.None);
		}

		// Token: 0x06008048 RID: 32840 RVA: 0x001B58DC File Offset: 0x001B3ADC
		private static string GetString(string value, Escape.LiteralKind literalKind, Escape.NewLineFormatting formatting)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				if (c <= '#')
				{
					switch (c)
					{
					case '\t':
						stringBuilder.Append("#(tab)");
						break;
					case '\n':
						if (formatting == Escape.NewLineFormatting.Unescape)
						{
							stringBuilder.Append(c);
						}
						else
						{
							stringBuilder.Append("#(lf)");
						}
						break;
					case '\v':
					case '\f':
						goto IL_01CB;
					case '\r':
						switch (formatting)
						{
						case Escape.NewLineFormatting.None:
							stringBuilder.Append("#(cr)");
							goto IL_0201;
						case Escape.NewLineFormatting.Unescape:
							stringBuilder.Append(c);
							goto IL_0201;
						}
						if (i + 1 < value.Length && value[i + 1] == '\n')
						{
							stringBuilder.Append("#(cr,lf)");
							i++;
						}
						else
						{
							stringBuilder.Append("#(cr)");
						}
						if (i + 1 < value.Length)
						{
							stringBuilder.Append("\"\r\n\"");
						}
						break;
					default:
						if (c != '"')
						{
							if (c != '#')
							{
								goto IL_01CB;
							}
							if (i + 1 < value.Length && value[i + 1] == '(')
							{
								stringBuilder.Append("#(#)");
							}
							else
							{
								stringBuilder.Append("#");
							}
						}
						else
						{
							stringBuilder.Append((literalKind == Escape.LiteralKind.String) ? "\"\"" : "\"");
						}
						break;
					}
				}
				else if (c <= '\u0085')
				{
					if (c != '\'')
					{
						if (c != '\u0085')
						{
							goto IL_01CB;
						}
						stringBuilder.Append("#(0085)");
					}
					else
					{
						stringBuilder.Append("'");
					}
				}
				else if (c != '\u2028')
				{
					if (c != '\u2029')
					{
						goto IL_01CB;
					}
					stringBuilder.Append("#(2029)");
				}
				else
				{
					stringBuilder.Append("#(2028)");
				}
				IL_0201:
				i++;
				continue;
				IL_01CB:
				if (!char.IsControl(c) && !Escape.IsNonCharacter(c))
				{
					stringBuilder.Append(c);
					goto IL_0201;
				}
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "#({0:X4})", (int)c));
				goto IL_0201;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06008049 RID: 32841 RVA: 0x001B5B00 File Offset: 0x001B3D00
		private static bool IsNonCharacter(char value)
		{
			return value >= '\ufdd0' && (value <= '\ufdef' || value >= '\ufffe');
		}

		// Token: 0x020012EC RID: 4844
		private enum LiteralKind
		{
			// Token: 0x040045DA RID: 17882
			String
		}

		// Token: 0x020012ED RID: 4845
		private enum NewLineFormatting
		{
			// Token: 0x040045DC RID: 17884
			None,
			// Token: 0x040045DD RID: 17885
			Unescape,
			// Token: 0x040045DE RID: 17886
			SplitLiteral
		}
	}
}
