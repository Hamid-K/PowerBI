using System;
using System.Runtime.CompilerServices;
using ParquetSharp.Schema;

namespace ParquetSharp
{
	// Token: 0x02000057 RID: 87
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	internal sealed class LogicalColumnReader<[IsUnmanaged, Nullable(0)] TPhysical, [Nullable(2)] TLogical, [Nullable(2)] TElement> : LogicalColumnReader<TElement> where TPhysical : struct
	{
		// Token: 0x0600023E RID: 574 RVA: 0x0000831C File Offset: 0x0000651C
		internal LogicalColumnReader(ColumnReader columnReader, int bufferLength)
			: base(columnReader, bufferLength)
		{
			LogicalReadConverterFactory logicalReadConverterFactory = columnReader.LogicalReadConverterFactory;
			LogicalRead<TLogical, TPhysical>.Converter converter = (LogicalRead<TLogical, TPhysical>.Converter)logicalReadConverterFactory.GetConverter<TLogical, TPhysical>(base.ColumnDescriptor, columnReader.ColumnChunkMetaData);
			Node[] schemaNodesPath = LogicalColumnStream<ColumnReader>.GetSchemaNodesPath(base.ColumnDescriptor.SchemaNode);
			try
			{
				LogicalRead<TLogical, TPhysical>.DirectReader directReader = (LogicalRead<TLogical, TPhysical>.DirectReader)logicalReadConverterFactory.GetDirectReader<TLogical, TPhysical>();
				LogicalBatchReaderFactory<TPhysical, TLogical> logicalBatchReaderFactory = new LogicalBatchReaderFactory<TPhysical, TLogical>((ColumnReader<TPhysical>)base.Source, (TPhysical[])this.Buffer, this.DefLevels, this.RepLevels, directReader, converter);
				this._batchReader = logicalBatchReaderFactory.GetReader<TElement>(schemaNodesPath);
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

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600023F RID: 575 RVA: 0x000083E4 File Offset: 0x000065E4
		public override bool HasNext
		{
			get
			{
				return this._batchReader.HasNext();
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000083F4 File Offset: 0x000065F4
		public override int ReadBatch([Nullable(new byte[] { 0, 1 })] Span<TElement> destination)
		{
			return this._batchReader.ReadBatch(destination);
		}

		// Token: 0x040000B0 RID: 176
		private readonly ILogicalBatchReader<TElement> _batchReader;
	}
}
