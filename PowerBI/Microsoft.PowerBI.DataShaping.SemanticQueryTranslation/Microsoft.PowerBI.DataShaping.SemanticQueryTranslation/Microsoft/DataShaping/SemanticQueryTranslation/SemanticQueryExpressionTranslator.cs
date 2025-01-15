using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.SemanticQueryTranslation.SparklineData;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.DataShaping.SemanticQueryTranslation
{
	// Token: 0x02000013 RID: 19
	internal sealed class SemanticQueryExpressionTranslator : DefaultResolvedQueryExpressionVisitor<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>
	{
		// Token: 0x0600008A RID: 138 RVA: 0x000037F9 File Offset: 0x000019F9
		private SemanticQueryExpressionTranslator(SemanticQueryTranslationErrorContext errorContext, EntityDataModel model, IConceptualSchema schema, IFeatureSwitchProvider featureSwitchProvider, bool createTransientMeasureForExtensionMeasures)
		{
			this.m_errorContext = errorContext;
			this.m_model = model;
			this.m_schema = schema;
			this.m_featureSwitchProvider = featureSwitchProvider;
			this.m_createTransientMeasureForExtensionMeasures = createTransientMeasureForExtensionMeasures;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003826 File Offset: 0x00001A26
		private bool UseConceptualSchema
		{
			get
			{
				return this.m_featureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003838 File Offset: 0x00001A38
		public static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression Generate(SemanticQueryTranslationErrorContext errorContext, EntityDataModel model, IConceptualSchema schema, IFeatureSwitchProvider featureSwitchProvider, ResolvedQueryDefinition resolvedQuery)
		{
			SemanticQueryExpressionTranslator semanticQueryExpressionTranslator = new SemanticQueryExpressionTranslator(errorContext, model, schema, featureSwitchProvider, false);
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression;
			try
			{
				queryExpression = SemanticQueryExpressionTranslator.GenerateSingleExpression(errorContext, resolvedQuery, semanticQueryExpressionTranslator);
			}
			catch (DataShapeEngineException)
			{
				throw;
			}
			catch (Exception ex) when (!ex.IsStoppingException())
			{
				errorContext.Register(SemanticQueryTranslationMessages.QdmTranslationError(EngineMessageSeverity.Error, ex.Message));
				queryExpression = null;
			}
			return queryExpression;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000038B0 File Offset: 0x00001AB0
		public static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression TranslateExpression(SemanticQueryTranslationErrorContext errorContext, EntityDataModel model, IConceptualSchema schema, IFeatureSwitchProvider featureSwitchProvider, ResolvedQueryExpression expression, bool createTransientMeasureForExtensionMeasures = false)
		{
			SemanticQueryExpressionTranslator semanticQueryExpressionTranslator = new SemanticQueryExpressionTranslator(errorContext, model, schema, featureSwitchProvider, createTransientMeasureForExtensionMeasures);
			return expression.Accept<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(semanticQueryExpressionTranslator);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000038D1 File Offset: 0x00001AD1
		private static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression GenerateSingleExpression(SemanticQueryTranslationErrorContext errorContext, ResolvedQueryDefinition resolvedQuery, SemanticQueryExpressionTranslator queryGenerationVisitor)
		{
			return resolvedQuery.Select[0].Expression.Accept<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(queryGenerationVisitor);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000038EC File Offset: 0x00001AEC
		public override Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression Visit(ResolvedQueryFloorExpression expression)
		{
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression;
			if (!this.TryValidateBinningFieldExpression(expression.Expression, out queryExpression))
			{
				return null;
			}
			if (!this.TryValidateDateTimeFloorExpression(expression.TimeUnit, queryExpression))
			{
				return null;
			}
			return queryExpression.TypeSafeFloor(expression.Size, expression.TimeUnit);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003930 File Offset: 0x00001B30
		public override Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression Visit(ResolvedQueryDiscretizeExpression expression)
		{
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression;
			if (!this.TryValidateBinningFieldExpression(expression.Expression, out queryExpression))
			{
				return null;
			}
			return DiscretizeExpressionTranslator.Translate(queryExpression, expression.Count);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000395C File Offset: 0x00001B5C
		public override Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression Visit(ResolvedQuerySparklineDataExpression expression)
		{
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression;
			global::System.ValueTuple<IEdmFieldInstance, IReadOnlyList<IEdmFieldInstance>> valueTuple;
			global::System.ValueTuple<IConceptualColumn, IReadOnlyList<IConceptualColumn>> valueTuple2;
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression2;
			if (!this.TryValidateSparklineDataExpression(expression, out queryExpression, out valueTuple, out valueTuple2, out queryExpression2))
			{
				return null;
			}
			return SparklineDataExpressionDaxTranslator.Translate(queryExpression, valueTuple, valueTuple2, expression.PointsPerSparkline, queryExpression2, expression.IncludeMinGroupingInterval, this.m_schema);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003998 File Offset: 0x00001B98
		private bool TryValidateSparklineDataExpression(ResolvedQuerySparklineDataExpression expression, out Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression measureExpr, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Field", "GroupingFields" })] out global::System.ValueTuple<IEdmFieldInstance, IReadOnlyList<IEdmFieldInstance>> fieldToGroupingFieldsMapping, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Column", "GroupingColumns" })] out global::System.ValueTuple<IConceptualColumn, IReadOnlyList<IConceptualColumn>> columnToGroupingColumnsMapping, out Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression scalarKeyExpr)
		{
			measureExpr = null;
			fieldToGroupingFieldsMapping = new global::System.ValueTuple<IEdmFieldInstance, IReadOnlyList<IEdmFieldInstance>>(null, null);
			columnToGroupingColumnsMapping = new global::System.ValueTuple<IConceptualColumn, IReadOnlyList<IConceptualColumn>>(null, null);
			scalarKeyExpr = null;
			return !(expression == null) && !(expression.Measure == null) && !expression.Groupings.IsNullOrEmpty<ResolvedQueryExpression>() && this.TryValidateSparklineDataMeasure(expression.Measure, out measureExpr) && this.TryValidateSparklineDataGroupings(expression.Groupings, out fieldToGroupingFieldsMapping, out columnToGroupingColumnsMapping) && (!(expression.ScalarKey != null) || this.TryValidateSparklineDataScalarKey(expression.ScalarKey, out scalarKeyExpr));
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003A32 File Offset: 0x00001C32
		private bool TryValidateSparklineDataMeasure(ResolvedQueryExpression expression, out Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression measureExpr)
		{
			measureExpr = expression.Accept<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(this);
			if (!(measureExpr is Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryMeasureExpression) && !(measureExpr is QueryFunctionExpression))
			{
				this.m_errorContext.Register(SemanticQueryTranslationMessages.SparklineDataTranslationUnexpectedMeasureError(EngineMessageSeverity.Error));
				return false;
			}
			return true;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003A64 File Offset: 0x00001C64
		private bool TryValidateSparklineDataGroupings(IReadOnlyList<ResolvedQueryExpression> groupings, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Field", "GroupingFields" })] out global::System.ValueTuple<IEdmFieldInstance, IReadOnlyList<IEdmFieldInstance>> fieldToGroupingFieldsMapping, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Column", "GroupingColumns" })] out global::System.ValueTuple<IConceptualColumn, IReadOnlyList<IConceptualColumn>> columnToGroupingColumnsMapping)
		{
			if (groupings.Count != 1)
			{
				this.m_errorContext.Register(SemanticQueryTranslationMessages.SparklineDataTranslationUnsupportedNumberOfGroupingError(EngineMessageSeverity.Error));
				fieldToGroupingFieldsMapping = new global::System.ValueTuple<IEdmFieldInstance, IReadOnlyList<IEdmFieldInstance>>(null, null);
				columnToGroupingColumnsMapping = new global::System.ValueTuple<IConceptualColumn, IReadOnlyList<IConceptualColumn>>(null, null);
				return false;
			}
			ResolvedQueryColumnExpression resolvedQueryColumnExpression = groupings[0] as ResolvedQueryColumnExpression;
			if (resolvedQueryColumnExpression == null)
			{
				this.m_errorContext.Register(SemanticQueryTranslationMessages.SparklineDataTranslationUnexpectedGroupingError(EngineMessageSeverity.Error));
				fieldToGroupingFieldsMapping = new global::System.ValueTuple<IEdmFieldInstance, IReadOnlyList<IEdmFieldInstance>>(null, null);
				columnToGroupingColumnsMapping = new global::System.ValueTuple<IConceptualColumn, IReadOnlyList<IConceptualColumn>>(null, null);
				return false;
			}
			global::System.ValueTuple<IEdmFieldInstance, IConceptualColumn> fieldInstanceAndColumn = this.GetFieldInstanceAndColumn(resolvedQueryColumnExpression);
			IEdmFieldInstance item = fieldInstanceAndColumn.Item1;
			IConceptualColumn item2 = fieldInstanceAndColumn.Item2;
			if (this.UseConceptualSchema ? (item2 == null) : (item == null))
			{
				this.m_errorContext.Register(SemanticQueryTranslationMessages.SparklineDataTranslationUnexpectedGroupingError(EngineMessageSeverity.Error));
				fieldToGroupingFieldsMapping = new global::System.ValueTuple<IEdmFieldInstance, IReadOnlyList<IEdmFieldInstance>>(null, null);
				columnToGroupingColumnsMapping = new global::System.ValueTuple<IConceptualColumn, IReadOnlyList<IConceptualColumn>>(null, null);
				return false;
			}
			if (this.UseConceptualSchema)
			{
				IReadOnlyList<IConceptualColumn> groupingColumns = this.GetGroupingColumns(resolvedQueryColumnExpression);
				columnToGroupingColumnsMapping = new global::System.ValueTuple<IConceptualColumn, IReadOnlyList<IConceptualColumn>>(item2, groupingColumns);
				fieldToGroupingFieldsMapping = new global::System.ValueTuple<IEdmFieldInstance, IReadOnlyList<IEdmFieldInstance>>(null, null);
			}
			else
			{
				IReadOnlyList<IEdmFieldInstance> groupingFields = this.GetGroupingFields(resolvedQueryColumnExpression, item);
				fieldToGroupingFieldsMapping = new global::System.ValueTuple<IEdmFieldInstance, IReadOnlyList<IEdmFieldInstance>>(item, groupingFields);
				columnToGroupingColumnsMapping = new global::System.ValueTuple<IConceptualColumn, IReadOnlyList<IConceptualColumn>>(null, null);
			}
			return true;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003B8C File Offset: 0x00001D8C
		private bool TryValidateSparklineDataScalarKey(ResolvedQueryExpression scalarKey, out Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression scalarKeyExpr)
		{
			scalarKeyExpr = scalarKey.Accept<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(this);
			ConceptualPrimitiveResultType conceptualPrimitiveResultType = scalarKeyExpr.ConceptualResultType as ConceptualPrimitiveResultType;
			if (scalarKeyExpr == null || conceptualPrimitiveResultType == null || !conceptualPrimitiveResultType.ConceptualDataType.IsScalar())
			{
				this.m_errorContext.Register(SemanticQueryTranslationMessages.SparklineDataTranslationUnexpectedScalarKeyError(EngineMessageSeverity.Error));
				return false;
			}
			return true;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003BD8 File Offset: 0x00001DD8
		private bool TryValidateBinningFieldExpression(ResolvedQueryExpression expression, out Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression fieldExpr)
		{
			fieldExpr = expression.Accept<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(this);
			if (!(fieldExpr is QueryFieldExpression))
			{
				this.m_errorContext.Register(SemanticQueryTranslationMessages.BinningTranslationErrorExpectedField(EngineMessageSeverity.Error));
				return false;
			}
			ConceptualPrimitiveType? conceptualPrimitiveType = fieldExpr.ConceptualResultType.GetPrimitiveTypeKind();
			ConceptualPrimitiveType conceptualPrimitiveType2 = ConceptualPrimitiveType.Binary;
			if (!((conceptualPrimitiveType.GetValueOrDefault() == conceptualPrimitiveType2) & (conceptualPrimitiveType != null)))
			{
				conceptualPrimitiveType = fieldExpr.ConceptualResultType.GetPrimitiveTypeKind();
				conceptualPrimitiveType2 = ConceptualPrimitiveType.Boolean;
				if (!((conceptualPrimitiveType.GetValueOrDefault() == conceptualPrimitiveType2) & (conceptualPrimitiveType != null)))
				{
					conceptualPrimitiveType = fieldExpr.ConceptualResultType.GetPrimitiveTypeKind();
					conceptualPrimitiveType2 = ConceptualPrimitiveType.Text;
					if (!((conceptualPrimitiveType.GetValueOrDefault() == conceptualPrimitiveType2) & (conceptualPrimitiveType != null)))
					{
						return true;
					}
				}
			}
			this.m_errorContext.Register(SemanticQueryTranslationMessages.BinningTranslationErrorInvalidDataType(EngineMessageSeverity.Error));
			return false;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003C88 File Offset: 0x00001E88
		private bool TryValidateDateTimeFloorExpression(TimeUnit? timeUnit, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression fieldExpr)
		{
			if (timeUnit != null && (timeUnit.Value == TimeUnit.Week || timeUnit.Value == TimeUnit.Decade))
			{
				this.m_errorContext.Register(SemanticQueryTranslationMessages.BinningTranslationErrorInvalidTimeUnit(EngineMessageSeverity.Error, timeUnit.Value));
				return false;
			}
			if ((fieldExpr.ConceptualResultType.IsDateTime() && timeUnit == null) || (!fieldExpr.ConceptualResultType.IsDateTime() && timeUnit != null))
			{
				this.m_errorContext.Register(SemanticQueryTranslationMessages.BinningTranslationErrorMissingTimeUnit(EngineMessageSeverity.Error));
				return false;
			}
			return true;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003D0C File Offset: 0x00001F0C
		public override Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression Visit(ResolvedQueryInExpression expression)
		{
			IReadOnlyList<ResolvedQueryExpression> expressions = expression.Expressions;
			IReadOnlyList<IReadOnlyList<ResolvedQueryExpression>> values = expression.Values;
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression = expressions[0].Accept<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(this);
			DataTableBuilder dataTableBuilder = new DataTableBuilder(new ConceptualPrimitiveResultType[] { (ConceptualPrimitiveResultType)queryExpression.ConceptualResultType });
			for (int i = 0; i < values.Count; i++)
			{
				ResolvedQueryExpression resolvedQueryExpression = values[i][0];
				Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression[] array = new Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression[] { resolvedQueryExpression.Accept<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(this) };
				dataTableBuilder.AddRow(array);
			}
			return queryExpression.InTable(dataTableBuilder.ToQueryTable().Expression);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003D9C File Offset: 0x00001F9C
		public override Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression Visit(ResolvedQueryComparisonExpression expression)
		{
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression;
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression2;
			if (!this.TryProcessComparands(expression, out queryExpression, out queryExpression2))
			{
				return this.VisitUnhandledExpression(expression);
			}
			switch (expression.ComparisonKind)
			{
			case Microsoft.InfoNav.Data.Contracts.Internal.QueryComparisonKind.Equal:
				return queryExpression.Equal(queryExpression2);
			case Microsoft.InfoNav.Data.Contracts.Internal.QueryComparisonKind.GreaterThan:
				return queryExpression.GreaterThan(queryExpression2);
			case Microsoft.InfoNav.Data.Contracts.Internal.QueryComparisonKind.GreaterThanOrEqual:
				return queryExpression.GreaterThanOrEqual(queryExpression2);
			case Microsoft.InfoNav.Data.Contracts.Internal.QueryComparisonKind.LessThan:
				return queryExpression.LessThan(queryExpression2);
			case Microsoft.InfoNav.Data.Contracts.Internal.QueryComparisonKind.LessThanOrEqual:
				return queryExpression.LessThanOrEqual(queryExpression2);
			default:
				this.m_errorContext.Register(SemanticQueryTranslationMessages.InvalidComparisonKind(EngineMessageSeverity.Error, expression.ComparisonKind));
				return null;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003E24 File Offset: 0x00002024
		private bool TryProcessComparands(ResolvedQueryComparisonExpression expression, out Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression leftExpression, out Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression rightExpression)
		{
			leftExpression = null;
			rightExpression = null;
			if (!this.IsNullLiteral(expression.Left))
			{
				leftExpression = expression.Left.Accept<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(this);
			}
			if (!this.IsNullLiteral(expression.Right))
			{
				rightExpression = expression.Right.Accept<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(this);
			}
			if (leftExpression == null && rightExpression == null)
			{
				this.m_errorContext.Register(SemanticQueryTranslationMessages.InvalidNullValueComparison(EngineMessageSeverity.Error));
				return false;
			}
			if (leftExpression == null)
			{
				leftExpression = rightExpression.ConceptualResultType.ToExpression(ScalarValue.Null);
			}
			if (rightExpression == null)
			{
				rightExpression = leftExpression.ConceptualResultType.ToExpression(ScalarValue.Null);
			}
			rightExpression = QueryExpressionBuilder.ResolveCrossTypeComparisons(leftExpression, rightExpression);
			return true;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003EC4 File Offset: 0x000020C4
		public override Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression Visit(ResolvedQueryContainsExpression expression)
		{
			return expression.Left.Accept<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(this).TextContains(expression.Right.Accept<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(this));
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003EE3 File Offset: 0x000020E3
		public override Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression Visit(ResolvedQueryLiteralExpression expression)
		{
			return QueryExpressionBuilder.Literal(new ScalarValue(expression.Value.GetValueAsObject()));
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003EFA File Offset: 0x000020FA
		public override Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression Visit(ResolvedQueryStartsWithExpression expression)
		{
			return expression.Left.Accept<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(this).StartsWith(expression.Right.Accept<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(this));
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003F19 File Offset: 0x00002119
		public override Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression Visit(ResolvedQueryNotExpression expression)
		{
			return expression.Expression.Accept<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(this).Not();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003F2C File Offset: 0x0000212C
		public override Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression Visit(ResolvedQueryAndExpression expression)
		{
			return expression.Left.Accept<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(this).And(expression.Right.Accept<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(this));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003F4B File Offset: 0x0000214B
		public override Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression Visit(ResolvedQueryOrExpression expression)
		{
			return expression.Left.Accept<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(this).Or(expression.Right.Accept<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(this));
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003F6C File Offset: 0x0000216C
		public override Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression Visit(ResolvedQueryColumnExpression expression)
		{
			EntitySet entitySet;
			IConceptualEntity conceptualEntity;
			if (!this.TryGetEdmEntitySet(expression, out entitySet, out conceptualEntity))
			{
				return null;
			}
			if (this.UseConceptualSchema)
			{
				return conceptualEntity.ScalarEntity().Field(expression.Column);
			}
			return entitySet.ScalarEntity(null).Field(expression.Column.EdmName);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003FBC File Offset: 0x000021BC
		public override Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression Visit(ResolvedQueryMeasureExpression expression)
		{
			if (this.UseConceptualSchema)
			{
				return expression.Measure.Entity.InvokeMeasure(expression.Measure);
			}
			if (!expression.Measure.Entity.Schema.IsDefaultSchema() && this.m_createTransientMeasureForExtensionMeasures)
			{
				EntitySet entitySet = this.m_model.EntitySets.FindByEdmReferenceName(expression.Measure.Entity.Name);
				if (entitySet == null)
				{
					this.m_errorContext.Register(SemanticQueryTranslationMessages.InvalidMeasureExpression(EngineMessageSeverity.Error));
					return null;
				}
				EdmMeasure edmMeasure = TransientEdmItemFactory.CreateMeasure(entitySet, expression.Measure.Name, ConceptualPrimitiveResultType.FromPrimitive(expression.Measure.ConceptualDataType));
				return entitySet.InvokeMeasure(edmMeasure, null, null);
			}
			else
			{
				EntitySet entitySet2 = this.m_model.EntitySets[expression.Measure.Entity.GetFullName()];
				if (entitySet2 == null)
				{
					this.m_errorContext.Register(SemanticQueryTranslationMessages.InvalidMeasureExpression(EngineMessageSeverity.Error));
					return null;
				}
				return entitySet2.InvokeMeasure(expression.Measure.EdmName);
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000040B4 File Offset: 0x000022B4
		public override Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression Visit(ResolvedQueryAggregationExpression expression)
		{
			EntitySet entitySet;
			IConceptualEntity conceptualEntity;
			string text;
			if (!this.TryGetAggregateEntitySetAndFieldName(expression, out entitySet, out conceptualEntity, out text))
			{
				return null;
			}
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression = entitySet.QdmAggregateArgument(text, conceptualEntity);
			return CoreFunctions.InvokeFunction(FunctionDescriptorFactory.GetDescriptor(DsqExpressionUtils.GetDsqFunctionName(expression, false)).BackingFunctionName, new Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression[] { queryExpression });
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000040FC File Offset: 0x000022FC
		private bool TryGetAggregateEntitySetAndFieldName(ResolvedQueryAggregationExpression expression, out EntitySet entitySet, out IConceptualEntity entity, out string fieldName)
		{
			entitySet = null;
			fieldName = null;
			entity = null;
			ResolvedQueryColumnExpression resolvedQueryColumnExpression = expression.Expression as ResolvedQueryColumnExpression;
			if (resolvedQueryColumnExpression != null)
			{
				entitySet = (this.UseConceptualSchema ? null : this.m_model.EntitySets[resolvedQueryColumnExpression.Column.Entity.GetFullName()]);
				fieldName = resolvedQueryColumnExpression.Column.EdmName;
				entity = resolvedQueryColumnExpression.Column.Entity;
				return true;
			}
			ResolvedQueryMeasureExpression resolvedQueryMeasureExpression = expression.Expression as ResolvedQueryMeasureExpression;
			if (resolvedQueryMeasureExpression != null)
			{
				entitySet = (this.UseConceptualSchema ? null : this.m_model.EntitySets[resolvedQueryMeasureExpression.Measure.Entity.GetFullName()]);
				fieldName = resolvedQueryMeasureExpression.Measure.EdmName;
				entity = resolvedQueryMeasureExpression.Measure.Entity;
				return true;
			}
			return false;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000041C8 File Offset: 0x000023C8
		private global::System.ValueTuple<IEdmFieldInstance, IConceptualColumn> GetFieldInstanceAndColumn(ResolvedQueryColumnExpression expression)
		{
			EntitySet entitySet;
			IConceptualEntity conceptualEntity;
			if (this.TryGetEdmEntitySet(expression, out entitySet, out conceptualEntity))
			{
				IEdmFieldInstance edmFieldInstance2;
				if (!this.UseConceptualSchema)
				{
					IEdmFieldInstance edmFieldInstance = entitySet.FieldInstance(expression.Column.EdmName);
					edmFieldInstance2 = edmFieldInstance;
				}
				else
				{
					edmFieldInstance2 = null;
				}
				IConceptualColumn conceptualColumn = (this.UseConceptualSchema ? expression.Column : null);
				return new global::System.ValueTuple<IEdmFieldInstance, IConceptualColumn>(edmFieldInstance2, conceptualColumn);
			}
			return new global::System.ValueTuple<IEdmFieldInstance, IConceptualColumn>(null, null);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004228 File Offset: 0x00002428
		private bool TryGetEdmEntitySet(ResolvedQueryColumnExpression expression, out EntitySet entitySet, out IConceptualEntity entity)
		{
			if (!expression.Column.Entity.Schema.IsDefaultSchema())
			{
				this.m_errorContext.Register(SemanticQueryTranslationMessages.InvalidColumnExpression(EngineMessageSeverity.Error));
				entitySet = null;
				entity = null;
				return false;
			}
			entitySet = (this.UseConceptualSchema ? null : this.m_model.EntitySets[expression.Column.Entity.GetFullName()]);
			entity = (this.UseConceptualSchema ? expression.Column.Entity : null);
			return true;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000042AC File Offset: 0x000024AC
		private IReadOnlyList<IEdmFieldInstance> GetGroupingFields(ResolvedQueryColumnExpression groupingExpression, IEdmFieldInstance groupingFieldInstance)
		{
			List<IEdmFieldInstance> list = new List<IEdmFieldInstance>();
			EntitySet entity = groupingFieldInstance.Entity;
			foreach (IConceptualColumn conceptualColumn in groupingExpression.Column.OrderByColumns)
			{
				if (!conceptualColumn.Equals(groupingExpression.Column))
				{
					EdmFieldInstance edmFieldInstance = entity.FieldInstance(conceptualColumn.EdmName);
					if (!list.Contains(edmFieldInstance))
					{
						list.Add(edmFieldInstance);
					}
				}
			}
			foreach (IConceptualColumn conceptualColumn2 in groupingExpression.Column.Grouping.IdentityColumns)
			{
				if (!conceptualColumn2.Equals(groupingExpression.Column))
				{
					EdmFieldInstance edmFieldInstance2 = entity.FieldInstance(conceptualColumn2.EdmName);
					if (!list.Contains(edmFieldInstance2))
					{
						list.Add(edmFieldInstance2);
					}
				}
			}
			return list.ToReadOnlyList<IEdmFieldInstance>();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000043BC File Offset: 0x000025BC
		private IReadOnlyList<IConceptualColumn> GetGroupingColumns(ResolvedQueryColumnExpression groupingExpression)
		{
			List<IConceptualColumn> list = new List<IConceptualColumn>();
			foreach (IConceptualColumn conceptualColumn in groupingExpression.Column.OrderByColumns)
			{
				if (!conceptualColumn.Equals(groupingExpression.Column) && !list.Contains(conceptualColumn))
				{
					list.Add(conceptualColumn);
				}
			}
			foreach (IConceptualColumn conceptualColumn2 in groupingExpression.Column.Grouping.IdentityColumns)
			{
				if (!conceptualColumn2.Equals(groupingExpression.Column) && !list.Contains(conceptualColumn2))
				{
					list.Add(conceptualColumn2);
				}
			}
			return list.ToReadOnlyList<IConceptualColumn>();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004490 File Offset: 0x00002690
		protected override Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression VisitUnhandledExpression(ResolvedQueryExpression expression)
		{
			this.m_errorContext.Register(SemanticQueryTranslationMessages.InvalidExpressionType(EngineMessageSeverity.Error, expression.GetType().Name));
			return null;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000044B0 File Offset: 0x000026B0
		private bool IsNullLiteral(ResolvedQueryExpression expression)
		{
			ResolvedQueryLiteralExpression resolvedQueryLiteralExpression = expression as ResolvedQueryLiteralExpression;
			return resolvedQueryLiteralExpression != null && resolvedQueryLiteralExpression.Value.Type == ConceptualPrimitiveType.Null;
		}

		// Token: 0x04000043 RID: 67
		private readonly EntityDataModel m_model;

		// Token: 0x04000044 RID: 68
		private readonly IConceptualSchema m_schema;

		// Token: 0x04000045 RID: 69
		private readonly SemanticQueryTranslationErrorContext m_errorContext;

		// Token: 0x04000046 RID: 70
		private readonly IFeatureSwitchProvider m_featureSwitchProvider;

		// Token: 0x04000047 RID: 71
		private readonly bool m_createTransientMeasureForExtensionMeasures;
	}
}
