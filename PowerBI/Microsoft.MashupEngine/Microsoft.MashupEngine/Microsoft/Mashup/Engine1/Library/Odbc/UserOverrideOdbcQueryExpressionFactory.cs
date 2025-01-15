using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000680 RID: 1664
	internal sealed class UserOverrideOdbcQueryExpressionFactory : OdbcQueryExpressionVisitorFactory
	{
		// Token: 0x0600343D RID: 13373 RVA: 0x000A7FF1 File Offset: 0x000A61F1
		private UserOverrideOdbcQueryExpressionFactory(OdbcSqlExpressionGenerator generator)
		{
			this.generator = generator;
		}

		// Token: 0x0600343E RID: 13374 RVA: 0x000A8000 File Offset: 0x000A6200
		public override OdbcQueryExpressionVisitor New(OdbcDataSource dataSource, IList<SelectItem> selectItems, OdbcQueryColumnInfo[] columnInfos, bool allowAggregates, bool softNumbers, bool tolerateConcatOverflow, int[] groupKey)
		{
			return new OdbcSqlExpressionGeneratorQueryExpressionVisitor(this.generator, dataSource, selectItems, columnInfos, allowAggregates, softNumbers, tolerateConcatOverflow, groupKey);
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x000A8018 File Offset: 0x000A6218
		public static OdbcQueryExpressionVisitorFactory New(OdbcSqlExpressionGenerator generator)
		{
			if (generator != null)
			{
				return new UserOverrideOdbcQueryExpressionFactory(generator);
			}
			return OdbcQueryExpressionVisitorFactory.Instance;
		}

		// Token: 0x04001778 RID: 6008
		private readonly OdbcSqlExpressionGenerator generator;
	}
}
