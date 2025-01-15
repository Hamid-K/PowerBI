using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp.Schema
{
	// Token: 0x0200009A RID: 154
	internal static class SchemaUtils
	{
		// Token: 0x060004B5 RID: 1205 RVA: 0x0000FFB8 File Offset: 0x0000E1B8
		[NullableContext(1)]
		public static bool IsListOrMap(Node[] schemaNodes)
		{
			if (schemaNodes.Length < 2)
			{
				return false;
			}
			Node node = schemaNodes[0];
			Node node2 = schemaNodes[1];
			bool flag2;
			using (LogicalType logicalType = node.LogicalType)
			{
				using (LogicalType logicalType2 = node2.LogicalType)
				{
					bool flag = node is GroupNode;
					if (flag)
					{
						flag2 = logicalType is ListLogicalType || logicalType is MapLogicalType;
						flag = flag2;
					}
					bool flag3 = flag;
					if (flag3)
					{
						Repetition repetition = node.Repetition;
						flag2 = repetition <= Repetition.Optional;
						flag3 = flag2;
					}
					flag2 = flag3 && node2 is GroupNode && logicalType2 is NoneLogicalType && node2.Repetition == Repetition.Repeated;
				}
			}
			return flag2;
		}
	}
}
