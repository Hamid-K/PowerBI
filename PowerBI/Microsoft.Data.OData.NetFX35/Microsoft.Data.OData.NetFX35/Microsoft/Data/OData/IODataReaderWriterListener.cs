using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000153 RID: 339
	internal interface IODataReaderWriterListener
	{
		// Token: 0x060008F5 RID: 2293
		void OnException();

		// Token: 0x060008F6 RID: 2294
		void OnCompleted();
	}
}
