using System;
using System.Net.Http;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.Identity
{
	// Token: 0x0200007E RID: 126
	internal class ServiceFabricManagedIdentitySource : ManagedIdentitySource
	{
		// Token: 0x0600044D RID: 1101 RVA: 0x0000D394 File Offset: 0x0000B594
		public static ManagedIdentitySource TryCreate(ManagedIdentityClientOptions options)
		{
			string identityEndpoint = EnvironmentVariables.IdentityEndpoint;
			string identityHeader = EnvironmentVariables.IdentityHeader;
			string identityServerThumbprint = EnvironmentVariables.IdentityServerThumbprint;
			if (string.IsNullOrEmpty(identityEndpoint) || string.IsNullOrEmpty(identityHeader) || string.IsNullOrEmpty(identityServerThumbprint))
			{
				return null;
			}
			Uri uri;
			if (!Uri.TryCreate(identityEndpoint, UriKind.Absolute, out uri))
			{
				throw new AuthenticationFailedException("The environment variable IDENTITY_ENDPOINT contains an invalid Uri.");
			}
			CredentialPipeline credentialPipeline = options.Pipeline;
			if (!options.PreserveTransport)
			{
				credentialPipeline = new CredentialPipeline(HttpPipelineBuilder.Build(new TokenCredentialOptions
				{
					Transport = ServiceFabricManagedIdentitySource.GetServiceFabricMITransport()
				}, Array.Empty<HttpPipelinePolicy>()), credentialPipeline.Diagnostics);
			}
			return new ServiceFabricManagedIdentitySource(credentialPipeline, uri, identityHeader, options);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000D425 File Offset: 0x0000B625
		internal static HttpClientTransport GetServiceFabricMITransport()
		{
			HttpClientHandler httpClientHandler = new HttpClientHandler();
			Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> func;
			if ((func = ServiceFabricManagedIdentitySource.<>O.<0>__ValidateMsiServerCertificate) == null)
			{
				func = (ServiceFabricManagedIdentitySource.<>O.<0>__ValidateMsiServerCertificate = new Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool>(ServiceFabricManagedIdentitySource.ValidateMsiServerCertificate));
			}
			httpClientHandler.ServerCertificateCustomValidationCallback = func;
			return new HttpClientTransport(httpClientHandler);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000D454 File Offset: 0x0000B654
		internal ServiceFabricManagedIdentitySource(CredentialPipeline pipeline, Uri endpoint, string identityHeaderValue, ManagedIdentityClientOptions options)
			: base(pipeline)
		{
			this._endpoint = endpoint;
			this._identityHeaderValue = identityHeaderValue;
			this._clientId = options.ClientId;
			ResourceIdentifier resourceIdentifier = options.ResourceIdentifier;
			this._resourceId = ((resourceIdentifier != null) ? resourceIdentifier.ToString() : null);
			if (!string.IsNullOrEmpty(options.ClientId) || null != options.ResourceIdentifier)
			{
				AzureIdentityEventSource.Singleton.ServiceFabricManagedIdentityRuntimeConfigurationNotSupported();
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000D4C4 File Offset: 0x0000B6C4
		protected override Request CreateRequest(string[] scopes)
		{
			string text = ScopeUtilities.ScopesToResource(scopes);
			Request request = base.Pipeline.HttpPipeline.CreateRequest();
			request.Method = RequestMethod.Get;
			request.Headers.Add("secret", this._identityHeaderValue);
			request.Uri.Reset(this._endpoint);
			request.Uri.AppendQuery("api-version", "2019-07-01-preview");
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

		// Token: 0x06000451 RID: 1105 RVA: 0x0000D58B File Offset: 0x0000B78B
		private static bool ValidateMsiServerCertificate(HttpRequestMessage message, X509Certificate2 cert, X509Chain certChain, SslPolicyErrors policyErrors)
		{
			return policyErrors == SslPolicyErrors.None || string.Compare(cert.GetCertHashString(), EnvironmentVariables.IdentityServerThumbprint, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x0400026E RID: 622
		private const string ServiceFabricMsiApiVersion = "2019-07-01-preview";

		// Token: 0x0400026F RID: 623
		private const string IdentityEndpointInvalidUriError = "The environment variable IDENTITY_ENDPOINT contains an invalid Uri.";

		// Token: 0x04000270 RID: 624
		private readonly Uri _endpoint;

		// Token: 0x04000271 RID: 625
		private readonly string _identityHeaderValue;

		// Token: 0x04000272 RID: 626
		private readonly string _clientId;

		// Token: 0x04000273 RID: 627
		private readonly string _resourceId;

		// Token: 0x02000122 RID: 290
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000651 RID: 1617
			public static Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> <0>__ValidateMsiServerCertificate;
		}
	}
}
