using System;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001C4D RID: 7245
	public class ConcurrentDocumentEvaluatorFactory : LimitedDocumentEvaluatorFactory
	{
		// Token: 0x0600B4C6 RID: 46278 RVA: 0x0024A6FD File Offset: 0x002488FD
		public ConcurrentDocumentEvaluatorFactory(string identity, int concurrentEvaluations)
			: base(identity)
		{
			this.concurrentEvaluations = concurrentEvaluations;
		}

		// Token: 0x0600B4C7 RID: 46279 RVA: 0x0024A70D File Offset: 0x0024890D
		protected override bool ShouldEvaluateNextPending(IEvaluation evaluation)
		{
			return base.RunningCount < this.concurrentEvaluations;
		}

		// Token: 0x04005BE5 RID: 23525
		private readonly int concurrentEvaluations;
	}
}
