using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ParquetSharp.Schema;

namespace ParquetSharp
{
	// Token: 0x02000058 RID: 88
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class LogicalColumnStream<TSource> : IDisposable where TSource : class, IDisposable
	{
		// Token: 0x06000241 RID: 577 RVA: 0x00008404 File Offset: 0x00006604
		protected LogicalColumnStream(TSource source, ColumnDescriptor descriptor, Type elementType, Type physicalType, int bufferLength)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.Source = source;
			if (descriptor == null)
			{
				throw new ArgumentNullException("descriptor");
			}
			this.ColumnDescriptor = descriptor;
			this.BufferLength = bufferLength;
			this.LogicalType = descriptor.LogicalType;
			this.Buffer = Array.CreateInstance(physicalType, bufferLength);
			this.DefLevels = ((descriptor.MaxDefinitionLevel == 0) ? null : new short[bufferLength]);
			this.RepLevels = ((descriptor.MaxRepetitionLevel == 0) ? null : new short[bufferLength]);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000084B4 File Offset: 0x000066B4
		public virtual void Dispose()
		{
			this.Source.Dispose();
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000084C8 File Offset: 0x000066C8
		[NullableContext(2)]
		protected static Type GetLeafElementType(Type type)
		{
			while (type != null)
			{
				Type type2;
				if (type != typeof(byte[]) && type.IsArray)
				{
					type = type.GetElementType();
				}
				else if (TypeUtils.IsNested(type, out type2))
				{
					type = type2;
				}
				else
				{
					Type type3;
					if (!TypeUtils.IsNullableNested(type, out type3))
					{
						break;
					}
					type = type3;
				}
			}
			return type;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000853C File Offset: 0x0000673C
		protected static Node[] GetSchemaNodesPath(Node node)
		{
			List<Node> list = new List<Node>();
			for (Node node2 = node; node2 != null; node2 = node2.Parent)
			{
				list.Add(node2);
			}
			list[list.Count - 1].Dispose();
			list.RemoveAt(list.Count - 1);
			list.Reverse();
			return list.ToArray();
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00008598 File Offset: 0x00006798
		public TSource Source { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000246 RID: 582 RVA: 0x000085A0 File Offset: 0x000067A0
		public ColumnDescriptor ColumnDescriptor { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000247 RID: 583 RVA: 0x000085A8 File Offset: 0x000067A8
		public int BufferLength { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000248 RID: 584 RVA: 0x000085B0 File Offset: 0x000067B0
		public LogicalType LogicalType { get; }

		// Token: 0x040000B5 RID: 181
		protected readonly Array Buffer;

		// Token: 0x040000B6 RID: 182
		[Nullable(2)]
		protected readonly short[] DefLevels;

		// Token: 0x040000B7 RID: 183
		[Nullable(2)]
		protected readonly short[] RepLevels;
	}
}
