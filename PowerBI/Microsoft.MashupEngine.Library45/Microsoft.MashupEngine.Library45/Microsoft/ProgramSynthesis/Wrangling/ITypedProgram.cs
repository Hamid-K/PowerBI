using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000B7 RID: 183
	public interface ITypedProgram<in TInput, out TOutput>
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000422 RID: 1058
		IEnumerable<IType> OutputTypes { get; }
	}
}
