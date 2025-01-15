using System;
using Microsoft.AnalysisServices.Tabular;

namespace Microsoft.PowerBI.ReportServer.AsServer.ProviderManager
{
	// Token: 0x02000027 RID: 39
	internal class ProviderDataSourceInfo
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00004D3A File Offset: 0x00002F3A
		public ProviderDataSourceInfo(ProviderDataSource providerDataSource, bool isDirectQuery)
		{
			this.providerDataSource = providerDataSource;
			this.isDirectQuery = isDirectQuery;
		}

		// Token: 0x0400006A RID: 106
		public ProviderDataSource providerDataSource;

		// Token: 0x0400006B RID: 107
		public bool isDirectQuery;
	}
}
