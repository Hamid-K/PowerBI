using System;
using System.Text;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001296 RID: 4758
	public struct BitArray
	{
		// Token: 0x06007CFC RID: 31996 RVA: 0x001ACD96 File Offset: 0x001AAF96
		public BitArray(int length)
		{
			this.length = length;
			this.bits = ((length > 32) ? new BitArray.Bits(length) : null);
			this.value = 0U;
		}

		// Token: 0x06007CFD RID: 31997 RVA: 0x001ACDBA File Offset: 0x001AAFBA
		private BitArray(int length, uint value, BitArray.Bits bits)
		{
			this.length = length;
			this.value = value;
			this.bits = bits;
		}

		// Token: 0x170021FC RID: 8700
		// (get) Token: 0x06007CFE RID: 31998 RVA: 0x001ACDD1 File Offset: 0x001AAFD1
		public bool Empty
		{
			get
			{
				if (this.bits == null)
				{
					return this.value == 0U;
				}
				return this.bits.Empty;
			}
		}

		// Token: 0x06007CFF RID: 31999 RVA: 0x001ACDF0 File Offset: 0x001AAFF0
		public override bool Equals(object obj)
		{
			if (!(obj is BitArray))
			{
				return false;
			}
			BitArray bitArray = (BitArray)obj;
			if (this.length != bitArray.length)
			{
				return false;
			}
			if (this.length <= 32)
			{
				return this.value == bitArray.value;
			}
			return this.bits == bitArray.bits;
		}

		// Token: 0x06007D00 RID: 32000 RVA: 0x001ACE45 File Offset: 0x001AB045
		public override int GetHashCode()
		{
			if (this.length <= 32)
			{
				return this.length ^ (int)this.value;
			}
			return this.length ^ this.bits.GetHashCode();
		}

		// Token: 0x170021FD RID: 8701
		// (get) Token: 0x06007D01 RID: 32001 RVA: 0x001ACE71 File Offset: 0x001AB071
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x06007D02 RID: 32002 RVA: 0x001ACE79 File Offset: 0x001AB079
		public BitArray Clone()
		{
			if (this.bits == null)
			{
				return new BitArray(this.length, this.value, null);
			}
			return new BitArray(this.length, this.value, this.bits.Clone());
		}

		// Token: 0x170021FE RID: 8702
		public bool this[int index]
		{
			get
			{
				if (this.bits == null)
				{
					return (this.value & (1U << index)) > 0U;
				}
				return this.bits[index];
			}
			set
			{
				if (this.bits != null)
				{
					this.bits[index] = value;
					return;
				}
				if (value)
				{
					this.value |= 1U << index;
					return;
				}
				this.value &= ~(1U << index);
			}
		}

		// Token: 0x06007D05 RID: 32005 RVA: 0x001ACF2C File Offset: 0x001AB12C
		public BitArray And(BitArray other)
		{
			if (this.length != other.length)
			{
				throw new InvalidOperationException();
			}
			if (this.bits == null)
			{
				return new BitArray(this.length, this.value & other.value, null);
			}
			return new BitArray(this.length, 0U, this.bits.And(other.bits));
		}

		// Token: 0x06007D06 RID: 32006 RVA: 0x001ACF8C File Offset: 0x001AB18C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("[", this.length + 2);
			for (int i = 0; i < this.length; i++)
			{
				stringBuilder.Append(this[i] ? '1' : '0');
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x040044E7 RID: 17639
		private int length;

		// Token: 0x040044E8 RID: 17640
		private uint value;

		// Token: 0x040044E9 RID: 17641
		private BitArray.Bits bits;

		// Token: 0x02001297 RID: 4759
		private class Bits
		{
			// Token: 0x06007D07 RID: 32007 RVA: 0x001ACFE2 File Offset: 0x001AB1E2
			public Bits(int length)
			{
				this.values = new uint[(length + 31) / 32];
			}

			// Token: 0x06007D08 RID: 32008 RVA: 0x001ACFFC File Offset: 0x001AB1FC
			public Bits(uint[] values)
			{
				this.values = values;
			}

			// Token: 0x06007D09 RID: 32009 RVA: 0x001AD00C File Offset: 0x001AB20C
			public BitArray.Bits Clone()
			{
				uint[] array = new uint[this.values.Length];
				Array.Copy(this.values, array, array.Length);
				return new BitArray.Bits(array);
			}

			// Token: 0x170021FF RID: 8703
			// (get) Token: 0x06007D0A RID: 32010 RVA: 0x001AD03C File Offset: 0x001AB23C
			public bool Empty
			{
				get
				{
					for (int i = 0; i < this.values.Length; i++)
					{
						if (this.values[i] != 0U)
						{
							return false;
						}
					}
					return true;
				}
			}

			// Token: 0x06007D0B RID: 32011 RVA: 0x001AD06C File Offset: 0x001AB26C
			public override bool Equals(object obj)
			{
				if (!(obj is BitArray.Bits))
				{
					return false;
				}
				BitArray.Bits bits = (BitArray.Bits)obj;
				if (this.values.Length != bits.values.Length)
				{
					return false;
				}
				for (int i = 0; i < this.values.Length; i++)
				{
					if (this.values[i] != bits.values[i])
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06007D0C RID: 32012 RVA: 0x001AD0C8 File Offset: 0x001AB2C8
			public override int GetHashCode()
			{
				uint num = 0U;
				for (int i = 0; i < this.values.Length; i++)
				{
					num ^= this.values[i];
				}
				return (int)num;
			}

			// Token: 0x17002200 RID: 8704
			public bool this[int index]
			{
				get
				{
					return (this.values[index / 32] & (1U << index % 32)) > 0U;
				}
				set
				{
					if (value)
					{
						this.values[index / 32] |= 1U << index % 32;
						return;
					}
					this.values[index / 32] &= ~(1U << index % 32);
				}
			}

			// Token: 0x06007D0F RID: 32015 RVA: 0x001AD150 File Offset: 0x001AB350
			public BitArray.Bits And(BitArray.Bits other)
			{
				uint[] array = new uint[this.values.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.values[i] & other.values[i];
				}
				return new BitArray.Bits(array);
			}

			// Token: 0x040044EA RID: 17642
			private uint[] values;
		}
	}
}
