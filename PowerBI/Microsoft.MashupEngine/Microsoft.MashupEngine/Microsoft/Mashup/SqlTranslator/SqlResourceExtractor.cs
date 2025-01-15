using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.ScriptDom;

namespace Microsoft.Mashup.SqlTranslator
{
	// Token: 0x0200203F RID: 8255
	internal sealed class SqlResourceExtractor : SqlExpressionVisitor<object, object>
	{
		// Token: 0x0600C9EF RID: 51695 RVA: 0x0028679D File Offset: 0x0028499D
		public static IEnumerable<string> ExtractResources(TSqlFragment fragment)
		{
			return new SqlResourceExtractor(fragment).ExtractResources();
		}

		// Token: 0x0600C9F0 RID: 51696 RVA: 0x002867AA File Offset: 0x002849AA
		private SqlResourceExtractor(TSqlFragment fragment)
		{
			this.fragment = fragment;
			this.resources = new HashSet<string>();
			this.tables = new HashSet<string>();
		}

		// Token: 0x0600C9F1 RID: 51697 RVA: 0x002867D0 File Offset: 0x002849D0
		public IEnumerable<string> ExtractResources()
		{
			SelectStatement selectStatement;
			if (SqlExpressionHelper.TryGetSelectStatement(this.fragment, out selectStatement))
			{
				WithCtesAndXmlNamespaces withCtesAndXmlNamespaces = selectStatement.WithCtesAndXmlNamespaces;
				if (withCtesAndXmlNamespaces != null)
				{
					for (int i = 0; i < withCtesAndXmlNamespaces.CommonTableExpressions.Count; i++)
					{
						CommonTableExpression commonTableExpression = withCtesAndXmlNamespaces.CommonTableExpressions[i];
						this.VisitQueryExpression(commonTableExpression.QueryExpression);
						this.tables.Add(commonTableExpression.ExpressionName.Value);
					}
				}
				this.VisitQueryExpression(selectStatement.QueryExpression);
				return this.resources;
			}
			return EmptyArray<string>.Instance;
		}

		// Token: 0x0600C9F2 RID: 51698 RVA: 0x00286858 File Offset: 0x00284A58
		protected override object NewFunctionCall(Identifier function, UniqueRowFilter uniqueRowFilter, IList<object> parameters)
		{
			if ((string.Equals(function.Value, "cube.dimension", StringComparison.OrdinalIgnoreCase) && parameters.Count == 2) || (string.Equals(function.Value, "cube.applyparameter", StringComparison.OrdinalIgnoreCase) && parameters.Count == 3))
			{
				string text = parameters[0] as string;
				if (text != null && !this.tables.Contains(text))
				{
					this.resources.Add(text);
				}
				return text;
			}
			return null;
		}

		// Token: 0x0600C9F3 RID: 51699 RVA: 0x002868CB File Offset: 0x00284ACB
		protected override object NewLiteralExpression(LiteralType type, string value)
		{
			if (type == LiteralType.String)
			{
				return value;
			}
			return null;
		}

		// Token: 0x0600C9F4 RID: 51700 RVA: 0x002868D4 File Offset: 0x00284AD4
		protected override SqlExpressionVisitor<object, object>.Table NewNamedTableReference(Identifier alias, MultiPartIdentifier table)
		{
			string text = string.Empty;
			for (int i = 0; i < table.Count; i++)
			{
				if (i != 0)
				{
					text += ".";
				}
				text += table[i].Value;
			}
			if (table.Count != 1 || !this.tables.Contains(text))
			{
				this.resources.Add(text);
			}
			return SqlResourceExtractor.emptyTable;
		}

		// Token: 0x0600C9F5 RID: 51701 RVA: 0x00286944 File Offset: 0x00284B44
		protected override SqlExpressionVisitor<object, object>.Table NewSchemaObjectFunctionTableReference(Identifier alias, IList<Identifier> columnAliases, MultiPartIdentifier function, IList<object> parameters)
		{
			if (function.Count == 1 && this.NewFunctionCall(function.Identifiers[0], UniqueRowFilter.NotSpecified, parameters) == null)
			{
				this.resources.Add(function.Identifiers[0].Value);
			}
			return SqlResourceExtractor.emptyTable;
		}

		// Token: 0x0600C9F6 RID: 51702 RVA: 0x000020FA File Offset: 0x000002FA
		protected override Identifier NewColumnIdentifier()
		{
			return null;
		}

		// Token: 0x0600C9F7 RID: 51703 RVA: 0x000020FA File Offset: 0x000002FA
		protected override Identifier NewTableIdentifier()
		{
			return null;
		}

		// Token: 0x0600C9F8 RID: 51704 RVA: 0x000020FA File Offset: 0x000002FA
		protected override object NewBinaryExpression(BinaryExpressionType type, object left, object right)
		{
			return null;
		}

		// Token: 0x0600C9F9 RID: 51705 RVA: 0x000020FA File Offset: 0x000002FA
		protected override object NewBooleanBinaryExpression(BooleanBinaryExpressionType type, object left, object right)
		{
			return null;
		}

