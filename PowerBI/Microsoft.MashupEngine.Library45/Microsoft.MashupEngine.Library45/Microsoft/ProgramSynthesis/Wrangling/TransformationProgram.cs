using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000D6 RID: 214
	public abstract class TransformationProgram<TProgram, TInput, TOutput> : Program<TInput, TOutput> where TProgram : TransformationProgram<TProgram, TInput, TOutput>
	{
		// Token: 0x060004BB RID: 1211 RVA: 0x000105C7 File Offset: 0x0000E7C7
		protected TransformationProgram(ProgramNode programNode, double score, Func<ProgramNode, ProgramNode> programNormalizingFunc = null)
			: base(programNode, score, programNormalizingFunc)
		{
		}

		// Token: 0x060004BC RID: 1212
		public abstract override TOutput Run(TInput input);
	}
}
