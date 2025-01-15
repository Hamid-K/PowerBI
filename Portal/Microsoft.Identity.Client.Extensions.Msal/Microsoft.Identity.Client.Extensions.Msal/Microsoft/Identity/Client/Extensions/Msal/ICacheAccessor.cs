using System;

namespace Microsoft.Identity.Client.Extensions.Msal
{
	// Token: 0x02000012 RID: 18
	internal interface ICacheAccessor
	{
		// Token: 0x0600003E RID: 62
		void Clear();

		// Token: 0x0600003F RID: 63
		byte[] Read();

		// Token: 0x06000040 RID: 64
		void Write(byte[] data);

		// Token: 0x06000041 RID: 65
		ICacheAccessor CreateForPersistenceValidation();
	}
}
