using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002F6 RID: 758
	public class SubstituteValues : IOptimizer
	{
		// Token: 0x0600106F RID: 4207 RVA: 0x0002EF9F File Offset: 0x0002D19F
		public SubstituteValues(IReadOnlyDictionary<SSARValue, SSARValue> substitutions)
		{
			this._substitutions = substitutions;
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x0002EFAE File Offset: 0x0002D1AE
		public List<SSAStep> Optimize(IReadOnlyList<SSAStep> steps)
		{
			return steps.Select(new Func<SSAStep, SSAStep>(this.Substitute)).ToList<SSAStep>();
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x0002EFC8 File Offset: 0x0002D1C8
		private void Substitute(SSAFunctionApplication app)
		{
			foreach (SSARValue ssarvalue in app.FunctionArguments.OfType<SSARValue>().ToList<SSARValue>())
			{
				SSARValue ssarvalue2;
				if (this._substitutions.TryGetValue(ssarvalue, out ssarvalue2))
				{
					app.SubstituteArgument(ssarvalue, ssarvalue2);
				}
				else
				{
					SSAFunctionApplication ssafunctionApplication = ssarvalue as SSAFunctionApplication;
					if (ssafunctionApplication != null)
					{
						this.Substitute(ssafunctionApplication);
					}
				}
			}
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x0002F04C File Offset: 0x0002D24C
		private SSAStep Substitute(SSAStep step)
		{
			SSARValue ssarvalue;
			if (this._substitutions.TryGetValue(step.RValue, out ssarvalue))
			{
				step.LValue.ImmediateUpLinks.Remove(step.RValue);
				step.RValue.ImmediateDownLinks.Remove(step.LValue);
				return new SSAStep(step.LValue, ssarvalue, "");
			}
			SSAFunctionApplication ssafunctionApplication = step.RValue as SSAFunctionApplication;
			if (ssafunctionApplication != null)
			{
				this.Substitute(ssafunctionApplication);
			}
			return step;
		}

		// Token: 0x040007ED RID: 2029
		private readonly IReadOnlyDictionary<SSARValue, SSARValue> _substitutions;
	}
}
