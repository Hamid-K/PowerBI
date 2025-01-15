using System;
using Microsoft.MachineLearning.Data;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Numeric
{
	// Token: 0x02000439 RID: 1081
	public class AdaptiveSGDOptimizer
	{
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06001666 RID: 5734 RVA: 0x000833E9 File Offset: 0x000815E9
		// (set) Token: 0x06001667 RID: 5735 RVA: 0x000833F1 File Offset: 0x000815F1
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

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06001668 RID: 5736 RVA: 0x000833FA File Offset: 0x000815FA
		// (set) Token: 0x06001669 RID: 5737 RVA: 0x00083402 File Offset: 0x00081602
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

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600166A RID: 5738 RVA: 0x00083414 File Offset: 0x00081614
		// (set) Token: 0x0600166B RID: 5739 RVA: 0x0008341C File Offset: 0x0008161C
		public int MaxSteps
		{
			get
			{
				return this._maxSteps;
			}
			set
			{
				Contracts.Check(value > 0);
				this._maxSteps = value;
			}
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x0008342E File Offset: 0x0008162E
		public AdaptiveSGDOptimizer(DTerminate Terminate, int batchSize = 1, int maxSteps = 0)
		{
			this._Terminate = Terminate;
			this._batchSize = batchSize;
			this._maxSteps = maxSteps;
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x0008344B File Offset: 0x0008164B
		private void AddSqSum(int index, float otherVal, ref float myVal)
		{
			myVal += otherVal * otherVal;
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x00083455 File Offset: 0x00081655
		private void ScaleByInverseSqrt(int index, float otherVal, ref float myVal)
		{
			myVal /= 100f;
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x00083464 File Offset: 0x00081664
		public void Minimize(SGDOptimizer.DStochasticGradient F, ref VBuffer<float> initial, ref VBuffer<float> result)
		{
			int length = initial.Length;
			VBuffer<float> vbuffer = VBufferUtils.CreateEmpty<float>(length);
			VBuffer<float> vbuffer2 = VBufferUtils.CreateEmpty<float>(length);
			initial.CopyTo(ref result);
			VBuffer<float> vbuffer3 = VBufferUtils.CreateEmpty<float>(length);
			int num = 0;
			while (this._maxSteps == 0 || num < this._maxSteps)
			{
				if (this._batchSize == 1)
				{
					F(ref result, ref vbuffer2);
				}
				else
				{
					VectorUtils.ScaleBy(ref vbuffer2, 0f);
					for (int i = 0; i < this._batchSize; i++)
					{
						F(ref result, ref vbuffer);
						VectorUtils.Add(ref vbuffer, ref vbuffer2);
					}
				}
				VBufferUtils.ApplyWith<float, float>(ref vbuffer2, ref vbuffer3, new VBufferUtils.PairManipulator<float, float>(this.AddSqSum));
				VBufferUtils.ApplyWith<float, float>(ref vbuffer3, ref vbuffer2, new VBufferUtils.PairManipulator<float, float>(this.ScaleByInverseSqrt));
				VectorUtils.AddMult(ref vbuffer2, -1f, ref result);
				if (this.Terminate(ref result))
				{
					return;
				}
				num++;
			}
		}

		// Token: 0x04000DBF RID: 3519
		private DTerminate _Terminate;

		// Token: 0x04000DC0 RID: 3520
		private int _batchSize;

		// Token: 0x04000DC1 RID: 3521
		private int _maxSteps;
	}
}
