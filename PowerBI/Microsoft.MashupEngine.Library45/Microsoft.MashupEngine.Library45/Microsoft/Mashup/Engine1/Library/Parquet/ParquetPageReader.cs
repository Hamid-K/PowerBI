using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Parquet.Interop;
using Microsoft.Mashup.Engine1.Library.Parquet.Schema;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb;
using Microsoft.OleDb.Serialization;
using ParquetSharp;

namespace Microsoft.Mashup.Engine1.Library.Parquet
{
	// Token: 0x02001F38 RID: 7992
	internal class ParquetPageReader : IPageReader, IDisposable
	{
		// Token: 0x06010CC0 RID: 68800 RVA: 0x0039D910 File Offset: 0x0039BB10
		private ParquetPageReader(StreamOwningParquetFileReader fileReader, int[] columnSelection, ParquetPrimitiveTypeMap[] typeMaps, TableSchema schema, RowCount skipCount)
		{
			this.columnsInfo = new ParquetPageReader.ColumnInfo[columnSelection.Length];
			for (int i = 0; i < this.columnsInfo.Length; i++)
			{
				this.columnsInfo[i] = new ParquetPageReader.ColumnInfo(fileReader.ParquetFileReader.FileMetaData.Schema.Column(columnSelection[i]), typeMaps[i]);
			}
			this.rowGroupsEnumerator = new ParquetRowGroupsEnumerator(fileReader, columnSelection);
			this.schema = schema;
			this.maxPageRowCount = SchemaTableHelper.MaxRowCount(schema);
			this.progress = new ReaderWriterProgress();
			this.rowGroupsEnumerator.MoveNext(skipCount, out skipCount);
			if (skipCount.Value > 0L)
			{
				for (int j = 0; j < this.rowGroupsEnumerator.CurrentColumnReaders.Length; j++)
				{
					this.rowGroupsEnumerator.CurrentColumnReaders[j].Skip(skipCount.Value);
				}
			}
		}

		// Token: 0x06010CC1 RID: 68801 RVA: 0x0039D9E8 File Offset: 0x0039BBE8
		public static IPageReader New(StreamOwningParquetFileReader fileReader, GroupSchemaElement schema, int[] columnSelection, RowCount skipCount)
		{
			ParquetPrimitiveTypeMap[] array;
			TableSchema tableSchema = schema.CreateTableSchema(out array);
			return new ParquetPageReader(fileReader, columnSelection, array, tableSchema, skipCount);
		}

		// Token: 0x17002C74 RID: 11380
		// (get) Token: 0x06010CC2 RID: 68802 RVA: 0x0039DA08 File Offset: 0x0039BC08
		public TableSchema Schema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x17002C75 RID: 11381
		// (get) Token: 0x06010CC3 RID: 68803 RVA: 0x0039DA10 File Offset: 0x0039BC10
		public IProgress Progress
		{
			get
			{
				return this.progress;
			}
		}

		// Token: 0x17002C76 RID: 11382
		// (get) Token: 0x06010CC4 RID: 68804 RVA: 0x0039DA18 File Offset: 0x0039BC18
		public int MaxPageRowCount
		{
			get
			{
				return this.maxPageRowCount;
			}
		}

		// Token: 0x06010CC5 RID: 68805 RVA: 0x0039DA20 File Offset: 0x0039BC20
		public IPage CreatePage()
		{
			return new ParquetPageReader.ParquetPage(this.columnsInfo, this.schema, this.MaxPageRowCount);
		}

		// Token: 0x06010CC6 RID: 68806 RVA: 0x0039DA3C File Offset: 0x0039BC3C
		public void Read(IPage page)
		{
			ParquetPageReader.ParquetPage parquetPage = (ParquetPageReader.ParquetPage)page;
			parquetPage.Clear(this.pageException);
			if (this.rowGroupsEnumerator.CurrentRowGroupReader == null || this.pageException != null)
			{
				return;
			}
			while (parquetPage.Read(this.rowGroupsEnumerator.CurrentColumnReaders) == 0 && (this.pageException = parquetPage.PageException) == null && this.rowGroupsEnumerator.MoveNext())
			{
			}
		}

		// Token: 0x06010CC7 RID: 68807 RVA: 0x00002188 File Offset: 0x00000388
		public IPageReader NextResult()
		{
			return null;
		}

		// Token: 0x06010CC8 RID: 68808 RVA: 0x0039DAA3 File Offset: 0x0039BCA3
		public void Dispose()
		{
			this.rowGroupsEnumerator.Dispose();
		}

		// Token: 0x040064B9 RID: 25785
		private readonly int maxPageRowCount;

