using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000148 RID: 328
	internal sealed class BatchQuerySortItemGenerator : IPlanSortItemVisitor
	{
		// Token: 0x06000C18 RID: 3096 RVA: 0x000310D7 File Offset: 0x0002F2D7
		private BatchQuerySortItemGenerator(GeneratedTable input, bool reverseSortOrder, ExpressionTable expressionTable, FederatedEntityDataModel model, bool useConceptualSchema)
		{
			this.m_sortClauses = new QueryItemCollection<QuerySortClause>();
			this.m_input = input;
			this.m_expressionTable = expressionTable;
			this.m_reverseSortOrder = reverseSortOrder;
			this.m_model = model;
			this.m_useConceptualSchema = useConceptualSchema;
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00031110 File Offset: 0x0002F310
		public static IReadOnlyList<QuerySortClause> Generate(GeneratedTable input, IReadOnlyList<PlanSortItem> planSorts, ExpressionTable expressionTable, FederatedEntityDataModel model, bool useConceptualSchema, bool reverseSortOrder = false)
		{
			BatchQuerySortItemGenerator batchQuerySortItemGenerator = new BatchQuerySortItemGenerator(input, reverseSortOrder, expressionTable, model, useConceptualSchema);
			foreach (PlanSortItem planSortItem in planSorts)
			{
				planSortItem.Accept(batchQuerySortItemGenerator);
			}
			return batchQuerySortItemGenerator.SortClauses;
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0003116C File Offset: 0x0002F36C
		public static IReadOnlyList<QuerySortClause> Generate(GeneratedTable input, PlanSortItem planSort, ExpressionTable expressionTable, FederatedEntityDataModel model, bool useConceptualSchema, bool reverseSortOrder = false)
		{
			BatchQuerySortItemGenerator batchQuerySortItemGenerator = new BatchQuerySortItemGenerator(input, reverseSortOrder, expressionTable, model, useConceptualSchema);
			planSort.Accept(batchQuerySortItemGenerator);
			return batchQuerySortItemGenerator.SortClauses;
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x00031193 File Offset: 0x0002F393
		private IReadOnlyList<QuerySortClause> SortClauses
		{
			get
			{
				return this.m_sortClauses;
			}
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0003119C File Offset: 0x0002F39C
		public void Visit(PlanMemberSortItem item)
		{
			foreach (SortKey sortKey in item.Member.Group.SortKeys)
			{
				ExpressionId value = sortKey.Value.ExpressionId.Value;
				QueryTableColumn queryTableColumn;
				if (this.m_input.ColumnMap.TryGetColumn(value, out queryTableColumn))
				{
					this.m_sortClauses.Add(queryTableColumn.Name, queryTableColumn.QdmReference().ToSortClause(BatchQueryGenerationUtils.TranslateSortDirection(sortKey.SortDirection.Value, this.m_reverseSortOrder)));
				}
				else
				{
					RelatedResolvedPropertyExpressionNode relatedResolvedPropertyExpressionNode = this.m_expressionTable.GetNode(value) as RelatedResolvedPropertyExpressionNode;
					if (relatedResolvedPropertyExpressionNode != null)
					{
						QueryRelatedColumnExpression queryRelatedColumnExpression;
						string text;
						if (this.m_useConceptualSchema)
						{
							IConceptualColumn conceptualColumn = relatedResolvedPropertyExpressionNode.Property.AsColumn();
							queryRelatedColumnExpression = QueryExpressionBuilder.RelatedColumn(conceptualColumn);
							text = conceptualColumn.GetFullName();
							this.m_sortClauses.Add(text, queryRelatedColumnExpression.ToSortClause(BatchQueryGenerationUtils.TranslateSortDirection(sortKey.SortDirection.Value, this.m_reverseSortOrder)));
						}
						else
						{
							EdmFieldInstance correspondingEdmFieldInstance = this.m_model.GetCorrespondingEdmFieldInstance(relatedResolvedPropertyExpressionNode.Property);
							queryRelatedColumnExpression = QueryExpressionBuilder.RelatedColumn(correspondingEdmFieldInstance, relatedResolvedPropertyExpressionNode.Property.AsColumn());
							text = correspondingEdmFieldInstance.QualifiedName.ToString();
						}
						this.m_sortClauses.Add(text, queryRelatedColumnExpression.ToSortClause(BatchQueryGenerationUtils.TranslateSortDirection(sortKey.SortDirection.Value, this.m_reverseSortOrder)));
					}
				}
			}
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0003133C File Offset: 0x0002F53C
		public void Visit(PlanColumnSortItem item)
		{
			QueryTableColumn queryTableColumn = this.m_input.ColumnMap[item.Name];
			this.m_sortClauses.Add(queryTableColumn.Name, queryTableColumn.QdmReference().ToSortClause(BatchQueryGenerationUtils.TranslateSortDirection(item.Direction, this.m_reverseSortOrder)));
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00031390 File Offset: 0x0002F590
		public void Visit(PlanAllColumnsSortItem item)
		{
			IEnumerable<QueryTableColumn> columns = this.m_input.QueryTable.Columns;
			HashSet<QueryTableColumn> hashSet = new HashSet<QueryTableColumn>(this.m_input.ColumnMap.GetAllColumns());
			foreach (QueryTableColumn queryTableColumn in columns)
			{
				if (hashSet.Contains(queryTableColumn))
				{
					this.m_sortClauses.Add(queryTableColumn.Name, queryTableColumn.QdmReference().ToSortClause(BatchQueryGenerationUtils.TranslateSortDirection(item.Direction, this.m_reverseSortOrder)));
				}
			}
		}

		// Token: 0x04000618 RID: 1560
		private readonly QueryItemCollection<QuerySortClause> m_sortClauses;

		// Token: 0x04000619 RID: 1561
		private readonly GeneratedTable m_input;

		// Token: 0x0400061A RID: 1562
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x0400061B RID: 1563
		private readonly bool m_reverseSortOrder;

		// Token: 0x0400061C RID: 1564
		private readonly FederatedEntityDataModel m_model;

		// Token: 0x0400061D RID: 1565
		private readonly bool m_useConceptualSchema;
	}
}
