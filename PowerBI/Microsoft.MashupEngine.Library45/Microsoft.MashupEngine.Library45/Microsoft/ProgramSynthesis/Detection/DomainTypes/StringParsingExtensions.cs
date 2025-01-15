using System;

namespace Microsoft.ProgramSynthesis.Detection.DomainTypes
{
	// Token: 0x02000AF3 RID: 2803
	internal static class StringParsingExtensions
	{
		// Token: 0x06004633 RID: 17971 RVA: 0x000DB4CC File Offset: 0x000D96CC
		public static bool ParseDecimalDigits(this string v, int index, out long value, out int nextIndex, int? maxDigits = null)
		{
			int num = maxDigits ?? int.MaxValue;
			value = 0L;
			bool flag = false;
			int num2 = 0;
			nextIndex = index;
			while (index < v.Length && char.IsDigit(v[index]) && num2 < num)
			{
				flag = true;
				value = value * 10L + (long)((ulong)v[index]) - 48L;
				index++;
				num2++;
			}
			nextIndex = index;
			return flag;
		}

		// Token: 0x06004634 RID: 17972 RVA: 0x000DB540 File Offset: 0x000D9740
		private static bool IsHex(this char c, out int value)
		{
			char c2 = char.ToLowerInvariant(c);
			if (char.IsDigit(c2))
			{
				value = (int)(c2 - '0');
				return true;
			}
			if (c2 >= 'a' && c2 <= 'f')
			{
				value = (int)('\n' + (c2 - 'a'));
				return true;
			}
			value = -1;
			return false;
		}

		// Token: 0x06004635 RID: 17973 RVA: 0x000DB580 File Offset: 0x000D9780
		public static bool ParseHexDigits(this string v, int index, out long value, out int nextIndex)
		{
			value = 0L;
			bool flag = false;
			int num;
			while (index < v.Length && v[index].IsHex(out num))
			{
				flag = true;
				value = value * 16L + (long)num;
				index++;
			}
			nextIndex = index;
			return flag;
		}

		// Token: 0x06004636 RID: 17974 RVA: 0x000DB5C4 File Offset: 0x000D97C4
		public static bool ParseCharacter(this string v, int index, char c, out int nextIndex)
		{
			if (index < v.Length && v[index] == c)
			{
				nextIndex = index + 1;
				return true;
			}
			nextIndex = index;
			return false;
		}

		// Token: 0x06004637 RID: 17975 RVA: 0x000DB5E4 File Offset: 0x000D97E4
		public static bool ParseCharacter(this string v, int index, char c, out int nextIndex, out char? parsedChar)
		{
			parsedChar = null;
			if (index < v.Length && v[index] == c)
			{
				nextIndex = index + 1;
				parsedChar = new char?(c);
				return true;
			}
			nextIndex = index;
			return false;
		}

		// Token: 0x06004638 RID: 17976 RVA: 0x000DB61C File Offset: 0x000D981C
		public static bool ParseString(this string v, int index, string pattern, out int nextIndex)
		{
			if (index + pattern.Length > v.Length)
			{
				nextIndex = index;
				return false;
			}
			for (int i = 0; i < pattern.Length; i++)
			{
				if (v[index + i] != pattern[i])
				{
					nextIndex = index;
					return false;
				}
			}
			nextIndex = index + pattern.Length;
			return true;
		}

		// Token: 0x06004639 RID: 17977 RVA: 0x000DB674 File Offset: 0x000D9874
		public static bool ParseWhitespace(this string v, int index, out int nextIndex)
		{
			bool flag = false;
			while (index < v.Length && char.IsWhiteSpace(v[index]))
			{
				flag = true;
				index++;
			}
			nextIndex = index;
			return flag;
		}

		// Token: 0x0600463A RID: 17978 RVA: 0x000DB6A7 File Offset: 0x000D98A7
		public static bool ParseWhitespaceAtEnd(this string v, int index, out int nextIndex)
		{
			nextIndex = index;
			return index >= v.Length || (v.ParseWhitespace(index, out nextIndex) && nextIndex >= v.Length);
		}

		// Token: 0x0600463B RID: 17979 RVA: 0x000DB6D0 File Offset: 0x000D98D0
		public static bool ParseUntil(this string v, int index, out int nextIndex, Predicate<char> stoppingPredicate)
		{
			bool flag = false;
			while (index < v.Length && !stoppingPredicate(v[index]))
			{
				flag = true;
				index++;
			}
			nextIndex = index;
			return flag;
		}

		// Token: 0x0600463C RID: 17980 RVA: 0x000DB704 File Offset: 0x000D9904
		public static bool ParseWhile(this string v, int index, out int nextIndex, Predicate<char> predicate)
		{
			return v.ParseUntil(index, out nextIndex, (char c) => !predicate(c));
		}

		// Token: 0x0600463D RID: 17981 RVA: 0x000DB734 File Offset: 0x000D9934
		public static bool ParseNewLine(this string v, int index, out int nextIndex)
		{
			if (index <= v.Length - 2 && v[index] == '\r' && v[index + 1] == '\n')
			{
				nextIndex = index + 2;
				return true;
			}
			if (index <= v.Length - 1 && v[index] == '\n')
			{
				nextIndex = index + 1;
				return true;
			}
			nextIndex = index;
			return false;
		}

		// Token: 0x0600463E RID: 17982 RVA: 0x000DB78C File Offset: 0x000D998C
		private static bool ParseSign(this string v, int index, out int nextIndex, out bool isNegative)
		{
			nextIndex = index;
			isNegative = false;
			if (index >= v.Length)
			{
				return false;
			}
			if (v[index] == '-' || v[index] == '−' || v[index] == '﹣' || v[index] == '－')
			{
				isNegative = true;
				nextIndex = index + 1;
				return true;
			}
			if (v[index] == '+' || v[index] == '﬩')
			{
				nextIndex = index + 1;
				return true;
			}
			nextIndex = index;
			return false;
		}

		// Token: 0x0600463F RID: 17983 RVA: 0x000DB810 File Offset: 0x000D9A10
		public static bool ParseSignedReal(this string v, int index, out double value, out int nextIndex, bool strict = false)
		{
			int num = index;
			value = double.NaN;
			bool flag;
			v.ParseSign(index, out index, out flag);
			if (!v.ParseReal(index, out value, out nextIndex, strict))
			{
				nextIndex = num;
				return false;
			}
			value = (flag ? (-value) : value);
			return true;
		}

		// Token: 0x06004640 RID: 17984 RVA: 0x000DB858 File Offset: 0x000D9A58
		public static bool ParseReal(this string v, int index, out double value, out int nextIndex, bool strict = false)
		{
			double num = 0.0;
			bool flag = false;
			nextIndex = index;
			value = double.NaN;
			while (index < v.Length && char.IsDigit(v[index]))
			{
				flag = true;
				num = num * 10.0 + (double)(v[index] - '0');
				index++;
			}
			if (index >= v.Length || (v[index] != '.' && v[index] != ',' && v[index] != '/'))
			{
				if (!strict)
				{
					value = (flag ? num : double.NaN);
					nextIndex = index;
					return flag;
				}
				return false;
			}
			else
			{
				index++;
				double num2 = 0.0;
				double num3 = 10.0;
				flag = false;
				while (index < v.Length && char.IsDigit(v[index]))
				{
					flag = true;
					num2 += (double)(v[index] - '0') / num3;
					num3 *= 10.0;
					index++;
				}
				if (flag)
				{
					nextIndex = index;
					value = num + num2;
					return true;
				}
				return false;
			}
		}
	}
}