		// Token: 0x040064BA RID: 25786
		private readonly ParquetRowGroupsEnumerator rowGroupsEnumerator;

		// Token: 0x040064BB RID: 25787
		private readonly TableSchema schema;

		// Token: 0x040064BC RID: 25788
		private readonly ReaderWriterProgress progress;

		// Token: 0x040064BD RID: 25789
		private readonly ParquetPageReader.ColumnInfo[] columnsInfo;

		// Token: 0x040064BE RID: 25790
		private ISerializedException pageException;

		// Token: 0x02001F39 RID: 7993
		private class ParquetPage : IPage, IDisposable
		{
			// Token: 0x06010CC9 RID: 68809 RVA: 0x0039DAB0 File Offset: 0x0039BCB0
			public ParquetPage(ParquetPageReader.ColumnInfo[] columnsInfo, TableSchema schema, int maxRowCount)
			{
				this.columnsPage = new ColumnsPage(schema, maxRowCount);
				this.loaders = new ParquetPageReader.Loader[columnsInfo.Length];
				for (int i = 0; i < columnsInfo.Length; i++)
				{
					this.loaders[i] = ParquetPageReader.Loader.Create(columnsInfo[i], this.columnsPage.GetColumn(i), maxRowCount);
				}
			}

			// Token: 0x17002C77 RID: 11383
			// (get) Token: 0x06010CCA RID: 68810 RVA: 0x0039DB09 File Offset: 0x0039BD09
			public int ColumnCount
			{
				get
				{
					return this.columnsPage.ColumnCount;
				}
			}

			// Token: 0x17002C78 RID: 11384
			// (get) Token: 0x06010CCB RID: 68811 RVA: 0x0039DB16 File Offset: 0x0039BD16
			public int RowCount
			{
				get
				{
					return this.columnsPage.RowCount;
				}
			}

			// Token: 0x06010CCC RID: 68812 RVA: 0x0039DB23 File Offset: 0x0039BD23
			public IColumn GetColumn(int ordinal)
			{
				return this.columnsPage.GetColumn(ordinal);
			}

			// Token: 0x17002C79 RID: 11385
			// (get) Token: 0x06010CCD RID: 68813 RVA: 0x0039DB31 File Offset: 0x0039BD31
			public IDictionary<int, IExceptionRow> ExceptionRows
			{
				get
				{
					return this.columnsPage.ExceptionRows;
				}
			}

			// Token: 0x17002C7A RID: 11386
			// (get) Token: 0x06010CCE RID: 68814 RVA: 0x0039DB3E File Offset: 0x0039BD3E
			public ISerializedException PageException
			{
				get
				{
					return this.pageException;
				}
			}

			// Token: 0x06010CCF RID: 68815 RVA: 0x0039DB46 File Offset: 0x0039BD46
			public void Dispose()
			{
				this.columnsPage.Dispose();
			}

			// Token: 0x06010CD0 RID: 68816 RVA: 0x0039DB54 File Offset: 0x0039BD54
			public int Read(ColumnReader[] columnReaders)
			{
				int num2;
				try
				{
					int num = 0;
					for (int i = 0; i < this.loaders.Length; i++)
					{
						num = this.loaders[i].Read(columnReaders[i]);
					}
					this.columnsPage.AddRows(num);
					this.LoadExceptionRows();
					num2 = num;
				}
				catch (ParquetException ex)
				{
					ISerializedException ex2;
					PageExceptionSerializer.TryGetPropertiesFromException(ParquetExceptionHandler.GetParquetValueException(ex), out ex2);
					this.pageException = ex2;
					num2 = 0;
				}
				return num2;
			}

			// Token: 0x06010CD1 RID: 68817 RVA: 0x0039DBC8 File Offset: 0x0039BDC8
			public void Clear(ISerializedException pageException = null)
			{
				this.columnsPage.Clear(null);
				ParquetPageReader.Loader[] array = this.loaders;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Clear();
				}
				this.pageException = pageException;
			}

			// Token: 0x06010CD2 RID: 68818 RVA: 0x0039DC08 File Offset: 0x0039BE08
			private void LoadExceptionRows()
			{
				for (int i = 0; i < this.loaders.Length; i++)
				{
					foreach (KeyValuePair<int, ISerializedException> keyValuePair in this.loaders[i].RowExceptions)
					{
						IExceptionRow exceptionRow;
						if (!this.ExceptionRows.TryGetValue(keyValuePair.Key, out exceptionRow))
						{
							exceptionRow = new ExceptionRow(new Dictionary<int, ISerializedException>());
							this.ExceptionRows.Add(keyValuePair.Key, exceptionRow);
						}
						exceptionRow.Exceptions[i] = keyValuePair.Value;
					}
				}
			}

