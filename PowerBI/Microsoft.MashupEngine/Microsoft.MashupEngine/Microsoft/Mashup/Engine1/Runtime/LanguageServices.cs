using System;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200136B RID: 4971
	public static class LanguageServices
	{
		// Token: 0x0200136C RID: 4972
		public static class Identifier
		{
			// Token: 0x060082D7 RID: 33495 RVA: 0x001BBA72 File Offset: 0x001B9C72
			public static string Escape(string section, string name)
			{
				return LanguageServices.Identifier.Escape(section) + "!" + LanguageServices.Identifier.Escape(name);
			}

			// Token: 0x060082D8 RID: 33496 RVA: 0x001BBA8A File Offset: 0x001B9C8A
			public static string Escape(string name)
			{
				return LanguageServices.Identifier.Escape(name, null);
			}

			// Token: 0x060082D9 RID: 33497 RVA: 0x001BBA94 File Offset: 0x001B9C94
			public static string Escape(string name, ContextualKeyword[] keywords)
			{
				bool flag = false;
				if (name.Length == 0)
				{
					flag = true;
				}
				else
				{
					if (!LanguageServices.Identifier.IsStartCharacter(name[0]) || !LanguageServices.Identifier.IsEndCharacter(name[name.Length - 1]))
					{
						flag = true;
					}
					int num = 1;
					while (!flag && num < name.Length - 1)
					{
						if (!LanguageServices.Identifier.IsPartCharacter(name[num]))
						{
							flag = name[num] != '.' || name[num - 1] == '.';
						}
						num++;
					}
					if (!flag)
					{
						flag = Keyword.GetKeyword8(name) != null;
						if (!flag && keywords != null)
						{
							ContextualKeyword? contextualKeyword = Keyword.GetContextualKeyword(name);
							if (contextualKeyword != null)
							{
								int num2 = 0;
								while (!flag && num2 < keywords.Length)
								{
									ContextualKeyword? contextualKeyword2 = contextualKeyword;
									ContextualKeyword contextualKeyword3 = keywords[num2];
									flag = (contextualKeyword2.GetValueOrDefault() == contextualKeyword3) & (contextualKeyword2 != null);
									num2++;
								}
							}
						}
					}
				}
				if (flag)
				{
					return "#\"" + Microsoft.Mashup.Engine1.Runtime.Escape.AsUnquotedString(name) + "\"";
				}
				return name;
			}

			// Token: 0x060082DA RID: 33498 RVA: 0x001BBB81 File Offset: 0x001B9D81
			public static bool TryParse(string text, out string identifier)
			{
				return LanguageServices.Identifier.TryParse(text, 0, text.Length, out identifier);
			}

			// Token: 0x060082DB RID: 33499 RVA: 0x001BBB91 File Offset: 0x001B9D91
			public static bool TryParse(StringSegment text, out string identifier)
			{
				return LanguageServices.Identifier.TryParse(text.String, text.Offset, text.Length, out identifier);
			}

			// Token: 0x060082DC RID: 33500 RVA: 0x001BBBB0 File Offset: 0x001B9DB0
			public static bool TryParse(string text, int offset, int length, out string identifier)
			{
				if (length < 0 || length > text.Length)
				{
					throw new ArgumentOutOfRangeException("length");
				}
				if (offset < 0 || offset > text.Length - length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				if (length > 0)
				{
					if (LanguageServices.Identifier.IsStartCharacter(text[offset]) && LanguageServices.Identifier.IsEndCharacter(text[offset + length - 1]))
					{
						for (int i = 1; i < length - 1; i++)
						{
							if (!LanguageServices.Identifier.IsPartCharacter(text[offset + i]) && (text[offset + i] != '.' || text[offset + i - 1] == '.'))
							{
								identifier = null;
								return false;
							}
						}
						identifier = text.Substring(offset, length);
						return true;
					}
					string text2;
					if (text[offset] == '#' && LiteralValue.TryParseStringLiteral(text, offset + 1, length - 1, out text2))
					{
						identifier = text2;
						return true;
					}
				}
				identifier = null;
				return false;
			}

			// Token: 0x060082DD RID: 33501 RVA: 0x001BBC85 File Offset: 0x001B9E85
			public static bool IsPartCharacter(char character)
			{
				return (character >= 'a' && character <= 'z') || (character >= 'A' && character <= 'Z') || (character >= '0' && character <= '9') || character == '_' || (character >= '\u0080' && LanguageServices.Identifier.IsUnicodePartCharacter(character));
			}

			// Token: 0x060082DE RID: 33502 RVA: 0x001BBCBC File Offset: 0x001B9EBC
			public static bool IsStartCharacter(char character)
			{
				return (character >= 'a' && character <= 'z') || (character >= 'A' && character <= 'Z') || character == '_' || (character >= '\u0080' && LanguageServices.Identifier.IsUnicodeStartCharacter(character));
			}

			// Token: 0x060082DF RID: 33503 RVA: 0x001BBCE9 File Offset: 0x001B9EE9
			public static bool IsEndCharacter(char character)
			{
				return LanguageServices.Identifier.IsPartCharacter(character);
			}

			// Token: 0x060082E0 RID: 33504 RVA: 0x001BBCF4 File Offset: 0x001B9EF4
			private static bool IsUnicodePartCharacter(char character)
			{
				switch (char.GetUnicodeCategory(character))
				{
				case UnicodeCategory.UppercaseLetter:
				case UnicodeCategory.LowercaseLetter:
				case UnicodeCategory.TitlecaseLetter:
				case UnicodeCategory.ModifierLetter:
				case UnicodeCategory.OtherLetter:
				case UnicodeCategory.NonSpacingMark:
				case UnicodeCategory.SpacingCombiningMark:
				case UnicodeCategory.DecimalDigitNumber:
				case UnicodeCategory.LetterNumber:
				case UnicodeCategory.Format:
				case UnicodeCategory.ConnectorPunctuation:
					return true;
				}
				return false;
			}

			// Token: 0x060082E1 RID: 33505 RVA: 0x001BBD60 File Offset: 0x001B9F60
			private static bool IsUnicodeStartCharacter(char character)
			{
				UnicodeCategory unicodeCategory = char.GetUnicodeCategory(character);
				return unicodeCategory <= UnicodeCategory.OtherLetter || unicodeCategory == UnicodeCategory.LetterNumber;
			}

			// Token: 0x04004737 RID: 18231
			public const char EscapedIdentifierPrefix = '#';
		}

		// Token: 0x0200136D RID: 4973
		public static class FieldIdentifier
		{
			// Token: 0x060082E2 RID: 33506 RVA: 0x001BBD80 File Offset: 0x001B9F80
			public static string Escape(string text)
			{
				string text2;
				if (LanguageServices.FieldIdentifier.TryParse(text, out text2))
				{
					return text;
				}
				return LanguageServices.Identifier.Escape(text);
			}

			// Token: 0x060082E3 RID: 33507 RVA: 0x001BBD9F File Offset: 0x001B9F9F
			public static bool TryParse(string text, out string identifier)
			{
				return LanguageServices.FieldIdentifier.TryParse(text, 0, text.Length, out identifier);
			}

			// Token: 0x060082E4 RID: 33508 RVA: 0x001BBDAF File Offset: 0x001B9FAF
			public static bool TryParse(StringSegment text, out string identifier)
			{
				return LanguageServices.FieldIdentifier.TryParse(text.String, text.Offset, text.Length, out identifier);
			}

			// Token: 0x060082E5 RID: 33509 RVA: 0x001BBDCC File Offset: 0x001B9FCC
			public static bool TryParse(string text, int offset, int length, out string identifier)
			{
				if (LanguageServices.Identifier.TryParse(text, offset, length, out identifier))
				{
					return true;
				}
				int num = offset;
				int num2 = offset + length;
				if (length == 0 || !LanguageServices.Identifier.IsPartCharacter(text[num]) || !LanguageServices.Identifier.IsPartCharacter(text[num2 - 1]))
				{
					identifier = null;
					return false;
				}
				while (offset < num2)
				{
					char c = text[offset++];
					if (c != ' ')
					{
						if (c == '.')
						{
							if (offset < num2 && text[offset] == '.')
							{
								identifier = null;
								return false;
							}
						}
						else if (!LanguageServices.Identifier.IsPartCharacter(c))
						{
							identifier = null;
							return false;
						}
					}
				}
				identifier = text.Substring(num, offset - num);
				return true;
			}
		}
	}
}
