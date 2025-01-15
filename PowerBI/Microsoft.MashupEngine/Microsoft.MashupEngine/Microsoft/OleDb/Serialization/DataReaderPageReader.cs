using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Serialization;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02001FB0 RID: 8112
	public class DataReaderPageReader : IPageReader, IDisposable
	{
		// Token: 0x0600C5E1 RID: 50657 RVA: 0x00276E2C File Offset: 0x0027502C
		public DataReaderPageReader(IDataReader reader)
			: this(reader, null)
		{
		}

		// Token: 0x0600C5E2 RID: 50658 RVA: 0x00276E38 File Offset: 0x00275038
		public DataReaderPageReader(IDataReader reader, DataReaderPageReader.ExceptionPropertyGetter propertyGetter)
		{
			if (propertyGetter == null)
			{
				propertyGetter = new DataReaderPageReader.ExceptionPropertyGetter(DataReaderPageReader.DefaultExceptionPropertyGetter);
			}
			this.reader = reader;
			this.schema = TableSchema.FromDataReader(reader);
			this.maxRowCount = SchemaTableHelper.MaxRowCount(this.schema);
			this.propertyGetter = propertyGetter;
			this.progress = new ReaderWriterProgress();
			this.first = true;
		}

		// Token: 0x1700300B RID: 12299
		// (get) Token: 0x0600C5E3 RID: 50659 RVA: 0x00276E99 File Offset: 0x00275099
		public TableSchema Schema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x1700300C RID: 12300
		// (get) Token: 0x0600C5E4 RID: 50660 RVA: 0x00276EA1 File Offset: 0x002750A1
		public IProgress Progress
		{
			get
			{
				return this.progress;
			}
		}

		// Token: 0x1700300D RID: 12301
		// (get) Token: 0x0600C5E5 RID: 50661 RVA: 0x00276EA9 File Offset: 0x002750A9
		public int MaxPageRowCount
		{
			get
			{
				return this.maxRowCount;
			}
		}

		// Token: 0x0600C5E6 RID: 50662 RVA: 0x00276EB1 File Offset: 0x002750B1
		public void Read(IPage page)
		{
			this.Read((DataReaderPageReader.ObjectPage)page);
		}

		// Token: 0x0600C5E7 RID: 50663 RVA: 0x00276EC0 File Offset: 0x002750C0
		private void Read(DataReaderPageReader.ObjectPage page)
		{
			page.Clear(this.pageException);
			if (!this.eof)
			{
				int num = (this.first ? Math.Min(100, this.maxRowCount) : this.maxRowCount);
				if (!page.Read(this.reader, num))
				{
					this.eof = true;
				}
				this.first = false;
				this.pageException = page.PageException;
			}
		}

		// Token: 0x0600C5E8 RID: 50664 RVA: 0x000020FA File Offset: 0x000002FA
		public IPageReader NextResult()
		{
			return null;
		}

		// Token: 0x0600C5E9 RID: 50665 RVA: 0x00276F28 File Offset: 0x00275128
		public void Dispose()
		{
			this.reader.Dispose();
		}

		// Token: 0x0600C5EA RID: 50666 RVA: 0x00276F35 File Offset: 0x00275135
		public IPage CreatePage()
		{
			return new DataReaderPageReader.ObjectPage(this.schema, this.maxRowCount, this.propertyGetter, this.progress);
		}

		// Token: 0x0600C5EB RID: 50667 RVA: 0x000E6755 File Offset: 0x000E4955
		private static bool DefaultExceptionPropertyGetter(Exception e, out ISerializedException properties)
		{
			properties = null;
			return false;
		}

		// Token: 0x0400651E RID: 25886
		private readonly IDataReader reader;

		// Token: 0x0400651F RID: 25887
		private readonly TableSchema schema;

		// Token: 0x04006520 RID: 25888
		private readonly int maxRowCount;

		// Token: 0x04006521 RID: 25889
		private readonly DataReaderPageReader.ExceptionPropertyGetter propertyGetter;

		// Token: 0x04006522 RID: 25890
		private readonly ReaderWriterProgress progress;

		// Token: 0x04006523 RID: 25891
		private bool eof;

		// Token: 0x04006524 RID: 25892
		private bool first;

		// Token: 0x04006525 RID: 25893
		private ISerializedException pageException;

		// Token: 0x02001FB1 RID: 8113
		// (Invoke) Token: 0x0600C5ED RID: 50669
		public delegate bool ExceptionPropertyGetter(Exception e, out ISerializedException properties);

		// Token: 0x02001FB2 RID: 8114
		private class ObjectPage : IConcurrentPage, IPage, IDisposable
		{
			// Token: 0x0600C5F0 RID: 50672 RVA: 0x00276F54 File Offset: 0x00275154
			public ObjectPage(TableSchema schema, int maxRowCount, DataReaderPageReader.ExceptionPropertyGetter propertyGetter, ReaderWriterProgress progress)
			{
				this.columnCount = schema.ColumnCount;
				this.values = new object[this.columnCount * maxRowCount];
				this.propertyGetter = propertyGetter;
				this.progress = progress;
				this.exceptionRows = new Dictionary<int, IExceptionRow>();
				this.columnsPage = new ColumnsPage(schema);
				this.rowCount = 0;
				this.pageException = null;
			}

			// Token: 0x1700300E RID: 12302
			// (get) Token: 0x0600C5F1 RID: 50673 RVA: 0x00276FBA File Offset: 0x002751BA
			public int ColumnCount
			{
				get
				{
					return this.columnCount;
				}
			}

			// Token: 0x1700300F RID: 12303
			// (get) Token: 0x0600C5F2 RID: 50674 RVA: 0x00276FC2 File Offset: 0x002751C2
			public int RowCount
			{
				get
				{
					return this.rowCount;
				}
			}

			// Token: 0x17003010 RID: 12304
			// (get) Token: 0x0600C5F3 RID: 50675 RVA: 0x00276FCA File Offset: 0x002751CA
			public IDictionary<int, IExceptionRow> ExceptionRows
			{
				get
				{
					return this.exceptionRows;
				}
			}

			// Token: 0x17003011 RID: 12305
			// (get) Token: 0x0600C5F4 RID: 50676 RVA: 0x00276FD2 File Offset: 0x002751D2
			public ISerializedException PageException
			{
				get
				{
					return this.pageException;
				}
			}

			// Token: 0x17003012 RID: 12306
			// (get) Token: 0x0600C5F5 RID: 50677 RVA: 0x00276FDA File Offset: 0x002751DA
			public bool IsBufferedForRead
			{
				get
				{
					return this.columnsPage.RowCount == this.rowCount;
				}
			}

			// Token: 0x0600C5F6 RID: 50678 RVA: 0x00276FEF File Offset: 0x002751EF
			public IColumn GetColumn(int ordinal)
			{
				this.SyncColumnsPage();
				return this.columnsPage.GetColumn(ordinal);
			}

			// Token: 0x0600C5F7 RID: 50679 RVA: 0x00277003 File Offset: 0x00275203
			public void BufferForRead()
			{
				this.SyncColumnsPage();
			}

			// Token: 0x0600C5F8 RID: 50680 RVA: 0x0027700B File Offset: 0x0027520B
			public void Clear(ISerializedException pageException = null)
			{
				this.exceptionRows.Clear();
				Array.Clear(this.values, 0, this.rowCount * this.columnCount);
				this.rowCount = 0;
				this.columnsPage.Clear(null);
				this.pageException = pageException;
			}

			// Token: 0x0600C5F9 RID: 50681 RVA: 0x0027704B File Offset: 0x0027524B
			public void Dispose()
			{
				this.columnsPage.Dispose();
			}

			// Token: 0x0600C5FA RID: 50682 RVA: 0x00277058 File Offset: 0x00275258
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
						ISerializedException ex2;
						if (this.propertyGetter(ex, out ex2))
						{
							this.pageException = ex2;
							this.progress.OnRows(1, 1);
							return false;
						}
						throw;
					}
					Dictionary<int, ISerializedException> dictionary = null;
					for (int k = 0; k < this.columnCount; k++)
					{
						try
						{
							this.values[num + k] = reader.GetValue(k);
						}
						catch (Exception ex3)
						{
							this.values[num + k] = DBNull.Value;
							if (dictionary == null)
							{
								dictionary = new Dictionary<int, ISerializedException>();
							}
							ISerializedException ex4;
							if (!this.propertyGetter(ex3, out ex4))
							{
								throw;
							}
							dictionary.Add(k, ex4);
						}
					}
					if (dictionary != null)
					{
						this.exceptionRows.Add(i, new ExceptionRow(dictionary));
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

			// Token: 0x0600C5FB RID: 50683 RVA: 0x002771B4 File Offset: 0x002753B4
			private IExceptionRow GetExceptionRow(ISerializedException exception)
			{
				Dictionary<int, ISerializedException> dictionary = new Dictionary<int, ISerializedException>(this.columnCount);
				for (int i = 0; i < this.columnCount; i++)
				{
					dictionary.Add(i, exception);
				}
				return new ExceptionRow(dictionary);
			}

			// Token: 0x0600C5FC RID: 50684 RVA: 0x002771EC File Offset: 0x002753EC
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

			// Token: 0x04006526 RID: 25894
			private readonly int columnCount;

			// Token: 0x04006527 RID: 25895
			private readonly object[] values;

			// Token: 0x04006528 RID: 25896
			private readonly DataReaderPageReader.ExceptionPropertyGetter propertyGetter;

			// Token: 0x04006529 RID: 25897
			private readonly ReaderWriterProgress progress;

			// Token: 0x0400652A RID: 25898
			private readonly Dictionary<int, IExceptionRow> exceptionRows;

			// Token: 0x0400652B RID: 25899
			private readonly ColumnsPage columnsPage;

			// Token: 0x0400652C RID: 25900
			private int rowCount;

			// Token: 0x0400652D RID: 25901
			private ISerializedException pageException;
		}
	}
}
