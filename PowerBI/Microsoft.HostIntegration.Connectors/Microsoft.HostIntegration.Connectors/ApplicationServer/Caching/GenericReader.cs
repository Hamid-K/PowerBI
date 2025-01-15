using System;
using System.Text;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001D8 RID: 472
	internal abstract class GenericReader
	{
		// Token: 0x06000F4C RID: 3916
		public abstract bool AreBytesAvailable(int bytes);

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000F4D RID: 3917
		public abstract int Position { get; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000F4E RID: 3918
		public abstract int Length { get; }

		// Token: 0x06000F4F RID: 3919
		protected abstract byte ReadOneByte();

		// Token: 0x06000F50 RID: 3920
		protected abstract void GetNextData(ArraySegment<byte> segment);

		// Token: 0x06000F51 RID: 3921
		protected abstract ArraySegment<byte> GetNextData(int length);

		// Token: 0x06000F52 RID: 3922 RVA: 0x000348E0 File Offset: 0x00032AE0
		public byte ReadByte()
		{
			return this.ReadOneByte();
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x000348E8 File Offset: 0x00032AE8
		public sbyte ReadSByte()
		{
			return (sbyte)this.ReadOneByte();
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x000348F4 File Offset: 0x00032AF4
		public bool ReadBooleanFromByte()
		{
			byte b = this.ReadByte();
			return b != 0;
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0003490F File Offset: 0x00032B0F
		public char ReadChar()
		{
			return (char)this.ReadUInt16();
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x00034918 File Offset: 0x00032B18
		public short ReadInt16()
		{
			ArraySegment<byte> nextData = this.GetNextData(2);
			return (short)((int)nextData.Array[nextData.Offset] | ((int)nextData.Array[nextData.Offset + 1] << 8));
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00034954 File Offset: 0x00032B54
		public ushort ReadUInt16()
		{
			ArraySegment<byte> nextData = this.GetNextData(2);
			return (ushort)((int)nextData.Array[nextData.Offset] | ((int)nextData.Array[nextData.Offset + 1] << 8));
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00034990 File Offset: 0x00032B90
		public int ReadInt32()
		{
			ArraySegment<byte> nextData = this.GetNextData(4);
			return GenericReader.GetInteger(nextData.Array, nextData.Offset);
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x000349B8 File Offset: 0x00032BB8
		public uint ReadUInt32()
		{
			ArraySegment<byte> nextData = this.GetNextData(4);
			return (uint)GenericReader.GetInteger(nextData.Array, nextData.Offset);
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x000349E0 File Offset: 0x00032BE0
		public long ReadInt64()
		{
			ArraySegment<byte> nextData = this.GetNextData(8);
			long num = (long)((ulong)GenericReader.GetInteger(nextData.Array, nextData.Offset));
			long num2 = (long)GenericReader.GetInteger(nextData.Array, nextData.Offset + 4) << 32;
			return num | num2;
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x00034A28 File Offset: 0x00032C28
		public ulong ReadUInt64()
		{
			ArraySegment<byte> nextData = this.GetNextData(8);
			ulong num = (ulong)GenericReader.GetInteger(nextData.Array, nextData.Offset);
			ulong num2 = (ulong)((ulong)((long)GenericReader.GetInteger(nextData.Array, nextData.Offset + 4)) << 32);
			return num | num2;
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x00034A70 File Offset: 0x00032C70
		public float ReadSingle()
		{
			ArraySegment<byte> nextData = this.GetNextData(4);
			return BitConverter.ToSingle(nextData.Array, nextData.Offset);
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00034A98 File Offset: 0x00032C98
		public double ReadDouble()
		{
			ArraySegment<byte> nextData = this.GetNextData(8);
			return BitConverter.ToDouble(nextData.Array, nextData.Offset);
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x00034AC0 File Offset: 0x00032CC0
		public string ReadString(int length, Encoding encoding)
		{
			ArraySegment<byte> nextData = this.GetNextData(length);
			return encoding.GetString(nextData.Array, nextData.Offset, length);
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x00034AEC File Offset: 0x00032CEC
		public void ReadBytes(byte[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", offset, "Array offset >= 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", count, "Count >= 0");
			}
			if (array.Length < offset + count)
			{
				throw new ArgumentException("Array passed in is not large enough");
			}
			if (count == 0)
			{
				return;
			}
			this.GetNextData(new ArraySegment<byte>(array, offset, count));
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x00034B60 File Offset: 0x00032D60
		private static int GetInteger(byte[] array, int offset)
		{
			return (int)array[offset] | ((int)array[offset + 1] << 8) | ((int)array[offset + 2] << 16) | ((int)array[offset + 3] << 24);
		}
	}
}
