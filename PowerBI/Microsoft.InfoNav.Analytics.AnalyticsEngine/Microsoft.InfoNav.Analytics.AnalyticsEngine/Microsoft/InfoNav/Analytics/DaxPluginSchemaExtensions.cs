using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AnalysisServices.Dax.ExtensionInterfaces;
using Microsoft.InfoNav.Analytics.Clustering;

namespace Microsoft.InfoNav.Analytics
{
	// Token: 0x02000015 RID: 21
	internal static class DaxPluginSchemaExtensions
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002D54 File Offset: 0x00000F54
		internal static Dictionary<string, List<string>> GetColumnNamesByRoles(this IEnumerable<IReadOnlyRow> roleMapping, ITracer tracer, bool includeNullRole = false)
		{
			Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>(TransformNameComparer.Instance);
			foreach (IReadOnlyRow readOnlyRow in roleMapping)
			{
				string text;
				string text2;
				readOnlyRow.ParseMappingRole(tracer, out text, out text2, includeNullRole);
				dictionary.Add(text2, text, 4);
			}
			return dictionary;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002DB4 File Offset: 0x00000FB4
		internal static Dictionary<string, List<int>> GetColumnIndexesByRoles(this IReadOnlyRowSchema rowSchema, IEnumerable<IReadOnlyRow> roleMapping, ITracer tracer, bool includeNullRole = false)
		{
			Dictionary<string, List<int>> dictionary = new Dictionary<string, List<int>>(TransformNameComparer.Instance);
			foreach (IReadOnlyRow readOnlyRow in roleMapping)
			{
				string text;
				string text2;
				readOnlyRow.ParseMappingRole(tracer, out text, out text2, includeNullRole);
				int num = rowSchema.IndexOf(text);
				if (num == -1)
				{
					tracer.TraceError("DaxPluginSchemaExtensions: Column {0} does not exist", new object[] { text });
					Utils.ThrowExtensionException(ClusteringErrorType.NonExistingColumn);
				}
				dictionary.Add(text2, num, 4);
			}
			return dictionary;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002E40 File Offset: 0x00001040
		internal static IRow CopyColumns(this IRowSchema rowSchema, IReadOnlyRow dataRow, int[] columnIndexes)
		{
			IRow row = rowSchema.CreateRow();
			for (int i = 0; i < columnIndexes.Length; i++)
			{
				int num = columnIndexes[i];
				row[i] = dataRow[num];
			}
			return row;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002E78 File Offset: 0x00001078
		internal static bool AreAllNumeric(this IReadOnlyRowSchema rowSchema, int[] columnIndexes)
		{
			foreach (int num in columnIndexes)
			{
				if (!rowSchema[num].DataType.IsNumeric())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002EAF File Offset: 0x000010AF
		internal static IColumn Create(this IColumnFactory columnFactory, string columnName, DbType dataType)
		{
			IColumn column = columnFactory.Create();
			column.Name = columnName;
			column.DataType = dataType;
			return column;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002EC8 File Offset: 0x000010C8
		internal static bool AreAllBlanks(this IReadOnlyRow dataRow, int[] columnIndexes)
		{
			foreach (int num in columnIndexes)
			{
				if (dataRow[num] != null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002EF8 File Offset: 0x000010F8
		private static void ParseMappingRole(this IReadOnlyRow dataRow, ITracer tracer, out string columnName, out string columnRole, bool includeNullRole = false)
		{
			if (dataRow.Count < 2)
			{
				tracer.TraceError("DaxPluginSchemaExtensions: The role mapping table should have 2 columns");
				Utils.ThrowExtensionException(ClusteringErrorType.InvalidRoleMappingTable);
			}
			columnName = dataRow[0] as string;
			if (string.IsNullOrEmpty(columnName))
			{
				tracer.TraceError("DaxPluginSchemaExtensions: Missing mapping column name");
				Utils.ThrowExtensionException(ClusteringErrorType.MissingMappingColumnName);
			}
			columnRole = dataRow[1] as string;
			if (string.IsNullOrEmpty(columnRole))
			{
				if (includeNullRole)
				{
					columnRole = "NULL";
					return;
				}
				tracer.TraceError("DaxPluginSchemaExtensions: Missing mapping column role");
				Utils.ThrowExtensionException(ClusteringErrorType.MissingMappingColumnRole);
			}
		}

		// Token: 0x0400006D RID: 109
		private const int ColumnNameIndex = 0;

		// Token: 0x0400006E RID: 110
		private const int RoleNameIndex = 1;
	}
}
