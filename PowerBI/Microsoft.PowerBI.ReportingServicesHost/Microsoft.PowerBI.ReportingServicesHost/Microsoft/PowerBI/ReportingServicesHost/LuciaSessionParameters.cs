using System;
using System.Threading.Tasks;
using Microsoft.PowerBI.Data.ModelSchemaAnalysis;
using Microsoft.PowerBI.ExploreHost.Lucia;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x0200004D RID: 77
	public sealed class LuciaSessionParameters
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x00004E4F File Offset: 0x0000304F
		public LuciaSessionParameters(Func<string> getLinguisticSchemaJson, LuciaSessionOptions options, IBulkMeasureExpressionProvider measureExpressionProvider = null, IStatisticsSourceProvider statisticsSourceProvider = null, Func<string, Task<string>> getDaxTemplate = null)
		{
			this.GetLinguisticSchemaJson = getLinguisticSchemaJson;
			this.MeasureExpressionProvider = measureExpressionProvider;
			this.StatisticsSourceProvider = statisticsSourceProvider;
			this.Options = options;
			this.GetDaxTemplate = getDaxTemplate;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00004E7C File Offset: 0x0000307C
		public Func<string> GetLinguisticSchemaJson { get; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00004E84 File Offset: 0x00003084
		public IBulkMeasureExpressionProvider MeasureExpressionProvider { get; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00004E8C File Offset: 0x0000308C
		public IStatisticsSourceProvider StatisticsSourceProvider { get; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00004E94 File Offset: 0x00003094
		public LuciaSessionOptions Options { get; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00004E9C File Offset: 0x0000309C
		public Func<string, Task<string>> GetDaxTemplate { get; }
	}
}
