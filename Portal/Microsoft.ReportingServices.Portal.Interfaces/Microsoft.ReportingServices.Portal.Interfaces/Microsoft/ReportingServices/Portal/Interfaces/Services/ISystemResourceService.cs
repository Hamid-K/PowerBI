using System;
using System.Linq;
using System.Security.Principal;
using Model;

namespace Microsoft.ReportingServices.Portal.Interfaces.Services
{
	// Token: 0x0200008F RID: 143
	public interface ISystemResourceService
	{
		// Token: 0x06000461 RID: 1121
		IQueryable<SystemResource> GetSystemResources(IPrincipal userPrincipal);

		// Token: 0x06000462 RID: 1122
		SystemResource GetSystemResourceByTypeName(IPrincipal userPrincipal, string typeName);

		// Token: 0x06000463 RID: 1123
		SystemResource InstallSystemResourcePackage(IPrincipal userPrincipal, byte[] packageBytes, string packageFileName, string typeName);

		// Token: 0x06000464 RID: 1124
		bool DeleteSystemResource(IPrincipal userPrincipal, Guid key);

		// Token: 0x06000465 RID: 1125
		bool TryGetPayload(IPrincipal userPrincipal, string typeName, string itemName, out string contentType, out string filename, out byte[] bytes);
	}
}
