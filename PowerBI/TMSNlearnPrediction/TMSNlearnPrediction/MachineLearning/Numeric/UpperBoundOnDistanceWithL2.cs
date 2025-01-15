using System;
using Microsoft.MachineLearning.Data;

namespace Microsoft.MachineLearning.Numeric
{
	// Token: 0x02000454 RID: 1108
	public sealed class UpperBoundOnDistanceWithL2 : StaticTerminationCriterion
	{
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x00085EC0 File Offset: 0x000840C0
		public float Tolerance
		{
			get
			{
				return this._tol;
			}
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x00085EC8 File Offset: 0x000840C8
		public UpperBoundOnDistanceWithL2(float sigmaSq = 1f, float tol = 0.01f)
		{
			this._sigmaSq = sigmaSq;
			this._tol = tol;
			this._bestBoundOnMin = float.NegativeInfinity;
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x00085EE9 File Offset: 0x000840E9
		public override string FriendlyName
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x00085EF4 File Offset: 0x000840F4
		public override bool Terminate(Optimizer.OptimizerState state, out string message)
		{
			VBuffer<float> grad = state.Grad;
			float num = VectorUtils.NormSquared(ref grad);
			float value = state.Value;
			float num2 = value - 0.5f * this._sigmaSq * num;
			this._bestBoundOnMin = Math.Max(this._bestBoundOnMin, num2);
			float num3 = (value - this._bestBoundOnMin) / Math.Abs(value);
			message = string.Format("{0,0:0.0000e0}", num3);
			return num3 < this._tol;
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x00085F68 File Offset: 0x00084168
		public override string ToString()
		{
			return string.Format("UB rel dist from opt, σ² = {0,0:0.00e0}, tol = {1,0:0.00e0}", this._sigmaSq, this._tol);
		}

		// Token: 0x04000E16 RID: 3606
		private readonly float _sigmaSq;

		// Token: 0x04000E17 RID: 3607
		private readonly float _tol;

		// Token: 0x04000E18 RID: 3608
		private float _bestBoundOnMin;
	}
}
