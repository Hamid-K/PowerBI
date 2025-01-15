using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000078 RID: 120
	internal sealed class RDLUpgradeResult
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0001708B File Offset: 0x0001528B
		public bool HasUnsupportedDundasCRIFeatures
		{
			get
			{
				return this.HasUnsupportedDundasChartFeatures || this.HasUnsupportedDundasGaugeFeatures;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0001709D File Offset: 0x0001529D
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x000170A5 File Offset: 0x000152A5
		public bool HasUnsupportedDundasChartFeatures
		{
			get
			{
				return this.hasUnsupportedDundasChartFeatures;
			}
			internal set
			{
				this.hasUnsupportedDundasChartFeatures = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x000170AE File Offset: 0x000152AE
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x000170B6 File Offset: 0x000152B6
		public bool HasUnsupportedDundasGaugeFeatures
		{
			get
			{
				return this.hasUnsupportedDundasGaugeFeatures;
			}
			internal set
			{
				this.hasUnsupportedDundasGaugeFeatures = value;
			}
		}

		// Token: 0x04000113 RID: 275
		private bool hasUnsupportedDundasChartFeatures;

		// Token: 0x04000114 RID: 276
		private bool hasUnsupportedDundasGaugeFeatures;
	}
}
