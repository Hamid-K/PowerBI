using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis
{
	// Token: 0x02000089 RID: 137
	public interface IAlternatingLanguage : ILanguage
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600030C RID: 780
		IEnumerable<ILanguage> Alternatives { get; }
	}
}
