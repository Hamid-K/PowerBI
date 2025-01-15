using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x02000302 RID: 770
	public class VariableNameOptimizer : IOptimizer
	{
		// Token: 0x060010C6 RID: 4294 RVA: 0x0002FFD5 File Offset: 0x0002E1D5
		public VariableNameOptimizer(IEnumerable<string> boundNames)
		{
			this._boundNames = new HashSet<string>(boundNames ?? Enumerable.Empty<string>());
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0002FFF4 File Offset: 0x0002E1F4
		public List<SSAStep> Optimize(IReadOnlyList<SSAStep> steps)
		{
			VariableNameOptimizer.<>c__DisplayClass2_0 CS$<>8__locals1 = new VariableNameOptimizer.<>c__DisplayClass2_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.liveNames = new HashSet<string>();
			foreach (SSAStep ssastep in steps)
			{
				VariableNameOptimizer.<>c__DisplayClass2_1 CS$<>8__locals2 = new VariableNameOptimizer.<>c__DisplayClass2_1();
				SSARegister lvalue = ssastep.LValue;
				string text = lvalue.GetName();
				int length = text.Length;
				int num = text.LastIndexOf('_');
				VariableNameOptimizer.<>c__DisplayClass2_1 CS$<>8__locals3 = CS$<>8__locals2;
				if (num == -1 || text.Length <= num + 1)
				{
					goto IL_008B;
				}
				IEnumerable<char> enumerable = text.Substring(num + 1);
				Func<char, bool> func;
				if ((func = VariableNameOptimizer.<>O.<0>__IsDigit) == null)
				{
					func = (VariableNameOptimizer.<>O.<0>__IsDigit = new Func<char, bool>(char.IsDigit));
				}
				if (!enumerable.All(func))
				{
					goto IL_008B;
				}
				string text2 = text.Substring(0, num);
				IL_0097:
				CS$<>8__locals3.basename = text2;
				text = CS$<>8__locals2.basename;
				if (CS$<>8__locals1.<Optimize>g__isBound|0(text))
				{
					IEnumerable<string> enumerable2 = from i in Enumerable.Range(0, int.MaxValue)
						select FormattableString.Invariant(FormattableStringFactory.Create("{0}_{1}", new object[] { CS$<>8__locals2.basename, i }));
					Func<string, bool> func2;
					if ((func2 = CS$<>8__locals1.<>9__2) == null)
					{
						func2 = (CS$<>8__locals1.<>9__2 = (string n) => !base.<Optimize>g__isBound|0(n));
					}
					text = enumerable2.First(func2);
				}
				lvalue.SetDesiredName(text);
				CS$<>8__locals1.liveNames.Add(text);
				continue;
				IL_008B:
				text2 = text;
				goto IL_0097;
			}
			return steps.ToList<SSAStep>();
		}

		// Token: 0x04000819 RID: 2073
		private HashSet<string> _boundNames;

		// Token: 0x02000303 RID: 771
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400081A RID: 2074
			public static Func<char, bool> <0>__IsDigit;
		}
	}
}
