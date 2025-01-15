using System;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Numeric
{
	// Token: 0x02000452 RID: 1106
	public sealed class MeanImprovementCriterion : ITerminationCriterion
	{
		// Token: 0x060016F5 RID: 5877 RVA: 0x00085CF5 File Offset: 0x00083EF5
		public MeanImprovementCriterion(float tol = 0.0001f, float lambda = 0.5f, int maxIterations = 2147483647)
		{
			this._tol = tol;
			this._lambda = lambda;
			this._maxIterations = maxIterations;
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060016F6 RID: 5878 RVA: 0x00085D12 File Offset: 0x00083F12
		public float Tolerance
		{
			get
			{
				return this._tol;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x00085D1A File Offset: 0x00083F1A
		public string FriendlyName
		{
			get
			{
				return "Mean Improvement";
			}
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x00085D24 File Offset: 0x00083F24
		public bool Terminate(Optimizer.OptimizerState state, out string message)
		{
			this._unnormMeanImprovement = state.LastValue - state.Value + this._lambda * this._unnormMeanImprovement;
			float num = this._unnormMeanImprovement * (1f - this._lambda) / (1f - MathUtils.Pow(this._lambda, (float)state.Iter));
			message = string.Format("{0:0.000e0}", num);
			return num < this._tol || state.Iter >= this._maxIterations;
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x00085DAD File Offset: 0x00083FAD
		public void Reset()
		{
			this._unnormMeanImprovement = 0f;
		}

		// Token: 0x04000E0E RID: 3598
		private readonly float _tol;

		// Token: 0x04000E0F RID: 3599
		private readonly float _lambda;

		// Token: 0x04000E10 RID: 3600
		private readonly int _maxIterations;

		// Token: 0x04000E11 RID: 3601
		private float _unnormMeanImprovement;
	}
}
