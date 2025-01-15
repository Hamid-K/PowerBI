using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics.V1
{
	// Token: 0x02000FA4 RID: 4004
	internal sealed class AdobeAnalyticsCubeContextProviderV1 : AdobeAnalyticsCubeContextProvider<AdobeAnalyticsReportDescriptionV1>
	{
		// Token: 0x0600695A RID: 26970 RVA: 0x0016A3F3 File Offset: 0x001685F3
		public AdobeAnalyticsCubeContextProviderV1(AdobeAnalyticsCube cube, AdobeAnalyticsServiceV1 service)
			: base(cube)
		{
			this.service = service;
		}

		// Token: 0x17001E54 RID: 7764
		// (get) Token: 0x0600695B RID: 26971 RVA: 0x0016A403 File Offset: 0x00168603
		public override IEngineHost EngineHost
		{
			get
			{
				return this.service.EngineHost;
			}
		}

		// Token: 0x0600695C RID: 26972 RVA: 0x0016A410 File Offset: 0x00168610
		protected override void AddDateGranularityDimension(CubeObjectTableBuilder dimensionsTableBuilder)
		{
			dimensionsTableBuilder.AddAdobeAnalyticsDateGranularityDimension(this);
		}

		// Token: 0x0600695D RID: 26973 RVA: 0x0016A419 File Offset: 0x00168619
		protected override void AddDateGranularityDisplayFolder(CubeObjectTableBuilder rootBuilder)
		{
			rootBuilder.AddAdobeAnalyticsDateGranularityDisplayFolder();
		}

		// Token: 0x0600695E RID: 26974 RVA: 0x0016A421 File Offset: 0x00168621
		protected override AdobeAnalyticsQueryCompiler<AdobeAnalyticsReportDescriptionV1> CreateCompiler(AdobeAnalyticsCube cube, IList<ParameterArguments> parameters)
		{
			return new AdobeAnalyticsQueryCompilerV1(cube, parameters);
		}

		// Token: 0x0600695F RID: 26975 RVA: 0x0016A42A File Offset: 0x0016862A
		protected override IEnumerator<IValueReference> GetCubeContextEnumerator(AdobeAnalyticsReportDescriptionV1 compiledReport, Keys keys)
		{
			return new AdobeAnalyticsCubeContextProviderV1.AdobeAnalyticsContextEnumerator(this.service, this.cube, compiledReport, keys);
		}

		// Token: 0x06006960 RID: 26976 RVA: 0x0016A43F File Offset: 0x0016863F
		protected override bool IsDateGranularity(string dimension)
		{
			return AdobeAnalyticsDateGranularityHierarchyV1.IsGranularity(dimension);
		}

		// Token: 0x04003A32 RID: 14898
		private readonly AdobeAnalyticsServiceV1 service;

		// Token: 0x02000FA5 RID: 4005
		private class AdobeAnalyticsContextEnumerator : AdobeAnalyticsResultEnumeratorV1
		{
			// Token: 0x06006961 RID: 26977 RVA: 0x0016A447 File Offset: 0x00168647
			public AdobeAnalyticsContextEnumerator(AdobeAnalyticsServiceV1 service, AdobeAnalyticsCube cube, AdobeAnalyticsReportDescriptionV1 compiledExpression, Keys keys)
			{
				this.service = service;
				this.cube = cube;
				this.compiledExpression = compiledExpression;
				this.keys = keys;
			}

			// Token: 0x06006962 RID: 26978 RVA: 0x0016A46C File Offset: 0x0016866C
			protected override Value GetResult()
			{
				return this.service.GetReport(AdobeAnalyticsRequestV1.NewReportRequest(this.compiledExpression, this.cube.Company), this.cube.Company);
			}

			// Token: 0x06006963 RID: 26979 RVA: 0x0016A49A File Offset: 0x0016869A
			protected override IList<string> GetGranularityLevels()
			{
				return this.compiledExpression.GranularityLevels.ToArray<string>();
			}

			// Token: 0x06006964 RID: 26980 RVA: 0x0016A4AC File Offset: 0x001686AC
			protected override IList<string> GetDimensions()
			{
				return this.compiledExpression.Dimensions.ToArray<string>();
			}

			// Token: 0x06006965 RID: 26981 RVA: 0x0016A4BE File Offset: 0x001686BE
			protected override IList<string> GetMeasures()
			{
				return this.compiledExpression.Measures.ToArray<string>();
			}

			// Token: 0x06006966 RID: 26982 RVA: 0x0016A4D0 File Offset: 0x001686D0
			protected override Keys GetKeys()
			{
				return this.keys;
			}

			// Token: 0x04003A33 RID: 14899
			private readonly AdobeAnalyticsServiceV1 service;

			// Token: 0x04003A34 RID: 14900
			private readonly AdobeAnalyticsCube cube;

			// Token: 0x04003A35 RID: 14901
			private readonly AdobeAnalyticsReportDescriptionV1 compiledExpression;

			// Token: 0x04003A36 RID: 14902
			private readonly Keys keys;
		}
	}
}
