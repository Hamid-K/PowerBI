using System;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace Microsoft.Data.Serialization
{
	// Token: 0x0200015B RID: 347
	public static class TableSchemaExtensions
	{
		// Token: 0x06000661 RID: 1633 RVA: 0x0000A6A8 File Offset: 0x000088A8
		public static IDataReaderWithTableSchema WithTableSchema(this IDataReader reader)
		{
			IDataReaderWithTableSchema dataReaderWithTableSchema = reader as IDataReaderWithTableSchema;
			if (dataReaderWithTableSchema == null)
			{
				dataReaderWithTableSchema = new TableSchemaExtensions.DataReaderWithTableSchema(reader);
			}
			return dataReaderWithTableSchema;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0000A6C8 File Offset: 0x000088C8
		public static DbDataReaderWithTableSchema WithTableSchema(this DbDataReader reader)
		{
			DbDataReaderWithTableSchema dbDataReaderWithTableSchema = reader as DbDataReaderWithTableSchema;
			if (dbDataReaderWithTableSchema == null)
			{
				dbDataReaderWithTableSchema = new TableSchemaExtensions.DbDataReaderWithSchema(reader);
			}
			return dbDataReaderWithTableSchema;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0000A6E8 File Offset: 0x000088E8
		public static DbDataReader Unwrap(this DbDataReaderWithTableSchema reader)
		{
			TableSchemaExtensions.DbDataReaderWithSchema dbDataReaderWithSchema = reader as TableSchemaExtensions.DbDataReaderWithSchema;
			if (dbDataReaderWithSchema != null)
			{
				return dbDataReaderWithSchema.Reader;
			}
			return reader;
		}

		// Token: 0x0200015C RID: 348
		private sealed class DataReaderWithTableSchema : IDataReaderWithTableSchema, IDataReader, IDisposable, IDataRecord
		{
			// Token: 0x06000664 RID: 1636 RVA: 0x0000A707 File Offset: 0x00008907
			public DataReaderWithTableSchema(IDataReader reader)
			{
				this.reader = reader;
			}

			// Token: 0x06000665 RID: 1637 RVA: 0x0000A716 File Offset: 0x00008916
			public void Close()
			{
				this.reader.Close();
			}

			// Token: 0x17000220 RID: 544
			// (get) Token: 0x06000666 RID: 1638 RVA: 0x0000A723 File Offset: 0x00008923
			public int Depth
			{
				get
				{
					return this.reader.Depth;
				}
			}

			// Token: 0x17000221 RID: 545
			// (get) Token: 0x06000667 RID: 1639 RVA: 0x0000A730 File Offset: 0x00008930
			public TableSchema Schema
			{
				get
				{
					if (this.schema == null)
					{
						this.schema = TableSchema.FromDataReader(this.reader);
					}
					return this.schema;
				}
			}

			// Token: 0x06000668 RID: 1640 RVA: 0x0000A751 File Offset: 0x00008951
			[Obsolete]
			public DataTable GetSchemaTable()
			{
				return this.reader.GetSchemaTable();
			}

			// Token: 0x17000222 RID: 546
			// (get) Token: 0x06000669 RID: 1641 RVA: 0x0000A75E File Offset: 0x0000895E
			public bool IsClosed
			{
				get
				{
					return this.reader.IsClosed;
				}
			}

			// Token: 0x0600066A RID: 1642 RVA: 0x0000A76B File Offset: 0x0000896B
			public bool NextResult()
			{
				return this.reader.NextResult();
			}

			// Token: 0x0600066B RID: 1643 RVA: 0x0000A778 File Offset: 0x00008978
			public bool Read()
			{
				return this.reader.Read();
			}

			// Token: 0x17000223 RID: 547
			// (get) Token: 0x0600066C RID: 1644 RVA: 0x0000A785 File Offset: 0x00008985
			public int RecordsAffected
			{
				get
				{
					return this.reader.RecordsAffected;
				}
			}

			// Token: 0x0600066D RID: 1645 RVA: 0x0000A792 File Offset: 0x00008992
			public void Dispose()
			{
				this.reader.Dispose();
			}

			// Token: 0x17000224 RID: 548
			// (get) Token: 0x0600066E RID: 1646 RVA: 0x0000A79F File Offset: 0x0000899F
			public int FieldCount
			{
				get
				{
					return this.reader.FieldCount;
				}
			}

			// Token: 0x0600066F RID: 1647 RVA: 0x0000A7AC File Offset: 0x000089AC
			public bool GetBoolean(int i)
			{
				return this.reader.GetBoolean(i);
			}

			// Token: 0x06000670 RID: 1648 RVA: 0x0000A7BA File Offset: 0x000089BA
			public byte GetByte(int i)
			{
				return this.reader.GetByte(i);
			}

			// Token: 0x06000671 RID: 1649 RVA: 0x0000A7C8 File Offset: 0x000089C8
			public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
			{
				return this.reader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
			}

			// Token: 0x06000672 RID: 1650 RVA: 0x0000A7DC File Offset: 0x000089DC
			public char GetChar(int i)
			{
				return this.reader.GetChar(i);
			}

			// Token: 0x06000673 RID: 1651 RVA: 0x0000A7EA File Offset: 0x000089EA
			public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
			{
				return this.reader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
			}

			// Token: 0x06000674 RID: 1652 RVA: 0x0000A7FE File Offset: 0x000089FE
			public IDataReader GetData(int i)
			{
				return this.reader.GetData(i);
			}

			// Token: 0x06000675 RID: 1653 RVA: 0x0000A80C File Offset: 0x00008A0C
			public string GetDataTypeName(int i)
			{
				return this.reader.GetDataTypeName(i);
			}

			// Token: 0x06000676 RID: 1654 RVA: 0x0000A81A File Offset: 0x00008A1A
			public DateTime GetDateTime(int i)
			{
				return this.reader.GetDateTime(i);
			}

			// Token: 0x06000677 RID: 1655 RVA: 0x0000A828 File Offset: 0x00008A28
			public decimal GetDecimal(int i)
			{
				return this.reader.GetDecimal(i);
			}

			// Token: 0x06000678 RID: 1656 RVA: 0x0000A836 File Offset: 0x00008A36
			public double GetDouble(int i)
			{
				return this.reader.GetDouble(i);
			}

			// Token: 0x06000679 RID: 1657 RVA: 0x0000A844 File Offset: 0x00008A44
			public Type GetFieldType(int i)
			{
				return this.reader.GetFieldType(i);
			}

			// Token: 0x0600067A RID: 1658 RVA: 0x0000A852 File Offset: 0x00008A52
			public float GetFloat(int i)
			{
				return this.reader.GetFloat(i);
			}

			// Token: 0x0600067B RID: 1659 RVA: 0x0000A860 File Offset: 0x00008A60
			public Guid GetGuid(int i)
			{
				return this.reader.GetGuid(i);
			}

			// Token: 0x0600067C RID: 1660 RVA: 0x0000A86E File Offset: 0x00008A6E
			public short GetInt16(int i)
			{
				return this.reader.GetInt16(i);
			}

			// Token: 0x0600067D RID: 1661 RVA: 0x0000A87C File Offset: 0x00008A7C
			public int GetInt32(int i)
			{
				return this.reader.GetInt32(i);
			}

			// Token: 0x0600067E RID: 1662 RVA: 0x0000A88A File Offset: 0x00008A8A
			public long GetInt64(int i)
			{
				return this.reader.GetInt64(i);
			}

			// Token: 0x0600067F RID: 1663 RVA: 0x0000A898 File Offset: 0x00008A98
			public string GetName(int i)
			{
				return this.reader.GetName(i);
			}

			// Token: 0x06000680 RID: 1664 RVA: 0x0000A8A6 File Offset: 0x00008AA6
			public int GetOrdinal(string name)
			{
				return this.reader.GetOrdinal(name);
			}

			// Token: 0x06000681 RID: 1665 RVA: 0x0000A8B4 File Offset: 0x00008AB4
			public string GetString(int i)
			{
				return this.reader.GetString(i);
			}

			// Token: 0x06000682 RID: 1666 RVA: 0x0000A8C2 File Offset: 0x00008AC2
			public object GetValue(int i)
			{
				return this.reader.GetValue(i);
			}

			// Token: 0x06000683 RID: 1667 RVA: 0x0000A8D0 File Offset: 0x00008AD0
			public int GetValues(object[] values)
			{
				return this.reader.GetValues(values);
			}

			// Token: 0x06000684 RID: 1668 RVA: 0x0000A8DE File Offset: 0x00008ADE
			public bool IsDBNull(int i)
			{
				return this.reader.IsDBNull(i);
			}

			// Token: 0x17000225 RID: 549
			public object this[string name]
			{
				get
				{
					return this.reader[name];
				}
			}

			// Token: 0x17000226 RID: 550
			public object this[int i]
			{
				get
				{
					return this.reader[i];
				}
			}

			// Token: 0x040003F3 RID: 1011
			private readonly IDataReader reader;

			// Token: 0x040003F4 RID: 1012
			private TableSchema schema;
		}

		// Token: 0x0200015D RID: 349
		private sealed class DbDataReaderWithSchema : DbDataReaderWithTableSchema
		{
			// Token: 0x06000687 RID: 1671 RVA: 0x0000A908 File Offset: 0x00008B08
			public DbDataReaderWithSchema(DbDataReader reader)
			{
				this.reader = reader;
			}

			// Token: 0x17000227 RID: 551
			// (get) Token: 0x06000688 RID: 1672 RVA: 0x0000A917 File Offset: 0x00008B17
			public DbDataReader Reader
			{
				get
				{
					return this.reader;
				}
			}

			// Token: 0x06000689 RID: 1673 RVA: 0x0000A91F File Offset: 0x00008B1F
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					this.reader.Dispose();
				}
			}

			// Token: 0x0600068A RID: 1674 RVA: 0x0000A92F File Offset: 0x00008B2F
			public override void Close()
			{
				this.reader.Close();
			}

			// Token: 0x17000228 RID: 552
			// (get) Token: 0x0600068B RID: 1675 RVA: 0x0000A93C File Offset: 0x00008B3C
			public override int Depth
			{
				get
				{
					return this.reader.Depth;
				}
			}

			// Token: 0x17000229 RID: 553
			// (get) Token: 0x0600068C RID: 1676 RVA: 0x0000A949 File Offset: 0x00008B49
			public override int FieldCount
			{
				get
				{
					return this.reader.FieldCount;
				}
			}

			// Token: 0x1700022A RID: 554
			// (get) Token: 0x0600068D RID: 1677 RVA: 0x0000A956 File Offset: 0x00008B56
			public override TableSchema Schema
			{
				get
				{
					if (this.schema == null)
					{
						this.schema = TableSchema.FromDataReader(this.reader);
					}
					return this.schema;
				}
			}

			// Token: 0x0600068E RID: 1678 RVA: 0x0000A977 File Offset: 0x00008B77
			public override bool GetBoolean(int ordinal)
			{
				return this.reader.GetBoolean(ordinal);
			}

			// Token: 0x0600068F RID: 1679 RVA: 0x0000A985 File Offset: 0x00008B85
			public override byte GetByte(int ordinal)
			{
				return this.reader.GetByte(ordinal);
			}

			// Token: 0x06000690 RID: 1680 RVA: 0x0000A993 File Offset: 0x00008B93
			public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
			{
				return this.reader.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
			}

			// Token: 0x06000691 RID: 1681 RVA: 0x0000A9A7 File Offset: 0x00008BA7
			public override char GetChar(int ordinal)
			{
				return this.reader.GetChar(ordinal);
			}

			// Token: 0x06000692 RID: 1682 RVA: 0x0000A9B5 File Offset: 0x00008BB5
			public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
			{
				return this.reader.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
			}

			// Token: 0x06000693 RID: 1683 RVA: 0x0000A9C9 File Offset: 0x00008BC9
			public override string GetDataTypeName(int ordinal)
			{
				return this.reader.GetDataTypeName(ordinal);
			}

			// Token: 0x06000694 RID: 1684 RVA: 0x0000A9D7 File Offset: 0x00008BD7
			public override DateTime GetDateTime(int ordinal)
			{
				return this.reader.GetDateTime(ordinal);
			}

			// Token: 0x06000695 RID: 1685 RVA: 0x0000A9E5 File Offset: 0x00008BE5
			public override decimal GetDecimal(int ordinal)
			{
				return this.reader.GetDecimal(ordinal);
			}

			// Token: 0x06000696 RID: 1686 RVA: 0x0000A9F3 File Offset: 0x00008BF3
			public override double GetDouble(int ordinal)
			{
				return this.reader.GetDouble(ordinal);
			}

			// Token: 0x06000697 RID: 1687 RVA: 0x0000AA01 File Offset: 0x00008C01
			public override IEnumerator GetEnumerator()
			{
				return this.reader.GetEnumerator();
			}

			// Token: 0x06000698 RID: 1688 RVA: 0x0000AA0E File Offset: 0x00008C0E
			public override Type GetFieldType(int ordinal)
			{
				return this.reader.GetFieldType(ordinal);
			}

			// Token: 0x06000699 RID: 1689 RVA: 0x0000AA1C File Offset: 0x00008C1C
			public override float GetFloat(int ordinal)
			{
				return this.reader.GetFloat(ordinal);
			}

			// Token: 0x0600069A RID: 1690 RVA: 0x0000AA2A File Offset: 0x00008C2A
			public override Guid GetGuid(int ordinal)
			{
				return this.reader.GetGuid(ordinal);
			}

			// Token: 0x0600069B RID: 1691 RVA: 0x0000AA38 File Offset: 0x00008C38
			public override short GetInt16(int ordinal)
			{
				return this.reader.GetInt16(ordinal);
			}

			// Token: 0x0600069C RID: 1692 RVA: 0x0000AA46 File Offset: 0x00008C46
			public override int GetInt32(int ordinal)
			{
				return this.reader.GetInt32(ordinal);
			}

			// Token: 0x0600069D RID: 1693 RVA: 0x0000AA54 File Offset: 0x00008C54
			public override long GetInt64(int ordinal)
			{
				return this.reader.GetInt64(ordinal);
			}

			// Token: 0x0600069E RID: 1694 RVA: 0x0000AA62 File Offset: 0x00008C62
			public override string GetName(int ordinal)
			{
				return this.reader.GetName(ordinal);
			}

			// Token: 0x0600069F RID: 1695 RVA: 0x0000AA70 File Offset: 0x00008C70
			public override int GetOrdinal(string name)
			{
				return this.reader.GetOrdinal(name);
			}

			// Token: 0x060006A0 RID: 1696 RVA: 0x0000AA7E File Offset: 0x00008C7E
			public override string GetString(int ordinal)
			{
				return this.reader.GetString(ordinal);
			}

			// Token: 0x060006A1 RID: 1697 RVA: 0x0000AA8C File Offset: 0x00008C8C
			public override Type GetProviderSpecificFieldType(int ordinal)
			{
				return this.reader.GetProviderSpecificFieldType(ordinal);
			}

			// Token: 0x060006A2 RID: 1698 RVA: 0x0000AA9A File Offset: 0x00008C9A
			public override object GetProviderSpecificValue(int ordinal)
			{
				return this.reader.GetProviderSpecificValue(ordinal);
			}

			// Token: 0x060006A3 RID: 1699 RVA: 0x0000AAA8 File Offset: 0x00008CA8
			public override object GetValue(int ordinal)
			{
				return this.reader.GetValue(ordinal);
			}

			// Token: 0x060006A4 RID: 1700 RVA: 0x0000AAB6 File Offset: 0x00008CB6
			public override int GetValues(object[] values)
			{
				return this.reader.GetValues(values);
			}

			// Token: 0x1700022B RID: 555
			// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0000AAC4 File Offset: 0x00008CC4
			public override bool HasRows
			{
				get
				{
					return this.reader.HasRows;
				}
			}

			// Token: 0x1700022C RID: 556
			// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0000AAD1 File Offset: 0x00008CD1
			public override bool IsClosed
			{
				get
				{
					return this.reader.IsClosed;
				}
			}

			// Token: 0x060006A7 RID: 1703 RVA: 0x0000AADE File Offset: 0x00008CDE
			public override bool IsDBNull(int ordinal)
			{
				return this.reader.IsDBNull(ordinal);
			}

			// Token: 0x060006A8 RID: 1704 RVA: 0x0000AAEC File Offset: 0x00008CEC
			public override bool NextResult()
			{
				return this.reader.NextResult();
			}

			// Token: 0x060006A9 RID: 1705 RVA: 0x0000AAF9 File Offset: 0x00008CF9
			public override bool Read()
			{
				return this.reader.Read();
			}

			// Token: 0x1700022D RID: 557
			// (get) Token: 0x060006AA RID: 1706 RVA: 0x0000AB06 File Offset: 0x00008D06
			public override int RecordsAffected
			{
				get
				{
					return this.reader.RecordsAffected;
				}
			}

			// Token: 0x1700022E RID: 558
			public override object this[string name]
			{
				get
				{
					return this.reader[name];
				}
			}

			// Token: 0x1700022F RID: 559
			public override object this[int ordinal]
			{
				get
				{
					return this.reader[ordinal];
				}
			}

			// Token: 0x040003F5 RID: 1013
			private readonly DbDataReader reader;

			// Token: 0x040003F6 RID: 1014
			private TableSchema schema;
		}
	}
}
