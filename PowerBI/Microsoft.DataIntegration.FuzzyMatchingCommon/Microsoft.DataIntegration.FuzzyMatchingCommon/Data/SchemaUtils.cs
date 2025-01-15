using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data.Sql;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Data
{
	// Token: 0x0200004F RID: 79
	[Serializable]
	public static class SchemaUtils
	{
		// Token: 0x06000274 RID: 628 RVA: 0x00013E44 File Offset: 0x00012044
		public static string CommaSeparatedListOfColumnNames(DataTable schemaTable)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < schemaTable.Rows.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(SqlName.DelimitElement(schemaTable.Rows[i][SchemaTableColumn.ColumnName].ToString()));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00013EAC File Offset: 0x000120AC
		public static DataRow FindColumnSchemaRow(DataTable schemaTable, string columnName, bool ignoreCase)
		{
			DataRow dataRow = null;
			if (!SchemaUtils.TryFindColumnSchemaRow(schemaTable, columnName, ignoreCase, out dataRow))
			{
				throw new ArgumentException(string.Format("A column with name '{0}' was not found in the schema table.", columnName));
			}
			return dataRow;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00013EDC File Offset: 0x000120DC
		public static bool TryFindColumnSchemaRow(DataTable schemaTable, string columnName, bool ignoreCase, out DataRow dataRow)
		{
			foreach (object obj in schemaTable.Rows)
			{
				DataRow dataRow2 = (DataRow)obj;
				if (string.Compare(dataRow2[SchemaTableColumn.ColumnName].ToString(), columnName, ignoreCase) == 0)
				{
					dataRow = dataRow2;
					return true;
				}
			}
			dataRow = null;
			return false;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00013F54 File Offset: 0x00012154
		public static int[] GetColumnIndexes(DataTable schemaTable, params string[] columnNames)
		{
			int[] array = new int[columnNames.Length];
			for (int i = 0; i < array.Length; i++)
			{
				DataRow dataRow = SchemaUtils.FindColumnSchemaRow(schemaTable, columnNames[i], false);
				array[i] = (int)dataRow[SchemaTableColumn.ColumnOrdinal];
			}
			return array;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00013F98 File Offset: 0x00012198
		public static DataRow GetRow(DataTable schemaTable, string columnName)
		{
			DataRow dataRow;
			if (!SchemaUtils.TryGetRow(schemaTable, columnName, out dataRow))
			{
				throw new ArgumentException(string.Format("Column named '{0}' was not found in the schema table.", columnName));
			}
			return dataRow;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00013FC4 File Offset: 0x000121C4
		public static bool TryGetRow(DataTable schemaTable, string columnName, StringComparison comparisonType, out DataRow row)
		{
			row = null;
			foreach (object obj in schemaTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (string.Equals((string)dataRow[SchemaTableColumn.ColumnName], columnName, comparisonType))
				{
					row = dataRow;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0001403C File Offset: 0x0001223C
		public static bool TryGetRow(DataTable schemaTable, string columnName, out DataRow row)
		{
			return SchemaUtils.TryGetRow(schemaTable, columnName, 2, out row) || SchemaUtils.TryGetRow(schemaTable, columnName, 3, out row);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00014054 File Offset: 0x00012254
		public static bool TryGetRow(DataTable schemaTable, int columnOrdinal, out DataRow row)
		{
			row = null;
			if (columnOrdinal < schemaTable.Rows.Count)
			{
				row = schemaTable.Rows[columnOrdinal];
				if (Convert.ToInt32(row[SchemaTableColumn.ColumnOrdinal]) == columnOrdinal)
				{
					return true;
				}
				row = null;
			}
			foreach (object obj in schemaTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (Convert.ToInt32(dataRow[SchemaTableColumn.ColumnOrdinal]) == columnOrdinal)
				{
					row = dataRow;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000140FC File Offset: 0x000122FC
		public static DataRow FindColumnSchemaRow(DataTable schemaTable, int columnOrdinal)
		{
			DataRow dataRow;
			SchemaUtils.TryGetRow(schemaTable, columnOrdinal, out dataRow);
			return dataRow;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00014114 File Offset: 0x00012314
		public static int[] GetRowsByOrdinal(DataTable schema)
		{
			int[] array = new int[schema.Rows.Count];
			for (int i = 0; i < schema.Rows.Count; i++)
			{
				array[(int)schema.Rows[i][SchemaTableColumn.ColumnOrdinal]] = i;
			}
			return array;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00014168 File Offset: 0x00012368
		public static int FindIntegerKeyColumnIndex(DataTable schemaTable)
		{
			if (schemaTable.Columns.Contains(SchemaTableColumn.ColumnOrdinal) && schemaTable.Columns.Contains(SchemaTableColumn.DataType) && schemaTable.Columns.Contains(SchemaTableColumn.IsUnique) && schemaTable.Columns.Contains(SchemaTableColumn.IsKey))
			{
				DataTableReader dataTableReader = schemaTable.CreateDataReader();
				while (dataTableReader.Read())
				{
					if ((bool)dataTableReader[SchemaTableColumn.IsUnique] && (bool)dataTableReader[SchemaTableColumn.IsKey] && typeof(int) == (Type)dataTableReader[SchemaTableColumn.DataType])
					{
						return (int)dataTableReader[SchemaTableColumn.ColumnOrdinal];
					}
				}
			}
			return -1;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00014226 File Offset: 0x00012426
		public static string GenerateUniqueColumnName(DataTable schemaTable, string columnNamePrefix)
		{
			return SchemaUtils.GenerateUniqueColumnName(Enumerable.Select<DataRow, string>(schemaTable.Rows.AsEnumerable<DataRow>(), (DataRow r) => r[SchemaTableColumn.ColumnName] as string), columnNamePrefix);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0001425D File Offset: 0x0001245D
		public static string GenerateUniqueColumnName(DataColumnCollection columns, string columnNamePrefix)
		{
			return SchemaUtils.GenerateUniqueColumnName(Enumerable.Select<DataColumn, string>(columns.AsEnumerable<DataColumn>(), (DataColumn c) => c.ColumnName), columnNamePrefix);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00014290 File Offset: 0x00012490
		public static string GenerateUniqueColumnName(IEnumerable<string> columnNames, string columnNamePrefix)
		{
			string text = columnNamePrefix;
			int num = 1;
			bool flag;
			do
			{
				flag = true;
				foreach (string text2 in columnNames)
				{
					if (string.Compare(text2, text, true) == 0)
					{
						if (Regex.IsMatch(text, "\\\\(\\d+\\\\)$"))
						{
							text = Regex.Replace(text, "\\\\(\\d+\\\\)$", string.Format("({0})", num));
						}
						else
						{
							text = text + " (" + num.ToString() + ")";
						}
						num++;
						flag = false;
						break;
					}
				}
			}
			while (!flag);
			return text;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00014334 File Offset: 0x00012534
		public static void CreateIndexOnColumnOrdinal(DataTable schemaTable)
		{
			if (schemaTable.PrimaryKey != null && schemaTable.PrimaryKey.Length != 0 && (schemaTable.PrimaryKey.Length != 1 || schemaTable.PrimaryKey[0].ColumnName.CompareTo(SchemaTableColumn.ColumnOrdinal) != 0))
			{
				throw new InvalidOperationException("A different PrimaryKey already exists on the schemaTable.");
			}
			schemaTable.PrimaryKey = new DataColumn[] { schemaTable.Columns[SchemaTableColumn.ColumnOrdinal] };
		}

		// Token: 0x06000283 RID: 643 RVA: 0x000143A0 File Offset: 0x000125A0
		public static DataRow GetSchemaRow(DataTable schemaTable, int columnOrdinal)
		{
			if (schemaTable.PrimaryKey.Length == 1 && schemaTable.PrimaryKey[0].ColumnName.CompareTo(SchemaTableColumn.ColumnOrdinal) == 0)
			{
				return schemaTable.Rows.Find(columnOrdinal);
			}
			foreach (object obj in schemaTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (columnOrdinal == (int)dataRow[SchemaTableColumn.ColumnOrdinal])
				{
					return dataRow;
				}
			}
			throw new Exception(string.Format("Column with ordinal {0} was not found in the schema table.", columnOrdinal));
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00014458 File Offset: 0x00012658
		public static string GetDataTypeName(DataTable schemaTable, int columnOrdinal)
		{
			return SchemaUtils.GetSchemaRow(schemaTable, columnOrdinal)[SchemaTableColumn.DataType].ToString();
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00014470 File Offset: 0x00012670
		public static string GetName(DataTable schemaTable, int columnOrdinal)
		{
			return SchemaUtils.GetSchemaRow(schemaTable, columnOrdinal)[SchemaTableColumn.ColumnName] as string;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00014488 File Offset: 0x00012688
		public static int GetOrdinal(DataTable schemaTable, string columnName, bool ignoreCase)
		{
			foreach (object obj in schemaTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (string.Compare(dataRow[SchemaTableColumn.ColumnName] as string, columnName, ignoreCase) == 0)
				{
					return (int)dataRow[SchemaTableColumn.ColumnOrdinal];
				}
			}
			throw new IndexOutOfRangeException();
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00014510 File Offset: 0x00012710
		public static DataTable CreateSchemaTable(string schemaTableName)
		{
			DataTable dataTable = new DataTable(schemaTableName);
			dataTable.Columns.Add(SchemaTableColumn.ColumnName, typeof(string));
			dataTable.Columns.Add(SchemaTableColumn.ColumnOrdinal, typeof(int));
			dataTable.Columns.Add(SchemaTableColumn.ColumnSize, typeof(int));
			dataTable.Columns.Add(SchemaTableColumn.NumericPrecision, typeof(short));
			dataTable.Columns.Add(SchemaTableColumn.NumericScale, typeof(short));
			dataTable.Columns.Add(SchemaTableColumn.IsUnique, typeof(bool));
			dataTable.Columns.Add(SchemaTableColumn.IsKey, typeof(bool));
			dataTable.Columns.Add(SchemaTableOptionalColumn.BaseServerName, typeof(string));
			dataTable.Columns.Add(SchemaTableOptionalColumn.BaseCatalogName, typeof(string));
			dataTable.Columns.Add(SchemaTableColumn.BaseColumnName, typeof(string));
			dataTable.Columns.Add(SchemaTableColumn.BaseSchemaName, typeof(string));
			dataTable.Columns.Add(SchemaTableColumn.BaseTableName, typeof(string));
			dataTable.Columns.Add(SchemaTableColumn.DataType, typeof(Type));
			dataTable.Columns.Add(SchemaTableColumn.AllowDBNull, typeof(bool));
			dataTable.Columns.Add(SchemaTableColumn.ProviderType, typeof(int));
			dataTable.Columns.Add(SchemaTableColumn.IsAliased, typeof(bool));
			dataTable.Columns.Add(SchemaTableColumn.IsExpression, typeof(bool));
			dataTable.Columns.Add(SchemaTableOptionalColumn.IsAutoIncrement, typeof(bool));
			dataTable.Columns.Add(SchemaTableOptionalColumn.IsRowVersion, typeof(bool));
			dataTable.Columns.Add(SchemaTableOptionalColumn.IsHidden, typeof(bool));
			dataTable.Columns.Add(SchemaTableColumn.IsLong, typeof(bool));
			dataTable.Columns.Add(SchemaTableOptionalColumn.IsReadOnly, typeof(bool));
			dataTable.Columns.Add(SchemaTableOptionalColumn.ProviderSpecificDataType, typeof(object));
			dataTable.Columns.Add(SchemaTableColumn.NonVersionedProviderType, typeof(int));
			return dataTable;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000147AC File Offset: 0x000129AC
		public static DataTable CreateDataTableFromSchemaTable(DataTable schemaTable)
		{
			DataTable dataTable = new DataTable();
			foreach (object obj in schemaTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				dataTable.Columns.Add((string)dataRow[SchemaTableColumn.ColumnName], (Type)dataRow[SchemaTableColumn.DataType]);
			}
			return dataTable;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00014834 File Offset: 0x00012A34
		public static DataTable CloneAndSortSchema(DataTable schemaTable)
		{
			DataTable dataTable = schemaTable.Clone();
			foreach (DataRow dataRow in schemaTable.Select(null, SchemaTableColumn.ColumnOrdinal + " ASC"))
			{
				dataTable.Rows.Add(dataRow.ItemArray);
			}
			SchemaUtils.CreateIndexOnColumnOrdinal(dataTable);
			return dataTable;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0001488C File Offset: 0x00012A8C
		public static bool SchemaRowOrderMatchesColumnOrdinal(DataTable schemaTable)
		{
			if (schemaTable != null)
			{
				for (int i = 0; i < schemaTable.Rows.Count; i++)
				{
					if ((int)schemaTable.Rows[i][SchemaTableColumn.ColumnOrdinal] != i)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000148D4 File Offset: 0x00012AD4
		public static bool IsStringType(Type t)
		{
			return typeof(string) == t || typeof(char[]) == t || typeof(string) == t || typeof(StringExtent) == t || typeof(ArraySegment<char>) == t || typeof(ArraySegment32<char>) == t || typeof(ArraySegmentBuilder<char>) == t;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00014940 File Offset: 0x00012B40
		public static DbType TypeToDbType(Type t)
		{
			if (t == typeof(bool))
			{
				return 3;
			}
			if (t == typeof(byte))
			{
				return 2;
			}
			if (t == typeof(DateTime))
			{
				return 6;
			}
			if (t == typeof(decimal))
			{
				return 7;
			}
			if (t == typeof(double))
			{
				return 8;
			}
			if (t == typeof(Guid))
			{
				return 9;
			}
			if (t == typeof(short))
			{
				return 10;
			}
			if (t == typeof(int))
			{
				return 11;
			}
			if (t == typeof(long))
			{
				return 12;
			}
			if (t == typeof(sbyte))
			{
				return 14;
			}
			if (t == typeof(float))
			{
				return 15;
			}
			if (t == typeof(string))
			{
				return 16;
			}
			if (t == typeof(ushort))
			{
				return 18;
			}
			if (t == typeof(uint))
			{
				return 19;
			}
			if (t == typeof(ulong))
			{
				return 20;
			}
			throw new ArgumentException("Could not convert Type '" + t.ToString() + "' to DbType.");
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00014A54 File Offset: 0x00012C54
		public static int ComputeRecordLength(DataTable schemaTable)
		{
			int num = 0;
			foreach (object obj in schemaTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if ((int)dataRow[SchemaTableColumn.ColumnSize] > 0)
				{
					num += (int)dataRow[SchemaTableColumn.ColumnSize];
				}
				else
				{
					num += 8;
				}
			}
			return num;
		}
	}
}
