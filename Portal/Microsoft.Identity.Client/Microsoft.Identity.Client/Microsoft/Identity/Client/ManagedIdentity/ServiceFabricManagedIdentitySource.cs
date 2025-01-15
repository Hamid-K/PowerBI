using System;
using System.Globalization;
using System.Net.Http;
using Microsoft.Identity.Client.AppConfig;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.ManagedIdentity
{
	// Token: 0x02000226 RID: 550
	internal class ServiceFabricManagedIdentitySource : AbstractManagedIdentity
	{
		// Token: 0x0600169C RID: 5788 RVA: 0x0004B0B4 File Offset: 0x000492B4
		public static AbstractManagedIdentity Create(RequestContext requestContext)
		{
			string identityEndpoint = EnvironmentVariables.IdentityEndpoint;
			requestContext.Logger.Info(() => "[Managed Identity] Service fabric managed identity is available.");
			Uri uri;
			if (!Uri.TryCreate(identityEndpoint, UriKind.Absolute, out uri))
			{
				string text = string.Format(CultureInfo.InvariantCulture, "[Managed Identity] The environment variable {0} contains an invalid Uri {1} in {2} managed identity source.", "IDENTITY_ENDPOINT", identityEndpoint, "Service Fabric");
				throw MsalServiceExceptionFactory.CreateManagedIdentityException("invalid_managed_identity_endpoint", text, null, ManagedIdentitySource.ServiceFabric, null);
			}
			requestContext.Logger.Verbose(() => "[Managed Identity] Creating Service Fabric managed identity. Endpoint URI: " + identityEndpoint);
			return new ServiceFabricManagedIdentitySource(requestContext, uri, EnvironmentVariables.IdentityHeader);
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x0004B166 File Offset: 0x00049366
		private ServiceFabricManagedIdentitySource(RequestContext requestContext, Uri endpoint, string identityHeaderValue)
			: base(requestContext, ManagedIdentitySource.ServiceFabric)
		{
			this._endpoint = endpoint;
			this._identityHeaderValue = identityHeaderValue;
			if (requestContext.ServiceBundle.Config.ManagedIdentityId.IsUserAssigned)
			{
				requestContext.Logger.Warning("[Managed Identity] Service Fabric user assigned managed identity ClientId or ResourceId is not configurable at runtime.");
			}
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x0004B1A8 File Offset: 0x000493A8
		protected override ManagedIdentityRequest CreateRequest(string resource)
		{
			ManagedIdentityRequest managedIdentityRequest = new ManagedIdentityRequest(HttpMethod.Get, this._endpoint);
			managedIdentityRequest.Headers["secret"] = this._identityHeaderValue;
			managedIdentityRequest.QueryParameters["api-version"] = "2019-07-01-preview";
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

		// Token: 0x040009A6 RID: 2470
		private const string ServiceFabricMsiApiVersion = "2019-07-01-preview";

		// Token: 0x040009A7 RID: 2471
		private readonly Uri _endpoint;

		// Token: 0x040009A8 RID: 2472
		private readonly string _identityHeaderValue;
	}
}
