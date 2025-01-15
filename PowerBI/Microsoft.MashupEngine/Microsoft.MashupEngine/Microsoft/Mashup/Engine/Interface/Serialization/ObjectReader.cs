using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;

namespace Microsoft.Mashup.Engine.Interface.Serialization
{
	// Token: 0x0200013A RID: 314
	public struct ObjectReader
	{
		// Token: 0x06000569 RID: 1385 RVA: 0x000083D0 File Offset: 0x000065D0
		public ObjectReader(BinaryReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x000083DC File Offset: 0x000065DC
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

		// Token: 0x0600056B RID: 1387 RVA: 0x000085EB File Offset: 0x000067EB
		private ObjectKind ReadKind()
		{
			return (ObjectKind)this.reader.ReadByte();
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x000085F8 File Offset: 0x000067F8
		public Type ReadType()
		{
			return ObjectReader.GetType(this.ReadKind());
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00008608 File Offset: 0x00006808
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

		// Token: 0x0600056E RID: 1390 RVA: 0x00008640 File Offset: 0x00006840
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

		// Token: 0x0600056F RID: 1391 RVA: 0x0000868C File Offset: 0x0000688C
		public DataTable ReadDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.Locale = CultureInfo.InvariantCulture;
			this.ReadColumns(dataTable);
			this.ReadPrimaryKey(dataTable);
			this.ReadRows(dataTable);
			return dataTable;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x000086C0 File Offset: 0x000068C0
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

		// Token: 0x06000571 RID: 1393 RVA: 0x00008714 File Offset: 0x00006914
		private void ReadPrimaryKey(DataTable table)
		{
			int num = this.reader.ReadInt32();
			if (num == 0)
			{
				return;
			}
			DataColumn[] array = new DataColumn[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = table.Columns[this.reader.ReadString()];
			}
			table.PrimaryKey = array;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00008764 File Offset: 0x00006964
		private void ReadProperties(PropertyCollection properties)
		{
			int num = this.reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string text = this.reader.ReadString();
				object obj = this.ReadObject();
				properties.Add(text, obj);
			}
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x000087A4 File Offset: 0x000069A4
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

		// Token: 0x06000574 RID: 1396 RVA: 0x00008800 File Offset: 0x00006A00
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

		// Token: 0x04000363 RID: 867
		private BinaryReader reader;
	}
}
