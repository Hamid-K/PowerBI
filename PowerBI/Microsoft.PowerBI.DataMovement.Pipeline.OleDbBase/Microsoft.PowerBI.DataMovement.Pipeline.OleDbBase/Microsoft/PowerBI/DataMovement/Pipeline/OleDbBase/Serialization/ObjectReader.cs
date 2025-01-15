using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000DB RID: 219
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal struct ObjectReader
	{
		// Token: 0x060003FC RID: 1020 RVA: 0x0000BB22 File Offset: 0x00009D22
		public ObjectReader(BinaryReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000BB2C File Offset: 0x00009D2C
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

		// Token: 0x060003FE RID: 1022 RVA: 0x0000BD3B File Offset: 0x00009F3B
		private ObjectKind ReadKind()
		{
			return (ObjectKind)this.reader.ReadByte();
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000BD48 File Offset: 0x00009F48
		public Type ReadType()
		{
			return ObjectReader.GetType(this.ReadKind());
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000BD58 File Offset: 0x00009F58
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

		// Token: 0x06000401 RID: 1025 RVA: 0x0000BD90 File Offset: 0x00009F90
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

		// Token: 0x06000402 RID: 1026 RVA: 0x0000BDDC File Offset: 0x00009FDC
		public DataTable ReadDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.Locale = CultureInfo.InvariantCulture;
			this.ReadColumns(dataTable);
			this.ReadRows(dataTable);
			return dataTable;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000BE0C File Offset: 0x0000A00C
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

		// Token: 0x06000404 RID: 1028 RVA: 0x0000BE60 File Offset: 0x0000A060
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

		// Token: 0x06000405 RID: 1029 RVA: 0x0000BEA0 File Offset: 0x0000A0A0
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

		// Token: 0x06000406 RID: 1030 RVA: 0x0000BEFC File Offset: 0x0000A0FC
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

		// Token: 0x040003CF RID: 975
		private BinaryReader reader;
	}
}
