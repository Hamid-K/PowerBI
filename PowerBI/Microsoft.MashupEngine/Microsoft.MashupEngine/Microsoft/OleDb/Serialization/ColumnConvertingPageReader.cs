using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02001FAD RID: 8109
	public class ColumnConvertingPageReader : IPageReader, IDisposable
	{
		// Token: 0x0600C5C0 RID: 50624 RVA: 0x002769A0 File Offset: 0x00274BA0
		public static IPageReader New(IPageReader reader, IDictionary<int, ColumnConversion> conversions)
		{
			if (conversions.Count > 0)
			{
				TableSchema tableSchema = reader.Schema.Copy();
				TableSchema tableSchema2 = new TableSchema(conversions.Count);
				Action<object, Column>[] array = new Action<object, Column>[conversions.Count];
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				int num = 0;
				for (int i = 0; i < tableSchema.ColumnCount; i++)
				{
					ColumnConversion columnConversion;
					if (conversions.TryGetValue(i, out columnConversion))
					{
						array[num] = columnConversion.AddValue;
						SchemaColumn column = tableSchema.GetColumn(i);
						column.DataType = columnConversion.ResultType;
						tableSchema2.AddColumn(column.Name, column.DataType, column.Nullable);
						dictionary.Add(i, num);
						num++;
					}
				}
				reader = new ColumnConvertingPageReader(reader, dictionary, tableSchema, tableSchema2, array);
			}
			return reader;
		}

		// Token: 0x0600C5C1 RID: 50625 RVA: 0x00276A64 File Offset: 0x00274C64
		private ColumnConvertingPageReader(IPageReader reader, Dictionary<int, int> convertedColumns, TableSchema newSchemaTable, TableSchema convertedSchemaTable, Action<object, Column>[] addValues)
		{
			this.reader = reader;
			this.convertedColumns = convertedColumns;
			this.newSchemaTable = newSchemaTable;
			this.convertedSchemaTable = convertedSchemaTable;
			this.addValues = addValues;
			this.maxRowCount = SchemaTableHelper.MaxRowCount(this.reader.Schema);
		}

		// Token: 0x17002FFF RID: 12287
		// (get) Token: 0x0600C5C2 RID: 50626 RVA: 0x00276AB2 File Offset: 0x00274CB2
		public TableSchema Schema
		{
			get
			{
				return this.newSchemaTable;
			}
		}

		// Token: 0x17003000 RID: 12288
		// (get) Token: 0x0600C5C3 RID: 50627 RVA: 0x00276ABA File Offset: 0x00274CBA
		public IProgress Progress
		{
			get
			{
				return this.reader.Progress;
			}
		}

		// Token: 0x17003001 RID: 12289
		// (get) Token: 0x0600C5C4 RID: 50628 RVA: 0x00276AC7 File Offset: 0x00274CC7
		public int MaxPageRowCount
		{
			get
			{
				return this.maxRowCount;
			}
		}

		// Token: 0x0600C5C5 RID: 50629 RVA: 0x00276ACF File Offset: 0x00274CCF
		public IPage CreatePage()
		{
			return new ColumnConvertingPageReader.Page(this.reader.CreatePage(), this.convertedColumns, this.convertedSchemaTable, this.addValues, this.maxRowCount);
		}

		// Token: 0x0600C5C6 RID: 50630 RVA: 0x00276AF9 File Offset: 0x00274CF9
		public void Read(IPage page)
		{
			((ColumnConvertingPageReader.Page)page).ReadPage(this.reader);
		}

		// Token: 0x0600C5C7 RID: 50631 RVA: 0x000020FA File Offset: 0x000002FA
		public IPageReader NextResult()
		{
			return null;
		}

		// Token: 0x0600C5C8 RID: 50632 RVA: 0x00276B0C File Offset: 0x00274D0C
		public void Dispose()
		{
			this.reader.Dispose();
		}

		// Token: 0x0400650F RID: 25871
		private readonly IPageReader reader;

		// Token: 0x04006510 RID: 25872
		private readonly TableSchema newSchemaTable;

		// Token: 0x04006511 RID: 25873
		private readonly TableSchema convertedSchemaTable;

		// Token: 0x04006512 RID: 25874
		private readonly Dictionary<int, int> convertedColumns;

		// Token: 0x04006513 RID: 25875
		private readonly Action<object, Column>[] addValues;

		// Token: 0x04006514 RID: 25876
		private readonly int maxRowCount;

		// Token: 0x02001FAE RID: 8110
		private sealed class Page : IPage, IDisposable
		{
			// Token: 0x0600C5C9 RID: 50633 RVA: 0x00276B19 File Offset: 0x00274D19
			public Page(IPage page, Dictionary<int, int> convertedColumns, TableSchema convertedSchemaTable, Action<object, Column>[] addValues, int maxRowCount)
			{
				this.page = page;
				this.convertedColumns = convertedColumns;
				this.convertedPage = new ColumnsPage(convertedSchemaTable, maxRowCount);
				this.addValues = addValues;
			}

			// Token: 0x17003002 RID: 12290
			// (get) Token: 0x0600C5CA RID: 50634 RVA: 0x00276B45 File Offset: 0x00274D45
			public int ColumnCount
			{
				get
				{
					return this.page.ColumnCount;
				}
			}

			// Token: 0x17003003 RID: 12291
			// (get) Token: 0x0600C5CB RID: 50635 RVA: 0x00276B52 File Offset: 0x00274D52
			public int RowCount
			{
				get
				{
					return this.page.RowCount;
				}
			}

			// Token: 0x17003004 RID: 12292
			// (get) Token: 0x0600C5CC RID: 50636 RVA: 0x00276B5F File Offset: 0x00274D5F
			public IDictionary<int, IExceptionRow> ExceptionRows
			{
				get
				{
					return this.page.ExceptionRows;
				}
			}

			// Token: 0x17003005 RID: 12293
			// (get) Token: 0x0600C5CD RID: 50637 RVA: 0x00276B6C File Offset: 0x00274D6C
			public ISerializedException PageException
			{
				get
				{
					return this.page.PageException;
				}
			}

			// Token: 0x0600C5CE RID: 50638 RVA: 0x00276B79 File Offset: 0x00274D79
			public void ReadPage(IPageReader reader)
			{
				reader.Read(this.page);
				this.convertedPage.Clear(null);
			}

			// Token: 0x0600C5CF RID: 50639 RVA: 0x00276B94 File Offset: 0x00274D94
			public IColumn GetColumn(int ordinal)
			{
				int num;
				if (this.convertedColumns.TryGetValue(ordinal, out num))
				{
					this.SyncConvertedPage();
					return this.convertedPage.GetColumn(num);
				}
				return this.page.GetColumn(ordinal);
			}

			// Token: 0x0600C5D0 RID: 50640 RVA: 0x00276BD0 File Offset: 0x00274DD0
			public void Dispose()
			{
				this.page.Dispose();
				this.convertedPage.Dispose();
			}

			// Token: 0x0600C5D1 RID: 50641 RVA: 0x00276BE8 File Offset: 0x00274DE8
			private void SyncConvertedPage()
			{
				if (this.convertedPage.RowCount == 0)
				{
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
					this.convertedPage.AddRows(this.page.RowCount);
				}
			}

			// Token: 0x04006515 RID: 25877
			private readonly IPage page;

			// Token: 0x04006516 RID: 25878
			private readonly ColumnsPage convertedPage;

			// Token: 0x04006517 RID: 25879
			private readonly Dictionary<int, int> convertedColumns;

			// Token: 0x04006518 RID: 25880
			private readonly Action<object, Column>[] addValues;
		}
	}
}
