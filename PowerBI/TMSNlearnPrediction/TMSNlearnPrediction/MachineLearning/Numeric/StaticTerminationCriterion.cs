using System;

namespace Microsoft.MachineLearning.Numeric
{
	// Token: 0x02000451 RID: 1105
	public abstract class StaticTerminationCriterion : ITerminationCriterion
	{
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060016F1 RID: 5873
		public abstract string FriendlyName { get; }

		// Token: 0x060016F2 RID: 5874
		public abstract bool Terminate(Optimizer.OptimizerState state, out string message);

		// Token: 0x060016F3 RID: 5875 RVA: 0x00085CEB File Offset: 0x00083EEB
		public void Reset()
		{
		}
	}
}
