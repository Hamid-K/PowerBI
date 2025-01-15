using System;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.Communication
{
	// Token: 0x0200044D RID: 1101
	[Serializable]
	public enum BindingSecurityMode
	{
		// Token: 0x04000BDE RID: 3038
		None,
		// Token: 0x04000BDF RID: 3039
		Transport,
		// Token: 0x04000BE0 RID: 3040
		Message,
		// Token: 0x04000BE1 RID: 3041
		TransportWithMessageCredential,
		// Token: 0x04000BE2 RID: 3042
		TransportCredentialOnly
	}
}
