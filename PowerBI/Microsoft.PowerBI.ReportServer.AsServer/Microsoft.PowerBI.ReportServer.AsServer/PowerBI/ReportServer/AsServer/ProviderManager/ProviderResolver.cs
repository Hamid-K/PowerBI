using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Tabular;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.PowerBI.ReportServer.AsServer.Mashup;

namespace Microsoft.PowerBI.ReportServer.AsServer.ProviderManager
{
	// Token: 0x0200002D RID: 45
	internal class ProviderResolver
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00005300 File Offset: 0x00003500
		static ProviderResolver()
		{
			ProviderResolver._providers.Add(new SqlProviderManager());
			ProviderResolver._providers.Add(new MashupProviderManager());
			ProviderResolver._providers.Add(new OracleProviderManager());
			ProviderResolver._providers.Add(new TeradataProviderManager());
			ProviderResolver._providers.Add(new OleDbProviderManager());
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005368 File Offset: 0x00003568
		public static IProviderManager ResolveProvider(ProviderDataSource dataSource)
		{
			IProviderManager providerManager = ProviderResolver._providers.FirstOrDefault((IProviderManager p) => p.CanCreateCredentials(dataSource));
			if (providerManager == null)
			{
				Logger.Info("This datasource contains an unsupported provider, scheduled data refresh will not available: DataSource: {0}, Provider: {1}, ConnectionString: {2}", new object[]
				{
					dataSource.Name,
					dataSource.Provider ?? "(null)",
					dataSource.ConnectionString ?? "(null)"
				});
			}
			return providerManager;
		}

		// Token: 0x04000078 RID: 120
		private static HashSet<IProviderManager> _providers = new HashSet<IProviderManager>();
	}
}
