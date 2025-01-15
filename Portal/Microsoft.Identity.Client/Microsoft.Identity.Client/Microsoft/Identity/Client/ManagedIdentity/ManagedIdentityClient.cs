using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.ManagedIdentity
{
	// Token: 0x02000221 RID: 545
	internal class ManagedIdentityClient
	{
		// Token: 0x0600167C RID: 5756 RVA: 0x0004AE16 File Offset: 0x00049016
		internal static void resetCachedSource()
		{
			ManagedIdentityClient.s_managedIdentitySourceDetected = new Lazy<ManagedIdentitySource>(() => ManagedIdentityClient.GetManagedIdentitySource());
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x0004AE44 File Offset: 0x00049044
		public ManagedIdentityClient(RequestContext requestContext)
		{
			using (requestContext.Logger.LogMethodDuration(LogLevel.Verbose, ".ctor", "/_/src/client/Microsoft.Identity.Client/ManagedIdentity/ManagedIdentityClient.cs"))
			{
				this._identitySource = ManagedIdentityClient.SelectManagedIdentitySource(requestContext);
			}
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x0004AE98 File Offset: 0x00049098
		internal Task<ManagedIdentityResponse> SendTokenRequestForManagedIdentityAsync(AcquireTokenForManagedIdentityParameters parameters, CancellationToken cancellationToken)
		{
			return this._identitySource.AuthenticateAsync(parameters, cancellationToken);
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x0004AEA8 File Offset: 0x000490A8
		private static AbstractManagedIdentity SelectManagedIdentitySource(RequestContext requestContext)
		{
			AbstractManagedIdentity abstractManagedIdentity;
			switch (ManagedIdentityClient.s_managedIdentitySourceDetected.Value)
			{
			case ManagedIdentitySource.AppService:
				abstractManagedIdentity = AppServiceManagedIdentitySource.Create(requestContext);
				break;
			case ManagedIdentitySource.AzureArc:
				abstractManagedIdentity = AzureArcManagedIdentitySource.Create(requestContext);
				break;
			case ManagedIdentitySource.CloudShell:
				abstractManagedIdentity = CloudShellManagedIdentitySource.Create(requestContext);
				break;
			case ManagedIdentitySource.ServiceFabric:
				abstractManagedIdentity = ServiceFabricManagedIdentitySource.Create(requestContext);
				break;
			default:
				abstractManagedIdentity = new ImdsManagedIdentitySource(requestContext);
				break;
			}
			return abstractManagedIdentity;
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x0004AF08 File Offset: 0x00049108
		private static ManagedIdentitySource GetManagedIdentitySource()
		{
			string identityEndpoint = EnvironmentVariables.IdentityEndpoint;
			string identityHeader = EnvironmentVariables.IdentityHeader;
			string identityServerThumbprint = EnvironmentVariables.IdentityServerThumbprint;
			string identityHeader2 = EnvironmentVariables.IdentityHeader;
			string msiEndpoint = EnvironmentVariables.MsiEndpoint;
			string imdsEndpoint = EnvironmentVariables.ImdsEndpoint;
			string podIdentityEndpoint = EnvironmentVariables.PodIdentityEndpoint;
			if (!string.IsNullOrEmpty(identityEndpoint) && !string.IsNullOrEmpty(identityHeader))
			{
				if (!string.IsNullOrEmpty(identityServerThumbprint))
				{
					return ManagedIdentitySource.ServiceFabric;
				}
				return ManagedIdentitySource.AppService;
			}
			else
			{
				if (!string.IsNullOrEmpty(msiEndpoint))
				{
					return ManagedIdentitySource.CloudShell;
				}
				if (!string.IsNullOrEmpty(identityEndpoint) && !string.IsNullOrEmpty(imdsEndpoint))
				{
					return ManagedIdentitySource.AzureArc;
				}
				return ManagedIdentitySource.DefaultToImds;
			}
		}

		// Token: 0x0400098E RID: 2446
		private readonly AbstractManagedIdentity _identitySource;

		// Token: 0x0400098F RID: 2447
		internal static Lazy<ManagedIdentitySource> s_managedIdentitySourceDetected = new Lazy<ManagedIdentitySource>(() => ManagedIdentityClient.GetManagedIdentitySource());
	}
}
