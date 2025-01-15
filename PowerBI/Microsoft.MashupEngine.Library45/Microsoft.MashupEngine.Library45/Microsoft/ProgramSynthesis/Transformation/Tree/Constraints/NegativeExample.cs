using System;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Constraints
{
	// Token: 0x02001EDA RID: 7898
	public class NegativeExample<TInput, TOutput> : Example<TInput, TOutput>
	{
		// Token: 0x06010A88 RID: 68232 RVA: 0x003959FB File Offset: 0x00393BFB
		public NegativeExample(TInput input, TOutput output, bool isSoft = false)
			: base(input, output, isSoft)
		{
		}
	}
}
