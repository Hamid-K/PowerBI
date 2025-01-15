using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000059 RID: 89
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	public abstract class LogicalColumnWriter : LogicalColumnStream<ColumnWriter>
	{
		// Token: 0x06000249 RID: 585 RVA: 0x000085B8 File Offset: 0x000067B8
		protected LogicalColumnWriter(ColumnWriter columnWriter, Type elementType, int bufferLength)
			: base(columnWriter, columnWriter.ColumnDescriptor, elementType, columnWriter.ElementType, bufferLength)
		{
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000085E0 File Offset: 0x000067E0
		internal static LogicalColumnWriter Create(ColumnWriter columnWriter, int bufferLength, [Nullable(2)] Type elementTypeOverride)
		{
			if (columnWriter == null)
			{
				throw new ArgumentNullException("columnWriter");
			}
			Column[] columns = columnWriter.RowGroupWriter.ParquetFileWriter.Columns;
			Type leafElementType = LogicalColumnStream<ColumnWriter>.GetLeafElementType(elementTypeOverride ?? ((columns != null) ? columns[columnWriter.ColumnIndex].LogicalSystemType : null));
			return columnWriter.ColumnDescriptor.Apply<LogicalColumnWriter>(columnWriter.LogicalTypeFactory, leafElementType, true, new LogicalColumnWriter.Creator(columnWriter, bufferLength));
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00008658 File Offset: 0x00006858
		internal static LogicalColumnWriter<TElementType> Create<[Nullable(2)] TElementType>(ColumnWriter columnWriter, int bufferLength, [Nullable(2)] Type elementTypeOverride)
		{
			LogicalColumnWriter logicalColumnWriter = LogicalColumnWriter.Create(columnWriter, bufferLength, elementTypeOverride);
			LogicalColumnWriter<TElementType> logicalColumnWriter2;
			try
			{
				logicalColumnWriter2 = (LogicalColumnWriter<TElementType>)logicalColumnWriter;
			}
			catch (InvalidCastException ex)
			{
				Type type = logicalColumnWriter.GetType();
				string name = columnWriter.ColumnDescriptor.Name;
				logicalColumnWriter.Dispose();
				if (type.GetGenericTypeDefinition() != typeof(LogicalColumnWriter<, , >))
				{
					throw;
				}
				Type type2 = type.GetGenericArguments()[2];
				Type typeFromHandle = typeof(TElementType);
				throw new InvalidCastException(string.Format("Tried to get a LogicalColumnWriter for column {0} ('{1}') ", columnWriter.ColumnIndex, name) + string.Format("with an element type of '{0}' ", typeFromHandle) + string.Format("but the actual element type is '{0}'.", type2), ex);
			}
			catch
			{
				logicalColumnWriter.Dispose();
				throw;
			}
			return logicalColumnWriter2;
		}

		// Token: 0x0600024C RID: 588
		public abstract TReturn Apply<[Nullable(2)] TReturn>(ILogicalColumnWriterVisitor<TReturn> visitor);

		// Token: 0x0200010F RID: 271
		[Nullable(0)]
		private sealed class Creator : IColumnDescriptorVisitor<LogicalColumnWriter>
		{
			// Token: 0x06000956 RID: 2390 RVA: 0x0002BAAC File Offset: 0x00029CAC
			public Creator(ColumnWriter columnWriter, int bufferLength)
			{
				this._columnWriter = columnWriter;
				this._bufferLength = bufferLength;
			}

			// Token: 0x06000957 RID: 2391 RVA: 0x0002BAC4 File Offset: 0x00029CC4
			[NullableContext(2)]
			[return: Nullable(1)]
			public LogicalColumnWriter OnColumnDescriptor<[IsUnmanaged, Nullable(0)] TPhysical, TLogical, TElement>() where TPhysical : struct
			{
				return new LogicalColumnWriter<TPhysical, TLogical, TElement>(this._columnWriter, this._bufferLength);
			}

			// Token: 0x040002D8 RID: 728
			private readonly ColumnWriter _columnWriter;

			// Token: 0x040002D9 RID: 729
			private readonly int _bufferLength;
		}
	}
}
