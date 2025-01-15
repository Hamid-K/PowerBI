using System;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive
{
	// Token: 0x0200071A RID: 1818
	public interface ITactic
	{
		// Token: 0x06002766 RID: 10086
		Optional<ProgramSet> LearnAlternative(IAlternatingLanguage language, Func<ILanguage, ProgramSet> learner);
	}
}
