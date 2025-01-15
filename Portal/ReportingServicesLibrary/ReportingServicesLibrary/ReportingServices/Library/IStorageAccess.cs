using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200010A RID: 266
	internal interface IStorageAccess : IDisposable
	{
		// Token: 0x06000A9E RID: 2718
		void Abort();

		// Token: 0x06000A9F RID: 2719
		void Commit();
	}
}
