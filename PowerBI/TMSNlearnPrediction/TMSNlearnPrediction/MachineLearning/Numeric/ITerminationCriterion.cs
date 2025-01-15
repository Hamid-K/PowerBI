using System;

namespace Microsoft.MachineLearning.Numeric
{
	// Token: 0x0200044F RID: 1103
	public interface ITerminationCriterion
	{
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060016E9 RID: 5865
		string FriendlyName { get; }

		// Token: 0x060016EA RID: 5866
		bool Terminate(Optimizer.OptimizerState state, out string message);

		// Token: 0x060016EB RID: 5867
		void Reset();
	}
}
