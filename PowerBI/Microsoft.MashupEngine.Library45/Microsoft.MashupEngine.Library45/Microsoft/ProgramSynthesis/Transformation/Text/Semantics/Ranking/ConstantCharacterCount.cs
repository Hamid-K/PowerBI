using System;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D40 RID: 7488
	internal class ConstantCharacterCount : ConstantStringFeature
	{
		// Token: 0x0600FC71 RID: 64625 RVA: 0x0035E06D File Offset: 0x0035C26D
		public ConstantCharacterCount(Grammar grammar)
			: base(grammar, "ConstantCharacterCount", -17.3907351641186)
		{
		}

		// Token: 0x0600FC72 RID: 64626 RVA: 0x0035E084 File Offset: 0x0035C284
		public override double Calculate(string s)
		{
			return (double)((s != null) ? s.Length : 0);
		}
	}
}
