using System;
using System.Threading;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x02000089 RID: 137
	internal class QueryGenerationContext
	{
		// Token: 0x0600068B RID: 1675 RVA: 0x00017DD4 File Offset: 0x00015FD4
		internal QueryGenerationContext(DataShape dataShape, ScopeTree scopeTree, DataShapeAnnotations annotations, FederatedEntityDataModel model, IFederatedConceptualSchema schema, TranslationErrorContext errorContext, IFeatureSwitchProvider featureSwitchProvider, ExpressionTable expressionTable, Microsoft.DataShaping.ServiceContracts.ITracer tracer, bool suppressModelGrouping, CancellationToken cancellationToken)
		{
			this.DataShape = dataShape;
			this.ScopeTree = scopeTree;
			this.Annotations = annotations;
			this.Model = model;
			this.Schema = schema;
			this.DaxCapabilities = DaxCapabilitiesBuilder.BuildCapabilities((model != null) ? model.BaseModel : null, schema.GetDefaultSchema(), featureSwitchProvider);
			this.ErrorContext = errorContext;
			this.FeatureSwitchProvider = featureSwitchProvider;
			this.ExpressionTable = expressionTable;
			this.Tracer = tracer;
			this.SuppressModelGrouping = suppressModelGrouping;
			this.CancellationToken = cancellationToken;
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x00017E5E File Offset: 0x0001605E
		public DataShape DataShape { get; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x00017E66 File Offset: 0x00016066
		public ScopeTree ScopeTree { get; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x00017E6E File Offset: 0x0001606E
		public DataShapeAnnotations Annotations { get; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00017E76 File Offset: 0x00016076
		public FederatedEntityDataModel Model { get; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x00017E7E File Offset: 0x0001607E
		public IFederatedConceptualSchema Schema { get; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x00017E86 File Offset: 0x00016086
		internal DaxCapabilities DaxCapabilities { get; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x00017E8E File Offset: 0x0001608E
		public TranslationErrorContext ErrorContext { get; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00017E96 File Offset: 0x00016096
		public IFeatureSwitchProvider FeatureSwitchProvider { get; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x00017E9E File Offset: 0x0001609E
		public ExpressionTable ExpressionTable { get; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x00017EA6 File Offset: 0x000160A6
		internal Microsoft.DataShaping.ServiceContracts.ITracer Tracer { get; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x00017EAE File Offset: 0x000160AE
		internal bool SuppressModelGrouping { get; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00017EB6 File Offset: 0x000160B6
		internal CancellationToken CancellationToken { get; }
	}
}
