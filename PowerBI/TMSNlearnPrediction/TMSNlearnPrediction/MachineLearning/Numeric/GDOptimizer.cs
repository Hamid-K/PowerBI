using System;
using Microsoft.MachineLearning.Data;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Numeric
{
	// Token: 0x0200045A RID: 1114
	public class GDOptimizer
	{
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x00086305 File Offset: 0x00084505
		// (set) Token: 0x06001722 RID: 5922 RVA: 0x0008630D File Offset: 0x0008450D
		public IDiffLineSearch LineSearch
		{
			get
			{
				return this._lineSearch;
			}
			set
			{
				this._lineSearch = value;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x00086316 File Offset: 0x00084516
		// (set) Token: 0x06001724 RID: 5924 RVA: 0x0008631E File Offset: 0x0008451E
		public int MaxSteps
		{
			get
			{
				return this._maxSteps;
			}
			set
			{
				Contracts.Check(value >= 0);
				this._maxSteps = value;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x00086333 File Offset: 0x00084533
		// (set) Token: 0x06001726 RID: 5926 RVA: 0x0008633B File Offset: 0x0008453B
		public DTerminate Terminate
		{
			get
			{
				return this._Terminate;
			}
			set
			{
				this._Terminate = value;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x00086344 File Offset: 0x00084544
		// (set) Token: 0x06001728 RID: 5928 RVA: 0x0008634C File Offset: 0x0008454C
		public bool UseCG
		{
			get
			{
				return this._useCG;
			}
			set
			{
				this._useCG = value;
			}
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x00086358 File Offset: 0x00084558
		public GDOptimizer(DTerminate Terminate, IDiffLineSearch lineSearch = null, bool useCG = false, int maxSteps = 0)
		{
			this._Terminate = Terminate;
			if (this._lineSearch == null)
			{
				if (useCG)
				{
					this._lineSearch = new CubicInterpLineSearch(0.01f);
				}
				else
				{
					this._lineSearch = new BacktrackingLineSearch(0.0001f);
				}
			}
			else
			{
				this._lineSearch = lineSearch;
			}
			this._maxSteps = maxSteps;
			this._useCG = useCG;
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x000863B8 File Offset: 0x000845B8
		public void Minimize(DifferentiableFunction Function, ref VBuffer<float> initial, ref VBuffer<float> result)
		{
			Contracts.Check(FloatUtils.IsFinite(initial.Values, initial.Count), "The initial vector contains NaNs or infinite values.");
			GDOptimizer.LineFunc lineFunc = new GDOptimizer.LineFunc(Function, ref initial, this._useCG);
			VBuffer<float> vbuffer = default(VBuffer<float>);
			initial.CopyTo(ref vbuffer);
			int num = 0;
			while (this._maxSteps == 0 || num < this._maxSteps)
			{
				this._lineSearch.Minimize(new DiffFunc1D(lineFunc.Eval), lineFunc.Value, lineFunc.Deriv);
				VBuffer<float> newPoint = lineFunc.NewPoint;
				if ((num > 0 && TerminateTester.ShouldTerminate(ref newPoint, ref vbuffer)) || this._Terminate(ref newPoint))
				{
					break;
				}
				newPoint.CopyTo(ref vbuffer);
				lineFunc.ChangeDir();
				num++;
			}
			lineFunc.NewPoint.CopyTo(ref result);
		}

		// Token: 0x04000E25 RID: 3621
		private IDiffLineSearch _lineSearch;

		// Token: 0x04000E26 RID: 3622
		private int _maxSteps;

		// Token: 0x04000E27 RID: 3623
		private DTerminate _Terminate;

		// Token: 0x04000E28 RID: 3624
		private bool _useCG;

		// Token: 0x0200045B RID: 1115
		private class LineFunc
		{
			// Token: 0x17000233 RID: 563
			// (get) Token: 0x0600172B RID: 5931 RVA: 0x00086486 File Offset: 0x00084686
			public VBuffer<float> NewPoint
			{
				get
				{
					return this._newPoint;
				}
			}

			// Token: 0x17000234 RID: 564
			// (get) Token: 0x0600172C RID: 5932 RVA: 0x0008648E File Offset: 0x0008468E
			public float Value
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x17000235 RID: 565
			// (get) Token: 0x0600172D RID: 5933 RVA: 0x00086496 File Offset: 0x00084696
			public float Deriv
			{
				get
				{
					return VectorUtils.DotProduct(ref this._dir, ref this._grad);
				}
			}

			// Token: 0x0600172E RID: 5934 RVA: 0x000864AC File Offset: 0x000846AC
			public LineFunc(DifferentiableFunction Function, ref VBuffer<float> initial, bool useCG = false)
			{
				initial.CopyTo(ref this._point);
				this._Func = Function;
				this._value = this._Func(ref this._point, ref this._grad, null);
				VectorUtils.ScaleInto(ref this._grad, -1f, ref this._dir);
				this._useCG = useCG;
			}

			// Token: 0x0600172F RID: 5935 RVA: 0x00086510 File Offset: 0x00084710
			public float Eval(float step, out float deriv)
			{
				VectorUtils.AddMultInto(ref this._point, step, ref this._dir, ref this._newPoint);
				this._newValue = this._Func(ref this._newPoint, ref this._newGrad, null);
				deriv = VectorUtils.DotProduct(ref this._dir, ref this._newGrad);
				return this._newValue;
			}

			// Token: 0x06001730 RID: 5936 RVA: 0x0008656C File Offset: 0x0008476C
			public void ChangeDir()
			{
				if (this._useCG)
				{
					float num = VectorUtils.NormSquared(ref this._newGrad);
					float num2 = VectorUtils.DotProduct(ref this._newGrad, ref this._grad);
					float num3 = VectorUtils.NormSquared(ref this._grad);
					float num4 = (num - num2) / num3;
					float num5 = Math.Max(0f, num4);
					VectorUtils.ScaleBy(ref this._dir, num5);
					VectorUtils.AddMult(ref this._newGrad, -1f, ref this._dir);
				}
				else
				{
					VectorUtils.ScaleInto(ref this._newGrad, -1f, ref this._dir);
				}
				this._newPoint.CopyTo(ref this._point);
				this._newGrad.CopyTo(ref this._grad);
				this._value = this._newValue;
			}

			// Token: 0x04000E29 RID: 3625
			private bool _useCG;

			// Token: 0x04000E2A RID: 3626
			private VBuffer<float> _point;

			// Token: 0x04000E2B RID: 3627
			private VBuffer<float> _newPoint;

			// Token: 0x04000E2C RID: 3628
			private VBuffer<float> _grad;

			// Token: 0x04000E2D RID: 3629
			private VBuffer<float> _newGrad;

			// Token: 0x04000E2E RID: 3630
			private VBuffer<float> _dir;

			// Token: 0x04000E2F RID: 3631
			private float _value;

			// Token: 0x04000E30 RID: 3632
			private float _newValue;

			// Token: 0x04000E31 RID: 3633
			private DifferentiableFunction _Func;
		}
	}
}
