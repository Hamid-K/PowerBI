using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002E1 RID: 737
	public interface ISubprogramTranslator<THeaderModule, TModule, TProgram, TInput, TOutput> where THeaderModule : Module where TModule : Module where TProgram : Program<TInput, TOutput>
	{
		// Token: 0x06000FEB RID: 4075
		Optional<SubprogramTranslationResult> Translate(ProgramNode subprogram, OptimizeFor optimization, Translator<THeaderModule, TModule, TProgram, TInput, TOutput> caller);

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000FEC RID: 4076
		string Name { get; }
	}
}
