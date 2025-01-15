using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.Model;
using Microsoft.DataShaping.QueryDesignModel.Common;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.InfoNav.Utils;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x0200014B RID: 331
	internal sealed class DaxTransform : QueryExpressionVisitor<DaxExpression>
	{
		// Token: 0x060011C7 RID: 4551 RVA: 0x00031E0B File Offset: 0x0003000B
		internal DaxTransform(DaxCapabilities daxCapabilities, string culture)
			: this(daxCapabilities, culture, CancellationToken.None)
		{
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00031E1C File Offset: 0x0003001C
		internal DaxTransform(DaxCapabilities daxCapabilities, string culture, CancellationToken cancellationToken)
		{
			this._scopeVars = new Stack<global::System.ValueTuple<string, DaxExpression>>();
			this._globalScopeVars = new Dictionary<string, DaxExpression>();
			base..ctor();
			this._daxCapabilities = daxCapabilities;
			this._cancellationToken = cancellationToken;
			this._cultureInfo = CultureInfo.GetCultureInfo(culture ?? string.Empty);
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00031E68 File Offset: 0x00030068
		internal DaxTransform(DaxCapabilities daxCapabilities, CultureInfo cultureInfo, CancellationToken cancellationToken)
		{
			this._scopeVars = new Stack<global::System.ValueTuple<string, DaxExpression>>();
			this._globalScopeVars = new Dictionary<string, DaxExpression>();
			base..ctor();
			this._daxCapabilities = daxCapabilities;
			this._cancellationToken = cancellationToken;
			this._cultureInfo = cultureInfo;
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x00031E9B File Offset: 0x0003009B
		public DaxCapabilities DaxCapabilities
		{
			get
			{
				return this._daxCapabilities;
			}
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00031EA3 File Offset: 0x000300A3
		internal DaxTransform ForkForIsolatedEvaluation()
		{
			return new DaxTransform(this._daxCapabilities, this._cultureInfo, this._cancellationToken);
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00031EBC File Offset: 0x000300BC
		public TranslationResult Translate(QueryCommandTree tree)
		{
			ArgumentValidation.CheckNotNull<QueryCommandTree>(tree, "tree");
			QueryExpression queryExpression = DaxTransform.ApplyDaxOptimizations(tree.Query);
			IReadOnlyList<QueryItemSourceLocation> readOnlyList;
			DaxExpression daxExpression = this.BuildOutputTable(queryExpression, out readOnlyList);
			IReadOnlyList<TranslationResultField> readOnlyList2 = daxExpression.ToResultFields(tree.GetRowResultType().Columns);
			return new TranslationResult(daxExpression.Text, readOnlyList2, readOnlyList);
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00031F08 File Offset: 0x00030108
		private static QueryExpression ApplyDaxOptimizations(QueryExpression root)
		{
			root = QueryAlgorithms.DaxScalarToIdentityComparisons(root);
			root = QueryAlgorithms.CountRowsToIsEmpty(root);
			return root;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00031F1C File Offset: 0x0003011C
		private DaxExpression BuildOutputTable(QueryExpression tableRoot, out IReadOnlyList<QueryItemSourceLocation> querySourceMap)
		{
			QueryBatchRootExpression queryBatchRootExpression = tableRoot as QueryBatchRootExpression;
			if (queryBatchRootExpression != null)
			{
				return this.BuildSingleResultBatchExpression(queryBatchRootExpression, out querySourceMap);
			}
			querySourceMap = null;
			QueryStartAtExpression queryStartAtExpression = tableRoot as QueryStartAtExpression;
			if (queryStartAtExpression != null)
			{
				return this.BuildStartAt(queryStartAtExpression);
			}
			QuerySortExpression querySortExpression = tableRoot as QuerySortExpression;
			if (querySortExpression != null)
			{
				return this.BuildOrderBy(querySortExpression);
			}
			return this.BuildEvaluate(tableRoot);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00031F6C File Offset: 0x0003016C
		private DaxExpression BuildSingleResultBatchExpression(QueryBatchRootExpression queryBatchRootExpression, out IReadOnlyList<QueryItemSourceLocation> querySourceMap)
		{
			ArgumentValidation.CheckNotNull<QueryBatchRootExpression>(queryBatchRootExpression, "queryBatchRootExpression");
			ArgumentValidation.CheckCondition(queryBatchRootExpression.OutputTables.Count == 1, "Only 1 output table is supported");
			DaxMultiPartBuilder daxMultiPartBuilder = new DaxMultiPartBuilder();
			DaxExpression daxExpression = this.BuildBatchContent(queryBatchRootExpression, daxMultiPartBuilder).Single<BatchTranslationTableResult>().DaxExpression;
			string text = daxMultiPartBuilder.ToCommandText(out querySourceMap);
			return daxExpression.ReplaceText(text);
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00031FC4 File Offset: 0x000301C4
		public BatchTranslationResult TranslateBatch(QueryCommandTree tree)
		{
			ArgumentValidation.CheckNotNull<QueryCommandTree>(tree, "tree");
			QueryExpression queryExpression = DaxTransform.ApplyDaxOptimizations(tree.Query);
			QueryBatchRootExpression queryBatchRootExpression = queryExpression as QueryBatchRootExpression;
			if (queryBatchRootExpression != null)
			{
				DaxMultiPartBuilder daxMultiPartBuilder = new DaxMultiPartBuilder();
				BatchTranslationTableResult[] array = this.BuildBatchContent(queryBatchRootExpression, daxMultiPartBuilder);
				IReadOnlyList<QueryItemSourceLocation> readOnlyList;
				return new BatchTranslationResult(daxMultiPartBuilder.ToCommandText(out readOnlyList), array, readOnlyList);
			}
			QueryExpressionWithLocalVariables queryExpressionWithLocalVariables = queryExpression as QueryExpressionWithLocalVariables;
			if (queryExpressionWithLocalVariables != null)
			{
				DaxExpression daxExpression = queryExpressionWithLocalVariables.Accept<DaxExpression>(this);
				IReadOnlyList<TranslationResultField> readOnlyList2 = daxExpression.ToResultFields(queryExpressionWithLocalVariables.GetRowResultType().Columns);
				BatchTranslationTableResult[] array2 = new BatchTranslationTableResult[]
				{
					new BatchTranslationTableResult(daxExpression, readOnlyList2)
				};
				return new BatchTranslationResult(daxExpression.Text, array2, null);
			}
			throw new DaxTranslationException(DevErrors.DaxTranslation.UnexpectedBatchRoot(queryExpression.GetType().Name));
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00032077 File Offset: 0x00030277
		private BatchTranslationTableResult[] BuildBatchContent(QueryBatchRootExpression batchRoot, DaxMultiPartBuilder builder)
		{
			this.PrepareQueryParameters(batchRoot.QueryParameters);
			this.BuildDeclarations(batchRoot.Declarations, builder);
			return this.BuildBatchOutputTables(batchRoot.OutputTables, builder);
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x000320A0 File Offset: 0x000302A0
		private void BuildDeclarations(IReadOnlyList<QueryBaseDeclarationExpression> declarations, DaxMultiPartBuilder builder)
		{
			if (declarations.IsNullOrEmpty<QueryBaseDeclarationExpression>())
			{
				return;
			}
			builder.Define(declarations.Count > 1);
			List<DaxExpressionWithSourceLocation> list = this.TransformDeclarations(declarations);
			foreach (DaxExpressionWithSourceLocation daxExpressionWithSourceLocation in list)
			{
				if (daxExpressionWithSourceLocation.SourceLocation != null)
				{
					builder.AddDefinition(daxExpressionWithSourceLocation.Expression, daxExpressionWithSourceLocation.SourceLocation);
				}
			}
			foreach (DaxExpressionWithSourceLocation daxExpressionWithSourceLocation2 in list)
			{
				if (daxExpressionWithSourceLocation2.SourceLocation == null)
				{
					builder.AddDefinition(daxExpressionWithSourceLocation2.Expression, daxExpressionWithSourceLocation2.SourceLocation);
				}
			}
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00032174 File Offset: 0x00030374
		private List<DaxExpressionWithSourceLocation> TransformDeclarations(IReadOnlyList<QueryBaseDeclarationExpression> declarations)
		{
			List<DaxExpressionWithSourceLocation> list = new List<DaxExpressionWithSourceLocation>(declarations.Count);
			foreach (QueryBaseDeclarationExpression queryBaseDeclarationExpression in declarations)
			{
				QueryFieldDeclarationExpression queryFieldDeclarationExpression = queryBaseDeclarationExpression as QueryFieldDeclarationExpression;
				if (queryFieldDeclarationExpression == null)
				{
					QueryMeasureDeclarationExpression queryMeasureDeclarationExpression = queryBaseDeclarationExpression as QueryMeasureDeclarationExpression;
					if (queryMeasureDeclarationExpression == null)
					{
						QueryVariableDeclarationExpression queryVariableDeclarationExpression = queryBaseDeclarationExpression as QueryVariableDeclarationExpression;
						if (queryVariableDeclarationExpression == null)
						{
							QueryTableDeclarationExpression queryTableDeclarationExpression = queryBaseDeclarationExpression as QueryTableDeclarationExpression;
							if (queryTableDeclarationExpression == null)
							{
								list.Add(new DaxExpressionWithSourceLocation(queryBaseDeclarationExpression.Accept<DaxExpression>(this), null));
							}
							else
							{
								list.AddRange(this.BuildTableDeclaration(queryTableDeclarationExpression));
							}
						}
						else
						{
							list.Add(this.BuildTopLevelVariableDeclaration(queryVariableDeclarationExpression));
						}
					}
					else
					{
						list.Add(this.BuildMeasureDeclaration(queryMeasureDeclarationExpression));
					}
				}
				else
				{
					list.Add(this.BuildFieldDeclaration(queryFieldDeclarationExpression));
				}
			}
			return list;
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0003224C File Offset: 0x0003044C
		private void PrepareQueryParameters(IReadOnlyList<QueryParameterDeclarationExpression> queryParameters)
		{
			if (queryParameters.Count == 0)
			{
				return;
			}
			this._queryParameters = new Dictionary<string, DaxExpression>(QueryNamingContext.NameComparer);
			for (int i = 0; i < queryParameters.Count; i++)
			{
				QueryParameterDeclarationExpression queryParameterDeclarationExpression = queryParameters[i];
				DaxExpression daxExpression = this.BuildQueryParameterReference(queryParameterDeclarationExpression);
				this._queryParameters.Add(queryParameterDeclarationExpression.Name, daxExpression);
			}
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x000322A8 File Offset: 0x000304A8
		private DaxExpression BuildQueryParameterReference(QueryParameterDeclarationExpression parameterDecl)
		{
			string text = DaxRef.Parameter(parameterDecl.Name);
			ConceptualResultType conceptualResultType = parameterDecl.ConceptualResultType;
			ConceptualTableType conceptualTableType = conceptualResultType as ConceptualTableType;
			if (conceptualTableType != null)
			{
				IReadOnlyList<DaxResultColumn> readOnlyList = this.CreateResultColumns(conceptualTableType.RowType.Columns);
				return DaxExpression.Table(text, readOnlyList, false);
			}
			if (!(conceptualResultType is ConceptualPrimitiveResultType))
			{
				throw new DaxTranslationException(DevErrors.DaxTranslation.UnsupportedQueryParameterResultType(parameterDecl.ConceptualResultType.ToString()));
			}
			return DaxExpression.Scalar(text);
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x00032314 File Offset: 0x00030514
		private DaxExpressionWithSourceLocation BuildTopLevelVariableDeclaration(QueryVariableDeclarationExpression varDecl)
		{
			DaxExpression daxExpression = varDecl.Accept<DaxExpression>(this);
			this._globalScopeVars.Add(varDecl.VariableName, daxExpression);
			return new DaxExpressionWithSourceLocation(daxExpression, null);
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x00032344 File Offset: 0x00030544
		private IReadOnlyList<DaxResultColumn> CreateResultColumns(IReadOnlyList<ConceptualTypeColumn> columns)
		{
			List<DaxResultColumn> list = new List<DaxResultColumn>(columns.Count);
			foreach (ConceptualTypeColumn conceptualTypeColumn in columns)
			{
				list.Add(new DaxResultColumn(conceptualTypeColumn.Name, DaxRef.Column(conceptualTypeColumn.Name)));
			}
			return list;
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x000323B0 File Offset: 0x000305B0
		private BatchTranslationTableResult[] BuildBatchOutputTables(IReadOnlyList<QueryExpression> tableExpressions, DaxMultiPartBuilder builder)
		{
			BatchTranslationTableResult[] array = new BatchTranslationTableResult[tableExpressions.Count];
			for (int i = 0; i < array.Length; i++)
			{
				QueryExpression queryExpression = tableExpressions[i];
				IReadOnlyList<QueryItemSourceLocation> readOnlyList;
				DaxExpression daxExpression = this.BuildOutputTable(queryExpression, out readOnlyList);
				builder.AddStatement(daxExpression);
				IReadOnlyList<TranslationResultField> readOnlyList2 = daxExpression.ToResultFields(queryExpression.GetRowResultType().Columns);
				array[i] = new BatchTranslationTableResult(daxExpression, readOnlyList2);
			}
			return array;
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x00032414 File Offset: 0x00030614
		internal TResult EvaluateInScope<TResult>(QueryExpressionBindingBase input, Func<DaxExpression, TResult> tableScopedFunc, bool wrapInputTableInKeepFilters = true)
		{
			DaxExpression daxExpression = input.Expression.Accept<DaxExpression>(this);
			if (wrapInputTableInKeepFilters && !(input.Expression is QueryCurrentGroupExpression))
			{
				daxExpression = DaxFunctions.KeepFilters(daxExpression);
			}
			this._scopeVars.Push(new global::System.ValueTuple<string, DaxExpression>(input.Variable.VariableName, daxExpression));
			TResult tresult = tableScopedFunc(daxExpression);
			this._scopeVars.Pop();
			return tresult;
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x00032474 File Offset: 0x00030674
		internal TResult EvaluateInScope<TResult>(IReadOnlyList<QueryVariableDeclarationExpression> inputs, Func<IReadOnlyList<DaxExpression>, TResult> scopedFunc)
		{
			List<DaxExpression> list = new List<DaxExpression>(inputs.Count);
			for (int i = 0; i < inputs.Count; i++)
			{
				QueryVariableDeclarationExpression queryVariableDeclarationExpression = inputs[i];
				DaxExpression daxExpression = queryVariableDeclarationExpression.Accept<DaxExpression>(this);
				this._scopeVars.Push(new global::System.ValueTuple<string, DaxExpression>(queryVariableDeclarationExpression.VariableName, daxExpression));
				list.Add(daxExpression);
			}
			TResult tresult = scopedFunc(list);
			for (int j = 0; j < list.Count; j++)
			{
				this._scopeVars.Pop();
			}
			return tresult;
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x000324F9 File Offset: 0x000306F9
		protected internal override DaxExpression Visit(QueryBatchRootExpression expression)
		{
			throw new DaxTranslationException("Unexpected QueryBatchRootExpression encountered.");
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x00032508 File Offset: 0x00030708
		protected internal override DaxExpression Visit(QueryAllExpression expression)
		{
			this.CheckForCancellation();
			if (expression.Target == null && expression.TargetEntity == null)
			{
				ConceptualTableType conceptualTableType = (ConceptualTableType)expression.ConceptualResultType;
				IReadOnlyList<DaxResultColumn> readOnlyList = this.CreateResultColumns(conceptualTableType.RowType.Columns);
				return DaxTransform.BuildAllForEmptyTable(expression.AllKind, readOnlyList);
			}
			if (expression.Fields == null && expression.Columns == null)
			{
				return DaxTransform.BuildAllForTable(expression);
			}
			return DaxTransform.BuildAllForColumn(expression);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x00032574 File Offset: 0x00030774
		private static DaxExpression BuildAllForColumn(QueryAllExpression expression)
		{
			IReadOnlyList<DaxResultColumn> readOnlyList;
			if (expression.Columns != null)
			{
				readOnlyList = expression.Columns.Select((IConceptualColumn c) => DaxResultColumn.FromColumn(c.ConceptualTypeColumn, expression.Target, expression.TargetEntity)).EvaluateReadOnly<DaxResultColumn>();
			}
			else
			{
				readOnlyList = expression.Fields.Select((EdmField f) => DaxResultColumn.FromColumn(f.Column, expression.Target, expression.TargetEntity)).EvaluateReadOnly<DaxResultColumn>();
			}
			QueryAllKind allKind = expression.AllKind;
			if (allKind == QueryAllKind.All)
			{
				return DaxFunctions.All(readOnlyList);
			}
			if (allKind != QueryAllKind.AllSelected)
			{
				throw new DaxTranslationException(DevErrors.DaxTranslation.UnexpectedAllKind(expression.AllKind.ToString()));
			}
			return DaxFunctions.AllSelected(readOnlyList);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x00032628 File Offset: 0x00030828
		private static DaxExpression BuildAllForTable(QueryAllExpression expression)
		{
			QueryAllKind allKind = expression.AllKind;
			if (allKind == QueryAllKind.All)
			{
				return DaxFunctions.All(DaxTransform.BuildTableScanCore(expression.Target, expression.TargetEntity));
			}
			if (allKind != QueryAllKind.AllSelected)
			{
				throw new DaxTranslationException(DevErrors.DaxTranslation.UnexpectedAllKind(expression.AllKind.ToString()));
			}
			return DaxFunctions.AllSelected(DaxTransform.BuildTableScanCore(expression.Target, expression.TargetEntity));
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00032691 File Offset: 0x00030891
		private static DaxExpression BuildAllForEmptyTable(QueryAllKind kind, IReadOnlyList<DaxResultColumn> columns)
		{
			if (kind == QueryAllKind.All)
			{
				return DaxFunctions.All(columns, true);
			}
			if (kind != QueryAllKind.AllSelected)
			{
				throw new DaxTranslationException(DevErrors.DaxTranslation.UnexpectedAllKind(kind.ToString()));
			}
			return DaxFunctions.AllSelected(columns, true);
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x000326C4 File Offset: 0x000308C4
		protected internal override DaxExpression Visit(QueryGenerateExpression expression)
		{
			this.CheckForCancellation();
			List<DaxExpression> list = expression.Inputs.Select((QueryExpressionBinding b) => b.Expression.Accept<DaxExpression>(this)).ToList<DaxExpression>();
			return this.CreateGenerateExpression(list, expression.GenerateKind);
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x00032704 File Offset: 0x00030904
		private DaxExpression CreateGenerateExpression(IList<DaxExpression> inputTables, QueryGenerateKind generateKind)
		{
			int count = inputTables.Count;
			DaxExpression daxExpression = inputTables[count - 1];
			for (int i = count - 2; i >= 0; i--)
			{
				if (generateKind != QueryGenerateKind.Generate)
				{
					if (generateKind != QueryGenerateKind.GenerateAll)
					{
						throw new DaxTranslationException(DevErrors.DaxTranslation.UnexpectedGenerateKind(generateKind.ToString()));
					}
					daxExpression = DaxFunctions.GenerateAll(DaxFunctions.KeepFilters(inputTables[i]), daxExpression);
				}
				else
				{
					daxExpression = DaxFunctions.Generate(DaxFunctions.KeepFilters(inputTables[i]), daxExpression);
				}
			}
			return daxExpression;
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0003277C File Offset: 0x0003097C
		protected internal override DaxExpression Visit(QueryCalculateExpression expression)
		{
			this.CheckForCancellation();
			DaxExpression daxExpression = expression.Argument.Accept<DaxExpression>(this);
			DaxExpression[] array = expression.Filters.Select((QueryExpression filterExpr) => this.TransformCalculateFilterExpression(filterExpr)).ToArray<DaxExpression>();
			if (expression.Argument.ConceptualResultType is ConceptualTableType)
			{
				return DaxFunctions.CalculateTable(daxExpression, array);
			}
			return DaxFunctions.Calculate(daxExpression, array);
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x000327DC File Offset: 0x000309DC
		private DaxExpression TransformCalculateFilterExpression(QueryExpression filterExpr)
		{
			DaxExpression daxExpression = filterExpr.Accept<DaxExpression>(this);
			if (filterExpr is QueryAllExpression)
			{
				return daxExpression;
			}
			return DaxFunctions.KeepFilters(daxExpression);
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x00032804 File Offset: 0x00030A04
		protected internal override DaxExpression Visit(QueryInExpression expression)
		{
			this.CheckForCancellation();
			bool flag = this._negationFunctionCount > 0 && !this._daxCapabilities.IsSupported(DaxFunctionKind.OptimizedNotInOperator);
			return DaxInTupleExpressionTranslator.Translate(expression, this, flag, this._cancellationToken);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00032844 File Offset: 0x00030A44
		protected internal override DaxExpression Visit(QueryTypeSafeFloorExpression expression)
		{
			this.CheckForCancellation();
			return DaxTypeSafeFloorExpressionTranslator.Translate(expression, this);
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00032854 File Offset: 0x00030A54
		protected internal override DaxExpression Visit(QueryInTableExpression expression)
		{
			this.CheckForCancellation();
			DaxExpression daxExpression = expression.LeftExpression.Accept<DaxExpression>(this);
			DaxExpression daxExpression2 = expression.RightExpression.Accept<DaxExpression>(this);
			return DaxOperators.In(daxExpression, daxExpression2);
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x00032888 File Offset: 0x00030A88
		protected internal override DaxExpression Visit(QueryComparisonExpression expression)
		{
			this.CheckForCancellation();
			DaxExpression daxExpression = expression.Left.Accept<DaxExpression>(this);
			DaxExpression daxExpression2 = expression.Right.Accept<DaxExpression>(this);
			switch (expression.ComparisonKind)
			{
			case QueryComparisonKind.Equals:
				return DaxOperators.Equal(daxExpression, daxExpression2);
			case QueryComparisonKind.NotEquals:
				return DaxOperators.NotEqual(daxExpression, daxExpression2);
			case QueryComparisonKind.GreaterThan:
				return DaxOperators.GreaterThan(daxExpression, daxExpression2);
			case QueryComparisonKind.GreaterThanOrEquals:
				return DaxOperators.GreaterThanOrEquals(daxExpression, daxExpression2);
			case QueryComparisonKind.LessThan:
				return DaxOperators.LessThan(daxExpression, daxExpression2);
			case QueryComparisonKind.LessThanOrEquals:
				return DaxOperators.LessThanOrEquals(daxExpression, daxExpression2);
			case QueryComparisonKind.EqualsIdentity:
				return DaxIdentityOperatorTranslator.EqualsIdentity(expression.Left, daxExpression, expression.Right, daxExpression2);
			case QueryComparisonKind.NotEqualsIdentity:
				return DaxIdentityOperatorTranslator.NotEqualsIdentity(expression.Left, daxExpression, expression.Right, daxExpression2);
			default:
				throw new DaxTranslationException(DevErrors.DaxTranslation.UnexpectedComparisonKind(expression.ComparisonKind.ToString()));
			}
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0003295A File Offset: 0x00030B5A
		protected internal override DaxExpression Visit(QueryCountRowsExpression expression)
		{
			this.CheckForCancellation();
			return DaxFunctions.CountRows(expression.Argument.Accept<DaxExpression>(this));
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x00032973 File Offset: 0x00030B73
		protected internal override DaxExpression Visit(QueryCrossJoinExpression expression)
		{
			this.CheckForCancellation();
			return DaxFunctions.CrossJoin(expression.Inputs.Select((QueryExpressionBinding b) => b.Expression.Accept<DaxExpression>(this)).ToArray<DaxExpression>());
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0003299C File Offset: 0x00030B9C
		protected internal override DaxExpression Visit(QueryDistinctExpression expression)
		{
			this.CheckForCancellation();
			return DaxFunctions.Distinct(expression.Argument.Accept<DaxExpression>(this));
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x000329B8 File Offset: 0x00030BB8
		protected internal override DaxExpression Visit(QueryDaxTextExpression expression)
		{
			this.CheckForCancellation();
			int num;
			return this.BuildDaxExternalContent(expression.Text, null, DaxInvalidExternalContentType.Inline, out num);
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x000329DC File Offset: 0x00030BDC
		private DaxExpression BuildDaxExternalContent(string externalDax, ScrubbedEntityPropertyReference itemReference, DaxInvalidExternalContentType contentType, out int externalContentLineCount)
		{
			DaxExternalContentCheckerResult daxExternalContentCheckerResult = DaxExternalContentChecker.Check(externalDax);
			if (!daxExternalContentCheckerResult.IsSafe)
			{
				throw new DaxInvalidExternalContentException("The query contains an invalid external DAX string.", daxExternalContentCheckerResult.ErrorCode, daxExternalContentCheckerResult.ErrorLine, daxExternalContentCheckerResult.ErrorPosition, itemReference, contentType);
			}
			externalContentLineCount = daxExternalContentCheckerResult.LineCount;
			return DaxExternalContent.DaxText(externalDax);
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x00032A26 File Offset: 0x00030C26
		protected internal override DaxExpression Visit(QueryExtensionExpression expression)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x00032A2D File Offset: 0x00030C2D
		protected internal override DaxExpression Visit(QueryStartAtExpression expression)
		{
			throw new DaxTranslationException("Unexpected QueryStartAtExpression encountered.");
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x00032A3C File Offset: 0x00030C3C
		protected internal override DaxExpression Visit(QueryFieldExpression expression)
		{
			return DaxExpression.Scalar(this.GetColumnRefFromFieldExpression(expression).ToString());
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x00032A64 File Offset: 0x00030C64
		protected internal override DaxExpression Visit(QueryFieldReferenceNameExpression expression)
		{
			this.CheckForCancellation();
			return DaxLiteral.FromString(expression.Table.Accept<DaxExpression>(this).GetResultColumnReference(expression.InternalFieldName).ToString());
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x00032AA1 File Offset: 0x00030CA1
		protected internal override DaxExpression Visit(QueryRelatedColumnExpression expression)
		{
			this.CheckForCancellation();
			return DaxFunctions.Related(this.GetColumnRefFromRelatedColumnExpression(expression));
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x00032AB5 File Offset: 0x00030CB5
		protected internal override DaxExpression Visit(QueryScalarEntityReferenceExpression expression)
		{
			throw new DaxTranslationException("Unexpected QueryScalarEntityReferenceExpression encountered.");
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00032AC4 File Offset: 0x00030CC4
		private DaxColumnRef GetColumnRefFromFieldExpression(QueryFieldExpression expression)
		{
			QueryScalarEntityReferenceExpression queryScalarEntityReferenceExpression = expression.Instance as QueryScalarEntityReferenceExpression;
			if (queryScalarEntityReferenceExpression != null)
			{
				return DaxRef.Column(expression.Column, queryScalarEntityReferenceExpression.Target, queryScalarEntityReferenceExpression.TargetEntity);
			}
			return this.ToFieldVariableReference(expression.Column, expression.Instance as QueryVariableReferenceExpression);
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00032B0F File Offset: 0x00030D0F
		private DaxColumnRef ToFieldVariableReference(ConceptualTypeColumn column, QueryVariableReferenceExpression varRef)
		{
			return this.ResolveVariableReference(varRef).Item2.GetResultColumnReference(column);
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00032B24 File Offset: 0x00030D24
		private DaxColumnRef GetColumnRefFromRelatedColumnExpression(QueryRelatedColumnExpression expression)
		{
			IConceptualColumn column = expression.Column;
			ConceptualTypeColumn conceptualTypeColumn = ((column != null) ? column.ConceptualTypeColumn : null) ?? expression.Field.Field.Column;
			EntitySet entity = expression.Field.Entity;
			IConceptualColumn column2 = expression.Column;
			return DaxRef.Column(conceptualTypeColumn, entity, (column2 != null) ? column2.Entity : null);
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x00032B80 File Offset: 0x00030D80
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "VariableName", "Expression" })]
		private global::System.ValueTuple<string, DaxExpression> ResolveVariableReference(QueryVariableReferenceExpression varRef)
		{
			DaxValidation.CheckCondition(varRef != null, "A variable reference was expected.");
			string varName = varRef.VariableName;
			DaxValidation.CheckCondition(this._scopeVars.Count > 0, DevErrors.DaxTranslation.VariableNotInScope(varName));
			global::System.ValueTuple<string, DaxExpression> valueTuple = this._scopeVars.FirstOrDefault(([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "VariableName", "Expression" })] global::System.ValueTuple<string, DaxExpression> scope) => scope.Item1 == varName);
			if (valueTuple.Item1 == null)
			{
				throw new DaxTranslationException(DevErrors.DaxTranslation.VariableNotInScope(varName));
			}
			return valueTuple;
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x00032BFC File Offset: 0x00030DFC
		protected internal override DaxExpression Visit(QueryFilterExpression expression)
		{
			this.CheckForCancellation();
			return this.EvaluateInScope<DaxExpression>(expression.Input, (DaxExpression inputTable) => DaxFunctions.Filter(inputTable, expression.Predicate.Accept<DaxExpression>(this)), true);
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x00032C44 File Offset: 0x00030E44
		protected internal override DaxExpression Visit(QueryFormatExpression expression)
		{
			this.CheckForCancellation();
			if (expression.Locale != null && !this._daxCapabilities.IsSupported(DaxFunctionKind.FormatByLocale))
			{
				throw new DaxTranslationException("The query uses format by locale expression. This is not supported by the underlying model.");
			}
			DaxExpression daxExpression = expression.Input.Accept<DaxExpression>(this);
			if (expression.Locale != null)
			{
				return DaxFunctions.Format(new DaxExpression[]
				{
					daxExpression,
					DaxLiteral.FromString(expression.FormatString),
					DaxLiteral.FromString(expression.Locale)
				});
			}
			return DaxFunctions.Format(new DaxExpression[]
			{
				daxExpression,
				DaxLiteral.FromString(expression.FormatString)
			});
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x00032CD8 File Offset: 0x00030ED8
		protected internal override DaxExpression Visit(QueryFunctionExpression expression)
		{
			this.CheckForCancellation();
			bool flag = expression.Function.FullName == "Core.Not";
			if (flag)
			{
				this._negationFunctionCount++;
			}
			QueryExpression queryExpression = this.HandleFunctionFallback(expression);
			DaxExpression daxExpression;
			if (queryExpression == expression)
			{
				daxExpression = DaxFunctionTransform.Create(expression, this).Translate();
			}
			else
			{
				daxExpression = queryExpression.Accept<DaxExpression>(this);
			}
			if (flag)
			{
				this._negationFunctionCount--;
			}
			return daxExpression;
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x00032D45 File Offset: 0x00030F45
		protected internal override DaxExpression Visit(QueryOperatorExpression expression)
		{
			this.CheckForCancellation();
			return DaxOperatorTransform.Create(expression, this).Translate();
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x00032D5C File Offset: 0x00030F5C
		private QueryExpression HandleFunctionFallback(QueryFunctionExpression expression)
		{
			if (!this._daxCapabilities.IsSupported(DaxFunctionKind.BinaryMinMax))
			{
				if (expression.Function.FullName == "Core.MinValue")
				{
					Func<QueryExpression, QueryExpression, QueryComparisonExpression> func;
					if ((func = DaxTransform.<>O.<0>__LessThan) == null)
					{
						func = (DaxTransform.<>O.<0>__LessThan = new Func<QueryExpression, QueryExpression, QueryComparisonExpression>(QueryExpressionBuilder.LessThan));
					}
					return DaxTransform.TranslateToIf(expression, func);
				}
				if (expression.Function.FullName == "Core.MaxValue")
				{
					Func<QueryExpression, QueryExpression, QueryComparisonExpression> func2;
					if ((func2 = DaxTransform.<>O.<1>__GreaterThan) == null)
					{
						func2 = (DaxTransform.<>O.<1>__GreaterThan = new Func<QueryExpression, QueryExpression, QueryComparisonExpression>(QueryExpressionBuilder.GreaterThan));
					}
					return DaxTransform.TranslateToIf(expression, func2);
				}
			}
			if (!this._daxCapabilities.IsSupported(DaxFunctionKind.Divide) && expression.Function.FullName == "Core.Divide")
			{
				return DaxTransform.TranslateDivideToIf(expression);
			}
			return expression;
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00032E18 File Offset: 0x00031018
		private static QueryExpression TranslateToIf(QueryFunctionExpression expression, Func<QueryExpression, QueryExpression, QueryComparisonExpression> comparisonFunc)
		{
			Contracts.Check(expression.Arguments.Count == 2, "Wrong argument count");
			QueryExpression queryExpression = expression.Arguments[0];
			QueryExpression queryExpression2 = expression.Arguments[1];
			return comparisonFunc(queryExpression, queryExpression2).If(queryExpression, queryExpression2);
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00032E68 File Offset: 0x00031068
		private static QueryExpression TranslateDivideToIf(QueryFunctionExpression expression)
		{
			Contracts.Check(expression.Arguments.Count == 2, "Wrong argument count");
			QueryExpression queryExpression = expression.Arguments[0];
			QueryExpression queryExpression2 = expression.Arguments[1];
			return queryExpression2.Equal(QueryExpressionBuilder.Literal(0)).If(queryExpression.DivideRaw(queryExpression2).ConceptualResultType.Null(), queryExpression.DivideRaw(queryExpression2));
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00032ED8 File Offset: 0x000310D8
		protected internal override DaxExpression Visit(QueryExtensionFunctionExpression expression)
		{
			this.CheckForCancellation();
			IList<DaxExpression> list = this.VisitAll(expression.Arguments).Evaluate<DaxExpression>();
			ConceptualTableType conceptualTableType = expression.ConceptualResultType as ConceptualTableType;
			if (conceptualTableType != null)
			{
				IReadOnlyList<DaxResultColumn> readOnlyList = this.CreateResultColumns(list, conceptualTableType.RowType.Columns, expression.ResultColumnLineage);
				return DaxFunctions.InvokeExtensionTableValuedFunction(expression.FunctionName, list, readOnlyList);
			}
			if (expression.ConceptualResultType is ConceptualPrimitiveResultType)
			{
				return DaxFunctions.InvokeExtensionScalarValuedFunction(expression.FunctionName, list);
			}
			throw new DaxTranslationException(DevErrors.DaxTranslation.UnsupportedExtensionFunctionResultType(expression.ConceptualResultType.ToString()));
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x00032F64 File Offset: 0x00031164
		private IReadOnlyList<DaxResultColumn> CreateResultColumns(IList<DaxExpression> arguments, IReadOnlyList<ConceptualTypeColumn> columns, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Name", "Index" })] IReadOnlyList<global::System.ValueTuple<string, int>> resultColumnLineage)
		{
			List<DaxResultColumn> list = new List<DaxResultColumn>(columns.Count);
			foreach (ConceptualTypeColumn conceptualTypeColumn in columns)
			{
				DaxResultColumn daxResultColumn;
				if (DaxTransform.TryResolveResultColumnLineage(arguments, conceptualTypeColumn, resultColumnLineage, out daxResultColumn))
				{
					list.Add(daxResultColumn);
				}
				else
				{
					list.Add(new DaxResultColumn(conceptualTypeColumn.Name, DaxRef.Column(conceptualTypeColumn.Name)));
				}
			}
			return list.ToReadOnlyList<DaxResultColumn>();
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00032FE8 File Offset: 0x000311E8
		private static bool TryResolveResultColumnLineage(IList<DaxExpression> arguments, ConceptualTypeColumn field, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Name", "Index" })] IReadOnlyList<global::System.ValueTuple<string, int>> resultColumnLineage, out DaxResultColumn resolvedColumn)
		{
			foreach (global::System.ValueTuple<string, int> valueTuple in resultColumnLineage)
			{
				if (QueryNamingContext.NameComparer.Equals(valueTuple.Item1, field.EdmName))
				{
					foreach (DaxResultColumn daxResultColumn in arguments[valueTuple.Item2].ResultColumns)
					{
						if (QueryNamingContext.NameComparer.Equals(daxResultColumn.QueryFieldName, field.EdmName))
						{
							resolvedColumn = daxResultColumn;
							return true;
						}
					}
					throw new DaxTranslationException(DevErrors.DaxTranslation.CouldNotResolveLineageForField(field.EdmName));
				}
			}
			resolvedColumn = default(DaxResultColumn);
			return false;
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x000330C8 File Offset: 0x000312C8
		protected internal override DaxExpression Visit(QueryGroupByExpression expression)
		{
			this.CheckForCancellation();
			DaxExpression daxExpression;
			if (DaxTransform.TryBuildGroupBySingleColumn(expression, out daxExpression))
			{
				return daxExpression;
			}
			bool flag = expression.Aggregates.Any((KeyValuePair<string, QueryExpression> e) => QueryAlgorithms.UsesCurrentGroup(e.Value));
			global::System.ValueTuple<DaxResultColumn, DaxExpression>[] aggregateColumns = new global::System.ValueTuple<DaxResultColumn, DaxExpression>[expression.Aggregates.Count];
			DaxExpression daxExpression2 = this.EvaluateInScope<DaxExpression>(expression.Input, delegate(DaxExpression table)
			{
				DaxUniqueNameGenerator daxUniqueNameGenerator = new DaxUniqueNameGenerator(table.ResultColumns.Select((DaxResultColumn c) => c.DaxColumnRef.ColumnName));
				for (int i = 0; i < aggregateColumns.Length; i++)
				{
					KeyValuePair<string, QueryExpression> keyValuePair = expression.Aggregates[i];
					DaxResultColumn daxResultColumn = new DaxResultColumn(keyValuePair.Key, DaxRef.Column(daxUniqueNameGenerator.MakeUniqueString(keyValuePair.Key)));
					aggregateColumns[i] = new global::System.ValueTuple<DaxResultColumn, DaxExpression>(daxResultColumn, keyValuePair.Value.Accept<DaxExpression>(this));
				}
				return table;
			}, aggregateColumns.Length != 0 && !flag);
			if (flag)
			{
				return this.BuildGroupByWithCurrentGroup(expression, aggregateColumns, daxExpression2);
			}
			return DaxTransform.BuildGroupByStandard(expression, aggregateColumns, daxExpression2);
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x000331A4 File Offset: 0x000313A4
		private static bool TryBuildGroupBySingleColumn(QueryGroupByExpression expression, out DaxExpression dax)
		{
			QueryScanExpression queryScanExpression = expression.Input.Expression as QueryScanExpression;
			DaxResultColumn daxResultColumn;
			if (queryScanExpression != null && DaxTransform.TryBuildGroupBySingleResultColumn(expression, queryScanExpression.Target, queryScanExpression.TargetEntity, out daxResultColumn))
			{
				if (queryScanExpression.ExcludeBlankRow)
				{
					dax = DaxFunctions.Distinct(daxResultColumn);
				}
				else
				{
					dax = DaxFunctions.Values(daxResultColumn);
				}
				return true;
			}
			QueryAllExpression queryAllExpression = expression.Input.Expression as QueryAllExpression;
			if (queryAllExpression != null && DaxTransform.TryBuildGroupBySingleResultColumn(expression, queryAllExpression.Target, queryAllExpression.TargetEntity, out daxResultColumn))
			{
				dax = DaxFunctions.All(daxResultColumn.AsList<DaxResultColumn>());
				return true;
			}
			dax = null;
			return false;
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00033234 File Offset: 0x00031434
		private static bool TryBuildGroupBySingleResultColumn(QueryGroupByExpression expression, EntitySet target, IConceptualEntity targetEntity, out DaxResultColumn resultColumn)
		{
			if (expression.GroupItems.Count == 1 && expression.Aggregates.Count == 0)
			{
				CompositeKeyGroupItem compositeKeyGroupItem = expression.GroupItems[0] as CompositeKeyGroupItem;
				if (compositeKeyGroupItem != null && compositeKeyGroupItem.Keys.Count == 1)
				{
					KeyValuePair<string, QueryExpression> keyValuePair = compositeKeyGroupItem.Keys[0];
					ConceptualTypeColumn groupKeyColumn = DaxTransform.GetGroupKeyColumn(expression.Input, keyValuePair.Value);
					if (groupKeyColumn != null)
					{
						resultColumn = new DaxResultColumn(keyValuePair.Key, DaxRef.Column(groupKeyColumn, target, targetEntity));
						return true;
					}
				}
			}
			resultColumn = default(DaxResultColumn);
			return false;
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x000332C8 File Offset: 0x000314C8
		private static DaxExpression BuildGroupByStandard(QueryGroupByExpression expression, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "ResultColumn", "Expression" })] global::System.ValueTuple<DaxResultColumn, DaxExpression>[] aggregateColumns, DaxExpression inputTable)
		{
			if (expression.GroupItems.Count == 0)
			{
				return DaxFunctions.CalculateTable(DaxFunctions.Row(aggregateColumns), new DaxExpression[] { inputTable });
			}
			List<IDaxGroupItem> list = DaxTransform.BuildGroupByItems(expression, inputTable, true);
			return DaxFunctions.Summarize(inputTable, list.ToArray(), aggregateColumns);
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00033310 File Offset: 0x00031510
		private static List<IDaxGroupItem> BuildGroupByItems(QueryGroupByExpression expression, DaxExpression inputTable, bool allowRollup)
		{
			List<IDaxGroupItem> list = new List<IDaxGroupItem>();
			foreach (IGroupItem groupItem in expression.GroupItems)
			{
				CompositeKeyGroupItem compositeKeyGroupItem = groupItem as CompositeKeyGroupItem;
				RollupGroupItem rollupGroupItem = groupItem as RollupGroupItem;
				if (compositeKeyGroupItem != null)
				{
					list.AddRange(DaxTransform.GetDaxGroupColumns(expression.Input, inputTable, compositeKeyGroupItem).Cast<IDaxGroupItem>());
				}
				else
				{
					if (rollupGroupItem == null || !allowRollup)
					{
						throw new DaxTranslationException("Unexpected group item encountered.");
					}
					List<IDaxGroupItem> list2 = new List<IDaxGroupItem>();
					foreach (CompositeKeyGroupItem compositeKeyGroupItem2 in rollupGroupItem.GroupItems)
					{
						DaxResultColumn[] array = DaxTransform.GetDaxGroupColumns(expression.Input, inputTable, compositeKeyGroupItem2).ToArray<DaxResultColumn>();
						if (array.Length == 1)
						{
							list2.Add(array[0]);
						}
						else
						{
							list2.Add(DaxFunctions.RollupGroup(array, null));
						}
					}
					list.Add(DaxFunctions.Rollup(list2.ToArray(), null));
				}
			}
			return list;
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00033440 File Offset: 0x00031640
		private static IDaxGroupItem[] BuildGroupByItems(QueryProjectExpression expression, QueryNewInstanceExpression projection, DaxExpression inputTable)
		{
			CompositeKeyGroupItem compositeKeyGroupItem = new CompositeKeyGroupItem(projection.Arguments);
			return DaxTransform.GetDaxGroupColumns(expression.Input, inputTable, compositeKeyGroupItem).Cast<IDaxGroupItem>().ToArray<IDaxGroupItem>();
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x00033470 File Offset: 0x00031670
		private static IEnumerable<DaxResultColumn> GetDaxGroupColumns(QueryGroupExpressionBinding groupByInput, DaxExpression inputTable, CompositeKeyGroupItem compositeKey)
		{
			foreach (KeyValuePair<string, QueryExpression> keyValuePair in compositeKey.Keys)
			{
				ConceptualTypeColumn groupKeyColumn = DaxTransform.GetGroupKeyColumn(groupByInput, keyValuePair.Value);
				DaxValidation.CheckCondition(groupKeyColumn != null, "The specified group key expression is not valid for DAX translation.");
				yield return new DaxResultColumn(keyValuePair.Key, inputTable.GetResultColumnReference(groupKeyColumn));
			}
			IEnumerator<KeyValuePair<string, QueryExpression>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00033490 File Offset: 0x00031690
		private static ConceptualTypeColumn GetGroupKeyColumn(QueryGroupExpressionBinding groupByInput, QueryExpression groupKeyExpr)
		{
			QueryFieldExpression queryFieldExpression = groupKeyExpr as QueryFieldExpression;
			if (queryFieldExpression != null && queryFieldExpression.Instance.Equals(groupByInput.Variable))
			{
				return queryFieldExpression.Column;
			}
			return null;
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x000334C2 File Offset: 0x000316C2
		private static IEnumerable<DaxResultColumn> GetDaxGroupColumns(QueryExpressionBinding groupByInput, DaxExpression inputTable, CompositeKeyGroupItem compositeKey)
		{
			foreach (KeyValuePair<string, QueryExpression> keyValuePair in compositeKey.Keys)
			{
				ConceptualTypeColumn groupKeyColumn = DaxTransform.GetGroupKeyColumn(groupByInput, keyValuePair.Value);
				DaxValidation.CheckCondition(groupKeyColumn != null, "The specified group key expression is not valid for DAX translation.");
				yield return new DaxResultColumn(keyValuePair.Key, inputTable.GetResultColumnReference(groupKeyColumn));
			}
			IEnumerator<KeyValuePair<string, QueryExpression>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x000334E0 File Offset: 0x000316E0
		private static ConceptualTypeColumn GetGroupKeyColumn(QueryExpressionBinding groupByInput, QueryExpression groupKeyExpr)
		{
			QueryFieldExpression queryFieldExpression = groupKeyExpr as QueryFieldExpression;
			if (queryFieldExpression != null && queryFieldExpression.Instance.Equals(groupByInput.Variable))
			{
				return queryFieldExpression.Column;
			}
			return null;
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00033514 File Offset: 0x00031714
		private DaxExpression BuildGroupByWithCurrentGroup(QueryGroupByExpression expression, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "ResultColumn", "Expression" })] global::System.ValueTuple<DaxResultColumn, DaxExpression>[] aggregateColumns, DaxExpression inputTable)
		{
			DaxValidation.CheckCondition(this._daxCapabilities.IsSupported(DaxFunctionKind.GroupBy), DevErrors.DaxTranslation.UnexpectedFunction(DaxFunctionKind.GroupBy.ToString()));
			List<IDaxGroupItem> list = DaxTransform.BuildGroupByItems(expression, inputTable, false);
			return DaxFunctions.GroupBy(inputTable, list.ToArray(), aggregateColumns);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0003355C File Offset: 0x0003175C
		protected internal override DaxExpression Visit(QueryCurrentGroupExpression expression)
		{
			this.CheckForCancellation();
			DaxValidation.CheckCondition(this._daxCapabilities.IsSupported(DaxFunctionKind.GroupBy), "The function 'CurrentGroup' can only be used with 'GroupBy' which is not supported.");
			return DaxFunctions.CurrentGroup(this.ResolveVariableReference(expression.Input).Item2.ResultColumns);
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x00033595 File Offset: 0x00031795
		protected internal override DaxExpression Visit(QueryDataSourceVariablesDeclarationExpression declarationExpression)
		{
			this.CheckForCancellation();
			return DaxStatements.DefineDataSourceVariable(declarationExpression.Expression.Accept<DaxExpression>(this));
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x000335B0 File Offset: 0x000317B0
		protected internal override DaxExpression Visit(QueryMParameterDeclarationExpression expression)
		{
			this.CheckForCancellation();
			DaxExpression daxExpression = expression.Expression.Accept<DaxExpression>(this);
			return DaxStatements.DefineMParameters(expression.ParameterName, daxExpression);
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x000335DC File Offset: 0x000317DC
		protected internal override DaxExpression Visit(QueryAddMissingItemsExpression expression)
		{
			this.CheckForCancellation();
			IEnumerable<DaxExpression> showAllExprs = null;
			IEnumerable<DaxExpression> groups = null;
			Func<IAddMissingItemsGroupItem, IEnumerable<DaxExpression>> <>9__1;
			DaxExpression daxExpression = this.EvaluateInScope<DaxExpression>(expression.Table, delegate(DaxExpression tableExpr)
			{
				showAllExprs = this.VisitAll(expression.ShowAllColumns).Evaluate<DaxExpression>();
				IEnumerable<IAddMissingItemsGroupItem> groups2 = expression.Groups;
				Func<IAddMissingItemsGroupItem, IEnumerable<DaxExpression>> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (IAddMissingItemsGroupItem g) => this.BuildAddMissingItemsGroupItem(g));
				}
				groups = groups2.SelectMany(func).Evaluate<DaxExpression>();
				return tableExpr;
			}, false);
			IEnumerable<DaxExpression> enumerable = this.VisitAll(expression.ContextTables);
			return DaxFunctions.AddMissingItems(showAllExprs, daxExpression, groups, enumerable);
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x00033658 File Offset: 0x00031858
		private IEnumerable<DaxExpression> BuildAddMissingItemsGroupItem(IAddMissingItemsGroupItem item)
		{
			AddMissingItemsRollup addMissingItemsRollup = item as AddMissingItemsRollup;
			if (addMissingItemsRollup != null)
			{
				return this.BuildAddMissingItemsRollup(addMissingItemsRollup);
			}
			AddMissingItemsGroupWithSubtotal addMissingItemsGroupWithSubtotal = item as AddMissingItemsGroupWithSubtotal;
			if (addMissingItemsGroupWithSubtotal != null)
			{
				return this.BuildAddMissingItemsGroupWithSubtotal(addMissingItemsGroupWithSubtotal);
			}
			AddMissingItemsGroup addMissingItemsGroup = item as AddMissingItemsGroup;
			if (addMissingItemsGroup != null)
			{
				return this.BuildAddMissingItemsGroup(addMissingItemsGroup);
			}
			throw new DaxTranslationException("Unexpected group item encountered.");
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x000336A5 File Offset: 0x000318A5
		private IEnumerable<DaxExpression> BuildAddMissingItemsGroup(AddMissingItemsGroup group)
		{
			return group.Keys.Select((QueryExpression k) => k.Accept<DaxExpression>(this));
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x000336BE File Offset: 0x000318BE
		private IEnumerable<DaxExpression> BuildAddMissingItemsGroupWithSubtotal(AddMissingItemsGroupWithSubtotal group)
		{
			yield return DaxFunctions.RollupGroup(this.BuildAddMissingItemsGroup(group.Group).ToArray<DaxExpression>());
			yield return group.SubtotalIndicator.Accept<DaxExpression>(this);
			if (group.ContextTables != null)
			{
				foreach (QueryExpression queryExpression in group.ContextTables)
				{
					yield return queryExpression.Accept<DaxExpression>(this);
				}
				IEnumerator<QueryExpression> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x000336D8 File Offset: 0x000318D8
		private IEnumerable<DaxExpression> BuildAddMissingItemsRollup(AddMissingItemsRollup rollup)
		{
			IEnumerable<DaxExpression> enumerable = rollup.Groups.SelectMany((AddMissingItemsGroupWithSubtotal g) => this.BuildAddMissingItemsGroupWithSubtotal(g));
			IEnumerable<DaxExpression> enumerable2;
			if (rollup.ContextTables.IsNullOrEmpty<QueryExpression>())
			{
				enumerable2 = enumerable;
			}
			else
			{
				enumerable2 = rollup.ContextTables.Select((QueryExpression c) => c.Accept<DaxExpression>(this)).Concat(enumerable);
			}
			return Microsoft.Reporting.Util.AsEnumerable<DaxExpression>(DaxFunctions.RollupIsSubtotal(enumerable2.ToArray<DaxExpression>()));
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0003373E File Offset: 0x0003193E
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Expression", "Direction" })]
		private global::System.ValueTuple<DaxExpression, SortDirection> TranslateSortClause(QuerySortClause sort)
		{
			return new global::System.ValueTuple<DaxExpression, SortDirection>(sort.Expression.Accept<DaxExpression>(this), sort.Direction);
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x00033758 File Offset: 0x00031958
		protected internal override DaxExpression Visit(QueryGroupAndJoinExpression expression)
		{
			this.CheckForCancellation();
			DaxValidation.CheckCondition(this._daxCapabilities.IsSupported(DaxFunctionKind.SummarizeColumns), "QueryGroupAndJoinExpression cannot be used when SummarizeColumns is not supported.");
			global::System.ValueTuple<DaxResultColumn, DaxExpression>[] array = new global::System.ValueTuple<DaxResultColumn, DaxExpression>[expression.AdditionalColumns.Count];
			for (int i = 0; i < array.Length; i++)
			{
				QueryGroupAndJoinAdditionalColumn queryGroupAndJoinAdditionalColumn = expression.AdditionalColumns[i];
				DaxResultColumn daxResultColumn = new DaxResultColumn(queryGroupAndJoinAdditionalColumn.Name, DaxRef.Column(queryGroupAndJoinAdditionalColumn.Name));
				DaxExpression daxExpression = queryGroupAndJoinAdditionalColumn.Expression.Accept<DaxExpression>(this);
				if (queryGroupAndJoinAdditionalColumn.SuppressJoinPredicate)
				{
					daxExpression = DaxFunctions.Ignore(daxExpression);
				}
				array[i] = new global::System.ValueTuple<DaxResultColumn, DaxExpression>(daxResultColumn, daxExpression);
			}
			List<IDaxGroupItem> list = new List<IDaxGroupItem>();
			foreach (IGroupItem groupItem in expression.GroupItems)
			{
				CompositeKeyGroupItem compositeKeyGroupItem = groupItem as CompositeKeyGroupItem;
				if (compositeKeyGroupItem != null)
				{
					list.AddRange(this.GetGroupAndJoinGrouping(compositeKeyGroupItem));
				}
				else
				{
					RollupAddIsSubtotalGroupItem rollupAddIsSubtotalGroupItem = groupItem as RollupAddIsSubtotalGroupItem;
					if (rollupAddIsSubtotalGroupItem == null)
					{
						throw new DaxTranslationException("Unexpected group item encountered.");
					}
					list.Add(this.GetRollupGrouping(rollupAddIsSubtotalGroupItem));
				}
			}
			IEnumerable<DaxExpression> enumerable = expression.ContextTables.Select((QueryExpression c) => c.Accept<DaxExpression>(this));
			return DaxFunctions.SummarizeColumns(list.ToArray(), enumerable, array);
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x000338A8 File Offset: 0x00031AA8
		private IEnumerable<IDaxGroupItem> GetGroupAndJoinGrouping(CompositeKeyGroupItem compositeKey)
		{
			List<IDaxGroupItem> list = new List<IDaxGroupItem>(compositeKey.Keys.Count);
			foreach (KeyValuePair<string, QueryExpression> keyValuePair in compositeKey.Keys)
			{
				list.Add(this.BuildScalarEntityFieldDaxResultColumn(keyValuePair.Key, keyValuePair.Value));
			}
			return list;
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00033920 File Offset: 0x00031B20
		private IList<DaxResultColumn> BuildScalarEntityFieldDaxResultColumns(IReadOnlyList<KeyValuePair<string, QueryExpression>> queryExpressions)
		{
			List<DaxResultColumn> list = new List<DaxResultColumn>(queryExpressions.Count);
			foreach (KeyValuePair<string, QueryExpression> keyValuePair in queryExpressions)
			{
				list.Add(this.BuildScalarEntityFieldDaxResultColumn(keyValuePair.Key, keyValuePair.Value));
			}
			return list;
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00033988 File Offset: 0x00031B88
		private DaxResultColumn BuildScalarEntityFieldDaxResultColumn(string name, QueryExpression expr)
		{
			QueryFieldExpression queryFieldExpression = expr as QueryFieldExpression;
			DaxValidation.CheckCondition(queryFieldExpression != null, "The specified group key expression is not valid for DAX translation.");
			QueryScalarEntityReferenceExpression queryScalarEntityReferenceExpression = queryFieldExpression.Instance as QueryScalarEntityReferenceExpression;
			DaxValidation.CheckCondition(queryScalarEntityReferenceExpression != null, "The specified group key expression is not valid for DAX translation.");
			DaxColumnRef daxColumnRef = DaxRef.Column(queryFieldExpression.Column, queryScalarEntityReferenceExpression.Target, queryScalarEntityReferenceExpression.TargetEntity);
			return new DaxResultColumn(name, daxColumnRef);
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x000339E4 File Offset: 0x00031BE4
		private IDaxGroupItem GetRollupGrouping(RollupAddIsSubtotalGroupItem rollup)
		{
			List<IDaxGroupItem> list = new List<IDaxGroupItem>(rollup.GroupItems.Count);
			foreach (NamedRollupGroupItem namedRollupGroupItem in rollup.GroupItems)
			{
				List<IDaxGroupItem> list2 = new List<IDaxGroupItem>();
				list2.AddRange(this.GetGroupAndJoinGrouping(namedRollupGroupItem.GroupKeysItem));
				IDaxGroupItem[] array = list2.ToArray();
				DaxExpression[] array2 = ((namedRollupGroupItem.ContextTables != null) ? namedRollupGroupItem.ContextTables.Select((QueryExpression c) => c.Accept<DaxExpression>(this)).ToArray<DaxExpression>() : null);
				IDaxGroupItem daxGroupItem = DaxFunctions.NamedRollupGroup(array, namedRollupGroupItem.SubtotalIndicatorColumnName, array2);
				list.Add(daxGroupItem);
			}
			DaxExpression[] array3 = ((rollup.ContextTables != null) ? rollup.ContextTables.Select((QueryExpression c) => c.Accept<DaxExpression>(this)).ToArray<DaxExpression>() : null);
			return DaxFunctions.RollupAddIsSubtotal(list.ToArray(), array3);
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00033ACC File Offset: 0x00031CCC
		protected internal override DaxExpression Visit(QueryIsAggregateExpression expression)
		{
			this.CheckForCancellation();
			QueryFieldExpression queryFieldExpression = expression.Argument as QueryFieldExpression;
			if (queryFieldExpression != null)
			{
				return DaxFunctions.IsSubtotal(this.GetColumnRefFromFieldExpression(queryFieldExpression));
			}
			throw new DaxTranslationException("Unexpected QueryIsAggregateExpression encountered.");
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00033B05 File Offset: 0x00031D05
		protected internal override DaxExpression Visit(QueryIsNullExpression expression)
		{
			this.CheckForCancellation();
			return DaxFunctions.IsBlank(expression.Argument.Accept<DaxExpression>(this));
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x00033B1E File Offset: 0x00031D1E
		protected internal override DaxExpression Visit(QueryIsEmptyExpression expression)
		{
			this.CheckForCancellation();
			if (this._daxCapabilities.IsSupported(DaxFunctionKind.IsEmpty))
			{
				return DaxFunctions.IsEmpty(expression.Argument.Accept<DaxExpression>(this));
			}
			return DaxFunctions.IsBlank(DaxFunctions.CountRows(expression.Argument.Accept<DaxExpression>(this)));
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00033B5C File Offset: 0x00031D5C
		protected internal override DaxExpression Visit(QueryDateDiffExpression expression)
		{
			this.CheckForCancellation();
			return DaxFunctions.DateDiff(expression.StartDate.Accept<DaxExpression>(this), expression.EndDate.Accept<DaxExpression>(this), expression.TimeUnit);
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00033B88 File Offset: 0x00031D88
		protected internal override DaxExpression Visit(QuerySampleAxisWithLocalMinMaxExpression expression)
		{
			this.CheckForCancellation();
			DaxExpression daxExpression = expression.Input.Expression.Accept<DaxExpression>(this);
			DaxExpression daxExpression2 = expression.MaxTargetPointCount.Accept<DaxExpression>(this);
			DaxColumnRef daxColumnRef = this.TransformToDaxColumnReference(daxExpression, expression.Axis);
			IReadOnlyList<DaxColumnRef> readOnlyList = this.TransformToDaxColumnReferences(daxExpression, expression.Measures);
			DaxExpression daxExpression3 = expression.MinPointsResolution.Accept<DaxExpression>(this);
			IReadOnlyList<DaxColumnRef> readOnlyList2 = this.TransformToDaxColumnReferences(daxExpression, expression.Series);
			DaxExpression daxExpression4 = expression.MaxPointsResolution.Accept<DaxExpression>(this);
			DaxExpression daxExpression5 = expression.MaxDynamicSeriesCount.Accept<DaxExpression>(this);
			return DaxFunctions.SampleAxisWithLocalMinMax(daxExpression2, daxExpression, daxColumnRef, readOnlyList, daxExpression3, readOnlyList2, expression.DynamicSeriesSelectionCriteria, expression.DynamicSeriesSelectionCriteriaOrder, daxExpression4, daxExpression5);
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00033C28 File Offset: 0x00031E28
		protected internal override DaxExpression Visit(QuerySampleCartesianPointsByCoverExpression expression)
		{
			this.CheckForCancellation();
			DaxExpression xDaxReference = null;
			DaxExpression yDaxReference = null;
			DaxExpression radiusDaxReference = null;
			DaxExpression daxExpression = this.EvaluateInScope<DaxExpression>(expression.Input, delegate(DaxExpression table)
			{
				xDaxReference = this.VisitOptionalExpression(expression.X);
				yDaxReference = this.VisitOptionalExpression(expression.Y);
				radiusDaxReference = this.VisitOptionalExpression(expression.Radius);
				return table;
			}, false);
			DaxExpression daxExpression2 = this.VisitOptionalExpression(expression.MaxTargetPointCount);
			DaxExpression daxExpression3 = this.VisitOptionalExpression(expression.MaxMinRatio);
			DaxExpression daxExpression4 = this.VisitOptionalExpression(expression.MaxBlankRatio);
			return DaxFunctions.SampleCartesianPointsByCover(daxExpression2, daxExpression, xDaxReference, yDaxReference, radiusDaxReference, daxExpression3, daxExpression4);
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00033CD2 File Offset: 0x00031ED2
		protected internal override DaxExpression Visit(QueryTopNPerLevelSampleExpression expression)
		{
			this.CheckForCancellation();
			return DaxTopNPerLevelTranslator.Translate(expression, this);
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00033CE1 File Offset: 0x00031EE1
		private DaxExpression VisitOptionalExpression(QueryExpression expression)
		{
			this.CheckForCancellation();
			if (expression == null)
			{
				return null;
			}
			return expression.Accept<DaxExpression>(this);
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00033CF8 File Offset: 0x00031EF8
		private DaxColumnRef TransformToDaxColumnReference(DaxExpression inputDax, QueryExpression expression)
		{
			QueryFieldExpression queryFieldExpression = expression as QueryFieldExpression;
			DaxValidation.CheckCondition(queryFieldExpression != null, "Unexpected expression encountered, expected to be field expression.");
			return inputDax.GetResultColumnReference(queryFieldExpression.Column);
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00033D28 File Offset: 0x00031F28
		private IReadOnlyList<DaxColumnRef> TransformToDaxColumnReferences(DaxExpression inputDax, IReadOnlyList<QueryExpression> expressions)
		{
			if (expressions == null)
			{
				return null;
			}
			List<DaxColumnRef> list = new List<DaxColumnRef>(expressions.Count);
			foreach (QueryExpression queryExpression in expressions)
			{
				list.Add(this.TransformToDaxColumnReference(inputDax, queryExpression));
			}
			return list;
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00033D8C File Offset: 0x00031F8C
		protected internal override DaxExpression Visit(QueryIsOnOrAfterExpression expression)
		{
			this.CheckForCancellation();
			if (!this._daxCapabilities.IsSupported(DaxFunctionKind.IsOnOrAfter))
			{
				return QueryAlgorithms.IsOnOrAfterToComparisons(expression).Accept<DaxExpression>(this);
			}
			return DaxFunctions.IsOnOrAfter(expression.Arguments.Select(new Func<QueryIsOnOrAfterArgument, Tuple<DaxExpression, DaxExpression, SortDirection>>(this.Visit)));
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00033DCB File Offset: 0x00031FCB
		private Tuple<DaxExpression, DaxExpression, SortDirection> Visit(QueryIsOnOrAfterArgument argument)
		{
			this.CheckForCancellation();
			return Tuple.Create<DaxExpression, DaxExpression, SortDirection>(argument.Left.Accept<DaxExpression>(this), argument.Right.Accept<DaxExpression>(this), argument.Direction);
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00033DF6 File Offset: 0x00031FF6
		protected internal override DaxExpression Visit(QueryIsAfterExpression expression)
		{
			this.CheckForCancellation();
			if (!this._daxCapabilities.IsSupported(DaxFunctionKind.IsAfter))
			{
				return QueryAlgorithms.IsAfterToComparisons(expression).Accept<DaxExpression>(this);
			}
			return DaxFunctions.IsAfter(expression.Arguments.Select(new Func<QueryIsOnOrAfterArgument, Tuple<DaxExpression, DaxExpression, SortDirection>>(this.Visit)));
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00033E38 File Offset: 0x00032038
		protected internal override DaxExpression Visit(QuerySwitchExpression expression)
		{
			this.CheckForCancellation();
			IReadOnlyList<QuerySwitchCase> cases = expression.Cases;
			QueryExpression defaultResult = expression.DefaultResult;
			int num = cases.Count * 2;
			num += ((defaultResult != null) ? 2 : 1);
			DaxExpression[] array = new DaxExpression[num];
			array[0] = expression.Input.Accept<DaxExpression>(this);
			for (int i = 0; i < cases.Count; i++)
			{
				QuerySwitchCase querySwitchCase = cases[i];
				array[i * 2 + 1] = querySwitchCase.Value.Accept<DaxExpression>(this);
				array[i * 2 + 2] = querySwitchCase.Result.Accept<DaxExpression>(this);
			}
			if (defaultResult != null)
			{
				array[num - 1] = defaultResult.Accept<DaxExpression>(this);
			}
			return DaxFunctions.Switch(array);
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00033EE0 File Offset: 0x000320E0
		protected internal override DaxExpression Visit(QueryTupleExpression expression)
		{
			this.CheckForCancellation();
			return this.TransformTuple<KeyValuePair<string, QueryExpression>>(expression.NamedColumns, (KeyValuePair<string, QueryExpression> kvp) => kvp.Value);
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x00033F14 File Offset: 0x00032114
		internal DaxExpression TransformTuple<T>(IReadOnlyList<T> columns, Func<T, QueryExpression> selector)
		{
			DaxDataTableRowStringBuilder daxDataTableRowStringBuilder = new DaxDataTableRowStringBuilder(columns.Count);
			daxDataTableRowStringBuilder.Begin();
			for (int i = 0; i < columns.Count; i++)
			{
				T t = columns[i];
				DaxExpression daxExpression = selector(t).Accept<DaxExpression>(this);
				daxDataTableRowStringBuilder.AppendColumn(daxExpression.Text);
			}
			daxDataTableRowStringBuilder.End();
			return DaxExpression.Tuple(daxDataTableRowStringBuilder.ToDax());
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00033F78 File Offset: 0x00032178
		protected internal override DaxExpression Visit(QueryDataTableExpression expression)
		{
			this.CheckForCancellation();
			IReadOnlyList<QueryTupleExpression> rows = expression.Rows;
			int count = rows[0].NamedColumns.Count;
			List<DaxResultColumn> list = new List<DaxResultColumn>(rows[0].NamedColumns.Count);
			DaxDataTableStringBuilder daxDataTableStringBuilder = new DaxDataTableStringBuilder(count);
			daxDataTableStringBuilder.Begin();
			for (int i = 0; i < rows.Count; i++)
			{
				IEnumerable<KeyValuePair<string, QueryExpression>> namedColumns = rows[i].NamedColumns;
				daxDataTableStringBuilder.BeginRow();
				foreach (KeyValuePair<string, QueryExpression> keyValuePair in namedColumns)
				{
					DaxExpression daxExpression = keyValuePair.Value.Accept<DaxExpression>(this);
					daxDataTableStringBuilder.AppendColumn(daxExpression.Text);
					if (i == 0)
					{
						string key = keyValuePair.Key;
						list.Add(new DaxResultColumn(key, new DaxColumnRef(key, DaxTableRef.Empty)));
					}
				}
				daxDataTableStringBuilder.EndRow();
			}
			daxDataTableStringBuilder.End();
			return DaxExpression.Table(daxDataTableStringBuilder.ToDax(), list, false);
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00034084 File Offset: 0x00032284
		protected internal override DaxExpression Visit(QueryLiteralExpression expression)
		{
			this.CheckForCancellation();
			if (!(expression.ConceptualResultType is ConceptualPrimitiveResultType))
			{
				DaxValidation.Fail(DevErrors.DaxTranslation.UnexpectedLiteralResultType(expression.ConceptualResultType.ToString()));
			}
			ConceptualPrimitiveType value = expression.ConceptualResultType.GetPrimitiveTypeKind().Value;
			object value2 = expression.Value.Value;
			switch (value)
			{
			case ConceptualPrimitiveType.Text:
				return DaxLiteral.FromString(Convert.ToString(value2, CultureInfo.InvariantCulture));
			case ConceptualPrimitiveType.Decimal:
				return DaxLiteral.FromDecimal(Convert.ToDecimal(value2, CultureInfo.InvariantCulture), this._cultureInfo);
			case ConceptualPrimitiveType.Double:
				return DaxLiteral.FromDouble(Convert.ToDouble(value2, CultureInfo.InvariantCulture));
			case ConceptualPrimitiveType.Integer:
				return DaxLiteral.FromInt64(Convert.ToInt64(value2, CultureInfo.InvariantCulture));
			case ConceptualPrimitiveType.Boolean:
				return DaxLiteral.FromBoolean(Convert.ToBoolean(value2, CultureInfo.InvariantCulture));
			case ConceptualPrimitiveType.DateTime:
				return DaxLiteral.FromDateTime(Convert.ToDateTime(value2, CultureInfo.InvariantCulture));
			case ConceptualPrimitiveType.Time:
				return DaxTransform.ConvertToDaxTimeSpanLiteral(value2);
			}
			throw new DaxTranslationException(DevErrors.DaxTranslation.UnexpectedLiteralResultType(value.ToString()));
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x0003419C File Offset: 0x0003239C
		private static DaxExpression ConvertToDaxTimeSpanLiteral(object actualValue)
		{
			string text = Convert.ToString(actualValue, CultureInfo.InvariantCulture);
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			if (Thread.CurrentThread.CurrentCulture != CultureInfo.InvariantCulture)
			{
				Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			}
			TimeSpan timeSpan = TimeSpan.Parse(text);
			if (currentCulture != CultureInfo.InvariantCulture)
			{
				Thread.CurrentThread.CurrentCulture = currentCulture;
			}
			return DaxLiteral.FromTimeSpan(timeSpan);
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x000341FC File Offset: 0x000323FC
		protected internal override DaxExpression Visit(QueryMeasureExpression expression)
		{
			this.CheckForCancellation();
			return DaxExpression.Scalar(DaxRef.Measure(expression.Measure, expression.Target, expression.TargetMeasure, expression.TargetEntity).ToString());
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0003423F File Offset: 0x0003243F
		protected internal override DaxExpression Visit(QueryNullExpression expression)
		{
			return DaxFunctions.Blank();
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00034246 File Offset: 0x00032446
		protected internal override DaxExpression Visit(QueryNewInstanceExpression expression)
		{
			throw new DaxTranslationException("Unexpected QueryNewInstanceExpression encountered.");
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00034254 File Offset: 0x00032454
		protected internal override DaxExpression Visit(QueryNewTableExpression expression)
		{
			this.CheckForCancellation();
			global::System.ValueTuple<DaxResultColumn, DaxExpression>[] item = this.BuildProjectedColumns(expression.Columns, DaxExpression.Null, null).Item1;
			return this.BuildNewRowProjection(item);
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00034288 File Offset: 0x00032488
		protected internal override DaxExpression Visit(QueryNonVisualExpression expression)
		{
			this.CheckForCancellation();
			DaxValidation.CheckCondition(this._daxCapabilities.IsSupported(DaxFunctionKind.NonVisual), DevErrors.DaxTranslation.UnexpectedFunction(DaxFunctionKind.NonVisual.ToString()));
			return DaxFunctions.NonVisual(expression.Argument.Accept<DaxExpression>(this));
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x000342D4 File Offset: 0x000324D4
		protected internal override DaxExpression Visit(QueryEnsureUniqueUnqualifiedNamesExpression expression)
		{
			this.CheckForCancellation();
			DaxExpression daxExpression = expression.Table.Accept<DaxExpression>(this);
			if (!expression.ForceRename && DaxTransform.HasUniqueUnqualifiedNames(daxExpression.ResultColumns))
			{
				return daxExpression;
			}
			DaxValidation.CheckCondition(this._daxCapabilities.IsSupported(DaxFunctionKind.SelectColumns), "EnsureUniqueUnqualifiedNames cannot be used when SelectColumns is not supported by the target model.");
			global::System.ValueTuple<DaxResultColumn, DaxExpression>[] array = DaxTransform.BuildProjectedColumnsWithQueryFieldName(daxExpression);
			return this.BuildSelectColumnsProjection(daxExpression, array);
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00034330 File Offset: 0x00032530
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "ResultColumn", "Expression" })]
		private static global::System.ValueTuple<DaxResultColumn, DaxExpression>[] BuildProjectedColumnsWithQueryFieldName(DaxExpression table)
		{
			IReadOnlyList<DaxResultColumn> resultColumns = table.ResultColumns;
			global::System.ValueTuple<DaxResultColumn, DaxExpression>[] array = new global::System.ValueTuple<DaxResultColumn, DaxExpression>[resultColumns.Count];
			for (int i = 0; i < array.Length; i++)
			{
				DaxResultColumn daxResultColumn = resultColumns[i];
				DaxResultColumn daxResultColumn2 = new DaxResultColumn(daxResultColumn.QueryFieldName, DaxRef.Column(daxResultColumn.QueryFieldName));
				DaxExpression daxExpression = DaxExpression.Scalar(daxResultColumn.DaxColumnRef.ToString());
				array[i] = new global::System.ValueTuple<DaxResultColumn, DaxExpression>(daxResultColumn2, daxExpression);
			}
			return array;
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x000343B0 File Offset: 0x000325B0
		private static bool HasUniqueUnqualifiedNames(IReadOnlyList<DaxResultColumn> resultColumns)
		{
			HashSet<string> hashSet = new HashSet<string>(QueryNamingContext.NameComparer);
			foreach (DaxResultColumn daxResultColumn in resultColumns)
			{
				if (!hashSet.Add(daxResultColumn.DaxColumnRef.ColumnName))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0003441C File Offset: 0x0003261C
		protected internal override DaxExpression Visit(QueryProjectExpression expression)
		{
			this.CheckForCancellation();
			QueryNewInstanceExpression queryNewInstanceExpression = expression.Projection as QueryNewInstanceExpression;
			DaxValidation.CheckCondition(queryNewInstanceExpression != null, "Unexpected QueryProjectExpression encountered.");
			DaxExpression daxExpression;
			if (this.ProcessCrossJoinProjection(expression, queryNewInstanceExpression, out daxExpression) || this.ProcessGeneralProjection(expression, queryNewInstanceExpression, out daxExpression))
			{
				return daxExpression;
			}
			throw new DaxTranslationException("Unexpected QueryProjectExpression encountered.");
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00034470 File Offset: 0x00032670
		protected internal override DaxExpression Visit(QueryNaturalJoinExpression expression)
		{
			this.CheckForCancellation();
			DaxExpression daxExpression = expression.Left.Expression.Accept<DaxExpression>(this);
			DaxExpression daxExpression2 = expression.Right.Expression.Accept<DaxExpression>(this);
			NaturalJoinKind joinKind = expression.JoinKind;
			if (joinKind == NaturalJoinKind.Inner)
			{
				return DaxFunctions.NaturalInnerJoin(daxExpression, daxExpression2);
			}
			if (joinKind != NaturalJoinKind.LeftOuter)
			{
				throw new DaxTranslationException(DevErrors.DaxTranslation.UnexpectedNaturalJoinKind(expression.JoinKind.ToString()));
			}
			return DaxFunctions.NaturalLeftOuterJoin(daxExpression, daxExpression2);
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x000344E8 File Offset: 0x000326E8
		protected internal override DaxExpression Visit(QueryImplicitJoinWithProjectionExpression expression)
		{
			this.CheckForCancellation();
			DaxExpression daxExpression = expression.PrimaryTable.Expression.Accept<DaxExpression>(this);
			List<DaxResultColumn> list = new List<DaxResultColumn>();
			foreach (ImplicitJoinSecondaryTable implicitJoinSecondaryTable in expression.SecondaryTables)
			{
				DaxExpression daxExpression2 = implicitJoinSecondaryTable.Table.Expression.Accept<DaxExpression>(this);
				foreach (KeyValuePair<string, QueryFieldExpression> keyValuePair in implicitJoinSecondaryTable.KeyColumns)
				{
					string key = keyValuePair.Key;
					QueryFieldExpression value = keyValuePair.Value;
					DaxColumnRef resultColumnReference = daxExpression2.GetResultColumnReference(value.Column.EdmName);
					DaxResultColumn daxResultColumn = new DaxResultColumn(key, resultColumnReference);
					list.Add(daxResultColumn);
				}
			}
			return DaxExpression.Table(daxExpression.Text, list, false);
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x000345E0 File Offset: 0x000327E0
		protected internal override DaxExpression Visit(QueryUnionAllExpression expression)
		{
			this.CheckForCancellation();
			return DaxFunctions.Union(expression.Tables.Select((QueryExpression table) => table.Accept<DaxExpression>(this)).ToArray<DaxExpression>());
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0003460C File Offset: 0x0003280C
		protected internal override DaxExpression Visit(QuerySubstituteWithIndexExpression expression)
		{
			this.CheckForCancellation();
			DaxExpression daxExpression = expression.Table.Expression.Accept<DaxExpression>(this);
			DaxResultColumn daxResultColumn = new DaxResultColumn(expression.IndexColumnName, DaxRef.Column(DaxUniqueNameGenerator.MakeUniqueColumnName(expression.IndexColumnName, daxExpression.ResultColumns)));
			DaxExpression daxExpression2 = DaxLiteral.FromString(expression.IndexColumnName);
			global::System.ValueTuple<DaxResultColumn, DaxExpression> valueTuple = new global::System.ValueTuple<DaxResultColumn, DaxExpression>(daxResultColumn, daxExpression2);
			DaxExpression daxExpression3 = expression.IndexTable.Expression.Accept<DaxExpression>(this);
			IEnumerable<DaxSortItem> daxSortArguments = DaxTransform.GetDaxSortArguments(expression.IndexTable, daxExpression3, expression.IndexTableSortOrder);
			return DaxFunctions.SubstituteWithIndex(daxExpression, valueTuple, daxExpression3, daxSortArguments);
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x0003469C File Offset: 0x0003289C
		protected internal override DaxExpression Visit(QueryMeasureDeclarationExpression expression)
		{
			this.CheckForCancellation();
			return this.BuildMeasureDeclaration(expression).Expression;
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x000346B0 File Offset: 0x000328B0
		protected internal override DaxExpression Visit(QueryFieldDeclarationExpression expression)
		{
			this.CheckForCancellation();
			return this.BuildFieldDeclaration(expression).Expression;
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x000346C4 File Offset: 0x000328C4
		private DaxExpressionWithSourceLocation BuildMeasureDeclaration(QueryMeasureDeclarationExpression expression)
		{
			QueryMeasureExpression measureRef = expression.MeasureRef;
			DaxMeasureRef daxMeasureRef = DaxRef.Measure(measureRef.Measure, measureRef.Target, measureRef.TargetMeasure, measureRef.TargetEntity);
			QueryExpression expression2 = expression.Expression;
			DaxTableRef daxTableRef = DaxRef.Table(measureRef.Target, measureRef.TargetEntity);
			IConceptualMeasure targetMeasure = measureRef.TargetMeasure;
			DaxExpression daxExpression;
			QueryItemSourceLocation queryItemSourceLocation;
			this.BuildModelExtensionDaxDeclaration<DaxMeasureRef>(expression2, daxTableRef, ((targetMeasure != null) ? targetMeasure.Name : null) ?? measureRef.Measure.ReferenceName, daxMeasureRef, DaxInvalidExternalContentType.MeasureDeclaration, ItemSourceType.QueryExtensionMeasure, out daxExpression, out queryItemSourceLocation);
			return new DaxExpressionWithSourceLocation(DaxStatements.DefineMeasure(daxMeasureRef, daxExpression), queryItemSourceLocation);
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00034748 File Offset: 0x00032948
		private DaxExpressionWithSourceLocation BuildFieldDeclaration(QueryFieldDeclarationExpression expression)
		{
			QueryFieldExpression fieldRef = expression.FieldRef;
			QdmEntityPlaceholderExpression qdmEntityPlaceholderExpression = fieldRef.Instance as QdmEntityPlaceholderExpression;
			DaxColumnRef daxColumnRef = DaxRef.Column(fieldRef.Column, qdmEntityPlaceholderExpression.Target, qdmEntityPlaceholderExpression.TargetEntity);
			DaxExpression daxExpression;
			QueryItemSourceLocation queryItemSourceLocation;
			this.BuildModelExtensionDaxDeclaration<DaxColumnRef>(expression.Expression, daxColumnRef.TableRef, fieldRef.Column.Name, daxColumnRef, DaxInvalidExternalContentType.FieldDeclaration, ItemSourceType.QueryExtensionColumn, out daxExpression, out queryItemSourceLocation);
			return new DaxExpressionWithSourceLocation(DaxStatements.DefineColumn(daxColumnRef, daxExpression), queryItemSourceLocation);
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x000347B4 File Offset: 0x000329B4
		private void BuildModelExtensionDaxDeclaration<T>(QueryExpression expression, DaxTableRef tableRef, string columnName, T modelExtensionRef, DaxInvalidExternalContentType contentType, ItemSourceType sourceType, out DaxExpression daxContent, out QueryItemSourceLocation itemSourceLocation)
		{
			QueryDaxTextExpression queryDaxTextExpression = expression as QueryDaxTextExpression;
			if (queryDaxTextExpression != null)
			{
				ScrubbedEntityPropertyReference scrubbedEntityPropertyReference = new ScrubbedEntityPropertyReference(new ScrubbedString(tableRef.ToString()), new ScrubbedString(columnName), null);
				int num;
				daxContent = this.BuildDaxExternalContent(queryDaxTextExpression.Text, scrubbedEntityPropertyReference, contentType, out num);
				itemSourceLocation = DaxTransform.BuildItemSourceLocation<T>(modelExtensionRef, num, scrubbedEntityPropertyReference, sourceType);
				return;
			}
			daxContent = expression.Accept<DaxExpression>(this);
			itemSourceLocation = null;
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0003481C File Offset: 0x00032A1C
		private static QueryItemSourceLocation BuildItemSourceLocation<T>(T extensionRef, int externalContentLineCount, ScrubbedEntityPropertyReference itemReference, ItemSourceType sourceType)
		{
			int num = DaxTransform.CountNewLines(extensionRef.ToString());
			int num2 = 1;
			int num3 = num2 + num + 1 + 1;
			int num4 = num3 + externalContentLineCount;
			int num5 = num4 + 1;
			return new QueryItemSourceLocation(itemReference, num2, num3, num4, num5, sourceType);
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0003485B File Offset: 0x00032A5B
		private static int CountNewLines(string text)
		{
			if (text == null)
			{
				return 0;
			}
			return DaxExternalContentChecker.Check(text).LineCount - 1;
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x00034870 File Offset: 0x00032A70
		private IReadOnlyList<DaxExpressionWithSourceLocation> BuildTableDeclaration(QueryTableDeclarationExpression expression)
		{
			this.CheckForCancellation();
			DaxExpression daxExpression = expression.Expression.Accept<DaxExpression>(this);
			daxExpression = daxExpression.EnsureUnqualifiedColumns();
			return this.ForkForIsolatedEvaluation().BuildTableDeclaration(expression, daxExpression);
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x000348A4 File Offset: 0x00032AA4
		private IReadOnlyList<DaxExpressionWithSourceLocation> BuildTableDeclaration(QueryTableDeclarationExpression expression, DaxExpression expressionDax)
		{
			List<DaxExpressionWithSourceLocation> list = new List<DaxExpressionWithSourceLocation>(1 + expression.AdditionalColumns.Count);
			DaxTableDefinitionBuilder daxTableDefinitionBuilder = DaxTableDefinitionBuilder.Table(DaxRef.Table(null, expression.Entity), expressionDax);
			this._scopeVars.Push(new global::System.ValueTuple<string, DaxExpression>(expression.Name, expressionDax));
			if (expression.VisualShape != null)
			{
				this.TranslateVisualShape(expression.VisualShape, daxTableDefinitionBuilder.VisualShape());
			}
			DaxExpression daxExpression = daxTableDefinitionBuilder.ToDax();
			list.Add(new DaxExpressionWithSourceLocation(daxExpression, null));
			foreach (QueryFieldDeclarationExpression queryFieldDeclarationExpression in expression.AdditionalColumns)
			{
				DaxExpressionWithSourceLocation daxExpressionWithSourceLocation = this.BuildFieldDeclaration(queryFieldDeclarationExpression);
				list.Add(daxExpressionWithSourceLocation);
			}
			this._scopeVars.Pop();
			return list;
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00034978 File Offset: 0x00032B78
		private void TranslateVisualShape(QueryVisualShape visualShape, IDaxVisualShapeBuilder builder)
		{
			foreach (QueryVisualAxis queryVisualAxis in visualShape.Axes)
			{
				this.TranslateVisualAxis(queryVisualAxis, builder.Axis(queryVisualAxis.Name));
			}
			if (!string.IsNullOrEmpty(visualShape.IsDensifiedColumnName))
			{
				builder.Densify(DaxLiteral.FromString(visualShape.IsDensifiedColumnName));
			}
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x000349F0 File Offset: 0x00032BF0
		private void TranslateVisualAxis(QueryVisualAxis visualAxis, IDaxVisualAxisBuilder builder)
		{
			foreach (QueryVisualAxisGroup queryVisualAxisGroup in visualAxis.Groups)
			{
				this.TranslateVisualAxisGroup(queryVisualAxisGroup, builder);
			}
			IReadOnlyList<global::System.ValueTuple<DaxExpression, SortDirection>> readOnlyList = visualAxis.OrderBy.CreateList(new Func<QuerySortClause, global::System.ValueTuple<DaxExpression, SortDirection>>(this.TranslateSortClause));
			builder.OrderBy(readOnlyList);
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00034A60 File Offset: 0x00032C60
		private void TranslateVisualAxisGroup(QueryVisualAxisGroup axisGroup, IDaxVisualAxisBuilder builder)
		{
			IReadOnlyList<DaxExpression> readOnlyList = axisGroup.Keys.CreateList((QueryExpression e) => e.Accept<DaxExpression>(this));
			DaxExpression daxExpression = ((axisGroup.SubtotalIndicator != null) ? axisGroup.SubtotalIndicator.Accept<DaxExpression>(this) : null);
			builder.Group(readOnlyList, daxExpression);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x00034AA5 File Offset: 0x00032CA5
		protected internal override DaxExpression Visit(QueryTableDeclarationExpression expression)
		{
			throw new DaxTranslationException("Unexpected QueryBatchRootExpression encountered.");
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00034AB1 File Offset: 0x00032CB1
		protected internal override DaxExpression Visit(QueryVariableDeclarationExpression expression)
		{
			this.CheckForCancellation();
			return DaxStatements.DefineVariable(expression.VariableName, expression.Expression.Accept<DaxExpression>(this));
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x00034AD0 File Offset: 0x00032CD0
		private bool ProcessCrossJoinProjection(QueryProjectExpression expression, QueryNewInstanceExpression projection, out DaxExpression result)
		{
			QueryCrossJoinExpression queryCrossJoinExpression = expression.Input.Expression as QueryCrossJoinExpression;
			if (queryCrossJoinExpression == null)
			{
				result = null;
				return false;
			}
			DaxExpression[] array = queryCrossJoinExpression.Inputs.Select((QueryExpressionBinding b) => b.Expression.Accept<DaxExpression>(this)).ToArray<DaxExpression>();
			if (!this.IsValidJoinProjection(array, projection))
			{
				result = null;
				return false;
			}
			result = DaxFunctions.CrossJoin(array);
			return true;
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x00034B2C File Offset: 0x00032D2C
		private bool IsValidJoinProjection(IReadOnlyList<DaxExpression> inputTables, QueryNewInstanceExpression projection)
		{
			var array = inputTables.SelectMany((DaxExpression f, int i) => f.ResultColumns.Select((DaxResultColumn c) => new
			{
				Index = i,
				ResultColumn = c
			})).ToArray();
			if (array.Length != projection.Arguments.Count)
			{
				return false;
			}
			for (int j = 0; j < array.Length; j++)
			{
				string queryFieldName = array[j].ResultColumn.QueryFieldName;
				if (projection.Arguments[j].Key != queryFieldName)
				{
					return false;
				}
				QueryFieldExpression queryFieldExpression = projection.Arguments[j].Value as QueryFieldExpression;
				if (queryFieldExpression == null || queryFieldExpression.Column.EdmName != queryFieldName)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00034BEC File Offset: 0x00032DEC
		private bool ProcessGeneralProjection(QueryProjectExpression expression, QueryNewInstanceExpression projection, out DaxExpression result)
		{
			global::System.ValueTuple<DaxResultColumn, DaxExpression>[] newColumns = null;
			bool inputIsPrefixOfResult = false;
			DaxExpression daxExpression = this.EvaluateInScope<DaxExpression>(expression.Input, delegate(DaxExpression input)
			{
				global::System.ValueTuple<global::System.ValueTuple<DaxResultColumn, DaxExpression>[], bool> valueTuple = this.BuildProjectedColumns(projection, input, expression.Input.Variable);
				newColumns = valueTuple.Item1;
				inputIsPrefixOfResult = valueTuple.Item2;
				return input;
			}, false);
			if (daxExpression.ResultColumns.Count == 0)
			{
				result = this.BuildNewRowProjection(newColumns);
				return true;
			}
			if ((daxExpression.ResultColumns.Count < newColumns.Length) & inputIsPrefixOfResult)
			{
				newColumns = newColumns.Skip(daxExpression.ResultColumns.Count).ToArray<global::System.ValueTuple<DaxResultColumn, DaxExpression>>();
				daxExpression = DaxFunctions.KeepFilters(daxExpression);
				result = this.BuildAddColumnsProjection(projection, daxExpression, newColumns);
				return true;
			}
			return this.ProcessSubsetProjection(expression, projection, daxExpression, newColumns, out result);
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00034CD8 File Offset: 0x00032ED8
		private bool ProcessSubsetProjection(QueryProjectExpression expression, QueryNewInstanceExpression projection, DaxExpression inputTable, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "ResultColumn", "Expression" })] global::System.ValueTuple<DaxResultColumn, DaxExpression>[] newColumns, out DaxExpression result)
		{
			ProjectSubsetStrategy projectSubsetStrategy = expression.ProjectSubsetStrategy;
			if (projectSubsetStrategy != ProjectSubsetStrategy.Default)
			{
				if (projectSubsetStrategy == ProjectSubsetStrategy.Summarize)
				{
					IDaxGroupItem[] array = DaxTransform.BuildGroupByItems(expression, projection, inputTable);
					result = this.BuildSummarizeProjection(inputTable, array, null);
					return true;
				}
				throw new DaxTranslationException(DevErrors.DaxTranslation.UnexpectedProjectSubsetStrategy(Enum.GetName(typeof(ProjectSubsetStrategy), expression.ProjectSubsetStrategy)));
			}
			else
			{
				if (this._daxCapabilities.IsSupported(DaxFunctionKind.SelectColumns))
				{
					inputTable = DaxFunctions.KeepFilters(inputTable);
					result = this.BuildSelectColumnsProjection(inputTable, newColumns);
					return true;
				}
				result = null;
				return false;
			}
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00034D58 File Offset: 0x00032F58
		private static int FindProjectionInputColumnIndex(QueryVariableReferenceExpression variable, KeyValuePair<string, QueryExpression> projectionArgument, IReadOnlyList<DaxResultColumn> inputTableColumns)
		{
			for (int i = 0; i < inputTableColumns.Count; i++)
			{
				string queryFieldName = inputTableColumns[i].QueryFieldName;
				if (projectionArgument.Key == queryFieldName)
				{
					QueryFieldExpression queryFieldExpression = projectionArgument.Value as QueryFieldExpression;
					if (queryFieldExpression != null && queryFieldExpression.Instance.Equals(variable) && queryFieldExpression.Column.EdmName == queryFieldName)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00034DC9 File Offset: 0x00032FC9
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "NewColumns", "InputIsPrefixOfResult", "ResultColumn", "Expression" })]
		private global::System.ValueTuple<global::System.ValueTuple<DaxResultColumn, DaxExpression>[], bool> BuildProjectedColumns(QueryNewInstanceExpression projection, DaxExpression inputTable, QueryVariableReferenceExpression variable)
		{
			return this.BuildProjectedColumns(projection.Arguments, inputTable, variable);
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x00034DDC File Offset: 0x00032FDC
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "NewColumns", "InputIsPrefixOfResult", "ResultColumn", "Expression" })]
		private global::System.ValueTuple<global::System.ValueTuple<DaxResultColumn, DaxExpression>[], bool> BuildProjectedColumns(ReadOnlyCollection<KeyValuePair<string, QueryExpression>> projectionArgs, DaxExpression inputTable, QueryVariableReferenceExpression variable)
		{
			DaxUniqueNameGenerator daxUniqueNameGenerator = new DaxUniqueNameGenerator();
			global::System.ValueTuple<DaxResultColumn, DaxExpression>[] array = new global::System.ValueTuple<DaxResultColumn, DaxExpression>[projectionArgs.Count];
			IReadOnlyList<DaxResultColumn> resultColumns = inputTable.ResultColumns;
			int num = 0;
			bool flag = true;
			if (variable != null)
			{
				int num2 = 0;
				while (num2 < projectionArgs.Count && num < resultColumns.Count)
				{
					KeyValuePair<string, QueryExpression> keyValuePair = projectionArgs[num2];
					int num3 = DaxTransform.FindProjectionInputColumnIndex(variable, keyValuePair, resultColumns);
					if (num3 >= 0)
					{
						flag = flag && num2 == num3;
						DaxResultColumn daxResultColumn = resultColumns[num3];
						array[num2] = new global::System.ValueTuple<DaxResultColumn, DaxExpression>(daxResultColumn, keyValuePair.Value.Accept<DaxExpression>(this));
						daxUniqueNameGenerator.RegisterString(daxResultColumn.DaxColumnRef.ColumnName);
						if (!string.IsNullOrEmpty(daxResultColumn.DaxColumnRef.TableName))
						{
							daxUniqueNameGenerator.RegisterString(daxResultColumn.ToIntermediateResultColumnName());
						}
						num++;
					}
					num2++;
				}
			}
			flag = flag && num == resultColumns.Count;
			if (num < projectionArgs.Count)
			{
				for (int i = 0; i < projectionArgs.Count; i++)
				{
					KeyValuePair<string, QueryExpression> keyValuePair2 = projectionArgs[i];
					if (array[i].Item2 == null)
					{
						string key = keyValuePair2.Key;
						DaxResultColumn daxResultColumn2 = new DaxResultColumn(key, DaxRef.Column(daxUniqueNameGenerator.MakeUniqueString(key)));
						array[i] = new global::System.ValueTuple<DaxResultColumn, DaxExpression>(daxResultColumn2, keyValuePair2.Value.Accept<DaxExpression>(this));
					}
				}
			}
			return new global::System.ValueTuple<global::System.ValueTuple<DaxResultColumn, DaxExpression>[], bool>(array, flag);
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00034F4C File Offset: 0x0003314C
		private DaxExpression BuildAddColumnsProjection(QueryNewInstanceExpression projection, DaxExpression inputDax, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "ResultColumn", "Expression" })] global::System.ValueTuple<DaxResultColumn, DaxExpression>[] newColumns)
		{
			ArgumentValidation.CheckNotNull<QueryNewInstanceExpression>(projection, "projection");
			ArgumentValidation.CheckNotNull<DaxExpression>(inputDax, "inputDax");
			ArgumentValidation.CheckCondition(inputDax.ResultColumns.Count > 0, "inputDax");
			ArgumentValidation.CheckCondition(projection.Arguments.Count > inputDax.ResultColumns.Count, "projection");
			return DaxFunctions.AddColumns(inputDax, newColumns);
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00034FB2 File Offset: 0x000331B2
		private DaxExpression BuildSelectColumnsProjection(DaxExpression inputDax, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "ResultColumn", "Expression" })] global::System.ValueTuple<DaxResultColumn, DaxExpression>[] columns)
		{
			ArgumentValidation.CheckNotNull<DaxExpression>(inputDax, "inputDax");
			ArgumentValidation.CheckCondition(inputDax.ResultColumns.Count > 0, "inputDax");
			return DaxFunctions.SelectColumns(inputDax, columns);
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x00034FDF File Offset: 0x000331DF
		private DaxExpression BuildSummarizeProjection(DaxExpression inputDax, IDaxGroupItem[] columns, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "ResultColumn", "Expression" })] global::System.ValueTuple<DaxResultColumn, DaxExpression>[] newColumns = null)
		{
			ArgumentValidation.CheckCondition(inputDax.ResultColumns.Count > 0, "inputDax");
			return DaxFunctions.Summarize(inputDax, columns, newColumns);
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x00035001 File Offset: 0x00033201
		private DaxExpression BuildNewRowProjection([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "ResultColumn", "Expression" })] global::System.ValueTuple<DaxResultColumn, DaxExpression>[] newColumns)
		{
			ArgumentValidation.CheckNotNull<global::System.ValueTuple<DaxResultColumn, DaxExpression>[]>(newColumns, "newColumns");
			ArgumentValidation.CheckCondition(newColumns.Length != 0, "Must project at least one column");
			return DaxFunctions.Row(newColumns);
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00035024 File Offset: 0x00033224
		protected internal override DaxExpression Visit(QueryScanExpression expression)
		{
			this.CheckForCancellation();
			DaxExpression daxExpression = DaxTransform.BuildTableScanCore(expression.Target, expression.TargetEntity);
			if (expression.ExcludeBlankRow)
			{
				return daxExpression;
			}
			return DaxFunctions.Values(daxExpression);
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x0003505C File Offset: 0x0003325C
		private static DaxExpression BuildTableScanCore(EntitySet entitySet, IConceptualEntity entity)
		{
			IReadOnlyList<DaxResultColumn> readOnlyList;
			if (entity != null)
			{
				readOnlyList = (from p in entity.GetExtensionAwareProperties().Where(delegate(IConceptualProperty p)
					{
						IConceptualColumn conceptualColumn = p as IConceptualColumn;
						return conceptualColumn != null && !conceptualColumn.IsRowNumber;
					})
					select p.AsColumn() into c
					select DaxResultColumn.FromColumn(c.ConceptualTypeColumn, null, entity)).ToList<DaxResultColumn>();
			}
			else
			{
				readOnlyList = (from f in entitySet.ElementType.Fields
					where !f.IsRowNumber()
					select DaxResultColumn.FromColumn(f.Column, entitySet, null)).ToList<DaxResultColumn>();
			}
			return DaxExpression.Table(DaxRef.Table(entitySet, entity).ToString(), readOnlyList, true);
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x00035164 File Offset: 0x00033364
		protected internal override DaxExpression Visit(QuerySingleValueExpression expression)
		{
			this.CheckForCancellation();
			return DaxExpression.Scalar(expression.Argument.Accept<DaxExpression>(this).Text);
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x00035182 File Offset: 0x00033382
		protected internal override DaxExpression Visit(QuerySortExpression expression)
		{
			throw new DaxTranslationException("Unexpected sort expression encountered. Sort expressions must consist of simple column references on the input table.");
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x00035190 File Offset: 0x00033390
		protected internal override DaxExpression Visit(QueryLimitExpression expression)
		{
			this.CheckForCancellation();
			DaxExpression daxExpression = expression.Input.Expression.Accept<DaxExpression>(this);
			DaxValidation.CheckCondition(expression.SortOrder.Count != 0 || expression.LimitKind == QueryLimitOperator.TopNSkip, "Encountered QueryLimitExpression without sort order.");
			DaxExpression daxExpression2 = expression.Count.Accept<DaxExpression>(this);
			ReadOnlyCollection<QuerySortClause> sortOrder = expression.SortOrder;
			switch (expression.LimitKind)
			{
			case QueryLimitOperator.TopN:
				return DaxFunctions.TopN(daxExpression2, daxExpression, sortOrder);
			case QueryLimitOperator.Sample:
				return DaxFunctions.Sample(daxExpression2, daxExpression, sortOrder);
			case QueryLimitOperator.TopNSkip:
			{
				DaxExpression daxExpression3 = expression.SkipCount.Accept<DaxExpression>(this);
				return DaxFunctions.TopNSkip(daxExpression2, daxExpression3, daxExpression, sortOrder);
			}
			default:
				throw new DaxTranslationException(DevErrors.DaxTranslation.UnexpectedLimitKind(expression.LimitKind.ToString()));
			}
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x00035251 File Offset: 0x00033451
		protected internal override DaxExpression Visit(QueryTypeCastExpression expression)
		{
			this.CheckForCancellation();
			return expression.Input.Accept<DaxExpression>(this);
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x00035268 File Offset: 0x00033468
		protected internal override DaxExpression Visit(QueryVariableReferenceExpression expression)
		{
			this.CheckForCancellation();
			DaxExpression daxExpression;
			if (this._globalScopeVars.TryGetValue(expression.VariableName, out daxExpression))
			{
				return DaxTransform.DereferenceVariable(expression, daxExpression);
			}
			global::System.ValueTuple<string, DaxExpression> valueTuple = this.ResolveVariableReference(expression);
			return DaxTransform.DereferenceVariable(expression, valueTuple.Item2);
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x000352AC File Offset: 0x000334AC
		private static DaxExpression DereferenceVariable(QueryVariableReferenceExpression expression, DaxExpression content)
		{
			return DaxExpression.ScalarOrTable(expression.VariableName, content.ResultColumns);
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x000352BF File Offset: 0x000334BF
		private IEnumerable<DaxExpression> VisitAll(IEnumerable<QueryExpression> queryExpressions)
		{
			return queryExpressions.Select((QueryExpression queryExpression) => queryExpression.Accept<DaxExpression>(this)).Evaluate<DaxExpression>();
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x000352D8 File Offset: 0x000334D8
		private DaxExpression BuildEvaluate(QueryExpression root)
		{
			return DaxStatements.Evaluate(root.Accept<DaxExpression>(this));
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x000352E8 File Offset: 0x000334E8
		private DaxExpression BuildOrderBy(QuerySortExpression sortExpr)
		{
			QueryExpressionBinding input = sortExpr.Input;
			DaxExpression daxExpression = this.BuildEvaluate(input.Expression);
			if (sortExpr.SortOrder.Count == 0)
			{
				return daxExpression;
			}
			List<DaxSortItem> list = DaxTransform.GetDaxSortArguments(input, daxExpression, sortExpr.SortOrder).ToList<DaxSortItem>();
			return DaxStatements.OrderBy(daxExpression, list);
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x00035332 File Offset: 0x00033532
		private static IEnumerable<DaxSortItem> GetDaxSortArguments(QueryExpressionBinding input, DaxExpression inputDax, IEnumerable<QuerySortClause> sortOrder)
		{
			foreach (QuerySortClause querySortClause in sortOrder)
			{
				QueryFieldExpression queryFieldExpression = querySortClause.Expression as QueryFieldExpression;
				DaxValidation.CheckCondition(queryFieldExpression != null && queryFieldExpression.Instance.Equals(input.Variable), "Unexpected sort expression encountered. Sort expressions must consist of simple column references on the input table.");
				yield return new DaxSortItem(inputDax.GetResultColumnReference(queryFieldExpression.Column), querySortClause.Direction);
			}
			IEnumerator<QuerySortClause> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00035350 File Offset: 0x00033550
		private static DaxSortItem GetDaxSortArgument(DaxExpression inputDax, QuerySortClause sortClause)
		{
			QueryFieldExpression queryFieldExpression = sortClause.Expression as QueryFieldExpression;
			DaxValidation.CheckCondition(queryFieldExpression != null, "Unexpected sort expression encountered. Sort expressions must consist of simple column references on the input table.");
			return new DaxSortItem(inputDax.GetResultColumnReference(queryFieldExpression.Column), sortClause.Direction);
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00035390 File Offset: 0x00033590
		private DaxExpression BuildStartAt(QueryStartAtExpression startAtExpr)
		{
			QuerySortExpression orderBy = startAtExpr.OrderBy;
			DaxExpression daxExpression = this.BuildOrderBy(orderBy);
			IEnumerable<string> enumerable = startAtExpr.Values.Select((QueryExpression v) => v.Accept<DaxExpression>(this).Text);
			return DaxStatements.StartAt(daxExpression, enumerable);
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x000353CC File Offset: 0x000335CC
		protected internal override DaxExpression Visit(QueryLookupValueExpression expression)
		{
			this.CheckForCancellation();
			IReadOnlyList<QueryLookupTuple> lookupTuples = expression.LookupTuples;
			DaxExpression[] array = new DaxExpression[lookupTuples.Count * 2 + 1];
			array[0] = expression.ResultColumn.Accept<DaxExpression>(this);
			for (int i = 0; i < lookupTuples.Count; i++)
			{
				QueryLookupTuple queryLookupTuple = lookupTuples[i];
				array[i * 2 + 1] = queryLookupTuple.SearchColumn.Accept<DaxExpression>(this);
				array[i * 2 + 2] = queryLookupTuple.SearchValue.Accept<DaxExpression>(this);
			}
			return DaxFunctions.LookupValue(array);
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0003544A File Offset: 0x0003364A
		protected internal override DaxExpression Visit(QueryEarlierExpression expression)
		{
			this.CheckForCancellation();
			return DaxFunctions.Earlier(new DaxExpression[]
			{
				expression.Column.Accept<DaxExpression>(this),
				DaxLiteral.FromInt32(expression.Number)
			});
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x0003547A File Offset: 0x0003367A
		protected internal override DaxExpression Visit(QueryConvertToStringExpression expression)
		{
			this.CheckForCancellation();
			return DaxFunctions.Concatenate(DaxLiteral.FromString(string.Empty), expression.Input.Accept<DaxExpression>(this));
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x000354A0 File Offset: 0x000336A0
		protected internal override DaxExpression Visit(QueryConcatenateExpression expression)
		{
			this.CheckForCancellation();
			if (expression.Inputs.Count > 2)
			{
				return DaxOperators.Concatenate(expression.Inputs.Select((QueryExpression input) => input.Accept<DaxExpression>(this)).AsReadOnlyList<DaxExpression>());
			}
			return DaxFunctions.Concatenate(expression.Inputs[0].Accept<DaxExpression>(this), expression.Inputs[1].Accept<DaxExpression>(this));
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0003550C File Offset: 0x0003370C
		protected internal override DaxExpression Visit(QueryConcatenateXExpression expression)
		{
			this.CheckForCancellation();
			DaxExpression inputTable = null;
			DaxExpression concatenateExpression = null;
			DaxExpression delimiter = null;
			DaxSortItem? daxSortItem = null;
			return DaxFunctions.ConcatenateX(this.EvaluateInScope<DaxExpression>(expression.Table, delegate(DaxExpression tableExpr)
			{
				inputTable = expression.Table.Expression.Accept<DaxExpression>(this);
				concatenateExpression = expression.Expression.Accept<DaxExpression>(this);
				if (expression.Delimiter != null)
				{
					delimiter = expression.Delimiter.Accept<DaxExpression>(this);
				}
				if (expression.OrderBy != null)
				{
					daxSortItem = new DaxSortItem?(DaxTransform.GetDaxSortArgument(inputTable, expression.OrderBy));
				}
				return tableExpr;
			}, false), concatenateExpression, delimiter, daxSortItem);
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x0003558C File Offset: 0x0003378C
		protected internal override DaxExpression Visit(QueryExpressionWithLocalVariables expression)
		{
			this.CheckForCancellation();
			DaxMultiPartBuilder builder = new DaxMultiPartBuilder();
			DaxExpression daxExpression = this.EvaluateInScope<DaxExpression>(expression.Declarations, delegate(IReadOnlyList<DaxExpression> daxDeclarations)
			{
				foreach (DaxExpression daxExpression2 in daxDeclarations)
				{
					builder.AddDefinition(daxExpression2, null);
				}
				return expression.Result.Accept<DaxExpression>(this);
			});
			if (!expression.Declarations.IsNullOrEmpty<QueryVariableDeclarationExpression>())
			{
				daxExpression = DaxStatements.Return(daxExpression);
			}
			builder.AddStatement(daxExpression);
			IReadOnlyList<QueryItemSourceLocation> readOnlyList;
			return DaxExpression.ScalarOrTable(builder.ToCommandText(out readOnlyList), daxExpression.ResultColumns);
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x0003561C File Offset: 0x0003381C
		protected internal override DaxExpression Visit(QueryTreatAsExpression expression)
		{
			this.CheckForCancellation();
			DaxExpression daxExpression = expression.InputTable.Accept<DaxExpression>(this);
			List<DaxExpression> list = expression.Columns.Select((KeyValuePair<string, QueryExpression> c) => c.Value.Accept<DaxExpression>(this)).ToList<DaxExpression>();
			IList<DaxResultColumn> list2 = this.BuildScalarEntityFieldDaxResultColumns(expression.Columns);
			return DaxFunctions.TreatAs(daxExpression, list, list2.ToReadOnlyList<DaxResultColumn>());
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00035674 File Offset: 0x00033874
		private void CheckForCancellation()
		{
			this._cancellationToken.ThrowIfCancellationRequested();
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x0003568F File Offset: 0x0003388F
		protected internal override DaxExpression Visit(QueryParameterDeclarationExpression expression)
		{
			throw new DaxTranslationException("Unexpected QueryParameterDeclarationExpression encountered.");
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x0003569C File Offset: 0x0003389C
		protected internal override DaxExpression Visit(QueryParameterReferenceExpression expression)
		{
			this.CheckForCancellation();
			DaxExpression daxExpression;
			if (this._queryParameters != null && this._queryParameters.TryGetValue(expression.Name, out daxExpression))
			{
				return daxExpression;
			}
			throw new DaxTranslationException(DevErrors.DaxTranslation.QueryParameterNotInScope(expression.Name));
		}

		// Token: 0x04000AE9 RID: 2793
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "VariableName", "Expression" })]
		private readonly Stack<global::System.ValueTuple<string, DaxExpression>> _scopeVars;

		// Token: 0x04000AEA RID: 2794
		private readonly Dictionary<string, DaxExpression> _globalScopeVars;

		// Token: 0x04000AEB RID: 2795
		private Dictionary<string, DaxExpression> _queryParameters;

		// Token: 0x04000AEC RID: 2796
		private readonly DaxCapabilities _daxCapabilities;

		// Token: 0x04000AED RID: 2797
		private readonly CultureInfo _cultureInfo;

		// Token: 0x04000AEE RID: 2798
		private readonly CancellationToken _cancellationToken;

		// Token: 0x04000AEF RID: 2799
		private int _negationFunctionCount;

		// Token: 0x02000390 RID: 912
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040012DC RID: 4828
			public static Func<QueryExpression, QueryExpression, QueryComparisonExpression> <0>__LessThan;

			// Token: 0x040012DD RID: 4829
			public static Func<QueryExpression, QueryExpression, QueryComparisonExpression> <1>__GreaterThan;
		}
	}
}
