using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Constraints
{
	// Token: 0x020019E2 RID: 6626
	public class SqlLearnConstraint : LearnConstraint
	{
		// Token: 0x0600D852 RID: 55378 RVA: 0x002DEAC0 File Offset: 0x002DCCC0
		public SqlLearnConstraint()
		{
			base.EnableMatchNames = MatchName.None;
			base.EnableSplit = false;
			base.EnableProperCase = false;
			base.EnableRoundNumber = false;
			base.EnableDateTimePart = false;
			base.EnableTimePart = false;
			base.EnableRoundDateTime = false;
			base.EnableParseDateTimePartial = false;
			base.EnableSliceBetween = false;
			base.EnableFromDateTimePart = false;
			base.EnableConditional = false;
		}
	}
}
