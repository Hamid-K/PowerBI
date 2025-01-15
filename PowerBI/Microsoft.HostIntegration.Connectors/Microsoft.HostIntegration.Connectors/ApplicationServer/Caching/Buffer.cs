using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000368 RID: 872
	internal class Buffer
	{
		// Token: 0x06001EA6 RID: 7846 RVA: 0x0005E02D File Offset: 0x0005C22D
		public Buffer(long id, byte[] array, long offset, int count)
		{
			this._id = id;
			this._array = array;
			this._offset = offset;
			this._count = count;
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001EA7 RID: 7847 RVA: 0x0005E052 File Offset: 0x0005C252
		public long Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x0005E05A File Offset: 0x0005C25A
		// (set) Token: 0x06001EA9 RID: 7849 RVA: 0x0005E06D File Offset: 0x0005C26D
		public long Next
		{
			get
			{
				return Buffer.ReadLong(this._array, this._offset);
			}
			set
			{
				Buffer.WriteLong(this._array, this._offset, value);
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001EAA RID: 7850 RVA: 0x0005E081 File Offset: 0x0005C281
		public int Count
		{
			get
			{
				return this._count - 8;
			}
		}

		// Token: 0x17000647 RID: 1607
		public byte this[long offset]
		{
			get
			{
				checked
				{
					return this._array[(int)((IntPtr)(unchecked(this._offset + 8L + offset)))];
				}
			}
			set
			{
				checked
				{
					this._array[(int)((IntPtr)(unchecked(this._offset + 8L + offset)))] = value;
				}
			}
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x0005E0B6 File Offset: 0x0005C2B6
		public void Read(int fromOffset, byte[] buffer, int toOffset, int count)
		{
			Array.Copy(this._array, this._offset + (long)fromOffset + 8L, buffer, (long)toOffset, (long)count);
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x0005E0D5 File Offset: 0x0005C2D5
		public void Write(int toOffset, byte[] buffer, int fromOffset, int count)
		{
			Array.Copy(buffer, (long)fromOffset, this._array, this._offset + (long)toOffset + 8L, (long)count);
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x0005E0F4 File Offset: 0x0005C2F4
		private static long ReadLong(byte[] buffer, long offset)
		{
			long num = (long)((ulong)(checked((int)buffer[(int)((IntPtr)offset)] | ((int)buffer[(int)((IntPtr)(unchecked(offset + 1L)))] << 8) | ((int)buffer[(int)((IntPtr)(unchecked(offset + 2L)))] << 16) | ((int)buffer[(int)((IntPtr)(unchecked(offset + 3L)))] << 24))));
			long num2 = (long)((ulong)(checked((int)buffer[(int)((IntPtr)(unchecked(offset + 4L)))] | ((int)buffer[(int)((IntPtr)(unchecked(offset + 5L)))] << 8) | ((int)buffer[(int)((IntPtr)(unchecked(offset + 6L)))] << 16) | ((int)buffer[(int)((IntPtr)(unchecked(offset + 7L)))] << 24))));
			return (num2 << 32) | num;
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x0005E158 File Offset: 0x0005C358
		private static void WriteLong(byte[] buffer, long offset, long value)
		{
			buffer[(int)(checked((IntPtr)offset))] = (byte)value;
			checked
			{
				buffer[(int)((IntPtr)(unchecked(offset + 1L)))] = unchecked((byte)(value >> 8));
				buffer[(int)((IntPtr)(unchecked(offset + 2L)))] = unchecked((byte)(value >> 16));
				buffer[(int)((IntPtr)(unchecked(offset + 3L)))] = unchecked((byte)(value >> 24));
				buffer[(int)((IntPtr)(unchecked(offset + 4L)))] = unchecked((byte)(value >> 32));
				buffer[(int)((IntPtr)(unchecked(offset + 5L)))] = unchecked((byte)(value >> 40));
				buffer[(int)((IntPtr)(unchecked(offset + 6L)))] = unchecked((byte)(value >> 48));
				buffer[(int)((IntPtr)(unchecked(offset + 7L)))] = unchecked((byte)(value >> 56));
			}
		}

		// Token: 0x04001158 RID: 4440
		public const int HeaderSize = 8;

		// Token: 0x04001159 RID: 4441
		public const int NullId = -1;

		// Token: 0x0400115A RID: 4442
		private long _id;

		// Token: 0x0400115B RID: 4443
		private byte[] _array;

		// Token: 0x0400115C RID: 4444
		private long _offset;

		// Token: 0x0400115D RID: 4445
		private int _count;
	}
}
