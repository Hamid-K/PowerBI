using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis
{
	// Token: 0x02000088 RID: 136
	public interface ILanguage
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000308 RID: 776
		Symbol LanguageSymbol { get; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000309 RID: 777
		IEnumerable<ProgramNode> AllElements { get; }

		// Token: 0x0600030A RID: 778
		ILanguage Intersect(ILanguage other);

		// Token: 0x0600030B RID: 779
		ProgramSet Intersect(ProgramSet other);
	}
}
