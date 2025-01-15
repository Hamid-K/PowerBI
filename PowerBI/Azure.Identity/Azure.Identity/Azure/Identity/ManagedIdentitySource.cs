using System;
using System.Globalization;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;

namespace Azure.Identity
{
	// Token: 0x02000072 RID: 114
	internal abstract class ManagedIdentitySource
	{
		// Token: 0x060003D9 RID: 985 RVA: 0x0000B634 File Offset: 0x00009834
		protected ManagedIdentitySource(CredentialPipeline pipeline)
		{
			this.Pipeline = pipeline;
			this._responseClassifier = new ManagedIdentitySource.ManagedIdentityResponseClassifier();
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0000B64E File Offset: 0x0000984E
		protected internal CredentialPipeline Pipeline { get; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000B656 File Offset: 0x00009856
		protected internal string ClientId { get; }

		// Token: 0x060003DC RID: 988 RVA: 0x0000B660 File Offset: 0x00009860
		public virtual async ValueTask<AccessToken> AuthenticateAsync(bool async, TokenRequestContext context, CancellationToken cancellationToken)
		{
			AccessToken accessToken;
			using (HttpMessage message = this.CreateHttpMessage(this.CreateRequest(context.Scopes)))
			{
				if (async)
				{
					await this.Pipeline.HttpPipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
				}
				else
				{
					this.Pipeline.HttpPipeline.Send(message, cancellationToken);
				}
				accessToken = await this.HandleResponseAsync(async, context, message, cancellationToken).ConfigureAwait(false);
			}
			return accessToken;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000B6BC File Offset: 0x000098BC
		protected virtual async ValueTask<AccessToken> HandleResponseAsync(bool async, TokenRequestContext context, HttpMessage message, CancellationToken cancellationToken)
		{
			Exception ex = null;
			Response response = message.Response;
			try
			{
				if (response.Status == 200)
				{
					if (cancellationToken.IsCancellationRequested)
					{
						throw new TaskCanceledException();
					}
					JsonDocument jsonDocument;
					if (async)
					{
						jsonDocument = await JsonDocument.ParseAsync(response.ContentStream, default(JsonDocumentOptions), cancellationToken).ConfigureAwait(false);
					}
					else
					{
						jsonDocument = JsonDocument.Parse(response.ContentStream, default(JsonDocumentOptions));
					}
					using (JsonDocument jsonDocument2 = jsonDocument)
					{
						JsonElement rootElement = jsonDocument2.RootElement;
						return ManagedIdentitySource.GetTokenFromResponse(in rootElement);
					}
				}
			}
			catch (JsonException ex2)
			{
				throw new CredentialUnavailableException("Managed Identity response was not in the expected format. See the inner exception for details.", ex2);
			}
			catch (Exception ex3) when (response.Status == 200)
			{
				throw new RequestFailedException("Response from Managed Identity was successful, but the operation timed out prior to completion.", ex3);
			}
			catch (Exception ex)
			{
			}
			if (response.Status == 403)
			{
				string text = response.Content.ToString();
				if (text.Contains("unreachable"))
				{
					throw new CredentialUnavailableException("Managed Identity response was not in the expected format. See the inner exception for details.", new Exception(text));
				}
			}
			throw new RequestFailedException(response, ex);
		}

		// Token: 0x060003DE RID: 990
		protected abstract Request CreateRequest(string[] scopes);

		// Token: 0x060003DF RID: 991 RVA: 0x0000B710 File Offset: 0x00009910
		protected virtual HttpMessage CreateHttpMessage(Request request)
		{
			return new HttpMessage(request, this._responseClassifier);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000B720 File Offset: 0x00009920
		internal static async Task<string> GetMessageFromResponse(Response response, bool async, CancellationToken cancellationToken)
		{
			string text;
			if (((response != null) ? response.ContentStream : null) == null || !response.ContentStream.CanRead || response.ContentStream.Length == 0L)
			{
				text = null;
			}
			else
			{
				try
				{
					response.ContentStream.Position = 0L;
					JsonDocument jsonDocument;
					if (async)
					{
						jsonDocument = await JsonDocument.ParseAsync(response.ContentStream, default(JsonDocumentOptions), cancellationToken).ConfigureAwait(false);
					}
					else
					{
						jsonDocument = JsonDocument.Parse(response.ContentStream, default(JsonDocumentOptions));
					}
					using (JsonDocument jsonDocument2 = jsonDocument)
					{
						JsonElement rootElement = jsonDocument2.RootElement;
						text = ManagedIdentitySource.GetMessageFromResponse(in rootElement);
					}
				}
				catch
				{
					text = "Response was not in a valid json format.";
				}
			}
			return text;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000B774 File Offset: 0x00009974
		protected static string GetMessageFromResponse(in JsonElement root)
		{
			foreach (JsonProperty jsonProperty in root.EnumerateObject())
			{
				if (jsonProperty.Name == "Message")
				{
					return jsonProperty.Value.GetString();
				}
			}
			return null;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000B7F0 File Offset: 0x000099F0
		private static AccessToken GetTokenFromResponse(in JsonElement root)
		{
			string text = null;
			DateTimeOffset? dateTimeOffset = null;
			foreach (JsonProperty jsonProperty in root.EnumerateObject())
			{
				string name = jsonProperty.Name;
				if (!(name == "access_token"))
				{
					if (name == "expires_on")
					{
						dateTimeOffset = ManagedIdentitySource.TryParseExpiresOn(jsonProperty.Value);
					}
				}
				else
				{
					text = jsonProperty.Value.GetString();
				}
			}
			if (text == null || dateTimeOffset == null)
			{
				throw new AuthenticationFailedException("Invalid response, the authentication response was not in the expected format.");
			}
			return new AccessToken(text, dateTimeOffset.Value);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000B8B8 File Offset: 0x00009AB8
		private static DateTimeOffset? TryParseExpiresOn(JsonElement jsonExpiresOn)
		{
			long num;
			if ((jsonExpiresOn.ValueKind == 4 && jsonExpiresOn.TryGetInt64(ref num)) || (jsonExpiresOn.ValueKind == 3 && long.TryParse(jsonExpiresOn.GetString(), out num)))
			{
				return new DateTimeOffset?(DateTimeOffset.FromUnixTimeSeconds(num));
			}
			DateTimeOffset dateTimeOffset;
			if (jsonExpiresOn.ValueKind == 3 && DateTimeOffset.TryParse(jsonExpiresOn.GetString(), CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTimeOffset))
			{
				return new DateTimeOffset?(dateTimeOffset);
			}
			return null;
		}

		// Token: 0x04000239 RID: 569
		internal const string AuthenticationResponseInvalidFormatError = "Invalid response, the authentication response was not in the expected format.";

		// Token: 0x0400023A RID: 570
		internal const string UnexpectedResponse = "Managed Identity response was not in the expected format. See the inner exception for details.";

		// Token: 0x0400023B RID: 571
		private ManagedIdentitySource.ManagedIdentityResponseClassifier _responseClassifier;

		// Token: 0x020000FA RID: 250
		private class ManagedIdentityResponseClassifier : ResponseClassifier
		{
			// Token: 0x060005AB RID: 1451 RVA: 0x000171CC File Offset: 0x000153CC
			public override bool IsRetriableResponse(HttpMessage message)
			{
				int status = message.Response.Status;
				return status == 404 || status == 410 || (status != 502 && base.IsRetriableResponse(message));
			}
		}
	}
}
