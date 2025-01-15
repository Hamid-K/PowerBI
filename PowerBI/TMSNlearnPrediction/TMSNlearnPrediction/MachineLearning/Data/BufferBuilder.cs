using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020002E1 RID: 737
	public abstract class BufferBuilder<T> : BufferBuilderBase<T>
	{
		// Token: 0x060010BF RID: 4287 RVA: 0x0005DC1A File Offset: 0x0005BE1A
		protected BufferBuilder(Combiner<T> comb)
			: base(comb)
		{
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x0005DC23 File Offset: 0x0005BE23
		public bool IsEmpty
		{
			get
			{
				return this._count == 0;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x0005DC2E File Offset: 0x0005BE2E
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0005DC36 File Offset: 0x0005BE36
		public void Reset(int length, bool dense)
		{
			base.ResetImpl(length, dense);
			base.SetActiveRangeImpl(0, length);
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x0005DC48 File Offset: 0x0005BE48
		public void AddFeatures(int index, ref VBuffer<T> buffer)
		{
			Contracts.Check(0 <= index && index <= this._length - buffer.Length);
			int count = buffer.Count;
			if (count == 0)
			{
				return;
			}
			T[] values = buffer.Values;
			if (buffer.IsDense)
			{
				if (this._dense)
				{
					for (int i = 0; i < count; i++)
					{
						this._comb.Combine(ref this._values[index + i], values[i]);
					}
					return;
				}
				for (int j = 0; j < count; j++)
				{
					base.AddFeature(index + j, values[j]);
				}
				return;
			}
			else
			{
				int[] indices = buffer.Indices;
				if (this._dense)
				{
					for (int k = 0; k < count; k++)
					{
						this._comb.Combine(ref this._values[index + indices[k]], values[k]);
					}
					return;
				}
				for (int l = 0; l < count; l++)
				{
					base.AddFeature(index + indices[l], values[l]);
				}
				return;
			}
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0005DD4C File Offset: 0x0005BF4C
		public void GetResult(ref VBuffer<T> buffer)
		{
			T[] values = buffer.Values;
			int[] indices = buffer.Indices;
			if (this.IsEmpty)
			{
				buffer = new VBuffer<T>(this._length, 0, values, indices);
				return;
			}
			int num;
			int num2;
			base.GetResult(ref values, ref indices, out num, out num2);
			if (num == num2)
			{
				buffer = new VBuffer<T>(num2, values, indices);
				return;
			}
			buffer = new VBuffer<T>(num2, num, values, indices);
		}
	}
}
