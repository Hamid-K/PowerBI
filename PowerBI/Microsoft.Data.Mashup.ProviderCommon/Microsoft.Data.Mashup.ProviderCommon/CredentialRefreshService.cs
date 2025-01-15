using System;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.EngineHost.Services;
using Microsoft.Mashup.Storage;

namespace Microsoft.Data.Mashup.ProviderCommon
{
	// Token: 0x02000004 RID: 4
	internal class CredentialRefreshService : ICredentialRefreshService
	{
		// Token: 0x06000014 RID: 20 RVA: 0x0000237D File Offset: 0x0000057D
		public CredentialRefreshService(ProviderContext context, ConnectionContext connection, IEvaluationConstants evaluationConstants = null)
		{
			this.providerContext = context;
			this.connectionContext = connection;
			this.evaluationConstants = evaluationConstants;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000239C File Offset: 0x0000059C
		public CredentialDataCollection Refresh(IResource resource, CredentialDataCollection previousCredential)
		{
			try
			{
				CredentialDataCollection credentialDataCollection = new CredentialDataCollection();
				ResourceCredentialCollection resourceCredentialCollection = previousCredential.ToResourceCredentials(resource, null, true);
				ResourceCredentialCollection resourceCredentialCollection2 = this.connectionContext.RefreshCredential(resourceCredentialCollection);
				credentialDataCollection.Credentials.AddRange(resourceCredentialCollection2.Select(new Func<IResourceCredential, CredentialData>(CredentialData.From)));
				return credentialDataCollection;
			}
			catch (Exception ex) when (ProviderTracing.TraceIsSafeException("CredentialRefreshService/Refresh", ex, this.evaluationConstants, resource))
			{
			}
			throw new InvalidResourceCredentialsException(resource, ProviderErrorStrings.OAuthRefreshFailed, null, null);
		}

		// Token: 0x04000003 RID: 3
		private readonly ProviderContext providerContext;

		// Token: 0x04000004 RID: 4
		private readonly ConnectionContext connectionContext;

		// Token: 0x04000005 RID: 5
		private readonly IEvaluationConstants evaluationConstants;
	}
}
