using System;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000078 RID: 120
	internal static class DbProviderInfoExtensions
	{
		// Token: 0x06000438 RID: 1080 RVA: 0x0000FB2F File Offset: 0x0000DD2F
		public static bool IsSqlCe(this DbProviderInfo providerInfo)
		{
			return !string.IsNullOrWhiteSpace(providerInfo.ProviderInvariantName) && providerInfo.ProviderInvariantName.StartsWith("System.Data.SqlServerCe", StringComparison.OrdinalIgnoreCase);
		}
	}
}
