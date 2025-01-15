using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001298 RID: 4760
	public abstract class BufferedListValue : ListValue
	{
		// Token: 0x17002201 RID: 8705
		// (get) Token: 0x06007D10 RID: 32016 RVA: 0x00002139 File Offset: 0x00000339
		public sealed override bool IsBuffered
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002202 RID: 8706
		// (get) Token: 0x06007D11 RID: 32017 RVA: 0x001AD193 File Offset: 0x001AB393
		public sealed override long LargeCount
		{
			get
			{
				return (long)this.Count;
			}
		}

		// Token: 0x06007D12 RID: 32018 RVA: 0x001AD19C File Offset: 0x001AB39C
		public sealed override IEnumerator<IValueReference> GetEnumerator()
		{
			return new BufferedListValue.BufferedEnumerator(this);
		}

		// Token: 0x06007D13 RID: 32019 RVA: 0x001AD1A4 File Offset: 0x001AB3A4
		public override IValueReference GetReference(int index)
		{
			return new BufferedListValue.BufferedValueReference(this, index);
		}

		// Token: 0x06007D14 RID: 32020 RVA: 0x001AD1B0 File Offset: 0x001AB3B0
		public override bool TryGetValue(Value indexValue, out Value value)
		{
			int asInteger = indexValue.AsInteger32;
			if (asInteger < 0)
			{
				throw ValueException.StructureIndexCannotBeNegative(asInteger, this);
			}
			if (asInteger < this.Count)
			{
				value = this[asInteger];
				return true;
			}
			value = Value.Null;
			return false;
		}

		// Token: 0x02001299 RID: 4761
		private class BufferedEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
		{
			// Token: 0x06007D16 RID: 32022 RVA: 0x001AD1F4 File Offset: 0x001AB3F4
			public BufferedEnumerator(BufferedListValue list)
			{
				this.list = list;
				this.index = -1;
			}

			// Token: 0x17002203 RID: 8707
			// (get) Token: 0x06007D17 RID: 32023 RVA: 0x001AD20A File Offset: 0x001AB40A
			public IValueReference Current
			{
				get
				{
					if (this.current == null)
					{
						this.current = this.list.GetReference(this.index);
					}
					return this.current;
				}
			}

			// Token: 0x06007D18 RID: 32024 RVA: 0x001AD234 File Offset: 0x001AB434
			public bool MoveNext()
			{
				this.current = null;
				int num = this.index + 1;
				if (num >= this.list.Count)
				{
					return false;
				}
				this.index = num;
				return true;
			}

			// Token: 0x17002204 RID: 8708
			// (get) Token: 0x06007D19 RID: 32025 RVA: 0x001AD269 File Offset: 0x001AB469
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06007D1A RID: 32026 RVA: 0x0000EE09 File Offset: 0x0000D009
			public void Reset()
			{
				throw new InvalidOperationException();
			}

			// Token: 0x06007D1B RID: 32027 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x040044EB RID: 17643
			private BufferedListValue list;

			// Token: 0x040044EC RID: 17644
			private int index;

			// Token: 0x040044ED RID: 17645
			private IValueReference current;
		}

		// Token: 0x0200129A RID: 4762
		private class BufferedValueReference : IValueReference
		{
			// Token: 0x06007D1C RID: 32028 RVA: 0x001AD271 File Offset: 0x001AB471
			public BufferedValueReference(ListValue array, int index)
			{
				this.list = array;
				this.index = index;
			}

			// Token: 0x17002205 RID: 8709
			// (get) Token: 0x06007D1D RID: 32029 RVA: 0x001AD287 File Offset: 0x001AB487
			public bool Evaluated
			{
				get
				{
					return this.value != null;
				}
			}

			// Token: 0x17002206 RID: 8710
			// (get) Token: 0x06007D1E RID: 32030 RVA: 0x001AD292 File Offset: 0x001AB492
			public Value Value
			{
				get
				{
					if (this.value == null)
					{
						this.value = this.list[this.index];
						this.list = null;
					}
					return this.value;
				}
			}

			// Token: 0x040044EE RID: 17646
			private ListValue list;

			// Token: 0x040044EF RID: 17647
			private int index;

			// Token: 0x040044F0 RID: 17648
			private Value value;
		}
	}
}
