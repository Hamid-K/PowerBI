using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Constraints
{
	// Token: 0x020019DC RID: 6620
	public class PowerQueryLearnConstraint : LearnConstraint
	{
		// Token: 0x0600D82E RID: 55342 RVA: 0x002DE6B2 File Offset: 0x002DC8B2
		public PowerQueryLearnConstraint()
		{
			base.EnableParseDateTimePartial = false;
			base.EnableMatchNames = MatchName.None;
			base.EnableReplace = false;
			base.EnableSliceBetween = false;
			base.EnableFromDateTimePart = false;
		}
	}
}
