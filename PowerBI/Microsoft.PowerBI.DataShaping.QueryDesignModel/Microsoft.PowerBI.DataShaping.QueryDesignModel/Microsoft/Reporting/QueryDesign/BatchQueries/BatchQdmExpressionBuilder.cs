using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.InfoNav.Utils;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000259 RID: 601
	internal static class BatchQdmExpressionBuilder
	{
		// Token: 0x06001A22 RID: 6690 RVA: 0x0004808C File Offset: 0x0004628C
		internal static QueryExpression Project(this QueryTable table, QueryTableColumn column)
		{
			QueryExpressionBinding queryExpressionBinding = table.ToBinding();
			return queryExpressionBinding.Project(queryExpressionBinding.Variable.Field(column.Name), ProjectSubsetStrategy.Default);
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x000480AC File Offset: 0x000462AC
		internal static QueryExpression Project(this QueryTable table, QueryExpression expression)
		{
			QueryExpressionBinding queryExpressionBinding = table.ToBinding();
			expression = expression.RewriteColumnReferences(table.Columns, queryExpressionBinding.Variable);
			return queryExpressionBinding.Project(expression, ProjectSubsetStrategy.Default);
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x000480DC File Offset: 0x000462DC
		internal static QueryExpressionBinding ToBinding(this QueryTable table)
		{
			return table.Expression.BindAs(table.BindingVariableNameSuggestion);
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x000480EF File Offset: 0x000462EF
		public static QueryTableColumn ToQueryTableColumn(this QueryExpression expression, string columnName)
		{
			return new QueryTableColumn(columnName, expression);
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x000480F8 File Offset: 0x000462F8
		public static QueryTableColumn CreateDerivedColumn<T>(string columnName) where T : struct
		{
			ConceptualPrimitiveResultType primitive = typeof(T).GetPrimitive();
			return new QueryTableColumn(columnName, new QueryNonComposableExpression(primitive));
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x00048121 File Offset: 0x00046321
		public static QueryTableColumn CreateBooleanIndicatorColumn(string columnName)
		{
			return new QueryNonComposableExpression(ConceptualPrimitiveResultType.Boolean).ToQueryTableColumn(columnName);
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x00048134 File Offset: 0x00046334
		public static QueryConcatenateXExpression ConcatenateX(this QueryTable table, QueryExpression expression, QueryExpression delimiter, QuerySortClause orderBy)
		{
			QueryExpressionBinding queryExpressionBinding = table.ToBinding();
			expression = expression.RewriteColumnReferences(table.Columns, queryExpressionBinding.Variable);
			if (orderBy != null)
			{
				orderBy = orderBy.RewriteColumnReferences(table.Columns, queryExpressionBinding.Variable);
			}
			return new QueryConcatenateXExpression(queryExpressionBinding, expression, delimiter, orderBy);
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x0004817C File Offset: 0x0004637C
		public static QdmTableColumnReferenceExpression QdmReference(this QueryTableColumn target)
		{
			return new QdmTableColumnReferenceExpression(target);
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x00048184 File Offset: 0x00046384
		public static QueryExpression FieldReferenceName(this QueryTable table, QueryTableColumn column)
		{
			return table.Expression.FieldReferenceName(column.Name);
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x00048197 File Offset: 0x00046397
		public static QueryTableColumn ToReferenceColumn(this QueryTableColumn target)
		{
			return target.QdmReference().ToQueryTableColumn(target.Name);
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x000481AA File Offset: 0x000463AA
		public static QuerySortClause ToSortClause(this QueryTableColumn target, SortDirection direction = SortDirection.Ascending)
		{
			return target.QdmReference().ToSortClause(direction);
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x000481B8 File Offset: 0x000463B8
		internal static QueryExpression RewriteColumnReferences(this QueryExpression expression, IReadOnlyList<QueryTableColumn> columns, QueryVariableReferenceExpression variableRef)
		{
			return expression.Accept<QueryExpression>(new BatchQdmExpressionBuilder.ExpressionColumnReferenceToFieldTransform(columns, variableRef));
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x000481C7 File Offset: 0x000463C7
		internal static QuerySortClause RewriteColumnReferences(this QuerySortClause sortClause, IReadOnlyList<QueryTableColumn> columns, QueryVariableReferenceExpression variableRef)
		{
			return new BatchQdmExpressionBuilder.ExpressionColumnReferenceToFieldTransform(columns, variableRef).Visit(sortClause);
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x000481D8 File Offset: 0x000463D8
		internal static QueryTableColumn RewriteColumnReferences(this QueryTableColumn column, IReadOnlyList<QueryTableColumn> referencableColumns, QueryVariableReferenceExpression variableRef)
		{
			QueryExpression queryExpression = column.Expression.RewriteColumnReferences(referencableColumns, variableRef);
			if (queryExpression == column.Expression)
			{
				return column;
			}
			return queryExpression.ToQueryTableColumn(column.Name);
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x0004820A File Offset: 0x0004640A
		internal static QueryExpression RewriteCurrentGroupPlaceholders(this QueryExpression expression, QueryTable table, QueryGroupExpressionBinding binding)
		{
			return expression.Accept<QueryExpression>(new BatchQdmExpressionBuilder.ExpressionCurrentGroupPlaceholderTransform(table, binding));
		}

		// Token: 0x020003EC RID: 1004
		private sealed class ExpressionColumnReferenceToFieldTransform : DefaultExpressionVisitor
		{
			// Token: 0x0600211B RID: 8475 RVA: 0x00059EBF File Offset: 0x000580BF
			internal ExpressionColumnReferenceToFieldTransform(IReadOnlyList<QueryTableColumn> columns, QueryVariableReferenceExpression variableRef)
			{
				this._columns = columns;
				this._variableRef = variableRef;
			}

			// Token: 0x0600211C RID: 8476 RVA: 0x00059ED8 File Offset: 0x000580D8
			protected internal override QueryExpression Visit(QueryExtensionExpression expression)
			{
				QdmTableColumnReferenceExpression columnRefExpr = expression as QdmTableColumnReferenceExpression;
				if (columnRefExpr != null)
				{
					if (!this._columns.Any((QueryTableColumn c) => c.Name == columnRefExpr.Target.Name))
					{
						Contract.RetailFail("Could not find referenced column {0} in the current table {1}.", columnRefExpr.Target.Name.MarkAsCustomerContent(), this._variableRef.VariableName.MarkAsCustomerContent());
					}
					return this._variableRef.Field(columnRefExpr.Target.Name);
				}
				return base.Visit(expression);
			}

			// Token: 0x0600211D RID: 8477 RVA: 0x00059F6A File Offset: 0x0005816A
			internal QuerySortClause Visit(QuerySortClause sortClause)
			{
				return this.VisitSortClause(sortClause);
			}

			// Token: 0x04001402 RID: 5122
			private readonly IReadOnlyList<QueryTableColumn> _columns;

			// Token: 0x04001403 RID: 5123
			private readonly QueryVariableReferenceExpression _variableRef;
		}

		// Token: 0x020003ED RID: 1005
		private sealed class ExpressionCurrentGroupPlaceholderTransform : DefaultExpressionVisitor
		{
			// Token: 0x0600211E RID: 8478 RVA: 0x00059F73 File Offset: 0x00058173
			internal ExpressionCurrentGroupPlaceholderTransform(QueryTable table, QueryGroupExpressionBinding binding)
			{
				this._table = table;
				this._binding = binding;
			}

			// Token: 0x0600211F RID: 8479 RVA: 0x00059F8C File Offset: 0x0005818C
			protected internal override QueryExpression Visit(QueryExtensionExpression expression)
			{
				QdmCurrentGroupPlaceholderExpression qdmCurrentGroupPlaceholderExpression = expression as QdmCurrentGroupPlaceholderExpression;
				if (qdmCurrentGroupPlaceholderExpression != null)
				{
					Contracts.Check(this._table.Equals(qdmCurrentGroupPlaceholderExpression.Table), "Expression does not match target table.");
					return this._binding.CurrentGroup();
				}
				return base.Visit(expression);
			}

			// Token: 0x04001404 RID: 5124
			private readonly QueryTable _table;

			// Token: 0x04001405 RID: 5125
			private readonly QueryGroupExpressionBinding _binding;
		}
	}
}
