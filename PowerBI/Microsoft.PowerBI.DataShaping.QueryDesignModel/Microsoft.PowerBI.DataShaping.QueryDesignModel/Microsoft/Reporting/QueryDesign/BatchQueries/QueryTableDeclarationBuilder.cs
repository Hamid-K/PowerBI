using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000273 RID: 627
	internal sealed class QueryTableDeclarationBuilder
	{
		// Token: 0x06001B21 RID: 6945 RVA: 0x0004C295 File Offset: 0x0004A495
		public QueryTableDeclarationBuilder(string tableName, QueryTable input, IConceptualSchema schema)
		{
			this._tableName = tableName;
			this._input = input;
			this._schema = schema;
			this._additionalColumns = new List<global::System.ValueTuple<string, QueryExpression>>();
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x0004C2BD File Offset: 0x0004A4BD
		public QueryVisualShapeBuilder AddVisualShape()
		{
			this._visualShapeBuilder = new QueryVisualShapeBuilder();
			return this._visualShapeBuilder;
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x0004C2D0 File Offset: 0x0004A4D0
		public QueryTableColumn AddAdditionalColumn(QueryExpression expression, string name)
		{
			this._additionalColumns.Add(new global::System.ValueTuple<string, QueryExpression>(name, expression));
			return new QueryNonComposableExpression(expression.ConceptualResultType).ToQueryTableColumn(name);
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x0004C2F8 File Offset: 0x0004A4F8
		public QueryTableDeclarationExpression ToDeclaration()
		{
			QueryVariableReferenceExpression variable = this._input.Expression.ConceptualResultType.GetRowType().Variable(this._tableName);
			Func<QueryExpression, QueryExpression> func = delegate(QueryExpression e)
			{
				if (e == null)
				{
					return null;
				}
				return e.RewriteColumnReferences(this._input.Columns, variable);
			};
			QueryVisualShape queryVisualShape = null;
			if (this._visualShapeBuilder != null)
			{
				queryVisualShape = this._visualShapeBuilder.ToVisualShape(func);
			}
			IConceptualEntity conceptualEntity = this.BuildEntity();
			IReadOnlyList<QueryFieldDeclarationExpression> readOnlyList = this.BuildFieldDeclarations(conceptualEntity, func);
			return this._input.Expression.DeclareTableAs(queryVisualShape, conceptualEntity, readOnlyList);
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x0004C380 File Offset: 0x0004A580
		private IConceptualEntity BuildEntity()
		{
			IReadOnlyList<ConceptualTypeColumn> readOnlyList = this.BuildEntityTypeColumns();
			return TransientEdmItemFactory.BuildEntity(this._tableName, this._schema, readOnlyList);
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x0004C3A8 File Offset: 0x0004A5A8
		private IReadOnlyList<ConceptualTypeColumn> BuildEntityTypeColumns()
		{
			IReadOnlyList<ConceptualTypeColumn> columns = this._input.Expression.GetRowResultType().Columns;
			QueryVisualShapeBuilder visualShapeBuilder = this._visualShapeBuilder;
			QueryTableColumn queryTableColumn = ((visualShapeBuilder != null) ? visualShapeBuilder.IsDensifiedColumn : null);
			if (this._additionalColumns.Count == 0 && queryTableColumn == null)
			{
				return columns;
			}
			List<ConceptualTypeColumn> list = new List<ConceptualTypeColumn>(columns.Count + this._additionalColumns.Count + ((queryTableColumn != null) ? 1 : 0));
			list.AddRange(columns);
			foreach (global::System.ValueTuple<string, QueryExpression> valueTuple in this._additionalColumns)
			{
				ConceptualPrimitiveResultType conceptualPrimitiveResultType = (ConceptualPrimitiveResultType)valueTuple.Item2.ConceptualResultType;
				list.Add(new ConceptualTypeColumn(conceptualPrimitiveResultType, valueTuple.Item1));
			}
			if (queryTableColumn != null)
			{
				list.Add(queryTableColumn.ConceptualResultType.Column(queryTableColumn.Name, null));
			}
			return list;
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x0004C49C File Offset: 0x0004A69C
		private IReadOnlyList<QueryFieldDeclarationExpression> BuildFieldDeclarations(IConceptualEntity entity, Func<QueryExpression, QueryExpression> rewriteExpression)
		{
			if (this._additionalColumns.Count == 0)
			{
				return Microsoft.DataShaping.Util.EmptyReadOnlyList<QueryFieldDeclarationExpression>();
			}
			List<QueryFieldDeclarationExpression> list = new List<QueryFieldDeclarationExpression>(this._additionalColumns.Count);
			foreach (global::System.ValueTuple<string, QueryExpression> valueTuple in this._additionalColumns)
			{
				QueryExpression queryExpression = rewriteExpression(valueTuple.Item2);
				list.Add(queryExpression.DeclareFieldAs(entity, valueTuple.Item1));
			}
			return list;
		}

		// Token: 0x04000EE2 RID: 3810
		private readonly string _tableName;

		// Token: 0x04000EE3 RID: 3811
		private readonly QueryTable _input;

		// Token: 0x04000EE4 RID: 3812
		private readonly IConceptualSchema _schema;

		// Token: 0x04000EE5 RID: 3813
		private QueryVisualShapeBuilder _visualShapeBuilder;

		// Token: 0x04000EE6 RID: 3814
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Name", "Expression" })]
		private List<global::System.ValueTuple<string, QueryExpression>> _additionalColumns;
	}
}
