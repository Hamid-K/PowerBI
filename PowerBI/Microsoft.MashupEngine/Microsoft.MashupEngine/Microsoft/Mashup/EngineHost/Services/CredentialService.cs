using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Storage;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019A4 RID: 6564
	public class CredentialService : ICredentialService
	{
		// Token: 0x0600A667 RID: 42599 RVA: 0x00226EB3 File Offset: 0x002250B3
		public CredentialService(CredentialManager manager)
		{
			this.syncRoot = new object();
			this.manager = manager;
		}

		// Token: 0x0600A668 RID: 42600 RVA: 0x00226ED0 File Offset: 0x002250D0
		public void UpdateExchangeCredential(IResource resource, ResourceCredentialCollection credentials)
		{
			CredentialDataCollection credentialDataCollection = new CredentialDataCollection();
			IResourceCredential resourceCredential = credentials[0];
			ExchangeCredentialAdornment exchangeCredentialAdornment = (ExchangeCredentialAdornment)credentials[1];
			BasicAuthCredential basicAuthCredential = resourceCredential as BasicAuthCredential;
			string text;
			string text2;
			if (basicAuthCredential != null)
			{
				text = basicAuthCredential.Username;
				text2 = basicAuthCredential.Password;
			}
			else
			{
				text = null;
				text2 = null;
			}
			credentialDataCollection.Credentials.Add(new ExchangeCredentialData(exchangeCredentialAdornment.EmailAddress, text, text2, exchangeCredentialAdornment.EwsUrl, exchangeCredentialAdornment.EwsSupportedSchema));
			object obj = this.syncRoot;
			lock (obj)
			{
				this.manager.UpdateCredential(new Resource(resource), credentialDataCollection);
			}
		}

		// Token: 0x0600A669 RID: 42601 RVA: 0x00226F80 File Offset: 0x00225180
		public ResourceCredentialCollection RefreshCredential(IResource resource, bool forceRefresh = false)
		{
			object obj = this.syncRoot;
			ResourceCredentialCollection resourceCredentialCollection;
			lock (obj)
			{
				resourceCredentialCollection = this.manager.RefreshCredential(new Resource(resource), forceRefresh);
			}
			return resourceCredentialCollection;
		}

		// Token: 0x0600A66A RID: 42602 RVA: 0x00226FD0 File Offset: 0x002251D0
		public bool TryGetCredentials(IResource resource, out ResourceCredentialCollection credentials)
		{
			object obj = this.syncRoot;
			bool flag2;
			lock (obj)
			{
				flag2 = this.manager.TryGetCredentials(new Resource(resource), out credentials);
			}
			return flag2;
		}

		// Token: 0x0400569A RID: 22170
		private readonly object syncRoot;

		// Token: 0x0400569B RID: 22171
		private readonly CredentialManager manager;
	}
}
