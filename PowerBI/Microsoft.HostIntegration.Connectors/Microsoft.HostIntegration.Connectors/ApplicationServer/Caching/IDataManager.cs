using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200020D RID: 525
	internal interface IDataManager
	{
		// Token: 0x06001106 RID: 4358
		IDMContainer CreateContainer();

		// Token: 0x06001107 RID: 4359
		IDMContainer CreateContainer(IContainerSchema schema);
	}
}
