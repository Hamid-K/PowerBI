using System;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.Analytics
{
	// Token: 0x020000B9 RID: 185
	internal static class SchemaRowExtensions
	{
		// Token: 0x060004B9 RID: 1209 RVA: 0x0000E218 File Offset: 0x0000C418
		internal static bool TryGetColumn(this ISchemaRow schemaRow, string name, out IColumn column, out int colIdx)
		{
			return SchemaRowExtensions.TryFindFirstColumn(schemaRow, (IColumn r) => string.Equals(r.Name, name, StringComparison.Ordinal), out column, out colIdx);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000E248 File Offset: 0x0000C448
		internal static bool TryGetRoleColumn(this ISchemaRow schemaRow, string roleName, out IColumn roleColumn, out int roleIndex)
		{
			return SchemaRowExtensions.TryFindFirstColumn(schemaRow, (IColumn r) => string.Equals(r.Role, roleName, StringComparison.Ordinal), out roleColumn, out roleIndex);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000E278 File Offset: 0x0000C478
		internal static bool TryFindFirstColumn(ISchemaRow schemaRow, Func<IColumn, bool> predicate, out IColumn column, out int columnIndex)
		{
			int count = schemaRow.Count;
			for (int i = 0; i < count; i++)
			{
				column = schemaRow.GetColumn(i);
				if (predicate(column))
				{
					columnIndex = i;
					return true;
				}
			}
			column = null;
			columnIndex = -1;
			return false;
		}
	}
}
