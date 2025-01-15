using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;
using ParquetSharp;

namespace Microsoft.Mashup.Engine1.Library.Parquet
{
	// Token: 0x02001F63 RID: 8035
	internal class ParquetRowGroupsEnumerator : IEnumerator<RowGroupReader>, IDisposable, IEnumerator
	{
		// Token: 0x06010DEB RID: 69099 RVA: 0x003A1B40 File Offset: 0x0039FD40
		public ParquetRowGroupsEnumerator(StreamOwningParquetFileReader fileReader, int[] columnSelection)
		{
			this.fileReader = new StreamOwningParquetFileReader(fileReader);
			this.columnSelection = columnSelection;
			this.numRowGroups = this.ParquetFileReader.FileMetaData.NumRowGroups;
			this.columnReaders = new ColumnReader[columnSelection.Length];
			this.rowGroupIndex = -1;
		}

		// Token: 0x17002CB7 RID: 11447
		// (get) Token: 0x06010DEC RID: 69100 RVA: 0x003A1B91 File Offset: 0x0039FD91
		public RowGroupReader CurrentRowGroupReader
		{
			get
			{
				return this.rowGroupReader;
			}
		}

		// Token: 0x17002CB8 RID: 11448
		// (get) Token: 0x06010DED RID: 69101 RVA: 0x003A1B99 File Offset: 0x0039FD99
		public ColumnReader[] CurrentColumnReaders
		{
			get
			{
				return this.columnReaders;
			}
		}

		// Token: 0x17002CB9 RID: 11449
		// (get) Token: 0x06010DEE RID: 69102 RVA: 0x003A1BA1 File Offset: 0x0039FDA1
		RowGroupReader IEnumerator<RowGroupReader>.Current
		{
			get
			{
				return this.CurrentRowGroupReader;
			}
		}

		// Token: 0x17002CBA RID: 11450
		// (get) Token: 0x06010DEF RID: 69103 RVA: 0x003A1BA1 File Offset: 0x0039FDA1
		object IEnumerator.Current
		{
			get
			{
				return this.CurrentRowGroupReader;
			}
		}

		// Token: 0x17002CBB RID: 11451
		// (get) Token: 0x06010DF0 RID: 69104 RVA: 0x003A1BA9 File Offset: 0x0039FDA9
		private ParquetFileReader ParquetFileReader
		{
			get
			{
				return this.fileReader.ParquetFileReader;
			}
		}

		// Token: 0x06010DF1 RID: 69105 RVA: 0x003A1BB8 File Offset: 0x0039FDB8
		public bool MoveNext()
		{
			RowCount rowCount;
			return this.MoveNext(RowCount.Zero, out rowCount);
		}

		// Token: 0x06010DF2 RID: 69106 RVA: 0x003A1BD4 File Offset: 0x0039FDD4
		public bool MoveNext(RowCount skipCount, out RowCount remainder)
		{
			if (this.rowGroupIndex >= this.numRowGroups)
			{
				remainder = RowCount.Zero;
				return false;
			}
			if (this.rowGroupIndex < 0 && skipCount.Value >= this.ParquetFileReader.FileMetaData.NumRows)
			{
				this.rowGroupIndex = this.numRowGroups;
				remainder = RowCount.Zero;
				return false;
			}
			long num = skipCount.Value;
			long num2 = 0L;
			for (;;)
			{
				num -= num2;
				this.DisposeRowGroupReader();
				this.rowGroupIndex++;
				if (this.rowGroupIndex >= this.numRowGroups)
				{
					break;
				}
				this.rowGroupReader = this.ParquetFileReader.RowGroup(this.rowGroupIndex);
				num2 = this.rowGroupReader.MetaData.NumRows;
				if (num < num2)
				{
					goto Block_5;
				}
			}
			remainder = RowCount.Zero;
			return false;
			Block_5:
			for (int i = 0; i < this.columnReaders.Length; i++)
			{
				this.columnReaders[i] = this.rowGroupReader.Column(this.columnSelection[i]);
			}
			remainder = new RowCount(num);
			return true;
		}

		// Token: 0x06010DF3 RID: 69107 RVA: 0x003A1CDC File Offset: 0x0039FEDC
		private void DisposeRowGroupReader()
		{
			if (this.rowGroupReader != null)
			{
				foreach (ColumnReader columnReader in this.columnReaders)
				{
					if (columnReader != null)
					{
						columnReader.Dispose();
					}
				}
				this.rowGroupReader.Dispose();
				this.rowGroupReader = null;
			}
		}

		// Token: 0x06010DF4 RID: 69108 RVA: 0x003A1D25 File Offset: 0x0039FF25
		public void Dispose()
		{
			this.DisposeRowGroupReader();
			this.fileReader.Dispose();
		}

		// Token: 0x06010DF5 RID: 69109 RVA: 0x001D2D64 File Offset: 0x001D0F64
		public void Reset()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0400655D RID: 25949
		private readonly StreamOwningParquetFileReader fileReader;

		// Token: 0x0400655E RID: 25950
		private readonly int[] columnSelection;

		// Token: 0x0400655F RID: 25951
		private readonly ColumnReader[] columnReaders;

		// Token: 0x04006560 RID: 25952
		private readonly int numRowGroups;

		// Token: 0x04006561 RID: 25953
		private int rowGroupIndex;

		// Token: 0x04006562 RID: 25954
		private RowGroupReader rowGroupReader;
	}
}
