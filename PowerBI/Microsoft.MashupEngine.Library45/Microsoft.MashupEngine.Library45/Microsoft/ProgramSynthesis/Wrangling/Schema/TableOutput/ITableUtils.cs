using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput
{
	// Token: 0x0200013A RID: 314
	public static class ITableUtils
	{
		// Token: 0x060006F6 RID: 1782 RVA: 0x00016590 File Offset: 0x00014790
		public static IEnumerable<T> Column<T>(this ITable<T> table, int columnIndex)
		{
			return table.Rows.Select((IEnumerable<T> row) => row.ElementAtOrDefault(columnIndex));
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x000165C4 File Offset: 0x000147C4
		public static IEnumerable<T> Column<T>(this ITable<T> table, string columnName)
		{
			int? num = table.ColumnNames.IndexOf(columnName);
			if (num != null)
			{
				int valueOrDefault = num.GetValueOrDefault();
				return table.Column(valueOrDefault);
			}
			return null;
		}
	}
}
