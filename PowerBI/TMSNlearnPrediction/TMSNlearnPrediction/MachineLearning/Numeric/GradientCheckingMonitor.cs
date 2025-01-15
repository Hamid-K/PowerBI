using System;
using Microsoft.MachineLearning.Data;

namespace Microsoft.MachineLearning.Numeric
{
	// Token: 0x02000450 RID: 1104
	public sealed class GradientCheckingMonitor : ITerminationCriterion
	{
		// Token: 0x060016EC RID: 5868 RVA: 0x00085BDC File Offset: 0x00083DDC
		public GradientCheckingMonitor(ITerminationCriterion termCrit, int gradientCheckInterval)
		{
			Contracts.CheckParam(gradientCheckInterval > 0, "gradientCheckInterval", "gradientCheckInterval must be positive.");
			this._termCrit = termCrit;
			this._gradCheckInterval = gradientCheckInterval;
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x00085C05 File Offset: 0x00083E05
		public string FriendlyName
		{
			get
			{
				return "Gradient Checking Monitor wrapping " + this._termCrit.FriendlyName;
			}
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x00085C1C File Offset: 0x00083E1C
		public bool Terminate(Optimizer.OptimizerState state, out string message)
		{
			bool flag = this._termCrit.Terminate(state, out message);
			if (flag || state.Iter % this._gradCheckInterval == 1)
			{
				message += string.Format("  GradCheck: {0,0:0.0000e0}", this.Check(state));
			}
			return flag;
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x00085C6C File Offset: 0x00083E6C
		private float Check(Optimizer.OptimizerState state)
		{
			Console.Error.Write("  Checking gradient...");
			Console.Error.Flush();
			VBuffer<float> x = state.X;
			VBuffer<float> lastDir = state.LastDir;
			float num = GradientTester.Test(state.Function, ref x, ref lastDir, true, ref this._newGrad, ref this._newX);
			for (int i = 0; i < "  Checking gradient...".Length; i++)
			{
				Console.Error.Write('\b');
			}
			return num;
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x00085CDE File Offset: 0x00083EDE
		public void Reset()
		{
			this._termCrit.Reset();
		}

		// Token: 0x04000E09 RID: 3593
		private const string _checkingMessage = "  Checking gradient...";

		// Token: 0x04000E0A RID: 3594
		private readonly ITerminationCriterion _termCrit;

		// Token: 0x04000E0B RID: 3595
		private readonly int _gradCheckInterval;

		// Token: 0x04000E0C RID: 3596
		private VBuffer<float> _newGrad;

		// Token: 0x04000E0D RID: 3597
		private VBuffer<float> _newX;
	}
}
