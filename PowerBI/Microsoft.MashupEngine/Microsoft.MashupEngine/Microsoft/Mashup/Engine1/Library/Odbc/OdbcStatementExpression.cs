using System;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000656 RID: 1622
	internal sealed class OdbcStatementExpression : OdbcSqlExpression
	{
		// Token: 0x06003359 RID: 13145 RVA: 0x000A4383 File Offset: 0x000A2583
		public OdbcStatementExpression(SqlStatement statement, Func<TableValue, Query> getSelectQuery = null)
		{
			this.Statement = statement;
			this.GetSelectQuery = getSelectQuery;
		}

		// Token: 0x17001279 RID: 4729
		// (get) Token: 0x0600335A RID: 13146 RVA: 0x000A4399 File Offset: 0x000A2599
		// (set) Token: 0x0600335B RID: 13147 RVA: 0x000A43A1 File Offset: 0x000A25A1
		public SqlStatement Statement { get; private set; }

		// Token: 0x1700127A RID: 4730
		// (get) Token: 0x0600335C RID: 13148 RVA: 0x000A43AA File Offset: 0x000A25AA
		// (set) Token: 0x0600335D RID: 13149 RVA: 0x000A43B2 File Offset: 0x000A25B2
		public Func<TableValue, Query> GetSelectQuery { get; private set; }

		// Token: 0x1700127B RID: 4731
		// (get) Token: 0x0600335E RID: 13150 RVA: 0x000023C4 File Offset: 0x000005C4
		public override OdbcSqlExpressionKind Kind
		{
			get
			{
				return OdbcSqlExpressionKind.Statement;
			}
		}

		// Token: 0x1700127C RID: 4732
		// (get) Token: 0x0600335F RID: 13151 RVA: 0x000A43BB File Offset: 0x000A25BB
		public override TypeValue TypeValue
		{
			get
			{
				return TypeValue.Table;
			}
		}

		// Token: 0x1700127D RID: 4733
		// (get) Token: 0x06003360 RID: 13152 RVA: 0x000033E7 File Offset: 0x000015E7
		public override OdbcConditionExpression AsCondition
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700127E RID: 4734
		// (get) Token: 0x06003361 RID: 13153 RVA: 0x000033E7 File Offset: 0x000015E7
		public override OdbcScalarExpression AsScalar
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700127F RID: 4735
		// (get) Token: 0x06003362 RID: 13154 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override OdbcStatementExpression AsStatement
		{
			get
			{
				return this;
			}
		}
	}
}
