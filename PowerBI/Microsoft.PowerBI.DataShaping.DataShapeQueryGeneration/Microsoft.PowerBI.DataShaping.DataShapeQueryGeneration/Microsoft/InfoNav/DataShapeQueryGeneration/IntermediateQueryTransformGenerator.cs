using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.DataShapeQueryGeneration.Annotations;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000D7 RID: 215
	internal sealed class IntermediateQueryTransformGenerator
	{
		// Token: 0x06000786 RID: 1926 RVA: 0x0001C4D0 File Offset: 0x0001A6D0
		private IntermediateQueryTransformGenerator(DataShapeGenerationErrorContext errorContext, QuerySourceExpressionReferenceContext sourceRefContext, QueryParameterReferenceContext parameterRefContext, DataShapeIdGenerator dataShapeIdGenerator, string resolvedQueryName)
		{
			this._errorContext = errorContext;
			this._transformResolver = new IntermediateQueryTransformResolver();
			this._expressionGenerator = new DsqExpressionGenerator(this._transformResolver, sourceRefContext, parameterRefContext, this._errorContext, false);
			this._dataShapeIdGenerator = dataShapeIdGenerator;
			this._sourceRefContext = sourceRefContext;
			this._resolvedQueryName = resolvedQueryName;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001C528 File Offset: 0x0001A728
		internal static bool TryGenerate(DataShapeGenerationErrorContext errorContext, ResolvedSemanticQueryDataShapeCommand command, QuerySourceExpressionReferenceContext sourceRefContext, QueryParameterReferenceContext parameterRefContext, SemanticQueryDataShapeAnnotations annotations, DataShapeIdGenerator dataShapeIdGenerator, out IntermediateQueryTransformGeneratorResult result)
		{
			Dictionary<ResolvedSemanticQueryDataShape, IntermediateQueryTransformContext> dictionary = new Dictionary<ResolvedSemanticQueryDataShape, IntermediateQueryTransformContext>();
			result = new IntermediateQueryTransformGeneratorResult(dictionary);
			IntermediateQueryTransformContext intermediateQueryTransformContext;
			if (!IntermediateQueryTransformGenerator.TryGenerate(errorContext, command.QueryDataShape.Query.Transform, sourceRefContext, parameterRefContext, command.QueryDataShape.Query.Name, annotations, dataShapeIdGenerator, out intermediateQueryTransformContext))
			{
				return false;
			}
			dictionary.Add(command.QueryDataShape, intermediateQueryTransformContext);
			return true;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001C584 File Offset: 0x0001A784
		internal static bool TryGenerate(DataShapeGenerationErrorContext errorContext, IReadOnlyList<ResolvedQueryTransform> queryTransforms, QuerySourceExpressionReferenceContext sourceRefContext, QueryParameterReferenceContext parameterRefContext, string resolvedQueryName, SemanticQueryDataShapeAnnotations annotations, DataShapeIdGenerator dataShapeIdGenerator, out IntermediateQueryTransformContext transformContext)
		{
			if (queryTransforms.IsNullOrEmpty<ResolvedQueryTransform>())
			{
				transformContext = IntermediateQueryTransformContext.Empty;
				return true;
			}
			return new IntermediateQueryTransformGenerator(errorContext, sourceRefContext, parameterRefContext, dataShapeIdGenerator, resolvedQueryName).TryGenerate(queryTransforms, out transformContext);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001C5AC File Offset: 0x0001A7AC
		private bool TryGenerate(IReadOnlyList<ResolvedQueryTransform> queryTransforms, out IntermediateQueryTransformContext transformContext)
		{
			bool flag;
			try
			{
				this._allowModelRef = true;
				List<IntermediateQueryTransform> list = DsqGenerationUtils.BuildList<IntermediateQueryTransform, ResolvedQueryTransform>(queryTransforms, new Func<ResolvedQueryTransform, IntermediateQueryTransform>(this.Generate));
				transformContext = new IntermediateQueryTransformContext(list, this._transformResolver);
				flag = true;
			}
			catch (IntermediateQueryTransformGenerator.IntermediateQueryTransformGeneratorException)
			{
				transformContext = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001C600 File Offset: 0x0001A800
		private IntermediateQueryTransform Generate(ResolvedQueryTransform queryTransform)
		{
			string text = this._dataShapeIdGenerator.CreateTransformId();
			IntermediateQueryTransformInput intermediateQueryTransformInput = this.Generate(queryTransform.Input, text);
			IntermediateQueryTransformOutput intermediateQueryTransformOutput = this.Generate(queryTransform.Output, text);
			IntermediateQueryTransform intermediateQueryTransform = new IntermediateQueryTransform(text, queryTransform.Name, queryTransform.Algorithm, intermediateQueryTransformInput, intermediateQueryTransformOutput);
			this._allowModelRef = false;
			return intermediateQueryTransform;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001C658 File Offset: 0x0001A858
		private IntermediateQueryTransformInput Generate(ResolvedQueryTransformInput queryInput, string transformId)
		{
			this._allowOutputRoleRef = false;
			IntermediateQueryTransformTable intermediateQueryTransformTable = this.Generate(queryInput.Table, transformId + "Input");
			return new IntermediateQueryTransformInput(DsqGenerationUtils.BuildList<IntermediateQueryTransformParameter, ResolvedQueryTransformParameter>(queryInput.Parameters, new Func<ResolvedQueryTransformParameter, IntermediateQueryTransformParameter>(this.Generate)), intermediateQueryTransformTable);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001C6A1 File Offset: 0x0001A8A1
		private IntermediateQueryTransformOutput Generate(ResolvedQueryTransformOutput queryOutput, string transformId)
		{
			this._allowOutputRoleRef = true;
			this._allowModelRef = false;
			return new IntermediateQueryTransformOutput(this.Generate(queryOutput.Table, transformId + "Output"));
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001C6D0 File Offset: 0x0001A8D0
		private IntermediateQueryTransformParameter Generate(ResolvedQueryTransformParameter queryParam)
		{
			ResolvedQueryLiteralExpression resolvedQueryLiteralExpression = queryParam.Expression as ResolvedQueryLiteralExpression;
			if (resolvedQueryLiteralExpression == null)
			{
				throw this.Fail(DataShapeGenerationMessages.InvalidTransformParameterExpression(EngineMessageSeverity.Error, queryParam.Name));
			}
			LiteralExpressionNode literalExpressionNode = ExpressionNodeBuilder.Literal(resolvedQueryLiteralExpression.Value.GetValueAsObject());
			return new IntermediateQueryTransformParameter(queryParam.Name, literalExpressionNode);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001C720 File Offset: 0x0001A920
		private IntermediateQueryTransformTable Generate(ResolvedQueryTransformTable queryTable, string tableId)
		{
			IntermediateQueryTransformTable intermediateQueryTransformTable;
			List<IntermediateQueryTransformTableColumn> list = this.GenerateColumns(queryTable.Columns, queryTable.Name, out intermediateQueryTransformTable);
			IntermediateQueryTransformTable intermediateQueryTransformTable2 = new IntermediateQueryTransformTable(tableId, queryTable.Name, list, intermediateQueryTransformTable, this._sourceRefContext, this._errorContext);
			this._transformResolver.Clear();
			this._transformResolver.RegisterColumns(queryTable.Columns, intermediateQueryTransformTable2.Columns);
			return intermediateQueryTransformTable2;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0001C788 File Offset: 0x0001A988
		private List<IntermediateQueryTransformTableColumn> GenerateColumns(IReadOnlyList<ResolvedQueryTransformTableColumn> queryColumns, string tableQueryName, out IntermediateQueryTransformTable sourceTable)
		{
			if (queryColumns == null)
			{
				sourceTable = null;
				return null;
			}
			sourceTable = null;
			List<IntermediateQueryTransformTableColumn> list = new List<IntermediateQueryTransformTableColumn>(queryColumns.Count);
			for (int i = 0; i < queryColumns.Count; i++)
			{
				IntermediateQueryTransformTable intermediateQueryTransformTable;
				list.Add(this.Generate(queryColumns[i], i, tableQueryName, out intermediateQueryTransformTable));
				if (sourceTable == null)
				{
					sourceTable = intermediateQueryTransformTable;
				}
			}
			return list;
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001C7DC File Offset: 0x0001A9DC
		private IntermediateQueryTransformTableColumn Generate(ResolvedQueryTransformTableColumn queryColumn, int columnIndex, string tableQueryName, out IntermediateQueryTransformTable sourceTable)
		{
			string text = DataShapeIdGenerator.CreateTransformColumnId(columnIndex);
			IntermediateQueryTransformTableColumn intermediateQueryTransformTableColumn;
			if (this.TryGenerateReferenceColumn(queryColumn, text, tableQueryName, out intermediateQueryTransformTableColumn, out sourceTable))
			{
				return intermediateQueryTransformTableColumn;
			}
			if (this.TryGenerateOutputRoleRefColumn(queryColumn, text, out intermediateQueryTransformTableColumn))
			{
				return intermediateQueryTransformTableColumn;
			}
			if (this.TryGenerateMeasureColumn(queryColumn, text, tableQueryName, out intermediateQueryTransformTableColumn))
			{
				return intermediateQueryTransformTableColumn;
			}
			if (this.TryGenerateModelColumnRefColumn(queryColumn, text, tableQueryName, out intermediateQueryTransformTableColumn))
			{
				return intermediateQueryTransformTableColumn;
			}
			if (this.TryGenerateSubqueryRefColumn(queryColumn, text, out intermediateQueryTransformTableColumn))
			{
				return intermediateQueryTransformTableColumn;
			}
			throw this.Fail(DataShapeGenerationMessages.InvalidTransformColumnExpression(EngineMessageSeverity.Error, tableQueryName, queryColumn.Name));
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001C850 File Offset: 0x0001AA50
		private bool TryGenerateReferenceColumn(ResolvedQueryTransformTableColumn queryColumn, string id, string tableQueryName, out IntermediateQueryTransformTableColumn column, out IntermediateQueryTransformTable sourceTable)
		{
			ResolvedQueryTransformTableColumnExpression resolvedQueryTransformTableColumnExpression = queryColumn.Expression as ResolvedQueryTransformTableColumnExpression;
			if (resolvedQueryTransformTableColumnExpression == null)
			{
				column = null;
				sourceTable = null;
				return false;
			}
			IntermediateQueryTransformTableColumn intermediateQueryTransformTableColumn;
			if (!this._transformResolver.TryResolveColumn(resolvedQueryTransformTableColumnExpression.Column, out intermediateQueryTransformTableColumn))
			{
				throw this.Fail(DataShapeGenerationMessages.InvalidTransformColumnReference(EngineMessageSeverity.Error, resolvedQueryTransformTableColumnExpression.Column.Name));
			}
			sourceTable = intermediateQueryTransformTableColumn.Table;
			column = new IntermediateQueryTransformTableColumn(id, intermediateQueryTransformTableColumn.DsqExpression(), queryColumn.Role, intermediateQueryTransformTableColumn.ActAs, intermediateQueryTransformTableColumn.FormatString, intermediateQueryTransformTableColumn.UnderlyingConceptualColumn, intermediateQueryTransformTableColumn.UnderlyingExpression, intermediateQueryTransformTableColumn.IsScalar);
			return true;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001C8EC File Offset: 0x0001AAEC
		private bool TryGenerateOutputRoleRefColumn(ResolvedQueryTransformTableColumn queryColumn, string id, out IntermediateQueryTransformTableColumn column)
		{
			ResolvedQueryExpression expression = queryColumn.Expression;
			ResolvedQueryTransformOutputRoleRefExpression resolvedQueryTransformOutputRoleRefExpression = expression as ResolvedQueryTransformOutputRoleRefExpression;
			if (resolvedQueryTransformOutputRoleRefExpression == null)
			{
				column = null;
				return false;
			}
			if (!this._allowOutputRoleRef)
			{
				throw this.Fail(DataShapeGenerationMessages.InvalidTransformOutputRoleRefExpression(EngineMessageSeverity.Error));
			}
			column = new IntermediateQueryTransformTableColumn(id, resolvedQueryTransformOutputRoleRefExpression.Role.TransformOutputRoleRef(FunctionUsageKind.Unassigned), queryColumn.Role, TransformTableColumnActAs.Detail, null, null, expression, null);
			return true;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001C958 File Offset: 0x0001AB58
		private bool TryGenerateMeasureColumn(ResolvedQueryTransformTableColumn queryColumn, string id, string tableQueryName, out IntermediateQueryTransformTableColumn column)
		{
			ResolvedQueryExpression expression = queryColumn.Expression;
			ProjectedDsqExpression projectedDsqExpression;
			if (!SemanticQueryProjectionGenerator.TryExtractMeasure(this._expressionGenerator, this._errorContext, null, null, expression, false, false, AllowedExpressionContent.Transform, new ExpressionContext(this._resolvedQueryName, SemanticQueryObjectType.TransformColumn, id), out projectedDsqExpression))
			{
				column = null;
				return false;
			}
			if (!this._allowModelRef)
			{
				throw this.Fail(DataShapeGenerationMessages.InvalidTransformColumnExpressionSchemaReference(EngineMessageSeverity.Error, tableQueryName, queryColumn.Name));
			}
			column = new IntermediateQueryTransformTableColumn(id, projectedDsqExpression.Value.DsqExpression, queryColumn.Role, TransformTableColumnActAs.Measure, projectedDsqExpression.Value.FormatString, null, expression, projectedDsqExpression.IsScalar);
			return true;
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0001C9F8 File Offset: 0x0001ABF8
		private bool TryGenerateModelColumnRefColumn(ResolvedQueryTransformTableColumn queryColumn, string id, string tableQueryName, out IntermediateQueryTransformTableColumn column)
		{
			ResolvedQueryExpression expression = queryColumn.Expression;
			IConceptualColumn conceptualColumn;
			if (!expression.TryGetAsProperty(out conceptualColumn))
			{
				column = null;
				return false;
			}
			if (!this._allowModelRef)
			{
				throw this.Fail(DataShapeGenerationMessages.InvalidTransformColumnExpressionSchemaReference(EngineMessageSeverity.Error, tableQueryName, queryColumn.Name));
			}
			column = new IntermediateQueryTransformTableColumn(id, conceptualColumn.DsqExpression(), queryColumn.Role, TransformTableColumnActAs.GroupKey, conceptualColumn.FormatString, conceptualColumn, expression, new bool?(conceptualColumn.ConceptualDataType.IsScalar()));
			return true;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0001CA6C File Offset: 0x0001AC6C
		private bool TryGenerateSubqueryRefColumn(ResolvedQueryTransformTableColumn queryColumn, string id, out IntermediateQueryTransformTableColumn column)
		{
			ResolvedQueryColumnReferenceExpression resolvedQueryColumnReferenceExpression = queryColumn.Expression as ResolvedQueryColumnReferenceExpression;
			IntermediateTableSchemaColumn intermediateTableSchemaColumn;
			if (resolvedQueryColumnReferenceExpression == null || !this._sourceRefContext.TryGetColumnInSource(resolvedQueryColumnReferenceExpression, this._errorContext, out intermediateTableSchemaColumn))
			{
				column = null;
				return false;
			}
			IConceptualColumn conceptualColumn = intermediateTableSchemaColumn.LineageProperty as IConceptualColumn;
			column = new IntermediateQueryTransformTableColumn(id, intermediateTableSchemaColumn.ValueCalculationId.StructureReference(), queryColumn.Role, TransformTableColumnActAs.GroupKey, intermediateTableSchemaColumn.FormatString, conceptualColumn, queryColumn.Expression, (conceptualColumn != null) ? new bool?(conceptualColumn.ConceptualDataType.IsScalar()) : null);
			return true;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0001CB00 File Offset: 0x0001AD00
		private IntermediateQueryTransformGenerator.IntermediateQueryTransformGeneratorException Fail(DataShapeGenerationMessage message)
		{
			this._errorContext.Register(message);
			return new IntermediateQueryTransformGenerator.IntermediateQueryTransformGeneratorException(message.TraceMessage);
		}

		// Token: 0x040003EA RID: 1002
		private const string InputTableIdSuffix = "Input";

		// Token: 0x040003EB RID: 1003
		private const string OutputTableIdSuffix = "Output";

		// Token: 0x040003EC RID: 1004
		private readonly DataShapeGenerationErrorContext _errorContext;

		// Token: 0x040003ED RID: 1005
		private readonly IntermediateQueryTransformResolver _transformResolver;

		// Token: 0x040003EE RID: 1006
		private readonly DsqExpressionGenerator _expressionGenerator;

		// Token: 0x040003EF RID: 1007
		private readonly DataShapeIdGenerator _dataShapeIdGenerator;

		// Token: 0x040003F0 RID: 1008
		private readonly string _resolvedQueryName;

		// Token: 0x040003F1 RID: 1009
		private readonly QuerySourceExpressionReferenceContext _sourceRefContext;

		// Token: 0x040003F2 RID: 1010
		private bool _allowModelRef;

		// Token: 0x040003F3 RID: 1011
		private bool _allowOutputRoleRef;

		// Token: 0x02000156 RID: 342
		private sealed class IntermediateQueryTransformGeneratorException : Exception
		{
			// Token: 0x060009D3 RID: 2515 RVA: 0x00025E96 File Offset: 0x00024096
			internal IntermediateQueryTransformGeneratorException(string message)
				: base(message)
			{
			}
		}
	}
}
