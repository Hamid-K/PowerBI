using System;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200017B RID: 379
	internal abstract class QueryExpressionVisitor<TResultType>
	{
		// Token: 0x060014A6 RID: 5286
		protected internal abstract TResultType Visit(QueryAddMissingItemsExpression expression);

		// Token: 0x060014A7 RID: 5287
		protected internal abstract TResultType Visit(QueryAllExpression expression);

		// Token: 0x060014A8 RID: 5288
		protected internal abstract TResultType Visit(QueryEarlierExpression expression);

		// Token: 0x060014A9 RID: 5289
		protected internal abstract TResultType Visit(QueryGenerateExpression expression);

		// Token: 0x060014AA RID: 5290
		protected internal abstract TResultType Visit(QueryBatchRootExpression expression);

		// Token: 0x060014AB RID: 5291
		protected internal abstract TResultType Visit(QueryCalculateExpression expression);

		// Token: 0x060014AC RID: 5292
		protected internal abstract TResultType Visit(QueryComparisonExpression expression);

		// Token: 0x060014AD RID: 5293
		protected internal abstract TResultType Visit(QueryConcatenateExpression expression);

		// Token: 0x060014AE RID: 5294
		protected internal abstract TResultType Visit(QueryConcatenateXExpression expression);

		// Token: 0x060014AF RID: 5295
		protected internal abstract TResultType Visit(QueryConvertToStringExpression expression);

		// Token: 0x060014B0 RID: 5296
		protected internal abstract TResultType Visit(QueryCountRowsExpression expression);

		// Token: 0x060014B1 RID: 5297
		protected internal abstract TResultType Visit(QueryCrossJoinExpression expression);

		// Token: 0x060014B2 RID: 5298
		protected internal abstract TResultType Visit(QueryCurrentGroupExpression expression);

		// Token: 0x060014B3 RID: 5299
		protected internal abstract TResultType Visit(QueryDataSourceVariablesDeclarationExpression expression);

		// Token: 0x060014B4 RID: 5300
		protected internal abstract TResultType Visit(QueryMParameterDeclarationExpression expression);

		// Token: 0x060014B5 RID: 5301
		protected internal abstract TResultType Visit(QueryDataTableExpression expression);

		// Token: 0x060014B6 RID: 5302
		protected internal abstract TResultType Visit(QueryDateDiffExpression expression);

		// Token: 0x060014B7 RID: 5303
		protected internal abstract TResultType Visit(QueryDaxTextExpression expression);

		// Token: 0x060014B8 RID: 5304
		protected internal abstract TResultType Visit(QueryDistinctExpression expression);

		// Token: 0x060014B9 RID: 5305
		protected internal abstract TResultType Visit(QueryEnsureUniqueUnqualifiedNamesExpression expression);

		// Token: 0x060014BA RID: 5306
		protected internal abstract TResultType Visit(QueryExpressionWithLocalVariables expression);

		// Token: 0x060014BB RID: 5307
		protected internal abstract TResultType Visit(QueryExtensionExpression expression);

		// Token: 0x060014BC RID: 5308
		protected internal abstract TResultType Visit(QueryExtensionFunctionExpression expression);

		// Token: 0x060014BD RID: 5309
		protected internal abstract TResultType Visit(QueryGroupAndJoinExpression expression);

		// Token: 0x060014BE RID: 5310
		protected internal abstract TResultType Visit(QueryGroupByExpression expression);

		// Token: 0x060014BF RID: 5311
		protected internal abstract TResultType Visit(QueryFieldDeclarationExpression expression);

		// Token: 0x060014C0 RID: 5312
		protected internal abstract TResultType Visit(QueryFieldExpression expression);

		// Token: 0x060014C1 RID: 5313
		protected internal abstract TResultType Visit(QueryFieldReferenceNameExpression expression);

		// Token: 0x060014C2 RID: 5314
		protected internal abstract TResultType Visit(QueryFilterExpression expression);

		// Token: 0x060014C3 RID: 5315
		protected internal abstract TResultType Visit(QueryFormatExpression expression);

		// Token: 0x060014C4 RID: 5316
		protected internal abstract TResultType Visit(QueryFunctionExpression expression);

		// Token: 0x060014C5 RID: 5317
		protected internal abstract TResultType Visit(QueryImplicitJoinWithProjectionExpression expression);

		// Token: 0x060014C6 RID: 5318
		protected internal abstract TResultType Visit(QueryInExpression expression);

		// Token: 0x060014C7 RID: 5319
		protected internal abstract TResultType Visit(QueryInTableExpression expression);

		// Token: 0x060014C8 RID: 5320
		protected internal abstract TResultType Visit(QueryIsAggregateExpression expression);

		// Token: 0x060014C9 RID: 5321
		protected internal abstract TResultType Visit(QueryIsEmptyExpression expression);

		// Token: 0x060014CA RID: 5322
		protected internal abstract TResultType Visit(QueryIsOnOrAfterExpression expression);

		// Token: 0x060014CB RID: 5323
		protected internal abstract TResultType Visit(QueryIsAfterExpression expression);

		// Token: 0x060014CC RID: 5324
		protected internal abstract TResultType Visit(QueryIsNullExpression expression);

		// Token: 0x060014CD RID: 5325
		protected internal abstract TResultType Visit(QueryLimitExpression expression);

		// Token: 0x060014CE RID: 5326
		protected internal abstract TResultType Visit(QueryLiteralExpression expression);

		// Token: 0x060014CF RID: 5327
		protected internal abstract TResultType Visit(QueryLookupValueExpression expression);

		// Token: 0x060014D0 RID: 5328
		protected internal abstract TResultType Visit(QueryMeasureDeclarationExpression expression);

		// Token: 0x060014D1 RID: 5329
		protected internal abstract TResultType Visit(QueryMeasureExpression expression);

		// Token: 0x060014D2 RID: 5330
		protected internal abstract TResultType Visit(QueryNaturalJoinExpression expression);

		// Token: 0x060014D3 RID: 5331
		protected internal abstract TResultType Visit(QueryNewInstanceExpression expression);

		// Token: 0x060014D4 RID: 5332
		protected internal abstract TResultType Visit(QueryNewTableExpression expression);

		// Token: 0x060014D5 RID: 5333
		protected internal abstract TResultType Visit(QueryNonVisualExpression expression);

		// Token: 0x060014D6 RID: 5334
		protected internal abstract TResultType Visit(QueryNullExpression expression);

		// Token: 0x060014D7 RID: 5335
		protected internal abstract TResultType Visit(QueryOperatorExpression expression);

		// Token: 0x060014D8 RID: 5336
		protected internal abstract TResultType Visit(QueryParameterDeclarationExpression expression);

		// Token: 0x060014D9 RID: 5337
		protected internal abstract TResultType Visit(QueryParameterReferenceExpression expression);

		// Token: 0x060014DA RID: 5338
		protected internal abstract TResultType Visit(QueryProjectExpression expression);

		// Token: 0x060014DB RID: 5339
		protected internal abstract TResultType Visit(QueryRelatedColumnExpression expression);

		// Token: 0x060014DC RID: 5340
		protected internal abstract TResultType Visit(QuerySampleAxisWithLocalMinMaxExpression expression);

		// Token: 0x060014DD RID: 5341
		protected internal abstract TResultType Visit(QuerySampleCartesianPointsByCoverExpression expression);

		// Token: 0x060014DE RID: 5342
		protected internal abstract TResultType Visit(QueryScalarEntityReferenceExpression expression);

		// Token: 0x060014DF RID: 5343
		protected internal abstract TResultType Visit(QueryScanExpression expression);

		// Token: 0x060014E0 RID: 5344
		protected internal abstract TResultType Visit(QuerySingleValueExpression expression);

		// Token: 0x060014E1 RID: 5345
		protected internal abstract TResultType Visit(QuerySortExpression expression);

		// Token: 0x060014E2 RID: 5346
		protected internal abstract TResultType Visit(QuerySubstituteWithIndexExpression expression);

		// Token: 0x060014E3 RID: 5347
		protected internal abstract TResultType Visit(QuerySwitchExpression expression);

		// Token: 0x060014E4 RID: 5348
		protected internal abstract TResultType Visit(QueryStartAtExpression expression);

		// Token: 0x060014E5 RID: 5349
		protected internal abstract TResultType Visit(QueryTableDeclarationExpression expression);

		// Token: 0x060014E6 RID: 5350
		protected internal abstract TResultType Visit(QueryTreatAsExpression expression);

		// Token: 0x060014E7 RID: 5351
		protected internal abstract TResultType Visit(QueryTupleExpression expression);

		// Token: 0x060014E8 RID: 5352
		protected internal abstract TResultType Visit(QueryTypeCastExpression expression);

		// Token: 0x060014E9 RID: 5353
		protected internal abstract TResultType Visit(QueryTypeSafeFloorExpression expression);

		// Token: 0x060014EA RID: 5354
		protected internal abstract TResultType Visit(QueryUnionAllExpression expression);

		// Token: 0x060014EB RID: 5355
		protected internal abstract TResultType Visit(QueryVariableDeclarationExpression expression);

		// Token: 0x060014EC RID: 5356
		protected internal abstract TResultType Visit(QueryVariableReferenceExpression expression);

		// Token: 0x060014ED RID: 5357
		protected internal abstract TResultType Visit(QueryTopNPerLevelSampleExpression expression);
	}
}
