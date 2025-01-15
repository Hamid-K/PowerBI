using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Serialization;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02001FC1 RID: 8129
	public class PageReader : IDisposable
	{
		// Token: 0x0600C65B RID: 50779 RVA: 0x002787E9 File Offset: 0x002769E9
		public PageReader(Stream stream)
		{
			this.stream = stream;
			this.buffer = new byte[1024];
		}

		// Token: 0x0600C65C RID: 50780 RVA: 0x00278808 File Offset: 0x00276A08
		public void Dispose()
		{
			this.stream.Close();
		}

		// Token: 0x0600C65D RID: 50781 RVA: 0x00278818 File Offset: 0x00276A18
		public TableSchema ReadSchema()
		{
			BinaryReader binaryReader = new BinaryReader(this.stream);
			return this.ReadSchema(binaryReader);
		}

		// Token: 0x0600C65E RID: 50782 RVA: 0x00278838 File Offset: 0x00276A38
		private TableSchema ReadSchema(BinaryReader reader)
		{
			int num = reader.ReadInt32();
			if (num < 0)
			{
				return null;
			}
			TableSchema tableSchema = new TableSchema(num);
			for (int i = 0; i < num; i++)
			{
				string text = reader.ReadString();
				ColumnType columnType = (ColumnType)reader.ReadByte();
				bool flag = reader.ReadBoolean();
				bool flag2 = reader.ReadBoolean();
				bool flag3 = reader.ReadBoolean();
				Type type = ValueWithMetadata.AddMetadata(Column.GetType(columnType), flag2);
				SchemaColumn schemaColumn = tableSchema.AddColumn(text, i + 1, type, flag);
				schemaColumn.IsKey = flag3;
				schemaColumn.ColumnSchema = this.ReadSchema(reader);
			}
			int num2 = reader.ReadInt32();
			if (num2 > 0)
			{
				byte[] array = new byte[num2];
				reader.Read(array, 0, array.Length);
				new ObjectReader(new BinaryReader(new MemoryStream(array))).ReadProperties(tableSchema.ExtendedProperties);
			}
			return tableSchema;
		}

		// Token: 0x0600C65F RID: 50783 RVA: 0x00278900 File Offset: 0x00276B00
		public void ReadExceptionRows(Dictionary<int, IExceptionRow> exceptionRows)
		{
			int num = this.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				int num2 = this.ReadInt32();
				exceptionRows.Add(num2, this.ReadExceptionRow());
			}
		}

		// Token: 0x0600C660 RID: 50784 RVA: 0x00278934 File Offset: 0x00276B34
		private IExceptionRow ReadExceptionRow()
		{
			int num = this.ReadInt32();
			IDictionary<int, ISerializedException> dictionary = new Dictionary<int, ISerializedException>(num);
			for (int i = 0; i < num; i++)
			{
				int num2 = this.ReadInt32();
				dictionary.Add(num2, this.ReadException());
			}
			return new ExceptionRow(dictionary);
		}

		// Token: 0x0600C661 RID: 50785 RVA: 0x00278978 File Offset: 0x00276B78
		public ISerializedException ReadException()
		{
			int num = this.ReadInt32();
			SerializedException ex = new SerializedException(num);
			for (int i = 0; i < num; i++)
			{
				string text = this.ReadString();
				string text2 = this.ReadString();
				ex.Add(text, text2);
			}
			return ex;
		}

		// Token: 0x0600C662 RID: 50786 RVA: 0x002789B8 File Offset: 0x00276BB8
		public void ReadArray(byte[] values, int offset, int count)
		{
			this.Read(values, offset, count);
		}

		// Token: 0x0600C663 RID: 50787 RVA: 0x002789C4 File Offset: 0x00276BC4
		public unsafe void ReadArray(ushort[] values, int offset, int count)
		{
			PageReader.CheckArray(values.Length, offset, count);
			if (count > 0)
			{
				fixed (ushort* ptr = &values[offset])
				{
					ushort* ptr2 = ptr;
					this.Read((void*)ptr2, count * 2);
				}
			}
		}

		// Token: 0x0600C664 RID: 50788 RVA: 0x002789F8 File Offset: 0x00276BF8
		public unsafe void ReadArray(uint[] values, int offset, int count)
		{
			PageReader.CheckArray(values.Length, offset, count);
			if (count > 0)
			{
				fixed (uint* ptr = &values[offset])
				{
					uint* ptr2 = ptr;
					this.Read((void*)ptr2, count * 4);
				}
			}
		}

		// Token: 0x0600C665 RID: 50789 RVA: 0x00278A2C File Offset: 0x00276C2C
		public unsafe void ReadArray(int[] values, int offset, int count)
		{
			PageReader.CheckArray(values.Length, offset, count);
			if (count > 0)
			{
				fixed (int* ptr = &values[offset])
				{
					int* ptr2 = ptr;
					this.Read((void*)ptr2, count * 4);
				}
			}
		}

		// Token: 0x0600C666 RID: 50790 RVA: 0x00278A60 File Offset: 0x00276C60
		public unsafe void ReadArray(ulong[] values, int offset, int count)
		{
			PageReader.CheckArray(values.Length, offset, count);
			if (count > 0)
			{
				fixed (ulong* ptr = &values[offset])
				{
					ulong* ptr2 = ptr;
					this.Read((void*)ptr2, count * 8);
				}
			}
		}

		// Token: 0x0600C667 RID: 50791 RVA: 0x00278A94 File Offset: 0x00276C94
		public unsafe void ReadArray(decimal[] values, int offset, int count)
		{
			PageReader.CheckArray(values.Length, offset, count);
			if (count > 0)
			{
				fixed (decimal* ptr = &values[offset])
				{
					decimal* ptr2 = ptr;
					this.Read((void*)ptr2, count * 16);
				}
			}
		}

		// Token: 0x0600C668 RID: 50792 RVA: 0x00278AC8 File Offset: 0x00276CC8
		public unsafe void ReadArray(Number[] values, int offset, int count)
		{
			PageReader.CheckArray(values.Length, offset, count);
			if (count > 0)
			{
				fixed (Number* ptr = &values[offset])
				{
					Number* ptr2 = ptr;
					this.Read((void*)ptr2, count * sizeof(Number));
				}
			}
		}

		// Token: 0x0600C669 RID: 50793 RVA: 0x00278B00 File Offset: 0x00276D00
		public unsafe void ReadArray(Guid[] values, int offset, int count)
		{
			PageReader.CheckArray(values.Length, offset, count);
			if (count > 0)
			{
				fixed (Guid* ptr = &values[offset])
				{
					Guid* ptr2 = ptr;
					this.Read((void*)ptr2, count * sizeof(Guid));
				}
			}
		}

		// Token: 0x0600C66A RID: 50794 RVA: 0x00278B38 File Offset: 0x00276D38
		public void ReadArray(Date[] values, int offset, int count)
		{
			this.ReadArray<Date>(values, offset, count, (long value) => new Date(DateTime.FromBinary(value)));
		}

		// Token: 0x0600C66B RID: 50795 RVA: 0x00278B62 File Offset: 0x00276D62
		public void ReadArray(Time[] values, int offset, int count)
		{
			this.ReadArray<Time>(values, offset, count, (long value) => new Time(new TimeSpan(value)));
		}

		// Token: 0x0600C66C RID: 50796 RVA: 0x00278B8C File Offset: 0x00276D8C
		public void ReadArray(DateTime[] values, int offset, int count)
		{
			this.ReadArray<DateTime>(values, offset, count, (long value) => DateTime.FromBinary(value));
		}

		// Token: 0x0600C66D RID: 50797 RVA: 0x00278BB8 File Offset: 0x00276DB8
		public void ReadArray(DateTimeOffset[] values, int offset, int count)
		{
			DateTime[] array = new DateTime[count];
			TimeSpan[] array2 = new TimeSpan[count];
			this.ReadArray(array, 0, count);
			this.ReadArray(array2, 0, count);
			for (int i = 0; i < count; i++)
			{
				values[offset + i] = new DateTimeOffset(array[i], array2[i]);
			}
		}

		// Token: 0x0600C66E RID: 50798 RVA: 0x00278C0D File Offset: 0x00276E0D
		public void ReadArray(TimeSpan[] values, int offset, int count)
		{
			this.ReadArray<TimeSpan>(values, offset, count, (long value) => new TimeSpan(value));
		}

		// Token: 0x0600C66F RID: 50799 RVA: 0x00278C38 File Offset: 0x00276E38
		public unsafe void ReadArray(char[] values, int offset, int count)
		{
			PageReader.CheckArray(values.Length, offset, count);
			if (count > 0)
			{
				fixed (char* ptr = &values[offset])
				{
					char* ptr2 = ptr;
					this.Read((void*)ptr2, count * 2);
				}
			}
		}

		// Token: 0x0600C670 RID: 50800 RVA: 0x00278C6C File Offset: 0x00276E6C
		public void ReadArray(string[] values, int offset, int count)
		{
			int[] array = this.ReadLengths(count);
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				num += array[i];
			}
			char[] array2 = this.ReadChars(num);
			int num2 = 0;
			for (int j = 0; j < count; j++)
			{
				values[j] = new string(array2, num2, array[j]);
				num2 += array[j];
			}
		}

		// Token: 0x0600C671 RID: 50801 RVA: 0x00278CCC File Offset: 0x00276ECC
		public void ReadArray(byte[][] values, int offset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				values[offset + i] = this.ReadBinary();
			}
		}

		// Token: 0x0600C672 RID: 50802 RVA: 0x00278CF0 File Offset: 0x00276EF0
		public bool ReadBool()
		{
			int num = this.stream.ReadByte();
			if (num == -1)
			{
				throw new EndOfStreamException();
			}
			return num != 0;
		}

		// Token: 0x0600C673 RID: 50803 RVA: 0x00278D0C File Offset: 0x00276F0C
		public int ReadInt32()
		{
			uint num = 0U;
			int num2 = 0;
			for (;;)
			{
				uint num3 = (uint)this.stream.ReadByte();
				if (num3 == 4294967295U)
				{
					break;
				}
				num |= (num3 & 127U) << num2;
				if ((num3 & 128U) == 0U)
				{
					return (int)num;
				}
				num2 += 7;
			}
			throw new EndOfStreamException();
		}

		// Token: 0x0600C674 RID: 50804 RVA: 0x00278D50 File Offset: 0x00276F50
		public unsafe string ReadString()
		{
			int num = this.ReadInt32();
			if (num == 0)
			{
				return null;
			}
			num--;
			byte[] array = this.GetBuffer(num * 2);
			this.Read(array, 0, num * 2);
			byte[] array2;
			byte* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			return new string((char*)ptr, 0, num);
		}

		// Token: 0x0600C675 RID: 50805 RVA: 0x00278DA2 File Offset: 0x00276FA2
		private static void CheckArray(int length, int offset, int count)
		{
			if (offset < 0 || offset > length)
			{
				throw new ArgumentException("offset");
			}
			if (count < 0 || count > length - offset)
			{
				throw new ArgumentException("count");
			}
		}

		// Token: 0x0600C676 RID: 50806 RVA: 0x00278DCC File Offset: 0x00276FCC
		private int[] ReadLengths(int count)
		{
			int num = this.stream.ReadByte();
			int[] array = new int[count];
			if (num == 1)
			{
				byte[] array2 = this.GetBuffer(count);
				this.stream.Read(array2, 0, count);
				for (int i = 0; i < count; i++)
				{
					array[i] = (int)array2[i];
				}
			}
			else
			{
				if (num != 4)
				{
					throw new InvalidOperationException();
				}
				this.ReadArray(array, 0, count);
			}
			return array;
		}

		// Token: 0x0600C677 RID: 50807 RVA: 0x00278E34 File Offset: 0x00277034
		private char[] ReadChars(int count)
		{
			char[] array = new char[count];
			int num = this.stream.ReadByte();
			if (num == 1)
			{
				byte[] array2 = this.GetBuffer(count);
				this.Read(array2, 0, count);
				for (int i = 0; i < count; i++)
				{
					array[i] = (char)array2[i];
				}
			}
			else
			{
				if (num != 2)
				{
					throw new InvalidOperationException();
				}
				this.ReadArray(array, 0, count);
			}
			return array;
		}

		// Token: 0x0600C678 RID: 50808 RVA: 0x00278E94 File Offset: 0x00277094
		private void ReadArray<T>(T[] values, int offset, int count, Func<long, T> func)
		{
			ulong[] array = new ulong[count];
			this.ReadArray(array, 0, count);
			for (int i = 0; i < count; i++)
			{
				values[offset + i] = func((long)array[i]);
			}
		}

		// Token: 0x0600C679 RID: 50809 RVA: 0x00278ED0 File Offset: 0x002770D0
		private byte[] ReadBinary()
		{
			int num = this.ReadInt32();
			byte[] array = new byte[num];
			this.Read(array, 0, num);
			return array;
		}

		// Token: 0x0600C67A RID: 50810 RVA: 0x00278EF8 File Offset: 0x002770F8
		private unsafe void Read(void* dst, int length)
		{
			byte[] array = this.GetBuffer(length);
			this.Read(array, 0, length);
			byte[] array2;
			void* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = (void*)(&array2[0]);
			}
			ulong* ptr2 = (ulong*)ptr;
			ulong* ptr3 = (ulong*)dst;
			while (length >= 8)
			{
				*(ptr3++) = *(ptr2++);
				length -= 8;
			}
			byte* ptr4 = (byte*)ptr2;
			byte* ptr5 = (byte*)ptr3;
			while (length >= 1)
			{
				*(ptr5++) = *(ptr4++);
				length--;
			}
			array2 = null;
		}

		// Token: 0x0600C67B RID: 50811 RVA: 0x00278F70 File Offset: 0x00277170
		private void Read(byte[] buffer, int offset, int count)
		{
			while (count > 0)
			{
				int num = this.stream.Read(buffer, offset, count);
				if (num == 0)
				{
					throw new InvalidOperationException();
				}
				offset += num;
				count -= num;
			}
		}

		// Token: 0x0600C67C RID: 50812 RVA: 0x00278FA5 File Offset: 0x002771A5
		private byte[] GetBuffer(int length)
		{
			if (length > this.buffer.Length)
			{
				this.buffer = new byte[Math.Max(length, this.buffer.Length * 2)];
			}
			return this.buffer;
		}

		// Token: 0x04006561 RID: 25953
		private readonly Stream stream;

		// Token: 0x04006562 RID: 25954
		private byte[] buffer;
	}
}
