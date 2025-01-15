using System;
using System.Threading;
using Microsoft.MachineLearning.Data;

namespace Microsoft.MachineLearning.Numeric
{
	// Token: 0x0200043C RID: 1084
	public class DifferentiableFunctionAggregator
	{
		// Token: 0x06001678 RID: 5752 RVA: 0x00083548 File Offset: 0x00081748
		public DifferentiableFunctionAggregator(IndexedDifferentiableFunction Func, int dim, int maxIndex, int threads = 64)
		{
			this._Func = Func;
			this._dim = dim;
			this._maxIndex = maxIndex;
			if (threads > maxIndex)
			{
				threads = maxIndex;
			}
			if (threads > 64)
			{
				threads = 64;
			}
			this._threads = threads;
			this._tempGrads = new VBuffer<float>[threads];
			this._threadFinished = new AutoResetEvent[threads];
			for (int i = 0; i < threads; i++)
			{
				this._threadFinished[i] = new AutoResetEvent(false);
			}
			this._tempVals = new float[threads];
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x000835CC File Offset: 0x000817CC
		private void Eval(object chunkIndexObj)
		{
			int num = (int)chunkIndexObj;
			int num2 = this._maxIndex / this._threads;
			int num3 = num2 + 1;
			int num4 = this._maxIndex % this._threads;
			int num5;
			int num6;
			if (num < num4)
			{
				num5 = num3 * num;
				num6 = num5 + num3;
			}
			else
			{
				num5 = num3 * num4 + num2 * (num - num4);
				num6 = num5 + num2;
			}
			this._tempVals[num] = 0f;
			VectorUtils.ScaleBy(ref this._tempGrads[num], 0f);
			VBuffer<float> vbuffer = default(VBuffer<float>);
			for (int i = num5; i < num6; i++)
			{
				vbuffer = new VBuffer<float>(0, 0, vbuffer.Values, vbuffer.Indices);
				this._tempVals[num] += this._Func(i, ref this._input, ref vbuffer);
				if (this._tempGrads[num].Length == 0)
				{
					vbuffer.CopyTo(ref this._tempGrads[num]);
				}
				else
				{
					VectorUtils.Add(ref vbuffer, ref this._tempGrads[num]);
				}
			}
			this._threadFinished[num].Set();
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x000836FC File Offset: 0x000818FC
		public float Eval(ref VBuffer<float> input, ref VBuffer<float> gradient)
		{
			this._input = input;
			for (int i = 0; i < this._threads; i++)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.Eval), i);
			}
			WaitHandle.WaitAll(this._threadFinished);
			VectorUtils.ScaleBy(ref gradient, 0f);
			float num = 0f;
			for (int j = 0; j < this._threads; j++)
			{
				if (gradient.Length == 0)
				{
					this._tempGrads[j].CopyTo(ref gradient);
				}
				else
				{
					VectorUtils.Add(ref this._tempGrads[j], ref gradient);
				}
				num += this._tempVals[j];
			}
			return num;
		}

		// Token: 0x04000DC2 RID: 3522
		private readonly IndexedDifferentiableFunction _Func;

		// Token: 0x04000DC3 RID: 3523
		private readonly int _maxIndex;

		// Token: 0x04000DC4 RID: 3524
		private readonly int _threads;

		// Token: 0x04000DC5 RID: 3525
		private readonly int _dim;

		// Token: 0x04000DC6 RID: 3526
		private readonly VBuffer<float>[] _tempGrads;

		// Token: 0x04000DC7 RID: 3527
		private VBuffer<float> _input;

		// Token: 0x04000DC8 RID: 3528
		private readonly float[] _tempVals;

		// Token: 0x04000DC9 RID: 3529
		private readonly AutoResetEvent[] _threadFinished;
	}
}
