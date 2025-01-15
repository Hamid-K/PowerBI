using System;
using System.Collections.Generic;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x0200208E RID: 8334
	public abstract class SecureTokenServicesStorage
	{
		// Token: 0x0600CBF9 RID: 52217
		public abstract List<ISecureTokenService> GetSecureTokenServices();

		// Token: 0x0600CBFA RID: 52218
		public abstract void AddSecureTokenService(ISecureTokenService service);

		// Token: 0x0600CBFB RID: 52219
		public abstract void DeleteSecureTokenService(ISecureTokenService service);
	}
}
