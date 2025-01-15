using System;
using System.Collections.Generic;
using Microsoft.BusinessIntelligence;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000012 RID: 18
	internal sealed class DataShapingFeatureSwitchProvider : IFeatureSwitchProvider
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002691 File Offset: 0x00000891
		internal static DataShapingFeatureSwitchProvider Create(FeatureSwitches featureSwitches)
		{
			return new DataShapingFeatureSwitchProvider(featureSwitches, null);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000269A File Offset: 0x0000089A
		private DataShapingFeatureSwitchProvider(FeatureSwitches featureSwitches, IReadOnlyDictionary<FeatureSwitchKind, bool> overrides)
		{
			this.m_overrides = overrides;
			this.m_featureSwitches = featureSwitches;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000026B0 File Offset: 0x000008B0
		public bool IsEnabled(FeatureSwitchKind kind)
		{
			bool flag;
			if (this.m_overrides != null && this.m_overrides.TryGetValue(kind, out flag))
			{
				return flag;
			}
			switch (kind)
			{
			case FeatureSwitchKind.DSEMultiSubqueries:
				return this.m_featureSwitches.DSEMultiSubqueriesEnabled;
			case FeatureSwitchKind.QueryExtensionColumns:
				return this.m_featureSwitches.QueryExtensionColumnsEnabled;
			case FeatureSwitchKind.QueryNativeExpressions:
				return this.m_featureSwitches.QueryNativeExpressionsEnabled;
			case FeatureSwitchKind.SubqueryTransform:
				return this.m_featureSwitches.DSESubqueryTransformEnabled;
			case FeatureSwitchKind.SemanticQueryLet:
				return this.m_featureSwitches.SemanticQueryLetEnabled;
			case FeatureSwitchKind.MParameterColumnMapping:
				return this.m_featureSwitches.MParameterColumnMappingEnabled;
			case FeatureSwitchKind.TransformRefersSubquery:
				return this.m_featureSwitches.TransformRefersSubqueryEnabled;
			case FeatureSwitchKind.SparklineData:
				return this.m_featureSwitches.SparklineDataEnabled;
			case FeatureSwitchKind.IncludeSafeASQueryErrorsInDsr:
				return this.m_featureSwitches.IncludeSafeASQueryErrorsInDsrEnabled;
			case FeatureSwitchKind.QueryExecutionMetrics:
				return this.m_featureSwitches.QueryExecutionMetricsEnabled;
			case FeatureSwitchKind.DSESemanticQueryParameters:
				return this.m_featureSwitches.DSESemanticQueryParametersEnabled;
			case FeatureSwitchKind.QDMConceptualSchema:
				return this.m_featureSwitches.QDMConceptualSchemaEnabled;
			case FeatureSwitchKind.ConceptualSchema:
				return this.m_featureSwitches.DSEConceptualSchemaEnabled;
			case FeatureSwitchKind.VisualCalculations:
				return this.m_featureSwitches.VisualCalculationsEnabled;
			default:
				return false;
			}
		}

		// Token: 0x04000056 RID: 86
		private readonly IReadOnlyDictionary<FeatureSwitchKind, bool> m_overrides;

		// Token: 0x04000057 RID: 87
		private readonly FeatureSwitches m_featureSwitches;
	}
}
