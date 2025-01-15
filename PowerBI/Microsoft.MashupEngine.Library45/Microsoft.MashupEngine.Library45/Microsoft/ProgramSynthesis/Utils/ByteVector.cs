using System;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000400 RID: 1024
	public struct ByteVector
	{
		// Token: 0x06001732 RID: 5938 RVA: 0x0004692C File Offset: 0x00044B2C
		public ByteVector(string[][] tokens)
		{
			this = default(ByteVector);
			for (int i = 0; i < tokens.Length; i++)
			{
				this.Update(i, (byte)tokens[i].Length);
			}
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x0004695C File Offset: 0x00044B5C
		public void Update(int index, byte pos)
		{
			if (index < 0)
			{
				throw new IndexOutOfRangeException();
			}
			if (index < 8)
			{
				this._pos0 &= ~(255UL << index * 8);
				this._pos0 |= (ulong)pos << index * 8;
				return;
			}
			if (index < 16)
			{
				index -= 8;
				this._pos1 &= ~(255UL << index * 8);
				this._pos1 |= (ulong)pos << index * 8;
				return;
			}
			if (index < 24)
			{
				index -= 16;
				this._pos2 &= ~(255UL << index * 8);
				this._pos2 |= (ulong)pos << index * 8;
				return;
			}
			if (index < 32)
			{
				index -= 24;
				this._pos3 &= ~(255UL << index * 8);
				this._pos3 |= (ulong)pos << index * 8;
				return;
			}
			if (index < 40)
			{
				index -= 32;
				this._pos4 &= ~(255UL << index * 8);
				this._pos4 |= (ulong)pos << index * 8;
				return;
			}
			if (index < 48)
			{
				index -= 40;
				this._pos5 &= ~(255UL << index * 8);
				this._pos5 |= (ulong)pos << index * 8;
				return;
			}
			if (index < 56)
			{
				index -= 48;
				this._pos6 &= ~(255UL << index * 8);
				this._pos6 |= (ulong)pos << index * 8;
				return;
			}
			if (index < 64)
			{
				index -= 56;
				this._pos7 &= ~(255UL << index * 8);
				this._pos7 |= (ulong)pos << index * 8;
				return;
			}
			throw new IndexOutOfRangeException();
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x00046B58 File Offset: 0x00044D58
		public byte GetByte(int index)
		{
			if (index < 0)
			{
				throw new IndexOutOfRangeException();
			}
			if (index < 8)
			{
				return (byte)((this._pos0 >> index * 8) & 255UL);
			}
			if (index < 16)
			{
				return (byte)((this._pos1 >> (index - 8) * 8) & 255UL);
			}
			if (index < 24)
			{
				return (byte)((this._pos2 >> (index - 16) * 8) & 255UL);
			}
			if (index < 32)
			{
				return (byte)((this._pos3 >> (index - 24) * 8) & 255UL);
			}
			if (index < 40)
			{
				return (byte)((this._pos4 >> (index - 32) * 8) & 255UL);
			}
			if (index < 48)
			{
				return (byte)((this._pos5 >> (index - 40) * 8) & 255UL);
			}
			if (index < 56)
			{
				return (byte)((this._pos6 >> (index - 48) * 8) & 255UL);
			}
			if (index < 64)
			{
				return (byte)((this._pos7 >> (index - 56) * 8) & 255UL);
			}
			throw new IndexOutOfRangeException();
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001735 RID: 5941 RVA: 0x00046C60 File Offset: 0x00044E60
		public bool IsInitial
		{
			get
			{
				return this._pos7 == 0UL && this._pos6 == 0UL && this._pos5 == 0UL && this._pos4 == 0UL && this._pos3 == 0UL && this._pos2 == 0UL && this._pos1 == 0UL && this._pos0 == 0UL;
			}
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x00046CB4 File Offset: 0x00044EB4
		public static bool operator ==(ByteVector b1, ByteVector b2)
		{
			return b1._pos0 == b2._pos0 && b1._pos1 == b2._pos1 && b1._pos2 == b2._pos2 && b1._pos3 == b2._pos3 && b1._pos4 == b2._pos4 && b1._pos5 == b2._pos5 && b1._pos6 == b2._pos6 && b1._pos7 == b2._pos7;
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x00046D34 File Offset: 0x00044F34
		public static bool operator !=(ByteVector b1, ByteVector b2)
		{
			return b1._pos0 != b2._pos0 || b1._pos1 != b2._pos1 || b1._pos2 != b2._pos2 || b1._pos3 != b2._pos3 || b1._pos4 != b2._pos4 || b1._pos5 != b2._pos5 || b1._pos6 != b2._pos6 || b1._pos7 != b2._pos7;
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x00046DB8 File Offset: 0x00044FB8
		public override bool Equals(object obj)
		{
			if (!(obj is ByteVector))
			{
				return false;
			}
			ByteVector byteVector = (ByteVector)obj;
			return this._pos0 == byteVector._pos0 && this._pos1 == byteVector._pos1 && this._pos2 == byteVector._pos2 && this._pos3 == byteVector._pos3 && this._pos4 == byteVector._pos4 && this._pos5 == byteVector._pos5 && this._pos6 == byteVector._pos6 && this._pos7 == byteVector._pos7;
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x00046E48 File Offset: 0x00045048
		public override int GetHashCode()
		{
			return (((((((17 * 23 + this._pos0.GetHashCode()) * 23 + this._pos1.GetHashCode()) * 23 + this._pos2.GetHashCode()) * 23 + this._pos3.GetHashCode()) * 23 + this._pos4.GetHashCode()) * 23 + this._pos5.GetHashCode()) * 23 + this._pos6.GetHashCode()) * 23 + this._pos7.GetHashCode();
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x00046ED0 File Offset: 0x000450D0
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{0:X16} {1:X16} {2:X16} {3:X16} {4:X16} {5:X16} {6:X16} {7:X16}", new object[] { this._pos7, this._pos6, this._pos5, this._pos4, this._pos3, this._pos2, this._pos1, this._pos0 }));
		}

		// Token: 0x04000B1E RID: 2846
		private ulong _pos0;

		// Token: 0x04000B1F RID: 2847
		private ulong _pos1;

		// Token: 0x04000B20 RID: 2848
		private ulong _pos2;

		// Token: 0x04000B21 RID: 2849
		private ulong _pos3;

		// Token: 0x04000B22 RID: 2850
		private ulong _pos4;

		// Token: 0x04000B23 RID: 2851
		private ulong _pos5;

		// Token: 0x04000B24 RID: 2852
		private ulong _pos6;

		// Token: 0x04000B25 RID: 2853
		private ulong _pos7;
	}
}
