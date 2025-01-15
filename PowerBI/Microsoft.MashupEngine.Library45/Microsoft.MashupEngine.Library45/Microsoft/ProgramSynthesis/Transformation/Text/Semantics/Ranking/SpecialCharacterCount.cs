using System;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D41 RID: 7489
	internal class SpecialCharacterCount : ConstantStringFeature
	{
		// Token: 0x0600FC73 RID: 64627 RVA: 0x0035E093 File Offset: 0x0035C293
		public SpecialCharacterCount(Grammar grammar)
			: base(grammar, "SpecialCharacterCount", -3.44671389968767)
		{
		}

		// Token: 0x0600FC74 RID: 64628 RVA: 0x0035E0AA File Offset: 0x0035C2AA
		public override double Calculate(string s)
		{
			return (double)((s != null) ? s.Count(new Func<char, bool>(ConstantStringFeature.CommonDelimiters.Contains)) : 0);
		}
	}
}
