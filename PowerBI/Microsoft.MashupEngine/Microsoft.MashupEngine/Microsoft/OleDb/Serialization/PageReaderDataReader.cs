using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Serialization;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02001FC3 RID: 8131
	public class PageReaderDataReader : IDataReaderWithTableSchema, IDataReader, IDisposable, IDataRecord
	{
		// Token: 0x0600C683 RID: 50819 RVA: 0x00279009 File Offset: 0x00277209
		public PageReaderDataReader(IPageReader reader)
			: this(reader, null, null)
		{
		}

		// Token: 0x0600C684 RID: 50820 RVA: 0x00279014 File Offset: 0x00277214
		public PageReaderDataReader(IPageReader reader, Func<ISerializedException, Exception> cellErrorHandler)
			: this(reader, cellErrorHandler, null)
		{
		}

		// Token: 0x0600C685 RID: 50821 RVA: 0x00279020 File Offset: 0x00277220
		public PageReaderDataReader(IPageReader reader, Func<ISerializedException, Exception> cellErrorHandler, Func<ISerializedException, Exception> pageExceptionHandler)
		{
			this.reader = new SinglePageReader(reader);
			this.cellErrorHandler = cellErrorHandler;
			this.pageExceptionHandler = pageExceptionHandler;
			this.page = this.reader.Page;
			this.readerState = PageReaderDataReader.ReaderState.InRows;
			this.fieldCount = this.reader.Schema.ColumnCount;
		}

		// Token: 0x0600C686 RID: 50822 RVA: 0x0027907B File Offset: 0x0027727B
		public void Dispose()
		{
			this.reader.Dispose();
		}

		// Token: 0x0600C687 RID: 50823 RVA: 0x00279088 File Offset: 0x00277288
		public void Close()
		{
			this.Dispose();
		}

		// Token: 0x17003022 RID: 12322
		// (get) Token: 0x0600C688 RID: 50824 RVA: 0x00002105 File Offset: 0x00000305
		public int Depth
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600C689 RID: 50825 RVA: 0x00279090 File Offset: 0x00277290
		public DataTable GetSchemaTable()
		{
			return this.reader.Schema.ToDataTable();
		}

		// Token: 0x17003023 RID: 12323
		// (get) Token: 0x0600C68A RID: 50826 RVA: 0x002790A2 File Offset: 0x002772A2
		public TableSchema Schema
		{
			get
			{
				return this.reader.Schema;
			}
		}

		// Token: 0x17003024 RID: 12324
		// (get) Token: 0x0600C68B RID: 50827 RVA: 0x002790AF File Offset: 0x002772AF
		public bool IsClosed
		{
			get
			{
				return this.reader == null;
			}
		}

		// Token: 0x0600C68C RID: 50828 RVA: 0x002790BC File Offset: 0x002772BC
		public bool NextResult()
		{
			if (this.readerState == PageReaderDataReader.ReaderState.AfterLastResult)
			{
				return false;
			}
			while (this.readerState == PageReaderDataReader.ReaderState.InRows)
			{
				this.Read();
			}
			IPageReader pageReader = this.reader.NextResult();
			if (pageReader != null)
			{
				this.reader.Dispose();
				this.reader = new SinglePageReader(pageReader);
				this.page = this.reader.Page;
				this.readerState = PageReaderDataReader.ReaderState.InRows;
				this.fieldCount = this.reader.Schema.ColumnCount;
				return true;
			}
			this.readerState = PageReaderDataReader.ReaderState.AfterLastResult;
			return false;
		}

		// Token: 0x0600C68D RID: 50829 RVA: 0x00279144 File Offset: 0x00277344
		public bool Read()
		{
			while (this.readerState == PageReaderDataReader.ReaderState.InRows)
			{
				this.rowIndex++;
				if (this.rowIndex < this.page.RowCount)
				{
					if (this.cellErrorHandler != null)
					{
						IExceptionRow exceptionRow;
						if (this.page.ExceptionRows.TryGetValue(this.rowIndex, out exceptionRow))
						{
							this.exceptions = exceptionRow.Exceptions;
						}
						else
						{
							this.exceptions = null;
						}
					}
					return true;
				}
				if (this.page is PageExceptionPage)
				{
					this.readerState = PageReaderDataReader.ReaderState.AfterRows;
					return false;
				}
				if (this.page.PageException != null)
				{
					if (this.pageExceptionHandler != null)
					{
						throw this.pageExceptionHandler(this.page.PageException);
					}
					this.page = new PageExceptionPage(this.FieldCount, this.page.PageException);
				}
				else
				{
					this.reader.Read();
					this.page = this.reader.Page;
					if (this.page.RowCount == 0 && this.page.PageException == null)
					{
						this.readerState = PageReaderDataReader.ReaderState.AfterRows;
						return false;
					}
				}
				this.rowIndex = -1;
			}
			return false;
		}

		// Token: 0x17003025 RID: 12325
		// (get) Token: 0x0600C68E RID: 50830 RVA: 0x0017811C File Offset: 0x0017631C
		public int RecordsAffected
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17003026 RID: 12326
		// (get) Token: 0x0600C68F RID: 50831 RVA: 0x0027925F File Offset: 0x0027745F
		public int FieldCount
		{
			get
			{
				return this.fieldCount;
			}
		}

		// Token: 0x0600C690 RID: 50832 RVA: 0x00279267 File Offset: 0x00277467
		public bool GetBoolean(int ordinal)
		{
			return this.GetColumn(ordinal).GetBoolean(this.rowIndex);
		}

		// Token: 0x0600C691 RID: 50833 RVA: 0x0027927B File Offset: 0x0027747B
		public byte GetByte(int ordinal)
		{
			return this.GetColumn(ordinal).GetByte(this.rowIndex);
		}

		// Token: 0x0600C692 RID: 50834 RVA: 0x000033E7 File Offset: 0x000015E7
		public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600C693 RID: 50835 RVA: 0x0027928F File Offset: 0x0027748F
		public char GetChar(int ordinal)
		{
			return (char)this.GetValue(ordinal);
		}

		// Token: 0x0600C694 RID: 50836 RVA: 0x000033E7 File Offset: 0x000015E7
		public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600C695 RID: 50837 RVA: 0x000033E7 File Offset: 0x000015E7
		public IDataReader GetData(int i)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600C696 RID: 50838 RVA: 0x000033E7 File Offset: 0x000015E7
		public string GetDataTypeName(int ordinal)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600C697 RID: 50839 RVA: 0x0027929D File Offset: 0x0027749D
		public DateTime GetDateTime(int ordinal)
		{
			return this.GetColumn(ordinal).GetDateTime(this.rowIndex);
		}

		// Token: 0x0600C698 RID: 50840 RVA: 0x002792B1 File Offset: 0x002774B1
		public decimal GetDecimal(int ordinal)
		{
			return this.GetColumn(ordinal).GetDecimal(this.rowIndex);
		}

		// Token: 0x0600C699 RID: 50841 RVA: 0x002792C5 File Offset: 0x002774C5
		public double GetDouble(int ordinal)
		{
			return this.GetColumn(ordinal).GetDouble(this.rowIndex);
		}

		// Token: 0x0600C69A RID: 50842 RVA: 0x002792D9 File Offset: 0x002774D9
		public Type GetFieldType(int ordinal)
		{
			return this.reader.Schema.GetColumn(ordinal).DataType;
		}

		// Token: 0x0600C69B RID: 50843 RVA: 0x002792F1 File Offset: 0x002774F1
		public float GetFloat(int ordinal)
		{
			return this.GetColumn(ordinal).GetFloat(this.rowIndex);
		}

		// Token: 0x0600C69C RID: 50844 RVA: 0x00279305 File Offset: 0x00277505
		public Guid GetGuid(int ordinal)
		{
			return this.GetColumn(ordinal).GetGuid(this.rowIndex);
		}

		// Token: 0x0600C69D RID: 50845 RVA: 0x00279319 File Offset: 0x00277519
		public short GetInt16(int ordinal)
		{
			return this.GetColumn(ordinal).GetInt16(this.rowIndex);
		}

		// Token: 0x0600C69E RID: 50846 RVA: 0x0027932D File Offset: 0x0027752D
		public int GetInt32(int ordinal)
		{
			return this.GetColumn(ordinal).GetInt32(this.rowIndex);
		}

		// Token: 0x0600C69F RID: 50847 RVA: 0x00279341 File Offset: 0x00277541
		public long GetInt64(int ordinal)
		{
			return this.GetColumn(ordinal).GetInt64(this.rowIndex);
		}

		// Token: 0x0600C6A0 RID: 50848 RVA: 0x00279355 File Offset: 0x00277555
		public string GetName(int ordinal)
		{
			return this.reader.Schema.GetColumn(ordinal).Name;
		}

		// Token: 0x0600C6A1 RID: 50849 RVA: 0x00279370 File Offset: 0x00277570
		public int GetOrdinal(string name)
		{
			int num;
			if (!this.reader.Schema.TryGetColumn(name, out num))
			{
				throw new IndexOutOfRangeException();
			}
			return num;
		}

		// Token: 0x0600C6A2 RID: 50850 RVA: 0x00279399 File Offset: 0x00277599
		public string GetString(int ordinal)
		{
			return this.GetColumn(ordinal).GetString(this.rowIndex);
		}

		// Token: 0x0600C6A3 RID: 50851 RVA: 0x002793AD File Offset: 0x002775AD
		public object GetValue(int ordinal)
		{
			return this.GetColumn(ordinal).GetObject(this.rowIndex);
		}

		// Token: 0x0600C6A4 RID: 50852 RVA: 0x002793C4 File Offset: 0x002775C4
		public int GetValues(object[] values)
		{
			int num = Math.Min(values.Length, this.page.ColumnCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this.GetValue(i);
			}
			return num;
		}

		// Token: 0x0600C6A5 RID: 50853 RVA: 0x002793FC File Offset: 0x002775FC
		public bool IsDBNull(int ordinal)
		{
			return this.GetColumn(ordinal).IsNull(this.rowIndex);
		}

		// Token: 0x17003027 RID: 12327
		public object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x17003028 RID: 12328
		public object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		// Token: 0x0600C6A8 RID: 50856 RVA: 0x000033E7 File Offset: 0x000015E7
		public IEnumerator GetEnumerator()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17003029 RID: 12329
		// (get) Token: 0x0600C6A9 RID: 50857 RVA: 0x000033E7 File Offset: 0x000015E7
		public bool HasRows
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600C6AA RID: 50858 RVA: 0x00279428 File Offset: 0x00277628
		private IColumn GetColumn(int ordinal)
		{
			ISerializedException ex;
			if (this.exceptions != null && this.exceptions.TryGetValue(ordinal, out ex))
			{
				throw this.cellErrorHandler(ex);
			}
			return this.page.GetColumn(ordinal);
		}

		// Token: 0x04006568 RID: 25960
		private readonly Func<ISerializedException, Exception> cellErrorHandler;

		// Token: 0x04006569 RID: 25961
		private readonly Func<ISerializedException, Exception> pageExceptionHandler;

		// Token: 0x0400656A RID: 25962
		private SinglePageReader reader;

		// Token: 0x0400656B RID: 25963
		private int rowIndex;

		// Token: 0x0400656C RID: 25964
		private IPage page;

		// Token: 0x0400656D RID: 25965
		private IDictionary<int, ISerializedException> exceptions;

		// Token: 0x0400656E RID: 25966
		private PageReaderDataReader.ReaderState readerState;

		// Token: 0x0400656F RID: 25967
		private int fieldCount;

		// Token: 0x02001FC4 RID: 8132
		private enum ReaderState
		{
			// Token: 0x04006571 RID: 25969
			InRows,
			// Token: 0x04006572 RID: 25970
			AfterRows,
			// Token: 0x04006573 RID: 25971
			AfterLastResult
		}
	}
}
