using System;
using System.Data;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x02000010 RID: 16
	public interface ISupportsSystemDataReader
	{
		// Token: 0x06000027 RID: 39
		IDataReader CreateDataReader(CommandBehavior behavior);
	}
}