			// Token: 0x040064BF RID: 25791
			private readonly ColumnsPage columnsPage;

			// Token: 0x040064C0 RID: 25792
			private readonly ParquetPageReader.Loader[] loaders;

			// Token: 0x040064C1 RID: 25793
			private ISerializedException pageException;
		}

		// Token: 0x02001F3A RID: 7994
		private class ColumnInfo
		{
			// Token: 0x06010CD3 RID: 68819 RVA: 0x0039DCB4 File Offset: 0x0039BEB4
			public ColumnInfo(ColumnDescriptor columnDescriptor, ParquetTypeMap typeMap)
			{
				this.columnDescriptor = columnDescriptor;
				this.typeMap = typeMap;
			}

			// Token: 0x17002C7B RID: 11387
			// (get) Token: 0x06010CD4 RID: 68820 RVA: 0x0039DCCA File Offset: 0x0039BECA
			public ColumnDescriptor ColumnDescriptor
			{
				get
				{
					return this.columnDescriptor;
				}
			}

			// Token: 0x17002C7C RID: 11388
			// (get) Token: 0x06010CD5 RID: 68821 RVA: 0x0039DCD2 File Offset: 0x0039BED2
			public ParquetTypeMap TypeMap
			{
				get
				{
					return this.typeMap;
				}
			}

			// Token: 0x040064C2 RID: 25794
			private readonly ColumnDescriptor columnDescriptor;

			// Token: 0x040064C3 RID: 25795
			private readonly ParquetTypeMap typeMap;
		}

		// Token: 0x02001F3B RID: 7995
		private abstract class Loader
		{
			// Token: 0x06010CD6 RID: 68822 RVA: 0x0039DCDC File Offset: 0x0039BEDC
			public static ParquetPageReader.Loader Create(ParquetPageReader.ColumnInfo columnInfo, Microsoft.OleDb.Column column, int maxRowCount)
			{
				switch (columnInfo.ColumnDescriptor.PhysicalType)
				{
				case PhysicalType.Boolean:
					return ParquetPageReader.Loader<bool>.Create(columnInfo, column, maxRowCount);
				case PhysicalType.Int32:
					return ParquetPageReader.Loader<int>.Create(columnInfo, column, maxRowCount);
				case PhysicalType.Int64:
					return ParquetPageReader.Loader<long>.Create(columnInfo, column, maxRowCount);
				case PhysicalType.Int96:
					return ParquetPageReader.Loader<Int96>.Create(columnInfo, column, maxRowCount);
				case PhysicalType.Float:
					return ParquetPageReader.Loader<float>.Create(columnInfo, column, maxRowCount);
				case PhysicalType.Double:
					return ParquetPageReader.Loader<double>.Create(columnInfo, column, maxRowCount);
				case PhysicalType.ByteArray:
					return ParquetPageReader.Loader<ByteArray>.Create(columnInfo, column, maxRowCount);
				case PhysicalType.FixedLenByteArray:
					return ParquetPageReader.Loader<FixedLenByteArray>.Create(columnInfo, column, maxRowCount);
				default:
					throw new InvalidOperationException();
				}
			}

			// Token: 0x17002C7D RID: 11389
			// (get) Token: 0x06010CD7 RID: 68823
			public abstract IDictionary<int, ISerializedException> RowExceptions { get; }

			// Token: 0x06010CD8 RID: 68824
			public abstract int Read(ColumnReader reader);

			// Token: 0x06010CD9 RID: 68825
			public abstract void Clear();
		}

		// Token: 0x02001F3C RID: 7996
		private abstract class Loader<T> : ParquetPageReader.Loader
		{
			// Token: 0x06010CDB RID: 68827 RVA: 0x0039DD6A File Offset: 0x0039BF6A
			private Loader(IParquetColumnBuffer<T> buffer, Microsoft.OleDb.Column column, Action<T> loadColumn)
			{
				if (buffer.MaxRepetitionLevel > 0 || buffer.MaxDefinitionLevel > 1)
				{
					throw new NotSupportedException();
				}
				this.buffer = buffer;
				this.column = column;
				this.exceptionRows = new Dictionary<int, ISerializedException>();
				this.loadColumn = loadColumn;
			}

			// Token: 0x17002C7E RID: 11390
			// (get) Token: 0x06010CDC RID: 68828 RVA: 0x0039DDAA File Offset: 0x0039BFAA
			public override IDictionary<int, ISerializedException> RowExceptions
			{
				get
				{
					return this.exceptionRows;
				}
			}

