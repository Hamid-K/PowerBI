using System;
using System.Data;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine1.Library.Common;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200161F RID: 5663
	internal class TableDataMapper
	{
		// Token: 0x06008E98 RID: 36504 RVA: 0x001DB958 File Offset: 0x001D9B58
		public TableSchema MapTableType(TableTypeValue tableType)
		{
			TableSchema tableSchema = this.MapRecordType(tableType.ItemType, tableType.MetaValue);
			TableKey primaryKey = tableType.GetPrimaryKey();
			if (primaryKey != null)
			{
				foreach (int num in primaryKey.Columns)
				{
					if (num < tableSchema.ColumnCount)
					{
						tableSchema.GetColumn(num).IsKey = true;
					}
				}
			}
			return tableSchema;
		}

		// Token: 0x06008E99 RID: 36505 RVA: 0x001DB9B8 File Offset: 0x001D9BB8
		public TableSchema MapRecordType(RecordTypeValue recordType, RecordValue typeMetaValue)
		{
			RecordValue fields = recordType.Fields;
			int length = fields.Keys.Length;
			TableSchema tableSchema = new TableSchema(length);
			TableDataMapper.SetMetadata(tableSchema.ExtendedProperties, typeMetaValue);
			for (int i = 0; i < length; i++)
			{
				SchemaColumn schemaColumn = this.MapType(i, fields.Keys[i], fields[i]["Type"].AsType);
				schemaColumn.Nullable = true;
				schemaColumn.Ordinal = new int?(i);
				schemaColumn.IsKey = false;
				tableSchema.AddColumn(schemaColumn);
			}
			return tableSchema;
		}

		// Token: 0x06008E9A RID: 36506 RVA: 0x001DBA48 File Offset: 0x001D9C48
		public virtual SchemaColumn MapType(int position, string columnName, TypeValue fieldType)
		{
			TypeValue nonNullable = fieldType.NonNullable;
			return new SchemaColumn(columnName)
			{
				DataType = this.GetType(nonNullable),
				Nullable = fieldType.IsNullable,
				ProviderType = new int?((int)DbData.MapToDbType(nonNullable, DbType.Object)),
				DataTypeName = this.GetTypeName(nonNullable),
				ColumnSize = new long?(-1L)
			};
		}

		// Token: 0x06008E9B RID: 36507 RVA: 0x001DBAA8 File Offset: 0x001D9CA8
		public virtual object ConvertValue(Value value, SchemaColumn column)
		{
			return value.ToOleDb(column.DataType);
		}

		// Token: 0x06008E9C RID: 36508 RVA: 0x001DBAB6 File Offset: 0x001D9CB6
		protected virtual Type GetType(TypeValue type)
		{
			return type.ToClrType();
		}

		// Token: 0x06008E9D RID: 36509 RVA: 0x001DBAC0 File Offset: 0x001D9CC0
		public static void SetMetadata(PropertyCollection propertyCollection, RecordValue recordValue)
		{
			RecordValue fields = recordValue.Type.AsRecordType.Fields;
			for (int i = 0; i < recordValue.Count; i++)
			{
				try
				{
					string text;
					if (!PreviewServices.IsDelayed(fields[i]["Type"].AsType, out text))
					{
						propertyCollection[recordValue.Keys[i]] = ValueMarshaller.MarshalToClr(recordValue[i]);
					}
				}
				catch (ValueException)
				{
				}
			}
		}

		// Token: 0x06008E9E RID: 36510 RVA: 0x001DBB44 File Offset: 0x001D9D44
		private string GetTypeName(TypeValue type)
		{
			switch (type.TypeKind)
			{
			case ValueKind.Time:
				return "time";
			case ValueKind.Date:
				return "date";
			case ValueKind.DateTime:
				return "datetime";
			case ValueKind.DateTimeZone:
				return "datetimezone";
			case ValueKind.Duration:
				return "duration";
			case ValueKind.Number:
				if (type.Equals(TypeValue.Int16))
				{
					return "Int16.Type";
				}
				if (type.Equals(TypeValue.Int32))
				{
					return "Int32.Type";
				}
				if (type.Equals(TypeValue.Int64))
				{
					return "Int64.Type";
				}
				if (type.Equals(TypeValue.Int8))
				{
					return "Int8.Type";
				}
				if (type.Equals(TypeValue.Byte))
				{
					return "Byte.Type";
				}
				if (type.Equals(TypeValue.Single))
				{
					return "Single.Type";
				}
				if (type.Equals(TypeValue.Decimal))
				{
					return "Decimal.Type";
				}
				if (type.Equals(TypeValue.Double))
				{
					return "Double.Type";
				}
				if (type.Equals(TypeValue.Currency))
				{
					return "Currency.Type";
				}
				if (type.Equals(TypeValue.Percentage))
				{
					return "Percentage.Type";
				}
				return "number";
			case ValueKind.Logical:
				return "logical";
			case ValueKind.Text:
				if (type.Equals(TypeValue.Guid))
				{
					return "Guid.Type";
				}
				if (type.Equals(TypeValue.Character))
				{
					return "Character.Type";
				}
				if (type.Equals(TypeValue.Uri))
				{
					return "Uri.Type";
				}
				if (type.Equals(TypeValue.Password))
				{
					return "Password.Type";
				}
				return "text";
			case ValueKind.Binary:
				return "binary";
			}
			return "any";
		}

		// Token: 0x04004D5A RID: 19802
		public static readonly TableDataMapper Instance = new TableDataMapper();
	}
}
