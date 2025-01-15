using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;

namespace Azure.Identity
{
	// Token: 0x02000027 RID: 39
	internal class AzureArcManagedIdentitySource : ManagedIdentitySource
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00004384 File Offset: 0x00002584
		public static ManagedIdentitySource TryCreate(ManagedIdentityClientOptions options)
		{
			string identityEndpoint = EnvironmentVariables.IdentityEndpoint;
			string imdsEndpoint = EnvironmentVariables.ImdsEndpoint;
			if (string.IsNullOrEmpty(identityEndpoint) || string.IsNullOrEmpty(imdsEndpoint))
			{
				return null;
			}
			Uri uri;
			if (!Uri.TryCreate(identityEndpoint, UriKind.Absolute, out uri))
			{
				throw new AuthenticationFailedException("The environment variable IDENTITY_ENDPOINT contains an invalid Uri.");
			}
			return new AzureArcManagedIdentitySource(uri, options);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000043CC File Offset: 0x000025CC
		internal AzureArcManagedIdentitySource(Uri endpoint, ManagedIdentityClientOptions options)
			: base(options.Pipeline)
		{
			this._endpoint = endpoint;
			this._clientId = options.ClientId;
			if (!string.IsNullOrEmpty(this._clientId) || null != options.ResourceIdentifier)
			{
				AzureIdentityEventSource.Singleton.UserAssignedManagedIdentityNotSupported("Azure Arc");
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004424 File Offset: 0x00002624
		protected override Request CreateRequest(string[] scopes)
		{
			if (!string.IsNullOrEmpty(this._clientId))
			{
				throw new AuthenticationFailedException("User assigned identity is not supported by the Azure Arc Managed Identity Endpoint. To authenticate with the system assigned identity omit the client id when constructing the ManagedIdentityCredential, or if authenticating with the DefaultAzureCredential ensure the AZURE_CLIENT_ID environment variable is not set.");
			}
			string text = ScopeUtilities.ScopesToResource(scopes);
			Request request = base.Pipeline.HttpPipeline.CreateRequest();
			request.Method = RequestMethod.Get;
			request.Headers.Add("Metadata", "true");
			request.Uri.Reset(this._endpoint);
			request.Uri.AppendQuery("api-version", "2019-11-01");
			request.Uri.AppendQuery("resource", text);
			return request;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000044BC File Offset: 0x000026BC
		protected override async ValueTask<AccessToken> HandleResponseAsync(bool async, TokenRequestContext context, HttpMessage message, CancellationToken cancellationToken)
		{
			Response response = message.Response;
			if (response.Status == 401)
			{
				string text;
				if (!response.Headers.TryGetValue("WWW-Authenticate", out text))
				{
					throw new AuthenticationFailedException("Did not receive expected WWW-Authenticate header in the response from Azure Arc Managed Identity Endpoint.");
				}
				string[] array = text.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length != 2)
				{
					throw new AuthenticationFailedException("The WWW-Authenticate header in the response from Azure Arc Managed Identity Endpoint did not match the expected format.");
				}
				string text2 = array[1];
				this.ValidatePath(text2);
				string text3 = "Basic " + File.ReadAllText(array[1]);
				using (Request request = this.CreateRequest(context.Scopes))
				{
					request.Headers.Add("Authorization", text3);
					HttpMessage challengeResponseMessage = base.Pipeline.HttpPipeline.CreateMessage();
					challengeResponseMessage.Request.Method = request.Method;
					challengeResponseMessage.Request.Uri.Reset(request.Uri.ToUri());
					foreach (HttpHeader httpHeader in request.Headers)
					{
						challengeResponseMessage.Request.Headers.Add(httpHeader.Name, httpHeader.Value);
					}
					HttpMessage httpMessage = challengeResponseMessage;
					Response response2;
					if (async)
					{
						response2 = await base.Pipeline.HttpPipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
					}
					else
					{
						response2 = base.Pipeline.HttpPipeline.SendRequest(request, cancellationToken);
					}
					httpMessage.Response = response2;
					httpMessage = null;
					return await base.HandleResponseAsync(async, context, challengeResponseMessage, cancellationToken).ConfigureAwait(false);
				}
			}
			return await base.HandleResponseAsync(async, context, message, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004520 File Offset: 0x00002720
		private void ValidatePath(string filePath)
		{
			if (!filePath.EndsWith(".key"))
			{
				throw new AuthenticationFailedException("The secret key file failed validation. File name is invalid.");
			}
			if (Environment.OSVersion.Platform == PlatformID.Win32NT)
			{
				string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "AzureConnectedMachineAgent", "Tokens");
				if (!filePath.StartsWith(text))
				{
					throw new AuthenticationFailedException("The secret key file failed validation. File path is invalid.");
				}
			}
			if (Environment.OSVersion.Platform == PlatformID.Unix)
			{
				string text2 = Path.Combine(new string[] { "/", "var", "opt", "azcmagent", "tokens" });
				if (!filePath.StartsWith(text2))
				{
					throw new AuthenticationFailedException("The secret key file failed validation. File path is invalid.");
				}
			}
			if (new FileInfo(filePath).Length > 4096L)
			{
				throw new AuthenticationFailedException("The secret key file failed validation. File is too large.");
			}
		}

		// Token: 0x0400006F RID: 111
		private const string IdentityEndpointInvalidUriError = "The environment variable IDENTITY_ENDPOINT contains an invalid Uri.";

		// Token: 0x04000070 RID: 112
		private const string NoChallengeErrorMessage = "Did not receive expected WWW-Authenticate header in the response from Azure Arc Managed Identity Endpoint.";

		// Token: 0x04000071 RID: 113
		private const string InvalidChallangeErrorMessage = "The WWW-Authenticate header in the response from Azure Arc Managed Identity Endpoint did not match the expected format.";

		// Token: 0x04000072 RID: 114
		private const string UserAssignedNotSupportedErrorMessage = "User assigned identity is not supported by the Azure Arc Managed Identity Endpoint. To authenticate with the system assigned identity omit the client id when constructing the ManagedIdentityCredential, or if authenticating with the DefaultAzureCredential ensure the AZURE_CLIENT_ID environment variable is not set.";

		// Token: 0x04000073 RID: 115
		private const string ArcApiVersion = "2019-11-01";

		// Token: 0x04000074 RID: 116
		private readonly string _clientId;

		// Token: 0x04000075 RID: 117
		private readonly Uri _endpoint;
	}
}
