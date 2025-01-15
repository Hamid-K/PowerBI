using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000232 RID: 562
	internal interface ILatch
	{
		// Token: 0x060012B6 RID: 4790
		void Latch();

		// Token: 0x060012B7 RID: 4791
		void UnLatch();
	}
}
