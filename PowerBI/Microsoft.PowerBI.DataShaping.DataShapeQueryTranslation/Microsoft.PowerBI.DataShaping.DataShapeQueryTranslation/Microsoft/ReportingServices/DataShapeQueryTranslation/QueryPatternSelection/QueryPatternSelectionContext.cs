using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryPatternSelection
{
	// Token: 0x0200006D RID: 109
	internal sealed class QueryPatternSelectionContext
	{
		// Token: 0x0600059E RID: 1438 RVA: 0x00014013 File Offset: 0x00012213
		internal QueryPatternSelectionContext(TranslationErrorContext errorContext, IFederatedConceptualSchema schema, ScopeTree scopeTree, DataShapeAnnotations annotations, ExpressionTable expressionTable, IFeatureSwitchProvider featureSwitchProvider, bool applyTransformsInQuery)
		{
			this.ErrorContext = errorContext;
			this.Schema = schema;
			this.ScopeTree = scopeTree;
			this.Annotations = annotations;
			this.ExpressionTable = expressionTable;
			this.ApplyTransformsInQuery = applyTransformsInQuery;
			this.FeatureSwitchProvider = featureSwitchProvider;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00014050 File Offset: 0x00012250
		public DataShapeAnnotations Annotations { get; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x00014058 File Offset: 0x00012258
		public ScopeTree ScopeTree { get; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00014060 File Offset: 0x00012260
		public ExpressionTable ExpressionTable { get; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x00014068 File Offset: 0x00012268
		public IFederatedConceptualSchema Schema { get; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00014070 File Offset: 0x00012270
		public TranslationErrorContext ErrorContext { get; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00014078 File Offset: 0x00012278
		public IFeatureSwitchProvider FeatureSwitchProvider { get; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x00014080 File Offset: 0x00012280
		public bool ApplyTransformsInQuery { get; }
	}
}
