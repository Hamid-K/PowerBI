using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ParquetSharp.IO;
using ParquetSharp.Schema;

namespace ParquetSharp
{
	// Token: 0x0200007D RID: 125
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ParquetFileWriter : IDisposable
	{
		// Token: 0x06000337 RID: 823 RVA: 0x0000CF54 File Offset: 0x0000B154
		public ParquetFileWriter(string path, Column[] columns, Compression compression = Compression.Snappy, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> keyValueMetadata = null)
		{
			using (GroupNode groupNode = Column.CreateSchemaNode(columns, "schema"))
			{
				using (WriterProperties writerProperties = ParquetFileWriter.CreateWriterProperties(compression))
				{
					if (keyValueMetadata != null)
					{
						this._keyValueMetadata = keyValueMetadata;
						this._parquetKeyValueMetadata = new KeyValueMetadata();
					}
					this._handle = ParquetFileWriter.CreateParquetFileWriter(path, groupNode, writerProperties, this._parquetKeyValueMetadata);
					this.Columns = columns;
				}
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000D000 File Offset: 0x0000B200
		public ParquetFileWriter(OutputStream outputStream, Column[] columns, Compression compression = Compression.Snappy, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> keyValueMetadata = null)
		{
			using (GroupNode groupNode = Column.CreateSchemaNode(columns, "schema"))
			{
				using (WriterProperties writerProperties = ParquetFileWriter.CreateWriterProperties(compression))
				{
					if (keyValueMetadata != null)
					{
						this._keyValueMetadata = keyValueMetadata;
						this._parquetKeyValueMetadata = new KeyValueMetadata();
					}
					this._handle = ParquetFileWriter.CreateParquetFileWriter(outputStream, groupNode, writerProperties, this._parquetKeyValueMetadata);
					this._outputStream = outputStream;
					this.Columns = columns;
				}
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000D0B4 File Offset: 0x0000B2B4
		public ParquetFileWriter(string path, Column[] columns, [Nullable(2)] LogicalTypeFactory logicalTypeFactory, Compression compression = Compression.Snappy, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> keyValueMetadata = null)
		{
			using (GroupNode groupNode = Column.CreateSchemaNode(columns, this.LogicalTypeFactory = logicalTypeFactory ?? LogicalTypeFactory.Default, "schema"))
			{
				using (WriterProperties writerProperties = ParquetFileWriter.CreateWriterProperties(compression))
				{
					if (keyValueMetadata != null)
					{
						this._keyValueMetadata = keyValueMetadata;
						this._parquetKeyValueMetadata = new KeyValueMetadata();
					}
					this._handle = ParquetFileWriter.CreateParquetFileWriter(path, groupNode, writerProperties, this._parquetKeyValueMetadata);
					this.Columns = columns;
				}
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000D178 File Offset: 0x0000B378
		public ParquetFileWriter(OutputStream outputStream, Column[] columns, [Nullable(2)] LogicalTypeFactory logicalTypeFactory, Compression compression = Compression.Snappy, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> keyValueMetadata = null)
		{
			using (GroupNode groupNode = Column.CreateSchemaNode(columns, this.LogicalTypeFactory = logicalTypeFactory ?? LogicalTypeFactory.Default, "schema"))
			{
				using (WriterProperties writerProperties = ParquetFileWriter.CreateWriterProperties(compression))
				{
					if (keyValueMetadata != null)
					{
						this._keyValueMetadata = keyValueMetadata;
						this._parquetKeyValueMetadata = new KeyValueMetadata();
					}
					this._handle = ParquetFileWriter.CreateParquetFileWriter(outputStream, groupNode, writerProperties, this._parquetKeyValueMetadata);
					this._outputStream = outputStream;
					this.Columns = columns;
				}
			}
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000D244 File Offset: 0x0000B444
		public ParquetFileWriter(Stream stream, Column[] columns, [Nullable(2)] LogicalTypeFactory logicalTypeFactory = null, Compression compression = Compression.Snappy, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> keyValueMetadata = null, bool leaveOpen = false)
		{
			using (GroupNode groupNode = Column.CreateSchemaNode(columns, this.LogicalTypeFactory = logicalTypeFactory ?? LogicalTypeFactory.Default, "schema"))
			{
				using (WriterProperties writerProperties = ParquetFileWriter.CreateWriterProperties(compression))
				{
					if (keyValueMetadata != null)
					{
						this._keyValueMetadata = keyValueMetadata;
						this._parquetKeyValueMetadata = new KeyValueMetadata();
					}
					ManagedOutputStream managedOutputStream = new ManagedOutputStream(stream, leaveOpen);
					this._handle = ParquetFileWriter.CreateParquetFileWriter(managedOutputStream, groupNode, writerProperties, this._parquetKeyValueMetadata);
					this._outputStream = managedOutputStream;
					this._ownedStream = true;
					this.Columns = columns;
				}
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000D320 File Offset: 0x0000B520
		public ParquetFileWriter(string path, Column[] columns, WriterProperties writerProperties, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> keyValueMetadata = null)
		{
			using (GroupNode groupNode = Column.CreateSchemaNode(columns, "schema"))
			{
				if (keyValueMetadata != null)
				{
					this._keyValueMetadata = keyValueMetadata;
					this._parquetKeyValueMetadata = new KeyValueMetadata();
				}
				this._handle = ParquetFileWriter.CreateParquetFileWriter(path, groupNode, writerProperties, this._parquetKeyValueMetadata);
				this.Columns = columns;
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000D3AC File Offset: 0x0000B5AC
		public ParquetFileWriter(OutputStream outputStream, Column[] columns, WriterProperties writerProperties, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> keyValueMetadata = null)
		{
			using (GroupNode groupNode = Column.CreateSchemaNode(columns, "schema"))
			{
				if (keyValueMetadata != null)
				{
					this._keyValueMetadata = keyValueMetadata;
					this._parquetKeyValueMetadata = new KeyValueMetadata();
				}
				this._handle = ParquetFileWriter.CreateParquetFileWriter(outputStream, groupNode, writerProperties, this._parquetKeyValueMetadata);
				this._outputStream = outputStream;
				this.Columns = columns;
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000D440 File Offset: 0x0000B640
		public ParquetFileWriter(string path, Column[] columns, [Nullable(2)] LogicalTypeFactory logicalTypeFactory, WriterProperties writerProperties, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> keyValueMetadata = null)
		{
			using (GroupNode groupNode = Column.CreateSchemaNode(columns, this.LogicalTypeFactory = logicalTypeFactory ?? LogicalTypeFactory.Default, "schema"))
			{
				if (keyValueMetadata != null)
				{
					this._keyValueMetadata = keyValueMetadata;
					this._parquetKeyValueMetadata = new KeyValueMetadata();
				}
				this._handle = ParquetFileWriter.CreateParquetFileWriter(path, groupNode, writerProperties, this._parquetKeyValueMetadata);
				this.Columns = columns;
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000D4E4 File Offset: 0x0000B6E4
		public ParquetFileWriter(OutputStream outputStream, Column[] columns, [Nullable(2)] LogicalTypeFactory logicalTypeFactory, WriterProperties writerProperties, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> keyValueMetadata = null)
		{
			using (GroupNode groupNode = Column.CreateSchemaNode(columns, this.LogicalTypeFactory = logicalTypeFactory ?? LogicalTypeFactory.Default, "schema"))
			{
				if (keyValueMetadata != null)
				{
					this._keyValueMetadata = keyValueMetadata;
					this._parquetKeyValueMetadata = new KeyValueMetadata();
				}
				this._handle = ParquetFileWriter.CreateParquetFileWriter(outputStream, groupNode, writerProperties, this._parquetKeyValueMetadata);
				this._outputStream = outputStream;
				this.Columns = columns;
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000D590 File Offset: 0x0000B790
		public ParquetFileWriter(Stream stream, Column[] columns, [Nullable(2)] LogicalTypeFactory logicalTypeFactory, WriterProperties writerProperties, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> keyValueMetadata = null, bool leaveOpen = false)
		{
			using (GroupNode groupNode = Column.CreateSchemaNode(columns, this.LogicalTypeFactory = logicalTypeFactory ?? LogicalTypeFactory.Default, "schema"))
			{
				if (keyValueMetadata != null)
				{
					this._keyValueMetadata = keyValueMetadata;
					this._parquetKeyValueMetadata = new KeyValueMetadata();
				}
				ManagedOutputStream managedOutputStream = new ManagedOutputStream(stream, leaveOpen);
				this._handle = ParquetFileWriter.CreateParquetFileWriter(managedOutputStream, groupNode, writerProperties, this._parquetKeyValueMetadata);
				this._outputStream = managedOutputStream;
				this._ownedStream = true;
				this.Columns = columns;
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000D64C File Offset: 0x0000B84C
		public ParquetFileWriter(string path, GroupNode schema, WriterProperties writerProperties, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> keyValueMetadata = null)
		{
			if (keyValueMetadata != null)
			{
				this._keyValueMetadata = keyValueMetadata;
				this._parquetKeyValueMetadata = new KeyValueMetadata();
			}
			this._handle = ParquetFileWriter.CreateParquetFileWriter(path, schema, writerProperties, this._parquetKeyValueMetadata);
			this.Columns = null;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000D6B0 File Offset: 0x0000B8B0
		public ParquetFileWriter(OutputStream outputStream, GroupNode schema, WriterProperties writerProperties, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> keyValueMetadata = null)
		{
			if (keyValueMetadata != null)
			{
				this._keyValueMetadata = keyValueMetadata;
				this._parquetKeyValueMetadata = new KeyValueMetadata();
			}
			this._handle = ParquetFileWriter.CreateParquetFileWriter(outputStream, schema, writerProperties, this._parquetKeyValueMetadata);
			this._outputStream = outputStream;
			this.Columns = null;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000D71C File Offset: 0x0000B91C
		public ParquetFileWriter(Stream stream, GroupNode schema, WriterProperties writerProperties, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> keyValueMetadata = null, bool leaveOpen = false)
		{
			if (keyValueMetadata != null)
			{
				this._keyValueMetadata = keyValueMetadata;
				this._parquetKeyValueMetadata = new KeyValueMetadata();
			}
			ManagedOutputStream managedOutputStream = new ManagedOutputStream(stream, leaveOpen);
			this._handle = ParquetFileWriter.CreateParquetFileWriter(managedOutputStream, schema, writerProperties, this._parquetKeyValueMetadata);
			this._outputStream = managedOutputStream;
			this._ownedStream = true;
			this.Columns = null;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000D798 File Offset: 0x0000B998
		public void Dispose()
		{
			this.SetKeyValueMetadata();
			KeyValueMetadata parquetKeyValueMetadata = this._parquetKeyValueMetadata;
			if (parquetKeyValueMetadata != null)
			{
				parquetKeyValueMetadata.Dispose();
			}
			FileMetaData fileMetaData = this._fileMetaData;
			if (fileMetaData != null)
			{
				fileMetaData.Dispose();
			}
			this._handle.Dispose();
			if (this._ownedStream)
			{
				OutputStream outputStream = this._outputStream;
				if (outputStream == null)
				{
					return;
				}
				outputStream.Dispose();
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000D808 File Offset: 0x0000BA08
		public void Close()
		{
			this.SetKeyValueMetadata();
			ExceptionInfo.Check(ParquetFileWriter.ParquetFileWriter_Close(this._handle.IntPtr));
			GC.KeepAlive(this._handle);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000D830 File Offset: 0x0000BA30
		public RowGroupWriter AppendRowGroup()
		{
			return new RowGroupWriter(ExceptionInfo.Return<IntPtr>(this._handle, new ExceptionInfo.GetFunction<IntPtr>(ParquetFileWriter.ParquetFileWriter_AppendRowGroup)), this);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000D850 File Offset: 0x0000BA50
		public RowGroupWriter AppendBufferedRowGroup()
		{
			return new RowGroupWriter(ExceptionInfo.Return<IntPtr>(this._handle, new ExceptionInfo.GetFunction<IntPtr>(ParquetFileWriter.ParquetFileWriter_AppendBufferedRowGroup)), this);
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000D870 File Offset: 0x0000BA70
		internal int NumColumns
		{
			get
			{
				return ExceptionInfo.Return<int>(this._handle, new ExceptionInfo.GetFunction<int>(ParquetFileWriter.ParquetFileWriter_Num_Columns));
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000D88C File Offset: 0x0000BA8C
		internal long NumRows
		{
			get
			{
				return ExceptionInfo.Return<long>(this._handle, new ExceptionInfo.GetFunction<long>(ParquetFileWriter.ParquetFileWriter_Num_Rows));
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000D8A8 File Offset: 0x0000BAA8
		internal int NumRowGroups
		{
			get
			{
				return ExceptionInfo.Return<int>(this._handle, new ExceptionInfo.GetFunction<int>(ParquetFileWriter.ParquetFileWriter_Num_Row_Groups));
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000D8C4 File Offset: 0x0000BAC4
		// (set) Token: 0x0600034C RID: 844 RVA: 0x0000D8CC File Offset: 0x0000BACC
		public LogicalTypeFactory LogicalTypeFactory { get; set; } = LogicalTypeFactory.Default;

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000D8D8 File Offset: 0x0000BAD8
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0000D8E0 File Offset: 0x0000BAE0
		public LogicalWriteConverterFactory LogicalWriteConverterFactory { get; set; } = LogicalWriteConverterFactory.Default;

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000D8EC File Offset: 0x0000BAEC
		public WriterProperties WriterProperties
		{
			get
			{
				WriterProperties writerProperties;
				if ((writerProperties = this._writerProperties) == null)
				{
					writerProperties = (this._writerProperties = new WriterProperties(ExceptionInfo.Return<IntPtr>(this._handle, new ExceptionInfo.GetFunction<IntPtr>(ParquetFileWriter.ParquetFileWriter_Properties))));
				}
				return writerProperties;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000D930 File Offset: 0x0000BB30
		public SchemaDescriptor Schema
		{
			get
			{
				return new SchemaDescriptor(ExceptionInfo.Return<IntPtr>(this._handle, new ExceptionInfo.GetFunction<IntPtr>(ParquetFileWriter.ParquetFileWriter_Schema)));
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000D950 File Offset: 0x0000BB50
		public ColumnDescriptor ColumnDescriptor(int i)
		{
			return new ColumnDescriptor(ExceptionInfo.Return<int, IntPtr>(this._handle, i, new ExceptionInfo.GetFunction<int, IntPtr>(ParquetFileWriter.ParquetFileWriter_Descr)));
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000D970 File Offset: 0x0000BB70
		public IReadOnlyDictionary<string, string> KeyValueMetadata
		{
			get
			{
				if (this._keyValueMetadata == null)
				{
					return new Dictionary<string, string>();
				}
				Dictionary<string, string> dictionary = new Dictionary<string, string>(this._keyValueMetadata.Count);
				foreach (KeyValuePair<string, string> keyValuePair in this._keyValueMetadata)
				{
					dictionary[keyValuePair.Key] = keyValuePair.Value;
				}
				return dictionary;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000D9F8 File Offset: 0x0000BBF8
		[Nullable(2)]
		public FileMetaData FileMetaData
		{
			[NullableContext(2)]
			get
			{
				if (this._fileMetaData != null)
				{
					return this._fileMetaData;
				}
				IntPtr intPtr = ExceptionInfo.Return<IntPtr>(this._handle, new ExceptionInfo.GetFunction<IntPtr>(ParquetFileWriter.ParquetFileWriter_Metadata));
				return this._fileMetaData = ((intPtr == IntPtr.Zero) ? null : new FileMetaData(intPtr));
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000DA58 File Offset: 0x0000BC58
		private void SetKeyValueMetadata()
		{
			if (this._keyValueMetadataSet)
			{
				return;
			}
			if (this._keyValueMetadata != null && this._parquetKeyValueMetadata != null)
			{
				this._parquetKeyValueMetadata.SetData(this._keyValueMetadata);
			}
			this._keyValueMetadataSet = true;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000DA94 File Offset: 0x0000BC94
		private static ParquetHandle CreateParquetFileWriter(string path, GroupNode schema, WriterProperties writerProperties, [Nullable(2)] KeyValueMetadata keyValueMetadata)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			if (writerProperties == null)
			{
				throw new ArgumentNullException("writerProperties");
			}
			path = LongPath.EnsureLongPathSafe(path);
			ParquetHandle parquetHandle;
			using (ByteBuffer byteBuffer = new ByteBuffer(0, 0))
			{
				IntPtr intPtr;
				ExceptionInfo.Check(ParquetFileWriter.ParquetFileWriter_OpenFile(StringUtil.ToCStringUtf8(path, byteBuffer), schema.Handle.IntPtr, writerProperties.Handle.IntPtr, (keyValueMetadata != null) ? keyValueMetadata.Handle.IntPtr : IntPtr.Zero, out intPtr));
				GC.KeepAlive(schema);
				GC.KeepAlive(writerProperties);
				parquetHandle = new ParquetHandle(intPtr, new Action<IntPtr>(ParquetFileWriter.ParquetFileWriter_Free));
			}
			return parquetHandle;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000DB6C File Offset: 0x0000BD6C
		private static ParquetHandle CreateParquetFileWriter(OutputStream outputStream, GroupNode schema, WriterProperties writerProperties, [Nullable(2)] KeyValueMetadata keyValueMetadata)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			if (outputStream.Handle == null)
			{
				throw new ArgumentNullException("Handle");
			}
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			if (writerProperties == null)
			{
				throw new ArgumentNullException("writerProperties");
			}
			IntPtr intPtr;
			ExceptionInfo.Check(ParquetFileWriter.ParquetFileWriter_Open(outputStream.Handle.IntPtr, schema.Handle.IntPtr, writerProperties.Handle.IntPtr, (keyValueMetadata != null) ? keyValueMetadata.Handle.IntPtr : IntPtr.Zero, out intPtr));
			GC.KeepAlive(schema);
			GC.KeepAlive(writerProperties);
			return new ParquetHandle(intPtr, new Action<IntPtr>(ParquetFileWriter.ParquetFileWriter_Free));
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000DC2C File Offset: 0x0000BE2C
		private static WriterProperties CreateWriterProperties(Compression compression)
		{
			WriterProperties writerProperties;
			using (WriterPropertiesBuilder writerPropertiesBuilder = new WriterPropertiesBuilder())
			{
				writerPropertiesBuilder.Compression(compression);
				writerProperties = writerPropertiesBuilder.Build();
			}
			return writerProperties;
		}

		// Token: 0x06000358 RID: 856
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ParquetFileWriter_OpenFile(IntPtr path, IntPtr schema, IntPtr writerProperties, IntPtr keyValueMetadata, out IntPtr writer);

		// Token: 0x06000359 RID: 857
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ParquetFileWriter_Open(IntPtr outputStream, IntPtr schema, IntPtr writerProperties, IntPtr keyValueMetadata, out IntPtr writer);

		// Token: 0x0600035A RID: 858
		[DllImport("ParquetSharpNative")]
		private static extern void ParquetFileWriter_Free(IntPtr writer);

		// Token: 0x0600035B RID: 859
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ParquetFileWriter_Close(IntPtr writer);

		// Token: 0x0600035C RID: 860
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ParquetFileWriter_AppendRowGroup(IntPtr writer, out IntPtr rowGroupWriter);

		// Token: 0x0600035D RID: 861
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ParquetFileWriter_AppendBufferedRowGroup(IntPtr writer, out IntPtr rowGroupWriter);

		// Token: 0x0600035E RID: 862
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ParquetFileWriter_Num_Columns(IntPtr writer, out int numColumns);

		// Token: 0x0600035F RID: 863
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ParquetFileWriter_Num_Rows(IntPtr writer, out long numRows);

		// Token: 0x06000360 RID: 864
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ParquetFileWriter_Num_Row_Groups(IntPtr writer, out int numRowGroups);

		// Token: 0x06000361 RID: 865
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ParquetFileWriter_Properties(IntPtr writer, out IntPtr properties);

		// Token: 0x06000362 RID: 866
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ParquetFileWriter_Schema(IntPtr writer, out IntPtr schema);

		// Token: 0x06000363 RID: 867
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ParquetFileWriter_Descr(IntPtr writer, int i, out IntPtr descr);

		// Token: 0x06000364 RID: 868
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ParquetFileWriter_Metadata(IntPtr writer, out IntPtr metadata);

		// Token: 0x040000EB RID: 235
		private readonly ParquetHandle _handle;

		// Token: 0x040000EC RID: 236
		[Nullable(2)]
		private readonly KeyValueMetadata _parquetKeyValueMetadata;

		// Token: 0x040000ED RID: 237
		[Nullable(new byte[] { 2, 1, 1 })]
		private readonly IReadOnlyDictionary<string, string> _keyValueMetadata;

		// Token: 0x040000EE RID: 238
		[Nullable(new byte[] { 2, 1 })]
		internal readonly Column[] Columns;

		// Token: 0x040000EF RID: 239
		[Nullable(2)]
		private FileMetaData _fileMetaData;

		// Token: 0x040000F0 RID: 240
		[Nullable(2)]
		private WriterProperties _writerProperties;

		// Token: 0x040000F1 RID: 241
		private bool _keyValueMetadataSet;

		// Token: 0x040000F2 RID: 242
		[Nullable(2)]
		private readonly OutputStream _outputStream;

		// Token: 0x040000F3 RID: 243
		private readonly bool _ownedStream;
	}
}
