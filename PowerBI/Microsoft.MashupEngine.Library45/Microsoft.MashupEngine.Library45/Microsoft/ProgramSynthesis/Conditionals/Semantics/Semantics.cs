using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Conditionals.Semantics
{
	// Token: 0x02000A55 RID: 2645
	public static class Semantics
	{
		// Token: 0x0600419D RID: 16797 RVA: 0x000CD646 File Offset: 0x000CB846
		public static bool Conjunction(bool pred, bool result)
		{
			return pred && result;
		}

		// Token: 0x0600419E RID: 16798 RVA: 0x000CD64B File Offset: 0x000CB84B
		[LazySemantics]
		public static bool Contains(LearningCacheSubstring s, RegularExpression r, int k)
		{
			return !string.IsNullOrEmpty((s != null) ? s.Value : null) && r.Run(s).Length == k;
		}

		// Token: 0x0600419F RID: 16799 RVA: 0x000CD66E File Offset: 0x000CB86E
		[LazySemantics]
		public static bool ContainsString(LearningCacheSubstring s, string str, int k)
		{
			return !string.IsNullOrEmpty((s != null) ? s.Value : null) && s.Value.AllIndexesOf(str, StringComparison.Ordinal).HasExactly(k);
		}

		// Token: 0x060041A0 RID: 16800 RVA: 0x00010FAF File Offset: 0x0000F1AF
		public static bool Disjunction(bool pred, bool result)
		{
			return pred || result;
		}

		// Token: 0x060041A1 RID: 16801 RVA: 0x000CD698 File Offset: 0x000CB898
		[LazySemantics]
		public static bool EndsWith(LearningCacheSubstring s, RegularExpression r)
		{
			if (string.IsNullOrEmpty((s != null) ? s.Value : null))
			{
				return false;
			}
			PositionMatch[] array = r.Run(s);
			return array.Length != 0 && array.Last<PositionMatch>().Right == s.End;
		}

		// Token: 0x060041A2 RID: 16802 RVA: 0x000CD6DB File Offset: 0x000CB8DB
		[LazySemantics]
		public static bool EndsWithDigit(LearningCacheSubstring s)
		{
			if (s == null)
			{
				return false;
			}
			Optional<char> optional = s.MaybeLastChar();
			Func<char, bool> func;
			if ((func = Semantics.<>O.<0>__IsDigit) == null)
			{
				func = (Semantics.<>O.<0>__IsDigit = new Func<char, bool>(char.IsDigit));
			}
			return optional.Select(func).OrElse(false);
		}

		// Token: 0x060041A3 RID: 16803 RVA: 0x000CD70E File Offset: 0x000CB90E
		[LazySemantics]
		public static bool EndsWithLetter(LearningCacheSubstring s)
		{
			if (s == null)
			{
				return false;
			}
			Optional<char> optional = s.MaybeLastChar();
			Func<char, bool> func;
			if ((func = Semantics.<>O.<1>__IsLetter) == null)
			{
				func = (Semantics.<>O.<1>__IsLetter = new Func<char, bool>(char.IsLetter));
			}
			return optional.Select(func).OrElse(false);
		}

		// Token: 0x060041A4 RID: 16804 RVA: 0x000CD741 File Offset: 0x000CB941
		[LazySemantics]
		public static bool EndsWithString(LearningCacheSubstring s, string str)
		{
			return !string.IsNullOrEmpty((s != null) ? s.Value : null) && s.EndsWith(str);
		}

		// Token: 0x060041A5 RID: 16805 RVA: 0x000CD75F File Offset: 0x000CB95F
		[LazySemantics]
		public static bool IsNull(LearningCacheSubstring s)
		{
			return ((s != null) ? s.Value : null) == null;
		}

		// Token: 0x060041A6 RID: 16806 RVA: 0x000CD770 File Offset: 0x000CB970
		[LazySemantics]
		public static bool IsNullOrWhiteSpace(LearningCacheSubstring s)
		{
			return string.IsNullOrWhiteSpace((s != null) ? s.Value : null);
		}

		// Token: 0x060041A7 RID: 16807 RVA: 0x000CD783 File Offset: 0x000CB983
		[LazySemantics]
		public static bool IsWhiteSpace(LearningCacheSubstring s)
		{
			return s != null && s.Value != null && string.IsNullOrWhiteSpace(s.Value);
		}

		// Token: 0x060041A8 RID: 16808 RVA: 0x000CD7A0 File Offset: 0x000CB9A0
		[LazySemantics]
		public static bool Matches(LearningCacheSubstring s, RegularExpression r)
		{
			if (string.IsNullOrEmpty((s != null) ? s.Value : null))
			{
				return false;
			}
			PositionMatch[] array = r.Run(s);
			return array.Length == 1 && array[0].Position == s.Start && array[0].Right == s.End;
		}

		// Token: 0x060041A9 RID: 16809 RVA: 0x000CD7FA File Offset: 0x000CB9FA
		public static bool Not(bool match)
		{
			return !match;
		}

		// Token: 0x060041AA RID: 16810 RVA: 0x000CD800 File Offset: 0x000CBA00
		[LazySemantics]
		public static bool StartsWith(LearningCacheSubstring s, RegularExpression r)
		{
			if (string.IsNullOrEmpty((s != null) ? s.Value : null))
			{
				return false;
			}
			PositionMatch[] array = r.Run(s);
			return array.Length != 0 && array[0].Position == s.Start;
		}

		// Token: 0x060041AB RID: 16811 RVA: 0x000CD844 File Offset: 0x000CBA44
		[LazySemantics]
		public static bool StartsWithDigit(LearningCacheSubstring s)
		{
			if (s == null)
			{
				return false;
			}
			Optional<char> optional = s.MaybeFirstChar();
			Func<char, bool> func;
			if ((func = Semantics.<>O.<0>__IsDigit) == null)
			{
				func = (Semantics.<>O.<0>__IsDigit = new Func<char, bool>(char.IsDigit));
			}
			return optional.Select(func).OrElse(false);
		}

		// Token: 0x060041AC RID: 16812 RVA: 0x000CD877 File Offset: 0x000CBA77
		[LazySemantics]
		public static bool StartsWithLetter(LearningCacheSubstring s)
		{
			if (s == null)
			{
				return false;
			}
			Optional<char> optional = s.MaybeFirstChar();
			Func<char, bool> func;
			if ((func = Semantics.<>O.<1>__IsLetter) == null)
			{
				func = (Semantics.<>O.<1>__IsLetter = new Func<char, bool>(char.IsLetter));
			}
			return optional.Select(func).OrElse(false);
		}

		// Token: 0x060041AD RID: 16813 RVA: 0x000CD8AA File Offset: 0x000CBAAA
		[LazySemantics]
		public static bool StartsWithString(LearningCacheSubstring s, string str)
		{
			return !string.IsNullOrEmpty((s != null) ? s.Value : null) && s.StartsWith(str);
		}

		// Token: 0x060041AE RID: 16814 RVA: 0x0000A5FD File Offset: 0x000087FD
		public static bool True()
		{
			return true;
		}

		// Token: 0x02000A56 RID: 2646
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001D90 RID: 7568
			public static Func<char, bool> <0>__IsDigit;

			// Token: 0x04001D91 RID: 7569
			public static Func<char, bool> <1>__IsLetter;
		}
	}
}
