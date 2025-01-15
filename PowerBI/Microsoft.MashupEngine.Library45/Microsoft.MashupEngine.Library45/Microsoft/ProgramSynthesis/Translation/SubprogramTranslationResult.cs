using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002F4 RID: 756
	public struct SubprogramTranslationResult
	{
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x0002EF20 File Offset: 0x0002D120
		public readonly IReadOnlyList<Record<string, IGeneratedFunction>> FunctionBindings { get; }

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x0002EF28 File Offset: 0x0002D128
		public readonly IReadOnlyList<SSAStep> SSASteps { get; }

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x0002EF30 File Offset: 0x0002D130
		public readonly SSAValue ComputedValue { get; }

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x0002EF38 File Offset: 0x0002D138
		public readonly ImmutableSortedSet<string> Imports { get; }

		// Token: 0x0600106A RID: 4202 RVA: 0x0002EF40 File Offset: 0x0002D140
		public SubprogramTranslationResult(IEnumerable<Record<string, IGeneratedFunction>> functionBindings, IEnumerable<SSAStep> ssaSteps, SSAValue computedValue, IEnumerable<string> imports = null)
		{
			this.FunctionBindings = functionBindings.ToList<Record<string, IGeneratedFunction>>();
			this.SSASteps = ssaSteps.ToList<SSAStep>();
			this.ComputedValue = computedValue;
			this.Imports = (imports ?? Enumerable.Empty<string>()).ToImmutableSortedSet<string>();
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0002EF77 File Offset: 0x0002D177
		public SubprogramTranslationResult(IEnumerable<SSAStep> ssaSteps, SSAValue computedValue, IEnumerable<string> imports = null)
		{
			this = new SubprogramTranslationResult(new Record<string, IGeneratedFunction>[0], ssaSteps, computedValue, imports);
		}
	}
}
