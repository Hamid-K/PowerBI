using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020003A8 RID: 936
	internal interface IBufferManager
	{
		// Token: 0x06002149 RID: 8521
		byte[] TakeBuffer(int size);

		// Token: 0x0600214A RID: 8522
		void ReleaseBuffer(byte[] buffer);

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x0600214B RID: 8523
		int MaxMessageSize { get; }

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x0600214C RID: 8524
		long MaxBufferPoolSize { get; }
	}
}
