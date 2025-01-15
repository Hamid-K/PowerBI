using System;
using System.Globalization;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.MSSQL
{
	// Token: 0x0200004B RID: 75
	internal sealed class MsSqlLiteralExpression : SqlLiteralExpression
	{
		// Token: 0x06000366 RID: 870 RVA: 0x0000C6E8 File Offset: 0x0000A8E8
		internal MsSqlLiteralExpression(IQPExpressionInfo qpInfo, SqlBatch sqlBatch)
			: base(qpInfo, sqlBatch)
		{
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000100B8 File Offset: 0x0000E2B8
		protected override ISqlSnippet CreateBasicSqlSnippetForString(string str)
		{
			str = str.Replace("'", "''");
			if (base.SqlBatch.IsStringInDatabaseCharset(str))
			{
				return new SqlStringSnippet("'{0}'", new object[] { str });
			}
			return new SqlStringSnippet("N'{0}'", new object[] { str });
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00010110 File Offset: 0x0000E310
		protected override ISqlSnippet CreateBasicSqlSnippetForDateTime(DateTime dateTime)
		{
			ISqlSnippet sqlSnippet = MsSqlFunctionExpression.ConvertDateTimeCommaSnippet;
			string text = "yyyy-MM-dd HH:mm:ss";
			if (dateTime.Millisecond > 0)
			{
				if (base.SqlBatch.ServerMajorVersion >= 10)
				{
					sqlSnippet = MsSqlFunctionExpression.ConvertDateTime2CommaSnippet;
					text += ".FFFFFFF";
				}
				else
				{
					text += ".FFF";
				}
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				sqlSnippet,
				new SqlStringSnippet("'{0}'", new object[] { dateTime.ToString(text, CultureInfo.InvariantCulture) }),
				MsSqlFunctionExpression.Comma121CloseParenSnippet
			});
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001019C File Offset: 0x0000E39C
		protected override ISqlSnippet CreateBasicSqlSnippetForTime(TimeSpan time)
		{
			DateTime dateTime = DateTime.MinValue + time;
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CastOpenParenSnippet,
				new SqlStringSnippet("'{0}'", new object[] { dateTime.ToString("HH:mm:ss.FFFFFFF", CultureInfo.InvariantCulture) }),
				MsSqlFunctionExpression.AsTime7CloseParenSnippet
			});
		}
	}
}
