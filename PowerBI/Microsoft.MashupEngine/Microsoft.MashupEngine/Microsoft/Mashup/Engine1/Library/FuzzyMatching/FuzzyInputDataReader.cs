using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.FuzzyMatching
{
	// Token: 0x02000B38 RID: 2872
	public class FuzzyInputDataReader : IDataReader, IDisposable, IDataRecord
	{
		// Token: 0x06004FA0 RID: 20384 RVA: 0x0010AFC0 File Offset: 0x001091C0
		public FuzzyInputDataReader(string inputTableName, Query query, int[] keys)
		{
			this.inputTableName = inputTableName;
			this.query = query;
			this.enumerator = query.GetRows().GetEnumerator();
			this.keys = keys;
			this.SetSchemaTable();
			this.isClosed = false;
			this.currentRecordId = 0;
		}

		// Token: 0x170018CD RID: 6349
		public object this[string name]
		{
			get
			{
				int num;
				if (this.query.Columns.TryGetKeyIndex(name, out num))
				{
					return this.GetValue(num);
				}
				throw new ArgumentException();
			}
		}

		// Token: 0x170018CE RID: 6350
		public object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		// Token: 0x170018CF RID: 6351
		// (get) Token: 0x06004FA3 RID: 20387 RVA: 0x00002105 File Offset: 0x00000305
		public int Depth
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170018D0 RID: 6352
		// (get) Token: 0x06004FA4 RID: 20388 RVA: 0x0010B048 File Offset: 0x00109248
		public int FieldCount
		{
			get
			{
				return this.keys.Length + 1;
			}
		}

		// Token: 0x170018D1 RID: 6353
		// (get) Token: 0x06004FA5 RID: 20389 RVA: 0x0010B054 File Offset: 0x00109254
		public bool IsClosed
		{
			get
			{
				return this.isClosed;
			}
		}

		// Token: 0x170018D2 RID: 6354
		// (get) Token: 0x06004FA6 RID: 20390 RVA: 0x00002105 File Offset: 0x00000305
		public int RecordsAffected
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170018D3 RID: 6355
		// (get) Token: 0x06004FA7 RID: 20391 RVA: 0x0010B05C File Offset: 0x0010925C
		public RecordValue CurrentRecord
		{
			get
			{
				return this.currentRecord;
			}
		}

		// Token: 0x06004FA8 RID: 20392 RVA: 0x0010B064 File Offset: 0x00109264
		public void Close()
		{
			this.Dispose();
		}

		// Token: 0x06004FA9 RID: 20393 RVA: 0x0010B06C File Offset: 0x0010926C
		public void Dispose()
		{
			this.enumerator.Dispose();
			this.isClosed = true;
		}

		// Token: 0x06004FAA RID: 20394 RVA: 0x000033E7 File Offset: 0x000015E7
		public bool GetBoolean(int i)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004FAB RID: 20395 RVA: 0x000033E7 File Offset: 0x000015E7
		public byte GetByte(int i)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004FAC RID: 20396 RVA: 0x000033E7 File Offset: 0x000015E7
		public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004FAD RID: 20397 RVA: 0x000033E7 File Offset: 0x000015E7
		public char GetChar(int i)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004FAE RID: 20398 RVA: 0x000033E7 File Offset: 0x000015E7
		public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004FAF RID: 20399 RVA: 0x000033E7 File Offset: 0x000015E7
		public IDataReader GetData(int i)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004FB0 RID: 20400 RVA: 0x0010B080 File Offset: 0x00109280
		public string GetDataTypeName(int i)
		{
			return this.schemaTable.Rows[i][InformationSchemaTableColumnName.DataType].ToString();
		}

		// Token: 0x06004FB1 RID: 20401 RVA: 0x000033E7 File Offset: 0x000015E7
		public DateTime GetDate(int i)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004FB2 RID: 20402 RVA: 0x000033E7 File Offset: 0x000015E7
		public DateTime GetDateTime(int i)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004FB3 RID: 20403 RVA: 0x000033E7 File Offset: 0x000015E7
		public decimal GetDecimal(int i)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004FB4 RID: 20404 RVA: 0x000033E7 File Offset: 0x000015E7
		public double GetDouble(int i)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004FB5 RID: 20405 RVA: 0x0010B0A2 File Offset: 0x001092A2
		public Type GetFieldType(int i)
		{
			return (Type)this.schemaTable.Rows[i][InformationSchemaTableColumnName.DataType];
		}

		// Token: 0x06004FB6 RID: 20406 RVA: 0x000033E7 File Offset: 0x000015E7
		public float GetFloat(int i)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004FB7 RID: 20407 RVA: 0x000033E7 File Offset: 0x000015E7
		public Guid GetGuid(int i)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004FB8 RID: 20408 RVA: 0x000033E7 File Offset: 0x000015E7
		public short GetInt16(int i)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004FB9 RID: 20409 RVA: 0x000033E7 File Offset: 0x000015E7
		public int GetInt32(int i)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004FBA RID: 20410 RVA: 0x000033E7 File Offset: 0x000015E7
		public long GetInt64(int i)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004FBB RID: 20411 RVA: 0x0010B0C4 File Offset: 0x001092C4
		public string GetName(int i)
		{
			if (i == 0)
			{
				return "RecordId";
			}
			return this.query.Columns[this.GetKeyOrdinal(i)];
		}

		// Token: 0x06004FBC RID: 20412 RVA: 0x0010B0E8 File Offset: 0x001092E8
		public int GetOrdinal(string name)
		{
			int num;
			if (this.query.Columns.TryGetKeyIndex(name, out num))
			{
				return num + 1;
			}
			throw new ArgumentException();
		}

		// Token: 0x06004FBD RID: 20413 RVA: 0x0010B113 File Offset: 0x00109313
		public DataTable GetSchemaTable()
		{
			return this.schemaTable;
		}

		// Token: 0x06004FBE RID: 20414 RVA: 0x0010B11B File Offset: 0x0010931B
		public string GetString(int i)
		{
			if (i == 0)
			{
				throw new InvalidOperationException();
			}
			return this.GetColumnValue(i).AsString;
		}

		// Token: 0x06004FBF RID: 20415 RVA: 0x0010B132 File Offset: 0x00109332
		public object GetValue(int i)
		{
			return this.GetValueByIndex(i);
		}

		// Token: 0x06004FC0 RID: 20416 RVA: 0x0010B13C File Offset: 0x0010933C
		public int GetValues(object[] values)
		{
			int num = Math.Min(this.keys.Length + 1, values.Length);
			for (int i = 0; i < num; i++)
			{
				values[i] = this.GetValueByIndex(i);
			}
			return num;
		}

		// Token: 0x06004FC1 RID: 20417 RVA: 0x0010B173 File Offset: 0x00109373
		public bool IsDBNull(int i)
		{
			return i != 0 && this.GetColumnValue(i).IsNull;
		}

		// Token: 0x06004FC2 RID: 20418 RVA: 0x00002105 File Offset: 0x00000305
		public bool NextResult()
		{
			return false;
		}

		// Token: 0x06004FC3 RID: 20419 RVA: 0x0010B188 File Offset: 0x00109388
		public bool Read()
		{
			if (!this.enumerator.MoveNext())
			{
				this.currentRecordId = 0;
				this.current = null;
				this.currentRecord = null;
				return false;
			}
			this.currentRecordId++;
			this.current = this.enumerator.Current;
			this.currentRecord = this.current.Value.AsRecord;
			return true;
		}

		// Token: 0x06004FC4 RID: 20420 RVA: 0x0010B1F0 File Offset: 0x001093F0
		private void SetSchemaTable()
		{
			string text = this.inputTableName;
			this.schemaTable = new DataTable(this.inputTableName)
			{
				Locale = CultureInfo.InvariantCulture
			};
			this.schemaTable.Columns.Add(InformationSchemaTableColumnName.ColumnName, typeof(string));
			this.schemaTable.Columns.Add(InformationSchemaTableColumnName.DataType, typeof(Type));
			this.schemaTable.Rows.Add(new object[]
			{
				"RecordId",
				typeof(int)
			});
			foreach (int num in this.keys)
			{
				Type type = this.query.GetColumnType(num).ToClrType();
				this.schemaTable.Rows.Add(new object[]
				{
					string.Format(CultureInfo.InvariantCulture, "{0}{1}", text, num),
					type
				});
			}
		}

		// Token: 0x06004FC5 RID: 20421 RVA: 0x0010B2ED File Offset: 0x001094ED
		private int GetKeyOrdinal(int i)
		{
			return this.keys[i - 1];
		}

		// Token: 0x06004FC6 RID: 20422 RVA: 0x0010B2F9 File Offset: 0x001094F9
		private Value GetColumnValue(int i)
		{
			return this.currentRecord[this.GetKeyOrdinal(i)];
		}

		// Token: 0x06004FC7 RID: 20423 RVA: 0x0010B310 File Offset: 0x00109510
		private object GetValueByIndex(int i)
		{
			if (i == 0)
			{
				return this.currentRecordId;
			}
			switch (this.GetColumnValue(i).Kind)
			{
			case ValueKind.Null:
				return DBNull.Value;
			case ValueKind.Text:
				return this.GetString(i);
			}
			throw new NotSupportedException();
		}

		// Token: 0x04002AB9 RID: 10937
		private const string RecordID = "RecordId";

		// Token: 0x04002ABA RID: 10938
		private readonly string inputTableName;

		// Token: 0x04002ABB RID: 10939
		private readonly Query query;

		// Token: 0x04002ABC RID: 10940
		private readonly IEnumerator<IValueReference> enumerator;

		// Token: 0x04002ABD RID: 10941
		private readonly int[] keys;

		// Token: 0x04002ABE RID: 10942
		private DataTable schemaTable;

		// Token: 0x04002ABF RID: 10943
		private bool isClosed;

		// Token: 0x04002AC0 RID: 10944
		private IValueReference current;

		// Token: 0x04002AC1 RID: 10945
		private RecordValue currentRecord;

		// Token: 0x04002AC2 RID: 10946
		private int currentRecordId;
	}
}