			// Token: 0x06010CDD RID: 68829 RVA: 0x0039DDB4 File Offset: 0x0039BFB4
			public override int Read(ColumnReader reader)
			{
				int num = 0;
				while (this.column.RowCount < this.buffer.Values.Length && reader.HasNext)
				{
					num += this.buffer.Read(reader, this.buffer.Values.Length - this.column.RowCount);
					this.SyncColumn();
					this.buffer.Clear();
				}
				return num;
			}

			// Token: 0x06010CDE RID: 68830 RVA: 0x0039DE21 File Offset: 0x0039C021
			public override void Clear()
			{
				this.buffer.Clear();
				this.column.Clear();
				this.exceptionRows.Clear();
			}

			// Token: 0x06010CDF RID: 68831 RVA: 0x0039DE44 File Offset: 0x0039C044
			public new static ParquetPageReader.Loader<T> Create(ParquetPageReader.ColumnInfo columnInfo, Microsoft.OleDb.Column column, int maxRowCount)
			{
				short maxDefinitionLevel = columnInfo.ColumnDescriptor.MaxDefinitionLevel;
				short maxRepetitionLevel = columnInfo.ColumnDescriptor.MaxRepetitionLevel;
				IParquetColumnBuffer<T> parquetColumnBuffer = ParquetColumnBuffer.CreateDense<T>(maxDefinitionLevel, maxRepetitionLevel, maxRowCount);
				Action<T> columnLoader = ((ParquetPrimitiveTypeMap<T>)columnInfo.TypeMap).GetColumnLoader(column);
				if (parquetColumnBuffer.DefinitionLevels != null)
				{
					return new ParquetPageReader.Loader<T>.NullableLoader(parquetColumnBuffer, column, columnLoader);
				}
				return new ParquetPageReader.Loader<T>.NonNullableLoader(parquetColumnBuffer, column, columnLoader);
			}

			// Token: 0x06010CE0 RID: 68832
			protected abstract void SyncColumn();

			// Token: 0x06010CE1 RID: 68833 RVA: 0x0039DE9C File Offset: 0x0039C09C
			private void LoadColumn(T value)
			{
				try
				{
					this.loadColumn(value);
				}
				catch (Exception ex)
				{
					if (!Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
					{
						throw;
					}
					this.AddException(this.column.RowCount, ex);
					this.column.AddNull();
				}
			}

			// Token: 0x06010CE2 RID: 68834 RVA: 0x0039DEF4 File Offset: 0x0039C0F4
			private void AddException(int row, Exception exception)
			{
				ISerializedException ex;
				PageExceptionSerializer.TryGetPropertiesFromException(exception, out ex);
				this.exceptionRows.Add(row, ex);
			}

			// Token: 0x040064C4 RID: 25796
			protected readonly IParquetColumnBuffer<T> buffer;

			// Token: 0x040064C5 RID: 25797
			private readonly Microsoft.OleDb.Column column;

			// Token: 0x040064C6 RID: 25798
			private readonly IDictionary<int, ISerializedException> exceptionRows;

			// Token: 0x040064C7 RID: 25799
			private readonly Action<T> loadColumn;

			// Token: 0x02001F3D RID: 7997
			private class NonNullableLoader : ParquetPageReader.Loader<T>
			{
				// Token: 0x06010CE3 RID: 68835 RVA: 0x0039DF17 File Offset: 0x0039C117
				public NonNullableLoader(IParquetColumnBuffer<T> buffer, Microsoft.OleDb.Column column, Action<T> loadColumn)
					: base(buffer, column, loadColumn)
				{
				}

				// Token: 0x06010CE4 RID: 68836 RVA: 0x0039DF24 File Offset: 0x0039C124
				protected override void SyncColumn()
				{
					for (int i = 0; i < this.buffer.ValuesCount; i++)
					{
						base.LoadColumn(this.buffer.Values[i]);
					}
				}
			}

			// Token: 0x02001F3E RID: 7998
			private class NullableLoader : ParquetPageReader.Loader<T>
			{
				// Token: 0x06010CE5 RID: 68837 RVA: 0x0039DF17 File Offset: 0x0039C117
				public NullableLoader(IParquetColumnBuffer<T> buffer, Microsoft.OleDb.Column column, Action<T> loadColumn)
					: base(buffer, column, loadColumn)
				{
				}

				// Token: 0x06010CE6 RID: 68838 RVA: 0x0039DF60 File Offset: 0x0039C160
				protected override void SyncColumn()
				{
					int num = 0;
					for (int i = 0; i < this.buffer.LevelsCount; i++)
					{
						if (this.buffer.DefinitionLevels[i] == 0)
						{
							this.column.AddNull();
						}
						else
						{
							base.LoadColumn(this.buffer.Values[num]);
							num++;
						}
					}
				}
			}
		}
	}
}
