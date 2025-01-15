using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Microsoft.Mashup.Engine.Interface.Serialization
{
	// Token: 0x0200013B RID: 315
	public struct ObjectWriter
	{
		// Token: 0x06000575 RID: 1397 RVA: 0x00008987 File Offset: 0x00006B87
		public ObjectWriter(BinaryWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00008990 File Offset: 0x00006B90
		public void WriteObject(object value)
		{
			ObjectKind kind = ObjectWriter.GetKind(value);
			this.WriteKind(kind);
			switch (kind)
			{
			case ObjectKind.Null:
			case ObjectKind.DBNull:
				return;
			case ObjectKind.Int32:
				this.writer.Write((int)value);
				return;
			case ObjectKind.Int64:
				this.writer.Write((long)value);
				return;
			case ObjectKind.Double:
				this.writer.Write((double)value);
				return;
			case ObjectKind.Decimal:
				this.writer.Write((decimal)value);
				return;
			case ObjectKind.String:
				this.writer.Write((string)value);
				return;
			case ObjectKind.Boolean:
				this.writer.Write((bool)value);
				return;
			case ObjectKind.DateTime:
				this.WriteDateTime((DateTime)value);
				return;
			case ObjectKind.Int16:
				this.writer.Write((short)value);
				return;
			case ObjectKind.Byte:
				this.writer.Write((byte)value);
				return;
			case ObjectKind.Single:
				this.writer.Write((float)value);
				return;
			case ObjectKind.Binary:
				this.WriteBinary((byte[])value);
				return;
			case ObjectKind.TimeSpan:
				this.WriteTimeSpan((TimeSpan)value);
				return;
			case ObjectKind.DateTimeOffset:
				this.WriteDateTimeOffset((DateTimeOffset)value);
				return;
			case ObjectKind.Guid:
				this.WriteGuid((Guid)value);
				return;
			case ObjectKind.Type:
				this.WriteType((Type)value);
				return;
			case ObjectKind.SByte:
				this.writer.Write((sbyte)value);
				return;
			case ObjectKind.UInt16:
				this.writer.Write((ushort)value);
				return;
			case ObjectKind.UInt32:
				this.writer.Write((uint)value);
				return;
			case ObjectKind.UInt64:
				this.writer.Write((ulong)value);
				return;
			case ObjectKind.DataTable:
				this.WriteDataTable((DataTable)value);
				return;
			case ObjectKind.List:
				this.WriteList((IList<object>)value);
				return;
			case ObjectKind.Record:
				this.WriteRecord((IDictionary<string, object>)value);
				return;
			}
			throw new NotSupportedException(value.GetType().FullName);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00008B8C File Offset: 0x00006D8C
		private void WriteKind(ObjectKind tag)
		{
			this.writer.Write((byte)tag);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00008B9B File Offset: 0x00006D9B
		private void WriteDateTime(DateTime value)
		{
			this.writer.Write(value.ToBinary());
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00008BAF File Offset: 0x00006DAF
		private void WriteTimeSpan(TimeSpan value)
		{
			this.writer.Write(value.Ticks);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00008BC4 File Offset: 0x00006DC4
		private void WriteDateTimeOffset(DateTimeOffset value)
		{
			this.writer.Write(value.DateTime.ToBinary());
			this.writer.Write(value.Offset.Ticks);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00008C05 File Offset: 0x00006E05
		private void WriteGuid(Guid value)
		{
			this.writer.Write(value.ToByteArray());
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00008C19 File Offset: 0x00006E19
		private void WriteBinary(byte[] value)
		{
			this.writer.Write(value.Length);
			this.writer.Write(value);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00008C35 File Offset: 0x00006E35
		public void WriteType(Type type)
		{
			this.WriteKind(ObjectWriter.GetKind(type));
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00008C44 File Offset: 0x00006E44
		public void WriteList(IList<object> value)
		{
			this.writer.Write(value.Count);
			for (int i = 0; i < value.Count; i++)
			{
				this.WriteObject(value[i]);
			}
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00008C80 File Offset: 0x00006E80
		public void WriteRecord(IDictionary<string, object> value)
		{
			this.writer.Write(value.Count);
			foreach (KeyValuePair<string, object> keyValuePair in value)
			{
				this.writer.Write(keyValuePair.Key);
				this.WriteObject(keyValuePair.Value);
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00008CF4 File Offset: 0x00006EF4
		public void WriteDataTable(DataTable table)
		{
			using (table.CreateDataReader())
			{
				this.WriteColumns(table);
				this.WritePrimaryKey(table);
				this.WriteRows(table);
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00008D3C File Offset: 0x00006F3C
		private void WriteColumns(DataTable table)
		{
			this.writer.Write(table.Columns.Count);
			foreach (object obj in table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				this.writer.Write(dataColumn.ColumnName);
				this.WriteType(dataColumn.DataType);
				this.WriteProperties(dataColumn.ExtendedProperties);
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00008DD0 File Offset: 0x00006FD0
		private void WritePrimaryKey(DataTable table)
		{
			this.writer.Write(table.PrimaryKey.Length);
			foreach (DataColumn dataColumn in table.PrimaryKey)
			{
				this.writer.Write(dataColumn.ColumnName);
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00008E1C File Offset: 0x0000701C
		private void WriteProperties(PropertyCollection properties)
		{
			if (properties == null)
			{
				this.writer.Write(0);
				return;
			}
			this.writer.Write(properties.Count);
			foreach (object obj in properties)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.writer.Write((string)dictionaryEntry.Key);
				this.WriteObject(dictionaryEntry.Value);
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00008EB0 File Offset: 0x000070B0
		private void WriteRows(DataTable table)
		{
			int count = table.Columns.Count;
			this.writer.Write(table.Rows.Count);
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				for (int i = 0; i < count; i++)
				{
					this.WriteObject(dataRow[i]);
				}
			}
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00008F40 File Offset: 0x00007140
		private static ObjectKind GetKind(object value)
		{
			if (value == null)
			{
				return ObjectKind.Null;
			}
			if (value is Type)
			{
				return ObjectKind.Type;
			}
			if (value is IList<object>)
			{
				return ObjectKind.List;
			}
			if (value is IDictionary<string, object>)
			{
				return ObjectKind.Record;
			}
			return ObjectWriter.GetKind(value.GetType());
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00008F74 File Offset: 0x00007174
		private static ObjectKind GetKind(Type type)
		{
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.DBNull:
				return ObjectKind.DBNull;
			case TypeCode.Boolean:
				return ObjectKind.Boolean;
			case TypeCode.SByte:
				return ObjectKind.SByte;
			case TypeCode.Byte:
				return ObjectKind.Byte;
			case TypeCode.Int16:
				return ObjectKind.Int16;
			case TypeCode.UInt16:
				return ObjectKind.UInt16;
			case TypeCode.Int32:
				return ObjectKind.Int32;
			case TypeCode.UInt32:
				return ObjectKind.UInt32;
			case TypeCode.Int64:
				return ObjectKind.Int64;
			case TypeCode.UInt64:
				return ObjectKind.UInt64;
			case TypeCode.Single:
				return ObjectKind.Single;
			case TypeCode.Double:
				return ObjectKind.Double;
			case TypeCode.Decimal:
				return ObjectKind.Decimal;
			case TypeCode.DateTime:
				return ObjectKind.DateTime;
			case TypeCode.String:
				return ObjectKind.String;
			}
			if (type == typeof(TimeSpan))
			{
				return ObjectKind.TimeSpan;
			}
			if (type == typeof(DateTimeOffset))
			{
				return ObjectKind.DateTimeOffset;
			}
			if (type == typeof(Guid))
			{
				return ObjectKind.Guid;
			}
			if (type == typeof(byte[]))
			{
				return ObjectKind.Binary;
			}
			if (type == typeof(Type))
			{
				return ObjectKind.Type;
			}
			if (type == typeof(DataTable))
			{
				return ObjectKind.DataTable;
			}
			if (type == typeof(object))
			{
				return ObjectKind.Object;
			}
			throw new NotSupportedException(type.FullName);
		}

		// Token: 0x04000364 RID: 868
		private readonly BinaryWriter writer;
	}
}
