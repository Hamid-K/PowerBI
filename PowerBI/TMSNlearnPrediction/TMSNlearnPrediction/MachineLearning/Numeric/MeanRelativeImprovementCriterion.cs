using System;
using System.Collections.Generic;

namespace Microsoft.MachineLearning.Numeric
{
	// Token: 0x02000453 RID: 1107
	public sealed class MeanRelativeImprovementCriterion : ITerminationCriterion
	{
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060016FA RID: 5882 RVA: 0x00085DBA File Offset: 0x00083FBA
		public float Tolerance
		{
			get
			{
				return this._tol;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060016FB RID: 5883 RVA: 0x00085DC2 File Offset: 0x00083FC2
		public int Iters
		{
			get
			{
				return this._n;
			}
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x00085DCA File Offset: 0x00083FCA
		public MeanRelativeImprovementCriterion(float tol = 0.0001f, int n = 5, int maxIterations = 2147483647)
		{
			this._tol = tol;
			this._n = n;
			this._maxIterations = maxIterations;
			this._pastValues = new Queue<float>(n);
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060016FD RID: 5885 RVA: 0x00085DF3 File Offset: 0x00083FF3
		public string FriendlyName
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x00085DFC File Offset: 0x00083FFC
		public bool Terminate(Optimizer.OptimizerState state, out string message)
		{
			float value = state.Value;
			if (this._pastValues.Count < this._n)
			{
				this._pastValues.Enqueue(value);
				message = "...";
				return false;
			}
			float num = (this._pastValues.Dequeue() - value) / (float)this._n;
			this._pastValues.Enqueue(value);
			float num2 = num / Math.Abs(value);
			message = string.Format("{0,0:0.0000e0}", num2);
			return num2 < this._tol || state.Iter >= this._maxIterations;
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x00085E91 File Offset: 0x00084091
		public override string ToString()
		{
			return string.Format("Mean rel impr over {0} iter'ns < tol: {1,0:0.000e0}", this._n, this._tol);
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x00085EB3 File Offset: 0x000840B3
		public void Reset()
		{
			this._pastValues.Clear();
		}

		// Token: 0x04000E12 RID: 3602
		private readonly int _n;

		// Token: 0x04000E13 RID: 3603
		private readonly float _tol;

		// Token: 0x04000E14 RID: 3604
		private readonly int _maxIterations;

		// Token: 0x04000E15 RID: 3605
		private Queue<float> _pastValues;
	}
}
