using System;
using System.Data;
using System.Xml;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x020000BD RID: 189
	internal class XEASXmlReader : IDataReader, IDisposable, IDataRecord
	{
		// Token: 0x06000203 RID: 515 RVA: 0x0001A184 File Offset: 0x0001A184
		internal XEASXmlReader(XmlReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0001A1B0 File Offset: 0x0001A1B0
		public void Close()
		{
			this.reader.Close();
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0001A1C8 File Offset: 0x0001A1C8
		public int Depth
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0001A1D8 File Offset: 0x0001A1D8
		public DataTable GetSchemaTable()
		{
			return new DataTable("XEvents Rows Table");
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0001A1F0 File Offset: 0x0001A1F0
		public bool IsClosed
		{
			get
			{
				return this.reader.ReadState == ReadState.Closed || this.reader.ReadState == ReadState.EndOfFile || this.reader.ReadState == ReadState.Error;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0001A22C File Offset: 0x0001A22C
		public bool NextResult()
		{
			return this.Read();
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0001A240 File Offset: 0x0001A240
		public bool Read()
		{
			if (this.IsClosed)
			{
				return false;
			}
			if (this.initialized)
			{
				if (this.reader.ReadState != ReadState.Initial)
				{
					return this.reader.ReadState == ReadState.Interactive && this.ReadRowElement();
				}
			}
			while (this.reader.Read() && (this.reader.NodeType != XmlNodeType.Element || !this.reader.LocalName.Equals(XEASXmlReader.ROW_ELEMENT_NAME, StringComparison.OrdinalIgnoreCase)))
			{
			}
			return this.ReadRowElement();
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0001A2C0 File Offset: 0x0001A2C0
		private bool ReadRowElement()
		{
			int num = 0;
			while (this.reader.Read())
			{
				if (this.reader.NodeType == XmlNodeType.Element)
				{
					for (int i = 0; i < XEASXmlReader.DATA_COLUMNS; i++)
					{
						if (this.reader.NodeType == XmlNodeType.Element && this.reader.LocalName.Equals(XEASXmlReader.COLUMN_NAMES[i], StringComparison.OrdinalIgnoreCase))
						{
							num++;
							if (this.reader.Read())
							{
								this.currentRow[i] = this.reader.Value;
							}
						}
					}
				}
				else if (this.reader.NodeType == XmlNodeType.EndElement && this.reader.LocalName.Equals(XEASXmlReader.ROW_ELEMENT_NAME, StringComparison.OrdinalIgnoreCase))
				{
					break;
				}
			}
			return num == XEASXmlReader.DATA_COLUMNS;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0001A380 File Offset: 0x0001A380
		public int RecordsAffected
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0001A390 File Offset: 0x0001A390
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0001A3A4 File Offset: 0x0001A3A4
		public int FieldCount
		{
			get
			{
				return XEASXmlReader.DATA_COLUMNS;
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0001A3B8 File Offset: 0x0001A3B8
		public bool GetBoolean(int i)
		{
			throw new NotImplementedException("Not supported");
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0001A3D0 File Offset: 0x0001A3D0
		public byte GetByte(int i)
		{
			throw new NotImplementedException("Not supported");
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0001A3E8 File Offset: 0x0001A3E8
		public long GetBytes(int ordinal, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			byte[] array = Convert.FromBase64String(this.currentRow[ordinal]);
			int num = array.Length;
			if (dataIndex > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("dataIndex", "Index out of range");
			}
			int num2 = (int)dataIndex;
			if (buffer == null)
			{
				return (long)num;
			}
			if (num2 < 0 || num2 >= num)
			{
				return 0L;
			}
			if (num2 < num)
			{
				if (num2 + length > num)
				{
					num -= num2;
				}
				else
				{
					num = length;
				}
			}
			Array.Copy(array, num2, buffer, bufferIndex, num);
			return (long)num;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0001A458 File Offset: 0x0001A458
		public char GetChar(int i)
		{
			throw new NotImplementedException("Not supported");
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0001A470 File Offset: 0x0001A470
		public long GetChars(int ordinal, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0001A484 File Offset: 0x0001A484
		public IDataReader GetData(int i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0001A498 File Offset: 0x0001A498
		public string GetDataTypeName(int i)
		{
			if (i == 0)
			{
				return typeof(int).ToString();
			}
			if (i == 1)
			{
				return typeof(string).ToString();
			}
			throw new IndexOutOfRangeException("Invalid column specified");
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0001A4D8 File Offset: 0x0001A4D8
		public DateTime GetDateTime(int i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0001A4EC File Offset: 0x0001A4EC
		public decimal GetDecimal(int i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0001A500 File Offset: 0x0001A500
		public double GetDouble(int i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0001A514 File Offset: 0x0001A514
		public Type GetFieldType(int i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0001A528 File Offset: 0x0001A528
		public float GetFloat(int i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0001A53C File Offset: 0x0001A53C
		public Guid GetGuid(int i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0001A550 File Offset: 0x0001A550
		public short GetInt16(int i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0001A564 File Offset: 0x0001A564
		public int GetInt32(int i)
		{
			if (i != 0)
			{
				throw new InvalidOperationException("Invalid Call, specified column is not an int.");
			}
			return int.Parse(this.currentRow[0]);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0001A58C File Offset: 0x0001A58C
		public long GetInt64(int i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0001A5A0 File Offset: 0x0001A5A0
		public string GetName(int i)
		{
			return XEASXmlReader.COLUMN_NAMES[i];
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0001A5B4 File Offset: 0x0001A5B4
		public int GetOrdinal(string name)
		{
			for (int i = 0; i < XEASXmlReader.COLUMN_NAMES.Length; i++)
			{
				if (XEASXmlReader.COLUMN_NAMES[i].Equals(name, StringComparison.OrdinalIgnoreCase))
				{
					return i;
				}
			}
			throw new InvalidOperationException("Invalid Call, specified column does not exist.");
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0001A5F0 File Offset: 0x0001A5F0
		public string GetString(int i)
		{
			if (i != 1)
			{
				throw new InvalidOperationException("Invalid Call, specified column is not a string.");
			}
			return this.currentRow[1];
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0001A614 File Offset: 0x0001A614
		public object GetValue(int i)
		{
			if (i == 0)
			{
				return this.GetInt32(0);
			}
			return this.GetString(1);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0001A638 File Offset: 0x0001A638
		public int GetValues(object[] values)
		{
			values[0] = this.GetInt32(0);
			values[1] = this.GetString(1);
			return 2;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0001A660 File Offset: 0x0001A660
		public bool IsDBNull(int i)
		{
			return false;
		}

		// Token: 0x17000021 RID: 33
		public object this[string name]
		{
			get
			{
				return this[this.GetOrdinal(name)];
			}
		}

		// Token: 0x17000022 RID: 34
		public object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		// Token: 0x04000249 RID: 585
		private bool initialized;

		// Token: 0x0400024A RID: 586
		private XmlReader reader;

		// Token: 0x0400024B RID: 587
		private static int DATA_COLUMNS = 2;

		// Token: 0x0400024C RID: 588
		private static string[] COLUMN_NAMES = new string[] { "XE_TYPE", "XE_DATA" };

		// Token: 0x0400024D RID: 589
		private static string ROW_ELEMENT_NAME = "row";

		// Token: 0x0400024E RID: 590
		private string[] currentRow = new string[XEASXmlReader.DATA_COLUMNS];
	}
}
