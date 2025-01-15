using System;
using Microsoft.Identity.Client.ApiConfig.Executors;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.ManagedIdentity;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200015C RID: 348
	public sealed class ManagedIdentityApplication : ApplicationBase, IManagedIdentityApplication, IApplicationBase
	{
		// Token: 0x06001123 RID: 4387 RVA: 0x0003B82C File Offset: 0x00039A2C
		internal ManagedIdentityApplication(ApplicationConfiguration configuration)
			: base(configuration)
		{
			ApplicationBase.GuardMobileFrameworks();
			this.AppTokenCacheInternal = configuration.AppTokenCacheInternalForTest ?? new TokenCache(base.ServiceBundle, true, null);
			base.ServiceBundle.ApplicationLogger.Verbose(() => string.Format("ManagedIdentityApplication {0} created", configuration.GetHashCode()));
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x0003B895 File Offset: 0x00039A95
		internal ITokenCacheInternal AppTokenCacheInternal { get; }

		// Token: 0x06001125 RID: 4389 RVA: 0x0003B89D File Offset: 0x00039A9D
		public AcquireTokenForManagedIdentityParameterBuilder AcquireTokenForManagedIdentity(string resource)
		{
			if (string.IsNullOrEmpty(resource))
			{
				throw new ArgumentNullException("resource");
			}
			return AcquireTokenForManagedIdentityParameterBuilder.Create(ClientExecutorFactory.CreateManagedIdentityExecutor(this), resource);
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x0003B8BE File Offset: 0x00039ABE
		public static ManagedIdentitySource GetManagedIdentitySource()
		{
			return ManagedIdentityClient.s_managedIdentitySourceDetected.Value;
		}
	}
}
