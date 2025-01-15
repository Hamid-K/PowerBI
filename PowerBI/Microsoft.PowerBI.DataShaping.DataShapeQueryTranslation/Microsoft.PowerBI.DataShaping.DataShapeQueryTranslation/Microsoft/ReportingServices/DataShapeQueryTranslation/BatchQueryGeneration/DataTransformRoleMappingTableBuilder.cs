using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000150 RID: 336
	internal sealed class DataTransformRoleMappingTableBuilder
	{
		// Token: 0x06000C68 RID: 3176 RVA: 0x00033863 File Offset: 0x00031A63
		internal DataTransformRoleMappingTableBuilder()
		{
			this.m_tableBuilder = new DataTableBuilder(DataTransformRoleMappingTableBuilder.ConceptualTableSchema);
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0003387C File Offset: 0x00031A7C
		internal void AddExistingColumn(QueryTable table, QueryTableColumn column, string role)
		{
			QueryExpression queryExpression = table.FieldReferenceName(column);
			QueryExpression queryExpression2 = DataTransformRoleMappingTableBuilder.CreateRoleNameExpression(role);
			this.AddColumn(queryExpression, queryExpression2);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x000338A0 File Offset: 0x00031AA0
		internal void AddNewColumn(string columnName, string role)
		{
			QueryLiteralExpression queryLiteralExpression = QueryExpressionBuilder.Literal(columnName);
			QueryExpression queryExpression = DataTransformRoleMappingTableBuilder.CreateRoleNameExpression(role);
			this.AddColumn(queryLiteralExpression, queryExpression);
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x000338C8 File Offset: 0x00031AC8
		private void AddColumn(QueryExpression nameExpression, QueryExpression roleExpression)
		{
			this.m_tableBuilder.AddRow(new QueryExpression[] { nameExpression, roleExpression });
			this.m_hasMapping = true;
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x000338EA File Offset: 0x00031AEA
		private static QueryExpression CreateRoleNameExpression(string role)
		{
			if (role == null)
			{
				return DataTransformConstants.NullStringLiteral;
			}
			return QueryExpressionBuilder.Literal(role);
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00033900 File Offset: 0x00031B00
		internal QueryTable ToQueryTable()
		{
			if (!this.m_hasMapping)
			{
				return this.CreateNullTable();
			}
			return this.m_tableBuilder.ToQueryTable();
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0003391C File Offset: 0x00031B1C
		private QueryTable CreateNullTable()
		{
			return new QueryTableDefinition(Util.EmptyReadOnlyCollection<QueryTableColumn>(), Literals.Null, "DataTable");
		}

		// Token: 0x0400063C RID: 1596
		private static readonly IReadOnlyList<ConceptualPrimitiveResultType> ConceptualTableSchema = new ConceptualPrimitiveResultType[]
		{
			ConceptualPrimitiveResultType.Text,
			ConceptualPrimitiveResultType.Text
		};

		// Token: 0x0400063D RID: 1597
		private readonly DataTableBuilder m_tableBuilder;

		// Token: 0x0400063E RID: 1598
		private bool m_hasMapping;
	}
}
