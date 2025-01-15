using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Constraints
{
	// Token: 0x020019CC RID: 6604
	public class ConditionalLearnConstraint : OperatorLearnConstraint, IOptionConstraint<LearnOptions>
	{
		// Token: 0x0600D77D RID: 55165 RVA: 0x002DD04B File Offset: 0x002DB24B
		public ConditionalLearnConstraint()
		{
		}

		// Token: 0x0600D77E RID: 55166 RVA: 0x002DD053 File Offset: 0x002DB253
		public ConditionalLearnConstraint(LearnConstraint parentLearnConstraint)
			: base(parentLearnConstraint)
		{
		}

		// Token: 0x0600D77F RID: 55167 RVA: 0x002DD05C File Offset: 0x002DB25C
		public override void SetOptions(LearnOptions options)
		{
			base.SetOptions(options);
			options.EnableConditional = false;
		}
	}
}
