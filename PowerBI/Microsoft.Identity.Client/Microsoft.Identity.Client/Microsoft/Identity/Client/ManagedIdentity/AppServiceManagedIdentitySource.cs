using System;
using System.Globalization;
using System.Net.Http;
using Microsoft.Identity.Client.AppConfig;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.ManagedIdentity
{
	// Token: 0x0200021C RID: 540
	internal class AppServiceManagedIdentitySource : AbstractManagedIdentity
	{
		// Token: 0x0600165F RID: 5727 RVA: 0x0004A2D4 File Offset: 0x000484D4
		public static AbstractManagedIdentity Create(RequestContext requestContext)
		{
			requestContext.Logger.Info(() => "[Managed Identity] App service managed identity is available.");
			Uri uri;
			if (!AppServiceManagedIdentitySource.TryValidateEnvVars(EnvironmentVariables.IdentityEndpoint, requestContext.Logger, out uri))
			{
				return null;
			}
			return new AppServiceManagedIdentitySource(requestContext, uri, EnvironmentVariables.IdentityHeader);
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x0004A32D File Offset: 0x0004852D
		private AppServiceManagedIdentitySource(RequestContext requestContext, Uri endpoint, string secret)
			: base(requestContext, ManagedIdentitySource.AppService)
		{
			this._endpoint = endpoint;
			this._secret = secret;
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x0004A348 File Offset: 0x00048548
		private static bool TryValidateEnvVars(string msiEndpoint, ILoggerAdapter logger, out Uri endpointUri)
		{
			endpointUri = null;
			try
			{
				endpointUri = new Uri(msiEndpoint);
			}
			catch (FormatException ex)
			{
				string text = string.Format(CultureInfo.InvariantCulture, "[Managed Identity] The environment variable {0} contains an invalid Uri {1} in {2} managed identity source.", "IDENTITY_ENDPOINT", msiEndpoint, "App Service");
				throw MsalServiceExceptionFactory.CreateManagedIdentityException("invalid_managed_identity_endpoint", text, ex, ManagedIdentitySource.AppService, null);
			}
			logger.Info(string.Format("[Managed Identity] Environment variables validation passed for app service managed identity. Endpoint URI: {0}. Creating App Service managed identity.", endpointUri));
			return true;
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x0004A3B8 File Offset: 0x000485B8
		protected override ManagedIdentityRequest CreateRequest(string resource)
		{
			ManagedIdentityRequest managedIdentityRequest = new ManagedIdentityRequest(HttpMethod.Get, this._endpoint);
			managedIdentityRequest.Headers.Add("X-IDENTITY-HEADER", this._secret);
			managedIdentityRequest.QueryParameters["api-version"] = "2019-08-01";
			managedIdentityRequest.QueryParameters["resource"] = resource;
			switch (this._requestContext.ServiceBundle.Config.ManagedIdentityId.IdType)
			{
			case ManagedIdentityIdType.ClientId:
				this._requestContext.Logger.Info("[Managed Identity] Adding user assigned client id to the request.");
				managedIdentityRequest.QueryParameters["client_id"] = this._requestContext.ServiceBundle.Config.ManagedIdentityId.UserAssignedId;
				break;
			case ManagedIdentityIdType.ResourceId:
				this._requestContext.Logger.Info("[Managed Identity] Adding user assigned resource id to the request.");
				managedIdentityRequest.QueryParameters["mi_res_id"] = this._requestContext.ServiceBundle.Config.ManagedIdentityId.UserAssignedId;
				break;
			case ManagedIdentityIdType.ObjectId:
				this._requestContext.Logger.Info("[Managed Identity] Adding user assigned object id to the request.");
				managedIdentityRequest.QueryParameters["object_id"] = this._requestContext.ServiceBundle.Config.ManagedIdentityId.UserAssignedId;
				break;
			}
			return managedIdentityRequest;
		}

		// Token: 0x0400097E RID: 2430
		private const string AppServiceMsiApiVersion = "2019-08-01";

		// Token: 0x0400097F RID: 2431
		private const string SecretHeaderName = "X-IDENTITY-HEADER";

		// Token: 0x04000980 RID: 2432
		private readonly Uri _endpoint;

		// Token: 0x04000981 RID: 2433
		private readonly string _secret;
	}
}
