using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002F5 RID: 757
	public abstract class SubprogramTranslator<THeaderModule, TModule, TProgram, TInput, TOutput> : ISubprogramTranslator<THeaderModule, TModule, TProgram, TInput, TOutput> where THeaderModule : Module where TModule : Module where TProgram : Program<TInput, TOutput>
	{
		// Token: 0x0600106C RID: 4204
		public abstract Optional<SubprogramTranslationResult> Translate(ProgramNode subprogram, OptimizeFor optimization, Translator<THeaderModule, TModule, TProgram, TInput, TOutput> caller);

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x0002EF88 File Offset: 0x0002D188
		public string Name { get; }

		// Token: 0x0600106E RID: 4206 RVA: 0x0002EF90 File Offset: 0x0002D190
		protected SubprogramTranslator(string name)
		{
			this.Name = name;
		}
	}
}
