using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Detection.DomainTypes
{
	// Token: 0x02000AF6 RID: 2806
	internal static class PhoneNumberParsingExtensions
	{
		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06004649 RID: 17993 RVA: 0x000DBAC3 File Offset: 0x000D9CC3
		private static HashSet<char> AllowedSeparators
		{
			get
			{
				return PhoneNumberParsingExtensions.AllowedSeparatorsLazy.Value;
			}
		}

		// Token: 0x0600464A RID: 17994 RVA: 0x000DBAD0 File Offset: 0x000D9CD0
		private static bool ParseSeparator(this string v, int index, out int nextIndex, out char? separatorChar)
		{
			nextIndex = index;
			separatorChar = null;
			if (index >= v.Length)
			{
				return false;
			}
			if (PhoneNumberParsingExtensions.AllowedSeparators.Contains(v[index]))
			{
				separatorChar = new char?(v[index]);
				nextIndex = index + 1;
				return true;
			}
			return false;
		}

		// Token: 0x0600464B RID: 17995 RVA: 0x000DBB20 File Offset: 0x000D9D20
		public static bool ParseParenthesized(this string v, int index, out int nextIndex, out int npaStart, out int npaEnd, out int nxxStart, out int nxxEnd, out int xxxxStart, out int xxxxEnd)
		{
			npaStart = (npaEnd = (nxxStart = (nxxEnd = (xxxxStart = (xxxxEnd = -1)))));
			nextIndex = index;
			if (index >= v.Length)
			{
				return false;
			}
			bool flag = v.ParseCharacter(index, '(', out index);
			if (!v.ParseFirstThree(index, out index, out npaStart, out npaEnd))
			{
				return false;
			}
			if (flag && !v.ParseCharacter(index, ')', out index))
			{
				return false;
			}
			char? c;
			v.ParseSeparator(index, out index, out c);
			if (!v.ParseLastSeven(index, out index, out nxxStart, out nxxEnd, out xxxxStart, out xxxxEnd))
			{
				return false;
			}
			nextIndex = index;
			return true;
		}

		// Token: 0x0600464C RID: 17996 RVA: 0x000DBBB0 File Offset: 0x000D9DB0
		private static bool ParsePlain(this string v, int index, out int nextIndex, out int npaStart, out int npaEnd, out int nxxStart, out int nxxEnd, out int xxxxStart, out int xxxxEnd)
		{
			npaStart = (npaEnd = (nxxStart = (nxxEnd = (xxxxStart = (xxxxEnd = -1)))));
			nextIndex = index;
			if (!v.ParseFirstThree(index, out index, out npaStart, out npaEnd))
			{
				return false;
			}
			char? c;
			v.ParseSeparator(index, out nextIndex, out c);
			return v.ParseLastSeven(index, out nextIndex, out nxxStart, out nxxEnd, out xxxxStart, out xxxxEnd);
		}

		// Token: 0x0600464D RID: 17997 RVA: 0x000DBC14 File Offset: 0x000D9E14
		public static bool ParseInternational(this string v, int index, out int nextIndex, out int npaStart, out int npaEnd, out int nxxStart, out int nxxEnd, out int xxxxStart, out int xxxxEnd)
		{
			nextIndex = index;
			npaStart = (npaEnd = (nxxStart = (nxxEnd = (xxxxStart = (xxxxEnd = -1)))));
			bool flag = v.ParseCharacter(index, '+', out index);
			if (flag && !v.ParseCharacter(index, '1', out index))
			{
				return false;
			}
			if (!flag)
			{
				v.ParseCharacter(index, '1', out index);
			}
			return v.ParseParenthesized(index, out nextIndex, out npaStart, out npaEnd, out nxxStart, out nxxEnd, out xxxxStart, out xxxxEnd) || v.ParsePlain(index, out nextIndex, out npaStart, out npaEnd, out nxxStart, out nxxEnd, out xxxxStart, out xxxxEnd);
		}

		// Token: 0x0600464E RID: 17998 RVA: 0x000DBC9C File Offset: 0x000D9E9C
		public static bool ParseWithLeadingZeros(this string v, int index, out int nextIndex, out int npaStart, out int npaEnd, out int nxxStart, out int nxxEnd, out int xxxxStart, out int xxxxEnd)
		{
			nextIndex = index;
			npaStart = (npaEnd = (nxxStart = (nxxEnd = (xxxxStart = (xxxxEnd = -1)))));
			bool flag = v.ParseCharacter(index, '0', out index);
			if (flag && !v.ParseCharacter(index, '0', out index))
			{
				return false;
			}
			if (flag && !v.ParseCharacter(index, '1', out index))
			{
				return false;
			}
			char? c;
			v.ParseSeparator(index, out index, out c);
			return v.ParseParenthesized(index, out nextIndex, out npaStart, out npaEnd, out nxxStart, out nxxEnd, out xxxxStart, out xxxxEnd) || v.ParsePlain(index, out nextIndex, out npaStart, out npaEnd, out nxxStart, out nxxEnd, out xxxxStart, out xxxxEnd);
		}

		// Token: 0x0600464F RID: 17999 RVA: 0x000DBD34 File Offset: 0x000D9F34
		private static bool ParseFirstThree(this string v, int index, out int nextIndex, out int npaStart, out int npaEnd)
		{
			npaStart = (npaEnd = -1);
			nextIndex = index;
			long num;
			int num2;
			if (!v.ParseDecimalDigits(index, out num, out num2, new int?(3)) || num2 - index != 3 || num < 200L)
			{
				return false;
			}
			npaStart = index;
			npaEnd = num2;
			nextIndex = num2;
			return true;
		}

		// Token: 0x06004650 RID: 18000 RVA: 0x000DBD7C File Offset: 0x000D9F7C
		private static bool ParseLastSeven(this string v, int index, out int nextIndex, out int nxxStart, out int nxxEnd, out int xxxxStart, out int xxxxEnd)
		{
			nxxStart = (nxxEnd = (xxxxStart = (xxxxEnd = -1)));
			nextIndex = index;
			long num;
			int num2;
			if (!v.ParseDecimalDigits(index, out num, out num2, new int?(3)) || num2 - index != 3 || num < 200L)
			{
				return false;
			}
			nxxStart = index;
			nxxEnd = num2;
			index = num2;
			char? c;
			v.ParseSeparator(index, out index, out c);
			long num3;
			if (!v.ParseDecimalDigits(index, out num3, out num2, new int?(4)) || num2 - index != 4)
			{
				return false;
			}
			xxxxStart = index;
			xxxxEnd = num2;
			nextIndex = num2;
			return true;
		}

		// Token: 0x04001FF5 RID: 8181
		private static readonly Lazy<HashSet<char>> AllowedSeparatorsLazy = new Lazy<HashSet<char>>(() => new HashSet<char>
		{
			'.', ' ', '`', '-', '֊', '־', '᠆', '‐', '‑', '‒',
			'–', '—', '―', '⸺', '⸻', '⹀', '﹘', '﹣', '－', '\'',
			'·', '՚', '՛', '·', '׳', '٫', '\u06df', '܁', '܂', '•',
			'․', '‧', '′', '‵', '⁃', '⸱', '⸳', '・', '﹒', '．'
		});
	}
}
