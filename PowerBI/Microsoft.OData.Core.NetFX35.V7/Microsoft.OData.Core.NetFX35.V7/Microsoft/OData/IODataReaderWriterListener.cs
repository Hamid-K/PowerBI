using System;

namespace Microsoft.OData
{
	// Token: 0x02000016 RID: 22
	internal interface IODataReaderWriterListener
	{
		// Token: 0x06000095 RID: 149
		void OnException();

		// Token: 0x06000096 RID: 150
		void OnCompleted();
	}
}
