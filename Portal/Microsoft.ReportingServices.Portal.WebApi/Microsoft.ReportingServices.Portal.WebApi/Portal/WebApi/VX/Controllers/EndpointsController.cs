using System;
using System.Web.Http;
using Microsoft.ReportingServices.Portal.WebApi.VX.Models;

namespace Microsoft.ReportingServices.Portal.WebApi.VX.Controllers
{
	// Token: 0x02000008 RID: 8
	public sealed class EndpointsController : ApiController
	{
		// Token: 0x0600000A RID: 10 RVA: 0x0000214C File Offset: 0x0000034C
		[Route("~/api/endpoints")]
		[HttpGet]
		public EndpointInfo GetEndpointInfo()
		{
			return new EndpointInfo
			{
				SupportedEndpoints = EndpointsController.apiVersions
			};
		}

		// Token: 0x04000038 RID: 56
		private static readonly string[] apiVersions = new string[] { "v1.0", "v2.0" };
	}
}
