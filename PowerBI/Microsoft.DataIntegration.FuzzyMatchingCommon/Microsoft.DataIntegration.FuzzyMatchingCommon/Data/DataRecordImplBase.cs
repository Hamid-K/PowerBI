using System;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Data
{
	// Token: 0x02000056 RID: 86
	[Serializable]
	public abstract class DataRecordImplBase : IDataRecord
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00014F5C File Offset: 0x0001315C
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x00014F64 File Offset: 0x00013164
		public DataTable Schema
		{
			get
			{
				return this.m_schema;
			}
			set
			{
				if (!SchemaUtils.SchemaRowOrderMatchesColumnOrdinal(value))
				{
					throw new ArgumentException("Schema table column ordinal column does not match the schema table row order.");
				}
				this.m_schema = value;
			}
		}

		// Token: 0x17000070 RID: 112
		public abstract object this[int i] { get; set; }

		// Token: 0x060002D6 RID: 726
		public abstract DataTable GetSchemaTable();

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00014F80 File Offset: 0x00013180
		public virtual int FieldCount
		{
			get
			{
				return this.GetSchemaTable().Rows.Count;
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00014F92 File Offset: 0x00013192
		public virtual object GetValue(int i)
		{
			if (!this.IsDBNull(i))
			{
				return this[i];
			}
			return null;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00014FA6 File Offset: 0x000131A6
		public virtual bool GetBoolean(int i)
		{
			return (bool)this[i];
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00014FB4 File Offset: 0x000131B4
		public virtual byte GetByte(int i)
		{
			return (byte)this[i];
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00014FC2 File Offset: 0x000131C2
		public virtual char GetChar(int i)
		{
			return (char)this[i];
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00014FD0 File Offset: 0x000131D0
		public virtual decimal GetDecimal(int i)
		{
			return (decimal)this[i];
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00014FDE File Offset: 0x000131DE
		public virtual double GetDouble(int i)
		{
			return (double)this[i];
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00014FEC File Offset: 0x000131EC
		public virtual float GetFloat(int i)
		{
			return (float)this[i];
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00014FFA File Offset: 0x000131FA
		public virtual short GetInt16(int i)
		{
			return (short)this[i];
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00015008 File Offset: 0x00013208
		public virtual int GetInt32(int i)
		{
			return (int)this[i];
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00015016 File Offset: 0x00013216
		public virtual long GetInt64(int i)
		{
			return (long)this[i];
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00015024 File Offset: 0x00013224
		public virtual string GetString(int i)
		{
			if (this.IsDBNull(i))
			{
				return null;
			}
			if (this[i] is ArraySegment<char>)
			{
				return DataRecordImplBase.ToString((ArraySegment<char>)this[i]);
			}
			return this[i].ToString();
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0001505D File Offset: 0x0001325D
		private static string ToString(ArraySegment<char> segment)
		{
			if (segment.Array != null && segment.Count != 0)
			{
				return new string(segment.Array, segment.Offset, segment.Count);
			}
			return string.Empty;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00015091 File Offset: 0x00013291
		public virtual DateTime GetDateTime(int i)
		{
			return (DateTime)this[i];
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0001509F File Offset: 0x0001329F
		public virtual Guid GetGuid(int i)
		{
			return (Guid)this[i];
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000150AD File Offset: 0x000132AD
		public virtual IDataReader GetData(int i)
		{
			if (!this.IsDBNull(i))
			{
				return (IDataReader)this[i];
			}
			return null;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x000150C6 File Offset: 0x000132C6
		public virtual bool IsDBNull(int i)
		{
			return this[i] == null || DBNull.Value == this[i];
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000150E1 File Offset: 0x000132E1
		public virtual int GetOrdinal(string name)
		{
			return DataRecordImplBase.GetOrdinal(this.GetSchemaTable(), name, true);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000150F0 File Offset: 0x000132F0
		public virtual string GetName(int i)
		{
			return DataRecordImplBase.GetName(this.GetSchemaTable(), i);
		}

		// Token: 0x17000072 RID: 114
		public virtual object this[string name]
		{
			get
			{
				return this[this.GetOrdinal(name)];
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0001510D File Offset: 0x0001330D
		public virtual Type GetFieldType(int i)
		{
			return (Type)this.GetSchemaTable().Rows[i][SchemaTableColumn.DataType];
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0001512F File Offset: 0x0001332F
		public virtual string GetDataTypeName(int i)
		{
			return DataRecordImplBase.GetDataTypeName(this.GetSchemaTable(), i);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00015140 File Offset: 0x00013340
		public virtual int GetValues(object[] values)
		{
			int num = Math.Min(values.GetLength(0), this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this[i];
			}
			return num;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00015178 File Offset: 0x00013378
		public virtual long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			byte[] array = this[i] as byte[];
			if (this[i] == null)
			{
				return 0L;
			}
			if (buffer == null)
			{
				return (long)array.Length;
			}
			Array.Copy(array, fieldOffset, buffer, (long)bufferOffset, (long)length);
			return Math.Min((long)array.Length - fieldOffset, (long)Math.Min(buffer.Length - bufferOffset, length));
		}

		// Token: 0x060002EF RID: 751 RVA: 0x000151D0 File Offset: 0x000133D0
		public virtual long GetChars(int i, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			if (this[i] is char[])
			{
				char[] array = this[i] as char[];
				if (buffer == null)
				{
					return (long)array.Length;
				}
				Array.Copy(array, fieldOffset, buffer, (long)bufferOffset, (long)length);
				return Math.Min((long)array.Length - fieldOffset, (long)Math.Min(buffer.Length - bufferOffset, length));
			}
			else if (this[i] is ArraySegment<char>)
			{
				ArraySegment<char> arraySegment = (ArraySegment<char>)this[i];
				if (buffer == null)
				{
					return (long)arraySegment.Count;
				}
				Array.Copy(arraySegment.Array, (long)arraySegment.Offset + fieldOffset, buffer, (long)bufferOffset, (long)length);
				return Math.Min((long)arraySegment.Count - fieldOffset, (long)Math.Min(buffer.Length - bufferOffset, length));
			}
			else
			{
				string text;
				if (this[i] is string)
				{
					text = this[i] as string;
				}
				else if (this[i] == null)
				{
					text = string.Empty;
				}
				else
				{
					text = this[i].ToString();
				}
				if (buffer == null)
				{
					return (long)text.Length;
				}
				text.CopyToEx((int)fieldOffset, buffer, bufferOffset, length);
				return Math.Min((long)text.Length - fieldOffset, (long)Math.Min(buffer.Length - bufferOffset, length));
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x000152FC File Offset: 0x000134FC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.FieldCount; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(" | ");
				}
				if (this.IsDBNull(i))
				{
					stringBuilder.Append("null");
				}
				else
				{
					stringBuilder.Append(this[i].ToString());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00015360 File Offset: 0x00013560
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

		// Token: 0x060002F2 RID: 754 RVA: 0x00015418 File Offset: 0x00013618
		protected static string GetDataTypeName(DataTable schemaTable, int i)
		{
			return DataRecordImplBase.GetSchemaRow(schemaTable, i)[SchemaTableColumn.DataType].ToString();
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00015430 File Offset: 0x00013630
		protected static string GetName(DataTable schemaTable, int i)
		{
			return DataRecordImplBase.GetSchemaRow(schemaTable, i)[SchemaTableColumn.ColumnName] as string;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00015448 File Offset: 0x00013648
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

		// Token: 0x04000077 RID: 119
		private DataTable m_schema;
	}
}
