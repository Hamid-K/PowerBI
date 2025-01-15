using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ParquetSharp.IO;

namespace ParquetSharp.RowOriented
{
	// Token: 0x020000A0 RID: 160
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ParquetRowWriter<[Nullable(2)] TTuple> : IDisposable
	{
		// Token: 0x060004DF RID: 1247 RVA: 0x00011008 File Offset: 0x0000F208
		internal ParquetRowWriter(string path, Column[] columns, Compression compression, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> keyValueMetadata, [Nullable(new byte[] { 1, 0 })] ParquetRowWriter<TTuple>.WriteAction writeAction)
			: this(new ParquetFileWriter(path, columns, compression, keyValueMetadata), writeAction)
		{
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001101C File Offset: 0x0000F21C
		internal ParquetRowWriter(string path, Column[] columns, WriterProperties writerProperties, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> keyValueMetadata, [Nullable(new byte[] { 1, 0 })] ParquetRowWriter<TTuple>.WriteAction writeAction)
			: this(new ParquetFileWriter(path, columns, writerProperties, keyValueMetadata), writeAction)
		{
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00011030 File Offset: 0x0000F230
		internal ParquetRowWriter(OutputStream outputStream, Column[] columns, Compression compression, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> keyValueMetadata, [Nullable(new byte[] { 1, 0 })] ParquetRowWriter<TTuple>.WriteAction writeAction)
			: this(new ParquetFileWriter(outputStream, columns, compression, keyValueMetadata), writeAction)
		{
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00011044 File Offset: 0x0000F244
		internal ParquetRowWriter(OutputStream outputStream, Column[] columns, WriterProperties writerProperties, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> keyValueMetadata, [Nullable(new byte[] { 1, 0 })] ParquetRowWriter<TTuple>.WriteAction writeAction)
			: this(new ParquetFileWriter(outputStream, columns, writerProperties, keyValueMetadata), writeAction)
		{
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00011058 File Offset: 0x0000F258
		private ParquetRowWriter(ParquetFileWriter parquetFileWriter, [Nullable(new byte[] { 1, 0 })] ParquetRowWriter<TTuple>.WriteAction writeAction)
		{
			this._parquetFileWriter = parquetFileWriter;
			this._rowGroupWriter = this._parquetFileWriter.AppendRowGroup();
			this._writeAction = writeAction;
			this._rows = new TTuple[1024];
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00011090 File Offset: 0x0000F290
		public void Dispose()
		{
			this.FlushAndDisposeRowGroup();
			this._parquetFileWriter.Dispose();
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000110A4 File Offset: 0x0000F2A4
		public void Close()
		{
			this.FlushAndDisposeRowGroup();
			this._parquetFileWriter.Close();
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x000110B8 File Offset: 0x0000F2B8
		public WriterProperties WriterProperties
		{
			get
			{
				return this._parquetFileWriter.WriterProperties;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x000110C8 File Offset: 0x0000F2C8
		public SchemaDescriptor Schema
		{
			get
			{
				return this._parquetFileWriter.Schema;
			}
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x000110D8 File Offset: 0x0000F2D8
		public ColumnDescriptor ColumnDescriptor(int i)
		{
			return this._parquetFileWriter.ColumnDescriptor(i);
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x000110E8 File Offset: 0x0000F2E8
		[Nullable(2)]
		public FileMetaData FileMetaData
		{
			[NullableContext(2)]
			get
			{
				return this._parquetFileWriter.FileMetaData;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x000110F8 File Offset: 0x0000F2F8
		public IReadOnlyDictionary<string, string> KeyValueMetadata
		{
			get
			{
				return this._parquetFileWriter.KeyValueMetadata;
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00011108 File Offset: 0x0000F308
		public void StartNewRowGroup()
		{
			if (this._rowGroupWriter == null)
			{
				throw new InvalidOperationException("writer has been closed or disposed");
			}
			this.FlushAndDisposeRowGroup();
			this._rowGroupWriter = this._parquetFileWriter.AppendRowGroup();
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00011138 File Offset: 0x0000F338
		public void WriteRows(IEnumerable<TTuple> rows)
		{
			foreach (TTuple ttuple in rows)
			{
				this.WriteRow(ttuple);
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001118C File Offset: 0x0000F38C
		public void WriteRowSpan([Nullable(new byte[] { 0, 1 })] ReadOnlySpan<TTuple> rows)
		{
			if (this._pos + rows.Length > this._rows.Length)
			{
				TTuple[] array = new TTuple[ParquetRowWriter<TTuple>.RoundUpToPowerOf2(this._pos + rows.Length)];
				Array.Copy(this._rows, array, this._pos);
				this._rows = array;
			}
			rows.CopyTo(this._rows.AsSpan(this._pos, rows.Length));
			this._pos += rows.Length;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00011220 File Offset: 0x0000F420
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WriteRow(TTuple row)
		{
			if (this._pos == this._rows.Length)
			{
				TTuple[] array = new TTuple[this._rows.Length * 2];
				Array.Copy(this._rows, array, this._rows.Length);
				this._rows = array;
			}
			TTuple[] rows = this._rows;
			int pos = this._pos;
			this._pos = pos + 1;
			rows[pos] = row;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001128C File Offset: 0x0000F48C
		internal void WriteColumn<[Nullable(2)] TValue>(TValue[] values, int length)
		{
			if (this._rowGroupWriter == null)
			{
				throw new InvalidOperationException("writer has been closed or disposed");
			}
			using (LogicalColumnWriter<TValue> logicalColumnWriter = this._rowGroupWriter.NextColumn().LogicalWriter<TValue>(4096))
			{
				logicalColumnWriter.WriteBatch(values, 0, length);
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000112F0 File Offset: 0x0000F4F0
		private void FlushAndDisposeRowGroup()
		{
			if (this._rowGroupWriter == null)
			{
				return;
			}
			try
			{
				this._writeAction(this, this._rows, this._pos);
				this._pos = 0;
			}
			finally
			{
				RowGroupWriter rowGroupWriter = this._rowGroupWriter;
				this._rowGroupWriter = null;
				rowGroupWriter.Dispose();
			}
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00011354 File Offset: 0x0000F554
		private static int RoundUpToPowerOf2(int x)
		{
			x--;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			x++;
			return x;
		}

		// Token: 0x04000169 RID: 361
		private readonly ParquetFileWriter _parquetFileWriter;

		// Token: 0x0400016A RID: 362
		[Nullable(new byte[] { 1, 0 })]
		private readonly ParquetRowWriter<TTuple>.WriteAction _writeAction;

		// Token: 0x0400016B RID: 363
		[Nullable(2)]
		private RowGroupWriter _rowGroupWriter;

		// Token: 0x0400016C RID: 364
		private TTuple[] _rows;

		// Token: 0x0400016D RID: 365
		private int _pos;

		// Token: 0x02000132 RID: 306
		// (Invoke) Token: 0x060009D3 RID: 2515
		[NullableContext(0)]
		internal delegate void WriteAction(ParquetRowWriter<TTuple> parquetRowWriter, TTuple[] rows, int length);
	}
}
