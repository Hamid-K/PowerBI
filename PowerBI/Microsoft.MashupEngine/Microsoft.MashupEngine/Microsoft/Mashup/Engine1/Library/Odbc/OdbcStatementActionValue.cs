using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x0200065C RID: 1628
	internal class OdbcStatementActionValue : ActionValue
	{
		// Token: 0x06003380 RID: 13184 RVA: 0x000A49D8 File Offset: 0x000A2BD8
		public OdbcStatementActionValue(OdbcDataSource dataSource, OdbcStatementExpression expression, string[] columnNames, bool countOnly)
		{
			this.dataSource = dataSource;
			this.expression = expression;
			this.columnNames = columnNames;
			this.countOnly = countOnly;
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x000A4A00 File Offset: 0x000A2C00
		public override Value Execute()
		{
			Value value;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				ScriptWriter scriptWriter = new ScriptWriter(stringWriter, this.dataSource.SqlSettings);
				this.expression.Statement.WriteCreateScript(scriptWriter);
				string text = stringWriter.ToString();
				IList<OdbcParameter> list = scriptWriter.Parameters.Select((DynamicParameter p) => (OdbcParameter)p.Value).ToArray<OdbcParameter>();
				DataTable dataTable = new DataTable
				{
					Locale = CultureInfo.InvariantCulture
				};
				if (this.countOnly)
				{
					long num = this.dataSource.ExecuteNonQuery(text, null, list);
					dataTable.Columns.Add("Value", typeof(int));
					dataTable.Rows.Add(new object[] { num });
				}
				else
				{
					using (IDataReader dataReader = this.dataSource.Execute(this.dataSource.Host.GetPersistentCache(), text, null, list, RowRange.All, this.columnNames, true, null))
					{
						dataTable.Load(dataReader);
					}
					if (this.expression.GetSelectQuery != null)
					{
						TableValue tableValue = DataReaderTableValue.New(dataTable);
						return new QueryTableValue(this.expression.GetSelectQuery(tableValue));
					}
				}
				value = DataReaderTableValue.New(dataTable);
			}
			return value;
		}

		// Token: 0x040016E7 RID: 5863
		private OdbcDataSource dataSource;

		// Token: 0x040016E8 RID: 5864
		private OdbcStatementExpression expression;

		// Token: 0x040016E9 RID: 5865
		private string[] columnNames;

		// Token: 0x040016EA RID: 5866
		private bool countOnly;
	}
}
