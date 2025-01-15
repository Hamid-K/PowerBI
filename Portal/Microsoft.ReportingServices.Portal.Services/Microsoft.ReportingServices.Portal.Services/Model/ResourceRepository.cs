using System;
using System.Security.Principal;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;

namespace Model
{
	// Token: 0x0200000F RID: 15
	internal class ResourceRepository : Resource
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002A70 File Offset: 0x00000C70
		public ResourceRepository(IPrincipal userPrincipal, ICatalogRepository catalogRepository)
		{
			if (userPrincipal == null)
			{
				throw new ArgumentNullException("userPrincipal");
			}
			if (catalogRepository == null)
			{
				throw new ArgumentNullException("catalogRepository");
			}
			this._userPrincipal = userPrincipal;
			this._catalogRepository = catalogRepository;
		}

		// Token: 0x04000045 RID: 69
		private readonly IPrincipal _userPrincipal;

		// Token: 0x04000046 RID: 70
		private readonly ICatalogRepository _catalogRepository;
	}
}
