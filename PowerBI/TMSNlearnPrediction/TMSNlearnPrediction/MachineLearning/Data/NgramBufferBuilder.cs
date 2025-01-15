using System;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000215 RID: 533
	internal sealed class NgramBufferBuilder
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x000407D2 File Offset: 0x0003E9D2
		public bool IsEmpty
		{
			get
			{
				return this._slotLim == 0;
			}
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x000407E0 File Offset: 0x0003E9E0
		public NgramBufferBuilder(int ngramLength, int skipLength, int slotLim, NgramIdFinder finder)
		{
			this._ngramLength = ngramLength;
			this._skipLength = skipLength;
			this._slotLim = slotLim;
			this._ngram = new uint[this._ngramLength];
			this._queue = new FixedSizeQueue<uint>(this._ngramLength + this._skipLength);
			this._bldr = new FloatBufferBuilder();
			this._finder = finder;
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00040844 File Offset: 0x0003EA44
		public void Reset()
		{
			this._bldr.Reset(this._slotLim, false);
			this._queue.Clear();
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x00040864 File Offset: 0x0003EA64
		public bool AddNgrams(ref VBuffer<uint> src, int icol, uint keyMax)
		{
			if (src.IsDense)
			{
				for (int i = 0; i < src.Length; i++)
				{
					uint num = src.Values[i];
					if (num > keyMax)
					{
						num = 0U;
					}
					this._queue.AddLast(num);
					if (this._queue.IsFull && !this.ProcessNgrams(icol))
					{
						return false;
					}
				}
			}
			else
			{
				int capacity = this._queue.Capacity;
				int num2 = 0;
				for (int j = 0; j < src.Length; j++)
				{
					uint num;
					if (num2 < src.Count && j == src.Indices[num2])
					{
						num = src.Values[num2];
						if (num > keyMax)
						{
							num = 0U;
						}
						num2++;
					}
					else
					{
						num = 0U;
					}
					this._queue.AddLast(num);
					if (this._queue.IsFull && !this.ProcessNgrams(icol))
					{
						return false;
					}
				}
			}
			if (this._queue.IsFull)
			{
				this._queue.RemoveFirst();
			}
			while (this._queue.Count > 0)
			{
				if (!this.ProcessNgrams(icol))
				{
					return false;
				}
				this._queue.RemoveFirst();
			}
			return true;
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00040970 File Offset: 0x0003EB70
		public void GetResult(ref VBuffer<float> dst)
		{
			this._bldr.GetResult(ref dst);
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00040980 File Offset: 0x0003EB80
		private bool ProcessNgrams(int icol)
		{
			this._ngram[0] = this._queue[0];
			bool flag = true;
			int num;
			if ((num = this._finder(this._ngram, 1, icol, ref flag)) >= 0)
			{
				this._bldr.AddFeature(num, 1f);
			}
			if (this._queue.Count == 1 || !flag)
			{
				return flag;
			}
			if (this._skipLength > 0)
			{
				return this.ProcessSkipNgrams(icol, 1, 0);
			}
			for (int i = 1; i < this._queue.Count; i++)
			{
				this._ngram[i] = this._queue[i];
				if ((num = this._finder(this._ngram, i + 1, icol, ref flag)) >= 0)
				{
					this._bldr.AddFeature(num, 1f);
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00040A54 File Offset: 0x0003EC54
		private bool ProcessSkipNgrams(int icol, int i, int skips)
		{
			bool flag = true;
			int num = skips;
			while (num <= this._skipLength && num + i < this._queue.Count)
			{
				this._ngram[i] = this._queue[num + i];
				int num2;
				if ((num2 = this._finder(this._ngram, i + 1, icol, ref flag)) >= 0)
				{
					this._bldr.AddFeature(num2, 1f);
				}
				if (!flag || (i + 1 < this._ngramLength && i + num + 1 < this._queue.Count && !this.ProcessSkipNgrams(icol, i + 1, num)))
				{
					return false;
				}
				num++;
			}
			return flag;
		}

		// Token: 0x04000681 RID: 1665
		public const int MaxSkipNgramLength = 10;

		// Token: 0x04000682 RID: 1666
		private readonly FloatBufferBuilder _bldr;

		// Token: 0x04000683 RID: 1667
		private readonly FixedSizeQueue<uint> _queue;

		// Token: 0x04000684 RID: 1668
		private readonly int _ngramLength;

		// Token: 0x04000685 RID: 1669
		private readonly int _skipLength;

		// Token: 0x04000686 RID: 1670
		private readonly uint[] _ngram;

		// Token: 0x04000687 RID: 1671
		private readonly int _slotLim;

		// Token: 0x04000688 RID: 1672
		private readonly NgramIdFinder _finder;
	}
}
