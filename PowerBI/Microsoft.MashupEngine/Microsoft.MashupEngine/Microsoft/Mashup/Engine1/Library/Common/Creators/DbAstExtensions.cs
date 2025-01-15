using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;

namespace Microsoft.Mashup.Engine1.Library.Common.Creators
{
	// Token: 0x02001197 RID: 4503
	internal static class DbAstExtensions
	{
		// Token: 0x060076F3 RID: 30451 RVA: 0x0019D0A8 File Offset: 0x0019B2A8
		public static SelectItem GetBestMatch(this IList<SelectItem> selectItems, ColumnReference column)
		{
			SelectItem selectItem = null;
			if (column != null)
			{
				foreach (SelectItem selectItem2 in selectItems)
				{
					if (selectItem2.Alias == null && column.Name.Equals(selectItem2.Name))
					{
						return selectItem2;
					}
					ColumnReference columnReference = selectItem2.Expression as ColumnReference;
					if (selectItem2.Alias != null && columnReference != null && columnReference.Name.Equals(column.Name) && (column.Qualifier == null || columnReference.Qualifier == null || columnReference.Qualifier.Equals(column.Qualifier)))
					{
						selectItem = selectItem2;
					}
				}
				return selectItem;
			}
			return selectItem;
		}
	}
}
