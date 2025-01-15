using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02001FAF RID: 8111
	public class ColumnsPage : IPage, IDisposable
	{
		// Token: 0x0600C5D2 RID: 50642 RVA: 0x00276CA4 File Offset: 0x00274EA4
		public ColumnsPage(TableSchema schema)
			: this(schema, SchemaTableHelper.MaxRowCount(schema))
		{
		}

		// Token: 0x0600C5D3 RID: 50643 RVA: 0x00276CB3 File Offset: 0x00274EB3
		public ColumnsPage(TableSchema schema, int maxRowCount)
		{
			this.maxRowCount = maxRowCount;
			this.columns = Column.CreateColumns(schema, this.maxRowCount);
			this.exceptionRows = new Dictionary<int, IExceptionRow>();
		}

		// Token: 0x17003006 RID: 12294
		// (get) Token: 0x0600C5D4 RID: 50644 RVA: 0x00276CDF File Offset: 0x00274EDF
		public int ColumnCount
		{
			get
			{
				return this.columns.Length;
			}
		}

		// Token: 0x17003007 RID: 12295
		// (get) Token: 0x0600C5D5 RID: 50645 RVA: 0x00276CE9 File Offset: 0x00274EE9
		public int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x17003008 RID: 12296
		// (get) Token: 0x0600C5D6 RID: 50646 RVA: 0x00276CF1 File Offset: 0x00274EF1
		public int MaxRowCount
		{
			get
			{
				return this.maxRowCount;
			}
		}

		// Token: 0x17003009 RID: 12297
		// (get) Token: 0x0600C5D7 RID: 50647 RVA: 0x00276CF9 File Offset: 0x00274EF9
		public IDictionary<int, IExceptionRow> ExceptionRows
		{
			get
			{
				return this.exceptionRows;
			}
		}

		// Token: 0x1700300A RID: 12298
		// (get) Token: 0x0600C5D8 RID: 50648 RVA: 0x00276D01 File Offset: 0x00274F01
		public ISerializedException PageException
		{
			get
			{
				return this.pageException;
			}
		}

		// Token: 0x0600C5D9 RID: 50649 RVA: 0x00276D09 File Offset: 0x00274F09
		IColumn IPage.GetColumn(int ordinal)
		{
			return this.GetColumn(ordinal);
		}

		// Token: 0x0600C5DA RID: 50650 RVA: 0x00276D12 File Offset: 0x00274F12
		public Column GetColumn(int ordinal)
		{
			return this.columns[ordinal];
		}

		// Token: 0x0600C5DB RID: 50651 RVA: 0x00276D1C File Offset: 0x00274F1C
		public void AddRow(object[] row)
		{
			for (int i = 0; i < row.Length; i++)
			{
				object obj = row[i];
				if (obj == DBNull.Value)
				{
					this.columns[i].AddNull();
				}
				else
				{
					this.columns[i].AddValue(obj);
				}
			}
			this.AddRow();
		}

		// Token: 0x0600C5DC RID: 50652 RVA: 0x00276D66 File Offset: 0x00274F66
		public void AddRow()
		{
			this.AddRows(1);
		}

		// Token: 0x0600C5DD RID: 50653 RVA: 0x00276D6F File Offset: 0x00274F6F
		public void AddRows(int count)
		{
			this.rowCount += count;
		}

		// Token: 0x0600C5DE RID: 50654 RVA: 0x00276D80 File Offset: 0x00274F80
		public void Clear(ISerializedException pageException = null)
		{
			for (int i = 0; i < this.columns.Length; i++)
			{
				this.columns[i].Clear();
			}
			this.exceptionRows.Clear();
			this.rowCount = 0;
			this.pageException = pageException;
		}

		// Token: 0x0600C5DF RID: 50655 RVA: 0x00276DC8 File Offset: 0x00274FC8
		public void Deserialize(PageReader reader)
		{
			this.Clear(null);
			this.rowCount = reader.ReadInt32();
			for (int i = 0; i < this.columns.Length; i++)
			{
				this.columns[i].Deserialize(reader);
			}
			reader.ReadExceptionRows(this.exceptionRows);
			if (reader.ReadInt32() != 0)
			{
				this.pageException = reader.ReadException();
			}
		}

		// Token: 0x0600C5E0 RID: 50656 RVA: 0x0000336E File Offset: 0x0000156E
		public void Dispose()
		{
		}

		// Token: 0x04006519 RID: 25881
		private readonly int maxRowCount;

		// Token: 0x0400651A RID: 25882
		private readonly Column[] columns;

		// Token: 0x0400651B RID: 25883
		private readonly Dictionary<int, IExceptionRow> exceptionRows;

		// Token: 0x0400651C RID: 25884
		private int rowCount;

		// Token: 0x0400651D RID: 25885
		private ISerializedException pageException;
	}
}
