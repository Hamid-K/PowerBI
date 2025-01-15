using System;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x0200003A RID: 58
	internal interface IODataStreamListener
	{
		// Token: 0x06000211 RID: 529
		void StreamRequested();

		// Token: 0x06000212 RID: 530
		Task StreamRequestedAsync();

		// Token: 0x06000213 RID: 531
		void StreamDisposed();
	}
}
