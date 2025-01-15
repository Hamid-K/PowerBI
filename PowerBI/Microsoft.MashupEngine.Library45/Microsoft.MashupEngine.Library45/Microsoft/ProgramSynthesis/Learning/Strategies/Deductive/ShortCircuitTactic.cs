using System;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive
{
	// Token: 0x0200071E RID: 1822
	public sealed class ShortCircuitTactic : ITactic
	{
		// Token: 0x0600276E RID: 10094 RVA: 0x00002130 File Offset: 0x00000330
		private ShortCircuitTactic()
		{
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x0600276F RID: 10095 RVA: 0x0006FB34 File Offset: 0x0006DD34
		public static ShortCircuitTactic Instance { get; } = new ShortCircuitTactic();

		// Token: 0x06002770 RID: 10096 RVA: 0x0006FB3C File Offset: 0x0006DD3C
		public Optional<ProgramSet> LearnAlternative(IAlternatingLanguage language, Func<ILanguage, ProgramSet> learner)
		{
			foreach (ILanguage language2 in language.Alternatives)
			{
				ProgramSet programSet = learner(language2);
				if (!ProgramSet.IsNullOrEmpty(programSet))
				{
					return programSet.Some<ProgramSet>();
				}
			}
			return ProgramSet.Empty(language.LanguageSymbol).Some<ProgramSet>();
		}
	}
}
