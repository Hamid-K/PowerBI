using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019AD RID: 6573
	public abstract class DelegatingCredentialService : ICredentialService
	{
		// Token: 0x0600A693 RID: 42643 RVA: 0x002275E4 File Offset: 0x002257E4
		public DelegatingCredentialService(ICredentialService credentialService)
		{
			this.credentialService = credentialService;
		}

		// Token: 0x0600A694 RID: 42644 RVA: 0x002275F3 File Offset: 0x002257F3
		public virtual void UpdateExchangeCredential(IResource resource, ResourceCredentialCollection credentials)
		{
			this.credentialService.UpdateExchangeCredential(resource, credentials);
		}

		// Token: 0x0600A695 RID: 42645 RVA: 0x00227602 File Offset: 0x00225802
		public virtual ResourceCredentialCollection RefreshCredential(IResource resource, bool forceRefresh = false)
		{
			return this.credentialService.RefreshCredential(resource, forceRefresh);
		}

		// Token: 0x0600A696 RID: 42646 RVA: 0x00227611 File Offset: 0x00225811
		public virtual bool TryGetCredentials(IResource resource, out ResourceCredentialCollection credentials)
		{
			return this.credentialService.TryGetCredentials(resource, out credentials);
		}

		// Token: 0x040056B0 RID: 22192
		private readonly ICredentialService credentialService;
	}
}
