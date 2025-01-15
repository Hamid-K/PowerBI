using System;
using Azure.Core;
using Azure.Core.Pipeline;
using Microsoft.Identity.Client;

namespace Azure.Identity
{
	// Token: 0x0200002D RID: 45
	internal class CredentialPipeline
	{
		// Token: 0x0600010B RID: 267 RVA: 0x00004E34 File Offset: 0x00003034
		private CredentialPipeline(TokenCredentialOptions options)
		{
			this.HttpPipeline = HttpPipelineBuilder.Build(new HttpPipelineOptions(options)
			{
				RequestFailedDetailsParser = new ManagedIdentityRequestFailedDetailsParser()
			});
			this.Diagnostics = new ClientDiagnostics(options, null);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004E78 File Offset: 0x00003078
		public CredentialPipeline(HttpPipeline httpPipeline, ClientDiagnostics diagnostics)
		{
			this.HttpPipeline = httpPipeline;
			this.Diagnostics = diagnostics;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004E90 File Offset: 0x00003090
		public static CredentialPipeline GetInstance(TokenCredentialOptions options, bool IsManagedIdentityCredential = false)
		{
			CredentialPipeline credentialPipeline;
			if (IsManagedIdentityCredential)
			{
				credentialPipeline = CredentialPipeline.configureOptionsForManagedIdentity(options);
			}
			else if (options != null)
			{
				credentialPipeline = new CredentialPipeline(options);
			}
			else
			{
				credentialPipeline = CredentialPipeline.s_singleton.Value;
			}
			return credentialPipeline;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004EC8 File Offset: 0x000030C8
		private static CredentialPipeline configureOptionsForManagedIdentity(TokenCredentialOptions options)
		{
			DefaultAzureCredentialOptions defaultAzureCredentialOptions = options as DefaultAzureCredentialOptions;
			TokenCredentialOptions tokenCredentialOptions;
			if (defaultAzureCredentialOptions != null)
			{
				tokenCredentialOptions = defaultAzureCredentialOptions.Clone<DefaultAzureCredentialOptions>();
			}
			else
			{
				tokenCredentialOptions = ((options != null) ? options.Clone<TokenCredentialOptions>() : null) ?? new TokenCredentialOptions();
			}
			TokenCredentialOptions tokenCredentialOptions2 = tokenCredentialOptions;
			tokenCredentialOptions2.Retry.MaxRetries = 5;
			tokenCredentialOptions = tokenCredentialOptions2;
			if (tokenCredentialOptions.RetryPolicy == null)
			{
				tokenCredentialOptions.RetryPolicy = new DefaultAzureCredentialImdsRetryPolicy(tokenCredentialOptions2.Retry, null);
			}
			tokenCredentialOptions2.IsChainedCredential = tokenCredentialOptions2 is DefaultAzureCredentialOptions;
			return new CredentialPipeline(tokenCredentialOptions2);
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00004F3F File Offset: 0x0000313F
		public HttpPipeline HttpPipeline { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00004F47 File Offset: 0x00003147
		public ClientDiagnostics Diagnostics { get; }

		// Token: 0x06000111 RID: 273 RVA: 0x00004F4F File Offset: 0x0000314F
		public IConfidentialClientApplication CreateMsalConfidentialClient(string tenantId, string clientId, string clientSecret)
		{
			return ConfidentialClientApplicationBuilder.Create(clientId).WithHttpClientFactory(new HttpPipelineClientFactory(this.HttpPipeline)).WithTenantId(tenantId)
				.WithClientSecret(clientSecret)
				.Build();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004F78 File Offset: 0x00003178
		public CredentialDiagnosticScope StartGetTokenScope(string fullyQualifiedMethod, TokenRequestContext context)
		{
			IScopeHandler scopeHandler = ScopeGroupHandler.Current ?? CredentialPipeline._defaultScopeHandler;
			CredentialDiagnosticScope credentialDiagnosticScope = new CredentialDiagnosticScope(this.Diagnostics, fullyQualifiedMethod, context, scopeHandler);
			credentialDiagnosticScope.Start();
			return credentialDiagnosticScope;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004FAC File Offset: 0x000031AC
		public CredentialDiagnosticScope StartGetTokenScopeGroup(string fullyQualifiedMethod, TokenRequestContext context)
		{
			ScopeGroupHandler scopeGroupHandler = new ScopeGroupHandler(fullyQualifiedMethod);
			CredentialDiagnosticScope credentialDiagnosticScope = new CredentialDiagnosticScope(this.Diagnostics, fullyQualifiedMethod, context, scopeGroupHandler);
			credentialDiagnosticScope.Start();
			return credentialDiagnosticScope;
		}

		// Token: 0x040000B6 RID: 182
		private static readonly Lazy<CredentialPipeline> s_singleton = new Lazy<CredentialPipeline>(() => new CredentialPipeline(new TokenCredentialOptions()));

		// Token: 0x040000B7 RID: 183
		private static readonly IScopeHandler _defaultScopeHandler = new CredentialPipeline.ScopeHandler();

		// Token: 0x020000AB RID: 171
		private class CredentialResponseClassifier : ResponseClassifier
		{
			// Token: 0x060004FE RID: 1278 RVA: 0x0001047A File Offset: 0x0000E67A
			public override bool IsRetriableResponse(HttpMessage message)
			{
				return base.IsRetriableResponse(message) || message.Response.Status == 404;
			}
		}

		// Token: 0x020000AC RID: 172
		private class ScopeHandler : IScopeHandler
		{
			// Token: 0x06000500 RID: 1280 RVA: 0x000104A1 File Offset: 0x0000E6A1
			public DiagnosticScope CreateScope(ClientDiagnostics diagnostics, string name)
			{
				return diagnostics.CreateScope(name, 0);
			}

			// Token: 0x06000501 RID: 1281 RVA: 0x000104AB File Offset: 0x0000E6AB
			public void Start(string name, in DiagnosticScope scope)
			{
				scope.Start();
			}

			// Token: 0x06000502 RID: 1282 RVA: 0x000104B3 File Offset: 0x0000E6B3
			public void Dispose(string name, in DiagnosticScope scope)
			{
				scope.Dispose();
			}

			// Token: 0x06000503 RID: 1283 RVA: 0x000104BB File Offset: 0x0000E6BB
			public void Fail(string name, in DiagnosticScope scope, Exception exception)
			{
				scope.Failed(exception);
			}

			// Token: 0x06000505 RID: 1285 RVA: 0x000104CC File Offset: 0x0000E6CC
			void IScopeHandler.Start(string name, in DiagnosticScope scope)
			{
				this.Start(name, in scope);
			}

			// Token: 0x06000506 RID: 1286 RVA: 0x000104D6 File Offset: 0x0000E6D6
			void IScopeHandler.Dispose(string name, in DiagnosticScope scope)
			{
				this.Dispose(name, in scope);
			}

			// Token: 0x06000507 RID: 1287 RVA: 0x000104E0 File Offset: 0x0000E6E0
			void IScopeHandler.Fail(string name, in DiagnosticScope scope, Exception exception)
			{
				this.Fail(name, in scope, exception);
			}
		}
	}
}
