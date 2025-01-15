using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;

namespace Azure.Identity
{
	// Token: 0x02000063 RID: 99
	internal class ImdsManagedIdentitySource : ManagedIdentitySource
	{
		// Token: 0x0600037E RID: 894 RVA: 0x0000AA48 File Offset: 0x00008C48
		internal ImdsManagedIdentitySource(ManagedIdentityClientOptions options)
			: base(options.Pipeline)
		{
			this._clientId = options.ClientId;
			ResourceIdentifier resourceIdentifier = options.ResourceIdentifier;
			this._resourceId = ((resourceIdentifier != null) ? resourceIdentifier.ToString() : null);
			this._imdsNetworkTimeout = options.InitialImdsConnectionTimeout;
			TokenCredentialOptions options2 = options.Options;
			this._isChainedCredential = options2 != null && options2.IsChainedCredential;
			this._imdsEndpoint = ImdsManagedIdentitySource.GetImdsUri();
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000AABB File Offset: 0x00008CBB
		internal static Uri GetImdsUri()
		{
			if (!string.IsNullOrEmpty(EnvironmentVariables.PodIdentityEndpoint))
			{
				return new UriBuilder(EnvironmentVariables.PodIdentityEndpoint)
				{
					Path = "/metadata/identity/oauth2/token"
				}.Uri;
			}
			return ImdsManagedIdentitySource.s_imdsEndpoint;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000AAEC File Offset: 0x00008CEC
		protected override Request CreateRequest(string[] scopes)
		{
			string text = ScopeUtilities.ScopesToResource(scopes);
			Request request = base.Pipeline.HttpPipeline.CreateRequest();
			request.Method = RequestMethod.Get;
			if (!this._isFirstRequest || !this._isChainedCredential)
			{
				ImdsManagedIdentitySource.SetNonProbeRequest(request);
			}
			request.Uri.Reset(this._imdsEndpoint);
			request.Uri.AppendQuery("api-version", "2018-02-01");
			request.Uri.AppendQuery("resource", text);
			if (!string.IsNullOrEmpty(this._clientId))
			{
				request.Uri.AppendQuery("client_id", this._clientId);
			}
			if (!string.IsNullOrEmpty(this._resourceId))
			{
				request.Uri.AppendQuery("mi_res_id", this._resourceId);
			}
			return request;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000ABB0 File Offset: 0x00008DB0
		protected override HttpMessage CreateHttpMessage(Request request)
		{
			HttpMessage httpMessage = base.CreateHttpMessage(request);
			if (this._isFirstRequest && this._isChainedCredential)
			{
				httpMessage.NetworkTimeout = this._imdsNetworkTimeout;
			}
			return httpMessage;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000ABE4 File Offset: 0x00008DE4
		public override async ValueTask<AccessToken> AuthenticateAsync(bool async, TokenRequestContext context, CancellationToken cancellationToken)
		{
			int num = 0;
			try
			{
				return await base.AuthenticateAsync(async, context, cancellationToken).ConfigureAwait(false);
			}
			catch (RequestFailedException ex) when (ex.Status == 0)
			{
				if (ex.InnerException is TaskCanceledException)
				{
					throw;
				}
				throw new CredentialUnavailableException("ManagedIdentityCredential authentication unavailable. No response received from the managed identity endpoint.", ex);
			}
			catch (TaskCanceledException ex2)
			{
				throw new CredentialUnavailableException("ManagedIdentityCredential authentication unavailable. No response received from the managed identity endpoint.", ex2);
			}
			catch (AggregateException ex3)
			{
				throw new CredentialUnavailableException("ManagedIdentityCredential authentication unavailable. Multiple attempts failed to obtain a token from the managed identity endpoint.", ex3);
			}
			catch (CredentialUnavailableException)
			{
				throw;
			}
			catch (ImdsManagedIdentitySource.ProbeRequestResponseException)
			{
				num = 1;
			}
			AccessToken accessToken;
			if (num == 1)
			{
				accessToken = await base.AuthenticateAsync(async, context, cancellationToken).ConfigureAwait(false);
			}
			return accessToken;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000AC40 File Offset: 0x00008E40
		protected override async ValueTask<AccessToken> HandleResponseAsync(bool async, TokenRequestContext context, HttpMessage message, CancellationToken cancellationToken)
		{
			Response response = message.Response;
			this._imdsNetworkTimeout = null;
			this._isFirstRequest = false;
			int status = response.Status;
			string text;
			if (status != 400)
			{
				if (status != 502)
				{
					if (status != 504)
					{
						text = null;
					}
					else
					{
						text = "ManagedIdentityCredential authentication unavailable. The request failed due to a gateway error.";
					}
				}
				else
				{
					text = "ManagedIdentityCredential authentication unavailable. The request failed due to a gateway error.";
				}
			}
			else
			{
				if (ImdsManagedIdentitySource.IsProbRequest(message))
				{
					throw new ImdsManagedIdentitySource.ProbeRequestResponseException();
				}
				text = "ManagedIdentityCredential authentication unavailable. The requested identity has not been assigned to this resource.";
			}
			string text2 = text;
			if (text2 != null)
			{
				string content = new RequestFailedException(response, null, new ImdsManagedIdentitySource.ImdsRequestFailedDetailsParser(text2)).Message;
				string text3 = await ManagedIdentitySource.GetMessageFromResponse(response, async, cancellationToken).ConfigureAwait(false);
				if (text3 != null)
				{
					content = content + Environment.NewLine + text3;
				}
				throw new CredentialUnavailableException(content);
			}
			return await base.HandleResponseAsync(async, context, message, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000ACA4 File Offset: 0x00008EA4
		public static bool IsProbRequest(HttpMessage message)
		{
			string text;
			return message.Request.Uri.Host == ImdsManagedIdentitySource.s_imdsEndpoint.Host && message.Request.Uri.Path == ImdsManagedIdentitySource.s_imdsEndpoint.AbsolutePath && !message.Request.Headers.TryGetValue("Metadata", out text);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000AD14 File Offset: 0x00008F14
		public static void SetNonProbeRequest(Request request)
		{
			request.Headers.Add("Metadata", "true");
		}

		// Token: 0x04000211 RID: 529
		internal static readonly Uri s_imdsEndpoint = new Uri("http://169.254.169.254/metadata/identity/oauth2/token");

		// Token: 0x04000212 RID: 530
		internal const string imddsTokenPath = "/metadata/identity/oauth2/token";

		// Token: 0x04000213 RID: 531
		internal const string metadataHeaderName = "Metadata";

		// Token: 0x04000214 RID: 532
		private const string ImdsApiVersion = "2018-02-01";

		// Token: 0x04000215 RID: 533
		internal const string IdentityUnavailableError = "ManagedIdentityCredential authentication unavailable. The requested identity has not been assigned to this resource.";

		// Token: 0x04000216 RID: 534
		internal const string NoResponseError = "ManagedIdentityCredential authentication unavailable. No response received from the managed identity endpoint.";

		// Token: 0x04000217 RID: 535
		internal const string TimeoutError = "ManagedIdentityCredential authentication unavailable. The request to the managed identity endpoint timed out.";

		// Token: 0x04000218 RID: 536
		internal const string GatewayError = "ManagedIdentityCredential authentication unavailable. The request failed due to a gateway error.";

		// Token: 0x04000219 RID: 537
		internal const string AggregateError = "ManagedIdentityCredential authentication unavailable. Multiple attempts failed to obtain a token from the managed identity endpoint.";

		// Token: 0x0400021A RID: 538
		private readonly string _clientId;

		// Token: 0x0400021B RID: 539
		private readonly string _resourceId;

		// Token: 0x0400021C RID: 540
		private readonly Uri _imdsEndpoint;

		// Token: 0x0400021D RID: 541
		private bool _isFirstRequest = true;

		// Token: 0x0400021E RID: 542
		private TimeSpan? _imdsNetworkTimeout;

		// Token: 0x0400021F RID: 543
		private bool _isChainedCredential;

		// Token: 0x020000ED RID: 237
		private class ImdsRequestFailedDetailsParser : RequestFailedDetailsParser
		{
			// Token: 0x06000590 RID: 1424 RVA: 0x00016982 File Offset: 0x00014B82
			public ImdsRequestFailedDetailsParser(string baseMessage)
			{
				this._baseMessage = baseMessage;
			}

			// Token: 0x06000591 RID: 1425 RVA: 0x00016991 File Offset: 0x00014B91
			public override bool TryParse(Response response, out ResponseError error, out IDictionary<string, string> data)
			{
				error = new ResponseError(null, this._baseMessage);
				data = null;
				return true;
			}

			// Token: 0x040004BE RID: 1214
			private readonly string _baseMessage;
		}

		// Token: 0x020000EE RID: 238
		internal class ProbeRequestResponseException : Exception
		{
		}
	}
}
