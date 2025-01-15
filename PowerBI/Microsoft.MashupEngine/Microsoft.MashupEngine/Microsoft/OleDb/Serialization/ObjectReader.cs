using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02001FB9 RID: 8121
	internal struct ObjectReader
	{
		// Token: 0x0600C612 RID: 50706 RVA: 0x00277730 File Offset: 0x00275930
		public ObjectReader(BinaryReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x0600C613 RID: 50707 RVA: 0x0027773C File Offset: 0x0027593C
		public object ReadObject()
		{
			switch (this.ReadKind())
			{
			case ObjectKind.Null:
				return null;
			case ObjectKind.Int32:
				return this.reader.ReadInt32();
			case ObjectKind.Int64:
				return this.reader.ReadInt64();
			case ObjectKind.Double:
				return this.reader.ReadDouble();
			case ObjectKind.Decimal:
				return this.reader.ReadDecimal();
			case ObjectKind.String:
				return this.reader.ReadString();
			case ObjectKind.Boolean:
				return this.reader.ReadBoolean();
			case ObjectKind.DateTime:
				return DateTime.FromBinary(this.reader.ReadInt64());
			case ObjectKind.DBNull:
				return DBNull.Value;
			case ObjectKind.Int16:
				return this.reader.ReadInt16();
			case ObjectKind.Byte:
				return this.reader.ReadByte();
			case ObjectKind.Single:
				return this.reader.ReadSingle();
			case ObjectKind.Binary:
				return this.reader.ReadBytes(this.reader.ReadInt32());
			case ObjectKind.TimeSpan:
				return new TimeSpan(this.reader.ReadInt64());
			case ObjectKind.DateTimeOffset:
			{
				DateTime dateTime = DateTime.FromBinary(this.reader.ReadInt64());
				TimeSpan timeSpan = new TimeSpan(this.reader.ReadInt64());
				return new DateTimeOffset(dateTime, timeSpan);
			}
			case ObjectKind.Guid:
				return new Guid(this.reader.ReadBytes(16));
			case ObjectKind.Type:
				return this.ReadType();
			case ObjectKind.SByte:
				return this.reader.ReadSByte();
			case ObjectKind.UInt16:
				return this.reader.ReadUInt16();
			case ObjectKind.UInt32:
				return this.reader.ReadUInt32();
			case ObjectKind.UInt64:
				return this.reader.ReadUInt64();
			case ObjectKind.DataTable:
				return this.ReadDataTable();
			case ObjectKind.List:
				return this.ReadList();
			case ObjectKind.Record:
				return this.ReadRecord();
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600C614 RID: 50708 RVA: 0x0027794B File Offset: 0x00275B4B
		private ObjectKind ReadKind()
		{
			return (ObjectKind)this.reader.ReadByte();
		}

		// Token: 0x0600C615 RID: 50709 RVA: 0x00277958 File Offset: 0x00275B58
		public Type ReadType()
		{
			return ObjectReader.GetType(this.ReadKind());
		}

		// Token: 0x0600C616 RID: 50710 RVA: 0x00277968 File Offset: 0x00275B68
		public IList<object> ReadList()
		{
			int num = this.reader.ReadInt32();
			object[] array = new object[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = this.ReadObject();
			}
			return array;
		}

		// Token: 0x0600C617 RID: 50711 RVA: 0x002779A0 File Offset: 0x00275BA0
		public IDictionary<string, object> ReadRecord()
		{
			int num = this.reader.ReadInt32();
			Dictionary<string, object> dictionary = new Dictionary<string, object>(num);
			for (int i = 0; i < num; i++)
			{
				string text = this.reader.ReadString();
				object obj = this.ReadObject();
				dictionary.Add(text, obj);
			}
			return dictionary;
		}

		// Token: 0x0600C618 RID: 50712 RVA: 0x002779EC File Offset: 0x00275BEC
		public DataTable ReadDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.Locale = CultureInfo.InvariantCulture;
			this.ReadColumns(dataTable);
			this.ReadRows(dataTable);
			return dataTable;
		}

		// Token: 0x0600C619 RID: 50713 RVA: 0x00277A1C File Offset: 0x00275C1C
		private void ReadColumns(DataTable table)
		{
			int num = this.reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string text = this.reader.ReadString();
				Type type = this.ReadType();
				DataColumn dataColumn = table.Columns.Add(text, type);
				this.ReadProperties(dataColumn.ExtendedProperties);
			}
		}

		// Token: 0x0600C61A RID: 50714 RVA: 0x00277A70 File Offset: 0x00275C70
		public void ReadProperties(PropertyCollection properties)
		{
			int num = this.reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string text = this.reader.ReadString();
				object obj = this.ReadObject();
				properties.Add(text, obj);
			}
		}

		// Token: 0x0600C61B RID: 50715 RVA: 0x00277AB0 File Offset: 0x00275CB0
		private void ReadRows(DataTable table)
		{
			int num = this.reader.ReadInt32();
			object[] array = new object[table.Columns.Count];
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < array.Length; j++)
				{
					array[j] = this.ReadObject();
				}
				table.Rows.Add(array);
			}
		}

		// Token: 0x0600C61C RID: 50716 RVA: 0x00277B0C File Offset: 0x00275D0C
		private static Type GetType(ObjectKind kind)
		{
			switch (kind)
			{
			case ObjectKind.Int32:
				return typeof(int);
			case ObjectKind.Int64:
				return typeof(long);
			case ObjectKind.Double:
				return typeof(double);
			case ObjectKind.Decimal:
				return typeof(decimal);
			case ObjectKind.String:
				return typeof(string);
			case ObjectKind.Boolean:
				return typeof(bool);
			case ObjectKind.DateTime:
				return typeof(DateTime);
			case ObjectKind.DBNull:
				return typeof(DBNull);
			case ObjectKind.Int16:
				return typeof(short);
			case ObjectKind.Byte:
				return typeof(byte);
			case ObjectKind.Single:
				return typeof(float);
			case ObjectKind.Binary:
				return typeof(byte[]);
			case ObjectKind.TimeSpan:
				return typeof(TimeSpan);
			case ObjectKind.DateTimeOffset:
				return typeof(DateTimeOffset);
			case ObjectKind.Guid:
				return typeof(Guid);
			case ObjectKind.Type:
				return typeof(Type);
			case ObjectKind.SByte:
				return typeof(sbyte);
			case ObjectKind.UInt16:
				return typeof(ushort);
			case ObjectKind.UInt32:
				return typeof(uint);
			case ObjectKind.UInt64:
				return typeof(ulong);
			case ObjectKind.DataTable:
				return typeof(DataTable);
			case ObjectKind.Object:
				return typeof(object);
			case ObjectKind.List:
				return typeof(IList<object>);
			case ObjectKind.Record:
				return typeof(IDictionary<string, object>);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x04006549 RID: 25929
		private BinaryReader reader;
	}
}
