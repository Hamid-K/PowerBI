using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001DD RID: 477
	internal interface ISizeBasedCacheObjectSerializer : IDataCacheObjectSerializer
	{
		// Token: 0x06000F8D RID: 3981
		int EstimateSerializationSize(object value);
	}
}
