using System;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D49 RID: 7497
	internal class AllCommonDelimiters : ConstantStringFeature
	{
		// Token: 0x0600FC87 RID: 64647 RVA: 0x0035E2FB File Offset: 0x0035C4FB
		public AllCommonDelimiters(Grammar grammar)
			: base(grammar, "AllCommonDelimiters", 46.4181876206461)
		{
		}

		// Token: 0x0600FC88 RID: 64648 RVA: 0x0035E312 File Offset: 0x0035C512
		public override double Calculate(string s)
		{
			return (s != null && s.All(new Func<char, bool>(ConstantStringFeature.CommonDelimiters.Contains))) > false;
		}
	}
}
