using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav
{
	// Token: 0x0200004B RID: 75
	public class TransformCapabilities
	{
		// Token: 0x06000140 RID: 320 RVA: 0x00002D48 File Offset: 0x00000F48
		public TransformCapabilities(IReadOnlyList<string> daxExtensionFunctionNames)
		{
			this.SupportedTransformCapabilities = new HashSet<TransformCapability>();
			if (daxExtensionFunctionNames != null)
			{
				foreach (string text in daxExtensionFunctionNames)
				{
					string text2;
					if (this._daxFunctionToTransformAlgorithmMappping.TryGetValue(text, out text2))
					{
						this.SupportedTransformCapabilities.Add(new TransformCapability(text2));
					}
				}
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00002EE0 File Offset: 0x000010E0
		public HashSet<TransformCapability> SupportedTransformCapabilities { get; }

		// Token: 0x06000142 RID: 322 RVA: 0x00002EE8 File Offset: 0x000010E8
		public bool IsTransformCapabilitySupported(TransformCapability transformCapability)
		{
			return this.SupportedTransformCapabilities.Contains(transformCapability);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00002EF6 File Offset: 0x000010F6
		public bool IsTransformSupported(string transformAlgorithmName)
		{
			return this.IsTransformCapabilitySupported(new TransformCapability(transformAlgorithmName));
		}

		// Token: 0x040000F4 RID: 244
		private readonly Dictionary<string, string> _daxFunctionToTransformAlgorithmMappping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
		{
			{ "SpatialClustering", "SpatialClustering" },
			{ "KMeansClustering", "KMeansClustering" },
			{ "AI.SampleTimeSeriesData", "SampleTimeSeriesData" },
			{ "AI.DetectAnomaly", "DetectAnomaly" },
			{ "AI.RegionWithMostPointsSummary", "RegionWithMostPointsSummary" },
			{ "AI.ChangeSummary", "ChangeSummary" },
			{ "AI.AggregateSummary", "AggregateSummary" },
			{ "AI.TopRightHandCornerSummary", "TopRightHandCornerSummary" },
			{ "AI.LargestDifferenceSummary", "LargestDifferenceSummary" },
			{ "AI.LargestCategorySummary", "LargestCategorySummary" },
			{ "AI.Unpivot", "Unpivot" },
			{ "AI.HighPointLowPointSummary", "HighPointLowPointSummary" },
			{ "AI.TrendSummary", "TrendSummary" },
			{ "AI.CorrelationSummary", "CorrelationSummary" },
			{ "AI.LargestDivergenceSummary", "LargestDivergenceSummary" },
			{ "AI.ExtractTrend", "ExtractTrend" },
			{ "AI.SampleAndDetectAnomaly", "SampleAndDetectAnomaly" }
		};
	}
}
