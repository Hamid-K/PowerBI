using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x0200025D RID: 605
	internal sealed class BatchQueryBuilder : QueryVariableDeclarationScopeBuilder, IQueryBuilder
	{
		// Token: 0x06001A3C RID: 6716 RVA: 0x00048304 File Offset: 0x00046504
		internal BatchQueryBuilder(EntityDataModel model, IConceptualSchema schema, IFeatureSwitchProvider featureSwitchProvider, DaxCapabilities languageCapabilities = null, bool suppressModelGrouping = false)
		{
			this._model = model;
			this._schema = schema;
			this._outputTables = new List<QueryExpression>();
			this._suppressModelGrouping = suppressModelGrouping;
			this._languageCapabilities = languageCapabilities ?? DaxCapabilitiesBuilder.BuildCapabilities(model, schema, featureSwitchProvider);
			this._declarations = new List<QueryBaseDeclarationExpression>();
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x00048358 File Offset: 0x00046558
		public BatchQueryDefinition ToQueryDefinition()
		{
			IReadOnlyList<QueryParameterDeclarationExpression> queryParameters = this._queryParameters;
			QueryBatchRootExpression queryBatchRootExpression = QueryExpressionBuilder.BatchRoot(queryParameters ?? Microsoft.DataShaping.Util.EmptyReadOnlyList<QueryParameterDeclarationExpression>(), this._declarations, this._outputTables);
			return this.CreateQueryDefinition(queryBatchRootExpression);
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x00048390 File Offset: 0x00046590
		public BatchQueryDefinition ToComposableQueryDefinition()
		{
			List<QueryVariableDeclarationExpression> list = new List<QueryVariableDeclarationExpression>(this._declarations.Count);
			foreach (QueryBaseDeclarationExpression queryBaseDeclarationExpression in this._declarations)
			{
				QueryVariableDeclarationExpression queryVariableDeclarationExpression = queryBaseDeclarationExpression as QueryVariableDeclarationExpression;
				if (queryVariableDeclarationExpression == null)
				{
					throw new InvalidOperationException(DevErrors.BatchQueryBuilder.InvalidDeclarationTypeInComposableQuery(queryBaseDeclarationExpression.GetType().Name));
				}
				list.Add(queryVariableDeclarationExpression);
			}
			if (this._outputTables.Count != 1)
			{
				throw new InvalidOperationException(DevErrors.BatchQueryBuilder.MultipleOutputTablesInComposableQuery);
			}
			QueryExpressionWithLocalVariables queryExpressionWithLocalVariables = QueryExpressionBuilder.CreateExpressionWithLocalVariables(list, this._outputTables[0]);
			return this.CreateQueryDefinition(queryExpressionWithLocalVariables);
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x0004844C File Offset: 0x0004664C
		private BatchQueryDefinition CreateQueryDefinition(QueryExpression rootExpr)
		{
			return new BatchQueryDefinition(new QueryCommandTree(this._model, this._schema, rootExpr, this._languageCapabilities));
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x0004846B File Offset: 0x0004666B
		public void AddOutputTable(QueryTable table)
		{
			this._outputTables.Add(table.Expression);
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x00048480 File Offset: 0x00046680
		public QueryParameterReferenceExpression DeclareQueryParameter(ConceptualResultType resultType, string parameterName)
		{
			QueryParameterDeclarationExpression queryParameterDeclarationExpression = resultType.DeclareParameterAs(parameterName);
			Microsoft.Reporting.Util.AddToLazyList<QueryParameterDeclarationExpression>(ref this._queryParameters, queryParameterDeclarationExpression);
			return queryParameterDeclarationExpression.Parameter;
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x000484A8 File Offset: 0x000466A8
		public QueryMeasureExpression DeclareMeasure(QueryExpression expression, EntitySet targetEntitySet, EdmMeasure edmMeasure, IConceptualEntity targetEntity, IConceptualMeasure measure)
		{
			QueryMeasureDeclarationExpression queryMeasureDeclarationExpression = expression.DeclareMeasureAs(targetEntitySet, edmMeasure, targetEntity, measure);
			return this.DeclareMeasure(queryMeasureDeclarationExpression);
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x000484C9 File Offset: 0x000466C9
		internal QueryMeasureExpression DeclareMeasure(QueryMeasureDeclarationExpression declarationExpr)
		{
			this._declarations.Add(declarationExpr);
			return declarationExpr.MeasureRef;
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x000484E0 File Offset: 0x000466E0
		public QueryFieldExpression DeclareField(QueryExpression expression, EntitySet targetEntitySet, EdmField field, IConceptualEntity targetEntity = null, IConceptualColumn column = null)
		{
			QueryFieldDeclarationExpression queryFieldDeclarationExpression = expression.RewriteEntityPlaceholdersToScalarEntityReferences(targetEntitySet, targetEntity).DeclareFieldAs(targetEntitySet, field, targetEntity, column);
			return this.DeclareField(queryFieldDeclarationExpression);
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x00048509 File Offset: 0x00046709
		internal QueryFieldExpression DeclareField(QueryFieldDeclarationExpression declarationExpr)
		{
			this._declarations.Add(declarationExpr);
			return declarationExpr.FieldRef;
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x00048520 File Offset: 0x00046720
		public void DeclareDataSourceVariables(string dataSourceVariables)
		{
			QueryDataSourceVariablesDeclarationExpression queryDataSourceVariablesDeclarationExpression = QueryExpressionBuilder.Literal(dataSourceVariables).DeclareDataSourceVariables();
			this._declarations.Add(queryDataSourceVariablesDeclarationExpression);
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x0004854A File Offset: 0x0004674A
		public void DeclareMParameter(QueryMParameterDeclarationExpression queryMParameterDeclarationExpression)
		{
			this._declarations.Add(queryMParameterDeclarationExpression);
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x00048558 File Offset: 0x00046758
		public IConceptualEntity DeclareTable(QueryTableDeclarationExpression tableDeclaration)
		{
			this._declarations.Add(tableDeclaration);
			return tableDeclaration.Entity;
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x0004856C File Offset: 0x0004676C
		protected override void AddVariableDeclaration(QueryVariableDeclarationExpression declaration)
		{
			this._declarations.Add(declaration);
		}

		// Token: 0x04000E83 RID: 3715
		private readonly EntityDataModel _model;

		// Token: 0x04000E84 RID: 3716
		private readonly IConceptualSchema _schema;

		// Token: 0x04000E85 RID: 3717
		private readonly DaxCapabilities _languageCapabilities;

		// Token: 0x04000E86 RID: 3718
		private readonly List<QueryExpression> _outputTables;

		// Token: 0x04000E87 RID: 3719
		private readonly bool _suppressModelGrouping;

		// Token: 0x04000E88 RID: 3720
		private List<QueryParameterDeclarationExpression> _queryParameters;

		// Token: 0x04000E89 RID: 3721
		private List<QueryBaseDeclarationExpression> _declarations;
	}
}
