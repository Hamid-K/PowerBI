using System;
using System.Runtime.CompilerServices;

namespace AngleSharp.Extensions
{
	// Token: 0x020000E4 RID: 228
	internal static class CharExtensions
	{
		// Token: 0x060006CD RID: 1741 RVA: 0x00032AB0 File Offset: 0x00030CB0
		public static int FromHex(this char c)
		{
			if (!c.IsDigit())
			{
				return (int)(c - (c.IsLowercaseAscii() ? 'W' : '7'));
			}
			return (int)(c - '0');
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00032AD0 File Offset: 0x00030CD0
		public static string ToHex(this byte num)
		{
			char[] array = new char[2];
			int num2 = num >> 4;
			array[0] = (char)(num2 + ((num2 < 10) ? 48 : 55));
			num2 = (int)num - 16 * num2;
			array[1] = (char)(num2 + ((num2 < 10) ? 48 : 55));
			return new string(array);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00032B18 File Offset: 0x00030D18
		public static string ToHex(this char character)
		{
			int num = (int)character;
			return num.ToString("x");
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00032B33 File Offset: 0x00030D33
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsInRange(this char c, int lower, int upper)
		{
			return (int)c >= lower && (int)c <= upper;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00032B42 File Offset: 0x00030D42
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNormalQueryCharacter(this char c)
		{
			return c.IsInRange(33, 126) && c != '"' && c != '`' && c != '#' && c != '<' && c != '>';
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00032B6E File Offset: 0x00030D6E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNormalPathCharacter(this char c)
		{
			return c.IsInRange(32, 126) && c != '"' && c != '`' && c != '#' && c != '<' && c != '>' && c != ' ' && c != '?';
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00032BA4 File Offset: 0x00030DA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsUppercaseAscii(this char c)
		{
			return c >= 'A' && c <= 'Z';
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00032BB5 File Offset: 0x00030DB5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsLowercaseAscii(this char c)
		{
			return c >= 'a' && c <= 'z';
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00032BC6 File Offset: 0x00030DC6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsAlphanumericAscii(this char c)
		{
			return c.IsDigit() || c.IsUppercaseAscii() || c.IsLowercaseAscii();
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00032BE0 File Offset: 0x00030DE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsHex(this char c)
		{
			return c.IsDigit() || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f');
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00032C05 File Offset: 0x00030E05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNonAscii(this char c)
		{
			return c != char.MaxValue && c >= '\u0080';
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00032C1C File Offset: 0x00030E1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNonPrintable(this char c)
		{
			return (c >= '\0' && c <= '\b') || (c >= '\u000e' && c <= '\u001f') || (c >= '\u007f' && c <= '\u009f');
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00032C44 File Offset: 0x00030E44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsLetter(this char c)
		{
			return c.IsUppercaseAscii() || c.IsLowercaseAscii();
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00032C56 File Offset: 0x00030E56
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsName(this char c)
		{
			return c.IsNonAscii() || c.IsLetter() || c == '_' || c == '-' || c.IsDigit();
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00032C7A File Offset: 0x00030E7A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNameStart(this char c)
		{
			return c.IsNonAscii() || c.IsUppercaseAscii() || c.IsLowercaseAscii() || c == '_';
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00032C9B File Offset: 0x00030E9B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsLineBreak(this char c)
		{
			return c == '\n' || c == '\r';
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00032CA9 File Offset: 0x00030EA9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsSpaceCharacter(this char c)
		{
			return c == ' ' || c == '\t' || c == '\n' || c == '\r' || c == '\f';
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00032CC8 File Offset: 0x00030EC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsWhiteSpaceCharacter(this char c)
		{
			return c.IsInRange(9, 13) || c == ' ' || c == '\u0085' || c == '\u00a0' || c == '\u1680' || c == '\u180e' || c.IsInRange(8192, 8202) || c == '\u2028' || c == '\u2029' || c == '\u202f' || c == '\u205f' || c == '\u3000';
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00032D42 File Offset: 0x00030F42
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsDigit(this char c)
		{
			return c >= '0' && c <= '9';
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00032D54 File Offset: 0x00030F54
		public static bool IsUrlCodePoint(this char c)
		{
			return c.IsAlphanumericAscii() || c == '!' || c == '$' || c == '&' || c == '\'' || c == '(' || c == ')' || c == '*' || c == '+' || c == '-' || c == ',' || c == '.' || c == '/' || c == ':' || c == ';' || c == '=' || c == '?' || c == '@' || c == '_' || c == '~' || c.IsInRange(160, 55295) || c.IsInRange(57344, 64975) || c.IsInRange(65008, 65533) || c.IsInRange(65536, 131069) || c.IsInRange(131072, 196605) || c.IsInRange(196608, 262141) || c.IsInRange(262144, 327677) || c.IsInRange(327680, 393213) || c.IsInRange(393216, 458749) || c.IsInRange(458752, 524285) || c.IsInRange(524288, 589821) || c.IsInRange(589824, 655357) || c.IsInRange(655360, 720893) || c.IsInRange(720896, 786429) || c.IsInRange(786432, 851965) || c.IsInRange(851968, 917501) || c.IsInRange(917504, 983037) || c.IsInRange(983040, 1048573) || c.IsInRange(1048576, 1114109);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00032F7B File Offset: 0x0003117B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsInvalid(this int c)
		{
			return c == 0 || c > 1114111 || (c > 55296 && c < 57343);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00032F9C File Offset: 0x0003119C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsOneOf(this char c, char a, char b)
		{
			return a == c || b == c;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00032FA8 File Offset: 0x000311A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsOneOf(this char c, char o1, char o2, char o3)
		{
			return c == o1 || c == o2 || c == o3;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00032FB8 File Offset: 0x000311B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsOneOf(this char c, char o1, char o2, char o3, char o4)
		{
			return c == o1 || c == o2 || c == o3 || c == o4;
		}
	}
}
