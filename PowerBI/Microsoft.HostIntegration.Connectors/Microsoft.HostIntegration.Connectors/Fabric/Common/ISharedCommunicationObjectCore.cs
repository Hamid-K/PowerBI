using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000418 RID: 1048
	internal interface ISharedCommunicationObjectCore
	{
		// Token: 0x06002467 RID: 9319
		bool OnOpen();

		// Token: 0x06002468 RID: 9320
		bool OnClose();

		// Token: 0x06002469 RID: 9321
		void OnAbort();
	}
}
