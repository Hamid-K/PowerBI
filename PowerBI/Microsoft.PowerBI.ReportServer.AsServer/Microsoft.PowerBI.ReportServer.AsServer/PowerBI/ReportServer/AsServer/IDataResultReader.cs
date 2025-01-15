using System;
using System.Data;

namespace Microsoft.PowerBI.ReportServer.AsServer
{
	// Token: 0x02000011 RID: 17
	public interface IDataResultReader : IDisposable
	{
		// Token: 0x06000059 RID: 89
		DataSet Evaluate();
	}
}
