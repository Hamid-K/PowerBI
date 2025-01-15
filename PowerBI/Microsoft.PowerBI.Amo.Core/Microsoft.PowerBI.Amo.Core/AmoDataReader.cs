using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200006D RID: 109
	public class AmoDataReader : IDataReader, IDisposable, IDataRecord, IEnumerable
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0002235D File Offset: 0x0002055D
		// (set) Token: 0x060005C7 RID: 1479 RVA: 0x00022365 File Offset: 0x00020565
		internal XmlaDataReader XmlaDataReader { get; private set; }

		// Token: 0x060005C8 RID: 1480 RVA: 0x0002236E File Offset: 0x0002056E
		internal AmoDataReader(XmlReader xmlReader)
		{
			this.XmlaDataReader = new XmlaDataReader(xmlReader, CommandBehavior.Default, false, null);
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x00022385 File Offset: 0x00020585
		public string RowsetName
		{
			get
			{
				return this.XmlaDataReader.RowsetName;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x00022392 File Offset: 0x00020592
		public XmlaResultCollection Results
		{
			get
			{
				return this.XmlaDataReader.Results;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0002239F File Offset: 0x0002059F
		public Dictionary<XName, string> TopLevelAttributes
		{
			get
			{
				return this.XmlaDataReader.TopLevelAttributes;
			}
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x000223AC File Offset: 0x000205AC
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x000223B4 File Offset: 0x000205B4
		public int Depth
		{
			get
			{
				return this.XmlaDataReader.Depth;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x000223C1 File Offset: 0x000205C1
		public int FieldCount
		{
			get
			{
				return this.XmlaDataReader.FieldCount;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x000223CE File Offset: 0x000205CE
		public bool IsClosed
		{
			get
			{
				return this.XmlaDataReader.IsClosed;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x000223DB File Offset: 0x000205DB
		public int RecordsAffected
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x000223DE File Offset: 0x000205DE
		public void Close()
		{
			this.XmlaDataReader.Close();
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000223EB File Offset: 0x000205EB
		public DataTable GetSchemaTable()
		{
			return this.XmlaDataReader.GetSchemaTable();
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x000223F8 File Offset: 0x000205F8
		public bool NextResult()
		{
			return this.XmlaDataReader.NextResult();
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00022405 File Offset: 0x00020605
		public bool Read()
		{
			return this.XmlaDataReader.Read();
		}

		// Token: 0x17000151 RID: 337
		public object this[int index]
		{
			get
			{
				return this.XmlaDataReader[index];
			}
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00022420 File Offset: 0x00020620
		public IDataReader GetData(int ordinal)
		{
			return this.XmlaDataReader.GetData(ordinal);
		}

		// Token: 0x17000152 RID: 338
		public object this[string columnName]
		{
			get
			{
				return this.XmlaDataReader[columnName];
			}
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0002243C File Offset: 0x0002063C
		public bool GetBoolean(int ordinal)
		{
			return this.XmlaDataReader.GetBoolean(ordinal);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0002244A File Offset: 0x0002064A
		public byte GetByte(int ordinal)
		{
			return this.XmlaDataReader.GetByte(ordinal);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00022458 File Offset: 0x00020658
		public long GetBytes(int ordinal, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			return this.XmlaDataReader.GetBytes(ordinal, dataIndex, buffer, bufferIndex, length);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0002246C File Offset: 0x0002066C
		public char GetChar(int ordinal)
		{
			return this.XmlaDataReader.GetChar(ordinal);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0002247A File Offset: 0x0002067A
		public long GetChars(int ordinal, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			return this.XmlaDataReader.GetChars(ordinal, dataIndex, buffer, bufferIndex, length);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0002248E File Offset: 0x0002068E
		public string GetDataTypeName(int index)
		{
			return this.XmlaDataReader.GetDataTypeName(index);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0002249C File Offset: 0x0002069C
		public DateTime GetDateTime(int ordinal)
		{
			return this.XmlaDataReader.GetDateTime(ordinal);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000224AA File Offset: 0x000206AA
		public decimal GetDecimal(int ordinal)
		{
			return this.XmlaDataReader.GetDecimal(ordinal);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x000224B8 File Offset: 0x000206B8
		public double GetDouble(int ordinal)
		{
			return this.XmlaDataReader.GetDouble(ordinal);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x000224C6 File Offset: 0x000206C6
		public Type GetFieldType(int ordinal)
		{
			return this.XmlaDataReader.GetFieldType(ordinal);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x000224D4 File Offset: 0x000206D4
		public float GetFloat(int ordinal)
		{
			return this.XmlaDataReader.GetFloat(ordinal);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x000224E2 File Offset: 0x000206E2
		public Guid GetGuid(int ordinal)
		{
			return this.XmlaDataReader.GetGuid(ordinal);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x000224F0 File Offset: 0x000206F0
		public short GetInt16(int ordinal)
		{
			return this.XmlaDataReader.GetInt16(ordinal);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x000224FE File Offset: 0x000206FE
		public int GetInt32(int ordinal)
		{
			return this.XmlaDataReader.GetInt32(ordinal);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0002250C File Offset: 0x0002070C
		public long GetInt64(int ordinal)
		{
			return this.XmlaDataReader.GetInt64(ordinal);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0002251A File Offset: 0x0002071A
		public string GetName(int ordinal)
		{
			return this.XmlaDataReader.GetName(ordinal);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00022528 File Offset: 0x00020728
		public int GetOrdinal(string name)
		{
			return this.XmlaDataReader.GetOrdinal(name);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00022536 File Offset: 0x00020736
		public string GetString(int ordinal)
		{
			return this.XmlaDataReader.GetString(ordinal);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00022544 File Offset: 0x00020744
		public TimeSpan GetTimeSpan(int ordinal)
		{
			return this.XmlaDataReader.GetTimeSpan(ordinal);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00022552 File Offset: 0x00020752
		public object GetValue(int ordinal)
		{
			return this.XmlaDataReader.GetValue(ordinal);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00022560 File Offset: 0x00020760
		public int GetValues(object[] values)
		{
			return this.XmlaDataReader.GetValues(values);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0002256E File Offset: 0x0002076E
		public bool IsDBNull(int ordinal)
		{
			return this.XmlaDataReader.IsDBNull(ordinal);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0002257C File Offset: 0x0002077C
		public IEnumerator GetEnumerator()
		{
			return this.XmlaDataReader.GetEnumerator();
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0002258E File Offset: 0x0002078E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
