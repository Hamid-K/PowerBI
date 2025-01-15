using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000E1 RID: 225
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class PageReader : IDisposable
	{
		// Token: 0x06000434 RID: 1076 RVA: 0x0000CB00 File Offset: 0x0000AD00
		public PageReader(Stream stream)
		{
			this.stream = stream;
			this.buffer = new byte[1024];
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000CB1F File Offset: 0x0000AD1F
		public void Dispose()
		{
			this.stream.Close();
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000CB2C File Offset: 0x0000AD2C
		public DataTable ReadSchema(bool readColumnOrdinals)
		{
			DataTable dataTable = new DataTable();
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("ColumnName", typeof(string));
			dataTable.Columns.Add("ColumnGuid", typeof(Guid));
			dataTable.Columns.Add("ColumnPropId", typeof(DBPROPID));
			dataTable.Columns.Add("ColumnOrdinal", typeof(int));
			dataTable.Columns.Add("DataType", typeof(Type));
			dataTable.Columns.Add("AllowDBNull", typeof(bool));
			dataTable.Columns.Add("IsKey", typeof(bool));
			BinaryReader binaryReader = new BinaryReader(this.stream);
			int num = binaryReader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string text = binaryReader.ReadString();
				Guid guid = Guid.Parse(binaryReader.ReadString());
				DBPROPID dbpropid = (DBPROPID)binaryReader.ReadUInt32();
				int num2 = (readColumnOrdinals ? binaryReader.ReadInt32() : (i + 1));
				ColumnType columnType = (ColumnType)binaryReader.ReadByte();
				bool flag = binaryReader.ReadBoolean();
				bool flag2 = binaryReader.ReadBoolean();
				Type type = Column.GetType(columnType);
				dataTable.Rows.Add(new object[] { text, guid, dbpropid, num2, type, flag, flag2 });
			}
			new ObjectReader(binaryReader).ReadProperties(dataTable.ExtendedProperties);
			return dataTable;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000CCE0 File Offset: 0x0000AEE0
		public void ReadExceptionRows(Dictionary<int, IExceptionRow> exceptionRows)
		{
			int num = this.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				int num2 = this.ReadInt32();
				exceptionRows.Add(num2, this.ReadExceptionRow());
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000CD14 File Offset: 0x0000AF14
		private IExceptionRow ReadExceptionRow()
		{
			int num = this.ReadInt32();
			IDictionary<int, IDictionary<string, string>> dictionary = new Dictionary<int, IDictionary<string, string>>(num);
			for (int i = 0; i < num; i++)
			{
				int num2 = this.ReadInt32();
				dictionary.Add(num2, this.ReadException());
			}
			return new ExceptionRow(dictionary);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000CD58 File Offset: 0x0000AF58
		private IDictionary<string, string> ReadException()
		{
			int num = this.ReadInt32();
			Dictionary<string, string> dictionary = new Dictionary<string, string>(num);
			for (int i = 0; i < num; i++)
			{
				string text = this.ReadString();
				string text2 = this.ReadString();
				dictionary.Add(text, text2);
			}
			return dictionary;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000CD98 File Offset: 0x0000AF98
		public void ReadArray(byte[] values, int offset, int count)
		{
			this.Read(values, offset, count);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000CDA4 File Offset: 0x0000AFA4
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

		// Token: 0x0600043C RID: 1084 RVA: 0x0000CDD8 File Offset: 0x0000AFD8
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

		// Token: 0x0600043D RID: 1085 RVA: 0x0000CE0C File Offset: 0x0000B00C
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

		// Token: 0x0600043E RID: 1086 RVA: 0x0000CE40 File Offset: 0x0000B040
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

		// Token: 0x0600043F RID: 1087 RVA: 0x0000CE74 File Offset: 0x0000B074
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

		// Token: 0x06000440 RID: 1088 RVA: 0x0000CEA8 File Offset: 0x0000B0A8
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

		// Token: 0x06000441 RID: 1089 RVA: 0x0000CEE0 File Offset: 0x0000B0E0
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

		// Token: 0x06000442 RID: 1090 RVA: 0x0000CF18 File Offset: 0x0000B118
		public void ReadArray(Date[] values, int offset, int count)
		{
			this.ReadArray<Date>(values, offset, count, (long value) => new Date(DateTime.FromBinary(value)));
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000CF42 File Offset: 0x0000B142
		public void ReadArray(Time[] values, int offset, int count)
		{
			this.ReadArray<Time>(values, offset, count, (long value) => new Time(new TimeSpan(value)));
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000CF6C File Offset: 0x0000B16C
		public void ReadArray(DateTime[] values, int offset, int count)
		{
			this.ReadArray<DateTime>(values, offset, count, (long value) => DateTime.FromBinary(value));
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000CF98 File Offset: 0x0000B198
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

		// Token: 0x06000446 RID: 1094 RVA: 0x0000CFED File Offset: 0x0000B1ED
		public void ReadArray(TimeSpan[] values, int offset, int count)
		{
			this.ReadArray<TimeSpan>(values, offset, count, (long value) => new TimeSpan(value));
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000D018 File Offset: 0x0000B218
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

		// Token: 0x06000448 RID: 1096 RVA: 0x0000D04C File Offset: 0x0000B24C
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

		// Token: 0x06000449 RID: 1097 RVA: 0x0000D0AC File Offset: 0x0000B2AC
		public void ReadArray(byte[][] values, int offset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				values[offset + i] = this.ReadBinary();
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000D0D0 File Offset: 0x0000B2D0
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

		// Token: 0x0600044B RID: 1099 RVA: 0x0000D114 File Offset: 0x0000B314
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

		// Token: 0x0600044C RID: 1100 RVA: 0x0000D166 File Offset: 0x0000B366
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

		// Token: 0x0600044D RID: 1101 RVA: 0x0000D190 File Offset: 0x0000B390
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

		// Token: 0x0600044E RID: 1102 RVA: 0x0000D1F8 File Offset: 0x0000B3F8
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

		// Token: 0x0600044F RID: 1103 RVA: 0x0000D258 File Offset: 0x0000B458
		private void ReadArray<[global::System.Runtime.CompilerServices.Nullable(2)] T>(T[] values, int offset, int count, Func<long, T> func)
		{
			ulong[] array = new ulong[count];
			this.ReadArray(array, 0, count);
			for (int i = 0; i < count; i++)
			{
				values[offset + i] = func((long)array[i]);
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000D294 File Offset: 0x0000B494
		private byte[] ReadBinary()
		{
			int num = this.ReadInt32();
			byte[] array = new byte[num];
			this.Read(array, 0, num);
			return array;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000D2BC File Offset: 0x0000B4BC
		[global::System.Runtime.CompilerServices.NullableContext(0)]
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

		// Token: 0x06000452 RID: 1106 RVA: 0x0000D334 File Offset: 0x0000B534
		private void Read(byte[] buffer, int offset, int count)
		{
			if (this.stream.Read(buffer, offset, count) != count)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000D34D File Offset: 0x0000B54D
		private byte[] GetBuffer(int length)
		{
			if (length > this.buffer.Length)
			{
				this.buffer = new byte[Math.Max(length, this.buffer.Length * 2)];
			}
			return this.buffer;
		}

		// Token: 0x040003E4 RID: 996
		private readonly Stream stream;

		// Token: 0x040003E5 RID: 997
		private byte[] buffer;
	}
}
