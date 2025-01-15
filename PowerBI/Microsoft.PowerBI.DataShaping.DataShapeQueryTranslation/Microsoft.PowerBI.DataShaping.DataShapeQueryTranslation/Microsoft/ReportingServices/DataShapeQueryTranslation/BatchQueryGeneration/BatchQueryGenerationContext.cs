using System;
using System.Threading;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.PowerBI.Analytics.Contracts.DaxDataTransform;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm;
using Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x0200013D RID: 317
	internal sealed class BatchQueryGenerationContext : QueryGenerationContext
	{
		// Token: 0x06000BB9 RID: 3001 RVA: 0x0002F0E8 File Offset: 0x0002D2E8
		internal BatchQueryGenerationContext(DataShape dataShape, ScopeTree scopeTree, DataShapeAnnotations annotations, FederatedEntityDataModel model, IFederatedConceptualSchema schema, TranslationErrorContext errorContext, IFeatureSwitchProvider featureSwitchProvider, ExpressionTable expressionTable, Microsoft.DataShaping.ServiceContracts.ITracer tracer, BatchSortByMeasureExpressionMappings sortByMeasureExpressions, GroupDetailMap groupDetailMapping, CalculationExpressionMap calculationExpressionMapping, IDaxDataTransformMetadataFactory transformMetadataFactory, DataShapeQueryTranslationTelemetry telemetry, bool generateComposableQuery, bool suppressModelGrouping, CancellationToken cancellationToken)
			: base(dataShape, scopeTree, annotations, model, schema, errorContext, featureSwitchProvider, expressionTable, tracer, suppressModelGrouping, cancellationToken)
		{
			this.SortByMeasureExpressions = sortByMeasureExpressions;
			this.GroupDetailMapping = groupDetailMapping;
			this.CalculationExpressionMapping = calculationExpressionMapping;
			this.TransformMetadataFactory = transformMetadataFactory;
			this.GenerateComposableQuery = generateComposableQuery;
			this.Telemetry = telemetry;
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x0002F13E File Offset: 0x0002D33E
		public BatchSortByMeasureExpressionMappings SortByMeasureExpressions { get; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0002F146 File Offset: 0x0002D346
		public GroupDetailMap GroupDetailMapping { get; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x0002F14E File Offset: 0x0002D34E
		public CalculationExpressionMap CalculationExpressionMapping { get; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x0002F156 File Offset: 0x0002D356
		public IDaxDataTransformMetadataFactory TransformMetadataFactory { get; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0002F15E File Offset: 0x0002D35E
		public bool GenerateComposableQuery { get; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x0002F166 File Offset: 0x0002D366
		public DataShapeQueryTranslationTelemetry Telemetry { get; }
	}
}
