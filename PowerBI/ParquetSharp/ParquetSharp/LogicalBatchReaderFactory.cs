using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using ParquetSharp.Schema;

namespace ParquetSharp
{
	// Token: 0x02000047 RID: 71
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class LogicalBatchReaderFactory<[IsUnmanaged, Nullable(0)] TPhysical, [Nullable(2)] TLogical> where TPhysical : struct
	{
		// Token: 0x06000202 RID: 514 RVA: 0x000067DC File Offset: 0x000049DC
		[NullableContext(2)]
		public LogicalBatchReaderFactory([Nullable(new byte[] { 1, 0 })] ColumnReader<TPhysical> physicalReader, [Nullable(new byte[] { 1, 0 })] TPhysical[] buffer, short[] defLevels, short[] repLevels, [Nullable(new byte[] { 2, 1, 0 })] LogicalRead<TLogical, TPhysical>.DirectReader directReader, [Nullable(new byte[] { 1, 1, 0 })] LogicalRead<TLogical, TPhysical>.Converter converter)
		{
			this._physicalReader = physicalReader;
			this._buffers = new LogicalStreamBuffers<TPhysical>(buffer, defLevels, repLevels);
			this._converter = converter;
			this._directReader = directReader;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000680C File Offset: 0x00004A0C
		public ILogicalBatchReader<TElement> GetReader<[Nullable(2)] TElement>(Node[] schemaNodes)
		{
			if (schemaNodes.Length != 1)
			{
				short num = (short)schemaNodes.Count((Node n) => n.Repetition > Repetition.Required);
				bool flag = schemaNodes.Last<Node>().Repetition == Repetition.Optional;
				this._bufferedReader = new BufferedReader<TLogical, TPhysical>(this._physicalReader, this._converter, this._buffers.Values, this._buffers.DefLevels, this._buffers.RepLevels, num, flag);
				return this.GetCompoundReader<TElement>(schemaNodes, 0, 0);
			}
			if (typeof(TElement) != typeof(TLogical))
			{
				throw new Exception(string.Format("Expected the element type ({0}) ", typeof(TElement)) + string.Format("to match the logical type ({0}) for scalar columns", typeof(TLogical)));
			}
			bool flag2 = schemaNodes[0].Repetition == Repetition.Optional;
			if (this._directReader != null && !flag2)
			{
				return new DirectReader<TElement, TPhysical>(this._physicalReader, this._directReader as LogicalRead<TElement, TPhysical>.DirectReader);
			}
			short num2 = ((flag2 > false) ? 1 : 0);
			return new ScalarReader<TLogical, TPhysical>(this._physicalReader, this._converter, this._buffers, num2) as ScalarReader<TElement, TPhysical>;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00006950 File Offset: 0x00004B50
		private ILogicalBatchReader<TElement> GetCompoundReader<[Nullable(2)] TElement>(Node[] schemaNodes, short definitionLevel, short repetitionLevel)
		{
			if (this._bufferedReader == null)
			{
				throw new Exception("A buffered reader is required for reading compound column values");
			}
			Type type;
			if (TypeUtils.IsNullableNested(typeof(TElement), out type))
			{
				if (schemaNodes.Length > 1)
				{
					Node node = schemaNodes[0];
					if (node is GroupNode && node.Repetition == Repetition.Optional)
					{
						return this.MakeNestedOptionalReader<TElement>(type, schemaNodes, definitionLevel, repetitionLevel);
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
						return this.MakeNestedReader<TElement>(type2, schemaNodes, definitionLevel, repetitionLevel);
					}
				}
				throw new Exception("Unexpected schema for required nested element type");
			}
			if (typeof(TElement).IsArray && SchemaUtils.IsListOrMap(schemaNodes))
			{
				return this.MakeArrayReader<TElement>(schemaNodes, definitionLevel, repetitionLevel);
			}
			if (schemaNodes.Length > 1)
			{
				GroupNode groupNode = schemaNodes[0] as GroupNode;
				if (groupNode != null)
				{
					bool flag = groupNode.Repetition == Repetition.Optional;
					Node[] array = schemaNodes.AsSpan<Node>().Slice(1).ToArray();
					short num = definitionLevel + ((flag > false) ? 1 : 0);
					return (ILogicalBatchReader<TElement>)this.MakeGenericReader(typeof(TElement), array, num, repetitionLevel);
				}
			}
			if (typeof(TElement) == typeof(TLogical))
			{
				if (schemaNodes.Length != 1)
				{
					throw new Exception("Expected only a single schema node for the leaf element reader");
				}
				return new LeafReader<TLogical, TPhysical>(this._bufferedReader) as LeafReader<TElement, TPhysical>;
			}
			else
			{
				Type type3;
				if (!TypeUtils.IsNullable(typeof(TElement), out type3) || !(type3 == typeof(TLogical)))
				{
					throw new Exception(string.Format("Failed to create a batch reader for type {0}", typeof(TElement)));
				}
				if (schemaNodes.Length != 1)
				{
					throw new Exception("Expected only a single schema node for the leaf element reader");
				}
				return this.MakeOptionalReader<TElement>(type3, schemaNodes, definitionLevel, repetitionLevel);
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00006B58 File Offset: 0x00004D58
		private ILogicalBatchReader<TElement> MakeArrayReader<[Nullable(2)] TElement>(Node[] schemaNodes, short definitionLevel, short repetitionLevel)
		{
			Type elementType = typeof(TElement).GetElementType();
			if (elementType == null)
			{
				throw new NullReferenceException(string.Format("Element type is null for type {0}, expected an array type", typeof(TElement)));
			}
			Type type = elementType;
			short num = ((schemaNodes[0].Repetition == Repetition.Optional) ? (definitionLevel + 1) : definitionLevel);
			short num2 = num + 1;
			short num3 = repetitionLevel + 1;
			Node[] array = schemaNodes.AsSpan<Node>().Slice(2).ToArray();
			bool flag = array[0].Repetition == Repetition.Optional;
			object obj = this.MakeGenericReader(type, array, num2, num3);
			return (ILogicalBatchReader<TElement>)Activator.CreateInstance(typeof(ArrayReader<, , >).MakeGenericType(new Type[]
			{
				typeof(TPhysical),
				typeof(TLogical),
				type
			}), new object[] { obj, this._bufferedReader, num, repetitionLevel, flag });
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00006C6C File Offset: 0x00004E6C
		private ILogicalBatchReader<TElement> MakeNestedReader<[Nullable(2)] TElement>(Type nestedType, Node[] schemaNodes, short definitionLevel, short repetitionLevel)
		{
			Node[] array = schemaNodes.AsSpan<Node>().Slice(1).ToArray();
			object obj = this.MakeGenericReader(nestedType, array, definitionLevel, repetitionLevel);
			return (ILogicalBatchReader<TElement>)Activator.CreateInstance(typeof(NestedReader<>).MakeGenericType(new Type[] { nestedType }), new object[]
			{
				obj,
				this._buffers.Length
			});
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00006CE4 File Offset: 0x00004EE4
		private ILogicalBatchReader<TElement> MakeNestedOptionalReader<[Nullable(2)] TElement>(Type nestedType, Node[] schemaNodes, short definitionLevel, short repetitionLevel)
		{
			definitionLevel += 1;
			Node[] array = schemaNodes.AsSpan<Node>().Slice(1).ToArray();
			object obj = this.MakeGenericReader(nestedType, array, definitionLevel, repetitionLevel);
			return (ILogicalBatchReader<TElement>)Activator.CreateInstance(typeof(OptionalNestedReader<, , >).MakeGenericType(new Type[]
			{
				typeof(TPhysical),
				typeof(TLogical),
				nestedType
			}), new object[] { obj, this._bufferedReader, definitionLevel });
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00006D78 File Offset: 0x00004F78
		private ILogicalBatchReader<TElement> MakeOptionalReader<[Nullable(2)] TElement>(Type innerType, Node[] schemaNodes, short definitionLevel, short repetitionLevel)
		{
			object obj = this.MakeGenericReader(innerType, schemaNodes, definitionLevel, repetitionLevel);
			return (ILogicalBatchReader<TElement>)Activator.CreateInstance(typeof(OptionalReader<, , >).MakeGenericType(new Type[]
			{
				typeof(TPhysical),
				typeof(TLogical),
				innerType
			}), new object[] { obj, this._bufferedReader, definitionLevel });
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00006DF0 File Offset: 0x00004FF0
		private object MakeGenericReader(Type elementType, Node[] schemaNodes, short nullDefinitionLevel, short repetitionLevel)
		{
			MethodInfo method = typeof(LogicalBatchReaderFactory<TPhysical, TLogical>).GetMethod("GetCompoundReader", BindingFlags.Instance | BindingFlags.NonPublic);
			if (method == null)
			{
				throw new Exception("Failed to reflect GetCompoundReader method");
			}
			return method.MakeGenericMethod(new Type[] { elementType }).Invoke(this, new object[] { schemaNodes, nullDefinitionLevel, repetitionLevel });
		}

		// Token: 0x04000079 RID: 121
		[Nullable(new byte[] { 1, 0 })]
		private readonly ColumnReader<TPhysical> _physicalReader;

		// Token: 0x0400007A RID: 122
		[Nullable(0)]
		private readonly LogicalStreamBuffers<TPhysical> _buffers;

		// Token: 0x0400007B RID: 123
		[Nullable(new byte[] { 2, 1, 0 })]
		private BufferedReader<TLogical, TPhysical> _bufferedReader;

		// Token: 0x0400007C RID: 124
		[Nullable(new byte[] { 2, 1, 0 })]
		private readonly LogicalRead<TLogical, TPhysical>.DirectReader _directReader;

		// Token: 0x0400007D RID: 125
		[Nullable(new byte[] { 1, 1, 0 })]
		private readonly LogicalRead<TLogical, TPhysical>.Converter _converter;
	}
}
