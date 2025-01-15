using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Xml;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200005B RID: 91
	public class AdomdDataReader : MarshalByRefObject, IDataReader, IDisposable, IDataRecord, IEnumerable, IXmlaDataReaderOwner
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x00021B65 File Offset: 0x0001FD65
		// (set) Token: 0x060005EF RID: 1519 RVA: 0x00021B6D File Offset: 0x0001FD6D
		internal XmlaDataReader XmlaDataReader { get; private set; }

		// Token: 0x060005F0 RID: 1520 RVA: 0x00021B76 File Offset: 0x0001FD76
		internal static AdomdDataReader CreateInstance(XmlReader xmlReader, CommandBehavior commandBehavior, AdomdConnection connection)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (xmlReader == null)
			{
				throw new ArgumentNullException("xmlReader");
			}
			XmlaClient.ReadUptoRoot(xmlReader);
			if (XmlaClient.IsAffectedObjectsResponseS(xmlReader))
			{
				return new AdomdAffectedObjectsReader(xmlReader, commandBehavior, connection);
			}
			return new AdomdDataReader(xmlReader, commandBehavior, connection, true);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00021BB4 File Offset: 0x0001FDB4
		protected AdomdDataReader(XmlReader xmlReader, CommandBehavior commandBehavior, AdomdConnection connection, bool readerAtRoot)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (xmlReader == null)
			{
				throw new ArgumentNullException("xmlReader");
			}
			this.connection = connection;
			this.XmlaDataReader = new XmlaDataReader(xmlReader, commandBehavior, readerAtRoot, this);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00021BEF File Offset: 0x0001FDEF
		private AdomdDataReader(XmlaDataReader xmlaDataReader)
		{
			this.XmlaDataReader = xmlaDataReader;
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x00021BFE File Offset: 0x0001FDFE
		public string RowsetName
		{
			get
			{
				return this.XmlaDataReader.RowsetName;
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00021C0B File Offset: 0x0001FE0B
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00021C13 File Offset: 0x0001FE13
		public int Depth
		{
			get
			{
				return this.XmlaDataReader.Depth;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x00021C20 File Offset: 0x0001FE20
		public int FieldCount
		{
			get
			{
				return this.XmlaDataReader.FieldCount;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00021C2D File Offset: 0x0001FE2D
		public bool IsClosed
		{
			get
			{
				return this.XmlaDataReader.IsClosed;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00021C3A File Offset: 0x0001FE3A
		public int RecordsAffected
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00021C40 File Offset: 0x0001FE40
		public void Close()
		{
			this.XmlaDataReader.Close();
			if (this.connection != null && (this.XmlaDataReader.CommandBehavior & CommandBehavior.CloseConnection) != CommandBehavior.Default)
			{
				this.connection.OpenedReader = null;
				this.connection.Close();
			}
			this.connection = null;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00021C8E File Offset: 0x0001FE8E
		public DataTable GetSchemaTable()
		{
			return this.XmlaDataReader.GetSchemaTable();
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00021C9B File Offset: 0x0001FE9B
		public bool NextResult()
		{
			return this.XmlaDataReader.NextResult();
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00021CA8 File Offset: 0x0001FEA8
		public bool Read()
		{
			return this.XmlaDataReader.Read();
		}

		// Token: 0x1700017C RID: 380
		public object this[int index]
		{
			get
			{
				return this.GetValue(index);
			}
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00021CBE File Offset: 0x0001FEBE
		public IDataReader GetData(int ordinal)
		{
			return this.GetDataReader(ordinal);
		}

		// Token: 0x1700017D RID: 381
		public object this[string columnName]
		{
			get
			{
				return this.XmlaDataReader[columnName];
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00021CD5 File Offset: 0x0001FED5
		public bool GetBoolean(int ordinal)
		{
			return this.XmlaDataReader.GetBoolean(ordinal);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00021CE3 File Offset: 0x0001FEE3
		public byte GetByte(int ordinal)
		{
			return this.XmlaDataReader.GetByte(ordinal);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00021CF1 File Offset: 0x0001FEF1
		public long GetBytes(int ordinal, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			return this.XmlaDataReader.GetBytes(ordinal, dataIndex, buffer, bufferIndex, length);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00021D05 File Offset: 0x0001FF05
		public char GetChar(int ordinal)
		{
			return this.XmlaDataReader.GetChar(ordinal);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00021D13 File Offset: 0x0001FF13
		public long GetChars(int ordinal, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			return this.XmlaDataReader.GetChars(ordinal, dataIndex, buffer, bufferIndex, length);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00021D27 File Offset: 0x0001FF27
		public string GetDataTypeName(int index)
		{
			return this.XmlaDataReader.GetDataTypeName(index);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00021D35 File Offset: 0x0001FF35
		public DateTime GetDateTime(int ordinal)
		{
			return this.XmlaDataReader.GetDateTime(ordinal);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00021D43 File Offset: 0x0001FF43
		public decimal GetDecimal(int ordinal)
		{
			return this.XmlaDataReader.GetDecimal(ordinal);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00021D51 File Offset: 0x0001FF51
		public double GetDouble(int ordinal)
		{
			return this.XmlaDataReader.GetDouble(ordinal);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00021D5F File Offset: 0x0001FF5F
		public Type GetFieldType(int ordinal)
		{
			return this.XmlaDataReader.GetFieldType(ordinal);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00021D6D File Offset: 0x0001FF6D
		public float GetFloat(int ordinal)
		{
			return this.XmlaDataReader.GetFloat(ordinal);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00021D7B File Offset: 0x0001FF7B
		public Guid GetGuid(int ordinal)
		{
			return this.XmlaDataReader.GetGuid(ordinal);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00021D89 File Offset: 0x0001FF89
		public short GetInt16(int ordinal)
		{
			return this.XmlaDataReader.GetInt16(ordinal);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00021D97 File Offset: 0x0001FF97
		public int GetInt32(int ordinal)
		{
			return this.XmlaDataReader.GetInt32(ordinal);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00021DA5 File Offset: 0x0001FFA5
		public long GetInt64(int ordinal)
		{
			return this.XmlaDataReader.GetInt64(ordinal);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00021DB3 File Offset: 0x0001FFB3
		public string GetName(int ordinal)
		{
			return this.XmlaDataReader.GetName(ordinal);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00021DC1 File Offset: 0x0001FFC1
		public int GetOrdinal(string name)
		{
			return this.XmlaDataReader.GetOrdinal(name);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00021DCF File Offset: 0x0001FFCF
		public string GetString(int ordinal)
		{
			return this.XmlaDataReader.GetString(ordinal);
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00021DDD File Offset: 0x0001FFDD
		public TimeSpan GetTimeSpan(int ordinal)
		{
			return this.XmlaDataReader.GetTimeSpan(ordinal);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00021DEC File Offset: 0x0001FFEC
		public object GetValue(int ordinal)
		{
			object obj = this.XmlaDataReader.GetValue(ordinal);
			if (obj is XmlaDataReader)
			{
				obj = this.GetDataReader(ordinal);
			}
			return obj;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00021E18 File Offset: 0x00020018
		public AdomdDataReader GetDataReader(int ordinal)
		{
			if (this.embeddedReaders == null)
			{
				this.embeddedReaders = new AdomdDataReader[this.FieldCount];
			}
			if (this.embeddedReaders[ordinal] == null)
			{
				XmlaDataReader dataReader = this.XmlaDataReader.GetDataReader(ordinal);
				this.embeddedReaders[ordinal] = new AdomdDataReader(dataReader);
			}
			return this.embeddedReaders[ordinal];
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00021E6B File Offset: 0x0002006B
		public int GetValues(object[] values)
		{
			return this.XmlaDataReader.GetValues(values);
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00021E79 File Offset: 0x00020079
		public bool IsDBNull(int ordinal)
		{
			return this.XmlaDataReader.IsDBNull(ordinal);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00021E87 File Offset: 0x00020087
		public AdomdDataReader.Enumerator GetEnumerator()
		{
			return new AdomdDataReader.Enumerator(this, this.XmlaDataReader.CommandBehavior);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00021E9A File Offset: 0x0002009A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00021EA7 File Offset: 0x000200A7
		void IXmlaDataReaderOwner.CloseConnection(bool endSession)
		{
			if (this.connection != null)
			{
				this.connection.Close(endSession);
				this.connection = null;
			}
		}

		// Token: 0x0400043D RID: 1085
		private AdomdConnection connection;

		// Token: 0x0400043F RID: 1087
		private AdomdDataReader[] embeddedReaders;

		// Token: 0x020001A5 RID: 421
		public struct Enumerator : IEnumerator
		{
			// Token: 0x060012EF RID: 4847 RVA: 0x00043AC4 File Offset: 0x00041CC4
			internal Enumerator(AdomdDataReader dataReader, CommandBehavior commandBehavior)
			{
				bool flag = (CommandBehavior.CloseConnection & commandBehavior) > CommandBehavior.Default;
				this.enumerator = new DbEnumerator(dataReader, flag);
			}

			// Token: 0x1700069D RID: 1693
			// (get) Token: 0x060012F0 RID: 4848 RVA: 0x00043AE6 File Offset: 0x00041CE6
			public IDataRecord Current
			{
				get
				{
					return (IDataRecord)this.enumerator.Current;
				}
			}

			// Token: 0x1700069E RID: 1694
			// (get) Token: 0x060012F1 RID: 4849 RVA: 0x00043AF8 File Offset: 0x00041CF8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060012F2 RID: 4850 RVA: 0x00043B00 File Offset: 0x00041D00
			public bool MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x060012F3 RID: 4851 RVA: 0x00043B0D File Offset: 0x00041D0D
			public void Reset()
			{
				this.enumerator.Reset();
			}

			// Token: 0x04000CB8 RID: 3256
			private DbEnumerator enumerator;
		}
	}
}
