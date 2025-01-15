using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ParquetSharp.IO;

namespace ParquetSharp.RowOriented
{
	// Token: 0x0200009F RID: 159
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ParquetRowReader<[Nullable(2)] TTuple> : IDisposable
	{
		// Token: 0x060004D3 RID: 1235 RVA: 0x00010DB4 File Offset: 0x0000EFB4
		internal ParquetRowReader(string path, [Nullable(new byte[] { 1, 0 })] ParquetRowReader<TTuple>.ReadAction readAction, MappedField[] fields)
			: this(new ParquetFileReader(path), readAction, fields)
		{
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00010DC4 File Offset: 0x0000EFC4
		internal ParquetRowReader(string path, ReaderProperties readerProperties, [Nullable(new byte[] { 1, 0 })] ParquetRowReader<TTuple>.ReadAction readAction, MappedField[] fields)
			: this(new ParquetFileReader(path, readerProperties), readAction, fields)
		{
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00010DD8 File Offset: 0x0000EFD8
		internal ParquetRowReader(string path, bool memoryMap, [Nullable(new byte[] { 1, 0 })] ParquetRowReader<TTuple>.ReadAction readAction, MappedField[] fields)
			: this(new ParquetFileReader(path, memoryMap), readAction, fields)
		{
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00010DEC File Offset: 0x0000EFEC
		internal ParquetRowReader(string path, bool memoryMap, ReaderProperties readerProperties, [Nullable(new byte[] { 1, 0 })] ParquetRowReader<TTuple>.ReadAction readAction, MappedField[] fields)
			: this(new ParquetFileReader(path, memoryMap, readerProperties), readAction, fields)
		{
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00010E00 File Offset: 0x0000F000
		internal ParquetRowReader(RandomAccessFile randomAccessFile, [Nullable(new byte[] { 1, 0 })] ParquetRowReader<TTuple>.ReadAction readAction, MappedField[] fields)
			: this(new ParquetFileReader(randomAccessFile), readAction, fields)
		{
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00010E10 File Offset: 0x0000F010
		internal ParquetRowReader(RandomAccessFile randomAccessFile, ReaderProperties readerProperties, [Nullable(new byte[] { 1, 0 })] ParquetRowReader<TTuple>.ReadAction readAction, MappedField[] fields)
			: this(new ParquetFileReader(randomAccessFile, readerProperties), readAction, fields)
		{
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00010E24 File Offset: 0x0000F024
		internal ParquetRowReader(ParquetFileReader parquetFileReader, [Nullable(new byte[] { 1, 0 })] ParquetRowReader<TTuple>.ReadAction readAction, MappedField[] fields)
		{
			this._parquetFileReader = parquetFileReader;
			this._readAction = readAction;
			this._columnMapping = (ParquetRowReader<TTuple>.HasExplicitColumnMapping(fields) ? new ParquetRowReader<TTuple>.ExplicitColumnMapping(this, fields) : null);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00010E58 File Offset: 0x0000F058
		public void Dispose()
		{
			this._parquetFileReader.Dispose();
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00010E68 File Offset: 0x0000F068
		public FileMetaData FileMetaData
		{
			get
			{
				return this._parquetFileReader.FileMetaData;
			}
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00010E78 File Offset: 0x0000F078
		public TTuple[] ReadRows(int rowGroup)
		{
			TTuple[] array2;
			using (this._rowGroupReader = this._parquetFileReader.RowGroup(rowGroup))
			{
				TTuple[] array = new TTuple[this._rowGroupReader.MetaData.NumRows];
				this._readAction(this, array, array.Length);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00010EE8 File Offset: 0x0000F0E8
		internal void ReadColumn<[Nullable(2)] TValue>(int column, TValue[] values, int length)
		{
			if (this._rowGroupReader == null)
			{
				throw new InvalidOperationException("row group reader has not been initialized");
			}
			RowGroupReader rowGroupReader = this._rowGroupReader;
			ParquetRowReader<TTuple>.ExplicitColumnMapping columnMapping = this._columnMapping;
			using (LogicalColumnReader<TValue> logicalColumnReader = rowGroupReader.Column((columnMapping != null) ? columnMapping.Get(column) : column).LogicalReader<TValue>(4096))
			{
				int num = logicalColumnReader.ReadBatch(values, 0, length);
				if (num != length)
				{
					throw new Exception(string.Format("length = {0} but only read {1} values", length, num));
				}
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00010F8C File Offset: 0x0000F18C
		private static bool HasExplicitColumnMapping(MappedField[] fields)
		{
			bool flag = Array.TrueForAll<MappedField>(fields, (MappedField f) => f.MappedColumn == null);
			bool flag2 = Array.TrueForAll<MappedField>(fields, (MappedField f) => f.MappedColumn != null);
			if (!flag2 && !flag)
			{
				throw new ArgumentException("when using MapToColumnAttribute, all fields and properties must have the mapping specified.");
			}
			return flag2;
		}

		// Token: 0x04000165 RID: 357
		private readonly ParquetFileReader _parquetFileReader;

		// Token: 0x04000166 RID: 358
		[Nullable(new byte[] { 1, 0 })]
		private readonly ParquetRowReader<TTuple>.ReadAction _readAction;

		// Token: 0x04000167 RID: 359
		[Nullable(new byte[] { 2, 0 })]
		private readonly ParquetRowReader<TTuple>.ExplicitColumnMapping _columnMapping;

		// Token: 0x04000168 RID: 360
		[Nullable(2)]
		private RowGroupReader _rowGroupReader;

		// Token: 0x0200012F RID: 303
		// (Invoke) Token: 0x060009C9 RID: 2505
		[NullableContext(0)]
		internal delegate void ReadAction(ParquetRowReader<TTuple> parquetRowReader, TTuple[] rows, int length);

		// Token: 0x02000130 RID: 304
		[Nullable(0)]
		private sealed class ExplicitColumnMapping
		{
			// Token: 0x060009CC RID: 2508 RVA: 0x0002C1D4 File Offset: 0x0002A3D4
			public ExplicitColumnMapping(ParquetRowReader<TTuple> parquetRowReader, MappedField[] fields)
			{
				if (!(from x in fields
					group x by x.MappedColumn).All((IGrouping<string, MappedField> g) => g.Count<MappedField>() == 1))
				{
					throw new ArgumentException("when using MapToColumnAttribute, each field must map to a unique column");
				}
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				SchemaDescriptor schema = parquetRowReader.FileMetaData.Schema;
				for (int i = 0; i < schema.NumColumns; i++)
				{
					dictionary[schema.Column(i).Name] = i;
				}
				for (int j = 0; j < fields.Length; j++)
				{
					string mappedColumn = fields[j].MappedColumn;
					if (mappedColumn == null)
					{
						throw new InvalidOperationException("mapped column name is null");
					}
					string text = mappedColumn;
					int num;
					if (!dictionary.TryGetValue(text, out num))
					{
						throw new ArgumentException(string.Format("{0} maps field '{1}' to parquet column ", typeof(TTuple), fields[j].Name) + "'" + fields[j].MappedColumn + "' but the target column does not exist in the input parquet file.");
					}
					this._fileColumnIndex[j] = dictionary[text];
				}
			}

			// Token: 0x060009CD RID: 2509 RVA: 0x0002C328 File Offset: 0x0002A528
			public int Get(int columnIndex)
			{
				return this._fileColumnIndex[columnIndex];
			}

			// Token: 0x0400031A RID: 794
			private readonly Dictionary<int, int> _fileColumnIndex = new Dictionary<int, int>();
		}
	}
}
