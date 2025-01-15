using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.Oracle
{
	// Token: 0x02000043 RID: 67
	internal sealed class OraSqlLiteralExpression : SqlLiteralExpression
	{
		// Token: 0x060002FE RID: 766 RVA: 0x0000C6E8 File Offset: 0x0000A8E8
		internal OraSqlLiteralExpression(IQPExpressionInfo qpInfo, OraSqlBatch sqlBatch)
			: base(qpInfo, sqlBatch)
		{
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000E600 File Offset: 0x0000C800
		protected override ISqlSnippet CreateBasicSqlSnippetForString(string str)
		{
			if (str.Length == 0)
			{
				return SqlNullExpression.SqlNull;
			}
			if (!this.SqlBatch.EnableUnistr || this.SqlBatch.IsStringInDatabaseCharset(str))
			{
				str = str.Replace("'", "''");
				return new SqlStringSnippet("'{0}'", new object[] { str });
			}
			byte[] bytes = Encoding.BigEndianUnicode.GetBytes(str);
			if (bytes.Length % 2 != 0)
			{
				throw SQEAssert.AssertFalseAndThrow("Invalid unicode representation for the string value \"{0}\"", new object[] { str });
			}
			StringBuilder stringBuilder = new StringBuilder("UNISTR('");
			int i = 0;
			while (i < bytes.Length)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "\\{0}{1}", bytes[i++].ToString("x", CultureInfo.InvariantCulture).PadLeft(2, '0'), bytes[i++].ToString("x", CultureInfo.InvariantCulture).PadLeft(2, '0'));
			}
			stringBuilder.Append("')");
			return new SqlStringSnippet(stringBuilder.ToString());
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000E70C File Offset: 0x0000C90C
		protected override ISqlSnippet CreateBasicSqlSnippetForDateTime(DateTime dateTime)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				OraSqlFunctionExpression.ToTimeStampOpenParenSnippet,
				new SqlStringSnippet("'{0}'", new object[] { dateTime.ToString("yyyy-MM-dd-HH-mm-ss.fff", CultureInfo.InvariantCulture) }),
				SqlExpression.CommaSnippet,
				OraSqlFunctionExpression.StrYYYYMMDDHHMISSFFSnippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00005C3C File Offset: 0x00003E3C
		protected override ISqlSnippet CreateBasicSqlSnippetForTime(TimeSpan time)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000E76B File Offset: 0x0000C96B
		private new OraSqlBatch SqlBatch
		{
			[DebuggerStepThrough]
			get
			{
				return (OraSqlBatch)base.SqlBatch;
			}
		}
	}
}
