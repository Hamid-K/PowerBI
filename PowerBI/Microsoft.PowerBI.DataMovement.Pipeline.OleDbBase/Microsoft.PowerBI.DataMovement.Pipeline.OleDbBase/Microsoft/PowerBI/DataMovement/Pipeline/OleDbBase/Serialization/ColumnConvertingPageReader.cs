using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000D0 RID: 208
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class ColumnConvertingPageReader : IPageReader, IDisposable
	{
		// Token: 0x060003B5 RID: 949 RVA: 0x0000B286 File Offset: 0x00009486
		private ColumnConvertingPageReader(IPageReader reader, Dictionary<int, int> convertedColumns, DataTable newSchemaTable, DataTable convertedSchemaTable, Action<object, Column>[] addValues)
		{
			this.reader = reader;
			this.convertedColumns = convertedColumns;
			this.newSchemaTable = newSchemaTable;
			this.convertedSchemaTable = convertedSchemaTable;
			this.addValues = addValues;
			this.cancelIssued = false;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000B2BC File Offset: 0x000094BC
		public DataTable SchemaTable
		{
			get
			{
				return this.newSchemaTable;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0000B2C4 File Offset: 0x000094C4
		public IProgress Progress
		{
			get
			{
				return this.reader.Progress;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000B2D1 File Offset: 0x000094D1
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x0000B2DB File Offset: 0x000094DB
		public bool CancelIssued
		{
			get
			{
				return this.cancelIssued;
			}
			set
			{
				this.cancelIssued = value;
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000B2E8 File Offset: 0x000094E8
		public static IPageReader New(IPageReader reader, IDictionary<int, ColumnConversion> conversions)
		{
			if (conversions.Count > 0)
			{
				DataTable dataTable = reader.SchemaTable.Copy();
				DataTable dataTable2 = new DataTable();
				dataTable2.Locale = CultureInfo.InvariantCulture;
				dataTable2.Columns.Add("ColumnName", typeof(string));
				dataTable2.Columns.Add("DataType", typeof(Type));
				dataTable2.Columns.Add("AllowDBNull", typeof(bool));
				Action<object, Column>[] array = new Action<object, Column>[conversions.Count];
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				int num = 0;
				for (int i = 0; i < reader.SchemaTable.Rows.Count; i++)
				{
					ColumnConversion columnConversion;
					if (conversions.TryGetValue(i, out columnConversion))
					{
						DataRow dataRow = reader.SchemaTable.Rows[i];
						string text = (string)dataRow["ColumnName"];
						bool flag = (bool)dataRow["AllowDBNull"];
						Type type = (Type)dataRow["DataType"];
						array[num] = columnConversion.AddValue;
						dataTable.Rows[i]["DataType"] = columnConversion.ResultType;
						dataTable2.Rows.Add(new object[] { text, columnConversion.ResultType, flag });
						dictionary.Add(i, num);
						num++;
					}
				}
				reader = new ColumnConvertingPageReader(reader, dictionary, dataTable, dataTable2, array);
			}
			return reader;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000B46F File Offset: 0x0000966F
		public IPage CreatePage()
		{
			return new ColumnConvertingPageReader.Page(this.reader.CreatePage(), this.convertedColumns, this.convertedSchemaTable, this.addValues);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000B493 File Offset: 0x00009693
		public void Read(IPage page)
		{
			((ColumnConvertingPageReader.Page)page).ReadPage(this.reader);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000B4A6 File Offset: 0x000096A6
		public void Dispose()
		{
			this.reader.Dispose();
		}

		// Token: 0x040003A2 RID: 930
		private readonly IPageReader reader;

		// Token: 0x040003A3 RID: 931
		private readonly DataTable newSchemaTable;

		// Token: 0x040003A4 RID: 932
		private readonly DataTable convertedSchemaTable;

		// Token: 0x040003A5 RID: 933
		private readonly Dictionary<int, int> convertedColumns;

		// Token: 0x040003A6 RID: 934
		private readonly Action<object, Column>[] addValues;

		// Token: 0x040003A7 RID: 935
		private volatile bool cancelIssued;

		// Token: 0x020000F5 RID: 245
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal sealed class Page : IPage, IDisposable
		{
			// Token: 0x060004F7 RID: 1271 RVA: 0x0000F168 File Offset: 0x0000D368
			internal Page(IPage page, Dictionary<int, int> convertedColumns, DataTable convertedSchemaTable, Action<object, Column>[] addValues)
			{
				this.page = page;
				this.convertedColumns = convertedColumns;
				this.convertedPage = new ColumnsPage(convertedSchemaTable);
				this.addValues = addValues;
			}

			// Token: 0x17000126 RID: 294
			// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0000F192 File Offset: 0x0000D392
			public int ColumnCount
			{
				get
				{
					return this.page.ColumnCount;
				}
			}

			// Token: 0x17000127 RID: 295
			// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0000F19F File Offset: 0x0000D39F
			public int RowCount
			{
				get
				{
					return this.page.RowCount;
				}
			}

			// Token: 0x17000128 RID: 296
			// (get) Token: 0x060004FA RID: 1274 RVA: 0x0000F1AC File Offset: 0x0000D3AC
			public IDictionary<int, IExceptionRow> ExceptionRows
			{
				get
				{
					return this.page.ExceptionRows;
				}
			}

			// Token: 0x060004FB RID: 1275 RVA: 0x0000F1BC File Offset: 0x0000D3BC
			public void ReadPage(IPageReader reader)
			{
				reader.Read(this.page);
				this.convertedPage.Clear();
				for (int i = 0; i < this.page.ColumnCount; i++)
				{
					int num;
					if (this.convertedColumns.TryGetValue(i, out num))
					{
						IColumn column = this.page.GetColumn(i);
						Column column2 = this.convertedPage.GetColumn(num);
						Action<object, Column> action = this.addValues[num];
						for (int j = 0; j < this.page.RowCount; j++)
						{
							if (column.IsNull(j))
							{
								column2.AddNull();
							}
							else
							{
								action(column.GetObject(j), column2);
							}
						}
					}
				}
			}

			// Token: 0x060004FC RID: 1276 RVA: 0x0000F26C File Offset: 0x0000D46C
			public IColumn GetColumn(int ordinal)
			{
				int num;
				if (this.convertedColumns.TryGetValue(ordinal, out num))
				{
					return this.convertedPage.GetColumn(num);
				}
				return this.page.GetColumn(ordinal);
			}

			// Token: 0x060004FD RID: 1277 RVA: 0x0000F2A2 File Offset: 0x0000D4A2
			public void Dispose()
			{
				this.page.Dispose();
				this.convertedPage.Dispose();
			}

			// Token: 0x0400041C RID: 1052
			private readonly IPage page;

			// Token: 0x0400041D RID: 1053
			private readonly ColumnsPage convertedPage;

			// Token: 0x0400041E RID: 1054
			private readonly Dictionary<int, int> convertedColumns;

			// Token: 0x0400041F RID: 1055
			private readonly Action<object, Column>[] addValues;
		}
	}
}
