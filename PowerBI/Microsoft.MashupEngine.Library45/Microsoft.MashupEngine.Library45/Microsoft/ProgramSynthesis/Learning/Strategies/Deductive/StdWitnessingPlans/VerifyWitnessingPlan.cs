using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.StdWitnessingPlans
{
	// Token: 0x02000746 RID: 1862
	internal class VerifyWitnessingPlan : StdWitnessingPlan<NonterminalRule, Spec>
	{
		// Token: 0x060027F0 RID: 10224 RVA: 0x0007178C File Offset: 0x0006F98C
		public override bool CanCall(NonterminalRule rule, Spec spec)
		{
			if (!base.CanCall(rule, spec))
			{
				return false;
			}
			bool flag = true;
			using (IEnumerator<Symbol> enumerator = rule.Body.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.IsVariable)
					{
						flag = false;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x000717EC File Offset: 0x0006F9EC
		internal override WitnessFunction PreferredWitnessFunctionFor(NonterminalRule rule, int parameter, Spec spec)
		{
			return rule.WitnessFunctionsFor(parameter, spec, null).FirstOrDefault<WitnessFunction>();
		}
	}
}
