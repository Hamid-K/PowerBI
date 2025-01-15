using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using ParquetSharp.Schema;

namespace ParquetSharp
{
	// Token: 0x02000050 RID: 80
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class LogicalBatchWriterFactory<[IsUnmanaged, Nullable(0)] TPhysical, [Nullable(2)] TLogical> where TPhysical : struct
	{
		// Token: 0x06000222 RID: 546 RVA: 0x000075F8 File Offset: 0x000057F8
		[NullableContext(2)]
		public LogicalBatchWriterFactory([Nullable(new byte[] { 1, 0 })] ColumnWriter<TPhysical> physicalWriter, [Nullable(new byte[] { 1, 0 })] TPhysical[] buffer, short[] defLevels, short[] repLevels, ByteBuffer byteBuffer, [Nullable(new byte[] { 1, 1, 0 })] LogicalWrite<TLogical, TPhysical>.Converter converter)
		{
			this._physicalWriter = physicalWriter;
			this._buffers = new LogicalStreamBuffers<TPhysical>(buffer, defLevels, repLevels);
			this._byteBuffer = byteBuffer;
			this._converter = converter;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00007628 File Offset: 0x00005828
		public ILogicalBatchWriter<TElement> GetWriter<[Nullable(2)] TElement>(Node[] schemaNodes)
		{
			return this.GetWriter<TElement>(schemaNodes, 0, 0, 0);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00007634 File Offset: 0x00005834
		private ILogicalBatchWriter<TElement> GetWriter<[Nullable(2)] TElement>(Node[] schemaNodes, short definitionLevel, short repetitionLevel, short firstRepetitionLevel)
		{
			if (typeof(TElement) == typeof(TLogical))
			{
				if (schemaNodes.Length != 1)
				{
					throw new Exception("Expected only a single schema node for the leaf element writer");
				}
				bool flag = schemaNodes[0].Repetition == Repetition.Optional;
				short num = (flag ? (definitionLevel + 1) : definitionLevel);
				return new ScalarWriter<TLogical, TPhysical>(this._physicalWriter, this._buffers, this._byteBuffer, this._converter, num, repetitionLevel, firstRepetitionLevel, flag) as ScalarWriter<TElement, TPhysical>;
			}
			else
			{
				Type type;
				if (TypeUtils.IsNullableNested(typeof(TElement), out type))
				{
					if (schemaNodes.Length > 1)
					{
						Node node = schemaNodes[0];
						if (node is GroupNode && node.Repetition == Repetition.Optional)
						{
							return this.MakeNestedOptionalWriter<TElement>(type, schemaNodes, definitionLevel, repetitionLevel, firstRepetitionLevel);
						}
					}
					throw new Exception("Unexpected schema for an optional nested element type");
				}
				Type type2;
				if (TypeUtils.IsNested(typeof(TElement), out type2))
				{
					if (schemaNodes.Length > 1)
					{
						Node node = schemaNodes[0];
						if (node is GroupNode && node.Repetition == Repetition.Required)
						{
							return this.MakeNestedWriter<TElement>(type2, schemaNodes, definitionLevel, repetitionLevel, firstRepetitionLevel);
						}
					}
					throw new Exception("Unexpected schema for required nested element type");
				}
				if (typeof(TElement).IsArray && SchemaUtils.IsListOrMap(schemaNodes))
				{
					return this.MakeArrayWriter<TElement>(schemaNodes, definitionLevel, repetitionLevel, firstRepetitionLevel);
				}
				throw new Exception(string.Format("Failed to create a batch writer for type {0}", typeof(TElement)));
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000077B0 File Offset: 0x000059B0
		private ILogicalBatchWriter<TElement> MakeArrayWriter<[Nullable(2)] TElement>(Node[] schemaNodes, short definitionLevel, short repetitionLevel, short firstRepetitionLevel)
		{
			Type elementType = typeof(TElement).GetElementType();
			if (elementType == null)
			{
				throw new NullReferenceException(string.Format("Element type is null for type {0}, expected an array type", typeof(TElement)));
			}
			Type type = elementType;
			bool flag = schemaNodes[0].Repetition == Repetition.Optional;
			short num = (flag ? (definitionLevel + 1) : definitionLevel);
			short num2 = num + 1;
			short num3 = repetitionLevel + 1;
			Node[] array = schemaNodes.AsSpan<Node>().Slice(2).ToArray();
			object obj = this.MakeGenericWriter(type, array, num2, num3, firstRepetitionLevel);
			object obj2 = this.MakeGenericWriter(type, array, num2, num3, repetitionLevel);
			return (ILogicalBatchWriter<TElement>)Activator.CreateInstance(typeof(ArrayWriter<, >).MakeGenericType(new Type[]
			{
				type,
				typeof(TPhysical)
			}), new object[] { obj, obj2, this._physicalWriter, flag, num, repetitionLevel, firstRepetitionLevel });
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000078C8 File Offset: 0x00005AC8
		private ILogicalBatchWriter<TElement> MakeNestedWriter<[Nullable(2)] TElement>(Type nestedType, Node[] schemaNodes, short definitionLevel, short repetitionLevel, short firstRepetitionLevel)
		{
			Node[] array = schemaNodes.AsSpan<Node>().Slice(1).ToArray();
			object obj = this.MakeGenericWriter(nestedType, array, definitionLevel, repetitionLevel, firstRepetitionLevel);
			object obj2 = this.MakeGenericWriter(nestedType, array, definitionLevel, repetitionLevel, repetitionLevel);
			return (ILogicalBatchWriter<TElement>)Activator.CreateInstance(typeof(NestedWriter<>).MakeGenericType(new Type[] { nestedType }), new object[]
			{
				obj,
				obj2,
				this._buffers.Length
			});
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00007954 File Offset: 0x00005B54
		private ILogicalBatchWriter<TElement> MakeNestedOptionalWriter<[Nullable(2)] TElement>(Type nestedType, Node[] schemaNodes, short definitionLevel, short repetitionLevel, short firstRepetitionLevel)
		{
			definitionLevel += 1;
			Node[] array = schemaNodes.AsSpan<Node>().Slice(1).ToArray();
			object obj = this.MakeGenericWriter(nestedType, array, definitionLevel, repetitionLevel, firstRepetitionLevel);
			object obj2 = this.MakeGenericWriter(nestedType, array, definitionLevel, repetitionLevel, repetitionLevel);
			return (ILogicalBatchWriter<TElement>)Activator.CreateInstance(typeof(OptionalNestedWriter<, >).MakeGenericType(new Type[]
			{
				nestedType,
				typeof(TPhysical)
			}), new object[] { obj, obj2, this._physicalWriter, this._buffers, definitionLevel, repetitionLevel, firstRepetitionLevel });
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00007A14 File Offset: 0x00005C14
		private object MakeGenericWriter(Type elementType, Node[] schemaNodes, short nullDefinitionLevel, short repetitionLevel, short firstRepetitionLevel)
		{
			MethodInfo method = typeof(LogicalBatchWriterFactory<TPhysical, TLogical>).GetMethod("GetWriter", BindingFlags.Instance | BindingFlags.NonPublic);
			if (method == null)
			{
				throw new Exception("Failed to reflect GetWriter method");
			}
			return method.MakeGenericMethod(new Type[] { elementType }).Invoke(this, new object[] { schemaNodes, nullDefinitionLevel, repetitionLevel, firstRepetitionLevel });
		}

		// Token: 0x04000092 RID: 146
		[Nullable(2)]
		private readonly ByteBuffer _byteBuffer;

		// Token: 0x04000093 RID: 147
		[Nullable(new byte[] { 1, 1, 0 })]
		private readonly LogicalWrite<TLogical, TPhysical>.Converter _converter;

		// Token: 0x04000094 RID: 148
		[Nullable(new byte[] { 1, 0 })]
		private readonly ColumnWriter<TPhysical> _physicalWriter;

		// Token: 0x04000095 RID: 149
		[Nullable(0)]
		private readonly LogicalStreamBuffers<TPhysical> _buffers;
	}
}
