using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Detection.RichDataTypes;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Json;
using Microsoft.ProgramSynthesis.Split.Text;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Util;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics
{
	// Token: 0x02001AC5 RID: 6853
	public static class Semantics
	{
		// Token: 0x0600E2B9 RID: 58041 RVA: 0x00302184 File Offset: 0x00300384
		private static object CastAsType(object cellValue, IRichDataType richDataType)
		{
			string text = cellValue as string;
			if (text == null)
			{
				return cellValue;
			}
			return richDataType.MaybeCastAsType(text).OrElse(null);
		}

		// Token: 0x0600E2BA RID: 58042 RVA: 0x003021AC File Offset: 0x003003AC
		public static ITable<object> DropColumn(ITable<object> inputTable, string columnName, DropCondition dropCondition)
		{
			int? num = inputTable.ColumnNames.IndexOf(columnName);
			if (num != null)
			{
				int columnIndex = num.GetValueOrDefault();
				Func<object, int, bool> <>9__2;
				return new Table<object>(inputTable.ColumnNames.Where((string _, int i) => i != columnIndex).ToList<string>(), inputTable.Rows.Select(delegate(IEnumerable<object> row)
				{
					Func<object, int, bool> func;
					if ((func = <>9__2) == null)
					{
						func = (<>9__2 = (object _, int i) => i != columnIndex);
					}
					return row.Where(func);
				}), null);
			}
			return null;
		}

		// Token: 0x0600E2BB RID: 58043 RVA: 0x00302220 File Offset: 0x00300420
		private static IEnumerable<object> ReplaceRowCell(IEnumerable<object> row, int columnIndex, object newCellValue)
		{
			return row.Select(delegate(object cellValue, int i)
			{
				if (i == columnIndex)
				{
					return newCellValue;
				}
				return cellValue;
			});
		}

		// Token: 0x0600E2BC RID: 58044 RVA: 0x00302253 File Offset: 0x00300453
		private static IEnumerable<object> GetColumn(ITable<object> inputTable, string sourceColumnName, bool isMixedColumn)
		{
			if (!isMixedColumn)
			{
				return inputTable.Column(sourceColumnName);
			}
			return inputTable.Column(sourceColumnName).ToCustomString();
		}

		// Token: 0x0600E2BD RID: 58045 RVA: 0x0030226C File Offset: 0x0030046C
		public static ITable<object> CastColumn(ITable<object> inputTable, string sourceColumnName, IRichDataType richDataType, bool isMixedColumn)
		{
			int? num = inputTable.ColumnNames.IndexOf(sourceColumnName);
			if (num != null)
			{
				int columnIndex = num.GetValueOrDefault();
				return new Table<object>(inputTable.ColumnNames, inputTable.Rows.Zip(from cellValue in Semantics.GetColumn(inputTable, sourceColumnName, isMixedColumn)
					select Semantics.CastAsType(cellValue, richDataType), (IEnumerable<object> row, object newCell) => Semantics.ReplaceRowCell(row, columnIndex, newCell)), null);
			}
			return null;
		}

		// Token: 0x0600E2BE RID: 58046 RVA: 0x003022EC File Offset: 0x003004EC
		public static IEnumerable<StringRegion> SelectColumnToSplit(ITable<object> inputTable, string columnName)
		{
			int? num = inputTable.ColumnNames.IndexOf(columnName);
			if (num != null)
			{
				int columnIndex = num.GetValueOrDefault();
				return inputTable.Rows.Select(delegate(IEnumerable<object> row)
				{
					string text = row.ElementAt(columnIndex) as string;
					if (text != null)
					{
						return SplitSession.CreateStringRegion(text);
					}
					return null;
				});
			}
			return null;
		}

		// Token: 0x0600E2BF RID: 58047 RVA: 0x0030233C File Offset: 0x0030053C
		public static ITable<object> AddSplitColumns(ITable<object> inputTable, string sourceColumnName, IEnumerable<IEnumerable<object>> newColumns)
		{
			Semantics.<>c__DisplayClass7_0 CS$<>8__locals1 = new Semantics.<>c__DisplayClass7_0();
			CS$<>8__locals1.inputTable = inputTable;
			Semantics.<>c__DisplayClass7_0 CS$<>8__locals2 = CS$<>8__locals1;
			IEnumerable<object> enumerable = newColumns.FirstOrDefault((IEnumerable<object> newCells) => newCells != null);
			CS$<>8__locals2.numNewColumns = ((enumerable != null) ? enumerable.Count<object>() : 0);
			CS$<>8__locals1.outputPrefix = sourceColumnName + "_";
			while (CS$<>8__locals1.inputTable.ColumnNames.Any((string name) => name.StartsWith(CS$<>8__locals1.outputPrefix)))
			{
				CS$<>8__locals1.outputPrefix += "_";
			}
			return new Table<object>(CS$<>8__locals1.inputTable.ColumnNames.Concat(from i in Enumerable.Range(1, CS$<>8__locals1.numNewColumns)
				select CS$<>8__locals1.outputPrefix + i.ToString()).ToList<string>(), CS$<>8__locals1.inputTable.Rows.Zip(newColumns, delegate(IEnumerable<object> row, IEnumerable<object> newCells)
			{
				int num = CS$<>8__locals1.inputTable.ColumnNames.Count<string>();
				int num2 = row.Count<object>();
				int num3 = num - num2;
				if (num3 > 0)
				{
					row = row.Concat(Enumerable.Repeat<object>(null, num3));
				}
				IEnumerable<object> enumerable2 = row;
				IEnumerable<object> enumerable3;
				if (newCells == null)
				{
					enumerable3 = null;
				}
				else
				{
					enumerable3 = newCells.Select(delegate(object cell)
					{
						SplitCell splitCell = cell as SplitCell;
						if (splitCell == null)
						{
							return cell;
						}
						StringRegion cellValue = splitCell.CellValue;
						if (cellValue == null)
						{
							return null;
						}
						return cellValue.Value;
					});
				}
				return enumerable2.Concat(enumerable3.ExtendToLength(CS$<>8__locals1.numNewColumns, null)).ToList<object>();
			}).ToList<List<object>>(), null);
		}

		// Token: 0x0600E2C0 RID: 58048 RVA: 0x00302430 File Offset: 0x00300630
		public static ITable<object> LabelEncode(ITable<object> inputTable, string sourceColumnName)
		{
			IEnumerable<object> enumerable = inputTable.Column(sourceColumnName);
			Dictionary<object, int> encodedLabels = (from d in enumerable.Distinct<object>()
				where d != null
				select d).OrderBy((object x) => x.ToString(), StringComparer.Ordinal).Enumerate<object>().ToDictionary((Record<int, object> r) => r.Item2, (Record<int, object> r) => r.Item1);
			IEnumerable<int> enumerable2 = enumerable.Select(delegate(object cell)
			{
				if (cell != null)
				{
					return encodedLabels.GetOrDefault(cell, 0);
				}
				return -1;
			});
			string text = "_";
			string text2 = "label_encoded";
			while (inputTable.ColumnNames.Contains(sourceColumnName + text + text2))
			{
				text += "_";
			}
			string text3 = sourceColumnName + text + text2;
			return new Table<object>(inputTable.ColumnNames.AppendItem(text3).ToList<string>(), inputTable.Rows.Zip(enumerable2, (IEnumerable<object> row, int newCell) => row.AppendItem(newCell)), null);
		}

		// Token: 0x0600E2C1 RID: 58049 RVA: 0x00302580 File Offset: 0x00300780
		public static ITable<object> OneHotEncode(ITable<object> inputTable, string sourceColumnName)
		{
			IEnumerable<object> enumerable = inputTable.Column(sourceColumnName);
			int? num = inputTable.ColumnNames.IndexOf(sourceColumnName);
			if (num != null)
			{
				int sourceColumnIndexValue = num.GetValueOrDefault();
				IEnumerable<string> categories = (from d in enumerable
					where d != null
					select d.ToString()).Distinct<string>().OrderBy((string d) => d, StringComparer.Ordinal);
				string mid_sep = "_";
				while (inputTable.ColumnNames.Any((string colName) => colName.StartsWith(sourceColumnName + mid_sep)))
				{
					mid_sep += "_";
				}
				Func<object, int, bool> <>9__9;
				return new Table<object>(inputTable.ColumnNames.Where((string val, int i) => i != sourceColumnIndexValue).Concat(categories.Select((string category) => sourceColumnName + mid_sep + category)), inputTable.Rows.Zip(enumerable.Select((object cell) => categories.Select((object category) => (cell != null && cell.Equals(category) > false) ? 1 : 0)), delegate(IEnumerable<object> row, IEnumerable<object> newCells)
				{
					Func<object, int, bool> func;
					if ((func = <>9__9) == null)
					{
						func = (<>9__9 = (object val, int i) => i != sourceColumnIndexValue);
					}
					return row.Where(func).Concat(newCells);
				}), null);
			}
			return null;
		}

		// Token: 0x0600E2C2 RID: 58050 RVA: 0x003026F0 File Offset: 0x003008F0
		public static ITable<object> MultiLabelBinarizer(ITable<object> inputTable, string sourceColumnName, string delimiter)
		{
			IEnumerable<object> enumerable = inputTable.Column(sourceColumnName);
			int? num = inputTable.ColumnNames.IndexOf(sourceColumnName);
			if (num != null)
			{
				int sourceColumnIndexValue = num.GetValueOrDefault();
				IEnumerable<string> categories = enumerable.Where((object d) => d != null).SelectMany((object d) => from a in d.ToString().Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries)
					select a.Trim() into a
					where a.Length > 0
					select a).Distinct<string>()
					.OrderBy((string d) => d, StringComparer.Ordinal);
				string mid_sep = "_";
				while (inputTable.ColumnNames.Any((string colName) => colName.StartsWith(sourceColumnName + mid_sep)))
				{
					mid_sep += "_";
				}
				Func<object, int, bool> <>9__14;
				return new Table<object>(inputTable.ColumnNames.Where((string val, int i) => i != sourceColumnIndexValue).Concat(categories.Select((string category) => sourceColumnName + mid_sep + category)), inputTable.Rows.Zip(from cellLabels in enumerable.Select(delegate(object cell)
					{
						string text = cell as string;
						if (text == null)
						{
							return new List<string>();
						}
						return (from v in text.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries)
							select v.Trim() into a
							where a.Length > 0
							select a).ToList<string>();
					})
					select from category in categories
						select (cellLabels.Contains(category) > false) ? 1 : 0, delegate(IEnumerable<object> row, IEnumerable<object> newCells)
				{
					Func<object, int, bool> func;
					if ((func = <>9__14) == null)
					{
						func = (<>9__14 = (object val, int i) => i != sourceColumnIndexValue);
					}
					return row.Where(func).Concat(newCells);
				}), null);
			}
			return null;
		}

		// Token: 0x0600E2C3 RID: 58051 RVA: 0x00302864 File Offset: 0x00300A64
		public static ITable<object> FillMissingValues(ITable<object> inputTable, string sourceColumnName, object fillValue, IEnumerable<object> missingValueMarkers, FillMethod fillMethod)
		{
			IEnumerable<object> enumerable = inputTable.Column(sourceColumnName);
			if (fillMethod != FillMethod.None && fillValue != null)
			{
				enumerable = enumerable.Select(delegate(object d)
				{
					if (!missingValueMarkers.Contains(d))
					{
						return d;
					}
					return fillValue;
				});
			}
			int? num = inputTable.ColumnNames.IndexOf(sourceColumnName);
			if (num != null)
			{
				int columnIndex = num.GetValueOrDefault();
				return new Table<object>(inputTable.ColumnNames, inputTable.Rows.Zip(enumerable, (IEnumerable<object> row, object newCell) => Semantics.ReplaceRowCell(row, columnIndex, newCell)), null);
			}
			return null;
		}

		// Token: 0x0600E2C4 RID: 58052 RVA: 0x003028F8 File Offset: 0x00300AF8
		public static ITable<object> DropRows(ITable<object> inputTable, DropCondition dropCondition)
		{
			ITable<object> table = inputTable;
			MissingCondition missingCondition = dropCondition as MissingCondition;
			if (missingCondition != null)
			{
				int missingValueCount = (int)Math.Ceiling(missingCondition.MissingValueFraction * (double)inputTable.ColumnNames.Count<string>());
				Func<object, bool> <>9__1;
				table = new Table<object>(table.ColumnNames, table.Rows.Where(delegate(IEnumerable<object> row)
				{
					Func<object, bool> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = delegate(object cell)
						{
							if (!Utilities.IsNa(cell))
							{
								string text = cell as string;
								return text != null && ((missingCondition.MissingValueTypes.HasFlag(MissingValueType.WhiteSpace) && string.IsNullOrWhiteSpace(text)) || (missingCondition.MissingValueTypes.HasFlag(MissingValueType.EmptyString) && text == string.Empty) || (missingCondition.MissingValueTypes.HasFlag(MissingValueType.NanString) && text == "NaN"));
							}
							return true;
						});
					}
					return row.Count(func) < missingValueCount;
				}), null);
			}
			if (dropCondition is DuplicateCondition)
			{
				table = new Table<object>(table.ColumnNames, table.Rows.Distinct(SequenceEquality<object>.Comparer), null);
			}
			OutlierCondition outlierCondition = dropCondition as OutlierCondition;
			if (outlierCondition != null)
			{
				Tuple<double, double> validBoundExclusive = outlierCondition.ValidBoundExclusive;
				int? num = inputTable.ColumnNames.IndexOf(outlierCondition.SourceColumnName);
				if (num != null)
				{
					int colIdxInt = num.GetValueOrDefault();
					table = new Table<object>(table.ColumnNames, table.Rows.Where(delegate(IEnumerable<object> row)
					{
						object obj = row.ElementAt(colIdxInt);
						double? num3;
						if (obj != null)
						{
							if (obj is double)
							{
								double num2 = (double)obj;
								num3 = new double?(num2);
							}
							else
							{
								num3 = (Utilities.IsNumeric(obj) ? new double?(Convert.ToDouble(obj)) : null);
							}
						}
						else
						{
							num3 = null;
						}
						double? num4 = num3;
						if (num4 != null)
						{
							double valueOrDefault = num4.GetValueOrDefault();
							return valueOrDefault > validBoundExclusive.Item1 && valueOrDefault < validBoundExclusive.Item2;
						}
						return true;
					}), null);
				}
			}
			return table;
		}

		// Token: 0x0600E2C5 RID: 58053 RVA: 0x00302A10 File Offset: 0x00300C10
		public static ITable<object> AddColumnsFromJson(ITable<object> inputTable, string sourceColumnName, Microsoft.ProgramSynthesis.Extraction.Json.Program program)
		{
			IEnumerable<object> enumerable = inputTable.Column(sourceColumnName);
			string text = "[" + string.Join(",", enumerable.Cast<string>()) + "]";
			ITable<string> table = program.Run(text);
			List<string> existingColumnNames = inputTable.ColumnNames.ToList<string>();
			return new Table<object>(inputTable.ColumnNames.Concat(table.ColumnNames.Select((string c) => Utilities.Uniquify(c, existingColumnNames))), inputTable.Rows.Zip(table.Rows, (IEnumerable<object> row, IEnumerable<string> newCells) => row.Concat(newCells).ToList<object>()).ToList<List<object>>(), null);
		}

		// Token: 0x02001AC6 RID: 6854
		private class TableIRowMapper<T>
		{
			// Token: 0x0600E2C6 RID: 58054 RVA: 0x00302AC4 File Offset: 0x00300CC4
			internal TableIRowMapper(ITable<T> table)
			{
				this.Table = table;
				this.ColumnIndexLookup = this.Table.ColumnNames.Enumerate<string>().ToDictionary((Record<int, string> t) => t.Item2, (Record<int, string> t) => t.Item1);
			}

			// Token: 0x170025EF RID: 9711
			// (get) Token: 0x0600E2C7 RID: 58055 RVA: 0x00302B37 File Offset: 0x00300D37
			public ITable<T> Table { get; }

			// Token: 0x170025F0 RID: 9712
			// (get) Token: 0x0600E2C8 RID: 58056 RVA: 0x00302B3F File Offset: 0x00300D3F
			public IReadOnlyDictionary<string, int> ColumnIndexLookup { get; }

			// Token: 0x170025F1 RID: 9713
			// (get) Token: 0x0600E2C9 RID: 58057 RVA: 0x00302B47 File Offset: 0x00300D47
			public IEnumerable<IRow> IRows
			{
				get
				{
					return this.Table.Rows.Select((IEnumerable<T> row) => new Semantics.TableIRowMapper<T>.Row(row, this.Table.ColumnNames, this.ColumnIndexLookup));
				}
			}

			// Token: 0x02001AC7 RID: 6855
			private class Row : IRow, IEquatable<IRow>
			{
				// Token: 0x0600E2CB RID: 58059 RVA: 0x00302B7E File Offset: 0x00300D7E
				public Row(IEnumerable<T> rowData, IEnumerable<string> columnNames, IReadOnlyDictionary<string, int> columnIndexLookup)
				{
					this.RowData = rowData;
					this.ColumnNames = columnNames;
					this.ColumnIndexLookup = columnIndexLookup;
				}

				// Token: 0x170025F2 RID: 9714
				// (get) Token: 0x0600E2CC RID: 58060 RVA: 0x00302B9B File Offset: 0x00300D9B
				public IEnumerable<string> ColumnNames { get; }

				// Token: 0x170025F3 RID: 9715
				// (get) Token: 0x0600E2CD RID: 58061 RVA: 0x00302BA3 File Offset: 0x00300DA3
				private IEnumerable<T> RowData { get; }

				// Token: 0x170025F4 RID: 9716
				// (get) Token: 0x0600E2CE RID: 58062 RVA: 0x00302BAB File Offset: 0x00300DAB
				private IReadOnlyDictionary<string, int> ColumnIndexLookup { get; }

				// Token: 0x0600E2CF RID: 58063 RVA: 0x00302BB4 File Offset: 0x00300DB4
				public bool Equals(IRow other)
				{
					Semantics.TableIRowMapper<T>.Row row = other as Semantics.TableIRowMapper<T>.Row;
					return row != null && this.ColumnIndexLookup == row.ColumnIndexLookup && this.RowData.SequenceEqual(row.RowData);
				}

				// Token: 0x0600E2D0 RID: 58064 RVA: 0x00302BEC File Offset: 0x00300DEC
				public bool TryGetValue(string columnName, out object value)
				{
					int num;
					if (this.ColumnIndexLookup.TryGetValue(columnName, out num))
					{
						value = this.RowData.ElementAt(num);
						return true;
					}
					value = null;
					return false;
				}
			}
		}
	}
}
