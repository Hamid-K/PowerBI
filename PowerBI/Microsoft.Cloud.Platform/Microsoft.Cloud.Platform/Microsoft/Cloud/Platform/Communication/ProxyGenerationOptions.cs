using System;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004EA RID: 1258
	[Flags]
	public enum ProxyGenerationOptions
	{
		// Token: 0x04000DA0 RID: 3488
		None = 0,
		// Token: 0x04000DA1 RID: 3489
		IgnoreRemovableParams = 1,
		// Token: 0x04000DA2 RID: 3490
		TraceHttpResponseHeadersOnFaultException = 2
	}
}
