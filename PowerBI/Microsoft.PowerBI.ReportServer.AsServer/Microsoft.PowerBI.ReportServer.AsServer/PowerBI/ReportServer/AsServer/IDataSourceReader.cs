using System;

namespace Microsoft.PowerBI.ReportServer.AsServer
{
	// Token: 0x02000015 RID: 21
	public interface IDataSourceReader : IDisposable
	{
		// Token: 0x0600007A RID: 122
		IDataResultReader ExecuteReader(string DiscoverDataSourceText);
	}
}
