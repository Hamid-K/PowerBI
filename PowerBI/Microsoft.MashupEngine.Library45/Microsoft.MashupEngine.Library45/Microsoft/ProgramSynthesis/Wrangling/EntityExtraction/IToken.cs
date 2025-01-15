using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction
{
	// Token: 0x020001A1 RID: 417
	public interface IToken<TSequenceable, out TSequence> where TSequence : IEnumerable<TSequenceable>
	{
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600091A RID: 2330
		TSequence SourceSequence { get; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600091B RID: 2331
		int StartInSequence { get; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600091C RID: 2332
		int EndInSequence { get; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600091D RID: 2333
		TSequence TokenSubSequence { get; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x0600091E RID: 2334
		string SourceString { get; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x0600091F RID: 2335
		int Start { get; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000920 RID: 2336
		int End { get; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000921 RID: 2337
		string Value { get; }
	}
}
