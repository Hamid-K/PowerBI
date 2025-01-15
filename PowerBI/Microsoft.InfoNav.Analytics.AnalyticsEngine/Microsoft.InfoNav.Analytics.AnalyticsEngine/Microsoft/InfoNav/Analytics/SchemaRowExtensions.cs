using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics
{
	// Token: 0x0200001D RID: 29
	internal static class SchemaRowExtensions
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00003420 File Offset: 0x00001620
		internal static IEnumerable<IColumn> GetColumns(this ISchemaRow schemaRow)
		{
			int columnCount = schemaRow.Count;
			int num;
			for (int i = 0; i < columnCount; i = num + 1)
			{
				yield return schemaRow.GetColumn(i);
				num = i;
			}
			yield break;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003430 File Offset: 0x00001630
		internal static string MakeUniqueName(this ISchemaRow schemaRow, string candidateName, HashSet<string> otherUsedNames = null)
		{
			IEnumerable<string> enumerable = (from c in schemaRow.GetColumns()
				select c.Name).Concat(otherUsedNames.EmptyIfNull<string>());
			return StringUtil.MakeUniqueName(candidateName, new HashSet<string>(enumerable, StringComparer.Ordinal));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003484 File Offset: 0x00001684
		internal static void ValidateRequiredRole(this ISchemaRow schemaRow, string roleName, out int[] roleIndexes, Func<DataType, bool> typeConstraint = null, bool findAll = false)
		{
			List<IColumn> list;
			List<int> list2;
			if (!schemaRow.TryGetRoleColumns(roleName, out list, out list2, findAll))
			{
				throw new TransformException(StringUtil.FormatInvariant("InputSchema is missing role {0}", roleName));
			}
			if (typeConstraint != null)
			{
				foreach (IColumn column in list)
				{
					if (!typeConstraint(column.DataType))
					{
						throw new TransformException(StringUtil.FormatInvariant("Role {0} has invalid type", roleName));
					}
				}
			}
			roleIndexes = list2.ToArray();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003518 File Offset: 0x00001718
		internal static void ValidateColumnsHasValidType(this ISchemaRow schemaRow, string roleName, IReadOnlyList<int> columnIndexes, Func<DataType, bool> typeConstraint)
		{
			foreach (int num in columnIndexes)
			{
				IColumn column = schemaRow.GetColumn(num);
				if (!typeConstraint(column.DataType))
				{
					throw new TransformException(StringUtil.FormatInvariant("Role {0} has invalid type", roleName));
				}
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003580 File Offset: 0x00001780
		internal static int ValidateRequiredRole(this ISchemaRow schemaRow, string roleName, Func<DataType, bool> typeConstraint = null)
		{
			int[] array;
			schemaRow.ValidateRequiredRole(roleName, out array, typeConstraint, false);
			return array[0];
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000359C File Offset: 0x0000179C
		internal static void ValidateAbsentRole(this ISchemaRow schemaRow, string roleName)
		{
			List<IColumn> list;
			List<int> list2;
			if (schemaRow.TryGetRoleColumns(roleName, out list, out list2, false))
			{
				throw new TransformException(StringUtil.FormatInvariant("InputSchema contains unexpected role {0}", roleName));
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000035C8 File Offset: 0x000017C8
		private static bool TryGetRoleColumns(this ISchemaRow schemaRow, string roleName, out List<IColumn> columns, out List<int> columnIndexes, bool findAll = false)
		{
			return SchemaRowExtensions.TryFindColumns(schemaRow, (IColumn r) => string.Equals(r.Role, roleName, StringComparison.Ordinal), out columns, out columnIndexes, findAll);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000035F8 File Offset: 0x000017F8
		private static bool TryFindColumns(ISchemaRow schemaRow, Func<IColumn, bool> columnSelector, out List<IColumn> columns, out List<int> columnIndexes, bool findAll = false)
		{
			int count = schemaRow.Count;
			columns = new List<IColumn>(count);
			columnIndexes = new List<int>(count);
			for (int i = 0; i < count; i++)
			{
				IColumn column = schemaRow.GetColumn(i);
				if (columnSelector(column))
				{
					columns.Add(column);
					columnIndexes.Add(i);
					if (!findAll)
					{
						return true;
					}
				}
			}
			return columnIndexes.Count > 0;
		}

		// Token: 0x0400008D RID: 141
		private static readonly int[] EmptyArray = new int[0];
	}
}
