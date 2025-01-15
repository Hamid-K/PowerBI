using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000600 RID: 1536
	internal class OdbcInsertStatement : SqlInsertStatement
	{
		// Token: 0x06003093 RID: 12435 RVA: 0x00092EFB File Offset: 0x000910FB
		public OdbcInsertStatement(TableReference table, List<IList<OdbcScalarExpression>> values, OutputClause outputClause = null, List<ColumnReference> columnList = null)
			: base(table, values.Select((IList<OdbcScalarExpression> l) => l.Select((OdbcScalarExpression v) => v.Expression)), outputClause, columnList)
		{
		}
	}
}
