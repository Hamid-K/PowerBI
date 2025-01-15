using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000055 RID: 85
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1 })]
	public abstract class LogicalColumnReader : LogicalColumnStream<ColumnReader>
	{
		// Token: 0x06000231 RID: 561 RVA: 0x000080D8 File Offset: 0x000062D8
		protected LogicalColumnReader(ColumnReader columnReader, Type elementType, int bufferLength)
			: base(columnReader, columnReader.ColumnDescriptor, elementType, columnReader.ElementType, bufferLength)
		{
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00008100 File Offset: 0x00006300
		internal static LogicalColumnReader Create(ColumnReader columnReader, int bufferLength, [Nullable(2)] Type elementTypeOverride, bool useNesting)
		{
			if (columnReader == null)
			{
				throw new ArgumentNullException("columnReader");
			}
			Type leafElementType = LogicalColumnStream<ColumnReader>.GetLeafElementType(elementTypeOverride);
			return columnReader.ColumnDescriptor.Apply<LogicalColumnReader>(columnReader.LogicalTypeFactory, leafElementType, useNesting, new LogicalColumnReader.Creator(columnReader, bufferLength));
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00008144 File Offset: 0x00006344
		internal static LogicalColumnReader<TElement> Create<[Nullable(2)] TElement>(ColumnReader columnReader, int bufferLength, [Nullable(2)] Type elementTypeOverride)
		{
			bool flag = LogicalColumnReader.ContainsNestedType(typeof(TElement));
			LogicalColumnReader logicalColumnReader = LogicalColumnReader.Create(columnReader, bufferLength, elementTypeOverride, flag);
			LogicalColumnReader<TElement> logicalColumnReader2;
			try
			{
				logicalColumnReader2 = (LogicalColumnReader<TElement>)logicalColumnReader;
			}
			catch (InvalidCastException ex)
			{
				Type type = logicalColumnReader.GetType();
				string name = columnReader.ColumnDescriptor.Name;
				logicalColumnReader.Dispose();
				if (type.GetGenericTypeDefinition() != typeof(LogicalColumnReader<, , >))
				{
					throw;
				}
				Type type2 = type.GetGenericArguments()[2];
				Type typeFromHandle = typeof(TElement);
				throw new InvalidCastException(string.Format("Tried to get a LogicalColumnReader for column {0} ('{1}') ", columnReader.ColumnIndex, name) + string.Format("with an element type of '{0}' ", typeFromHandle) + string.Format("but the actual element type is '{0}'.", type2), ex);
			}
			catch
			{
				logicalColumnReader.Dispose();
				throw;
			}
			return logicalColumnReader2;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000234 RID: 564
		public abstract bool HasNext { get; }

		// Token: 0x06000235 RID: 565
		public abstract TReturn Apply<[Nullable(2)] TReturn>(ILogicalColumnReaderVisitor<TReturn> visitor);

		// Token: 0x06000236 RID: 566 RVA: 0x00008228 File Offset: 0x00006428
		private static bool ContainsNestedType(Type type)
		{
			for (;;)
			{
				if (type != typeof(byte[]) && type.IsArray)
				{
					type = type.GetElementType();
				}
				else
				{
					Type type2;
					if (!TypeUtils.IsNullable(type, out type2))
					{
						break;
					}
					type = type2;
				}
			}
			Type type3;
			return TypeUtils.IsNested(type, out type3);
		}

		// Token: 0x0200010D RID: 269
		[Nullable(0)]
		private sealed class Creator : IColumnDescriptorVisitor<LogicalColumnReader>
		{
			// Token: 0x0600094E RID: 2382 RVA: 0x0002B990 File Offset: 0x00029B90
			public Creator(ColumnReader columnReader, int bufferLength)
			{
				this._columnReader = columnReader;
				this._bufferLength = bufferLength;
			}

			// Token: 0x0600094F RID: 2383 RVA: 0x0002B9A8 File Offset: 0x00029BA8
			[NullableContext(2)]
			[return: Nullable(1)]
			public LogicalColumnReader OnColumnDescriptor<[IsUnmanaged, Nullable(0)] TPhysical, TLogical, TElement>() where TPhysical : struct
			{
				return new LogicalColumnReader<TPhysical, TLogical, TElement>(this._columnReader, this._bufferLength);
			}

			// Token: 0x040002D0 RID: 720
			private readonly ColumnReader _columnReader;

			// Token: 0x040002D1 RID: 721
			private readonly int _bufferLength;
		}
	}
}
