using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000D1 RID: 209
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class ColumnsPage : IPage, IDisposable
	{
		// Token: 0x060003BE RID: 958 RVA: 0x0000B4B3 File Offset: 0x000096B3
		public ColumnsPage(DataTable schemaTable)
			: this(schemaTable, SchemaTableHelper.MaxRowCount(schemaTable))
		{
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000B4C2 File Offset: 0x000096C2
		public ColumnsPage(DataTable schemaTable, int maxRowCount)
		{
			this.maxRowCount = maxRowCount;
			this.columns = ColumnsPage.CreateColumns(schemaTable, this.maxRowCount);
			this.exceptionRows = new Dictionary<int, IExceptionRow>();
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000B4EE File Offset: 0x000096EE
		public int ColumnCount
		{
			get
			{
				return this.columns.Length;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000B4F8 File Offset: 0x000096F8
		public int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000B500 File Offset: 0x00009700
		public int MaxRowCount
		{
			get
			{
				return this.maxRowCount;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000B508 File Offset: 0x00009708
		public IDictionary<int, IExceptionRow> ExceptionRows
		{
			get
			{
				return this.exceptionRows;
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000B510 File Offset: 0x00009710
		IColumn IPage.GetColumn(int ordinal)
		{
			return this.GetColumn(ordinal);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000B519 File Offset: 0x00009719
		public Column GetColumn(int ordinal)
		{
			return this.columns[ordinal];
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000B524 File Offset: 0x00009724
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

		// Token: 0x060003C7 RID: 967 RVA: 0x0000B56E File Offset: 0x0000976E
		public void AddRow()
		{
			this.rowCount++;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000B580 File Offset: 0x00009780
		public void Clear()
		{
			for (int i = 0; i < this.columns.Length; i++)
			{
				this.columns[i].Clear();
			}
			this.exceptionRows.Clear();
			this.rowCount = 0;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000B5C0 File Offset: 0x000097C0
		public void Deserialize(PageReader reader)
		{
			this.Clear();
			try
			{
				this.rowCount = reader.ReadInt32();
				for (int i = 0; i < this.columns.Length; i++)
				{
					this.columns[i].Deserialize(reader);
				}
				reader.ReadExceptionRows(this.exceptionRows);
			}
			catch (EndOfStreamException)
			{
				this.Clear();
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000B628 File Offset: 0x00009828
		public void Dispose()
		{
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000B62C File Offset: 0x0000982C
		private static Column[] CreateColumns(DataTable schemaTable, int maxRowCount)
		{
			Column[] array = new Column[schemaTable.Rows.Count];
			for (int i = 0; i < array.Length; i++)
			{
				string text = (string)schemaTable.Rows[i]["ColumnName"];
				Type type = (Type)schemaTable.Rows[i]["DataType"];
				bool flag = (bool)schemaTable.Rows[i]["AllowDBNull"];
				array[i] = Column.Create(type, flag, maxRowCount);
			}
			return array;
		}

		// Token: 0x040003A8 RID: 936
		private readonly int maxRowCount;

		// Token: 0x040003A9 RID: 937
		private readonly Column[] columns;

		// Token: 0x040003AA RID: 938
		private readonly Dictionary<int, IExceptionRow> exceptionRows;

		// Token: 0x040003AB RID: 939
		private int rowCount;
	}
}
