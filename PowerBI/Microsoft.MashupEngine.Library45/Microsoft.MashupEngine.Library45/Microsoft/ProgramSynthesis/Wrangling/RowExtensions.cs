using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000C6 RID: 198
	public static class RowExtensions
	{
		// Token: 0x06000473 RID: 1139 RVA: 0x0000FB0C File Offset: 0x0000DD0C
		public static object Get(this IRow row, string columnName)
		{
			object obj;
			if (!row.TryGetValue(columnName, out obj))
			{
				throw new KeyNotFoundException(FormattableString.Invariant(FormattableStringFactory.Create("row does not have column {0}.", new object[] { columnName })));
			}
			return obj;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000FB44 File Offset: 0x0000DD44
		public static IEnumerable<object> Values(this IRow row)
		{
			IEnumerable<string> columnNames = row.ColumnNames;
			if (columnNames == null)
			{
				return null;
			}
			return from i in columnNames
				orderby i
				select i into name
				select row.Get(name);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000FBA4 File Offset: 0x0000DDA4
		public static IEnumerable<KeyValuePair<string, object>> KeyValuePairs(this IRow row)
		{
			IEnumerable<string> columnNames = row.ColumnNames;
			if (columnNames == null)
			{
				return null;
			}
			return from i in columnNames
				orderby i
				select i into columnName
				select KVP.Create<string, object>(columnName, row.Get(columnName));
		}
	}
}
