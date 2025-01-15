using System;

namespace Microsoft.OData.Core
{
	// Token: 0x020000A2 RID: 162
	internal interface IODataReaderWriterListener
	{
		// Token: 0x06000612 RID: 1554
		void OnException();

		// Token: 0x06000613 RID: 1555
		void OnCompleted();
	}
}
