using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000DC RID: 220
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal struct ObjectWriter
	{
		// Token: 0x06000407 RID: 1031 RVA: 0x0000C083 File Offset: 0x0000A283
		public ObjectWriter(BinaryWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000C08C File Offset: 0x0000A28C
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

		// Token: 0x06000409 RID: 1033 RVA: 0x0000C288 File Offset: 0x0000A488
		private void WriteKind(ObjectKind tag)
		{
			this.writer.Write((byte)tag);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000C297 File Offset: 0x0000A497
		private void WriteDateTime(DateTime value)
		{
			this.writer.Write(value.ToBinary());
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000C2AB File Offset: 0x0000A4AB
		private void WriteTimeSpan(TimeSpan value)
		{
			this.writer.Write(value.Ticks);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000C2C0 File Offset: 0x0000A4C0
		private void WriteDateTimeOffset(DateTimeOffset value)
		{
			this.writer.Write(value.DateTime.ToBinary());
			this.writer.Write(value.Offset.Ticks);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000C301 File Offset: 0x0000A501
		private void WriteGuid(Guid value)
		{
			this.writer.Write(value.ToByteArray());
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000C315 File Offset: 0x0000A515
		private void WriteBinary(byte[] value)
		{
			this.writer.Write(value.Length);
			this.writer.Write(value);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000C331 File Offset: 0x0000A531
		public void WriteType(Type type)
		{
			this.WriteKind(ObjectWriter.GetKind(type));
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000C340 File Offset: 0x0000A540
		public void WriteList(IList<object> value)
		{
			this.writer.Write(value.Count);
			for (int i = 0; i < value.Count; i++)
			{
				this.WriteObject(value[i]);
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000C37C File Offset: 0x0000A57C
		public void WriteRecord(IDictionary<string, object> value)
		{
			this.writer.Write(value.Count);
			foreach (KeyValuePair<string, object> keyValuePair in value)
			{
				this.writer.Write(keyValuePair.Key);
				this.WriteObject(keyValuePair.Value);
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000C3F0 File Offset: 0x0000A5F0
		public void WriteDataTable(DataTable table)
		{
			using (table.CreateDataReader())
			{
				this.WriteColumns(table);
				this.WriteRows(table);
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000C430 File Offset: 0x0000A630
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

		// Token: 0x06000414 RID: 1044 RVA: 0x0000C4C4 File Offset: 0x0000A6C4
		public void WriteProperties(PropertyCollection properties)
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

		// Token: 0x06000415 RID: 1045 RVA: 0x0000C558 File Offset: 0x0000A758
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

		// Token: 0x06000416 RID: 1046 RVA: 0x0000C5E8 File Offset: 0x0000A7E8
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

		// Token: 0x06000417 RID: 1047 RVA: 0x0000C61C File Offset: 0x0000A81C
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

		// Token: 0x040003D0 RID: 976
		private readonly BinaryWriter writer;
	}
}
