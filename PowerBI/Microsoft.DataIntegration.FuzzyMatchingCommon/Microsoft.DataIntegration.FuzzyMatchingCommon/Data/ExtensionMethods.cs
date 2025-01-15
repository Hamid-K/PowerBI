using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Text;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Data
{
	// Token: 0x0200004E RID: 78
	public static class ExtensionMethods
	{
		// Token: 0x0600026F RID: 623 RVA: 0x00013D03 File Offset: 0x00011F03
		public static IEnumerable<DataColumn> AsEnumerable(this DataColumnCollection dcc)
		{
			foreach (object obj in dcc)
			{
				yield return (DataColumn)obj;
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00013D13 File Offset: 0x00011F13
		public static IEnumerable<DataRow> AsEnumerable(this DataRowCollection drc)
		{
			foreach (object obj in drc)
			{
				yield return (DataRow)obj;
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00013D23 File Offset: 0x00011F23
		public static string ToCsv(this DataTable table, string delimiter = ",", bool includeHeader = true, string[] columnNames = null, int rowCount = 2147483647)
		{
			return table.ToSeparatedValues(delimiter, includeHeader, columnNames, rowCount);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00013D30 File Offset: 0x00011F30
		public static string ToSeparatedValues(this DataTable table, string delimiter = "\t", bool includeHeader = true, string[] columnNames = null, int rowCount = 2147483647)
		{
			int[] columnIndexes;
			if (columnNames == null)
			{
				columnIndexes = Enumerable.ToArray<int>(Enumerable.Range(0, table.Columns.Count));
			}
			else
			{
				columnIndexes = Enumerable.ToArray<int>(Enumerable.Select<string, int>(columnNames, (string c) => table.Columns.IndexOf(c)));
			}
			StringBuilder sb = new StringBuilder();
			if (includeHeader)
			{
				sb.AppendLine(Enumerable.Select<int, string>(columnIndexes, (int i) => table.Columns[i].ColumnName).ToSeparatedValues(delimiter));
			}
			Enumerable.Take<DataRow>(table.Rows.AsEnumerable<DataRow>(), rowCount).ForEach(delegate(DataRow r)
			{
				sb.AppendLine(Enumerable.Select<object, string>(Enumerable.Select<int, object>(columnIndexes, (int i) => r.ItemArray[i]), delegate(object v)
				{
					if (v != null)
					{
						return v.ToString();
					}
					return string.Empty;
				}).ToSeparatedValues(delimiter));
			});
			return sb.ToString();
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00013E04 File Offset: 0x00012004
		public static string GenerateUniqueColumnName(this DataTable table, string columnNamePrefix)
		{
			string text;
			using (DataTableReader dataTableReader = table.CreateDataReader())
			{
				text = SchemaUtils.GenerateUniqueColumnName(dataTableReader.GetSchemaTable(), columnNamePrefix);
			}
			return text;
		}
	}
}
