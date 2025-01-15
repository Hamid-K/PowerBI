using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Learning.Strategies
{
	// Token: 0x020006F0 RID: 1776
	public abstract class DomainGuidedCBSLearningLogic : DomainLearningLogic
	{
		// Token: 0x06002696 RID: 9878 RVA: 0x0006CC62 File Offset: 0x0006AE62
		public DomainGuidedCBSLearningLogic(Grammar grammar, DomainGuidedCBS.Config config)
			: base(grammar)
		{
			this.Config = config;
			this.LearnedPrograms = null;
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06002697 RID: 9879 RVA: 0x0006CC79 File Offset: 0x0006AE79
		public DomainGuidedCBS.Config Config { get; }

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06002698 RID: 9880 RVA: 0x0006CC81 File Offset: 0x0006AE81
		// (set) Token: 0x06002699 RID: 9881 RVA: 0x0006CC89 File Offset: 0x0006AE89
		public Dictionary<Symbol, Dictionary<object[], LearnerState>> LearnedPrograms { get; set; }

		// Token: 0x0600269A RID: 9882
		public abstract object[][][] Ranker(object[][] rankingStates, object[][] nonRankingStates, object[][] inputStates);
	}
}
