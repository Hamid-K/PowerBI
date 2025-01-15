using System;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.PowerBI.ReportServer.WebApi.Pbix;
using Microsoft.ReportingServices.Portal.ODataClient.V2;

namespace Microsoft.PowerBI.ReportServer.WebApi.Catalog
{
	// Token: 0x02000041 RID: 65
	public interface ICatalogService
	{
		// Token: 0x06000128 RID: 296
		Task<PbixComponents> GetPbixComponentsAsync(IPrincipal userPrincipal, Guid catalogItemId, string requestId, string sessionId, PbixReportComponents pbixMetadata);

		// Token: 0x06000129 RID: 297
		Task<PowerBIReport> GetPbixReportMetadataAsync(IPrincipal userPrincipal, Guid catalogItemId);
	}
}
