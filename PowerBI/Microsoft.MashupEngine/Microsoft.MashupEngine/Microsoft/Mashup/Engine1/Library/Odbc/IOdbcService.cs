using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005B0 RID: 1456
	internal interface IOdbcService
	{
		// Token: 0x06002DE7 RID: 11751
		IOdbcConnection CreateConnection(OdbcConnectionProperties args);

		// Token: 0x06002DE8 RID: 11752
		IList<string> GetInstalledDrivers();

		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x06002DE9 RID: 11753
		int PageSize { get; }
	}
}
