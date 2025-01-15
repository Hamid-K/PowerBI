using System;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Numeric
{
	// Token: 0x0200044B RID: 1099
	public class GoldenSectionSearch : ILineSearch, IDiffLineSearch
	{
		// Token: 0x060016CC RID: 5836 RVA: 0x000853D1 File Offset: 0x000835D1
		public GoldenSectionSearch(int maxNumSteps)
		{
			this._maxNumSteps = maxNumSteps;
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x00085401 File Offset: 0x00083601
		public GoldenSectionSearch(float minWindow)
		{
			this._minWindow = minWindow;
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060016CE RID: 5838 RVA: 0x00085431 File Offset: 0x00083631
		// (set) Token: 0x060016CF RID: 5839 RVA: 0x00085439 File Offset: 0x00083639
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

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060016D0 RID: 5840 RVA: 0x00085442 File Offset: 0x00083642
		// (set) Token: 0x060016D1 RID: 5841 RVA: 0x0008544A File Offset: 0x0008364A
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

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060016D2 RID: 5842 RVA: 0x00085453 File Offset: 0x00083653
		// (set) Token: 0x060016D3 RID: 5843 RVA: 0x0008545B File Offset: 0x0008365B
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

		// Token: 0x060016D4 RID: 5844 RVA: 0x00085464 File Offset: 0x00083664
		private static void Rotate<T>(ref T a, ref T b, ref T c)
		{
			T t = a;
			a = b;
			b = c;
			c = t;
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x00085497 File Offset: 0x00083697
		public float Minimize(DiffFunc1D F, float initVal, float initDeriv)
		{
			return this.Minimize(F);
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x000854BC File Offset: 0x000836BC
		public float Minimize(DiffFunc1D Func)
		{
			float d;
			return this.Minimize((float x) => Func(x, out d));
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x000854E8 File Offset: 0x000836E8
		public float Minimize(Func<float, float> Func)
		{
			this._step = this.FindMinimum(Func);
			return Math.Min(this._step, this._maxStep);
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x00085508 File Offset: 0x00083708
		private float FindMinimum(Func<float, float> Func)
		{
			GoldenSectionSearch.StepAndValue stepAndValue = new GoldenSectionSearch.StepAndValue(Func, this._step / GoldenSectionSearch.PHI);
			GoldenSectionSearch.StepAndValue stepAndValue2 = new GoldenSectionSearch.StepAndValue(Func, this._step);
			GoldenSectionSearch.StepAndValue stepAndValue3 = new GoldenSectionSearch.StepAndValue(Func);
			if (stepAndValue.Value < stepAndValue2.Value)
			{
				do
				{
					GoldenSectionSearch.Rotate<GoldenSectionSearch.StepAndValue>(ref stepAndValue3, ref stepAndValue2, ref stepAndValue);
					stepAndValue.Step = stepAndValue2.Step / GoldenSectionSearch.PHI;
				}
				while (stepAndValue.Value < stepAndValue2.Value);
			}
			else
			{
				stepAndValue3.Step = this._step * GoldenSectionSearch.PHI;
				while (stepAndValue3.Value < stepAndValue2.Value)
				{
					GoldenSectionSearch.Rotate<GoldenSectionSearch.StepAndValue>(ref stepAndValue, ref stepAndValue2, ref stepAndValue3);
					if (stepAndValue.Step >= this._maxStep)
					{
						return this._maxStep;
					}
					stepAndValue3.Step = stepAndValue2.Step * GoldenSectionSearch.PHI;
				}
			}
			float num = 1f - 1f / (GoldenSectionSearch.PHI * GoldenSectionSearch.PHI);
			int num2 = 0;
			if (num <= this.MinWindow || num2 == this.MaxNumSteps)
			{
				return stepAndValue2.Step;
			}
			GoldenSectionSearch.StepAndValue stepAndValue4 = new GoldenSectionSearch.StepAndValue(Func, stepAndValue.Step + (stepAndValue3.Step - stepAndValue.Step) / GoldenSectionSearch.PHI);
			while (stepAndValue2.Value != stepAndValue4.Value)
			{
				if (stepAndValue2.Value < stepAndValue4.Value)
				{
					GoldenSectionSearch.Rotate<GoldenSectionSearch.StepAndValue>(ref stepAndValue3, ref stepAndValue4, ref stepAndValue2);
					stepAndValue2.Step = stepAndValue3.Step - (stepAndValue3.Step - stepAndValue.Step) / GoldenSectionSearch.PHI;
				}
				else
				{
					GoldenSectionSearch.Rotate<GoldenSectionSearch.StepAndValue>(ref stepAndValue, ref stepAndValue2, ref stepAndValue4);
					if (stepAndValue.Step >= this._maxStep)
					{
						return this._maxStep;
					}
					stepAndValue4.Step = stepAndValue.Step + (stepAndValue3.Step - stepAndValue.Step) / GoldenSectionSearch.PHI;
				}
				num2++;
				num = (stepAndValue3.Step - stepAndValue.Step) / stepAndValue3.Step;
				if (num <= this.MinWindow || num2 >= this.MaxNumSteps)
				{
					if (stepAndValue2.Value < stepAndValue4.Value)
					{
						return stepAndValue2.Step;
					}
					return stepAndValue4.Step;
				}
			}
			return stepAndValue2.Step;
		}

		// Token: 0x04000DFC RID: 3580
		private float _step = 1f;

		// Token: 0x04000DFD RID: 3581
		private int _maxNumSteps = int.MaxValue;

		// Token: 0x04000DFE RID: 3582
		private float _minWindow;

		// Token: 0x04000DFF RID: 3583
		private float _maxStep = float.PositiveInfinity;

		// Token: 0x04000E00 RID: 3584
		private static float PHI = (1f + MathUtils.Sqrt(5f)) / 2f;

		// Token: 0x0200044C RID: 1100
		private class StepAndValue
		{
			// Token: 0x060016DA RID: 5850 RVA: 0x00085720 File Offset: 0x00083920
			public StepAndValue(Func<float, float> Func)
			{
				this._Func = Func;
				this._step = (this._value = float.NaN);
			}

			// Token: 0x060016DB RID: 5851 RVA: 0x0008574E File Offset: 0x0008394E
			public StepAndValue(Func<float, float> Func, float initStep)
				: this(Func)
			{
				this.Step = initStep;
			}

			// Token: 0x1700021B RID: 539
			// (get) Token: 0x060016DC RID: 5852 RVA: 0x0008575E File Offset: 0x0008395E
			// (set) Token: 0x060016DD RID: 5853 RVA: 0x00085766 File Offset: 0x00083966
			public float Step
			{
				get
				{
					return this._step;
				}
				set
				{
					this._step = value;
					this._value = this._Func(value);
				}
			}

			// Token: 0x1700021C RID: 540
			// (get) Token: 0x060016DE RID: 5854 RVA: 0x00085781 File Offset: 0x00083981
			public float Value
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x04000E01 RID: 3585
			private Func<float, float> _Func;

			// Token: 0x04000E02 RID: 3586
			private float _step;

			// Token: 0x04000E03 RID: 3587
			private float _value;
		}
	}
}
