using System;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000C1 RID: 193
	public abstract class SimpleProgramLoader<TProgram, TInput, TOutput> : SimpleSymbolProgramLoader<TProgram, TInput, TOutput> where TProgram : Program<TInput, TOutput>
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000459 RID: 1113
		protected abstract Grammar Grammar { get; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0000EA45 File Offset: 0x0000CC45
		protected sealed override Symbol StartSymbol
		{
			get
			{
				return this.Grammar.StartSymbol;
			}
		}
	}
}
