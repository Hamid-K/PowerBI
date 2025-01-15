using System;
using System.Configuration;
using System.Data.Entity.Internal.ConfigFile;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Infrastructure.Design
{
	// Token: 0x0200029B RID: 667
	public class AppConfigReader
	{
		// Token: 0x06002157 RID: 8535 RVA: 0x0005D6EA File Offset: 0x0005B8EA
		public AppConfigReader(Configuration configuration)
		{
			Check.NotNull<Configuration>(configuration, "configuration");
			this._configuration = configuration;
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x0005D708 File Offset: 0x0005B908
		public string GetProviderServices(string invariantName)
		{
			EntityFrameworkSection entityFrameworkSection = (EntityFrameworkSection)this._configuration.GetSection("entityFramework");
			if (entityFrameworkSection == null)
			{
				return null;
			}
			return (from ProviderElement p in entityFrameworkSection.Providers
				where p.InvariantName == invariantName
				select p.ProviderTypeName).FirstOrDefault<string>();
		}

		// Token: 0x04000B99 RID: 2969
		private readonly Configuration _configuration;
	}
}
