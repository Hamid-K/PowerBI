using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Http;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.PlatformsCommon.Shared;

namespace Microsoft.Identity.Client.ManagedIdentity
{
	// Token: 0x0200021D RID: 541
	internal class AzureArcManagedIdentitySource : AbstractManagedIdentity
	{
		// Token: 0x06001663 RID: 5731 RVA: 0x0004A50C File Offset: 0x0004870C
		public static AbstractManagedIdentity Create(RequestContext requestContext)
		{
			string identityEndpoint = EnvironmentVariables.IdentityEndpoint;
			requestContext.Logger.Info(() => "[Managed Identity] Azure Arc managed identity is available.");
			Uri endpointUri;
			if (!Uri.TryCreate(identityEndpoint, UriKind.Absolute, out endpointUri))
			{
				string text = string.Format(CultureInfo.InvariantCulture, "[Managed Identity] The environment variable {0} contains an invalid Uri {1} in {2} managed identity source.", "IDENTITY_ENDPOINT", identityEndpoint, "Azure Arc");
				throw MsalServiceExceptionFactory.CreateManagedIdentityException("invalid_managed_identity_endpoint", text, null, ManagedIdentitySource.AzureArc, null);
			}
			requestContext.Logger.Verbose(delegate
			{
				string text2 = "[Managed Identity] Creating Azure Arc managed identity. Endpoint URI: ";
				Uri endpointUri2 = endpointUri;
				return text2 + ((endpointUri2 != null) ? endpointUri2.ToString() : null);
			});
			return new AzureArcManagedIdentitySource(endpointUri, requestContext);
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x0004A5B4 File Offset: 0x000487B4
		private AzureArcManagedIdentitySource(Uri endpoint, RequestContext requestContext)
			: base(requestContext, ManagedIdentitySource.AzureArc)
		{
			this._endpoint = endpoint;
			if (requestContext.ServiceBundle.Config.ManagedIdentityId.IsUserAssigned)
			{
				string text = string.Format(CultureInfo.InvariantCulture, "[Managed Identity] User assigned identity is not supported by the {0} Managed Identity. To authenticate with the system assigned identity omit the client id in ManagedIdentityApplicationBuilder.Create().", "Azure Arc");
				throw MsalServiceExceptionFactory.CreateManagedIdentityException("user_assigned_managed_identity_not_supported", text, null, ManagedIdentitySource.AzureArc, null);
			}
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x0004A614 File Offset: 0x00048814
		protected override ManagedIdentityRequest CreateRequest(string resource)
		{
			ManagedIdentityRequest managedIdentityRequest = new ManagedIdentityRequest(HttpMethod.Get, this._endpoint);
			managedIdentityRequest.Headers.Add("Metadata", "true");
			managedIdentityRequest.QueryParameters["api-version"] = "2019-11-01";
			managedIdentityRequest.QueryParameters["resource"] = resource;
			return managedIdentityRequest;
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x0004A66C File Offset: 0x0004886C
		protected override async Task<ManagedIdentityResponse> HandleResponseAsync(AcquireTokenForManagedIdentityParameters parameters, HttpResponse response, CancellationToken cancellationToken)
		{
			this._requestContext.Logger.Verbose(() => string.Format("[Managed Identity] Response received. Status code: {0}", response.StatusCode));
			ManagedIdentityResponse managedIdentityResponse;
			if (response.StatusCode == HttpStatusCode.Unauthorized)
			{
				string text;
				if (!response.HeadersAsDictionary.TryGetValue("WWW-Authenticate", out text))
				{
					this._requestContext.Logger.Error("[Managed Identity] WWW-Authenticate header is expected but not found.");
					throw MsalServiceExceptionFactory.CreateManagedIdentityException("managed_identity_request_failed", "[Managed Identity] Did not receive expected WWW-Authenticate header in the response from Azure Arc Managed Identity Endpoint.", null, ManagedIdentitySource.AzureArc, null);
				}
				string[] array = text.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
				this.ValidateSplitChallenge(array);
				string text2 = "Basic " + File.ReadAllText(array[1]);
				ManagedIdentityRequest managedIdentityRequest = this.CreateRequest(parameters.Resource);
				this._requestContext.Logger.Verbose(() => "[Managed Identity] Adding authorization header to the request.");
				managedIdentityRequest.Headers.Add("Authorization", text2);
				HttpResponse httpResponse = await this._requestContext.ServiceBundle.HttpManager.SendGetAsync(managedIdentityRequest.ComputeUri(), managedIdentityRequest.Headers, this._requestContext.Logger, true, cancellationToken).ConfigureAwait(false);
				response = httpResponse;
				managedIdentityResponse = await base.HandleResponseAsync(parameters, response, cancellationToken).ConfigureAwait(false);
			}
			else
			{
				managedIdentityResponse = await base.HandleResponseAsync(parameters, response, cancellationToken).ConfigureAwait(false);
			}
			return managedIdentityResponse;
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x0004A6C8 File Offset: 0x000488C8
		private void ValidateSplitChallenge(string[] splitChallenge)
		{
			if (splitChallenge.Length != 2)
			{
				throw this.CreateManagedIdentityException("managed_identity_request_failed", "[Managed Identity] The WWW-Authenticate header in the response from Azure Arc Managed Identity Endpoint did not match the expected format.");
			}
			this._requestContext.Logger.Verbose(() => "[Managed Identity] Challenge is valid. FilePath: " + splitChallenge[1]);
			if (!this.IsValidPath(splitChallenge[1]))
			{
				throw this.CreateManagedIdentityException("managed_identity_request_failed", "[Managed Identity] The file on the file path in the WWW-Authenticate header is not secure.");
			}
			this._requestContext.Logger.Verbose(() => "[Managed Identity] File path is valid. Path: " + splitChallenge[1]);
			long length = new FileInfo(splitChallenge[1]).Length;
			if (!File.Exists(splitChallenge[1]) || length > 4096L)
			{
				this._requestContext.Logger.Error(string.Format("[Managed Identity] File does not exist or is greater than 4096 bytes. File exists: {0}. Length of file: {1}", File.Exists(splitChallenge[1]), length));
				throw this.CreateManagedIdentityException("managed_identity_request_failed", "[Managed Identity] The file on the file path in the WWW-Authenticate header is not secure.");
			}
			this._requestContext.Logger.Verbose(() => "[Managed Identity] File exists and is less than 4096 bytes.");
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x0004A7F4 File Offset: 0x000489F4
		private MsalException CreateManagedIdentityException(string errorCode, string errorMessage)
		{
			return MsalServiceExceptionFactory.CreateManagedIdentityException(errorCode, errorMessage, null, ManagedIdentitySource.AzureArc, null);
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x0004A814 File Offset: 0x00048A14
		private bool IsValidPath(string path)
		{
			string text;
			if (DesktopOsHelper.IsWindows())
			{
				text = Environment.ExpandEnvironmentVariables("%ProgramData%\\AzureConnectedMachineAgent\\Tokens\\") + Path.GetFileNameWithoutExtension(path) + ".key";
			}
			else
			{
				if (!DesktopOsHelper.IsLinux())
				{
					throw this.CreateManagedIdentityException("managed_identity_request_failed", "[Managed Identity] The platform is not supported by Azure Arc. Azure Arc only supports Windows and Linux.");
				}
				text = "/var/opt/azcmagent/tokens/" + Path.GetFileNameWithoutExtension(path) + ".key";
			}
			return path.Equals(text);
		}

		// Token: 0x04000982 RID: 2434
		private const string ArcApiVersion = "2019-11-01";

		// Token: 0x04000983 RID: 2435
		private const string AzureArc = "Azure Arc";

		// Token: 0x04000984 RID: 2436
		private readonly Uri _endpoint;
	}
}
