using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics.V2
{
	// Token: 0x02000F86 RID: 3974
	internal sealed class AdobeAnalyticsCubeContextProviderV2 : AdobeAnalyticsCubeContextProvider<AdobeAnalyticsReportDescriptionV2>
	{
		// Token: 0x060068B3 RID: 26803 RVA: 0x00167C67 File Offset: 0x00165E67
		public AdobeAnalyticsCubeContextProviderV2(AdobeAnalyticsCube cube, AdobeAnalyticsServiceV2 service)
			: base(cube)
		{
			this.service = service;
		}

		// Token: 0x17001E3E RID: 7742
		// (get) Token: 0x060068B4 RID: 26804 RVA: 0x00167C77 File Offset: 0x00165E77
		public override IEngineHost EngineHost
		{
			get
			{
				return this.service.EngineHost;
			}
		}

		// Token: 0x060068B5 RID: 26805 RVA: 0x00167C84 File Offset: 0x00165E84
		protected override void AddDateGranularityDimension(CubeObjectTableBuilder dimensionsTableBuilder)
		{
			dimensionsTableBuilder.AddAdobeAnalyticsDateGranularityDimension(this);
		}

		// Token: 0x060068B6 RID: 26806 RVA: 0x00167C8D File Offset: 0x00165E8D
		protected override void AddDateGranularityDisplayFolder(CubeObjectTableBuilder rootBuilder)
		{
			rootBuilder.AddAdobeAnalyticsDateGranularityDisplayFolder();
		}

		// Token: 0x060068B7 RID: 26807 RVA: 0x00167C95 File Offset: 0x00165E95
		protected override AdobeAnalyticsQueryCompiler<AdobeAnalyticsReportDescriptionV2> CreateCompiler(AdobeAnalyticsCube cube, IList<ParameterArguments> parameters)
		{
			return new AdobeAnalyticsQueryCompilerV2(cube, parameters);
		}

		// Token: 0x060068B8 RID: 26808 RVA: 0x00167C9E File Offset: 0x00165E9E
		protected override IEnumerator<IValueReference> GetCubeContextEnumerator(AdobeAnalyticsReportDescriptionV2 compiledReport, Keys keys)
		{
			return new AdobeAnalyticsCubeContextProviderV2.AdobeAnalyticsContextEnumerator(this.service, this.cube, compiledReport, keys);
		}

		// Token: 0x060068B9 RID: 26809 RVA: 0x00167CB3 File Offset: 0x00165EB3
		protected override bool IsDateGranularity(string dimension)
		{
			return AdobeAnalyticsDateGranularityHierarchyV2.IsGranularity(dimension);
		}

		// Token: 0x060068BA RID: 26810 RVA: 0x00167CBC File Offset: 0x00165EBC
		protected override void AddDimensionDisplayFolder(CubeObjectTableBuilder rootBuilder)
		{
			CubeObjectTableBuilder cubeObjectTableBuilder = CubeObjectTableBuilder.NewWithoutLink();
			foreach (AdobeAnalyticsDimension adobeAnalyticsDimension in this.cube.Dimensions)
			{
				if (!this.IsDateGranularity(adobeAnalyticsDimension.Id) && !AdobeAnalyticsReportDescriptionV2.IsSegmentDimension(adobeAnalyticsDimension.Id))
				{
					CubeObjectTableBuilder cubeObjectTableBuilder2 = CubeObjectTableBuilder.NewWithoutLink();
					cubeObjectTableBuilder2.AddDimensionAttribute(adobeAnalyticsDimension.Id, adobeAnalyticsDimension.Name);
					cubeObjectTableBuilder.AddDimensionFolder(adobeAnalyticsDimension.Id, adobeAnalyticsDimension.Name, adobeAnalyticsDimension.Name, cubeObjectTableBuilder2.ToTable());
				}
			}
			rootBuilder.AddFolder("Dimensions", cubeObjectTableBuilder.ToTable());
			this.AddSegmentDimensionDisplayFolder(rootBuilder);
		}

		// Token: 0x060068BB RID: 26811 RVA: 0x00167D78 File Offset: 0x00165F78
		private void AddSegmentDimensionDisplayFolder(CubeObjectTableBuilder rootBuilder)
		{
			CubeObjectTableBuilder cubeObjectTableBuilder = CubeObjectTableBuilder.NewWithoutLink();
			CubeObjectTableBuilder cubeObjectTableBuilder2 = CubeObjectTableBuilder.NewWithoutLink();
			AdobeAnalyticsDimension segmentDimension = AdobeAnalyticsReportDescriptionV2.SegmentDimension;
			cubeObjectTableBuilder2.AddDimensionAttribute(segmentDimension.Id, segmentDimension.Name);
			cubeObjectTableBuilder.AddDimensionFolder(segmentDimension.Id, segmentDimension.Name, segmentDimension.Name, cubeObjectTableBuilder2.ToTable());
			rootBuilder.AddFolder("Segment Dimension", cubeObjectTableBuilder.ToTable());
		}

		// Token: 0x040039AF RID: 14767
		private readonly AdobeAnalyticsServiceV2 service;

		// Token: 0x02000F87 RID: 3975
		private class AdobeAnalyticsContextEnumerator : AdobeAnalyticsResultEnumeratorV2
		{
			// Token: 0x060068BC RID: 26812 RVA: 0x00167DD8 File Offset: 0x00165FD8
			public AdobeAnalyticsContextEnumerator(AdobeAnalyticsServiceV2 service, AdobeAnalyticsCube cube, AdobeAnalyticsReportDescriptionV2 compiledExpression, Keys keys)
			{
				this.service = service;
				this.cube = cube;
				this.compiledExpression = compiledExpression;
				this.keys = keys;
			}

			// Token: 0x060068BD RID: 26813 RVA: 0x00167DFD File Offset: 0x00165FFD
			protected override Value GetResult()
			{
				return this.service.GetReport(this.compiledExpression, this.cube.Company);
			}

			// Token: 0x060068BE RID: 26814 RVA: 0x00167E1C File Offset: 0x0016601C
			protected override IList<string> GetDimensions()
			{
				IEnumerable<string> granularityLevels = this.compiledExpression.GranularityLevels;
				IEnumerable<string> enumerable = this.compiledExpression.Dimensions.OrderBy((string d) => d);
				return granularityLevels.Concat(enumerable).ToList<string>();
			}

			// Token: 0x060068BF RID: 26815 RVA: 0x00167E6F File Offset: 0x0016606F
			protected override IList<string> GetMeasures()
			{
				return this.compiledExpression.Metrics.ToArray<string>();
			}

			// Token: 0x060068C0 RID: 26816 RVA: 0x00167E81 File Offset: 0x00166081
			protected override Keys GetKeys()
			{
				return this.keys;
			}

			// Token: 0x040039B0 RID: 14768
			private readonly AdobeAnalyticsServiceV2 service;

			// Token: 0x040039B1 RID: 14769
			private readonly AdobeAnalyticsCube cube;

			// Token: 0x040039B2 RID: 14770
			private readonly AdobeAnalyticsReportDescriptionV2 compiledExpression;

			// Token: 0x040039B3 RID: 14771
			private readonly Keys keys;
		}
	}
}
