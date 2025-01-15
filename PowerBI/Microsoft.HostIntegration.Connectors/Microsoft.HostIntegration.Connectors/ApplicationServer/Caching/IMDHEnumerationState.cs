using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200023F RID: 575
	internal interface IMDHEnumerationState
	{
		// Token: 0x06001312 RID: 4882
		MDHNode getNextNode();

		// Token: 0x06001313 RID: 4883
		MDHNode getContainer();
	}
}
