using System;
using Azure.Core;

namespace Azure.Identity
{
	// Token: 0x02000020 RID: 32
	internal class AppServiceManagedIdentitySource : ManagedIdentitySource
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003D7F File Offset: 0x00001F7F
		protected virtual string AppServiceMsiApiVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003D86 File Offset: 0x00001F86
		protected virtual string SecretHeaderName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003D8D File Offset: 0x00001F8D
		protected virtual string ClientIdHeaderName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003D94 File Offset: 0x00001F94
		protected static bool TryValidateEnvVars(string msiEndpoint, string secret, out Uri endpointUri)
		{
			endpointUri = null;
			if (string.IsNullOrEmpty(msiEndpoint) || string.IsNullOrEmpty(secret))
			{
				return false;
			}
			try
			{
				endpointUri = new Uri(msiEndpoint);
			}
			catch (FormatException ex)
			{
				throw new AuthenticationFailedException("The environment variable MSI_ENDPOINT contains an invalid Uri.", ex);
			}
			return true;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003DE0 File Offset: 0x00001FE0
		protected AppServiceManagedIdentitySource(CredentialPipeline pipeline, Uri endpoint, string secret, ManagedIdentityClientOptions options)
			: base(pipeline)
		{
			this._endpoint = endpoint;
			this._secret = secret;
			this._clientId = options.ClientId;
			ResourceIdentifier resourceIdentifier = options.ResourceIdentifier;
			this._resourceId = ((resourceIdentifier != null) ? resourceIdentifier.ToString() : null);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003E20 File Offset: 0x00002020
		protected override Request CreateRequest(string[] scopes)
		{
			string text = ScopeUtilities.ScopesToResource(scopes);
			Request request = base.Pipeline.HttpPipeline.CreateRequest();
			request.Method = RequestMethod.Get;
			request.Headers.Add(this.SecretHeaderName, this._secret);
			request.Uri.Reset(this._endpoint);
			request.Uri.AppendQuery("api-version", this.AppServiceMsiApiVersion);
			request.Uri.AppendQuery("resource", text);
			if (!string.IsNullOrEmpty(this._clientId))
			{
				request.Uri.AppendQuery(this.ClientIdHeaderName, this._clientId);
			}
			if (!string.IsNullOrEmpty(this._resourceId))
			{
				request.Uri.AppendQuery("mi_res_id", this._resourceId);
			}
			return request;
		}

		// Token: 0x04000055 RID: 85
		private const string MsiEndpointInvalidUriError = "The environment variable MSI_ENDPOINT contains an invalid Uri.";

		// Token: 0x04000056 RID: 86
		private readonly Uri _endpoint;

		// Token: 0x04000057 RID: 87
		private readonly string _secret;

		// Token: 0x04000058 RID: 88
		private readonly string _clientId;

		// Token: 0x04000059 RID: 89
		private readonly string _resourceId;
	}
}
