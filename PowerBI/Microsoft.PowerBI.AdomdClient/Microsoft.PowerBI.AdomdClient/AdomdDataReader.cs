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
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00021835 File Offset: 0x0001FA35
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x0002183D File Offset: 0x0001FA3D
		internal XmlaDataReader XmlaDataReader { get; private set; }

		// Token: 0x060005E3 RID: 1507 RVA: 0x00021846 File Offset: 0x0001FA46
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

		// Token: 0x060005E4 RID: 1508 RVA: 0x00021884 File Offset: 0x0001FA84
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

		// Token: 0x060005E5 RID: 1509 RVA: 0x000218BF File Offset: 0x0001FABF
		private AdomdDataReader(XmlaDataReader xmlaDataReader)
		{
			this.XmlaDataReader = xmlaDataReader;
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x000218CE File Offset: 0x0001FACE
		public string RowsetName
		{
			get
			{
				return this.XmlaDataReader.RowsetName;
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x000218DB File Offset: 0x0001FADB
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x000218E3 File Offset: 0x0001FAE3
		public int Depth
		{
			get
			{
				return this.XmlaDataReader.Depth;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x000218F0 File Offset: 0x0001FAF0
		public int FieldCount
		{
			get
			{
				return this.XmlaDataReader.FieldCount;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x000218FD File Offset: 0x0001FAFD
		public bool IsClosed
		{
			get
			{
				return this.XmlaDataReader.IsClosed;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0002190A File Offset: 0x0001FB0A
		public int RecordsAffected
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00021910 File Offset: 0x0001FB10
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

		// Token: 0x060005ED RID: 1517 RVA: 0x0002195E File Offset: 0x0001FB5E
		public DataTable GetSchemaTable()
		{
			return this.XmlaDataReader.GetSchemaTable();
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0002196B File Offset: 0x0001FB6B
		public bool NextResult()
		{
			return this.XmlaDataReader.NextResult();
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00021978 File Offset: 0x0001FB78
		public bool Read()
		{
			return this.XmlaDataReader.Read();
		}

		// Token: 0x17000176 RID: 374
		public object this[int index]
		{
			get
			{
				return this.GetValue(index);
			}
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0002198E File Offset: 0x0001FB8E
		public IDataReader GetData(int ordinal)
		{
			return this.GetDataReader(ordinal);
		}

		// Token: 0x17000177 RID: 375
		public object this[string columnName]
		{
			get
			{
				return this.XmlaDataReader[columnName];
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x000219A5 File Offset: 0x0001FBA5
		public bool GetBoolean(int ordinal)
		{
			return this.XmlaDataReader.GetBoolean(ordinal);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x000219B3 File Offset: 0x0001FBB3
		public byte GetByte(int ordinal)
		{
			return this.XmlaDataReader.GetByte(ordinal);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x000219C1 File Offset: 0x0001FBC1
		public long GetBytes(int ordinal, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			return this.XmlaDataReader.GetBytes(ordinal, dataIndex, buffer, bufferIndex, length);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x000219D5 File Offset: 0x0001FBD5
		public char GetChar(int ordinal)
		{
			return this.XmlaDataReader.GetChar(ordinal);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x000219E3 File Offset: 0x0001FBE3
		public long GetChars(int ordinal, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			return this.XmlaDataReader.GetChars(ordinal, dataIndex, buffer, bufferIndex, length);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x000219F7 File Offset: 0x0001FBF7
		public string GetDataTypeName(int index)
		{
			return this.XmlaDataReader.GetDataTypeName(index);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00021A05 File Offset: 0x0001FC05
		public DateTime GetDateTime(int ordinal)
		{
			return this.XmlaDataReader.GetDateTime(ordinal);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00021A13 File Offset: 0x0001FC13
		public decimal GetDecimal(int ordinal)
		{
			return this.XmlaDataReader.GetDecimal(ordinal);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00021A21 File Offset: 0x0001FC21
		public double GetDouble(int ordinal)
		{
			return this.XmlaDataReader.GetDouble(ordinal);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00021A2F File Offset: 0x0001FC2F
		public Type GetFieldType(int ordinal)
		{
			return this.XmlaDataReader.GetFieldType(ordinal);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00021A3D File Offset: 0x0001FC3D
		public float GetFloat(int ordinal)
		{
			return this.XmlaDataReader.GetFloat(ordinal);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00021A4B File Offset: 0x0001FC4B
		public Guid GetGuid(int ordinal)
		{
			return this.XmlaDataReader.GetGuid(ordinal);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00021A59 File Offset: 0x0001FC59
		public short GetInt16(int ordinal)
		{
			return this.XmlaDataReader.GetInt16(ordinal);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00021A67 File Offset: 0x0001FC67
		public int GetInt32(int ordinal)
		{
			return this.XmlaDataReader.GetInt32(ordinal);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00021A75 File Offset: 0x0001FC75
		public long GetInt64(int ordinal)
		{
			return this.XmlaDataReader.GetInt64(ordinal);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00021A83 File Offset: 0x0001FC83
		public string GetName(int ordinal)
		{
			return this.XmlaDataReader.GetName(ordinal);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00021A91 File Offset: 0x0001FC91
		public int GetOrdinal(string name)
		{
			return this.XmlaDataReader.GetOrdinal(name);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00021A9F File Offset: 0x0001FC9F
		public string GetString(int ordinal)
		{
			return this.XmlaDataReader.GetString(ordinal);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00021AAD File Offset: 0x0001FCAD
		public TimeSpan GetTimeSpan(int ordinal)
		{
			return this.XmlaDataReader.GetTimeSpan(ordinal);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00021ABC File Offset: 0x0001FCBC
		public object GetValue(int ordinal)
		{
			object obj = this.XmlaDataReader.GetValue(ordinal);
			if (obj is XmlaDataReader)
			{
				obj = this.GetDataReader(ordinal);
			}
			return obj;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00021AE8 File Offset: 0x0001FCE8
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

		// Token: 0x06000608 RID: 1544 RVA: 0x00021B3B File Offset: 0x0001FD3B
		public int GetValues(object[] values)
		{
			return this.XmlaDataReader.GetValues(values);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00021B49 File Offset: 0x0001FD49
		public bool IsDBNull(int ordinal)
		{
			return this.XmlaDataReader.IsDBNull(ordinal);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00021B57 File Offset: 0x0001FD57
		public AdomdDataReader.Enumerator GetEnumerator()
		{
			return new AdomdDataReader.Enumerator(this, this.XmlaDataReader.CommandBehavior);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00021B6A File Offset: 0x0001FD6A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00021B77 File Offset: 0x0001FD77
		void IXmlaDataReaderOwner.CloseConnection(bool endSession)
		{
			if (this.connection != null)
			{
				this.connection.Close(endSession);
				this.connection = null;
			}
		}

		// Token: 0x04000430 RID: 1072
		private AdomdConnection connection;

		// Token: 0x04000432 RID: 1074
		private AdomdDataReader[] embeddedReaders;

		// Token: 0x020001A5 RID: 421
		public struct Enumerator : IEnumerator
		{
			// Token: 0x060012E2 RID: 4834 RVA: 0x00043588 File Offset: 0x00041788
			internal Enumerator(AdomdDataReader dataReader, CommandBehavior commandBehavior)
			{
				bool flag = (CommandBehavior.CloseConnection & commandBehavior) > CommandBehavior.Default;
				this.enumerator = new DbEnumerator(dataReader, flag);
			}

			// Token: 0x17000697 RID: 1687
			// (get) Token: 0x060012E3 RID: 4835 RVA: 0x000435AA File Offset: 0x000417AA
			public IDataRecord Current
			{
				get
				{
					return (IDataRecord)this.enumerator.Current;
				}
			}

			// Token: 0x17000698 RID: 1688
			// (get) Token: 0x060012E4 RID: 4836 RVA: 0x000435BC File Offset: 0x000417BC
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060012E5 RID: 4837 RVA: 0x000435C4 File Offset: 0x000417C4
			public bool MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x060012E6 RID: 4838 RVA: 0x000435D1 File Offset: 0x000417D1
			public void Reset()
			{
				this.enumerator.Reset();
			}

			// Token: 0x04000CA7 RID: 3239
			private DbEnumerator enumerator;
		}
	}
}
