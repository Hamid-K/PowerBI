using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Http;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Json.Linq;

namespace Microsoft.Identity.Client.ManagedIdentity
{
	// Token: 0x0200021B RID: 539
	internal abstract class AbstractManagedIdentity
	{
		// Token: 0x06001655 RID: 5717 RVA: 0x00049E74 File Offset: 0x00048074
		protected AbstractManagedIdentity(RequestContext requestContext, ManagedIdentitySource sourceType)
		{
			this._requestContext = requestContext;
			this._sourceType = sourceType;
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x00049E8C File Offset: 0x0004808C
		public virtual async Task<ManagedIdentityResponse> AuthenticateAsync(AcquireTokenForManagedIdentityParameters parameters, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				this._requestContext.Logger.Error("[Managed Identity] Authentication unavailable. The request to the managed identity endpoint timed out.");
				cancellationToken.ThrowIfCancellationRequested();
			}
			string resource = parameters.Resource;
			ManagedIdentityRequest managedIdentityRequest = this.CreateRequest(resource);
			ManagedIdentityResponse managedIdentityResponse;
			try
			{
				HttpResponse httpResponse;
				if (managedIdentityRequest.Method == HttpMethod.Get)
				{
					httpResponse = await this._requestContext.ServiceBundle.HttpManager.SendGetForceResponseAsync(managedIdentityRequest.ComputeUri(), managedIdentityRequest.Headers, this._requestContext.Logger, true, cancellationToken).ConfigureAwait(false);
				}
				else
				{
					httpResponse = await this._requestContext.ServiceBundle.HttpManager.SendPostForceResponseAsync(managedIdentityRequest.ComputeUri(), managedIdentityRequest.Headers, managedIdentityRequest.BodyParameters, this._requestContext.Logger, cancellationToken).ConfigureAwait(false);
				}
				HttpResponse httpResponse2 = httpResponse;
				managedIdentityResponse = await this.HandleResponseAsync(parameters, httpResponse2, cancellationToken).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				this.HandleException(ex, ManagedIdentitySource.None, null);
				throw;
			}
			return managedIdentityResponse;
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x00049EE0 File Offset: 0x000480E0
		protected virtual Task<ManagedIdentityResponse> HandleResponseAsync(AcquireTokenForManagedIdentityParameters parameters, HttpResponse response, CancellationToken cancellationToken)
		{
			if (response.StatusCode == HttpStatusCode.OK)
			{
				this._requestContext.Logger.Info("[Managed Identity] Successful response received.");
				return Task.FromResult<ManagedIdentityResponse>(this.GetSuccessfulResponse(response));
			}
			string messageFromErrorResponse = this.GetMessageFromErrorResponse(response);
			this._requestContext.Logger.Error(string.Format("[Managed Identity] request failed, HttpStatusCode: {0} Error message: {1}", response.StatusCode, messageFromErrorResponse));
			throw MsalServiceExceptionFactory.CreateManagedIdentityException("managed_identity_request_failed", messageFromErrorResponse, null, this._sourceType, new int?((int)response.StatusCode));
		}

		// Token: 0x06001658 RID: 5720
		protected abstract ManagedIdentityRequest CreateRequest(string resource);

		// Token: 0x06001659 RID: 5721 RVA: 0x00049F68 File Offset: 0x00048168
		protected ManagedIdentityResponse GetSuccessfulResponse(HttpResponse response)
		{
			ManagedIdentityResponse managedIdentityResponse = JsonHelper.DeserializeFromJson<ManagedIdentityResponse>(response.Body);
			if (managedIdentityResponse == null || managedIdentityResponse.AccessToken.IsNullOrEmpty<char>() || managedIdentityResponse.ExpiresOn.IsNullOrEmpty<char>())
			{
				this._requestContext.Logger.Error("[Managed Identity] Response is either null or insufficient for authentication.");
				throw MsalServiceExceptionFactory.CreateManagedIdentityException("managed_identity_request_failed", "[Managed Identity] Invalid response, the authentication response received did not contain the expected fields.", null, this._sourceType, null);
			}
			return managedIdentityResponse;
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x00049FD4 File Offset: 0x000481D4
		internal string GetMessageFromErrorResponse(HttpResponse response)
		{
			if (string.IsNullOrEmpty((response != null) ? response.Body : null))
			{
				return "[Managed Identity] Authentication unavailable. No response received from the managed identity endpoint.";
			}
			string text;
			try
			{
				ManagedIdentityErrorResponse managedIdentityErrorResponse = JsonHelper.DeserializeFromJson<ManagedIdentityErrorResponse>((response != null) ? response.Body : null);
				text = this.ExtractErrorMessageFromManagedIdentityErrorResponse(managedIdentityErrorResponse);
			}
			catch
			{
				text = this.TryGetMessageFromNestedErrorResponse(response.Body);
			}
			return text;
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x0004A038 File Offset: 0x00048238
		private string ExtractErrorMessageFromManagedIdentityErrorResponse(ManagedIdentityErrorResponse managedIdentityErrorResponse)
		{
			StringBuilder stringBuilder = new StringBuilder("[Managed Identity] ");
			if (!string.IsNullOrEmpty(managedIdentityErrorResponse.Error))
			{
				stringBuilder.Append("Error Code: " + managedIdentityErrorResponse.Error + " ");
			}
			if (!string.IsNullOrEmpty(managedIdentityErrorResponse.Message))
			{
				stringBuilder.Append("Error Message: " + managedIdentityErrorResponse.Message + " ");
			}
			if (!string.IsNullOrEmpty(managedIdentityErrorResponse.ErrorDescription))
			{
				stringBuilder.Append("Error Description: " + managedIdentityErrorResponse.ErrorDescription + " ");
			}
			if (!string.IsNullOrEmpty(managedIdentityErrorResponse.CorrelationId))
			{
				stringBuilder.Append("Managed Identity Correlation ID: " + managedIdentityErrorResponse.CorrelationId + " Use this Correlation ID for further investigation.");
			}
			if (stringBuilder.Length == "[Managed Identity] ".Length)
			{
				return "[Managed Identity] The error response was either empty or could not be parsed..";
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x0004A114 File Offset: 0x00048314
		private string TryGetMessageFromNestedErrorResponse(string response)
		{
			try
			{
				JToken jtoken;
				JsonHelper.TryGetValue(JsonHelper.ParseIntoJsonObject(response), "error", out jtoken);
				StringBuilder stringBuilder = new StringBuilder("[Managed Identity] ");
				JToken jtoken2;
				if (JsonHelper.TryGetValue(JsonHelper.ToJsonObject(jtoken), "code", out jtoken2))
				{
					stringBuilder.Append(string.Format("Error Code: {0} ", jtoken2));
				}
				JToken jtoken3;
				if (JsonHelper.TryGetValue(JsonHelper.ToJsonObject(jtoken), "message", out jtoken3))
				{
					stringBuilder.Append(string.Format("Error Message: {0}", jtoken3));
				}
				if (jtoken3 != null || jtoken2 != null)
				{
					return stringBuilder.ToString();
				}
			}
			catch
			{
			}
			this._requestContext.Logger.Error("[Managed Identity] The error response was either empty or could not be parsed.. Error response received from the server: " + response + ".");
			return "[Managed Identity] The error response was either empty or could not be parsed.. Error response received from the server: " + response + ".";
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x0004A1E4 File Offset: 0x000483E4
		private void HandleException(Exception ex, ManagedIdentitySource managedIdentitySource = ManagedIdentitySource.None, string additionalInfo = null)
		{
			ManagedIdentitySource managedIdentitySource2 = ((managedIdentitySource != ManagedIdentitySource.None) ? managedIdentitySource : this._sourceType);
			HttpRequestException ex2 = ex as HttpRequestException;
			if (ex2 != null)
			{
				AbstractManagedIdentity.CreateAndThrowException("managed_identity_unreachable_network", ex2.Message, ex2, managedIdentitySource2);
				return;
			}
			if (ex is TaskCanceledException)
			{
				this._requestContext.Logger.Error("[Managed Identity] Authentication unavailable. The request to the managed identity endpoint timed out.");
				return;
			}
			FormatException ex3 = ex as FormatException;
			if (ex3 != null)
			{
				string text = additionalInfo ?? ex3.Message;
				this._requestContext.Logger.Error("[Managed Identity] Format Exception: " + text);
				AbstractManagedIdentity.CreateAndThrowException("invalid_managed_identity_endpoint", text, ex3, managedIdentitySource2);
				return;
			}
			if (!(ex is MsalServiceException))
			{
				this._requestContext.Logger.Error("[Managed Identity] Exception: " + ex.Message);
				AbstractManagedIdentity.CreateAndThrowException("managed_identity_request_failed", ex.Message, ex, managedIdentitySource2);
			}
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x0004A2B4 File Offset: 0x000484B4
		private static void CreateAndThrowException(string errorCode, string errorMessage, Exception innerException, ManagedIdentitySource source)
		{
			throw MsalServiceExceptionFactory.CreateManagedIdentityException(errorCode, errorMessage, innerException, source, null);
		}

		// Token: 0x0400097A RID: 2426
		protected readonly RequestContext _requestContext;

		// Token: 0x0400097B RID: 2427
		internal const string TimeoutError = "[Managed Identity] Authentication unavailable. The request to the managed identity endpoint timed out.";

		// Token: 0x0400097C RID: 2428
		internal readonly ManagedIdentitySource _sourceType;

		// Token: 0x0400097D RID: 2429
		private const string ManagedIdentityPrefix = "[Managed Identity] ";
	}
}
