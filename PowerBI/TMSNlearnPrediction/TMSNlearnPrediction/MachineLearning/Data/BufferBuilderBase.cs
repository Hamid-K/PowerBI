using System;
using System.Diagnostics;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020002E0 RID: 736
	public abstract class BufferBuilderBase<T>
	{
		// Token: 0x060010B6 RID: 4278 RVA: 0x0005D515 File Offset: 0x0005B715
		protected BufferBuilderBase(Combiner<T> comb)
		{
			this._comb = comb;
			this._values = new T[8];
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0005D530 File Offset: 0x0005B730
		[Conditional("DEBUG")]
		public void AssertValid()
		{
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0005D534 File Offset: 0x0005B734
		protected void ResetImpl(int length, bool dense)
		{
			this._length = length;
			this._dense = false;
			this._sorted = true;
			this._count = 0;
			this._ifeatCur = 0;
			this._cfeatCur = 0;
			if (dense)
			{
				if (this._values.Length < this._length)
				{
					this._values = new T[this._length];
				}
				else
				{
					Array.Clear(this._values, 0, this._length);
				}
				this._dense = true;
				this._count = this._length;
				return;
			}
			if (this._indices == null)
			{
				this._indices = new int[8];
			}
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x0005D5CB File Offset: 0x0005B7CB
		protected void SetActiveRangeImpl(int ifeat, int cfeat)
		{
			this._ifeatCur = ifeat;
			this._cfeatCur = cfeat;
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x0005D5DC File Offset: 0x0005B7DC
		public void AddFeature(int index, T value)
		{
			if (this._comb.IsDefault(value))
			{
				return;
			}
			index += this._ifeatCur;
			if (this._dense)
			{
				this._comb.Combine(ref this._values[index], value);
				return;
			}
			if (!this._sorted)
			{
				if (this._count < this._length)
				{
					checked
					{
						if (this._values.Length <= this._count)
						{
							Array.Resize<T>(ref this._values, Math.Max(Math.Min(this._length, this._count * 2), 8));
						}
						if (this._indices.Length <= this._count)
						{
							Array.Resize<int>(ref this._indices, Math.Max(Math.Min(this._length, this._count * 2), 8));
						}
						this._values[this._count] = value;
						this._indices[this._count] = index;
					}
					this._count++;
					return;
				}
				this.SortAndSumDups();
				if (this._dense)
				{
					this._comb.Combine(ref this._values[index], value);
					return;
				}
			}
			if (this._count >= this._length / 2 - 1)
			{
				this.MakeDense();
				this._comb.Combine(ref this._values[index], value);
				return;
			}
			checked
			{
				if (this._values.Length <= this._count)
				{
					Array.Resize<T>(ref this._values, Math.Max(Math.Min(this._length, this._count * 2), 8));
				}
				if (this._indices.Length <= this._count)
				{
					Array.Resize<int>(ref this._indices, Math.Max(Math.Min(this._length, this._count * 2), 8));
				}
			}
			if (this._count >= 20 && this._indices[this._count - 20] > index)
			{
				this._values[this._count] = value;
				this._indices[this._count] = index;
				this._count++;
				this._sorted = false;
				return;
			}
			int num = this._count;
			while (--num >= 0)
			{
				int num2 = this._indices[num] - index;
				if (num2 <= 0)
				{
					if (num2 >= 0)
					{
						this._comb.Combine(ref this._values[num], value);
						return;
					}
					break;
				}
			}
			num++;
			int num3 = this._count;
			while (--num3 >= num)
			{
				this._indices[num3 + 1] = this._indices[num3];
				this._values[num3 + 1] = this._values[num3];
			}
			this._indices[num] = index;
			this._values[num] = value;
			this._count++;
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0005D88C File Offset: 0x0005BA8C
		protected void SortAndSumDups()
		{
			Array.Sort<int, T>(this._indices, this._values, 0, this._count);
			int i = 0;
			int num = 0;
			while (i < this._count)
			{
				if (!this._comb.IsDefault(this._values[i]))
				{
					this._values[num] = this._values[i];
					this._indices[num++] = this._indices[i];
					while (++i < this._count)
					{
						if (this._indices[num - 1] == this._indices[i])
						{
							this._comb.Combine(ref this._values[num - 1], this._values[i]);
						}
						else
						{
							if (num < i)
							{
								this._indices[num] = this._indices[i];
								this._values[num] = this._values[i];
							}
							num++;
						}
					}
					this._count = num;
					this._sorted = true;
					if (this._count >= this._length / 2)
					{
						this.MakeDense();
					}
					return;
				}
			}
			this._count = 0;
			this._sorted = true;
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0005D9B0 File Offset: 0x0005BBB0
		protected void MakeDense()
		{
			if (this._values.Length < this._length)
			{
				Array.Resize<T>(ref this._values, this._length);
			}
			int num = this._length;
			int num2 = this._count;
			while (--num2 >= 0)
			{
				int num3 = this._indices[num2];
				while (--num > num3)
				{
					this._values[num] = default(T);
				}
				this._values[num] = this._values[num2];
			}
			while (--num >= 0)
			{
				this._values[num] = default(T);
			}
			this._dense = true;
			this._count = this._length;
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x0005DA6C File Offset: 0x0005BC6C
		public bool TryGetFeature(int index, out T v)
		{
			int num = index + this._ifeatCur;
			if (this._dense)
			{
				v = this._values[num];
				return true;
			}
			if (!this._sorted)
			{
				this.SortAndSumDups();
				if (this._dense)
				{
					v = this._values[num];
					return true;
				}
			}
			int num2 = Utils.FindIndexSorted(this._indices, 0, this._count, num);
			if (num2 < this._count && num == this._indices[num2])
			{
				v = this._values[num2];
				return true;
			}
			v = default(T);
			return false;
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0005DB0C File Offset: 0x0005BD0C
		protected void GetResult(ref T[] values, ref int[] indices, out int count, out int length)
		{
			if (this._count == 0)
			{
				count = 0;
				length = this._length;
				return;
			}
			if (!this._dense)
			{
				if (!this._sorted)
				{
					this.SortAndSumDups();
				}
				if (!this._dense && this._count >= this._length / 2)
				{
					this.MakeDense();
				}
			}
			if (this._dense)
			{
				if (Utils.Size<T>(values) < this._length)
				{
					values = new T[this._length];
				}
				Array.Copy(this._values, values, this._length);
				count = this._length;
				length = this._length;
				return;
			}
			if (Utils.Size<T>(values) < this._count)
			{
				values = new T[this._count];
			}
			if (Utils.Size<int>(indices) < this._count)
			{
				indices = new int[this._count];
			}
			Array.Copy(this._values, values, this._count);
			Array.Copy(this._indices, indices, this._count);
			count = this._count;
			length = this._length;
		}

		// Token: 0x04000978 RID: 2424
		private const int InsertThreshold = 20;

		// Token: 0x04000979 RID: 2425
		protected readonly Combiner<T> _comb;

		// Token: 0x0400097A RID: 2426
		protected int _length;

		// Token: 0x0400097B RID: 2427
		protected bool _dense;

		// Token: 0x0400097C RID: 2428
		protected bool _sorted;

		// Token: 0x0400097D RID: 2429
		protected int _count;

		// Token: 0x0400097E RID: 2430
		protected int[] _indices;

		// Token: 0x0400097F RID: 2431
		protected T[] _values;

		// Token: 0x04000980 RID: 2432
		protected int _ifeatCur;

		// Token: 0x04000981 RID: 2433
		protected int _cfeatCur;
	}
}
