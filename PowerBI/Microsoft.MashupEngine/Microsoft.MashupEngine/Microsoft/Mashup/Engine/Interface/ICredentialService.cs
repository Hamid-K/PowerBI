using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000046 RID: 70
	public interface ICredentialService
	{
		// Token: 0x0600013E RID: 318
		ResourceCredentialCollection RefreshCredential(IResource resource, bool forceRefresh = false);

		// Token: 0x0600013F RID: 319
		void UpdateExchangeCredential(IResource resource, ResourceCredentialCollection updatedCredential);

		// Token: 0x06000140 RID: 320
		bool TryGetCredentials(IResource resource, out ResourceCredentialCollection credentials);
	}
}
