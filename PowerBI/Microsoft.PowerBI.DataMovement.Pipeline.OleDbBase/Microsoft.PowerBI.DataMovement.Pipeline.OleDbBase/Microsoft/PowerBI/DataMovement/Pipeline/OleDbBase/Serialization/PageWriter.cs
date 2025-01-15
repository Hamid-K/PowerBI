using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000E3 RID: 227
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class PageWriter : IDisposable
	{
		// Token: 0x0600047B RID: 1147 RVA: 0x0000D76D File Offset: 0x0000B96D
		public PageWriter(Stream stream)
		{
			this.stream = stream;
			this.buffer = new byte[1024];
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000D78C File Offset: 0x0000B98C
		public void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000D799 File Offset: 0x0000B999
		public void Dispose()
		{
			this.stream.Close();
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000D7A8 File Offset: 0x0000B9A8
		public void WriteSchema(DataTable schemaTable, bool writeColumnOrdinals)
		{
			BinaryWriter binaryWriter = new BinaryWriter(this.stream);
			binaryWriter.Write(schemaTable.Rows.Count);
			bool flag = schemaTable.Columns["IsKey"] != null;
			for (int i = 0; i < schemaTable.Rows.Count; i++)
			{
				string text = (string)schemaTable.Rows[i]["ColumnName"];
				Guid guid = (Guid)schemaTable.Rows[i]["ColumnGuid"];
				DBPROPID dbpropid = (DBPROPID)schemaTable.Rows[i]["ColumnPropId"];
				int num = (int)schemaTable.Rows[i]["ColumnOrdinal"];
				Type type = (Type)schemaTable.Rows[i]["DataType"];
				bool flag2 = (bool)schemaTable.Rows[i]["AllowDBNull"];
				ColumnType columnType = Column.GetColumnType(type);
				object obj = (flag ? schemaTable.Rows[i]["IsKey"] : false);
				bool flag3 = obj is bool && (bool)obj;
				binaryWriter.Write(text);
				binaryWriter.Write(guid.ToString());
				binaryWriter.Write((uint)dbpropid);
				if (writeColumnOrdinals)
				{
					binaryWriter.Write(num);
				}
				binaryWriter.Write((byte)columnType);
				binaryWriter.Write(flag2);
				binaryWriter.Write(flag3);
			}
			new ObjectWriter(binaryWriter).WriteProperties(schemaTable.ExtendedProperties);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000D948 File Offset: 0x0000BB48
		public void WritePage(IPage page)
		{
			this.WriteInt32(page.RowCount);
			for (int i = 0; i < page.ColumnCount; i++)
			{
				((Column)page.GetColumn(i)).Serialize(this);
			}
			this.WriteExceptionRows(page.ExceptionRows);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000D990 File Offset: 0x0000BB90
		public void WriteExceptionRows(IDictionary<int, IExceptionRow> exceptionRows)
		{
			this.WriteInt32(exceptionRows.Count);
			foreach (KeyValuePair<int, IExceptionRow> keyValuePair in exceptionRows)
			{
				this.WriteInt32(keyValuePair.Key);
				this.WriteExceptionRow(keyValuePair.Value);
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000D9F8 File Offset: 0x0000BBF8
		private void WriteExceptionRow(IExceptionRow row)
		{
			this.WriteInt32(row.Exceptions.Count);
			foreach (KeyValuePair<int, IDictionary<string, string>> keyValuePair in row.Exceptions)
			{
				this.WriteInt32(keyValuePair.Key);
				this.WriteException(keyValuePair.Value);
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000DA6C File Offset: 0x0000BC6C
		private void WriteException(IDictionary<string, string> exception)
		{
			this.WriteInt32(exception.Count);
			foreach (KeyValuePair<string, string> keyValuePair in exception)
			{
				this.WriteString(keyValuePair.Key);
				this.WriteString(keyValuePair.Value);
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000DAD4 File Offset: 0x0000BCD4
		public void WriteArray(byte[] values, int offset, int count)
		{
			this.stream.Write(values, offset, count);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000DAE4 File Offset: 0x0000BCE4
		public unsafe void WriteArray(ushort[] values, int offset, int count)
		{
			PageWriter.CheckArray(values.Length, offset, count);
			if (count > 0)
			{
				fixed (ushort* ptr = &values[offset])
				{
					ushort* ptr2 = ptr;
					this.Write((void*)ptr2, count * 2);
				}
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000DB18 File Offset: 0x0000BD18
		public unsafe void WriteArray(uint[] values, int offset, int count)
		{
			PageWriter.CheckArray(values.Length, offset, count);
			if (count > 0)
			{
				fixed (uint* ptr = &values[offset])
				{
					uint* ptr2 = ptr;
					this.Write((void*)ptr2, count * 4);
				}
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000DB4C File Offset: 0x0000BD4C
		public unsafe void WriteArray(int[] values, int offset, int count)
		{
			PageWriter.CheckArray(values.Length, offset, count);
			if (count > 0)
			{
				fixed (int* ptr = &values[offset])
				{
					int* ptr2 = ptr;
					this.Write((void*)ptr2, count * 4);
				}
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000DB80 File Offset: 0x0000BD80
		public unsafe void WriteArray(ulong[] values, int offset, int count)
		{
			PageWriter.CheckArray(values.Length, offset, count);
			if (count > 0)
			{
				fixed (ulong* ptr = &values[offset])
				{
					ulong* ptr2 = ptr;
					this.Write((void*)ptr2, count * 8);
				}
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000DBB4 File Offset: 0x0000BDB4
		public unsafe void WriteArray(decimal[] values, int offset, int count)
		{
			PageWriter.CheckArray(values.Length, offset, count);
			if (count > 0)
			{
				fixed (decimal* ptr = &values[offset])
				{
					decimal* ptr2 = ptr;
					this.Write((void*)ptr2, count * 16);
				}
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000DBE8 File Offset: 0x0000BDE8
		public unsafe void WriteArray(Number[] values, int offset, int count)
		{
			PageWriter.CheckArray(values.Length, offset, count);
			if (count > 0)
			{
				fixed (Number* ptr = &values[offset])
				{
					Number* ptr2 = ptr;
					this.Write((void*)ptr2, count * sizeof(Number));
				}
			}
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000DC20 File Offset: 0x0000BE20
		public unsafe void WriteArray(Guid[] values, int offset, int count)
		{
			PageWriter.CheckArray(values.Length, offset, count);
			if (count > 0)
			{
				fixed (Guid* ptr = &values[offset])
				{
					Guid* ptr2 = ptr;
					this.Write((void*)ptr2, count * sizeof(Guid));
				}
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000DC58 File Offset: 0x0000BE58
		public void WriteArray(Date[] values, int offset, int count)
		{
			this.WriteArray<Date>(values, offset, count, (Date value) => value.DateTime.ToBinary());
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000DC82 File Offset: 0x0000BE82
		public void WriteArray(Time[] values, int offset, int count)
		{
			this.WriteArray<Time>(values, offset, count, (Time value) => value.TimeSpan.Ticks);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000DCAC File Offset: 0x0000BEAC
		public void WriteArray(DateTime[] values, int offset, int count)
		{
			this.WriteArray<DateTime>(values, offset, count, (DateTime value) => value.ToBinary());
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000DCD8 File Offset: 0x0000BED8
		public void WriteArray(DateTimeOffset[] values, int offset, int count)
		{
			DateTime[] array = new DateTime[count];
			TimeSpan[] array2 = new TimeSpan[count];
			for (int i = 0; i < count; i++)
			{
				DateTimeOffset dateTimeOffset = values[offset + i];
				array[i] = dateTimeOffset.DateTime;
				array2[i] = dateTimeOffset.Offset;
			}
			this.WriteArray(array, 0, count);
			this.WriteArray(array2, 0, count);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000DD37 File Offset: 0x0000BF37
		public void WriteArray(TimeSpan[] values, int offset, int count)
		{
			this.WriteArray<TimeSpan>(values, offset, count, (TimeSpan value) => value.Ticks);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000DD64 File Offset: 0x0000BF64
		public unsafe void WriteArray(char[] values, int offset, int count)
		{
			PageWriter.CheckArray(values.Length, offset, count);
			if (count > 0)
			{
				fixed (char* ptr = &values[offset])
				{
					char* ptr2 = ptr;
					this.Write((void*)ptr2, count * 2);
				}
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000DD97 File Offset: 0x0000BF97
		public void WriteArray(string[] values, int offset, int count)
		{
			if (!this.TryWriteByteLengths(values, offset, count))
			{
				this.WriteIntLengths(values, offset, count);
			}
			if (!this.TryWriteByteStrings(values, offset, count))
			{
				this.WriteCharStrings(values, offset, count);
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000DDC4 File Offset: 0x0000BFC4
		public void WriteArray(byte[][] values, int offset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				byte[] array = values[offset + i];
				this.WriteInt32(array.Length);
				this.stream.Write(array, 0, array.Length);
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000DDFC File Offset: 0x0000BFFC
		public void WriteInt32(int value)
		{
			uint num;
			for (num = (uint)value; num >= 128U; num >>= 7)
			{
				this.stream.WriteByte((byte)((num & 127U) | 128U));
			}
			this.stream.WriteByte((byte)num);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000DE3C File Offset: 0x0000C03C
		public unsafe void WriteString(string value)
		{
			if (value == null)
			{
				this.WriteInt32(0);
				return;
			}
			this.WriteInt32(value.Length + 1);
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				this.Write((void*)ptr, value.Length * 2);
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000DE83 File Offset: 0x0000C083
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

		// Token: 0x06000496 RID: 1174 RVA: 0x0000DEB0 File Offset: 0x0000C0B0
		private void WriteArray<[global::System.Runtime.CompilerServices.Nullable(2)] T>(T[] values, int offset, int count, Func<T, long> func)
		{
			ulong[] array = new ulong[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = (ulong)func(values[offset + i]);
			}
			this.WriteArray(array, 0, count);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000DEEC File Offset: 0x0000C0EC
		private bool TryWriteByteLengths(string[] values, int offset, int count)
		{
			byte[] array = this.GetBuffer(count);
			for (int i = 0; i < count; i++)
			{
				int length = values[offset + i].Length;
				if (length > 255)
				{
					return false;
				}
				array[i] = (byte)length;
			}
			this.stream.WriteByte(1);
			this.stream.Write(this.buffer, 0, count);
			return true;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000DF48 File Offset: 0x0000C148
		private void WriteIntLengths(string[] values, int offset, int count)
		{
			int[] array = new int[count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = values[offset + i].Length;
			}
			this.stream.WriteByte(4);
			this.WriteArray(array, 0, array.Length);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000DF90 File Offset: 0x0000C190
		private bool TryWriteByteStrings(string[] values, int offset, int count)
		{
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				num += values[offset + i].Length;
			}
			byte[] array = this.GetBuffer(num);
			int num2 = 0;
			for (int j = 0; j < count; j++)
			{
				foreach (char c in values[offset + j])
				{
					if (c >= '\u0080')
					{
						return false;
					}
					array[num2++] = (byte)c;
				}
			}
			this.stream.WriteByte(1);
			this.stream.Write(array, 0, num);
			return true;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000E02C File Offset: 0x0000C22C
		private unsafe void WriteCharStrings(string[] values, int offset, int count)
		{
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				num += values[offset + i].Length;
			}
			char[] array = new char[num];
			num = 0;
			for (int j = 0; j < count; j++)
			{
				string text = values[offset + j];
				text.CopyTo(0, array, num, text.Length);
				num += text.Length;
			}
			this.stream.WriteByte(2);
			char[] array2;
			char* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			this.Write((void*)ptr, num * 2);
			array2 = null;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000E0C4 File Offset: 0x0000C2C4
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private unsafe void Write(void* src, int count)
		{
			int i = count;
			byte[] array;
			void* ptr;
			if ((array = this.GetBuffer(i)) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = (void*)(&array[0]);
			}
			ulong* ptr2 = (ulong*)src;
			ulong* ptr3 = (ulong*)ptr;
			while (i >= 8)
			{
				*(ptr3++) = *(ptr2++);
				i -= 8;
			}
			byte* ptr4 = (byte*)ptr2;
			byte* ptr5 = (byte*)ptr3;
			while (i >= 1)
			{
				*(ptr5++) = *(ptr4++);
				i--;
			}
			array = null;
			this.stream.Write(this.buffer, 0, count);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000E144 File Offset: 0x0000C344
		private byte[] GetBuffer(int length)
		{
			if (length > this.buffer.Length)
			{
				this.buffer = new byte[Math.Max(length, this.buffer.Length * 2)];
			}
			return this.buffer;
		}

		// Token: 0x040003EE RID: 1006
		private readonly Stream stream;

		// Token: 0x040003EF RID: 1007
		private byte[] buffer;
	}
}
