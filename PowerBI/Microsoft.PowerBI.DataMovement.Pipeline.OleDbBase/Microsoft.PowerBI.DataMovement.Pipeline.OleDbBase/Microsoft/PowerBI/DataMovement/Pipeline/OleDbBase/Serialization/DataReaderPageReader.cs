using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000D2 RID: 210
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class DataReaderPageReader : IPageReader, IDisposable
	{
		// Token: 0x060003CC RID: 972 RVA: 0x0000B6B8 File Offset: 0x000098B8
		public DataReaderPageReader(IDataReader reader)
			: this(reader, null)
		{
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000B6C4 File Offset: 0x000098C4
		public DataReaderPageReader(IDataReader reader, DataReaderPageReader.ExceptionPropertyGetter propertyGetter)
		{
			if (propertyGetter == null)
			{
				DataReaderPageReader.ExceptionPropertyGetter exceptionPropertyGetter;
				if ((exceptionPropertyGetter = DataReaderPageReader.<>O.<0>__DefaultExceptionPropertyGetter) == null)
				{
					exceptionPropertyGetter = (DataReaderPageReader.<>O.<0>__DefaultExceptionPropertyGetter = new DataReaderPageReader.ExceptionPropertyGetter(DataReaderPageReader.DefaultExceptionPropertyGetter));
				}
				propertyGetter = exceptionPropertyGetter;
			}
			this.reader = reader;
			this.schemaTable = reader.GetSchemaTable();
			this.maxRowCount = SchemaTableHelper.MaxRowCount(this.schemaTable);
			this.propertyGetter = propertyGetter;
			this.progress = new ReaderWriterProgress();
			this.first = true;
			this.cancelIssued = false;
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000B73D File Offset: 0x0000993D
		public DataTable SchemaTable
		{
			get
			{
				return this.schemaTable;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000B745 File Offset: 0x00009945
		public IProgress Progress
		{
			get
			{
				return this.progress;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000B74D File Offset: 0x0000994D
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x0000B757 File Offset: 0x00009957
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

		// Token: 0x060003D2 RID: 978 RVA: 0x0000B762 File Offset: 0x00009962
		public void Read(IPage page)
		{
			this.Read((DataReaderPageReader.ObjectPage)page);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000B770 File Offset: 0x00009970
		private void Read(DataReaderPageReader.ObjectPage page)
		{
			page.Clear();
			if (!this.eof)
			{
				int num = (this.first ? Math.Min(100, this.maxRowCount) : this.maxRowCount);
				if (!page.Read(this.reader, num))
				{
					this.eof = true;
				}
				this.first = false;
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000B7C6 File Offset: 0x000099C6
		public void Dispose()
		{
			this.reader.Dispose();
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000B7D3 File Offset: 0x000099D3
		public IPage CreatePage()
		{
			return new DataReaderPageReader.ObjectPage(this.schemaTable, this.propertyGetter, this.progress);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000B7EC File Offset: 0x000099EC
		private static bool DefaultExceptionPropertyGetter(Exception e, out IDictionary<string, string> properties)
		{
			properties = null;
			return false;
		}

		// Token: 0x040003AC RID: 940
		private readonly IDataReader reader;

		// Token: 0x040003AD RID: 941
		private readonly DataTable schemaTable;

		// Token: 0x040003AE RID: 942
		private readonly int maxRowCount;

		// Token: 0x040003AF RID: 943
		private readonly DataReaderPageReader.ExceptionPropertyGetter propertyGetter;

		// Token: 0x040003B0 RID: 944
		private readonly ReaderWriterProgress progress;

		// Token: 0x040003B1 RID: 945
		private bool eof;

		// Token: 0x040003B2 RID: 946
		private bool first;

		// Token: 0x040003B3 RID: 947
		private volatile bool cancelIssued;

		// Token: 0x020000F6 RID: 246
		// (Invoke) Token: 0x060004FF RID: 1279
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public delegate bool ExceptionPropertyGetter(Exception e, out IDictionary<string, string> properties);

		// Token: 0x020000F7 RID: 247
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal class ObjectPage : IPage, IDisposable
		{
			// Token: 0x06000502 RID: 1282 RVA: 0x0000F2BC File Offset: 0x0000D4BC
			internal ObjectPage(DataTable schemaTable, DataReaderPageReader.ExceptionPropertyGetter propertyGetter, ReaderWriterProgress progress)
			{
				int num = SchemaTableHelper.MaxRowCount(schemaTable);
				this.columnCount = schemaTable.Rows.Count;
				this.values = new object[this.columnCount * num];
				this.propertyGetter = propertyGetter;
				this.progress = progress;
				this.exceptionRows = new Dictionary<int, IExceptionRow>();
				this.columnsPage = new ColumnsPage(schemaTable);
				this.rowCount = 0;
			}

			// Token: 0x17000129 RID: 297
			// (get) Token: 0x06000503 RID: 1283 RVA: 0x0000F326 File Offset: 0x0000D526
			public int ColumnCount
			{
				get
				{
					return this.columnCount;
				}
			}

			// Token: 0x1700012A RID: 298
			// (get) Token: 0x06000504 RID: 1284 RVA: 0x0000F32E File Offset: 0x0000D52E
			public int RowCount
			{
				get
				{
					return this.rowCount;
				}
			}

			// Token: 0x1700012B RID: 299
			// (get) Token: 0x06000505 RID: 1285 RVA: 0x0000F336 File Offset: 0x0000D536
			public IDictionary<int, IExceptionRow> ExceptionRows
			{
				get
				{
					return this.exceptionRows;
				}
			}

			// Token: 0x06000506 RID: 1286 RVA: 0x0000F33E File Offset: 0x0000D53E
			public IColumn GetColumn(int ordinal)
			{
				this.SyncColumnsPage();
				return this.columnsPage.GetColumn(ordinal);
			}

			// Token: 0x06000507 RID: 1287 RVA: 0x0000F352 File Offset: 0x0000D552
			public void Clear()
			{
				this.exceptionRows.Clear();
				Array.Clear(this.values, 0, this.rowCount * this.columnCount);
				this.rowCount = 0;
				this.columnsPage.Clear();
			}

			// Token: 0x06000508 RID: 1288 RVA: 0x0000F38A File Offset: 0x0000D58A
			public void Dispose()
			{
				this.columnsPage.Dispose();
			}

			// Token: 0x06000509 RID: 1289 RVA: 0x0000F398 File Offset: 0x0000D598
			public bool Read(IDataReader reader, int rowCount)
			{
				int num = 0;
				for (int i = 0; i < rowCount; i++)
				{
					try
					{
						if (!reader.Read())
						{
							return false;
						}
					}
					catch (Exception ex)
					{
						for (int j = 0; j < this.columnCount; j++)
						{
							this.values[num + j] = DBNull.Value;
						}
						IDictionary<string, string> dictionary;
						if (this.propertyGetter(ex, out dictionary))
						{
							this.exceptionRows.Add(i, this.GetExceptionRow(dictionary));
							this.rowCount++;
							this.progress.OnRows(1, 1);
							return false;
						}
						throw;
					}
					Dictionary<int, IDictionary<string, string>> dictionary2 = null;
					for (int k = 0; k < this.columnCount; k++)
					{
						try
						{
							this.values[num + k] = reader.GetValue(k);
						}
						catch (Exception ex2)
						{
							this.values[num + k] = DBNull.Value;
							if (dictionary2 == null)
							{
								dictionary2 = new Dictionary<int, IDictionary<string, string>>();
							}
							IDictionary<string, string> dictionary3;
							if (!this.propertyGetter(ex2, out dictionary3))
							{
								throw;
							}
							dictionary2.Add(k, dictionary3);
						}
					}
					if (dictionary2 != null)
					{
						this.exceptionRows.Add(i, new ExceptionRow(dictionary2));
						this.progress.OnRows(1, 1);
					}
					else
					{
						this.progress.OnRows(1, 0);
					}
					num += this.columnCount;
					this.rowCount++;
				}
				return true;
			}

			// Token: 0x0600050A RID: 1290 RVA: 0x0000F50C File Offset: 0x0000D70C
			private IExceptionRow GetExceptionRow(IDictionary<string, string> exception)
			{
				Dictionary<int, IDictionary<string, string>> dictionary = new Dictionary<int, IDictionary<string, string>>(this.columnCount);
				for (int i = 0; i < this.columnCount; i++)
				{
					dictionary.Add(i, exception);
				}
				return new ExceptionRow(dictionary);
			}

			// Token: 0x0600050B RID: 1291 RVA: 0x0000F544 File Offset: 0x0000D744
			private void SyncColumnsPage()
			{
				if (this.columnsPage.RowCount != this.rowCount)
				{
					int num = 0;
					for (int i = 0; i < this.rowCount; i++)
					{
						for (int j = 0; j < this.columnCount; j++)
						{
							object obj = this.values[num++];
							if (obj == DBNull.Value)
							{
								this.columnsPage.GetColumn(j).AddNull();
							}
							else
							{
								this.columnsPage.GetColumn(j).AddValue(obj);
							}
						}
						this.columnsPage.AddRow();
					}
					foreach (KeyValuePair<int, IExceptionRow> keyValuePair in this.exceptionRows)
					{
						this.columnsPage.ExceptionRows.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}

			// Token: 0x04000420 RID: 1056
			private readonly int columnCount;

			// Token: 0x04000421 RID: 1057
			private readonly object[] values;

			// Token: 0x04000422 RID: 1058
			private readonly DataReaderPageReader.ExceptionPropertyGetter propertyGetter;

			// Token: 0x04000423 RID: 1059
			private readonly ReaderWriterProgress progress;

			// Token: 0x04000424 RID: 1060
			private readonly Dictionary<int, IExceptionRow> exceptionRows;

			// Token: 0x04000425 RID: 1061
			private readonly ColumnsPage columnsPage;

			// Token: 0x04000426 RID: 1062
			private int rowCount;
		}

		// Token: 0x020000F8 RID: 248
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000427 RID: 1063
			[global::System.Runtime.CompilerServices.Nullable(0)]
			public static DataReaderPageReader.ExceptionPropertyGetter <0>__DefaultExceptionPropertyGetter;
		}
	}
}
