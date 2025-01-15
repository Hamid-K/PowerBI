using System;
using Microsoft.MachineLearning.Data;

namespace Microsoft.MachineLearning.Numeric
{
	// Token: 0x02000455 RID: 1109
	public sealed class RelativeNormGradient : StaticTerminationCriterion
	{
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x00085F8A File Offset: 0x0008418A
		public float Tolerance
		{
			get
			{
				return this._tol;
			}
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x00085F92 File Offset: 0x00084192
		public RelativeNormGradient(float tol = 0.0001f)
		{
			this._tol = tol;
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06001708 RID: 5896 RVA: 0x00085FA1 File Offset: 0x000841A1
		public override string FriendlyName
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x00085FAC File Offset: 0x000841AC
		public override bool Terminate(Optimizer.OptimizerState state, out string message)
		{
			VBuffer<float> grad = state.Grad;
			float num = VectorUtils.Norm(ref grad);
			float num2 = num / Math.Abs(state.Value);
			message = string.Format("{0,0:0.0000e0}", num2);
			return num2 < this._tol;
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x00085FF1 File Offset: 0x000841F1
		public override string ToString()
		{
			return string.Format("Norm of grad / value < {0,0:0.00e0}", this._tol);
		}

		// Token: 0x04000E19 RID: 3609
		private readonly float _tol;
	}
}
