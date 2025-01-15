using System;
using Microsoft.MachineLearning.Data;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Numeric
{
	// Token: 0x02000457 RID: 1111
	public class SGDOptimizer
	{
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x00086008 File Offset: 0x00084208
		// (set) Token: 0x06001710 RID: 5904 RVA: 0x00086010 File Offset: 0x00084210
		public int BatchSize
		{
			get
			{
				return this._batchSize;
			}
			set
			{
				Contracts.Check(value > 0);
				this._batchSize = value;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x00086022 File Offset: 0x00084222
		// (set) Token: 0x06001712 RID: 5906 RVA: 0x0008602A File Offset: 0x0008422A
		public float Momentum
		{
			get
			{
				return this._momentum;
			}
			set
			{
				Contracts.Check(0f <= value && value < 1f);
				this._momentum = value;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x0008604B File Offset: 0x0008424B
		// (set) Token: 0x06001714 RID: 5908 RVA: 0x00086053 File Offset: 0x00084253
		public float T0
		{
			get
			{
				return this._t0;
			}
			set
			{
				Contracts.Check(value >= 0f);
				this._t0 = value;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x0008606C File Offset: 0x0008426C
		// (set) Token: 0x06001716 RID: 5910 RVA: 0x00086074 File Offset: 0x00084274
		public bool Averaging
		{
			get
			{
				return this._averaging;
			}
			set
			{
				this._averaging = value;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06001717 RID: 5911 RVA: 0x0008607D File Offset: 0x0008427D
		// (set) Token: 0x06001718 RID: 5912 RVA: 0x00086085 File Offset: 0x00084285
		public SGDOptimizer.RateScheduleType RateSchedule
		{
			get
			{
				return this._rateSchedule;
			}
			set
			{
				this._rateSchedule = value;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06001719 RID: 5913 RVA: 0x0008608E File Offset: 0x0008428E
		// (set) Token: 0x0600171A RID: 5914 RVA: 0x00086096 File Offset: 0x00084296
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

		// Token: 0x0600171B RID: 5915 RVA: 0x000860AB File Offset: 0x000842AB
		public SGDOptimizer(DTerminate terminate, SGDOptimizer.RateScheduleType rateSchedule = SGDOptimizer.RateScheduleType.Sqrt, bool averaging = false, float t0 = 1f, int batchSize = 1, float momentum = 0f, int maxSteps = 0)
		{
			this._terminate = terminate;
			this._rateSchedule = rateSchedule;
			this._averaging = averaging;
			this._t0 = t0;
			this._batchSize = batchSize;
			this._momentum = momentum;
			this._maxSteps = maxSteps;
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x000860E8 File Offset: 0x000842E8
		public void Minimize(SGDOptimizer.DStochasticGradient f, ref VBuffer<float> initial, ref VBuffer<float> result)
		{
			Contracts.Check(FloatUtils.IsFinite(initial.Values, initial.Count), "The initial vector contains NaNs or infinite values.");
			int length = initial.Length;
			VBuffer<float> vbuffer = VBufferUtils.CreateEmpty<float>(length);
			VBuffer<float> vbuffer2 = VBufferUtils.CreateEmpty<float>(length);
			VBuffer<float> vbuffer3 = default(VBuffer<float>);
			initial.CopyTo(ref vbuffer3);
			VBuffer<float> vbuffer4 = default(VBuffer<float>);
			VBuffer<float> vbuffer5 = VBufferUtils.CreateEmpty<float>(length);
			int num = 0;
			while (this._maxSteps == 0 || num < this._maxSteps)
			{
				if (this._momentum == 0f)
				{
					vbuffer2 = new VBuffer<float>(vbuffer2.Length, 0, vbuffer2.Values, vbuffer2.Indices);
				}
				else
				{
					VectorUtils.ScaleBy(ref vbuffer2, this._momentum);
				}
				float num2;
				switch (this._rateSchedule)
				{
				case SGDOptimizer.RateScheduleType.Constant:
					num2 = 1f / this._t0;
					break;
				case SGDOptimizer.RateScheduleType.Sqrt:
					num2 = 1f / (this._t0 + MathUtils.Sqrt((float)num));
					break;
				case SGDOptimizer.RateScheduleType.Linear:
					num2 = 1f / (this._t0 + (float)num);
					break;
				default:
					throw Contracts.Except();
				}
				float num3 = (1f - this._momentum) / (float)this._batchSize;
				for (int i = 0; i < this._batchSize; i++)
				{
					f(ref vbuffer3, ref vbuffer);
					VectorUtils.AddMult(ref vbuffer, num3, ref vbuffer2);
				}
				if (this._averaging)
				{
					Utils.Swap<VBuffer<float>>(ref vbuffer5, ref vbuffer4);
					VectorUtils.ScaleBy(ref vbuffer4, ref vbuffer5, (float)num / (float)(num + 1));
					VectorUtils.AddMult(ref vbuffer2, -num2, ref vbuffer3);
					VectorUtils.AddMult(ref vbuffer3, 1f / (float)(num + 1), ref vbuffer5);
					if ((num > 0 && TerminateTester.ShouldTerminate(ref vbuffer5, ref vbuffer4)) || this._terminate(ref vbuffer5))
					{
						result = vbuffer5;
						return;
					}
				}
				else
				{
					Utils.Swap<VBuffer<float>>(ref vbuffer3, ref vbuffer4);
					VectorUtils.AddMult(ref vbuffer2, -num2, ref vbuffer4, ref vbuffer3);
					if ((num > 0 && TerminateTester.ShouldTerminate(ref vbuffer3, ref vbuffer4)) || this._terminate(ref vbuffer3))
					{
						result = vbuffer3;
						return;
					}
				}
				num++;
			}
			result = (this._averaging ? vbuffer5 : vbuffer3);
		}

		// Token: 0x04000E1A RID: 3610
		private int _batchSize;

		// Token: 0x04000E1B RID: 3611
		private float _momentum;

		// Token: 0x04000E1C RID: 3612
		private float _t0;

		// Token: 0x04000E1D RID: 3613
		private readonly DTerminate _terminate;

		// Token: 0x04000E1E RID: 3614
		private bool _averaging;

		// Token: 0x04000E1F RID: 3615
		private SGDOptimizer.RateScheduleType _rateSchedule;

		// Token: 0x04000E20 RID: 3616
		private int _maxSteps;

		// Token: 0x02000458 RID: 1112
		public enum RateScheduleType
		{
			// Token: 0x04000E22 RID: 3618
			Constant,
			// Token: 0x04000E23 RID: 3619
			Sqrt,
			// Token: 0x04000E24 RID: 3620
			Linear
		}

		// Token: 0x02000459 RID: 1113
		// (Invoke) Token: 0x0600171E RID: 5918
		public delegate void DStochasticGradient(ref VBuffer<float> x, ref VBuffer<float> grad);
	}
}
