using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000643 RID: 1603
	internal class OdbcQueryExpressionVisitorFactory
	{
		// Token: 0x060032FC RID: 13052 RVA: 0x000A3747 File Offset: 0x000A1947
		public virtual OdbcQueryExpressionVisitor New(OdbcDataSource dataSource, IList<SelectItem> selectItems, OdbcQueryColumnInfo[] columnInfos, bool allowAggregates, bool softNumbers, bool tolerateConcatOverflow, int[] groupKey)
		{
			return new OdbcQueryExpressionVisitor(dataSource, selectItems, columnInfos, allowAggregates, softNumbers, tolerateConcatOverflow, groupKey);
		}

		// Token: 0x040016B1 RID: 5809
		public static OdbcQueryExpressionVisitorFactory Instance = new OdbcQueryExpressionVisitorFactory();
	}
}
