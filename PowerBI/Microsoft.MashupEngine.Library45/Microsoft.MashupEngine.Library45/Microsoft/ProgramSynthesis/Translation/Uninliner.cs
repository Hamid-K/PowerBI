using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002FC RID: 764
	public class Uninliner : IOptimizer
	{
		// Token: 0x060010A9 RID: 4265 RVA: 0x0002F9E8 File Offset: 0x0002DBE8
		private static IEnumerable<SSAStep> UninlineStep(SSAStep step)
		{
			SSARValue rvalue = step.RValue;
			SSAFunctionApplication app = rvalue as SSAFunctionApplication;
			if (app != null)
			{
				foreach (SSAFunctionApplication arg in app.FunctionArguments.OfType<SSAFunctionApplication>().Distinct<SSAFunctionApplication>().ToList<SSAFunctionApplication>())
				{
					SSAStep newStep = new SSAStep(new SSARegister(arg.ValueType), arg, "");
					yield return newStep;
					app.SubstituteArgument(arg, newStep.LValue);
					newStep = null;
					arg = null;
				}
				List<SSAFunctionApplication>.Enumerator enumerator = default(List<SSAFunctionApplication>.Enumerator);
			}
			yield return step;
			yield break;
			yield break;
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0002F9F8 File Offset: 0x0002DBF8
		public List<SSAStep> Optimize(IReadOnlyList<SSAStep> steps)
		{
			List<SSAStep> list = steps.ToList<SSAStep>();
			int count;
			do
			{
				count = list.Count;
				IEnumerable<SSAStep> enumerable = list;
				Func<SSAStep, IEnumerable<SSAStep>> func;
				if ((func = Uninliner.<>O.<0>__UninlineStep) == null)
				{
					func = (Uninliner.<>O.<0>__UninlineStep = new Func<SSAStep, IEnumerable<SSAStep>>(Uninliner.UninlineStep));
				}
				list = enumerable.SelectMany(func).ToList<SSAStep>();
			}
			while (list.Count != count);
			return list;
		}

		// Token: 0x04000809 RID: 2057
		public static readonly Uninliner Instance = new Uninliner();

		// Token: 0x020002FD RID: 765
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400080A RID: 2058
			public static Func<SSAStep, IEnumerable<SSAStep>> <0>__UninlineStep;
		}
	}
}
