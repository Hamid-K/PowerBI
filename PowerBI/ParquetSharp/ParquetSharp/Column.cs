using System;
using System.Linq;
using System.Runtime.CompilerServices;
using ParquetSharp.Schema;

namespace ParquetSharp
{
	// Token: 0x02000012 RID: 18
	[NullableContext(1)]
	[Nullable(0)]
	public class Column
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002DBC File Offset: 0x00000FBC
		public Column(Type logicalSystemType, string name, [Nullable(2)] LogicalType logicalTypeOverride = null)
			: this(logicalSystemType, name, logicalTypeOverride, Column.GetTypeLength(logicalSystemType))
		{
			if (logicalSystemType == null)
			{
				throw new ArgumentNullException("logicalSystemType");
			}
			this.LogicalSystemType = logicalSystemType;
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.Name = name;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002E10 File Offset: 0x00001010
		public Column(Type logicalSystemType, string name, [Nullable(2)] LogicalType logicalTypeOverride, int length)
		{
			bool flag = logicalSystemType == typeof(decimal) || logicalSystemType == typeof(decimal?);
			bool flag2 = logicalSystemType == typeof(Guid) || logicalSystemType == typeof(Guid?);
			if (length != -1 && !flag && !flag2)
			{
				throw new ArgumentException("length can only be set with the decimal or Guid type");
			}
			if (flag && !(logicalTypeOverride is DecimalLogicalType))
			{
				throw new ArgumentException("decimal type requires a DecimalLogicalType override");
			}
			if (flag2 && !(logicalTypeOverride is UuidLogicalType))
			{
				throw new ArgumentException("Guid type requires a UuidLogicalType override");
			}
			if (logicalSystemType == null)
			{
				throw new ArgumentNullException("logicalSystemType");
			}
			this.LogicalSystemType = logicalSystemType;
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.Name = name;
			this.LogicalTypeOverride = logicalTypeOverride;
			this.Length = length;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002F18 File Offset: 0x00001118
		public Node CreateSchemaNode()
		{
			return this.CreateSchemaNode(LogicalTypeFactory.Default);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002F28 File Offset: 0x00001128
		public Node CreateSchemaNode(LogicalTypeFactory typeFactory)
		{
			return Column.CreateSchemaNode(typeFactory, this.LogicalSystemType, this.Name, this.LogicalTypeOverride, this.Length);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002F48 File Offset: 0x00001148
		public static GroupNode CreateSchemaNode(Column[] columns, string nodeName = "schema")
		{
			return Column.CreateSchemaNode(columns, LogicalTypeFactory.Default, nodeName);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002F58 File Offset: 0x00001158
		public static GroupNode CreateSchemaNode(Column[] columns, LogicalTypeFactory logicalTypeFactory, string nodeName = "schema")
		{
			if (columns == null)
			{
				throw new ArgumentNullException("columns");
			}
			Node[] array = columns.Select((Column c) => c.CreateSchemaNode(logicalTypeFactory)).ToArray<Node>();
			GroupNode groupNode;
			try
			{
				groupNode = new GroupNode(nodeName, Repetition.Required, array, null);
			}
			finally
			{
				Node[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i].Dispose();
				}
			}
			return groupNode;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002FE0 File Offset: 0x000011E0
		private unsafe static int GetTypeLength(Type logicalSystemType)
		{
			if (logicalSystemType == typeof(decimal) || logicalSystemType == typeof(decimal?))
			{
				return sizeof(Decimal128);
			}
			if (logicalSystemType == typeof(Guid) || logicalSystemType == typeof(Guid?))
			{
				return 16;
			}
			return -1;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003050 File Offset: 0x00001250
		private static Node CreateSchemaNode(LogicalTypeFactory logicalTypeFactory, Type type, string name, [Nullable(2)] LogicalType logicalTypeOverride, int length)
		{
			global::System.ValueTuple<LogicalType, Repetition, PhysicalType> valueTuple;
			if (logicalTypeFactory.TryGetParquetTypes(type, out valueTuple))
			{
				global::System.ValueTuple<LogicalType, PhysicalType> typesOverride = logicalTypeFactory.GetTypesOverride(logicalTypeOverride, valueTuple.Item1, valueTuple.Item3);
				return new PrimitiveNode(name, valueTuple.Item2, typesOverride.Item1, typesOverride.Item2, length);
			}
			if (type.IsArray)
			{
				Node node = Column.CreateSchemaNode(logicalTypeFactory, type.GetElementType(), "item", logicalTypeOverride, length);
				GroupNode groupNode = new GroupNode("list", Repetition.Repeated, new Node[] { node }, null);
				try
				{
					using (LogicalType logicalType = LogicalType.List())
					{
						return new GroupNode(name, Repetition.Optional, new GroupNode[] { groupNode }, logicalType);
					}
				}
				finally
				{
					groupNode.Dispose();
					node.Dispose();
				}
			}
			throw new ArgumentException(string.Format("unsupported logical type {0}", type));
		}

		// Token: 0x04000027 RID: 39
		public readonly Type LogicalSystemType;

		// Token: 0x04000028 RID: 40
		public readonly string Name;

		// Token: 0x04000029 RID: 41
		[Nullable(2)]
		public readonly LogicalType LogicalTypeOverride;

		// Token: 0x0400002A RID: 42
		public readonly int Length;
	}
}
