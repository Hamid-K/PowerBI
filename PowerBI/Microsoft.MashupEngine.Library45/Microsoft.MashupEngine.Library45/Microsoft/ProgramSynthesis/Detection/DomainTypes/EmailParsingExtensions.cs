using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Detection.DomainTypes
{
	// Token: 0x02000AEB RID: 2795
	internal static class EmailParsingExtensions
	{
		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x060045EE RID: 17902 RVA: 0x000DA134 File Offset: 0x000D8334
		private static HashSet<char> SpecialChars
		{
			get
			{
				return EmailParsingExtensions.SpecialCharsLazy.Value;
			}
		}

		// Token: 0x060045EF RID: 17903 RVA: 0x000DA140 File Offset: 0x000D8340
		private static bool IsPrintableAscii(this char c)
		{
			return c >= '!' && c <= '~';
		}

		// Token: 0x060045F0 RID: 17904 RVA: 0x000DA151 File Offset: 0x000D8351
		private static bool IsUnicode(this char c)
		{
			return c >= '\u0080';
		}

		// Token: 0x060045F1 RID: 17905 RVA: 0x000DA15E File Offset: 0x000D835E
		private static bool IsWhitespace(this char c)
		{
			return char.IsWhiteSpace(c) && c != '\n' && c != '\r';
		}

		// Token: 0x060045F2 RID: 17906 RVA: 0x000DA177 File Offset: 0x000D8377
		private static bool IsATextChar(this char c)
		{
			return !EmailParsingExtensions.SpecialChars.Contains(c) && (c.IsPrintableAscii() || c.IsUnicode());
		}

		// Token: 0x060045F3 RID: 17907 RVA: 0x000DA198 File Offset: 0x000D8398
		private static bool IsCTextChar(this char c)
		{
			return c != '(' && c != ')' && c != '\\' && (c.IsPrintableAscii() || c.IsUnicode());
		}

		// Token: 0x060045F4 RID: 17908 RVA: 0x000DA1BB File Offset: 0x000D83BB
		private static bool IsDTextChar(this char c)
		{
			return c != '[' && c != ']' && c != '\\' && (c.IsPrintableAscii() || c.IsUnicode());
		}

		// Token: 0x060045F5 RID: 17909 RVA: 0x000DA1DE File Offset: 0x000D83DE
		private static bool IsQTextChar(this char c)
		{
			return c != '\\' && c != '"' && (c.IsPrintableAscii() || c.IsUnicode());
		}

		// Token: 0x060045F6 RID: 17910 RVA: 0x000DA1FC File Offset: 0x000D83FC
		private static bool ParseAText(this string v, int index, out int nextIndex)
		{
			Predicate<char> predicate;
			if ((predicate = EmailParsingExtensions.<>O.<0>__IsATextChar) == null)
			{
				predicate = (EmailParsingExtensions.<>O.<0>__IsATextChar = new Predicate<char>(EmailParsingExtensions.IsATextChar));
			}
			return v.ParseWhile(index, out nextIndex, predicate);
		}

		// Token: 0x060045F7 RID: 17911 RVA: 0x000DA221 File Offset: 0x000D8421
		private static bool ParseCText(this string v, int index, out int nextIndex)
		{
			Predicate<char> predicate;
			if ((predicate = EmailParsingExtensions.<>O.<1>__IsCTextChar) == null)
			{
				predicate = (EmailParsingExtensions.<>O.<1>__IsCTextChar = new Predicate<char>(EmailParsingExtensions.IsCTextChar));
			}
			return v.ParseWhile(index, out nextIndex, predicate);
		}

		// Token: 0x060045F8 RID: 17912 RVA: 0x000DA246 File Offset: 0x000D8446
		private static bool ParseQText(this string v, int index, out int nextIndex)
		{
			Predicate<char> predicate;
			if ((predicate = EmailParsingExtensions.<>O.<2>__IsQTextChar) == null)
			{
				predicate = (EmailParsingExtensions.<>O.<2>__IsQTextChar = new Predicate<char>(EmailParsingExtensions.IsQTextChar));
			}
			return v.ParseWhile(index, out nextIndex, predicate);
		}

		// Token: 0x060045F9 RID: 17913 RVA: 0x000DA26B File Offset: 0x000D846B
		private static bool ParseDText(this string v, int index, out int nextIndex)
		{
			Predicate<char> predicate;
			if ((predicate = EmailParsingExtensions.<>O.<3>__IsDTextChar) == null)
			{
				predicate = (EmailParsingExtensions.<>O.<3>__IsDTextChar = new Predicate<char>(EmailParsingExtensions.IsDTextChar));
			}
			return v.ParseWhile(index, out nextIndex, predicate);
		}

		// Token: 0x060045FA RID: 17914 RVA: 0x000DA290 File Offset: 0x000D8490
		private static bool ParseQuotedPair(this string v, int index, out int nextIndex)
		{
			nextIndex = index;
			if (!v.ParseCharacter(index, '\\', out index))
			{
				return false;
			}
			if (index >= v.Length)
			{
				return false;
			}
			if (!v[index].IsPrintableAscii() && !v[index].IsUnicode() && !v[index].IsWhitespace())
			{
				return false;
			}
			nextIndex = index + 1;
			return true;
		}

		// Token: 0x060045FB RID: 17915 RVA: 0x000DA2EC File Offset: 0x000D84EC
		private static bool ParseFws(this string v, int index, out int nextIndex)
		{
			nextIndex = index;
			int num = index;
			Predicate<char> predicate;
			if ((predicate = EmailParsingExtensions.<>O.<4>__IsWhitespace) == null)
			{
				predicate = (EmailParsingExtensions.<>O.<4>__IsWhitespace = new Predicate<char>(EmailParsingExtensions.IsWhitespace));
			}
			bool flag = v.ParseWhile(num, out index, predicate);
			bool flag2 = v.ParseNewLine(index, out index);
			int num2 = index;
			Predicate<char> predicate2;
			if ((predicate2 = EmailParsingExtensions.<>O.<4>__IsWhitespace) == null)
			{
				predicate2 = (EmailParsingExtensions.<>O.<4>__IsWhitespace = new Predicate<char>(EmailParsingExtensions.IsWhitespace));
			}
			bool flag3 = v.ParseWhile(num2, out index, predicate2);
			if ((flag2 && flag3) || (!flag2 && (flag || flag3)))
			{
				nextIndex = index;
				return true;
			}
			return false;
		}

		// Token: 0x060045FC RID: 17916 RVA: 0x000DA363 File Offset: 0x000D8563
		private static bool ParseCContent(this string v, int index, out int nextIndex)
		{
			return v.ParseCText(index, out nextIndex) || v.ParseQuotedPair(index, out nextIndex) || v.ParseComment(index, out nextIndex);
		}

		// Token: 0x060045FD RID: 17917 RVA: 0x000DA383 File Offset: 0x000D8583
		private static bool ParseComment(this string v, int index, out int nextIndex)
		{
			nextIndex = index;
			if (!v.ParseCharacter(index, '(', out index))
			{
				return false;
			}
			for (;;)
			{
				v.ParseFws(index, out index);
				if (v.ParseCharacter(index, ')', out index))
				{
					break;
				}
				if (!v.ParseCContent(index, out index))
				{
					return false;
				}
			}
			nextIndex = index;
			return true;
		}

		// Token: 0x060045FE RID: 17918 RVA: 0x000DA3C0 File Offset: 0x000D85C0
		private static bool ParseCfws(this string v, int index, out int nextIndex)
		{
			int num = index;
			v.ParseFws(index, out index);
			while (v.ParseComment(index, out index))
			{
				v.ParseFws(index, out index);
			}
			nextIndex = index;
			return num == index;
		}

		// Token: 0x060045FF RID: 17919 RVA: 0x000DA3F8 File Offset: 0x000D85F8
		private static bool ParseQuotedString(this string v, int index, out int nextIndex, out int start, out int end)
		{
			nextIndex = index;
			start = (end = -1);
			v.ParseCfws(index, out index);
			if (!v.ParseCharacter(index, '"', out index))
			{
				return false;
			}
			start = index;
			v.ParseFws(index, out index);
			while (index < v.Length)
			{
				end = index;
				if (v.ParseCharacter(index, '"', out index))
				{
					v.ParseCfws(index, out nextIndex);
					return true;
				}
				v.ParseQContent(index, out index);
				v.ParseFws(index, out index);
				if (index == end)
				{
					break;
				}
			}
			return false;
		}

		// Token: 0x06004600 RID: 17920 RVA: 0x000DA479 File Offset: 0x000D8679
		private static bool ParseQContent(this string v, int index, out int nextIndex)
		{
			return v.ParseQText(index, out nextIndex) || v.ParseQuotedPair(index, out nextIndex);
		}

		// Token: 0x06004601 RID: 17921 RVA: 0x000DA490 File Offset: 0x000D8690
		private static bool ParseAtom(this string v, int index, out int nextIndex, out int textStart, out int textEnd)
		{
			textStart = (textEnd = -1);
			nextIndex = index;
			v.ParseCfws(index, out index);
			int num;
			if (!v.ParseAText(index, out num))
			{
				return false;
			}
			textStart = index;
			textEnd = num;
			index = num;
			v.ParseCfws(index, out nextIndex);
			return true;
		}

		// Token: 0x06004602 RID: 17922 RVA: 0x000DA4D4 File Offset: 0x000D86D4
		private static bool ParseDotAtom(this string v, int index, out int nextIndex, out int textStart, out int textEnd)
		{
			nextIndex = index;
			textStart = (textEnd = -1);
			v.ParseCfws(index, out index);
			int num;
			if (!v.ParseDotAtomText(index, out num))
			{
				return false;
			}
			textStart = index;
			textEnd = num;
			index = num;
			v.ParseCfws(index, out nextIndex);
			return true;
		}

		// Token: 0x06004603 RID: 17923 RVA: 0x000DA518 File Offset: 0x000D8718
		private static bool ParseDotAtomText(this string v, int index, out int nextIndex)
		{
			nextIndex = index;
			if (!v.ParseAText(index, out index))
			{
				return false;
			}
			for (;;)
			{
				nextIndex = index;
				if (!v.ParseCharacter(index, '.', out index))
				{
					break;
				}
				if (!v.ParseAText(index, out index))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004604 RID: 17924 RVA: 0x000DA548 File Offset: 0x000D8748
		private static bool ParseWord(this string v, int index, out int nextIndex, out int wordStart, out int wordEnd)
		{
			return v.ParseAtom(index, out nextIndex, out wordStart, out wordEnd) || v.ParseQuotedString(index, out nextIndex, out wordStart, out wordEnd);
		}

		// Token: 0x06004605 RID: 17925 RVA: 0x000DA564 File Offset: 0x000D8764
		private static bool ParsePhrase(this string v, int index, out int nextIndex, out int phraseStart, out int phraseEnd)
		{
			bool flag = false;
			phraseStart = (phraseEnd = -1);
			int num;
			int num2;
			while (v.ParseWord(index, out index, out num, out num2))
			{
				if (!flag)
				{
					flag = true;
					phraseStart = num;
				}
				phraseEnd = num2;
			}
			nextIndex = index;
			return flag;
		}

		// Token: 0x06004606 RID: 17926 RVA: 0x000DA5A0 File Offset: 0x000D87A0
		private static bool ParseDomainLiteral(this string v, int index, out int nextIndex, out int start, out int end)
		{
			nextIndex = index;
			start = (end = -1);
			v.ParseCfws(index, out index);
			if (!v.ParseCharacter(index, '[', out index))
			{
				return false;
			}
			start = index;
			v.ParseFws(index, out index);
			for (;;)
			{
				end = index;
				if (v.ParseCharacter(index, ']', out index))
				{
					break;
				}
				if (!v.ParseDText(index, out index))
				{
					return false;
				}
				v.ParseFws(index, out index);
			}
			v.ParseCfws(index, out nextIndex);
			return true;
		}

		// Token: 0x06004607 RID: 17927 RVA: 0x000DA613 File Offset: 0x000D8813
		private static bool ParseLocalPart(this string v, int index, out int nextIndex, out int localPartStart, out int localPartEnd)
		{
			return v.ParseDotAtom(index, out nextIndex, out localPartStart, out localPartEnd) || v.ParseQuotedString(index, out nextIndex, out localPartStart, out localPartEnd);
		}

		// Token: 0x06004608 RID: 17928 RVA: 0x000DA62F File Offset: 0x000D882F
		private static bool ParseDomain(this string v, int index, out int nextIndex, out int domainStart, out int domainEnd)
		{
			return v.ParseDotAtom(index, out nextIndex, out domainStart, out domainEnd) || v.ParseDomainLiteral(index, out nextIndex, out domainStart, out domainEnd);
		}

		// Token: 0x06004609 RID: 17929 RVA: 0x000DA64C File Offset: 0x000D884C
		private static bool ParseAddrSpec(this string v, int index, out int nextIndex, out int localPartStart, out int localPartEnd, out int domainStart, out int domainEnd)
		{
			nextIndex = index;
			localPartStart = (localPartEnd = (domainStart = (domainEnd = -1)));
			if (!v.ParseLocalPart(index, out index, out localPartStart, out localPartEnd))
			{
				return false;
			}
			if (!v.ParseCharacter(index, '@', out index))
			{
				return false;
			}
			if (!v.ParseDomain(index, out index, out domainStart, out domainEnd))
			{
				return false;
			}
			nextIndex = index;
			return true;
		}

		// Token: 0x0600460A RID: 17930 RVA: 0x000DA6A5 File Offset: 0x000D88A5
		private static bool ParseDisplayName(this string v, int index, out int nextIndex, out int displayNameStart, out int displayNameEnd)
		{
			return v.ParsePhrase(index, out nextIndex, out displayNameStart, out displayNameEnd);
		}

		// Token: 0x0600460B RID: 17931 RVA: 0x000DA6B4 File Offset: 0x000D88B4
		private static bool ParseAngleAddr(this string v, int index, out int nextIndex, out int localPartStart, out int localPartEnd, out int domainStart, out int domainEnd)
		{
			nextIndex = index;
			localPartStart = (localPartEnd = (domainStart = (domainEnd = -1)));
			v.ParseCfws(index, out index);
			if (!v.ParseCharacter(index, '<', out index))
			{
				return false;
			}
			if (!v.ParseAddrSpec(index, out index, out localPartStart, out localPartEnd, out domainStart, out domainEnd))
			{
				return false;
			}
			if (!v.ParseCharacter(index, '>', out index))
			{
				return false;
			}
			v.ParseCfws(index, out nextIndex);
			return true;
		}

		// Token: 0x0600460C RID: 17932 RVA: 0x000DA720 File Offset: 0x000D8920
		private static bool ParseNameAddr(this string v, int index, out int nextIndex, out int? displayNameStart, out int? displayNameEnd, out int localPartStart, out int localPartEnd, out int domainStart, out int domainEnd)
		{
			nextIndex = index;
			displayNameStart = (displayNameEnd = null);
			localPartStart = (localPartEnd = (domainStart = (domainEnd = -1)));
			int num;
			int num2;
			if (!v.ParseDisplayName(index, out index, out num, out num2))
			{
				return false;
			}
			displayNameStart = new int?(num);
			displayNameEnd = new int?(num2);
			if (!v.ParseAngleAddr(index, out index, out localPartStart, out localPartEnd, out domainStart, out domainEnd))
			{
				return false;
			}
			nextIndex = index;
			return true;
		}

		// Token: 0x0600460D RID: 17933 RVA: 0x000DA7A4 File Offset: 0x000D89A4
		public static bool ParseMailbox(this string v, int index, out int nextIndex, out int? displayNameStart, out int? displayNameEnd, out int localPartStart, out int localPartEnd, out int domainStart, out int domainEnd)
		{
			displayNameStart = (displayNameEnd = null);
			localPartStart = (localPartEnd = (domainStart = (domainEnd = -1)));
			nextIndex = index;
			return !string.IsNullOrEmpty(v) && (v.ParseAddrSpec(index, out nextIndex, out localPartStart, out localPartEnd, out domainStart, out domainEnd) || v.ParseNameAddr(index, out nextIndex, out displayNameStart, out displayNameEnd, out localPartStart, out localPartEnd, out domainStart, out domainEnd) || v.ParseAngleAddr(index, out nextIndex, out localPartStart, out localPartEnd, out domainStart, out domainEnd));
		}

		// Token: 0x04001FE7 RID: 8167
		private static readonly Lazy<HashSet<char>> SpecialCharsLazy = new Lazy<HashSet<char>>(() => new HashSet<char>
		{
			'(', ')', '<', '>', '[', ']', ':', ';', '@', '\\',
			',', '.', '"'
		});

		// Token: 0x02000AEC RID: 2796
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001FE8 RID: 8168
			public static Predicate<char> <0>__IsATextChar;

			// Token: 0x04001FE9 RID: 8169
			public static Predicate<char> <1>__IsCTextChar;

			// Token: 0x04001FEA RID: 8170
			public static Predicate<char> <2>__IsQTextChar;

			// Token: 0x04001FEB RID: 8171
			public static Predicate<char> <3>__IsDTextChar;

			// Token: 0x04001FEC RID: 8172
			public static Predicate<char> <4>__IsWhitespace;
		}
	}
}
