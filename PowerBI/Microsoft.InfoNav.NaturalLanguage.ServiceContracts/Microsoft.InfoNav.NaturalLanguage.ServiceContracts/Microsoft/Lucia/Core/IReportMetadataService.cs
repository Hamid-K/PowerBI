using System;
using System.Threading.Tasks;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000A9 RID: 169
	public interface IReportMetadataService
	{
		// Token: 0x0600036F RID: 879
		Task<ReportMetadataResult[]> LookupReportMetadataAsync(ReportMetadataRequest[] requests, string databaseName);
	}
}
