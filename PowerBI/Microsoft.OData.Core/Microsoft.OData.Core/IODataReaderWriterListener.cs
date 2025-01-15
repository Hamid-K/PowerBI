using System;

namespace Microsoft.OData
{
	// Token: 0x0200003D RID: 61
	internal interface IODataReaderWriterListener
	{
		// Token: 0x0600021C RID: 540
		void OnException();

		// Token: 0x0600021D RID: 541
		void OnCompleted();
	}
}
