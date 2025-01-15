using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000024 RID: 36
	internal abstract class ParsingDataReader : DbDataReader
	{
		// Token: 0x060001BA RID: 442 RVA: 0x00007E59 File Offset: 0x00006059
		protected ParsingDataReader(SapBwCommand command, int startRow = 1)
		{
			this.command = command;
			this.connection = (SapBwConnection)command.Connection;
			this.StartRow = startRow;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00007E80 File Offset: 0x00006080
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00007E88 File Offset: 0x00006088
		protected int StartRow
		{
			get
			{
				return this.startRow;
			}
			set
			{
				this.startRow = value;
				this.endRow = this.startRow + this.connection.BatchSize - 1;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00007EAB File Offset: 0x000060AB
		protected int EndRow
		{
			get
			{
				return this.endRow;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001BE RID: 446
		protected abstract ColumnProvider ColumnProvider { get; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001BF RID: 447
		protected abstract int BatchSize { get; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00007EB3 File Offset: 0x000060B3
		public override int Depth
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00007EB6 File Offset: 0x000060B6
		public override bool IsClosed
		{
			get
			{
				return this.exhausted;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00007EBE File Offset: 0x000060BE
		public override int RecordsAffected
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00007EC1 File Offset: 0x000060C1
		public override bool HasRows
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00007EC8 File Offset: 0x000060C8
		public override int FieldCount
		{
			get
			{
				return this.ColumnProvider.ColumnCount;
			}
		}

		// Token: 0x17000068 RID: 104
		public override object this[string name]
		{
			get
			{
				int num;
				if (this.row != null && this.ColumnProvider.TryGetColumnIndex(name, out num))
				{
					return this.row[num];
				}
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x17000069 RID: 105
		public override object this[int i]
		{
			get
			{
				this.AssertIndex(i);
				return this.row[i];
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00007F1C File Offset: 0x0000611C
		private void AssertIndex(int i)
		{
			if (this.row == null || i < 0 || i >= this.row.Length)
			{
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x060001C8 RID: 456
		protected abstract void EnsureInitialized();

		// Token: 0x060001C9 RID: 457
		public abstract override void Close();

		// Token: 0x060001CA RID: 458 RVA: 0x00007F3B File Offset: 0x0000613B
		public override bool GetBoolean(int i)
		{
			return (bool)this[i];
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00007F49 File Offset: 0x00006149
		public override byte GetByte(int i)
		{
			return (byte)this[i];
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00007F58 File Offset: 0x00006158
		public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (fieldOffset < 0L)
			{
				throw new ArgumentOutOfRangeException("fieldOffset");
			}
			if (bufferOffset < 0 || (bufferOffset > 0 && buffer != null && bufferOffset >= buffer.Length))
			{
				throw new ArgumentOutOfRangeException("bufferOffset");
			}
			this.AssertIndex(i);
			byte[] array = (byte[])this.row[i];
			if (buffer == null)
			{
				return (long)array.Length;
			}
			long num = Math.Min((long)array.Length - fieldOffset, (long)length);
			if (num > 0L)
			{
				Array.Copy(array, fieldOffset, buffer, (long)bufferOffset, num);
				return num;
			}
			return 0L;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00007FE5 File Offset: 0x000061E5
		public override char GetChar(int i)
		{
			return (char)this[i];
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00007FF4 File Offset: 0x000061F4
		public override long GetChars(int i, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (fieldOffset < 0L)
			{
				throw new ArgumentOutOfRangeException("fieldOffset");
			}
			if (bufferOffset < 0 || (bufferOffset > 0 && buffer != null && bufferOffset >= buffer.Length))
			{
				throw new ArgumentOutOfRangeException("bufferOffset");
			}
			this.AssertIndex(i);
			char[] array = (char[])this.row[i];
			if (buffer == null)
			{
				return (long)array.Length;
			}
			long num = Math.Min((long)array.Length - fieldOffset, (long)length);
			if (num > 0L)
			{
				Array.Copy(array, fieldOffset, buffer, (long)bufferOffset, num);
				return num;
			}
			return 0L;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00008084 File Offset: 0x00006284
		public override string GetDataTypeName(int i)
		{
			Type fieldType = this.GetFieldType(i);
			if (!(fieldType == null))
			{
				return fieldType.Name;
			}
			return null;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000080AA File Offset: 0x000062AA
		public override DateTime GetDateTime(int i)
		{
			return (DateTime)this[i];
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000080B8 File Offset: 0x000062B8
		public override decimal GetDecimal(int i)
		{
			return (decimal)this[i];
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000080C6 File Offset: 0x000062C6
		public override double GetDouble(int i)
		{
			return (double)this[i];
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000080D4 File Offset: 0x000062D4
		public override Type GetFieldType(int i)
		{
			if (this.ColumnProvider.IsValidIndex(i))
			{
				return this.ColumnProvider.Columns[i].ClrType;
			}
			throw new IndexOutOfRangeException();
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00008100 File Offset: 0x00006300
		public override float GetFloat(int i)
		{
			return (float)this[i];
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000810E File Offset: 0x0000630E
		public override Guid GetGuid(int i)
		{
			return (Guid)this[i];
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000811C File Offset: 0x0000631C
		public override short GetInt16(int i)
		{
			return (short)this[i];
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000812A File Offset: 0x0000632A
		public override int GetInt32(int i)
		{
			return (int)this[i];
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00008138 File Offset: 0x00006338
		public override long GetInt64(int i)
		{
			return (long)this[i];
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00008148 File Offset: 0x00006348
		public override string GetName(int i)
		{
			string text;
			if (this.ColumnProvider.TryGetColumnName(i, out text))
			{
				return text;
			}
			throw new IndexOutOfRangeException();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000816C File Offset: 0x0000636C
		public override int GetOrdinal(string name)
		{
			int num;
			if (this.ColumnProvider.TryGetColumnIndex(name, out num))
			{
				return num;
			}
			return -1;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000818C File Offset: 0x0000638C
		public override string GetString(int i)
		{
			object obj = this[i];
			if (this[i] is string)
			{
				return obj as string;
			}
			return obj.ToString();
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000081BC File Offset: 0x000063BC
		public override object GetValue(int i)
		{
			return this[i];
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000081C8 File Offset: 0x000063C8
		public override int GetValues(object[] values)
		{
			int num = ((values.Length < this.FieldCount) ? values.Length : this.FieldCount);
			if (this.row != null)
			{
				for (int i = 0; i < num; i++)
				{
					values[i] = this.row[i];
				}
			}
			return num;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000820C File Offset: 0x0000640C
		public override bool IsDBNull(int i)
		{
			return this[i] == DBNull.Value;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000821C File Offset: 0x0000641C
		public override bool NextResult()
		{
			return false;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00008220 File Offset: 0x00006420
		public override bool Read()
		{
			if (!this.exhausted)
			{
				object[] array;
				if (this.TryGetRow(out array))
				{
					this.row = array;
					return true;
				}
				this.exhausted = true;
			}
			return false;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00008250 File Offset: 0x00006450
		protected virtual bool TryGetRow(out object[] newRow)
		{
			if (this.currentRowCount == this.BatchSize)
			{
				if (this.rowEnumerator.MoveNext())
				{
					throw new InvalidOperationException(Resources.FoundExtraRows);
				}
				this.rowEnumerator.Dispose();
				this.rowEnumerator = null;
				this.currentRowCount = 0;
				this.StartRow = this.endRow + 1;
			}
			this.EnsureInitialized();
			if (this.rowEnumerator.MoveNext())
			{
				newRow = this.rowEnumerator.Current;
				this.currentRowCount++;
				return true;
			}
			newRow = null;
			return false;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000082E3 File Offset: 0x000064E3
		public override IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000082EC File Offset: 0x000064EC
		public override DataTable GetSchemaTable()
		{
			if (this.schemaTable == null)
			{
				if (this.ColumnProvider == null)
				{
					throw new InvalidOperationException(Resources.FailedToBuildSchemaTable);
				}
				this.schemaTable = new DataTable("SchemaTable")
				{
					Locale = CultureInfo.InvariantCulture,
					MinimumCapacity = this.ColumnProvider.ColumnCount
				};
				this.schemaTable.Columns.Add(SchemaTableColumn.ColumnName, typeof(string));
				this.schemaTable.Columns.Add(SchemaTableColumn.ColumnOrdinal, typeof(int));
				this.schemaTable.Columns.Add(SchemaTableColumn.ColumnSize, typeof(int));
				this.schemaTable.Columns.Add(SchemaTableColumn.NumericPrecision, typeof(short));
				this.schemaTable.Columns.Add(SchemaTableColumn.NumericScale, typeof(short));
				this.schemaTable.Columns.Add(SchemaTableColumn.DataType, typeof(Type));
				this.schemaTable.Columns.Add(SchemaTableColumn.ProviderType, typeof(int));
				this.schemaTable.Columns.Add(SchemaTableColumn.IsLong, typeof(bool));
				this.schemaTable.Columns.Add(SchemaTableColumn.AllowDBNull, typeof(bool));
				this.schemaTable.Columns.Add(SchemaTableColumn.IsUnique, typeof(bool));
				this.schemaTable.Columns.Add(SchemaTableColumn.IsKey, typeof(bool));
				this.schemaTable.Columns.Add(SchemaTableColumn.BaseSchemaName, typeof(string));
				this.schemaTable.Columns.Add(SchemaTableColumn.BaseTableName, typeof(string));
				this.schemaTable.Columns.Add(SchemaTableColumn.BaseColumnName, typeof(string));
				this.schemaTable.Columns.Add("DataTypeName", typeof(string));
				int num = 0;
				foreach (MdxColumn mdxColumn in this.ColumnProvider.Columns)
				{
					DataRow dataRow = this.schemaTable.NewRow();
					dataRow[0] = mdxColumn.ColumnName;
					dataRow[1] = num;
					dataRow[2] = mdxColumn.Length;
					dataRow[3] = mdxColumn.Precision.GetValueOrDefault();
					dataRow[4] = 0;
					dataRow[5] = mdxColumn.ClrType;
					dataRow[6] = (int)mdxColumn.DataType;
					dataRow[7] = false;
					dataRow[8] = true;
					dataRow[9] = false;
					dataRow[10] = false;
					dataRow[11] = null;
					dataRow[12] = this.ColumnProvider.Table;
					dataRow[13] = mdxColumn.FieldName;
					dataRow[14] = mdxColumn.DataTypeName;
					this.schemaTable.Rows.Add(dataRow);
					num++;
				}
				foreach (object obj in this.schemaTable.Columns)
				{
					((DataColumn)obj).ReadOnly = false;
				}
				this.schemaTable.AcceptChanges();
			}
			return this.schemaTable;
		}

		// Token: 0x040000AA RID: 170
		protected readonly SapBwCommand command;

		// Token: 0x040000AB RID: 171
		protected readonly SapBwConnection connection;

		// Token: 0x040000AC RID: 172
		protected IEnumerator<object[]> rowEnumerator;

		// Token: 0x040000AD RID: 173
		protected bool exhausted;

		// Token: 0x040000AE RID: 174
		private DataTable schemaTable;

		// Token: 0x040000AF RID: 175
		private object[] row;

		// Token: 0x040000B0 RID: 176
		private int currentRowCount;

		// Token: 0x040000B1 RID: 177
		private int startRow;

		// Token: 0x040000B2 RID: 178
		private int endRow;
	}
}
