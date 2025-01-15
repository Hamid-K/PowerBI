using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000087 RID: 135
	[Serializable]
	internal class RidSignaturesDataReader : IDataReader, IDisposable, IDataRecord
	{
		// Token: 0x06000550 RID: 1360 RVA: 0x0001879E File Offset: 0x0001699E
		public RidSignaturesDataReader(DataTable schema)
		{
			this.m_schema = schema;
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x000187BF File Offset: 0x000169BF
		public int Depth
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x000187C2 File Offset: 0x000169C2
		public bool IsClosed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x000187C5 File Offset: 0x000169C5
		public int RecordsAffected
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x000187C8 File Offset: 0x000169C8
		public void Close()
		{
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x000187CA File Offset: 0x000169CA
		public void Dispose()
		{
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x000187CC File Offset: 0x000169CC
		public bool NextResult()
		{
			return false;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x000187D0 File Offset: 0x000169D0
		public void AddRidSignatures(int recordId, int lookupId, int hashTableId, IEnumerable<int> signatures)
		{
			RidSignaturesDataReader.SignatureList signatureList = new RidSignaturesDataReader.SignatureList(recordId, lookupId, hashTableId, signatures);
			if (signatureList.m_signatures.Count > 0)
			{
				this.m_enquedSignatureLists.Push(signatureList);
				this.Count += signatureList.m_signatures.Count;
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0001881C File Offset: 0x00016A1C
		public bool Read()
		{
			if (this.m_enquedSignatureLists.Count > 0)
			{
				if (this.m_signatureIdx == this.m_enquedSignatureLists.Peek().m_signatures.Count - 1)
				{
					this.m_enquedSignatureLists.Pop();
					this.m_signatureIdx = -1;
				}
				if (this.m_enquedSignatureLists.Count > 0)
				{
					this.m_signatureIdx++;
					return true;
				}
			}
			if (this.m_enquedSignatureLists.Count == 0)
			{
				this.Count = 0;
				this.m_signatureIdx = -1;
				return false;
			}
			return true;
		}

		// Token: 0x17000126 RID: 294
		public object this[int i]
		{
			get
			{
				switch (i)
				{
				case 0:
					return this.m_enquedSignatureLists.Peek().m_signatures[this.m_signatureIdx];
				case 1:
					return this.m_enquedSignatureLists.Peek().m_lookupId;
				case 2:
					return this.m_enquedSignatureLists.Peek().m_hashTableId;
				case 3:
					return this.m_enquedSignatureLists.Peek().m_recordId;
				default:
					throw new ArgumentException();
				}
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0001893C File Offset: 0x00016B3C
		public DataTable GetSchemaTable()
		{
			return this.m_schema;
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x00018944 File Offset: 0x00016B44
		public int FieldCount
		{
			get
			{
				return this.GetSchemaTable().Rows.Count;
			}
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00018956 File Offset: 0x00016B56
		public object GetValue(int i)
		{
			if (!this.IsDBNull(i))
			{
				return this[i];
			}
			return null;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0001896A File Offset: 0x00016B6A
		public bool GetBoolean(int i)
		{
			return (bool)this[i];
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00018978 File Offset: 0x00016B78
		public byte GetByte(int i)
		{
			return (byte)this[i];
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00018986 File Offset: 0x00016B86
		public char GetChar(int i)
		{
			return (char)this[i];
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00018994 File Offset: 0x00016B94
		public decimal GetDecimal(int i)
		{
			return (decimal)this[i];
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000189A2 File Offset: 0x00016BA2
		public double GetDouble(int i)
		{
			return (double)this[i];
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000189B0 File Offset: 0x00016BB0
		public float GetFloat(int i)
		{
			return (float)this[i];
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x000189BE File Offset: 0x00016BBE
		public short GetInt16(int i)
		{
			return (short)this[i];
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000189CC File Offset: 0x00016BCC
		public int GetInt32(int i)
		{
			return (int)this[i];
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000189DA File Offset: 0x00016BDA
		public long GetInt64(int i)
		{
			return (long)this[i];
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x000189E8 File Offset: 0x00016BE8
		public string GetString(int i)
		{
			if (!this.IsDBNull(i))
			{
				return this[i].ToString();
			}
			return null;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00018A01 File Offset: 0x00016C01
		public DateTime GetDateTime(int i)
		{
			return (DateTime)this[i];
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00018A0F File Offset: 0x00016C0F
		public Guid GetGuid(int i)
		{
			return (Guid)this[i];
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00018A1D File Offset: 0x00016C1D
		public IDataReader GetData(int i)
		{
			if (!this.IsDBNull(i))
			{
				return (IDataReader)this[i];
			}
			return null;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00018A36 File Offset: 0x00016C36
		public bool IsDBNull(int i)
		{
			return this[i] == null || DBNull.Value == this[i];
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00018A51 File Offset: 0x00016C51
		public int GetOrdinal(string name)
		{
			return RidSignaturesDataReader.GetOrdinal(this.GetSchemaTable(), name, true);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00018A60 File Offset: 0x00016C60
		public string GetName(int i)
		{
			return RidSignaturesDataReader.GetName(this.GetSchemaTable(), i);
		}

		// Token: 0x17000128 RID: 296
		public object this[string name]
		{
			get
			{
				return this[this.GetOrdinal(name)];
			}
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00018A7D File Offset: 0x00016C7D
		public Type GetFieldType(int i)
		{
			return (Type)this.GetSchemaTable().Rows[i][SchemaTableColumn.DataType];
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00018A9F File Offset: 0x00016C9F
		public string GetDataTypeName(int i)
		{
			return RidSignaturesDataReader.GetDataTypeName(this.GetSchemaTable(), i);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00018AB0 File Offset: 0x00016CB0
		public int GetValues(object[] values)
		{
			int num = Math.Min(values.GetLength(0), this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this[i];
			}
			return num;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00018AE7 File Offset: 0x00016CE7
		public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00018AEE File Offset: 0x00016CEE
		public long GetChars(int i, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00018AF8 File Offset: 0x00016CF8
		protected static DataRow GetSchemaRow(DataTable schemaTable, int columnOrdinal)
		{
			if (schemaTable.PrimaryKey.Length == 1 && schemaTable.PrimaryKey[0].ColumnName.CompareTo(SchemaTableColumn.ColumnOrdinal) == 0)
			{
				return schemaTable.Rows.Find(columnOrdinal);
			}
			foreach (object obj in schemaTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (columnOrdinal == (int)dataRow[SchemaTableColumn.ColumnOrdinal])
				{
					return dataRow;
				}
			}
			throw new Exception(string.Format("Column with ordinal {0} was not found in the schema table.", columnOrdinal));
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00018BB0 File Offset: 0x00016DB0
		protected static string GetDataTypeName(DataTable schemaTable, int i)
		{
			return RidSignaturesDataReader.GetSchemaRow(schemaTable, i)[SchemaTableColumn.DataType].ToString();
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00018BC8 File Offset: 0x00016DC8
		protected static string GetName(DataTable schemaTable, int i)
		{
			return RidSignaturesDataReader.GetSchemaRow(schemaTable, i)[SchemaTableColumn.ColumnName] as string;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00018BE0 File Offset: 0x00016DE0
		protected static int GetOrdinal(DataTable schemaTable, string columnName, bool ignoreCase)
		{
			foreach (object obj in schemaTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (string.Compare(dataRow[SchemaTableColumn.ColumnName] as string, columnName, ignoreCase) == 0)
				{
					return (int)dataRow[SchemaTableColumn.ColumnOrdinal];
				}
			}
			throw new IndexOutOfRangeException();
		}

		// Token: 0x040001D4 RID: 468
		private DataTable m_schema;

		// Token: 0x040001D5 RID: 469
		private Stack<RidSignaturesDataReader.SignatureList> m_enquedSignatureLists = new Stack<RidSignaturesDataReader.SignatureList>();

		// Token: 0x040001D6 RID: 470
		private int m_signatureIdx = -1;

		// Token: 0x040001D7 RID: 471
		public int Count;

		// Token: 0x02000165 RID: 357
		private enum Column
		{
			// Token: 0x040005BF RID: 1471
			Signature,
			// Token: 0x040005C0 RID: 1472
			LookupId,
			// Token: 0x040005C1 RID: 1473
			HashTableId,
			// Token: 0x040005C2 RID: 1474
			RecordId
		}

		// Token: 0x02000166 RID: 358
		private class SignatureList
		{
			// Token: 0x06000CDD RID: 3293 RVA: 0x00037307 File Offset: 0x00035507
			public SignatureList(int r, int l, int h, IEnumerable<int> sigs)
			{
				this.m_recordId = r;
				this.m_lookupId = l;
				this.m_hashTableId = h;
				this.m_signatures = new List<int>(sigs);
			}

			// Token: 0x040005C3 RID: 1475
			public int m_lookupId;

			// Token: 0x040005C4 RID: 1476
			public int m_hashTableId;

			// Token: 0x040005C5 RID: 1477
			public int m_recordId;

			// Token: 0x040005C6 RID: 1478
			public List<int> m_signatures;
		}
	}
}
