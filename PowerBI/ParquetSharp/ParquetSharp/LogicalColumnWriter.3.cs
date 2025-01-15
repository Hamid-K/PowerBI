using System;
using System.Runtime.CompilerServices;
using ParquetSharp.Schema;

namespace ParquetSharp
{
	// Token: 0x0200005B RID: 91
	[NullableContext(2)]
	[Nullable(new byte[] { 0, 1 })]
	internal sealed class LogicalColumnWriter<[IsUnmanaged, Nullable(0)] TPhysical, TLogical, TElement> : LogicalColumnWriter<TElement> where TPhysical : struct
	{
		// Token: 0x06000252 RID: 594 RVA: 0x00008774 File Offset: 0x00006974
		[NullableContext(1)]
		internal LogicalColumnWriter(ColumnWriter columnWriter, int bufferLength)
			: base(columnWriter, bufferLength)
		{
			this._byteBuffer = ((typeof(TPhysical) == typeof(ByteArray) || typeof(TPhysical) == typeof(FixedLenByteArray)) ? new ByteBuffer(bufferLength, 0) : null);
			LogicalWrite<TLogical, TPhysical>.Converter converter = (LogicalWrite<TLogical, TPhysical>.Converter)columnWriter.LogicalWriteConverterFactory.GetConverter<TLogical, TPhysical>(base.ColumnDescriptor, this._byteBuffer);
			Node[] schemaNodesPath = LogicalColumnStream<ColumnWriter>.GetSchemaNodesPath(base.ColumnDescriptor.SchemaNode);
			try
			{
				LogicalBatchWriterFactory<TPhysical, TLogical> logicalBatchWriterFactory = new LogicalBatchWriterFactory<TPhysical, TLogical>((ColumnWriter<TPhysical>)base.Source, (TPhysical[])this.Buffer, this.DefLevels, this.RepLevels, this._byteBuffer, converter);
				this._batchWriter = logicalBatchWriterFactory.GetWriter<TElement>(schemaNodesPath);
			}
			finally
			{
				Node[] array = schemaNodesPath;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Dispose();
				}
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000887C File Offset: 0x00006A7C
		public override void Dispose()
		{
			ByteBuffer byteBuffer = this._byteBuffer;
			if (byteBuffer != null)
			{
				byteBuffer.Dispose();
			}
			base.Dispose();
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000889C File Offset: 0x00006A9C
		public override void WriteBatch([Nullable(new byte[] { 0, 1 })] ReadOnlySpan<TElement> values)
		{
			this._batchWriter.WriteBatch(values);
		}

		// Token: 0x040000B8 RID: 184
		private readonly ByteBuffer _byteBuffer;

		// Token: 0x040000B9 RID: 185
		[Nullable(1)]
		private readonly ILogicalBatchWriter<TElement> _batchWriter;
	}
}
