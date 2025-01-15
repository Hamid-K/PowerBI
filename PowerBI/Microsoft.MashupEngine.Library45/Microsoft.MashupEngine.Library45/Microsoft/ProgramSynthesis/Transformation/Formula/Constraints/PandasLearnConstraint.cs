using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Constraints
{
	// Token: 0x020019D6 RID: 6614
	public class PandasLearnConstraint : LearnConstraint
	{
		// Token: 0x0600D80D RID: 55309 RVA: 0x002DE290 File Offset: 0x002DC490
		public PandasLearnConstraint()
		{
			base.EnableParseDateTimePartial = false;
		}
	}
}
