using System;

namespace Microsoft.InfoNav
{
	// Token: 0x0200000A RID: 10
	internal struct BitVector : IEquatable<BitVector>, IComparable<BitVector>
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000022B3 File Offset: 0x000004B3
		internal BitVector(int size, bool defaultValue = false)
		{
			this = new BitVector(BitVector.CreateValues(size, defaultValue), size);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022C4 File Offset: 0x000004C4
		private BitVector(int[] values, int size)
		{
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			this._values = values;
			this._size = size;
			int num = this._size % 32;
			if (num != 0)
			{
				this._values[this._values.Length - 1] &= (1 << num) - 1;
			}
		}

		// Token: 0x17000005 RID: 5
		internal bool this[int index]
		{
			get
			{
				if (index < 0 || index >= this._size)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return (this._values[index / 32] & (1 << index % 32)) != 0;
			}
			set
			{
				if (index < 0 || index >= this._size)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				int num = 1 << index % 32;
				if (value)
				{
					this._values[index / 32] |= num;
					return;
				}
				this._values[index / 32] &= ~num;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023AC File Offset: 0x000005AC
		public static BitVector operator |(BitVector x, BitVector y)
		{
			int num = x._values.Length;
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = x._values[i] | y._values[i];
			}
			return new BitVector(array, x._size);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023F8 File Offset: 0x000005F8
		public static BitVector operator &(BitVector x, BitVector y)
		{
			int num = x._values.Length;
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = x._values[i] & y._values[i];
			}
			return new BitVector(array, x._size);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002444 File Offset: 0x00000644
		public static BitVector operator ^(BitVector x, BitVector y)
		{
			int num = x._values.Length;
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = x._values[i] ^ y._values[i];
			}
			return new BitVector(array, x._size);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002490 File Offset: 0x00000690
		public static BitVector operator ~(BitVector x)
		{
			int num = x._values.Length;
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = ~x._values[i];
			}
			return new BitVector(array, x._size);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024D1 File Offset: 0x000006D1
		public static bool operator ==(BitVector x, BitVector y)
		{
			return x.Equals(y);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024DB File Offset: 0x000006DB
		public static bool operator !=(BitVector x, BitVector y)
		{
			return !(x == y);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024E8 File Offset: 0x000006E8
		internal bool HasFlag(BitVector flag)
		{
			int num = this._values.Length;
			for (int i = 0; i < num; i++)
			{
				if ((this._values[i] & flag._values[i]) != flag._values[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002528 File Offset: 0x00000728
		public override bool Equals(object obj)
		{
			return obj is BitVector && this.Equals((BitVector)obj);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002540 File Offset: 0x00000740
		public bool Equals(BitVector other)
		{
			if (this._size != other._size)
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

		// Token: 0x0600001F RID: 31 RVA: 0x00002588 File Offset: 0x00000788
		public override int GetHashCode()
		{
			int num = this._values.Length - 1;
			if (num < 0)
			{
				return -1;
			}
			int num2 = this._values[num];
			for (int i = 0; i < num; i++)
			{
				num2 = Hashing.CombineHash(num2, this._values[i]);
			}
			return num2;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025CC File Offset: 0x000007CC
		public int CompareTo(BitVector other)
		{
			if (this._values.Length == 0)
			{
				return 0;
			}
			for (int i = this._values.Length - 1; i >= 0; i--)
			{
				uint num = (uint)this._values[i];
				int num2 = num.CompareTo((uint)other._values[i]);
				if (num2 != 0)
				{
					return num2;
				}
			}
			return 0;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002618 File Offset: 0x00000818
		private static int[] CreateValues(int size, bool defaultValue)
		{
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			int num = ((size == 0) ? 0 : ((size - 1) / 32 + 1));
			int[] array = new int[num];
			int num2 = (defaultValue ? (-1) : 0);
			for (int i = 0; i < num; i++)
			{
				array[i] = num2;
			}
			return array;
		}

		// Token: 0x04000033 RID: 51
		private const int BitsPerInt = 32;

		// Token: 0x04000034 RID: 52
		private readonly int _size;

		// Token: 0x04000035 RID: 53
		private readonly int[] _values;
	}
}
