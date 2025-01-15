using System;

namespace Microsoft.Identity.Client.ApiConfig.Executors
{
	// Token: 0x020002E1 RID: 737
	internal static class ClientExecutorFactory
	{
		// Token: 0x06001B4D RID: 6989 RVA: 0x000576B3 File Offset: 0x000558B3
		public static IPublicClientApplicationExecutor CreatePublicClientExecutor(PublicClientApplication publicClientApplication)
		{
			return new PublicClientExecutor(publicClientApplication.ServiceBundle, publicClientApplication);
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x000576C1 File Offset: 0x000558C1
		public static IConfidentialClientApplicationExecutor CreateConfidentialClientExecutor(ConfidentialClientApplication confidentialClientApplication)
		{
			ApplicationBase.GuardMobileFrameworks();
			return new ConfidentialClientExecutor(confidentialClientApplication.ServiceBundle, confidentialClientApplication);
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x000576D4 File Offset: 0x000558D4
		public static IManagedIdentityApplicationExecutor CreateManagedIdentityExecutor(ManagedIdentityApplication managedIdentityApplication)
		{
			ApplicationBase.GuardMobileFrameworks();
			return new ManagedIdentityExecutor(managedIdentityApplication.ServiceBundle, managedIdentityApplication);
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x000576E7 File Offset: 0x000558E7
		public static IClientApplicationBaseExecutor CreateClientApplicationBaseExecutor(ClientApplicationBase clientApplicationBase)
		{
			return new ClientApplicationBaseExecutor(clientApplicationBase.ServiceBundle, clientApplicationBase);
		}
	}
}
