using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.AppConfig;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Http;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.ManagedIdentity
{
	// Token: 0x02000220 RID: 544
	internal class ImdsManagedIdentitySource : AbstractManagedIdentity
	{
		// Token: 0x06001675 RID: 5749 RVA: 0x0004AA68 File Offset: 0x00048C68
		internal ImdsManagedIdentitySource(RequestContext requestContext)
			: base(requestContext, ManagedIdentitySource.Imds)
		{
			requestContext.Logger.Info(() => "[Managed Identity] Defaulting to IMDS endpoint for managed identity.");
			if (!string.IsNullOrEmpty(EnvironmentVariables.PodIdentityEndpoint))
			{
				requestContext.Logger.Verbose(() => "[Managed Identity] Environment variable AZURE_POD_IDENTITY_AUTHORITY_HOST for IMDS returned endpoint: " + EnvironmentVariables.PodIdentityEndpoint);
				UriBuilder uriBuilder = new UriBuilder(EnvironmentVariables.PodIdentityEndpoint)
				{
					Path = "/metadata/identity/oauth2/token"
				};
				this._imdsEndpoint = uriBuilder.Uri;
			}
			else
			{
				requestContext.Logger.Verbose(() => "[Managed Identity] Unable to find AZURE_POD_IDENTITY_AUTHORITY_HOST environment variable for IMDS, using the default endpoint.");
				this._imdsEndpoint = ImdsManagedIdentitySource.s_imdsEndpoint;
			}
			requestContext.Logger.Verbose(delegate
			{
				string text = "[Managed Identity] Creating IMDS managed identity source. Endpoint URI: ";
				Uri imdsEndpoint = this._imdsEndpoint;
				return text + ((imdsEndpoint != null) ? imdsEndpoint.ToString() : null);
			});
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x0004AB50 File Offset: 0x00048D50
		protected override ManagedIdentityRequest CreateRequest(string resource)
		{
			ManagedIdentityRequest managedIdentityRequest = new ManagedIdentityRequest(HttpMethod.Get, this._imdsEndpoint);
			managedIdentityRequest.Headers.Add("Metadata", "true");
			managedIdentityRequest.QueryParameters["api-version"] = "2018-02-01";
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

		// Token: 0x06001677 RID: 5751 RVA: 0x0004ACA4 File Offset: 0x00048EA4
		protected override async Task<ManagedIdentityResponse> HandleResponseAsync(AcquireTokenForManagedIdentityParameters parameters, HttpResponse response, CancellationToken cancellationToken)
		{
			HttpStatusCode statusCode = response.StatusCode;
			string text;
			if (statusCode != HttpStatusCode.BadRequest)
			{
				if (statusCode != HttpStatusCode.BadGateway)
				{
					if (statusCode != HttpStatusCode.GatewayTimeout)
					{
						text = null;
					}
					else
					{
						text = "[Managed Identity] Authentication unavailable. The request failed due to a gateway error.";
					}
				}
				else
				{
					text = "[Managed Identity] Authentication unavailable. The request failed due to a gateway error.";
				}
			}
			else
			{
				text = "[Managed Identity] Authentication unavailable. Either the requested identity has not been assigned to this resource, or other errors could be present. Ensure the identity is correctly assigned and check the inner exception for more details. For more information, visit https://aka.ms/msal-managed-identity.";
			}
			string text2 = text;
			if (text2 != null)
			{
				string text3 = ImdsManagedIdentitySource.CreateRequestFailedMessage(response, text2);
				string messageFromErrorResponse = base.GetMessageFromErrorResponse(response);
				text3 = text3 + Environment.NewLine + messageFromErrorResponse;
				this._requestContext.Logger.Error(string.Format("Error message: {0} Http status code: {1}", text3, response.StatusCode));
				throw MsalServiceExceptionFactory.CreateManagedIdentityException("managed_identity_request_failed", text3, null, ManagedIdentitySource.Imds, null);
			}
			return await base.HandleResponseAsync(parameters, response, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x0004AD00 File Offset: 0x00048F00
		internal static string CreateRequestFailedMessage(HttpResponse response, string message)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(message ?? "[Managed Identity] Service request failed.").Append("Status: ").Append(response.StatusCode.ToString());
			if (response.Body != null)
			{
				stringBuilder.AppendLine().AppendLine("Content:").AppendLine(response.Body);
			}
			stringBuilder.AppendLine().AppendLine("Headers:");
			foreach (KeyValuePair<string, string> keyValuePair in response.HeadersAsDictionary)
			{
				stringBuilder.AppendLine(keyValuePair.Key + ": " + keyValuePair.Value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000987 RID: 2439
		private static readonly Uri s_imdsEndpoint = new Uri("http://169.254.169.254/metadata/identity/oauth2/token");

		// Token: 0x04000988 RID: 2440
		private const string ImdsTokenPath = "/metadata/identity/oauth2/token";

		// Token: 0x04000989 RID: 2441
		private const string ImdsApiVersion = "2018-02-01";

		// Token: 0x0400098A RID: 2442
		private const string DefaultMessage = "[Managed Identity] Service request failed.";

		// Token: 0x0400098B RID: 2443
		internal const string IdentityUnavailableError = "[Managed Identity] Authentication unavailable. Either the requested identity has not been assigned to this resource, or other errors could be present. Ensure the identity is correctly assigned and check the inner exception for more details. For more information, visit https://aka.ms/msal-managed-identity.";

		// Token: 0x0400098C RID: 2444
		internal const string GatewayError = "[Managed Identity] Authentication unavailable. The request failed due to a gateway error.";

		// Token: 0x0400098D RID: 2445
		private readonly Uri _imdsEndpoint;
	}
}
