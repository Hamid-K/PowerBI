using System;
using System.Data;
using System.Data.Common;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A21 RID: 2593
	internal class DrdaSchemaDataSourceInformation
	{
		// Token: 0x170013AE RID: 5038
		// (get) Token: 0x06005166 RID: 20838 RVA: 0x0014836B File Offset: 0x0014656B
		public static string SchemaName
		{
			get
			{
				return "DataSourceInformation";
			}
		}

		// Token: 0x06005167 RID: 20839 RVA: 0x00148374 File Offset: 0x00146574
		public static DataTable Execute(DrdaConnection connection)
		{
			DataTable dataTable = new DataTable(DrdaSchemaDataSourceInformation.SchemaName);
			dataTable.Columns.Add(DbMetaDataColumnNames.CompositeIdentifierSeparatorPattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.DataSourceProductName, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.DataSourceProductVersion, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.DataSourceProductVersionNormalized, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.GroupByBehavior, typeof(GroupByBehavior));
			dataTable.Columns.Add(DbMetaDataColumnNames.IdentifierPattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.IdentifierCase, typeof(IdentifierCase));
			dataTable.Columns.Add(DbMetaDataColumnNames.OrderByColumnsInSelect, typeof(bool));
			dataTable.Columns.Add(DbMetaDataColumnNames.ParameterMarkerFormat, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.ParameterMarkerPattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.ParameterNameMaxLength, typeof(int));
			dataTable.Columns.Add(DbMetaDataColumnNames.ParameterNamePattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.QuotedIdentifierPattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.QuotedIdentifierCase, typeof(IdentifierCase));
			dataTable.Columns.Add(DbMetaDataColumnNames.StatementSeparatorPattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.StringLiteralPattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.SupportedJoinOperators, typeof(SupportedJoinOperators));
			DataRow dataRow = dataTable.NewRow();
			dataRow[DbMetaDataColumnNames.CompositeIdentifierSeparatorPattern] = "\\.";
			dataRow[DbMetaDataColumnNames.DataSourceProductName] = connection.ServerClass;
			dataRow[DbMetaDataColumnNames.DataSourceProductVersion] = connection.ServerVersion;
			dataRow[DbMetaDataColumnNames.DataSourceProductVersionNormalized] = connection.ServerVersion;
			dataRow[DbMetaDataColumnNames.GroupByBehavior] = GroupByBehavior.Unknown;
			dataRow[DbMetaDataColumnNames.IdentifierPattern] = "[^1234567890 \"%&'\\(\\)\\*\\+,\\|!-\\./:;<=>\\?_][^ \"%&'\\(\\)\\*\\+,\\|!-\\./:;<=>\\?_]*";
			dataRow[DbMetaDataColumnNames.IdentifierCase] = IdentifierCase.Insensitive;
			if (connection.ServerClass.Equals("DB2/NT"))
			{
				dataRow[DbMetaDataColumnNames.OrderByColumnsInSelect] = false;
			}
			else
			{
				dataRow[DbMetaDataColumnNames.OrderByColumnsInSelect] = true;
			}
			dataRow[DbMetaDataColumnNames.ParameterMarkerFormat] = "@{0}";
			dataRow[DbMetaDataColumnNames.ParameterMarkerPattern] = "(@[A-Za-z0-9_$#]*)";
			dataRow[DbMetaDataColumnNames.ParameterNameMaxLength] = 0;
			dataRow[DbMetaDataColumnNames.ParameterNamePattern] = string.Empty;
			dataRow[DbMetaDataColumnNames.QuotedIdentifierPattern] = "(([^]|\"\")*)\"";
			dataRow[DbMetaDataColumnNames.QuotedIdentifierCase] = IdentifierCase.Sensitive;
			dataRow[DbMetaDataColumnNames.StatementSeparatorPattern] = string.Empty;
			dataRow[DbMetaDataColumnNames.StringLiteralPattern] = string.Empty;
			dataRow[DbMetaDataColumnNames.SupportedJoinOperators] = DBNull.Value;
			dataTable.Rows.Add(dataRow);
			dataRow.AcceptChanges();
			return dataTable;
		}
	}
}
