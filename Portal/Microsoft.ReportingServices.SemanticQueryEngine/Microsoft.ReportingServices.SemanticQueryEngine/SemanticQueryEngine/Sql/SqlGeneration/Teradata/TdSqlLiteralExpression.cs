using System;
using System.Globalization;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.Teradata
{
	// Token: 0x0200003E RID: 62
	internal sealed class TdSqlLiteralExpression : SqlLiteralExpression
	{
		// Token: 0x0600029F RID: 671 RVA: 0x0000C6E8 File Offset: 0x0000A8E8
		internal TdSqlLiteralExpression(IQPExpressionInfo qpInfo, TdSqlBatch sqlBatch)
			: base(qpInfo, sqlBatch)
		{
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000C6F2 File Offset: 0x0000A8F2
		protected override ISqlSnippet CreateBasicSqlSnippetForString(string str)
		{
			str = str.Replace("'", "''");
			return new SqlStringSnippet("'{0}'", new object[] { str });
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000C71A File Offset: 0x0000A91A
		protected override ISqlSnippet CreateBasicSqlSnippetForDateTime(DateTime dateTime)
		{
			return TdSqlFunctionExpression.CastAsTimestamp(new SqlStringSnippet("'{0}'", new object[] { dateTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff", CultureInfo.InvariantCulture) }));
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000C748 File Offset: 0x0000A948
		protected override ISqlSnippet CreateBasicSqlSnippetForTime(TimeSpan time)
		{
			DateTime dateTime = DateTime.MinValue + time;
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CastOpenParenSnippet,
				new SqlStringSnippet("'{0}'", new object[] { dateTime.ToString("HH:mm:ss.FFFFFF", CultureInfo.InvariantCulture) }),
				TdSqlFunctionExpression.AsTime6CloseParenSnippet
			});
		}
	}
}
