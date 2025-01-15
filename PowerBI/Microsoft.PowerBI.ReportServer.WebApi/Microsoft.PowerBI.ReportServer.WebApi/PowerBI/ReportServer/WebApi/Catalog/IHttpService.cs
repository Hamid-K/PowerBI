using System;
using System.Net.Http;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.ReportServer.WebApi.Catalog
{
	// Token: 0x02000042 RID: 66
	internal interface IHttpService
	{
		// Token: 0x0600012A RID: 298
		Task<HttpResponseMessage> InvokeApi(IPrincipal userPrincipal, Uri uriToInvoke);

		// Token: 0x0600012B RID: 299
		Task<HttpResponseMessage> InvokeApiWithTrustedProcessToken(IPrincipal userPrincipal, Uri catalogItemContentUrl);
	}
}
