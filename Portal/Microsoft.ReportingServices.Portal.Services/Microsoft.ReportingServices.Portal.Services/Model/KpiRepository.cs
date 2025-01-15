using System;
using System.Security.Principal;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;

namespace Model
{
	// Token: 0x0200000A RID: 10
	internal class KpiRepository : Kpi
	{
		// Token: 0x06000019 RID: 25 RVA: 0x0000254D File Offset: 0x0000074D
		public KpiRepository(IPrincipal userPrincipal, ICatalogRepository catalogRepository)
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

		// Token: 0x04000038 RID: 56
		private readonly IPrincipal _userPrincipal;

		// Token: 0x04000039 RID: 57
		private readonly ICatalogRepository _catalogRepository;
	}
}
