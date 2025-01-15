using System;
using System.Threading;
using Microsoft.ProgramSynthesis.Learning.Strategies;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Learning
{
	// Token: 0x020006BA RID: 1722
	public interface ISynthesisStrategy
	{
		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06002541 RID: 9537
		Type SpecType { get; }

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06002542 RID: 9538
		StrategyAttribute[] Attributes { get; }

		// Token: 0x06002543 RID: 9539
		void Initialize(SynthesisEngine engine);

		// Token: 0x06002544 RID: 9540
		Optional<ProgramSet> Learn(SynthesisEngine engine, LearningTask task, CancellationToken cancel);

		// Token: 0x06002545 RID: 9541
		bool CanCall(Spec spec);
	}
}