		// Token: 0x0600C9FA RID: 51706 RVA: 0x000020FA File Offset: 0x000002FA
		protected override object NewBooleanComparisonExpression(BooleanComparisonType type, object left, object right)
		{
			return null;
		}

		// Token: 0x0600C9FB RID: 51707 RVA: 0x000020FA File Offset: 0x000002FA
		protected override object NewBooleanIsNullExpression(bool isNot, object expression)
		{
			return null;
		}

		// Token: 0x0600C9FC RID: 51708 RVA: 0x000020FA File Offset: 0x000002FA
		protected override object NewBooleanNotExpression(object expression)
		{
			return null;
		}

		// Token: 0x0600C9FD RID: 51709 RVA: 0x000020FA File Offset: 0x000002FA
		protected override object NewBooleanParenthesisExpression(object expression)
		{
			return null;
		}

		// Token: 0x0600C9FE RID: 51710 RVA: 0x000020FA File Offset: 0x000002FA
		protected override object NewCastCall(object dataType, object expression)
		{
			return null;
		}

		// Token: 0x0600C9FF RID: 51711 RVA: 0x000020FA File Offset: 0x000002FA
		protected override object NewCoalesceExpression(IList<object> expressions)
		{
			return null;
		}

		// Token: 0x0600CA00 RID: 51712 RVA: 0x000020FA File Offset: 0x000002FA
		protected override object NewColumnReferenceExpression(ColumnType type, MultiPartIdentifier identifier)
		{
			return null;
		}

		// Token: 0x0600CA01 RID: 51713 RVA: 0x000020FA File Offset: 0x000002FA
		protected override object NewConvertCall(object dataType, object expression)
		{
			return null;
		}

		// Token: 0x0600CA02 RID: 51714 RVA: 0x000020FA File Offset: 0x000002FA
		protected override object NewExpressionWithSortOrder(SortOrder sortOrder, object expression)
		{
			return null;
		}

		// Token: 0x0600CA03 RID: 51715 RVA: 0x000020FA File Offset: 0x000002FA
		protected override object NewInPredicate(object expression, IList<object> values, object subquery)
		{
			return null;
		}

		// Token: 0x0600CA04 RID: 51716 RVA: 0x000020FA File Offset: 0x000002FA
		protected override object NewLeftFunctionCall(object expression, object count)
		{
			return null;
		}

		// Token: 0x0600CA05 RID: 51717 RVA: 0x000020FA File Offset: 0x000002FA
		protected override object NewParenthesisExpression(object expression)
		{
			return null;
		}

		// Token: 0x0600CA06 RID: 51718 RVA: 0x000020FA File Offset: 0x000002FA
		protected override object NewRightFunctionCall(object expression, object count)
		{
			return null;
		}

		// Token: 0x0600CA07 RID: 51719 RVA: 0x000020FA File Offset: 0x000002FA
		protected override object NewScalarSubquery(SqlExpressionVisitor<object, object>.Table table, bool list)
		{
			return null;
		}

		// Token: 0x0600CA08 RID: 51720 RVA: 0x000020FA File Offset: 0x000002FA
		protected override object NewSearchedCaseExpression(IList<Tuple<object, object>> whenExpressions, object elseExpression)
		{
			return null;
		}

		// Token: 0x0600CA09 RID: 51721 RVA: 0x000020FA File Offset: 0x000002FA
		protected override object NewSqlDataTypeReference(MultiPartIdentifier name, SqlDataTypeOption sqlDataTypeOption, IList<object> parameters)
		{
			return null;
		}

		// Token: 0x0600CA0A RID: 51722 RVA: 0x000020FA File Offset: 0x000002FA
		protected override object NewUnaryExpression(UnaryExpressionType type, object expression)
		{
			return null;
		}

		// Token: 0x0600CA0B RID: 51723 RVA: 0x000020FA File Offset: 0x000002FA
		protected override object NewVariableReferenceExpression(string name)
		{
			return null;
		}

		// Token: 0x0600CA0C RID: 51724 RVA: 0x00286993 File Offset: 0x00284B93
		protected override SqlExpressionVisitor<object, object>.Table NewJoinParenthesisTableReference(SqlExpressionVisitor<object, object>.Table table)
		{
			return SqlResourceExtractor.emptyTable;
		}

		// Token: 0x0600CA0D RID: 51725 RVA: 0x00286993 File Offset: 0x00284B93
		protected override SqlExpressionVisitor<object, object>.Table NewQualifiedJoin(SqlExpressionVisitor<object, object>.Table firstTable, SqlExpressionVisitor<object, object>.Table secondTable, QualifiedJoinType joinType, object searchCondition)
		{
			return SqlResourceExtractor.emptyTable;
		}

