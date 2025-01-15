using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Storage;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019EB RID: 6635
	public interface ICredentialRefreshService
	{
		// Token: 0x0600A7EA RID: 42986
		CredentialDataCollection Refresh(IResource resource, CredentialDataCollection previousCredential);
	}
}
