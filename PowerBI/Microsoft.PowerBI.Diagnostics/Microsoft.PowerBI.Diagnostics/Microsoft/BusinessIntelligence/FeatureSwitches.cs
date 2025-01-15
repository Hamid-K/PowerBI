using System;
using System.Text;

namespace Microsoft.BusinessIntelligence
{
	// Token: 0x02000038 RID: 56
	public sealed class FeatureSwitches
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x000037F6 File Offset: 0x000019F6
		public FeatureSwitches(IFeatureSwitchesProxy featureSwitchesProxy = null)
		{
			this.FeatureSwitchesProxy = featureSwitchesProxy;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003805 File Offset: 0x00001A05
		public IFeatureSwitchesProxy FeatureSwitchesProxy { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000380D File Offset: 0x00001A0D
		public bool IncludeSafeASQueryErrorsInDsrEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("IncludeSafeASQueryErrorsInDsr");
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x0000381A File Offset: 0x00001A1A
		public bool LiveConnectEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("LiveConnectEnabled");
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003827 File Offset: 0x00001A27
		public bool CsdlCachingEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("CsdlCaching");
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003834 File Offset: 0x00001A34
		public bool QueryNativeExpressionsEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("QueryNativeExpressions");
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003841 File Offset: 0x00001A41
		public bool QueryExtensionColumnsEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("QueryExtensionColumns");
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000384E File Offset: 0x00001A4E
		public bool QueryExecutionMetricsEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("QueryExecutionMetrics");
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000385B File Offset: 0x00001A5B
		public bool DSEMultiSubqueriesEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("DSEMultiSubqueries");
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00003868 File Offset: 0x00001A68
		public bool PreserveSQFilterCorrelationEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("PreserveSQFilterCorrelation");
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00003875 File Offset: 0x00001A75
		public bool PreserveSQFilterTargetsEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("PreserveSQFilterTargets");
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003882 File Offset: 0x00001A82
		public bool InsightsContinuationTokensEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("InsightsContinuationTokens");
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000CF RID: 207 RVA: 0x0000388F File Offset: 0x00001A8F
		public bool DSESubqueryTransformEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("DSESubqueryTransform");
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000389C File Offset: 0x00001A9C
		public bool SemanticQueryLetEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("SemanticQueryLet");
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x000038A9 File Offset: 0x00001AA9
		public bool MParameterColumnMappingEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("MParameterColumnMapping");
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000038B6 File Offset: 0x00001AB6
		public bool InsightsComplexTuplesEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("InsightsComplexTuples");
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x000038C3 File Offset: 0x00001AC3
		public bool InsightsMParameterEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("InsightsMParameter");
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000038D0 File Offset: 0x00001AD0
		public bool AnalyticsSamplingTransformEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("AnalyticsSamplingTransform");
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x000038DD File Offset: 0x00001ADD
		public bool AnalyticsAnomalyDetectionTransformEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("AnalyticsAnomalyDetectionTransform");
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000038EA File Offset: 0x00001AEA
		public bool AnalyticsNLGTransformsEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("AnalyticsNLGTransforms");
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x000038F7 File Offset: 0x00001AF7
		public bool MsolapTracingEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("MsolapTracing");
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003904 File Offset: 0x00001B04
		public bool TransformRefersSubqueryEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("TransformRefersSubquery");
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00003911 File Offset: 0x00001B11
		public bool QDMConceptualSchemaEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("QDMConceptualSchema");
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000DA RID: 218 RVA: 0x0000391E File Offset: 0x00001B1E
		public bool SparklineDataEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("SparklineData");
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000DB RID: 219 RVA: 0x0000392B File Offset: 0x00001B2B
		public bool DSESemanticQueryParametersEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("DSESemanticQueryParameters");
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003938 File Offset: 0x00001B38
		public bool DSEConceptualSchemaEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("DSEConceptualSchema");
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00003945 File Offset: 0x00001B45
		public bool VisualCalculationsEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("VisualCalculations");
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00003952 File Offset: 0x00001B52
		public bool AsyncExploreClientFlowsEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("AsyncExploreClientFlows");
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000DF RID: 223 RVA: 0x0000395F File Offset: 0x00001B5F
		internal bool TestFeatureEnabled
		{
			get
			{
				return this.GetSwitchValueForProperty("TestFeature");
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000396C File Offset: 0x00001B6C
		private bool GetSwitchValueForProperty(string featureSwitchName)
		{
			return this.FeatureSwitchesProxy != null && this.FeatureSwitchesProxy.GetSwitchValue(featureSwitchName);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003984 File Offset: 0x00001B84
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			FeatureSwitches.AppendKeyValue(stringBuilder, "IncludeSafeASQueryErrorsInDsr", this.IncludeSafeASQueryErrorsInDsrEnabled);
			FeatureSwitches.AppendKeyValue(stringBuilder, "LiveConnectEnabled", this.LiveConnectEnabled);
			FeatureSwitches.AppendKeyValue(stringBuilder, "CsdlCaching", this.CsdlCachingEnabled);
			FeatureSwitches.AppendKeyValue(stringBuilder, "QueryNativeExpressions", this.QueryNativeExpressionsEnabled);
			FeatureSwitches.AppendKeyValue(stringBuilder, "QueryExtensionColumns", this.QueryExtensionColumnsEnabled);
			FeatureSwitches.AppendKeyValue(stringBuilder, "QueryExecutionMetrics", this.QueryExecutionMetricsEnabled);
			FeatureSwitches.AppendKeyValue(stringBuilder, "InsightsContinuationTokens", this.InsightsContinuationTokensEnabled);
			FeatureSwitches.AppendKeyValue(stringBuilder, "DSEMultiSubqueries", this.DSEMultiSubqueriesEnabled);
			FeatureSwitches.AppendKeyValue(stringBuilder, "PreserveSQFilterCorrelation", this.PreserveSQFilterCorrelationEnabled);
			FeatureSwitches.AppendKeyValue(stringBuilder, "PreserveSQFilterTargets", this.PreserveSQFilterTargetsEnabled);
			FeatureSwitches.AppendKeyValue(stringBuilder, "DSESubqueryTransform", this.DSESubqueryTransformEnabled);
			FeatureSwitches.AppendKeyValue(stringBuilder, "SemanticQueryLet", this.SemanticQueryLetEnabled);
			FeatureSwitches.AppendKeyValue(stringBuilder, "InsightsComplexTuples", this.InsightsComplexTuplesEnabled);
			FeatureSwitches.AppendKeyValue(stringBuilder, "InsightsMParameter", this.InsightsMParameterEnabled);
			FeatureSwitches.AppendKeyValue(stringBuilder, "MParameterColumnMapping", this.MParameterColumnMappingEnabled);
			FeatureSwitches.AppendKeyValue(stringBuilder, "AnalyticsSamplingTransform", this.AnalyticsSamplingTransformEnabled);
			FeatureSwitches.AppendKeyValue(stringBuilder, "AnalyticsAnomalyDetectionTransform", this.AnalyticsAnomalyDetectionTransformEnabled);
			FeatureSwitches.AppendKeyValue(stringBuilder, "AnalyticsNLGTransforms", this.AnalyticsNLGTransformsEnabled);
			FeatureSwitches.AppendKeyValue(stringBuilder, "MsolapTracing", this.MsolapTracingEnabled);
			FeatureSwitches.AppendKeyValue(stringBuilder, "QDMConceptualSchema", this.QDMConceptualSchemaEnabled);
			FeatureSwitches.AppendKeyValue(stringBuilder, "SparklineData", this.SparklineDataEnabled);
			FeatureSwitches.AppendKeyValue(stringBuilder, "DSESemanticQueryParameters", this.DSESemanticQueryParametersEnabled);
			FeatureSwitches.AppendKeyValue(stringBuilder, "DSEConceptualSchema", this.DSEConceptualSchemaEnabled);
			FeatureSwitches.AppendKeyValue(stringBuilder, "VisualCalculations", this.VisualCalculationsEnabled);
			FeatureSwitches.AppendKeyValue(stringBuilder, "AsyncExploreClientFlows", this.AsyncExploreClientFlowsEnabled);
			return stringBuilder.ToString();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003B44 File Offset: 0x00001D44
		private static void AppendKeyValue(StringBuilder sb, string key, bool value)
		{
			sb.Append(key + " = " + value.ToString() + ",");
		}

		// Token: 0x0400006D RID: 109
		private const string IncludeSafeASQueryErrorsInDsrKey = "IncludeSafeASQueryErrorsInDsr";

		// Token: 0x0400006E RID: 110
		private const string LiveConnectKey = "LiveConnectEnabled";

		// Token: 0x0400006F RID: 111
		private const string CsdlCachingKey = "CsdlCaching";

		// Token: 0x04000070 RID: 112
		private const string QueryNativeExpressionsKey = "QueryNativeExpressions";

		// Token: 0x04000071 RID: 113
		private const string QueryExtensionColumnsKey = "QueryExtensionColumns";

		// Token: 0x04000072 RID: 114
		private const string QueryExecutionMetricsKey = "QueryExecutionMetrics";

		// Token: 0x04000073 RID: 115
		private const string DSEMultiSubqueriesKey = "DSEMultiSubqueries";

		// Token: 0x04000074 RID: 116
		private const string PreserveSQFilterCorrelationKey = "PreserveSQFilterCorrelation";

		// Token: 0x04000075 RID: 117
		private const string PreserveSQFilterTargetsKey = "PreserveSQFilterTargets";

		// Token: 0x04000076 RID: 118
		private const string DSESubqueryTransformKey = "DSESubqueryTransform";

		// Token: 0x04000077 RID: 119
		private const string SemanticQueryLetKey = "SemanticQueryLet";

		// Token: 0x04000078 RID: 120
		private const string MParameterColumnMappingKey = "MParameterColumnMapping";

		// Token: 0x04000079 RID: 121
		private const string MsolapTracingKey = "MsolapTracing";

		// Token: 0x0400007A RID: 122
		internal const string InsightsContinuationTokensKey = "InsightsContinuationTokens";

		// Token: 0x0400007B RID: 123
		internal const string InsightsComplexTuplesKey = "InsightsComplexTuples";

		// Token: 0x0400007C RID: 124
		internal const string InsightsMParameterKey = "InsightsMParameter";

		// Token: 0x0400007D RID: 125
		internal const string AnalyticsSamplingTransformKey = "AnalyticsSamplingTransform";

		// Token: 0x0400007E RID: 126
		internal const string AnalyticsAnomalyDetectionTransformKey = "AnalyticsAnomalyDetectionTransform";

		// Token: 0x0400007F RID: 127
		internal const string AnalyticsNLGTransformsKey = "AnalyticsNLGTransforms";

		// Token: 0x04000080 RID: 128
		internal const string TransformRefersSubqueryKey = "TransformRefersSubquery";

		// Token: 0x04000081 RID: 129
		internal const string QDMConceptualSchemaKey = "QDMConceptualSchema";

		// Token: 0x04000082 RID: 130
		internal const string SparklineDataKey = "SparklineData";

		// Token: 0x04000083 RID: 131
		internal const string DSESemanticQueryParametersKey = "DSESemanticQueryParameters";

		// Token: 0x04000084 RID: 132
		internal const string DSEConceptualSchemaKey = "DSEConceptualSchema";

		// Token: 0x04000085 RID: 133
		internal const string VisualCalculationsKey = "VisualCalculations";

		// Token: 0x04000086 RID: 134
		internal const string AsyncExploreClientFlowsKey = "AsyncExploreClientFlows";

		// Token: 0x04000087 RID: 135
		private static readonly StringComparer NameComparer = StringComparer.OrdinalIgnoreCase;
	}
}
