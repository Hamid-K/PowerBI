using System;

namespace System.Diagnostics.Metrics
{
	// Token: 0x0200005D RID: 93
	internal struct StringSequenceMany : IEquatable<StringSequenceMany>, IStringSequence
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x0000B10F File Offset: 0x0000930F
		public StringSequenceMany(string[] values)
		{
			this._values = values;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000B118 File Offset: 0x00009318
		public Span<string> AsSpan()
		{
			return MemoryExtensions.AsSpan<string>(this._values);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000B128 File Offset: 0x00009328
		public bool Equals(StringSequenceMany other)
		{
			if (this._values.Length != other._values.Length)
			{
				return false;
			}
			for (int i = 0; i < this._values.Length; i++)
			{
				if (this._values[i] != other._values[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000B178 File Offset: 0x00009378
		public override bool Equals(object obj)
		{
			if (obj is StringSequenceMany)
			{
				StringSequenceMany stringSequenceMany = (StringSequenceMany)obj;
				return this.Equals(stringSequenceMany);
			}
			return false;
		}

		// Token: 0x17000098 RID: 152
		public string this[int i]
		{
			get
			{
				return this._values[i];
			}
			set
			{
				this._values[i] = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000B1B2 File Offset: 0x000093B2
		public int Length
		{
			get
			{
				return this._values.Length;
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000B1BC File Offset: 0x000093BC
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < this._values.Length; i++)
			{
				num <<= 3;
				num ^= this._values[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x04000129 RID: 297
		private readonly string[] _values;
	}
}
