using System;
using System.Globalization;
using System.Net.Http;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.ManagedIdentity
{
	// Token: 0x0200021E RID: 542
	internal class CloudShellManagedIdentitySource : AbstractManagedIdentity
	{
		// Token: 0x0600166B RID: 5739 RVA: 0x0004A888 File Offset: 0x00048A88
		public static AbstractManagedIdentity Create(RequestContext requestContext)
		{
			string msiEndpoint = EnvironmentVariables.MsiEndpoint;
			requestContext.Logger.Info(() => "[Managed Identity] Cloud shell managed identity is available.");
			Uri uri;
			try
			{
				uri = new Uri(msiEndpoint);
			}
			catch (FormatException ex)
			{
				requestContext.Logger.Error("[Managed Identity] Invalid endpoint found for the environment variable MSI_ENDPOINT: " + msiEndpoint);
				string text = string.Format(CultureInfo.InvariantCulture, "[Managed Identity] The environment variable {0} contains an invalid Uri {1} in {2} managed identity source.", "MSI_ENDPOINT", msiEndpoint, "Cloud Shell");
				throw MsalServiceExceptionFactory.CreateManagedIdentityException("invalid_managed_identity_endpoint", text, ex, ManagedIdentitySource.CloudShell, null);
			}
			requestContext.Logger.Verbose(() => "[Managed Identity] Creating cloud shell managed identity. Endpoint URI: " + msiEndpoint);
			return new CloudShellManagedIdentitySource(uri, requestContext);
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x0004A960 File Offset: 0x00048B60
		private CloudShellManagedIdentitySource(Uri endpoint, RequestContext requestContext)
			: base(requestContext, ManagedIdentitySource.CloudShell)
		{
			this._endpoint = endpoint;
			if (requestContext.ServiceBundle.Config.ManagedIdentityId.IsUserAssigned)
			{
				string text = string.Format(CultureInfo.InvariantCulture, "[Managed Identity] User assigned identity is not supported by the {0} Managed Identity. To authenticate with the system assigned identity omit the client id in ManagedIdentityApplicationBuilder.Create().", "Cloud Shell");
				throw MsalServiceExceptionFactory.CreateManagedIdentityException("user_assigned_managed_identity_not_supported", text, null, ManagedIdentitySource.CloudShell, null);
			}
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x0004A9C0 File Offset: 0x00048BC0
		protected override ManagedIdentityRequest CreateRequest(string resource)
		{
			return new ManagedIdentityRequest(HttpMethod.Post, this._endpoint)
			{
				Headers = 
				{
					{ "ContentType", "application/x-www-form-urlencoded" },
					{ "Metadata", "true" }
				},
				BodyParameters = { { "resource", resource } }
			};
		}

		// Token: 0x04000985 RID: 2437
		private readonly Uri _endpoint;

		// Token: 0x04000986 RID: 2438
		private const string CloudShell = "Cloud Shell";
	}
}
