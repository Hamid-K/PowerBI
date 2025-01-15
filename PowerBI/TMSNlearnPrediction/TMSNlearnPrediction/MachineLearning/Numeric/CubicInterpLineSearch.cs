using System;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Numeric
{
	// Token: 0x02000449 RID: 1097
	public class CubicInterpLineSearch : IDiffLineSearch
	{
		// Token: 0x060016B9 RID: 5817 RVA: 0x0008504E File Offset: 0x0008324E
		public CubicInterpLineSearch(int maxNumSteps)
		{
			this._maxNumSteps = maxNumSteps;
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x0008507E File Offset: 0x0008327E
		public CubicInterpLineSearch(float minWindow)
		{
			this._minWindow = minWindow;
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060016BB RID: 5819 RVA: 0x000850AE File Offset: 0x000832AE
		// (set) Token: 0x060016BC RID: 5820 RVA: 0x000850B6 File Offset: 0x000832B6
		public int MaxNumSteps
		{
			get
			{
				return this._maxNumSteps;
			}
			set
			{
				this._maxNumSteps = value;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x000850BF File Offset: 0x000832BF
		// (set) Token: 0x060016BE RID: 5822 RVA: 0x000850C7 File Offset: 0x000832C7
		public float MinWindow
		{
			get
			{
				return this._minWindow;
			}
			set
			{
				this._minWindow = value;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060016BF RID: 5823 RVA: 0x000850D0 File Offset: 0x000832D0
		// (set) Token: 0x060016C0 RID: 5824 RVA: 0x000850D8 File Offset: 0x000832D8
		public float MaxStep
		{
			get
			{
				return this._maxStep;
			}
			set
			{
				this._maxStep = value;
			}
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x000850E4 File Offset: 0x000832E4
		private static float CubicInterp(CubicInterpLineSearch.StepValueDeriv a, CubicInterpLineSearch.StepValueDeriv b)
		{
			float num = a.Deriv + b.Deriv - 3f * (a.Value - b.Value) / (a.Step - b.Step);
			float num2 = (float)Math.Sign(b.Step - a.Step) * MathUtils.Sqrt(num * num - a.Deriv * b.Deriv);
			float num3 = b.Deriv + num2 - num;
			float num4 = b.Deriv - a.Deriv + 2f * num2;
			return b.Step - (b.Step - a.Step) * num3 / num4;
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x00085188 File Offset: 0x00083388
		private static void Swap<T>(ref T a, ref T b)
		{
			T t = a;
			a = b;
			b = t;
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x000851AF File Offset: 0x000833AF
		public float Minimize(DiffFunc1D Func, float initValue, float initDeriv)
		{
			this._step = this.FindMinimum(Func, initValue, initDeriv);
			return Math.Min(this._step, this._maxStep);
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x000851D4 File Offset: 0x000833D4
		private float FindMinimum(DiffFunc1D Func, float initValue, float initDeriv)
		{
			Contracts.CheckParam(initDeriv < 0f, "initDeriv", "Cannot search in direction of ascent!");
			CubicInterpLineSearch.StepValueDeriv stepValueDeriv = new CubicInterpLineSearch.StepValueDeriv(Func, 0f, initValue, initDeriv);
			CubicInterpLineSearch.StepValueDeriv stepValueDeriv2 = new CubicInterpLineSearch.StepValueDeriv(Func, this._step);
			while (stepValueDeriv2.Deriv < 0f)
			{
				CubicInterpLineSearch.Swap<CubicInterpLineSearch.StepValueDeriv>(ref stepValueDeriv, ref stepValueDeriv2);
				if (stepValueDeriv.Step >= this._maxStep)
				{
					return this._maxStep;
				}
				stepValueDeriv2.Step = stepValueDeriv.Step * 2f;
			}
			float num = 1f;
			CubicInterpLineSearch.StepValueDeriv stepValueDeriv3 = new CubicInterpLineSearch.StepValueDeriv(Func);
			int num2 = 1;
			float num3;
			for (;;)
			{
				num3 = CubicInterpLineSearch.CubicInterp(stepValueDeriv, stepValueDeriv2);
				if (num <= this.MinWindow || num2 == this.MaxNumSteps)
				{
					break;
				}
				float num4 = this._minProgress * (stepValueDeriv2.Step - stepValueDeriv.Step);
				float num5 = stepValueDeriv2.Step - num4;
				if (num3 > num5)
				{
					num3 = num5;
				}
				float num6 = stepValueDeriv.Step + num4;
				if (num3 < num6)
				{
					num3 = num6;
				}
				stepValueDeriv3.Step = num3;
				if (stepValueDeriv3.Deriv == 0f || stepValueDeriv3.Step == stepValueDeriv.Step || stepValueDeriv3.Step == stepValueDeriv2.Step)
				{
					goto IL_0111;
				}
				if (stepValueDeriv3.Deriv < 0f)
				{
					CubicInterpLineSearch.Swap<CubicInterpLineSearch.StepValueDeriv>(ref stepValueDeriv, ref stepValueDeriv3);
				}
				else
				{
					CubicInterpLineSearch.Swap<CubicInterpLineSearch.StepValueDeriv>(ref stepValueDeriv2, ref stepValueDeriv3);
				}
				if (stepValueDeriv.Step >= this._maxStep)
				{
					goto Block_9;
				}
				num = (stepValueDeriv2.Step - stepValueDeriv.Step) / stepValueDeriv2.Step;
				num2++;
			}
			return num3;
			IL_0111:
			return stepValueDeriv3.Step;
			Block_9:
			return this._maxStep;
		}

		// Token: 0x04000DF3 RID: 3571
		private float _step = 1f;

		// Token: 0x04000DF4 RID: 3572
		private int _maxNumSteps;

		// Token: 0x04000DF5 RID: 3573
		private float _minWindow;

		// Token: 0x04000DF6 RID: 3574
		private float _minProgress = 0.01f;

		// Token: 0x04000DF7 RID: 3575
		private float _maxStep = float.PositiveInfinity;

		// Token: 0x0200044A RID: 1098
		private class StepValueDeriv
		{
			// Token: 0x060016C5 RID: 5829 RVA: 0x0008534E File Offset: 0x0008354E
			public StepValueDeriv(DiffFunc1D Func)
			{
				this._Func = Func;
			}

			// Token: 0x060016C6 RID: 5830 RVA: 0x0008535D File Offset: 0x0008355D
			public StepValueDeriv(DiffFunc1D Func, float initStep)
			{
				this._Func = Func;
				this.Step = initStep;
			}

			// Token: 0x060016C7 RID: 5831 RVA: 0x00085373 File Offset: 0x00083573
			public StepValueDeriv(DiffFunc1D Func, float initStep, float initVal, float initDeriv)
			{
				this._Func = Func;
				this._step = initStep;
				this._value = initVal;
				this._deriv = initDeriv;
			}

			// Token: 0x17000215 RID: 533
			// (get) Token: 0x060016C8 RID: 5832 RVA: 0x00085398 File Offset: 0x00083598
			// (set) Token: 0x060016C9 RID: 5833 RVA: 0x000853A0 File Offset: 0x000835A0
			public float Step
			{
				get
				{
					return this._step;
				}
				set
				{
					this._step = value;
					this._value = this._Func(value, out this._deriv);
				}
			}

			// Token: 0x17000216 RID: 534
			// (get) Token: 0x060016CA RID: 5834 RVA: 0x000853C1 File Offset: 0x000835C1
			public float Value
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x17000217 RID: 535
			// (get) Token: 0x060016CB RID: 5835 RVA: 0x000853C9 File Offset: 0x000835C9
			public float Deriv
			{
				get
				{
					return this._deriv;
				}
			}

			// Token: 0x04000DF8 RID: 3576
			private DiffFunc1D _Func;

			// Token: 0x04000DF9 RID: 3577
			private float _step;

			// Token: 0x04000DFA RID: 3578
			private float _value;

			// Token: 0x04000DFB RID: 3579
			private float _deriv;
		}
	}
}
