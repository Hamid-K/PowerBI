using System;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x0200065A RID: 1626
	internal static class OdbcSqlSettings
	{
		// Token: 0x0600337C RID: 13180 RVA: 0x000A48B8 File Offset: 0x000A2AB8
		public static SqlSettings From(OdbcDataSourceInfo dataSourceInfo)
		{
			string text = dataSourceInfo.IdentifierQuoteChar.Trim();
			bool flag = text.Length > 0;
			char[] array = EmptyArray<char>.Instance;
			if (dataSourceInfo.IdentifierSpecialCharacters != null && !flag)
			{
				array = dataSourceInfo.IdentifierSpecialCharacters.ToCharArray();
			}
			SqlSettings sqlSettings = new SqlSettings();
			sqlSettings.InvalidIdentifierCharacters = array;
			sqlSettings.PagingStrategy = PagingStrategy.RowCountOnly;
			Func<string, string> func;
			if (!flag)
			{
				func = (string s) => s;
			}
			else
			{
				func = SqlSettings.StandardQuote(text);
			}
			sqlSettings.QuoteIdentifier = func;
			sqlSettings.UseCommaForCrossJoin = false;
			sqlSettings.MaxIdentifierLength = ((dataSourceInfo.MaxIdentifierNameLength == 0) ? int.MaxValue : dataSourceInfo.MaxIdentifierNameLength);
			sqlSettings.SupportsCaseExpression = true;
			sqlSettings.DeleteCommand = SqlLanguageStrings.DeleteFromSqlString;
			SqlSettings sqlSettings2 = sqlSettings;
			if (dataSourceInfo.CatalogNameSeparator != null)
			{
				sqlSettings2.CatalogSeparator = new ConstantSqlString(dataSourceInfo.CatalogNameSeparator);
				if (dataSourceInfo.CatalogNameLocation != null)
				{
					Odbc32.SQL_QL value = dataSourceInfo.CatalogNameLocation.Value;
					if (value != Odbc32.SQL_QL.SQL_QL_START)
					{
						if (value == Odbc32.SQL_QL.SQL_QL_END)
						{
							sqlSettings2.CatalogLocation = CatalogNameLocation.End;
						}
					}
					else
					{
						sqlSettings2.CatalogLocation = CatalogNameLocation.Start;
					}
				}
			}
			return sqlSettings2;
		}
	}
}
