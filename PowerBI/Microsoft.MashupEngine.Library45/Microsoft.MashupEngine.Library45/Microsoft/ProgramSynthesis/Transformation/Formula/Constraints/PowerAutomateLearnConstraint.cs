using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Constraints
{
	// Token: 0x020019D8 RID: 6616
	public class PowerAutomateLearnConstraint : LearnConstraint
	{
		// Token: 0x0600D820 RID: 55328 RVA: 0x002DE586 File Offset: 0x002DC786
		public PowerAutomateLearnConstraint()
		{
			base.EnableMatchNames = MatchName.None;
			base.EnableProperCase = false;
			base.EnableRoundNumber = false;
			base.EnableParseDateTimePartial = true;
		}
	}
}
