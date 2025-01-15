using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp.RowOriented
{
	// Token: 0x0200009C RID: 156
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public sealed class MapToColumnAttribute : Attribute
	{
		// Token: 0x060004B7 RID: 1207 RVA: 0x000100DC File Offset: 0x0000E2DC
		public MapToColumnAttribute(string columnName)
		{
			this.ColumnName = columnName;
		}

		// Token: 0x0400015F RID: 351
		public readonly string ColumnName;
	}
}
