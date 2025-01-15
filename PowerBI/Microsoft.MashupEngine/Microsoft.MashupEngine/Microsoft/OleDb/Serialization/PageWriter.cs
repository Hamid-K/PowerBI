using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Data.Serialization;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02001FC5 RID: 8133
	public class PageWriter : IDisposable
	{
		// Token: 0x0600C6AB RID: 50859 RVA: 0x00279466 File Offset: 0x00277666
		public PageWriter(Stream stream)
		{
			this.stream = stream;
			this.buffer = new byte[1024];
		}

		// Token: 0x0600C6AC RID: 50860 RVA: 0x00279485 File Offset: 0x00277685
		public void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x0600C6AD RID: 50861 RVA: 0x00279492 File Offset: 0x00277692
		public void Dispose()
		{
			this.stream.Close();
		}

		// Token: 0x0600C6AE RID: 50862 RVA: 0x002794A0 File Offset: 0x002776A0
		public void WriteSchema(TableSchema schema)
		{
			BinaryWriter binaryWriter = new BinaryWriter(this.stream);
			this.WriteSchema(binaryWriter, schema);
		}

		// Token: 0x0600C6AF RID: 50863 RVA: 0x002794C4 File Offset: 0x002776C4
		private void WriteSchema(BinaryWriter writer, TableSchema schema)
		{
			if (schema == null)
			{
				writer.Write(-1);
				return;
			}
			writer.Write(schema.ColumnCount);
			foreach (SchemaColumn schemaColumn in schema)
			{
				bool flag;
				ColumnType columnType = Column.GetColumnType(schemaColumn.DataType, out flag);
				writer.Write(schemaColumn.Name);
				writer.Write((byte)columnType);
				writer.Write(schemaColumn.Nullable);
				writer.Write(flag);
				writer.Write(schemaColumn.IsKey);
				this.WriteSchema(writer, schemaColumn.ColumnSchema);
			}
			if (schema.ExtendedProperties.Count > 0)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					new ObjectWriter(new BinaryWriter(memoryStream)).WriteProperties(schema.ExtendedProperties);
					byte[] array = memoryStream.ToArray();
					writer.Write(array.Length);
					writer.Write(array);
					return;
				}
			}
			writer.Write(0);
		}

		// Token: 0x0600C6B0 RID: 50864 RVA: 0x002795D8 File Offset: 0x002777D8
		public void WritePage(IPage page)
		{
			this.WriteInt32(page.RowCount);
			for (int i = 0; i < page.ColumnCount; i++)
			{
				((Column)page.GetColumn(i)).Serialize(this);
			}
			this.WriteExceptionRows(page.ExceptionRows);
			bool flag = page.PageException != null;
			this.WriteInt32((flag > false) ? 1 : 0);
			if (flag)
			{
				this.WriteException(page.PageException);
			}
		}

		// Token: 0x0600C6B1 RID: 50865 RVA: 0x00279644 File Offset: 0x00277844
		public void WriteExceptionRows(IDictionary<int, IExceptionRow> exceptionRows)
		{
			this.WriteInt32(exceptionRows.Count);
			foreach (KeyValuePair<int, IExceptionRow> keyValuePair in exceptionRows)
			{
				this.WriteInt32(keyValuePair.Key);
				this.WriteExceptionRow(keyValuePair.Value);
			}
		}

		// Token: 0x0600C6B2 RID: 50866 RVA: 0x002796AC File Offset: 0x002778AC
		private void WriteExceptionRow(IExceptionRow row)
		{
			this.WriteInt32(row.Exceptions.Count);
			foreach (KeyValuePair<int, ISerializedException> keyValuePair in row.Exceptions)
			{
				this.WriteInt32(keyValuePair.Key);
				this.WriteException(keyValuePair.Value);
			}
		}

		// Token: 0x0600C6B3 RID: 50867 RVA: 0x00279720 File Offset: 0x00277920
		private void WriteException(ISerializedException exception)
		{
			this.WriteInt32(exception.Count);
			foreach (KeyValuePair<string, string> keyValuePair in exception)
			{
				this.WriteString(keyValuePair.Key);
				this.WriteString(keyValuePair.Value);
			}
		}

		// Token: 0x0600C6B4 RID: 50868 RVA: 0x00279788 File Offset: 0x00277988
		public void WriteArray(byte[] values, int offset, int count)
		{
			this.stream.Write(values, offset, count);
		}

		// Token: 0x0600C6B5 RID: 50869 RVA: 0x00279798 File Offset: 0x00277998
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

		// Token: 0x0600C6B6 RID: 50870 RVA: 0x002797CC File Offset: 0x002779CC
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

		// Token: 0x0600C6B7 RID: 50871 RVA: 0x00279800 File Offset: 0x00277A00
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

		// Token: 0x0600C6B8 RID: 50872 RVA: 0x00279834 File Offset: 0x00277A34
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

		// Token: 0x0600C6B9 RID: 50873 RVA: 0x00279868 File Offset: 0x00277A68
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

		// Token: 0x0600C6BA RID: 50874 RVA: 0x0027989C File Offset: 0x00277A9C
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

		// Token: 0x0600C6BB RID: 50875 RVA: 0x002798D4 File Offset: 0x00277AD4
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

		// Token: 0x0600C6BC RID: 50876 RVA: 0x0027990C File Offset: 0x00277B0C
		public void WriteArray(Date[] values, int offset, int count)
		{
			this.WriteArray<Date>(values, offset, count, (Date value) => value.DateTime.ToBinary());
		}

		// Token: 0x0600C6BD RID: 50877 RVA: 0x00279936 File Offset: 0x00277B36
		public void WriteArray(Time[] values, int offset, int count)
		{
			this.WriteArray<Time>(values, offset, count, (Time value) => value.TimeSpan.Ticks);
		}

		// Token: 0x0600C6BE RID: 50878 RVA: 0x00279960 File Offset: 0x00277B60
		public void WriteArray(DateTime[] values, int offset, int count)
		{
			this.WriteArray<DateTime>(values, offset, count, (DateTime value) => value.ToBinary());
		}

		// Token: 0x0600C6BF RID: 50879 RVA: 0x0027998C File Offset: 0x00277B8C
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

		// Token: 0x0600C6C0 RID: 50880 RVA: 0x002799EB File Offset: 0x00277BEB
		public void WriteArray(TimeSpan[] values, int offset, int count)
		{
			this.WriteArray<TimeSpan>(values, offset, count, (TimeSpan value) => value.Ticks);
		}

		// Token: 0x0600C6C1 RID: 50881 RVA: 0x00279A18 File Offset: 0x00277C18
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

		// Token: 0x0600C6C2 RID: 50882 RVA: 0x00279A4B File Offset: 0x00277C4B
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

		// Token: 0x0600C6C3 RID: 50883 RVA: 0x00279A78 File Offset: 0x00277C78
		public void WriteArray(byte[][] values, int offset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				byte[] array = values[offset + i];
				this.WriteInt32(array.Length);
				this.stream.Write(array, 0, array.Length);
			}
		}

		// Token: 0x0600C6C4 RID: 50884 RVA: 0x00279AB0 File Offset: 0x00277CB0
		public void WriteBool(bool value)
		{
			this.stream.WriteByte((value > false) ? 1 : 0);
		}

		// Token: 0x0600C6C5 RID: 50885 RVA: 0x00279AC4 File Offset: 0x00277CC4
		public void WriteInt32(int value)
		{
			uint num;
			for (num = (uint)value; num >= 128U; num >>= 7)
			{
				this.stream.WriteByte((byte)((num & 127U) | 128U));
			}
			this.stream.WriteByte((byte)num);
		}

		// Token: 0x0600C6C6 RID: 50886 RVA: 0x00279B04 File Offset: 0x00277D04
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

		// Token: 0x0600C6C7 RID: 50887 RVA: 0x00278DA2 File Offset: 0x00276FA2
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

		// Token: 0x0600C6C8 RID: 50888 RVA: 0x00279B4C File Offset: 0x00277D4C
		private void WriteArray<T>(T[] values, int offset, int count, Func<T, long> func)
		{
			ulong[] array = new ulong[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = (ulong)func(values[offset + i]);
			}
			this.WriteArray(array, 0, count);
		}

		// Token: 0x0600C6C9 RID: 50889 RVA: 0x00279B88 File Offset: 0x00277D88
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

		// Token: 0x0600C6CA RID: 50890 RVA: 0x00279BE4 File Offset: 0x00277DE4
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

		// Token: 0x0600C6CB RID: 50891 RVA: 0x00279C2C File Offset: 0x00277E2C
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

		// Token: 0x0600C6CC RID: 50892 RVA: 0x00279CC8 File Offset: 0x00277EC8
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

		// Token: 0x0600C6CD RID: 50893 RVA: 0x00279D60 File Offset: 0x00277F60
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

		// Token: 0x0600C6CE RID: 50894 RVA: 0x00279DE0 File Offset: 0x00277FE0
		private byte[] GetBuffer(int length)
		{
			if (length > this.buffer.Length)
			{
				this.buffer = new byte[Math.Max(length, this.buffer.Length * 2)];
			}
			return this.buffer;
		}

		// Token: 0x04006574 RID: 25972
		private readonly Stream stream;

		// Token: 0x04006575 RID: 25973
		private byte[] buffer;
	}
}
