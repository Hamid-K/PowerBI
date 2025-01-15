using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive
{
	// Token: 0x0200071D RID: 1821
	public sealed class StdTactic : ITactic
	{
		// Token: 0x0600276A RID: 10090 RVA: 0x00002130 File Offset: 0x00000330
		private StdTactic()
		{
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x0600276B RID: 10091 RVA: 0x0006FAB0 File Offset: 0x0006DCB0
		public static StdTactic Instance { get; } = new StdTactic();

		// Token: 0x0600276C RID: 10092 RVA: 0x0006FAB8 File Offset: 0x0006DCB8
		public Optional<ProgramSet> LearnAlternative(IAlternatingLanguage language, Func<ILanguage, ProgramSet> learner)
		{
			List<ProgramSet> list = new List<ProgramSet>();
			foreach (ILanguage language2 in language.Alternatives)
			{
				ProgramSet programSet = learner(language2);
				if (!ProgramSet.IsNullOrEmpty(programSet))
				{
					list.Add(programSet);
				}
			}
			return list.NormalizedUnion().Some<ProgramSet>();
		}
	}
}
