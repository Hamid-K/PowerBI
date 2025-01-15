using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02001FBA RID: 8122
	internal struct ObjectWriter
	{
		// Token: 0x0600C61D RID: 50717 RVA: 0x00277C93 File Offset: 0x00275E93
		public ObjectWriter(BinaryWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x0600C61E RID: 50718 RVA: 0x00277C9C File Offset: 0x00275E9C
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

		// Token: 0x0600C61F RID: 50719 RVA: 0x00277E98 File Offset: 0x00276098
		private void WriteKind(ObjectKind tag)
		{
			this.writer.Write((byte)tag);
		}

		// Token: 0x0600C620 RID: 50720 RVA: 0x00277EA7 File Offset: 0x002760A7
		private void WriteDateTime(DateTime value)
		{
			this.writer.Write(value.ToBinary());
		}

		// Token: 0x0600C621 RID: 50721 RVA: 0x00277EBB File Offset: 0x002760BB
		private void WriteTimeSpan(TimeSpan value)
		{
			this.writer.Write(value.Ticks);
		}

		// Token: 0x0600C622 RID: 50722 RVA: 0x00277ED0 File Offset: 0x002760D0
		private void WriteDateTimeOffset(DateTimeOffset value)
		{
			this.writer.Write(value.DateTime.ToBinary());
			this.writer.Write(value.Offset.Ticks);
		}

		// Token: 0x0600C623 RID: 50723 RVA: 0x00277F11 File Offset: 0x00276111
		private void WriteGuid(Guid value)
		{
			this.writer.Write(value.ToByteArray());
		}

		// Token: 0x0600C624 RID: 50724 RVA: 0x00277F25 File Offset: 0x00276125
		private void WriteBinary(byte[] value)
		{
			this.writer.Write(value.Length);
			this.writer.Write(value);
		}

		// Token: 0x0600C625 RID: 50725 RVA: 0x00277F41 File Offset: 0x00276141
		public void WriteType(Type type)
		{
			this.WriteKind(ObjectWriter.GetKind(type));
		}

		// Token: 0x0600C626 RID: 50726 RVA: 0x00277F50 File Offset: 0x00276150
		public void WriteList(IList<object> value)
		{
			this.writer.Write(value.Count);
			for (int i = 0; i < value.Count; i++)
			{
				this.WriteObject(value[i]);
			}
		}

		// Token: 0x0600C627 RID: 50727 RVA: 0x00277F8C File Offset: 0x0027618C
		public void WriteRecord(IDictionary<string, object> value)
		{
			this.writer.Write(value.Count);
			foreach (KeyValuePair<string, object> keyValuePair in value)
			{
				this.writer.Write(keyValuePair.Key);
				this.WriteObject(keyValuePair.Value);
			}
		}

		// Token: 0x0600C628 RID: 50728 RVA: 0x00278000 File Offset: 0x00276200
		public void WriteDataTable(DataTable table)
		{
			using (table.CreateDataReader())
			{
				this.WriteColumns(table);
				this.WriteRows(table);
			}
		}

		// Token: 0x0600C629 RID: 50729 RVA: 0x00278040 File Offset: 0x00276240
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

		// Token: 0x0600C62A RID: 50730 RVA: 0x002780D4 File Offset: 0x002762D4
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

		// Token: 0x0600C62B RID: 50731 RVA: 0x00278168 File Offset: 0x00276368
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

		// Token: 0x0600C62C RID: 50732 RVA: 0x002781F8 File Offset: 0x002763F8
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

		// Token: 0x0600C62D RID: 50733 RVA: 0x0027822C File Offset: 0x0027642C
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

		// Token: 0x0400654A RID: 25930
		private readonly BinaryWriter writer;
	}
}
