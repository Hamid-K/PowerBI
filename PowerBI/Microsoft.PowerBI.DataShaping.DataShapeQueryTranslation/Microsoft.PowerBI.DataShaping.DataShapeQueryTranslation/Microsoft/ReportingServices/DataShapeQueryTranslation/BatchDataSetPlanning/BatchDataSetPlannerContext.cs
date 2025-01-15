using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000165 RID: 357
	internal sealed class BatchDataSetPlannerContext : IShowAllTableBuilderContext, ICommonPlanningContext, ISubqueryPlanOperationGeneratorContext, ILimitPlanningContext, IValueFilterPlanningContext, IDetailTableBuilderContext, IAggregatesPlanningContext
	{
		// Token: 0x06000CF0 RID: 3312 RVA: 0x00035918 File Offset: 0x00033B18
		internal BatchDataSetPlannerContext(WritableExpressionTable expressionTable, IFederatedConceptualSchema schema, DataShapeAnnotations annotations, ScopeTree scopeTree, TranslationErrorContext errorContext, BatchSortByMeasureExpressionMappings sortByMeasureExpressionMappings, CalculationExpressionMap calculationMap, GroupDetailMap groupDetailMap, DataTransformReferenceMap transformReferenceMap, DataShapeQueryTranslationTelemetry telemetryInfo, double? enhancedSamplingAdditionalKeyPointsRatio, bool applyTransformsInQuery, bool generateComposableQueryColumnNames, IFeatureSwitchProvider featureSwitches, IDataShapeDefaultValueContextManager defaultValueContextManager)
		{
			this.OutputExpressionTable = expressionTable;
			this.Schema = schema;
			this.ErrorContext = errorContext;
			this.Annotations = annotations;
			this.ScopeTree = scopeTree;
			this.SortByMeasureExpressionMappings = sortByMeasureExpressionMappings;
			this.CalculationMap = calculationMap;
			this.GroupDetailMap = groupDetailMap;
			this.TransformReferenceMap = transformReferenceMap;
			this.TelemetryInfo = telemetryInfo;
			this.EnhancedSamplingAdditionalKeyPointsRatio = enhancedSamplingAdditionalKeyPointsRatio ?? 0.1;
			this.ApplyTransformsInQuery = applyTransformsInQuery;
			this.GenerateComposableQueryColumnNames = generateComposableQueryColumnNames;
			this.FeatureSwitches = featureSwitches;
			this.DefaultValueContextManager = defaultValueContextManager;
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x000359BC File Offset: 0x00033BBC
		public IFederatedConceptualSchema Schema { get; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x000359C4 File Offset: 0x00033BC4
		public ScopeTree ScopeTree { get; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x000359CC File Offset: 0x00033BCC
		public DataShapeAnnotations Annotations { get; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x000359D4 File Offset: 0x00033BD4
		public WritableExpressionTable OutputExpressionTable { get; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x000359DC File Offset: 0x00033BDC
		public TranslationErrorContext ErrorContext { get; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x000359E4 File Offset: 0x00033BE4
		public BatchSortByMeasureExpressionMappings SortByMeasureExpressionMappings { get; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x000359EC File Offset: 0x00033BEC
		public CalculationExpressionMap CalculationMap { get; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x000359F4 File Offset: 0x00033BF4
		public GroupDetailMap GroupDetailMap { get; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x000359FC File Offset: 0x00033BFC
		public DataTransformReferenceMap TransformReferenceMap { get; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x00035A04 File Offset: 0x00033C04
		public DataShapeQueryTranslationTelemetry TelemetryInfo { get; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x00035A0C File Offset: 0x00033C0C
		public double EnhancedSamplingAdditionalKeyPointsRatio { get; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x00035A14 File Offset: 0x00033C14
		public bool ApplyTransformsInQuery { get; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x00035A1C File Offset: 0x00033C1C
		internal bool GenerateComposableQueryColumnNames { get; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x00035A24 File Offset: 0x00033C24
		public IFeatureSwitchProvider FeatureSwitches { get; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x00035A2C File Offset: 0x00033C2C
		public bool HasSortByMeasure
		{
			get
			{
				return this.SortByMeasureExpressionMappings.Count > 0;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x00035A3C File Offset: 0x00033C3C
		internal IDataShapeDefaultValueContextManager DefaultValueContextManager { get; }
	}
}
