using System;
using System.Data;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x02000006 RID: 6
	public interface IDbConnectionWrapper
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14
		IDbConnection Connection { get; }

		// Token: 0x0600000F RID: 15
		bool IsOpen();
	}
}