		// Token: 0x0600CA0E RID: 51726 RVA: 0x00286993 File Offset: 0x00284B93
		protected override SqlExpressionVisitor<object, object>.Table NewBinaryQueryExpression(SqlExpressionVisitor<object, object>.Table firstTable, SqlExpressionVisitor<object, object>.Table secondTable, BinaryQueryExpressionType binaryQueryExpressionType, bool binaryQueryAll)
		{
			return SqlResourceExtractor.emptyTable;
		}

		// Token: 0x0600CA0F RID: 51727 RVA: 0x00286993 File Offset: 0x00284B93
		protected override SqlExpressionVisitor<object, object>.Table NewQueryDerivedTable(Identifier alias, IList<Identifier> columns, SqlExpressionVisitor<object, object>.Table table)
		{
			return SqlResourceExtractor.emptyTable;
		}

		// Token: 0x0600CA10 RID: 51728 RVA: 0x00286993 File Offset: 0x00284B93
		protected override SqlExpressionVisitor<object, object>.Table NewQueryParenthesisExpression(SqlExpressionVisitor<object, object>.Table table)
		{
			return SqlResourceExtractor.emptyTable;
		}

		// Token: 0x0600CA11 RID: 51729 RVA: 0x00286993 File Offset: 0x00284B93
		protected override SqlExpressionVisitor<object, object>.Table NewQuerySpecification(SqlExpressionVisitor<object, object>.Table table)
		{
			return SqlResourceExtractor.emptyTable;
		}

		// Token: 0x0600CA12 RID: 51730 RVA: 0x00286993 File Offset: 0x00284B93
		protected override SqlExpressionVisitor<object, object>.Table NewVariableTableReference(Identifier alias, string name)
		{
			return SqlResourceExtractor.emptyTable;
		}

		// Token: 0x0600CA13 RID: 51731 RVA: 0x00286993 File Offset: 0x00284B93
		protected override SqlExpressionVisitor<object, object>.Table NewComputedColumnsClause(SqlExpressionVisitor<object, object>.Table table, IList<Tuple<Identifier, object>> computedColumns)
		{
			return SqlResourceExtractor.emptyTable;
		}

		// Token: 0x0600CA14 RID: 51732 RVA: 0x00286993 File Offset: 0x00284B93
		protected override SqlExpressionVisitor<object, object>.Table NewDistinctClause(SqlExpressionVisitor<object, object>.Table table)
		{
			return SqlResourceExtractor.emptyTable;
		}

		// Token: 0x0600CA15 RID: 51733 RVA: 0x00286993 File Offset: 0x00284B93
		protected override SqlExpressionVisitor<object, object>.Table NewFromClause(IList<SqlExpressionVisitor<object, object>.Table> tableRefs)
		{
			return SqlResourceExtractor.emptyTable;
		}

		// Token: 0x0600CA16 RID: 51734 RVA: 0x00286993 File Offset: 0x00284B93
		protected override SqlExpressionVisitor<object, object>.Table NewGroupByClause(SqlExpressionVisitor<object, object>.Table table, IList<object> groupingSpecs, IList<Tuple<Identifier, object>> computedColumns)
		{
			return SqlResourceExtractor.emptyTable;
		}

		// Token: 0x0600CA17 RID: 51735 RVA: 0x00286993 File Offset: 0x00284B93
		protected override SqlExpressionVisitor<object, object>.Table NewOrderByClause(SqlExpressionVisitor<object, object>.Table table, IList<object> orderByElements)
		{
			return SqlResourceExtractor.emptyTable;
		}

		// Token: 0x0600CA18 RID: 51736 RVA: 0x00286993 File Offset: 0x00284B93
		protected override SqlExpressionVisitor<object, object>.Table NewSelectedColumnsClause(SqlExpressionVisitor<object, object>.Table table, IList<MultiPartIdentifier> selection)
		{
			return SqlResourceExtractor.emptyTable;
		}

		// Token: 0x0600CA19 RID: 51737 RVA: 0x00286993 File Offset: 0x00284B93
		protected override SqlExpressionVisitor<object, object>.Table NewTopClause(SqlExpressionVisitor<object, object>.Table table, object expression)
		{
			return SqlResourceExtractor.emptyTable;
		}

		// Token: 0x0600CA1A RID: 51738 RVA: 0x00286993 File Offset: 0x00284B93
		protected override SqlExpressionVisitor<object, object>.Table NewWhereClause(SqlExpressionVisitor<object, object>.Table table, object searchCondition)
		{
			return SqlResourceExtractor.emptyTable;
		}

		// Token: 0x040066CB RID: 26315
		private static readonly SqlExpressionVisitor<object, object>.Table emptyTable = new SqlExpressionVisitor<object, object>.Table
		{
			Inputs = new SqlExpressionVisitor<object, object>.Table[0],
			Columns = new SqlExpressionVisitor<object, object>.ColumnBinding[0],
			Expression = null
		};

		// Token: 0x040066CC RID: 26316
		private readonly TSqlFragment fragment;

		// Token: 0x040066CD RID: 26317
		private readonly HashSet<string> resources;

		// Token: 0x040066CE RID: 26318
		private readonly HashSet<string> tables;
	}
}
