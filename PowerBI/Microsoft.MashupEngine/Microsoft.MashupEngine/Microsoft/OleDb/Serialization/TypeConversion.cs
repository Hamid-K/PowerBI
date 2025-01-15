using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Serialization;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02000176 RID: 374
	internal sealed class TypeConversion
	{
		// Token: 0x0600071A RID: 1818 RVA: 0x0000C421 File Offset: 0x0000A621
		public TypeConversion(Type fromType, Type[] toTypes, ColumnConversion columnConversion)
		{
			this.fromType = fromType;
			this.toTypes = toTypes;
			this.columnConversion = columnConversion;
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0000C43E File Offset: 0x0000A63E
		public TypeConversion(Type fromType, Type[] toTypes, ValueConversion valueConversion)
		{
			this.fromType = fromType;
			this.toTypes = toTypes;
			this.valueConversion = valueConversion;
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0000C45B File Offset: 0x0000A65B
		public Type FromType
		{
			get
			{
				return this.fromType;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0000C463 File Offset: 0x0000A663
		public Type[] ToTypes
		{
			get
			{
				return this.toTypes;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0000C46B File Offset: 0x0000A66B
		public ColumnConversion ColumnConversion
		{
			get
			{
				return this.columnConversion;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0000C473 File Offset: 0x0000A673
		public ValueConversion ValueConversion
		{
			get
			{
				return this.valueConversion;
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0000C47C File Offset: 0x0000A67C
		public static Dictionary<int, TypeConversion> GetTypeConversions(TableSchema sourceSchemaTable, TableSchema targetSchemaTable, IList<TypeConversion> typeConversions)
		{
			Dictionary<int, TypeConversion> dictionary = new Dictionary<int, TypeConversion>();
			for (int i = 0; i < sourceSchemaTable.ColumnCount; i++)
			{
				SchemaColumn column = sourceSchemaTable.GetColumn(i);
				Type sourceType = column.DataType;
				Type targetType = targetSchemaTable.GetColumn(column.Name).DataType;
				TypeConversion typeConversion = typeConversions.Where((TypeConversion tc) => tc.FromType == sourceType && (tc.ToTypes == null || tc.ToTypes.Contains(targetType))).SingleOrDefault<TypeConversion>();
				if (typeConversion != null)
				{
					dictionary.Add(i, typeConversion);
				}
			}
			return dictionary;
		}

		// Token: 0x0400046D RID: 1133
		private readonly Type fromType;

		// Token: 0x0400046E RID: 1134
		private readonly Type[] toTypes;

		// Token: 0x0400046F RID: 1135
		private readonly ColumnConversion columnConversion;

		// Token: 0x04000470 RID: 1136
		private readonly ValueConversion valueConversion;
	}
}
