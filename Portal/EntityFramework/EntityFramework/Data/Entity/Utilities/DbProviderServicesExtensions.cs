using System;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Utilities
{
	// Token: 0x0200007A RID: 122
	internal static class DbProviderServicesExtensions
	{
		// Token: 0x0600043A RID: 1082 RVA: 0x0000FB9C File Offset: 0x0000DD9C
		public static string GetProviderManifestTokenChecked(this DbProviderServices providerServices, DbConnection connection)
		{
			string providerManifestToken;
			try
			{
				providerManifestToken = providerServices.GetProviderManifestToken(connection);
			}
			catch (ProviderIncompatibleException ex)
			{
				throw new ProviderIncompatibleException(Strings.FailedToGetProviderInformation, ex);
			}
			return providerManifestToken;
		}
	}
}
