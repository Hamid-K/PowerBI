using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001263 RID: 4707
	public class BinaryFormatReader : IDisposable
	{
		// Token: 0x06007C09 RID: 31753 RVA: 0x001AB074 File Offset: 0x001A9274
		public BinaryFormatReader(Stream stream)
		{
			this.stream = stream;
			this.offset = 0L;
			this.bufferPool = new BinaryFormatBufferPool();
			this.current.limit = long.MaxValue;
			this.current.byteOrder = ByteOrder.BigEndian;
			this.scopes = new Stack<BinaryFormatReader.Scope>();
		}

		// Token: 0x170021D4 RID: 8660
		// (get) Token: 0x06007C0A RID: 31754 RVA: 0x001AB0CC File Offset: 0x001A92CC
		public long Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x170021D5 RID: 8661
		// (get) Token: 0x06007C0B RID: 31755 RVA: 0x001AB0D4 File Offset: 0x001A92D4
		public BinaryFormatBufferPool BufferPool
		{
			get
			{
				return this.bufferPool;
			}
		}

		// Token: 0x06007C0C RID: 31756 RVA: 0x001AB0DC File Offset: 0x001A92DC
		public void PushLimit(long length)
		{
			this.scopes.Push(this.current);
			this.current.limit = checked(this.offset + length);
		}

		// Token: 0x06007C0D RID: 31757 RVA: 0x001AB102 File Offset: 0x001A9302
		public void PushByteOrder(ByteOrder byteOrder)
		{
			this.scopes.Push(this.current);
			this.current.byteOrder = byteOrder;
		}

		// Token: 0x06007C0E RID: 31758 RVA: 0x001AB121 File Offset: 0x001A9321
		public void Pop()
		{
			this.current = this.scopes.Pop();
		}

		// Token: 0x06007C0F RID: 31759 RVA: 0x001AB134 File Offset: 0x001A9334
		public int ReadOptionalByte()
		{
			if (this.current.limit - this.offset == 0L)
			{
				return -1;
			}
			int num = this.stream.ReadByte();
			if (num >= 0)
			{
				this.offset += 1L;
			}
			return num;
		}

		// Token: 0x06007C10 RID: 31760 RVA: 0x001AB16C File Offset: 0x001A936C
		public int ReadOptionalBytes(byte[] bytes, int offset, int count)
		{
			long num = this.current.limit - this.offset;
			if ((long)count > num)
			{
				count = (int)num;
			}
			if (count == 0)
			{
				return 0;
			}
			int num2 = this.stream.Read(bytes, offset, count);
			this.offset += (long)num2;
			return num2;
		}

		// Token: 0x06007C11 RID: 31761 RVA: 0x001AB1B9 File Offset: 0x001A93B9
		public void Dispose()
		{
			this.stream.Dispose();
		}

		// Token: 0x06007C12 RID: 31762 RVA: 0x001AB1C6 File Offset: 0x001A93C6
		public bool TryReadSingle(out float value)
		{
			return this.TryReadSingle(this.current.byteOrder, out value);
		}

		// Token: 0x06007C13 RID: 31763 RVA: 0x001AB1DC File Offset: 0x001A93DC
		public bool TryReadDouble(out double value)
		{
			long num;
			if (!this.TryReadInt64(this.current.byteOrder, out num))
			{
				value = 0.0;
				return false;
			}
			value = BitConverter.Int64BitsToDouble(num);
			return true;
		}

		// Token: 0x06007C14 RID: 31764 RVA: 0x001AB214 File Offset: 0x001A9414
		public bool TryReadDecimal(out decimal value)
		{
			int num;
			if (this.TryReadInt32(ByteOrder.LittleEndian, out num))
			{
				int num2 = this.ReadInt32(ByteOrder.LittleEndian);
				int num3 = this.ReadInt32(ByteOrder.LittleEndian);
				int num4 = this.ReadInt32(ByteOrder.LittleEndian);
				try
				{
					if ((num4 & 2130771967) != 0)
					{
						throw ValueException.NewDataFormatError<Message0>(Strings.BinaryFormat_InvalidDecimalBytes, Value.Null, null);
					}
					byte b = (byte)((num4 >> 16) & 255);
					bool flag = ((long)num4 & (long)((ulong)int.MinValue)) != 0L;
					value = new decimal(num, num2, num3, flag, b);
				}
				catch (ArgumentException)
				{
					throw ValueException.NewDataFormatError<Message0>(Strings.BinaryFormat_InvalidDecimalBytes, Value.Null, null);
				}
				return true;
			}
			value = 0m;
			return false;
		}

		// Token: 0x06007C15 RID: 31765 RVA: 0x001AB2BC File Offset: 0x001A94BC
		public bool TryReadInt16(out short value)
		{
			return this.TryReadInt16(this.current.byteOrder, out value);
		}

		// Token: 0x06007C16 RID: 31766 RVA: 0x001AB2D0 File Offset: 0x001A94D0
		public bool TryReadInt32(out int value)
		{
			return this.TryReadInt32(this.current.byteOrder, out value);
		}

		// Token: 0x06007C17 RID: 31767 RVA: 0x001AB2E4 File Offset: 0x001A94E4
		public bool TryReadInt64(out long value)
		{
			return this.TryReadInt64(this.current.byteOrder, out value);
		}

		// Token: 0x06007C18 RID: 31768 RVA: 0x001AB2F8 File Offset: 0x001A94F8
		public byte ReadByte()
		{
			int num = this.ReadOptionalByte();
			if (num < 0)
			{
				throw this.NewUnexpectedEndOfInputException();
			}
			return (byte)num;
		}

		// Token: 0x06007C19 RID: 31769 RVA: 0x001AB30C File Offset: 0x001A950C
		public short ReadInt16()
		{
			short num;
			if (!this.TryReadInt16(out num))
			{
				throw this.NewUnexpectedEndOfInputException();
			}
			return num;
		}

		// Token: 0x06007C1A RID: 31770 RVA: 0x001AB32C File Offset: 0x001A952C
		public int ReadInt32()
		{
			int num;
			if (!this.TryReadInt32(out num))
			{
				throw this.NewUnexpectedEndOfInputException();
			}
			return num;
		}

		// Token: 0x06007C1B RID: 31771 RVA: 0x001AB34C File Offset: 0x001A954C
		public long ReadInt64()
		{
			long num;
			if (!this.TryReadInt64(out num))
			{
				throw this.NewUnexpectedEndOfInputException();
			}
			return num;
		}

		// Token: 0x06007C1C RID: 31772 RVA: 0x001AB36C File Offset: 0x001A956C
		public bool TryRead7BitEncodedUnsignedInteger(out ulong value)
		{
			int num = this.ReadOptionalByte();
			if (num >= 0)
			{
				value = 0UL;
				int num2 = 0;
				while (num2 != 63 || (num & 254) == 0)
				{
					if ((num & 128) == 0)
					{
						value = (ulong)(((long)num << num2) | (long)value);
						return true;
					}
					value = (ulong)((((long)num & 127L) << num2) | (long)value);
					num2 += 7;
					num = (int)this.ReadByte();
				}
				throw this.New7BitEncodedIntegerTooLargeException();
			}
			value = 0UL;
			return false;
		}

		// Token: 0x06007C1D RID: 31773 RVA: 0x001AB3DC File Offset: 0x001A95DC
		public byte[] ReadBytes(int count)
		{
			byte[] array;
			if (!this.TryReadBytes(count, out array))
			{
				throw this.NewUnexpectedEndOfInputException();
			}
			return array;
		}

		// Token: 0x06007C1E RID: 31774 RVA: 0x001AB3FC File Offset: 0x001A95FC
		public void ReadBytes(byte[] bytes, int offset, int count)
		{
			if (!this.TryReadBytes(bytes, offset, count))
			{
				throw this.NewUnexpectedEndOfInputException();
			}
		}

		// Token: 0x06007C1F RID: 31775 RVA: 0x001AB410 File Offset: 0x001A9610
		public bool TryReadBytes(byte[] bytes, int offset, int count)
		{
			if (count > 0)
			{
				int num = this.ReadOptionalByte();
				if (num < 0)
				{
					return false;
				}
				bytes[offset] = (byte)num;
				checked
				{
					offset++;
				}
				count--;
				this.ReadRemainingBytes(bytes, offset, count);
			}
			return true;
		}

		// Token: 0x06007C20 RID: 31776 RVA: 0x001AB448 File Offset: 0x001A9648
		public bool TryReadBytes(int count, out byte[] bytes)
		{
			if (count <= 0)
			{
				bytes = new byte[0];
				return true;
			}
			int num = this.ReadOptionalByte();
			if (num < 0)
			{
				bytes = null;
				return false;
			}
			bytes = new byte[count];
			bytes[0] = (byte)num;
			count--;
			this.ReadRemainingBytes(bytes, 1, count);
			return true;
		}

		// Token: 0x06007C21 RID: 31777 RVA: 0x001AB491 File Offset: 0x001A9691
		public ValueException NewUnexpectedEndOfInputException()
		{
			return ValueException.NewDataFormatError(Strings.BinaryFormat_EndOfInput(this.Offset), Value.Null, null);
		}

		// Token: 0x06007C22 RID: 31778 RVA: 0x001AB4B3 File Offset: 0x001A96B3
		public ValueException NewNotEnoughReadException(long amountNotread)
		{
			return ValueException.NewDataFormatError(Strings.BinaryFormat_NotEnoughRead(amountNotread), Value.Null, null);
		}

		// Token: 0x06007C23 RID: 31779 RVA: 0x001AB4D0 File Offset: 0x001A96D0
		public ValueException New7BitEncodedIntegerTooLargeException()
		{
			return ValueException.NewDataFormatError<Message0>(Strings.BinaryFormat_7BitEncodedValueTooLarge, Value.Null, null);
		}

		// Token: 0x06007C24 RID: 31780 RVA: 0x001AB4E4 File Offset: 0x001A96E4
		protected bool TryReadSingle(ByteOrder byteOrder, out float value)
		{
			BinaryFormatBufferPool binaryFormatBufferPool = this.BufferPool;
			byte[] array = binaryFormatBufferPool.TakeBuffer(8);
			if (this.TryReadBytes(array, 0, 4))
			{
				int num;
				if (byteOrder == ByteOrder.BigEndian)
				{
					array[4] = array[3];
					array[5] = array[2];
					array[6] = array[1];
					array[7] = array[0];
					num = 4;
				}
				else
				{
					if (byteOrder != ByteOrder.LittleEndian)
					{
						throw new InvalidOperationException();
					}
					num = 0;
				}
				value = BitConverter.ToSingle(array, num);
				binaryFormatBufferPool.ReturnBuffer(array);
				return true;
			}
			value = 0f;
			return false;
		}

		// Token: 0x06007C25 RID: 31781 RVA: 0x001AB554 File Offset: 0x001A9754
		protected bool TryReadInt16(ByteOrder byteOrder, out short value)
		{
			int num = this.ReadOptionalByte();
			if (num >= 0)
			{
				int num2 = (int)this.ReadByte();
				if (byteOrder == ByteOrder.BigEndian)
				{
					value = BinaryFormat.Int16FromBigEndian(num, num2);
				}
				else
				{
					if (byteOrder != ByteOrder.LittleEndian)
					{
						throw new InvalidOperationException();
					}
					value = BinaryFormat.Int16FromLittleEndian(num, num2);
				}
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x06007C26 RID: 31782 RVA: 0x001AB59C File Offset: 0x001A979C
		protected int ReadInt32(ByteOrder byteOrder)
		{
			int num;
			if (!this.TryReadInt32(byteOrder, out num))
			{
				throw this.NewUnexpectedEndOfInputException();
			}
			return num;
		}

		// Token: 0x06007C27 RID: 31783 RVA: 0x001AB5BC File Offset: 0x001A97BC
		protected bool TryReadInt32(ByteOrder byteOrder, out int value)
		{
			int num = this.ReadOptionalByte();
			if (num >= 0)
			{
				int num2 = (int)this.ReadByte();
				int num3 = (int)this.ReadByte();
				int num4 = (int)this.ReadByte();
				if (byteOrder == ByteOrder.BigEndian)
				{
					value = BinaryFormat.Int32FromBigEndian(num, num2, num3, num4);
				}
				else
				{
					if (byteOrder != ByteOrder.LittleEndian)
					{
						throw new InvalidOperationException();
					}
					value = BinaryFormat.Int32FromLittleEndian(num, num2, num3, num4);
				}
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x06007C28 RID: 31784 RVA: 0x001AB618 File Offset: 0x001A9818
		protected bool TryReadInt64(ByteOrder byteOrder, out long value)
		{
			int num;
			if (this.TryReadInt32(out num))
			{
				int num2 = this.ReadInt32();
				if (byteOrder == ByteOrder.BigEndian)
				{
					value = BinaryFormat.Int64FromBigEndian(num, num2);
				}
				else
				{
					if (byteOrder != ByteOrder.LittleEndian)
					{
						throw new InvalidOperationException();
					}
					value = BinaryFormat.Int64FromLittleEndian(num, num2);
				}
				return true;
			}
			value = 0L;
			return false;
		}

		// Token: 0x06007C29 RID: 31785 RVA: 0x001AB660 File Offset: 0x001A9860
		protected void ReadRemainingBytes(byte[] bytes, int offset, int count)
		{
			if (count > 0)
			{
				for (;;)
				{
					int num = count;
					int num2 = this.ReadOptionalBytes(bytes, offset, num);
					if (num2 == 0)
					{
						break;
					}
					offset += num2;
					count -= num2;
				}
				if (count != 0)
				{
					throw this.NewUnexpectedEndOfInputException();
				}
			}
		}

		// Token: 0x0400449E RID: 17566
		private readonly Stream stream;

		// Token: 0x0400449F RID: 17567
		private readonly BinaryFormatBufferPool bufferPool;

		// Token: 0x040044A0 RID: 17568
		private long offset;

		// Token: 0x040044A1 RID: 17569
		private BinaryFormatReader.Scope current;

		// Token: 0x040044A2 RID: 17570
		private Stack<BinaryFormatReader.Scope> scopes;

		// Token: 0x02001264 RID: 4708
		private struct Scope
		{
			// Token: 0x06007C2A RID: 31786 RVA: 0x001AB696 File Offset: 0x001A9896
			public Scope(long limit, ByteOrder byteOrder)
			{
				this.limit = limit;
				this.byteOrder = byteOrder;
			}

			// Token: 0x040044A3 RID: 17571
			public ByteOrder byteOrder;

			// Token: 0x040044A4 RID: 17572
			public long limit;
		}
	}
}
