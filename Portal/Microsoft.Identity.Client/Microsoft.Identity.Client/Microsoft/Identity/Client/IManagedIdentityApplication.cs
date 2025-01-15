using System;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000152 RID: 338
	public interface IManagedIdentityApplication : IApplicationBase
	{
		// Token: 0x060010CA RID: 4298
		AcquireTokenForManagedIdentityParameterBuilder AcquireTokenForManagedIdentity(string resource);
	}
}
