using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular;
using Microsoft.PowerBI.ReportServer.PbixLib.Parsing;

namespace Microsoft.PowerBI.ReportServer.AsServer.ProviderManager
{
	// Token: 0x02000029 RID: 41
	internal interface IProviderManager
	{
		// Token: 0x060000EB RID: 235
		bool CanCreateCredentials(ProviderDataSource providerDataSource);

		// Token: 0x060000EC RID: 236
		ProviderDataSourceCredentials CreateCredentials(ProviderDataSource providerDataSource, IEnumerable<PbixDataSource> dataSources);

		// Token: 0x060000ED RID: 237
		IEnumerable<PbixDataSource> BuildDataModelDataSources(ProviderDataSourceInfo providerDataSourceInfo);

		// Token: 0x060000EE RID: 238
		ProviderDataSourceCredentials RemoveCredentials(ProviderDataSource providerDataSource);
	}
}
