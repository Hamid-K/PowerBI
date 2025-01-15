using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001FC RID: 508
	internal interface IBaseDataNode
	{
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x0600108F RID: 4239
		object Key { get; }

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001090 RID: 4240
		object Data { get; }
	}
}
