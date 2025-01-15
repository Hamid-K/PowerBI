using System;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x020005B8 RID: 1464
	public interface IServiceEndpoint
	{
		// Token: 0x17001ED8 RID: 7896
		// (get) Token: 0x06005303 RID: 21251
		string Host { get; }

		// Token: 0x17001ED9 RID: 7897
		// (get) Token: 0x06005304 RID: 21252
		int Port { get; }
	}
}
