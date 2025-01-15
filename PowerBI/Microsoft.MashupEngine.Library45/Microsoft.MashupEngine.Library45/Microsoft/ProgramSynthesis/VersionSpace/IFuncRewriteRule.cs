using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.VersionSpace
{
	// Token: 0x02000294 RID: 660
	public interface IFuncRewriteRule
	{
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000E58 RID: 3672
		ProgramNode Source { get; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000E59 RID: 3673
		Func<ProgramNode, IReadOnlyDictionary<Hole, ProgramNode>, ProgramNode> Target { get; }
	}
}
